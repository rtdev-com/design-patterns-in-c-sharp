namespace TestingTheFacade
{
    /**** STEP 1 - Define Facade ****/
    public class HomeTheaterFacade
    {
        private TV _tvComponent;
        private Stereo _stereoComponent;

        public HomeTheaterFacade()
        {
            _tvComponent = new TV();
            _stereoComponent = new Stereo();
        }

        public void WatchMovie()
        {
            _tvComponent.On();
            _stereoComponent.On();
            _stereoComponent.Mode = "AUX";
            _tvComponent.Play();
        }

        public void EndMovie()
        {
            _tvComponent.Off();
            _stereoComponent.Off();
        }
        
        public void PlayRadio()
        {
            _stereoComponent.On();
            _stereoComponent.Mode = "FM";
        }

        public void EndRadio()
        {
            _stereoComponent.Off();
        }
    }

    /**** STEP 2 - Define Components ****/
    public class TV
    {
        public void On()
        {
            Console.WriteLine("The TV is ON");
        }

        public void Play()
        {
            Console.WriteLine("The TV is playing a movie");
        }

        public void Off()
        {
            Console.WriteLine("The TV is OFF");
        }
    }

    public class Stereo
    {
        private string _mode = "AM";
        public string Mode 
        { 
            get { return _mode; }
            set 
            {
                _mode = value;
                Console.WriteLine($"The Stereo mode is set to {Mode}");
            }
        }

        public void On()
        {
            Console.WriteLine("The Stereo is ON");
        }

        public void Off()
        {
            Console.WriteLine("The Stereo is OFF");
        }       
    }
    
    /**** STEP 3 ****/
    class Program
    {
        static void Main(string[] args)
        {
            HomeTheaterFacade facade = new HomeTheaterFacade();

            Console.WriteLine("Testing the Home Theater...");
            facade.WatchMovie();
            facade.EndMovie();

            Console.WriteLine("\nTesting the Radio...");
            facade.PlayRadio();
            facade.EndRadio();
        }
    }
}