using System;

namespace SingletonPatternExample
{
    public class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();

        private Logger()
        {
        }

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger logger1 = Logger.GetInstance();
            Logger logger2 = Logger.GetInstance();

            logger1.Log("Hello from Logger 1");
            logger2.Log("Hello from Logger 2");

            if (logger1 == logger2)
            {
                Console.WriteLine("Both loggers are the same instance. Singleton pattern works!");
            }
            else
            {
                Console.WriteLine("Loggers are different instances.");
            }
        }
    }
}
