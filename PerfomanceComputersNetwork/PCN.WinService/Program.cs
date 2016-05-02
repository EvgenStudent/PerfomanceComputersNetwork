using System;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using PCN.WinService.Install;

namespace PCN.WinService
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                var parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        if (!ServiceInstalled())
                            ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
                        break;
                    case "--uninstall":
                        if (ServiceInstalled())
                            ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static bool ServiceInstalled()
        {
            return ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == InstallTimeConfigurationManager.GetConfigurationValue("SystemServiceName")) != null;
        }
    }
}