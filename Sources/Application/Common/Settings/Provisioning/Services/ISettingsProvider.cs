using DrinkBuddy.Common.Settings.Provisioning.Models;

namespace DrinkBuddy.Common.Settings.Provisioning.Services
{
    public interface ISettingsProvider
    {
        AppSettings AppSettings { get; }
    }
}