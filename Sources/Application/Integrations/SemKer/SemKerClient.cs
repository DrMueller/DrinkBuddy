using DrinkBuddy.Application.IntegrationInterfaces.SemKer;
using DrinkBuddy.Common.Settings.Provisioning.Services;

namespace DrinkBuddy.Integrations.SemKer
{
    public class SemKerClient(ISettingsProvider settingsProvider) : ISemKerClient
    {
        private const string DeplyomentName = "gpt-4.1";
        private const string ModelId = "gpt-4.1";

        public Task<string> SendAsync(SemKerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}