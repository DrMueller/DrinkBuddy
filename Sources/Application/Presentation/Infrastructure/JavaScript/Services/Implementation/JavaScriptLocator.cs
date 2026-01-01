using DrinkBuddy.Common.Settings.Provisioning.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using modan.AS4.Presentation.Infrastructure.JavaScript.Services;

namespace DrinkBuddy.Presentation.Infrastructure.JavaScript.Services.Implementation
{
    [UsedImplicitly]
    public class JavaScriptLocator(
        ISettingsProvider settingsProvider) : IJavaScriptLocator
    {
        private const string SemVerVariable = "__GitVersion.SemVer__";
        private const string SuffixTemplate = "?v={0}";

        public Task<string> LocateJsFilePathAsync(ComponentBase component)
        {
            var type = component.GetType();
            var assemblyFullName = type.Assembly.FullName;
            var assemblyName = type.Assembly.FullName!.Substring(0, assemblyFullName!.IndexOf(','));
            var relativeNamespace = type.FullName!.Replace(assemblyName, string.Empty);

            if (type.IsGenericType)
            {
                relativeNamespace = relativeNamespace.Substring(0, relativeNamespace.IndexOf('`'));
            }

            var path = relativeNamespace.Replace(".", "/");
            path += ".razor.js";
            var version = GetCacheSuffix();
            var suffix = string.Format(SuffixTemplate, version);

            path += suffix;
            return Task.FromResult(path);
        }

        private string GetCacheSuffix()
        {
            var appSettingVersion = settingsProvider.AppSettings.AppVersion;

            var version =
                appSettingVersion == SemVerVariable ? DateTime.Now.Ticks.ToString() : appSettingVersion;

            return version;
        }
    }
}