namespace TestingTheDuckCode
{
    /**** STEP 1 ****/
    public abstract class Duck
    {
        /* Declare two refrence variables for the behavior
        interface types. All duck subclasses (in the same
        package) import these */
        public FlyBehavior FlyBehavior { get; set; }
        public QuackBehavior QuackBehavior { get; set; }

        public void SetFlyBehavior(FlyBehavior fb)
        {
            FlyBehavior = fb;
        }

        public void SetQuackBehavior(QuackBehavior qb)
        {
            QuackBehavior = qb;
        }

        public abstract void Display();
        public void PerformFly()
        {
            // Delegates to behavior class
            FlyBehavior.Fly();
        }

        public void PerformQuack()
        {
            // Delegates to behavior class
            QuackBehavior.Quack();
        }
        
        public void Swim()
        {
            Console.WriteLine("All ducks float, even decoys!");
        }
    }

    public class MallardDuck : Duck
    {
        // Constructor
        public MallardDuck()
        {
            /* Inherits the quackBehavior and flyBehavior
            instance variables from class Duck */
            QuackBehavior = new DefaultQuack();
            FlyBehavior = new FlyWithWings();
        }

        override public void Display()
        {
            Console.WriteLine("I'm a real Mallard duck");
        }
    }

    public class ModelDuck : Duck
    {
        public ModelDuck()
        {
            /* Inherits the quackBehavior and flyBehavior
            instance variables from class Duck */
            QuackBehavior = new DefaultQuack();
            FlyBehavior = new FlyNoWay();
        }

        override public void Display()
        {
            Console.WriteLine("I'm a model duck");
        }
    }

    /**** STEP 2 ****/
    public interface FlyBehavior
    {
        // Interface that all flying behavior classes implement
        public void Fly();
    }

    public class FlyWithWings : FlyBehavior
    {
        public void Fly()
        {
            // For ducks that fly...
            Console.WriteLine("I'm flying!!");
        }
    }

    public class FlyNoWay : FlyBehavior
    {
        public void Fly()
        {
            // For ducks that do NOT fly...
            Console.WriteLine("I can't fly");
        }
    }

    public class FlyRocketPowered : FlyBehavior
    {
        public void Fly()
        {
            // For ducks that do NOT fly...
            Console.WriteLine("I'm flying with a rocket!");
        }        
    }

    /**** STEP 3 ****/
    public interface QuackBehavior
    {
        public void Quack();
    }

    public class DefaultQuack : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Quack");
        }
    }

    public class MuteQuack : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("<< Silence >>");
        }
    }

    public class Squeak : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Squeak");
        }
    }

    /**** STEP 4 ****/
    class Program
    {
        static void Main(string[] args)
        {
            Duck mallard = new MallardDuck();
            mallard.PerformQuack();
            mallard.PerformFly();

            Duck model = new ModelDuck();
            model.PerformFly();
            model.SetFlyBehavior(new FlyRocketPowered());
            model.PerformFly();

        }
    }
}