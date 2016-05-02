using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using PCN.WinService.Install;

namespace PCN.WinService
{
    [RunInstaller(true)]
    public class CurrentServiceInstaller : Installer
    {
        public CurrentServiceInstaller() : base()
        {
            var serviceProcessInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            //Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //Service Information
            serviceInstaller.DisplayName = InstallTimeConfigurationManager.GetConfigurationValue("ServiceDisplayName");
            serviceInstaller.Description = InstallTimeConfigurationManager.GetConfigurationValue("ServiceDescription");
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DelayedAutoStart = true;
            serviceInstaller.ServiceName = InstallTimeConfigurationManager.GetConfigurationValue("SystemServiceName");

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);

            Committed += (sender, args) =>
            {
                //auto start the service once the installation is finished
                var controller = new ServiceController(InstallTimeConfigurationManager.GetConfigurationValue("SystemServiceName"));
                controller.Start();
            };
        }
    }
}