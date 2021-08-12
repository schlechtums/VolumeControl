using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var title = $"Volume Control version {version?.Major}.{version?.Minor}.{version?.Build}";
            System.Console.WriteLine(title);
            System.Console.Title = title;

            new Task(() => new ViewModel.ViewModel(new ConsoleLogger())).Start();
            while (true)
                Thread.Sleep(1000);
        }
    }
}
