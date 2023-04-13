namespace Singleton
{
    public class Logger
    {
        private static Logger? _uniqueInstance;

        private Logger()
        {
            // Make the constructor private!
        }

        public static Logger GetInstance()
        {
            if (_uniqueInstance == null)
            {
                /* This is a lazy instantiation because an instance will
                   not be created if this method is never called */
                _uniqueInstance = new Logger();
            }
            return _uniqueInstance;
        }

        public void PrintLog()
        {
            Console.WriteLine($"I am a Logger class");
        }
    }

    /**** STEP 8 ****/
    class Program
    {
        static void Main(string[] args)
        {
            Logger.GetInstance().PrintLog();
        }
    }

}