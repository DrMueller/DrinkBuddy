using DrinkBuddy.Presentation.Infrastructure.JavaScript.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DrinkBuddy.Presentation.Shared.Camera
{
    public partial class Camera
    {
        private IJSObjectReference? _module;
        private string? _imageDataUrl;

        [Inject]
        public required IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public required EventCallback<string> OnPictureTaken { get; set; }

        [Inject]
        private IJavaScriptLocator JsLocator { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var jsFilePath = await JsLocator.LocateJsFilePathAsync(this);
                _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", jsFilePath);
            }
        }

        private async Task StartCameraAsync()
        {
            await _module!.InvokeVoidAsync("startCamera");
        }

        private async Task TakePictureAsync()
        {
            _imageDataUrl = await _module!.InvokeAsync<string>("takePicture");

            if (!string.IsNullOrWhiteSpace(_imageDataUrl))
            {
                await OnPictureTaken.InvokeAsync(_imageDataUrl);
            }
        }
    }
}