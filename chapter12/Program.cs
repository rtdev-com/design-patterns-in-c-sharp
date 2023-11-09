namespace CompoundPatterns
{
    /**** STEP 1 - Create Quackable Interface ****/
    public interface IQuackable : IQuackObservable
    {
        public void Quack();
    }

    /**** STEP 2 - Create Ducks ****/
    public class MallardDuck : IQuackable
    {
        Observable Observable;

        public MallardDuck()
        {
            Observable = new Observable(this);
        }

        public void Quack()
        {
            Console.WriteLine("   Quack.");
            NotifyObservers();
        }

        public void RegisterObserver(Observer observer)
        {
            Observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            Observable.NotifyObservers();
        }
    }

    public class ReadheadDuck : IQuackable
    {
        Observable Observable;

        public ReadheadDuck()
        {
            Observable = new Observable(this);
        }

        public void Quack()
        {
            Console.WriteLine("   Quack.");
            NotifyObservers();
        }

        public void RegisterObserver(Observer observer)
        {
            Observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            Observable.NotifyObservers();
        }
    }

    public class RubberDuck : IQuackable
    {
        Observable Observable;

        public RubberDuck()
        {
            Observable = new Observable(this);
        }

        public void Quack()
        {
            Console.WriteLine("   Squeak.");
            NotifyObservers();
        }

        public void RegisterObserver(Observer observer)
        {
            Observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            Observable.NotifyObservers();
        }
    }

    /**** STEP 2.1 - Create a Goose + Adapter ****/
    public class Goose
    {
        public void Honk()
        {
            Console.WriteLine("   Honk.");
        }
    }

    public class GooseAdapter : IQuackable
    {
        Goose Goose;
        Observable Observable;

        public GooseAdapter(Goose goose)
        {
            Goose = goose;
            Observable = new Observable(this);
        }

        public void Quack()
        {
            Goose.Honk();
            NotifyObservers();
        }

        public void RegisterObserver(Observer observer)
        {
            Observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            Observable.NotifyObservers();
        }
    }

    /**** STEP 2.2 - Write a decorator ****/
    public class QuackCounter : IQuackable
    {
        IQuackable Duck;
        static int nQuacks;

        public QuackCounter(IQuackable duck)
        {
            Duck = duck;
        }

        public void Quack()
        {
            Duck.Quack();
            nQuacks++;
        }

        public static int GetQuacks()
        {
            return nQuacks;
        }

        public void RegisterObserver(Observer observer)
        {
            Duck.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            Duck.NotifyObservers();
        }
    }

    /**** STEP 2.3 - Create a Duck factory ****/
    public abstract class AbstractDuckFactory
    {
        public abstract IQuackable createMallardDuck();
        public abstract IQuackable createRedheadDuck();
        public abstract IQuackable createRubberDuck();
    }

    public class CountingDuckFactory : AbstractDuckFactory
    {
        public override IQuackable createMallardDuck()
        {
            return new QuackCounter(new MallardDuck());
        }

        public override IQuackable createRedheadDuck()
        {
            return new QuackCounter(new ReadheadDuck());
        }

        public override IQuackable createRubberDuck()
        {
            return new QuackCounter(new RubberDuck());
        }
    }

    /**** STEP 2.4 - Implement Flocking / Composite Pattern ****/
    public class Flock : IQuackable
    {
        List<IQuackable> quackers = new List<IQuackable>();

        public void Add(IQuackable quacker)
        {
            quackers.Add(quacker);
        }

        public void Quack()
        {
            foreach (var quacker in quackers)
            {
                quacker.Quack();
            }
        }

        public void RegisterObserver(Observer observer)
        {
            foreach (var quacker in quackers)
            {
                quacker.RegisterObserver(observer);
            }
        }

        public void NotifyObservers() {}
    }

    /**** STEP 2.5 - Implement Observer Pattern ****/
    public interface Observer
    {
        public void Update(IQuackObservable duck);
    }

    public class Quackologist : Observer
    {
        public void Update(IQuackObservable duck)
        {
            Console.WriteLine($"  '{duck}' just quacked");
        }
    }

    public interface IQuackObservable
    {
        public void RegisterObserver(Observer observer);
        public void NotifyObservers();
    }

    public class Observable : IQuackObservable
    {
        List<Observer> Observers = new List<Observer>();
        IQuackObservable Duck;

        public Observable(IQuackObservable duck)
        {
            Duck = duck;
        }

        public void RegisterObserver(Observer observer)
        {
            Observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update(Duck);
            }
        }
    }

    /**** STEP 3 - Create a Simulator ****/
    public class DuckSimulator
    {
        static void Main(string[] args)
        {
            DuckSimulator simulator = new DuckSimulator();
            AbstractDuckFactory duckFactory = new CountingDuckFactory();

            simulator.Simulate(duckFactory);
        }

        void Simulate(AbstractDuckFactory duckFactory)
        {
            IQuackable mallardDuck = new QuackCounter(new MallardDuck());
            IQuackable redheadDuck = new QuackCounter(new ReadheadDuck());
            IQuackable rubberDuck = new QuackCounter(new RubberDuck());
            IQuackable gooseDuck = new QuackCounter(new GooseAdapter(new Goose()));

            IQuackable mallardDuck1 = new QuackCounter(new MallardDuck());
            IQuackable mallardDuck2 = new QuackCounter(new MallardDuck());
            IQuackable mallardDuck3 = new QuackCounter(new MallardDuck());

            Console.WriteLine("\nDuck Simulator:\n");

            Flock flockOfDucks = new Flock();

            flockOfDucks.Add(mallardDuck);
            flockOfDucks.Add(redheadDuck);
            flockOfDucks.Add(rubberDuck);
            flockOfDucks.Add(gooseDuck);

            Flock flockOfMallards = new Flock();

            flockOfMallards.Add(mallardDuck1);
            flockOfMallards.Add(mallardDuck2);
            flockOfMallards.Add(mallardDuck3);

            flockOfDucks.Add(flockOfMallards);

            Console.WriteLine($"\n  Whole Flock With Observer:\n");

            Quackologist quackologist = new Quackologist();
            flockOfDucks.RegisterObserver(quackologist);

            Simulate(flockOfDucks);

            Console.WriteLine($"\n  Ducks quacked {QuackCounter.GetQuacks()} times.");
        }

        void Simulate(IQuackable duck)
        {
            duck.Quack();
        }
    }
}