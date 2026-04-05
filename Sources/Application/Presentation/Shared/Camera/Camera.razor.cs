using DrinkBuddy.Presentation.Infrastructure.JavaScript.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DrinkBuddy.Presentation.Shared.Camera
{
    public partial class Camera
    {
        private string? _error;
        private string? _imageDataUrl;
        private IJSObjectReference? _module;

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
            try
            {
                _error = null;

                await _module!.InvokeVoidAsync("startCamera");
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
        }

        private async Task TakePictureAsync()
        {
            try
            {
                _error = null;

                var result = await _module!.InvokeAsync<string>("takePicture");
                _imageDataUrl = result;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
        }

        [PublicAPI]
        public class CameraStartResult
        {
            public int Height { get; set; }
            public bool Ok { get; set; }
            public int ReadyState { get; set; }
            public int Width { get; set; }
        }

        [PublicAPI]
        public class TakePictureResult
        {
            public string DataUrl { get; set; } = "";
            public int Height { get; set; }
            public int ReadyState { get; set; }
            public int VideoHeight { get; set; }
            public int VideoWidth { get; set; }
            public int Width { get; set; }
        }
    }
}