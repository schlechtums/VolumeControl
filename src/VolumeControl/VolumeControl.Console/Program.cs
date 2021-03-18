using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VolumeControl.ViewModel.Types.Loggers;

namespace VolumeControl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Task(() => new ViewModel.ViewModel(new ConsoleLogger())).Start();
            while (true)
                Thread.Sleep(1000);
        }
    }
}