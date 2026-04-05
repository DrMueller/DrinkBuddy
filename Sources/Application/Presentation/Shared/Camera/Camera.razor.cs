using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DrinkBuddy.Presentation.Shared.Camera
{
    public partial class Camera
    {
        private string? _error;
        private string? _imageDataUrl;

        [Parameter]
        public required EventCallback<string> OnPictureTaken { get; set; }

        private async Task OnFileSelectedAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.GetMultipleFiles(1).FirstOrDefault();
                if (file == null)
                {
                    return;
                }

                // 🔥 KRITISCH: zuerst verkleinern
                var resized = await file.RequestImageFileAsync("image/jpeg", 1024, 1024);

                await using var stream = resized.OpenReadStream(2_000_000);
                using var ms = new MemoryStream();

                await stream.CopyToAsync(ms);

                var base64 = Convert.ToBase64String(ms.ToArray());

                _imageDataUrl = $"data:image/jpeg;base64,{base64}";

                await OnPictureTaken.InvokeAsync(_imageDataUrl);
            }
            catch (Exception ex)
            {
                _error = ex.ToString();
            }
        }
    }
}