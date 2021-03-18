using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeControl.ViewModel.Types.Loggers
{
    public interface ILogger
    {
        void Log(String msg);
    }
}