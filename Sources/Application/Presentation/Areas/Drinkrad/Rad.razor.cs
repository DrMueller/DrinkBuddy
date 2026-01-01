using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using modan.AS4.Presentation.Infrastructure.JavaScript.Services;

namespace DrinkBuddy.Presentation.Areas.Drinkrad
{
    public partial class Rad
    {
        private double _currentDeg; // "steht" danach
        private IJSObjectReference? _module;
        private double _targetDeg; // absolutes Ziel

        private bool IsSpinning { get; set; }

        private int DurationMs { get; set; } = 3500;

        [Inject]
        public required IJavaScriptLocator JsLocator { get; set; }

        [Inject]
        public required IJSRuntime JsRuntime { get; set; }

        public WheelSegment? LastResult { get; set; }

        [Parameter]
        public int MaxFullTurns { get; set; } = 7;

        [Parameter]
        public int MinFullTurns { get; set; } = 4;

        [Parameter]
        public IReadOnlyList<WheelSegment> Segments { get; set; } =
            new List<WheelSegment>
            {
                new("Rot", "#ef4444"),
                new("Gelb", "#facc15"),
                new("Gruen", "#22c55e"),
                new("Blau", "#3b82f6"),
                new("Orange", "#f97316"),
                new("Lila", "#a855f7")
            };

        [Inject]
        private IJSRuntime JS { get; set; } = default!;

        private string WheelStyle => $@"
background: {BuildConicGradient()};
transform: rotate({_currentDeg}deg);
--spin-to: {_targetDeg}deg;
animation-duration: {DurationMs}ms;";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AssureJavascriptModuleAsync();
            }
        }

        private static double Mod(double x, double m)
        {
            return (x % m + m) % m;
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
                parts.Add($"{Segments[i].Color} {from:0.###}deg {to:0.###}deg");
            }

            return $"conic-gradient({string.Join(", ", parts)})";
        }

        private async Task PropagateResultAsync()
        {
            // Endposition "fixieren"
            _currentDeg = _targetDeg % 360.0;

            var n = Segments.Count;
            var step = 360.0 / n;

            var effective = Mod(90.0 - _currentDeg, 360.0);
            var index = (int)Math.Floor(effective / step);
            if (index < 0)
            {
                index = 0;
            }

            if (index >= n)
            {
                index = n - 1;
            }

            LastResult = Segments[index];
            IsSpinning = false;

            StateHasChanged();
        }

        private async Task SpinAsync()
        {
            if (Segments.Count == 0)
            {
                return;
            }

            IsSpinning = true;
            LastResult = null;
            StateHasChanged();

            // Starttick via Timer (JS) waehrend Animation

            var rnd = Random.Shared;

            var fullTurns = rnd.Next(MinFullTurns, MaxFullTurns + 1);
            var extra = rnd.NextDouble() * 360.0;
            _targetDeg = _currentDeg + fullTurns * 360.0 + extra;

            await _module!.InvokeVoidAsync("restartSpin");
            StateHasChanged();

            await Task.Delay(DurationMs + 1000);
            await PropagateResultAsync();
        }

        public record WheelSegment(string Label, string Color);
    }
}