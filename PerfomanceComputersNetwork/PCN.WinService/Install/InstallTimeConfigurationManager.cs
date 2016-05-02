using System.Configuration;
using System.Reflection;

namespace PCN.WinService.Install
{
    public class InstallTimeConfigurationManager
    {
        public static string GetConfigurationValue(string key)
        {
            var service = Assembly.GetAssembly(typeof (CurrentServiceInstaller));
            var config = ConfigurationManager.OpenExeConfiguration(service.Location);

            return config.AppSettings.Settings[key].Value;
        }
    }
}