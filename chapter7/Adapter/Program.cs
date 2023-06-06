namespace Adapter
{
    /**** STEP 1 - Define Duck related objects ****/
    public interface IDuck
    {
        public void Quack();
        public void Fly();
    }

    public class MallardDuck : IDuck
    {
        public void Quack()
        {
            Console.WriteLine("Quack!");
        }

        public void Fly()
        {
            Console.WriteLine("I am flying!");
        }
    }
    
    /**** STEP 2 - Define Turkey related objects ****/
    public interface ITurkey
    {
        public void Gobble(); // This is different from Ducks
        public void Fly();
    }

    public class WildTurkey : ITurkey
    {
        public void Gobble()
        {
            Console.WriteLine("Gobble Gobble.");
        }

        public void Fly()
        {
            Console.WriteLine("I am flying a short distance.");
        }
    }

    /**** STEP 3 - Create an adapter for the Turkey ****/
    public class TurkeyAdapter : IDuck
    {
        private ITurkey _turkey;

        public TurkeyAdapter(ITurkey turkey)
        {
            _turkey = turkey;
        }

        public void Quack()
        {
            _turkey.Gobble();
        }

        public void Fly()
        {
            // Turkeys take shorter flights than Ducks
            for (int i = 0; i < 5; i++)
            {
                _turkey.Fly();
            }
        }
    }

    /**** STEP 4 - Test the adapter ****/
    class Program
    {
        static void TestDuck(IDuck duck)
        {
            duck.Quack();
            duck.Fly();
        }

        static void Main(string[] args)
        {
            // Create a Turkey and a Duck
            IDuck mallard = new MallardDuck();
            ITurkey turkey = new WildTurkey();

            // Adapt
            IDuck turkeyAdapter = new TurkeyAdapter(turkey);

            // Test the Turkey
            Console.WriteLine("The Turkey says...");
            turkey.Gobble();
            turkey.Fly();
            Console.WriteLine();

            // Test the Duck
            Console.WriteLine("The Duck says...");
            TestDuck(mallard);
            Console.WriteLine();

            // Test the Adapter
            Console.WriteLine("The Adapter says...");
            TestDuck(turkeyAdapter);
            Console.WriteLine();
        }
    }
}