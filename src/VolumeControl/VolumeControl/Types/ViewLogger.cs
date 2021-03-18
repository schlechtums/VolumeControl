using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeControl.ViewModel.Types.Loggers;

namespace VolumeControl.Types
{
    public class ViewLogger : ILogger
    {
        public void Log(String msg)
        {
            var w = MainWindow.WindowSingleton;
            w.Dispatcher.Invoke(() => w._Messages.Text = $"{msg}{Environment.NewLine}{w._Messages.Text}");
        }
    }
}