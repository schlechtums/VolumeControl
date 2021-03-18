using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeControl.ViewModel.Types.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}