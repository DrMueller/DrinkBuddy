using DrinkBuddy.Presentation.Infrastructure.JavaScript.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DrinkBuddy.Presentation.Areas.Drinkrad
{
    public partial class Rad
    {
        private IJSObjectReference? _module;
        private double _targetDeg;

        [Inject]
        public required IJavaScriptLocator JsLocator { get; set; }

        [Inject]
        public required IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public int MaxFullTurns { get; set; } = 7;

        [Parameter]
        public int MinFullTurns { get; set; } = 4;

        [Parameter]
        public required IReadOnlyCollection<WheelSegment> Segments { get; set; }

        private int DurationMs { get; } = 3500;

        private bool IsSpinning { get; set; }

        private string WheelStyle => $@"
background: {BuildConicGradient()};

--spin-to: {_targetDeg}deg;
animation-duration: {DurationMs}ms;";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AssureJavascriptModuleAsync();
            }
        }

        private async Task AssureJavascriptModuleAsync()
        {
            var jsFilePath = await JsLocator.LocateJsFilePathAsync(this);
            _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", jsFilePath);
        }

        private string BuildConicGradient()
        {
            var n = Math.Max(1, Segments.Count);
            var step = 360.0 / n;

            var parts = new List<string>(n);
            for (var i = 0; i < n; i++)
            {
                var from = i * step;
                var to = (i + 1) * step;
                parts.Add($"{Segments.ElementAt(i).Color} {from:0.###}deg {to:0.###}deg");
            }

            return $"conic-gradient({string.Join(", ", parts)})";
        }

        private async Task SpinAsync()
        {
            if (Segments.Count == 0)
            {
                return;
            }

            IsSpinning = true;
            StateHasChanged();

            var rnd = Random.Shared;

            var fullTurns = rnd.Next(MinFullTurns, MaxFullTurns + 1);
            var extra = rnd.NextDouble() * 360.0;
            _targetDeg = fullTurns * 360.0 + extra;

            await _module!.InvokeVoidAsync("restartSpin");
            StateHasChanged();

            await Task.Delay(DurationMs + 500);
            IsSpinning = false;
        }

        public record WheelSegment(string Label, string Color);
    }
}