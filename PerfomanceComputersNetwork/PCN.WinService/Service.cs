using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;
using PCN.BL.Services;

namespace PCN.WinService
{
    partial class Service : ServiceBase
    {
        private Timer _timer;
        private readonly MeasureService _measureService = new MeasureService();
        private readonly SendService _sendService = new SendService(new Uri(ConfigurationManager.AppSettings["ApiBaseUri"]));

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(double.Parse(ConfigurationManager.AppSettings["TimerInterval"]));
            _timer.Elapsed += async (sender, eventArgs) =>
            {
                try
                {
                    await _sendService.SendComputerInfo(_measureService.GetComputerInfo());
                    await _sendService.SendCpu(_measureService.GetCpuUsage());
                    await _sendService.SendRam(_measureService.GetRamUsage());
                }
                catch (Exception exception)
                {
                }
            };
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        protected override void OnPause()
        {
            _timer.Stop();
        }

        protected override void OnContinue()
        {
            _timer.Start();
        }
    }
}