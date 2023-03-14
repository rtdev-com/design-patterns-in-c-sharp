namespace Factory
{

    /**** STEP 1 ****/
    public abstract class PizzaStore
    {
        /* Uses CreatePizza method to create a pizza */
        public Pizza OrderPizza(string type)
        {
            Pizza pizza = CreatePizza(type);

            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }

        /* Subclasses will decide which pizza to make */
        public abstract Pizza CreatePizza(string type);
    }

    /**** Step 2 ****/
    public class NYPizzaStore : PizzaStore
    {
        override public Pizza CreatePizza(string item)
        {
            if (item == "cheese")
            {
                return new NYStyleCheesePizza();
            }
            else
            {
                return null;
            }
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        override public Pizza CreatePizza(string item)
        {
            if (item == "cheese")
            {
                return new ChicagoStyleCheesePizza();
            }
            else
            {
                return null;
            }
        }
    }


    /**** STEP 3 ****/
    public abstract class Pizza
    {
        public string name { get; set; }
        public string dough;
        public string sauce;
        public List<string> toppings = new List<string>();

        public void Prepare()
        {
            Console.WriteLine($"Preparing {name}");
            Console.WriteLine($"Tossing dough...");
            Console.WriteLine($"Adding sauce...");
            Console.WriteLine($"Adding toppings:");
            foreach (string topping in toppings)
            {
                Console.WriteLine($"  {topping}");
            }
        }

        public void Bake()
        {
            Console.WriteLine($"Bake for 25 minutes at 350");
        }

        virtual public void Cut()
        {
            Console.WriteLine($"Cutting pizza into diagonal slices");
        }

        public void Box()
        {
            Console.WriteLine($"Place pizza in official PizzaStore box");
        }
    }
    
    public class NYStyleCheesePizza : Pizza
    {
        public NYStyleCheesePizza()
        {
            name = "NY Style Sauce and Cheese Pizza";
            dough = "Thin Crust Dough";
            sauce = "Marinara Sauce";

            toppings.Add("Grated Reggiano Cheese");
        }
    }

    public class ChicagoStyleCheesePizza : Pizza
    {
        public ChicagoStyleCheesePizza()
        {
            name = "Chicago Sauce and Cheese Pizza";
            dough = "Extra Thick Crust Dough";
            sauce = "Plum Tomato Sauce";

            toppings.Add("Shredded Mozzarella Cheese");
        }

        override public void Cut()
        {
            Console.WriteLine($"Cutting pizza into square slices");
        }
    }

    /**** STEP 4 ****/
    class Program
    {
        static void Main(string[] args)
        {
            PizzaStore nyStore = new NYPizzaStore();
            PizzaStore chicagoStore = new ChicagoPizzaStore();

            Pizza pizza = nyStore.OrderPizza("cheese");
            Console.WriteLine($"Ethan ordered a {pizza.name} \n");

            pizza = chicagoStore.OrderPizza("cheese");
            Console.WriteLine($"Joel ordered a {pizza.name} \n");
        }
    }
}