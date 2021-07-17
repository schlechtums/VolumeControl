using System;
using System.IO;
using System.Reflection;

namespace VolumeControl.ViewModel.Types.Loggers
{
    public class FileLogger : ILogger
    {
        public FileLogger()
            : this(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                "logs",
                                $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt"))
        { }

        public FileLogger(String logFile)
        {
            this._LogFile = logFile;

            var dir = Path.GetDirectoryName(this._LogFile);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private String _LogFile;

        public void Log(string msg)
        {
            File.AppendAllText(this._LogFile, $"{msg}{Environment.NewLine}");
        }
    }
}