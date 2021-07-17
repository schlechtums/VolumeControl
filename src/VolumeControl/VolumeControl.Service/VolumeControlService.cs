using System;
using System.Reflection;
using System.ServiceProcess;
using VolumeControl.ViewModel.Types.Loggers;

namespace VolumeControl.Service
{
    public partial class VolumeControlService : ServiceBase
    {
        public VolumeControlService()
        {
            InitializeComponent();
        }

        private ILogger _Logger;

        protected override void OnStart(String[] args)
        {
            this._Logger = new FileLogger();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            this._Logger.Log($"Volume Control Service version {version?.Major}.{version?.Minor}.{version?.Build}");

            new ViewModel.ViewModel(this._Logger);
        }

        protected override void OnStop()
        {
            this._Logger.Log("Volume Control Service shutting down");
        }
    }
}
