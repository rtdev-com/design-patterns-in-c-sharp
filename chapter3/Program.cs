namespace starbuzz
{
    /**** STEP 1 ****/
    public abstract class Beverage
    {
        /* An abstract class with cost implemented in
        subclasses */
        public virtual string Description { get; protected set; } = "Unknown Beverage";

        public abstract double Cost();
    }

    public abstract class CondimentDecorator : Beverage
    {
        Beverage beverage;

        // Ensure that decorators implement GetDescription
        public abstract override string Description { get; }
    }

    /**** STEP 2 - Beverages ****/
    public class Espresso : Beverage
    {
        public Espresso()
        {
            Description = "Espresso";
        }

        public override double Cost()
        {
            return 1.99;
        }
    }

    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            Description = "House Blend Coffee";
        }


        public override double Cost()
        {
            return 0.89;
        }
    }

    public class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            Description = "Dark Roast Coffee";
        }


        public override double Cost()
        {
            return 0.99;
        }
    }

    /* I am omitting a decaf because no one would order it */

    /**** STEP 3 - Condiments ****/
    public class Mocha : CondimentDecorator
    {
        Beverage _beverage;
        // Recall: CondimentDecorator extends Beverage class
        public Mocha(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override string Description
        {
            get { return _beverage.Description + ", Mocha"; }
        }

        public override double Cost()
        {
            return _beverage.Cost() + 0.20;
        }
    }

    public class Soy : CondimentDecorator
    {
        Beverage _beverage;

        public Soy(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override string Description
        {
            get { return _beverage.Description + ", Soy"; }
        }

        public override double Cost()
        {
            return _beverage.Cost() + 0.15;
        }
    }

    public class Whip : CondimentDecorator
    {
        Beverage _beverage;

        public Whip(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override string Description
        {
            get { return _beverage.Description + ", Whip"; }
        }

        public override double Cost()
        {
            return _beverage.Cost() + 0.10;
        }
    }

    /**** STEP 4 ****/
    class Program
    {
        static void Main(string[] args)
        {
            Beverage beverage = new Espresso();
            Console.WriteLine($"{beverage.Description} ${beverage.Cost()}");

            Beverage beverage1 = new DarkRoast();
            beverage1 = new Mocha(beverage1);
            beverage1 = new Mocha(beverage1);
            beverage1 = new Whip(beverage1);
            Console.WriteLine($"{beverage1.Description} ${beverage1.Cost()}");

            Beverage beverage2 = new HouseBlend();
            beverage2 = new Soy(beverage2);
            beverage2 = new Mocha(beverage2);
            beverage2 = new Whip(beverage2);
            Console.WriteLine($"{beverage2.Description} ${beverage2.Cost()}");
        }
    }
}