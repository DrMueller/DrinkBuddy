using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace modan.AS4.Presentation.Infrastructure.JavaScript.Services
{
    public interface IJavaScriptLocator
    {
        Task<string> LocateJsFilePathAsync(ComponentBase component);
    }
}