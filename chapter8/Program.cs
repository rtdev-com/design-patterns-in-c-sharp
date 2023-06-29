namespace TemplateMethodExample
{
    /**** STEP 1 - Algorithm class ****/
    public abstract class CaffeineBeverage
    {
        public void PrepareRecepie()
        {
            BoilWater();      // Concrete operation
            Brew();           // Primitive operation
            PourInCup();      // Concrete operation
            AddCondiments();  // Primitive operation
        }

        // Declare steps (that are different for every class) as abstract
        public abstract void Brew();
        public abstract void AddCondiments();

        // Implement steps that are the same for all
        public void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        public void PourInCup()
        {
            Console.WriteLine("Pouring into a cup");
        }
    }

    /**** STEP 2 - Subclasses ****/
    public class Tea : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("Steeping the tea");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("Adding Lemon");
        }
    }

    public class Coffee : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("Dripping Coffee through filter");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("Adding Sugar and Milk");
        }
    }

    /**** STEP 3 - Test Template Method ****/
    class Program
    {
        static void Main(string[] args)
        {
            Tea tea = new Tea();
            Coffee coffee = new Coffee();

            Console.WriteLine("\n-Making tea...");
            tea.PrepareRecepie();

            Console.WriteLine("\n-Making coffee...");
            coffee.PrepareRecepie();
        }
    }
}