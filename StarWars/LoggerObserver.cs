using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StarWars
{
    class Logger
    {
        private Action<string> _LoggerAction;

        public void LoggerProcess(string message)
        {
            _LoggerAction?.Invoke(message);
        }

        public void AddObserver(Action<string> ObserverMethod)
        {
            _LoggerAction += ObserverMethod;
        }
    }

    class ConsoleMessageLogger
    {
        public void LogMessage(string message) 
            => Console.WriteLine($"{DateTime.Now.ToShortTimeString()} : {message}");
    }

    class FileMessageLogger
    {
        private readonly string _FileName;
        public FileMessageLogger(string FileName) => _FileName = FileName;

        public void FileLogMessage(string message)
        {
            using (var file = File.AppendText(_FileName))
            {
                file.WriteLine($"{DateTime.Now.ToShortTimeString()} : {message}");
            }
        }
    }
}