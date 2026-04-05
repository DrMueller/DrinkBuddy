using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DrinkBuddy.Presentation.Shared.Camera
{
    public partial class Camera
    {
        private string? _imageDataUrl;

        [Parameter]
        public required EventCallback<string> OnPictureTaken { get; set; }

        private async Task OnFileSelectedAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;

            using var stream = file.OpenReadStream(10_000_000);
            using var ms = new MemoryStream();

            await stream.CopyToAsync(ms);

            var base64 = Convert.ToBase64String(ms.ToArray());

            _imageDataUrl = $"data:{file.ContentType};base64,{base64}";
            await OnPictureTaken.InvokeAsync(_imageDataUrl);
        }
    }
}