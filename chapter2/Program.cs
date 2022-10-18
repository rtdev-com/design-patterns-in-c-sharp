    namespace TestingTheWeatherStationCode
{
    /**** STEP 1: Implement Weather Station Interfaces ****/
    public interface ISubject
    {
        public void RegisterObserver(IObserver o);
        public void RemoveObserver(IObserver o);
        public void NotifyObservers();
    }

    public interface IObserver
    {
        // Update( ... ) will be implemented by all observers
        public void Update(float temp, float humidity, float pressure);
    }

    public interface IDisplayElement
    {
        public void Display();
    }

    /**** STEP 2: Implement the Subject Interface ****/
    public class WeatherData : ISubject
    {
        private List<IObserver> _observers;
        private float _temperature;
        private float _humidity;
        private float _pressure;

        public WeatherData()
        {
            _observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            // Tell observers that state changed
            foreach (IObserver observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
            }
        }

        public void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
            MeasurementsChanged();
        }
    }

    /**** STEP 3: Build Display Elements ****/
    public class CurrentConditionDisplay : IObserver, IDisplayElement
    {
        /* Implements Observer to obtain changes from the WeatherData
        object & implements DisplayElement due to API requirement for
        every display element to implement this interface */

        private float _temperature;
        private float _humidity;
        private WeatherData _weatherData;

        public CurrentConditionDisplay(WeatherData weatherData)
        {
            // Register WeatherData object
            _weatherData = weatherData;
            _weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"Current conditions: {_temperature}F degrees & {_humidity}% humidity");
        }
    }

    /*** STEP 4: (Optional) Heat Index Display ****/
    public class HeatIndexDisplay : IObserver, IDisplayElement
    {
        /* Implements Observer to obtain changes from the WeatherData
        object & implements DisplayElement due to API requirement for
        every display element to implement this interface */

        private float _heatIndex;
        private WeatherData _weatherData;

        public HeatIndexDisplay(WeatherData weatherData)
        {
            // Register WeatherData object
            _weatherData = weatherData;
            _weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _heatIndex = computeHeatIndex(temperature, humidity);
            Display();
        }

        private float computeHeatIndex(float t, float rh) 
        {
            float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh) +
                        (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) +
                        (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
                        (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 *  
                        (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) +
                        (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +     
                        0.000000000843296 * (t * t * rh * rh * rh)) -
                        (0.0000000000481975 * (t * t * t * rh * rh * rh)));
        
            return index;
        }


        public void Display()
        {
            Console.WriteLine($"Heat index: {_heatIndex}");
        }
    }

    /**** STEP 5: Test Weather Station ****/
    class Program
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();
            CurrentConditionDisplay currentDisplay = new CurrentConditionDisplay(weatherData);
            HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);

            weatherData.SetMeasurements(80, 65, 30.4f);
            weatherData.SetMeasurements(82, 70, 29.2f);
            weatherData.SetMeasurements(78, 90, 29.2f);
        }
    }
}