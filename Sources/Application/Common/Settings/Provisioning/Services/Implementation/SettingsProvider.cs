using DrinkBuddy.Common.Settings.Provisioning.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace DrinkBuddy.Common.Settings.Provisioning.Services.Implementation
{
    [UsedImplicitly]
    public class SettingsProvider(
        IOptions<AppSettings> appSettings
    )
        : ISettingsProvider
    {
        public AppSettings AppSettings => appSettings.Value;
    }
}