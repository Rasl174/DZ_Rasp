using System;
using System.IO;

namespace CS_DZ_Rasp_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder fileLogWriter = new FileLogWritter("log.txt");
            Pathfinder consoleLogWriter = new ConsoleLogWritter();
            Pathfinder firdayFileLogWriter = new SecureLogWritter(fileLogWriter, DayOfWeek.Friday);
            Pathfinder firdayConsoleLogWriter = new SecureLogWritter(consoleLogWriter, DayOfWeek.Friday);
            Pathfinder connectedLogWriter = new ConnectedLogWriter(consoleLogWriter, firdayFileLogWriter);
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }

    class Pathfinder
    {
        private ILogger _logger;

        public virtual void Find(string message)
        {
            _logger.WriteError(message);
        }
    }

    class ConsoleLogWritter : Pathfinder, ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : Pathfinder, ILogger
    {
        private string _path;

        public FileLogWritter(string path)
        {
            _path = path;
        }

        public void WriteError(string message)
        {
            File.WriteAllText(_path, message);
        }
    }

    class SecureLogWritter : Pathfinder , ILogger
    {
        private Pathfinder _fileLogWriter;
        private DayOfWeek _dayOfWeek;

        public SecureLogWritter(Pathfinder fileLogWriter, DayOfWeek dayOfWeek)
        {
            _fileLogWriter = fileLogWriter;
            _dayOfWeek = dayOfWeek;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == _dayOfWeek)
            {
                _fileLogWriter.Find(message);
            }
        }
    }

    class ConnectedLogWriter : Pathfinder, ILogger
    {
        private Pathfinder[] _pathfinders;

        public ConnectedLogWriter(params Pathfinder[] pathfinders)
        {
            _pathfinders = pathfinders;
        }

        public void WriteError(string message)
        {
            foreach (var pathfinder in _pathfinders)
            {
                pathfinder.Find(message);
            }
        }
    }
}