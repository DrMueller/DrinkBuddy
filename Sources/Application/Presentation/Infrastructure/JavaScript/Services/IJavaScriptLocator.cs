using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Infrastructure.JavaScript.Services
{
    public interface IJavaScriptLocator
    {
        Task<string> LocateJsFilePathAsync(ComponentBase component);
    }
}