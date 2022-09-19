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

        public void setFlyBehavior(FlyBehavior fb)
        {
            FlyBehavior = fb;
        }

        public void setQuackBehavior(QuackBehavior qb)
        {
            QuackBehavior = qb;
        }

        public abstract void display();
        public void performFly()
        {
            // Delegates to behavior class
            FlyBehavior.fly();
        }

        public void performQuack()
        {
            // Delegates to behavior class
            QuackBehavior.quack();
        }
        
        public void swim()
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
            QuackBehavior = new Quack();
            FlyBehavior = new FlyWithWings();
        }

        override public void display()
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
            QuackBehavior = new Quack();
            FlyBehavior = new FlyNoWay();
        }

        override public void display()
        {
            Console.WriteLine("I'm a model duck");
        }
    }

    /**** STEP 2 ****/
    public interface FlyBehavior
    {
        // Interface that all flying behavior classes implement
        public void fly();
    }

    public class FlyWithWings : FlyBehavior
    {
        public void fly()
        {
            // For ducks that fly...
            Console.WriteLine("I'm flying!!");
        }
    }

    public class FlyNoWay : FlyBehavior
    {
        public void fly()
        {
            // For ducks that do NOT fly...
            Console.WriteLine("I can't fly");
        }
    }

    public class FlyRocketPowered : FlyBehavior
    {
        public void fly()
        {
            // For ducks that do NOT fly...
            Console.WriteLine("I'm flying with a rocket!");
        }        
    }

    /**** STEP 3 ****/
    public interface QuackBehavior
    {
        public void quack();
    }

    public class Quack : QuackBehavior
    {
        public void quack()
        {
            Console.WriteLine("Quack");
        }
    }

    public class MuteQuack : QuackBehavior
    {
        public void quack()
        {
            Console.WriteLine("<< Silence >>");
        }
    }

    public class Squeak : QuackBehavior
    {
        public void quack()
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
            mallard.performQuack();
            mallard.performFly();

            Duck model = new ModelDuck();
            model.performFly();
            model.setFlyBehavior(new FlyRocketPowered());
            model.performFly();

        }
    }
}