using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DrinkBuddy.Presentation.Shared.Camera
{
    public partial class Camera
    {
        private string? _error;
        private string? _imageDataUrl;

        [Parameter]
        public required EventCallback<byte[]> OnPictureTaken { get; set; }

        private async Task OnFileSelectedAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.GetMultipleFiles(1).FirstOrDefault();
                if (file == null)
                {
                    return;
                }

                var resized = await file.RequestImageFileAsync("image/jpeg", 512, 512);

                await using var stream = resized.OpenReadStream(2_000_000);
                using var ms = new MemoryStream();

                await stream.CopyToAsync(ms);

                var base64 = Convert.ToBase64String(ms.ToArray());
                _imageDataUrl = $"data:image/jpeg;base64,{base64}";
                var imageBytes = ms.ToArray();

                await OnPictureTaken.InvokeAsync(imageBytes);
            }
            catch (Exception ex)
            {
                _error = ex.ToString();
            }
        }
    }
}