using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;

namespace DrinkBuddy.Domain.Integrations.SemKer
{
    public interface ISemKerClient
    {
        Task<string> SendAsync(DrinkRequest request);
    }
}
