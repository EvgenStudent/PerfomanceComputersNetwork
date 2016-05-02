using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace PCN.WinService
{
    partial class Service : ServiceBase
    {
        private Timer _timer;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(double.Parse(ConfigurationManager.AppSettings["TimerInterval"]));
            _timer.Elapsed += (sender, eventArgs) =>
            {
                
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