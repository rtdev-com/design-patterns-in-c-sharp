using System.Text;

namespace Factory
{

    /**** STEP 1 - Create the general Pizza store ****/
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

    /**** Step 2 - Create pizza stores (p 152) ****/
    public class NYPizzaStore : PizzaStore
    {
        override public Pizza CreatePizza(string item)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory = new NYPizzaIngredientFactory();

            if (item.Equals("cheese"))
            {
                pizza = new CheesePizza(ingredientFactory);
                pizza.Name = "New York Style Cheese Pizza";
            }
            else if (item.Equals("clam"))
            {
                pizza = new ClamPizza(ingredientFactory);
                pizza.Name = "New York Style Clam Pizza";
            }

            return pizza;
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        override public Pizza CreatePizza(string item)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory = new ChicagoIngredientFactory();

            if (item.Equals("cheese"))
            {
                pizza = new CheesePizza(ingredientFactory);
                pizza.Name = "New York Style Cheese Pizza";
            }
            else if (item.Equals("clam"))
            {
                pizza = new ClamPizza(ingredientFactory);
                pizza.Name = "New York Style Clam Pizza";
            }
            
            return pizza;
        }
    }

    /**** STEP 3 - Build the Ingredient Factories (p 146) ****/
    public abstract class IPizzaIngredientFactory
    {
        public abstract IDough CreateDough();
        public abstract ISauce CreateSauce();
        public abstract ICheese CreateCheese();
        public abstract IPepperoni CreatePepperoni();
        public abstract IClams CreateClam();
        public abstract IVeggies[] CreateVeggies();
    }

    /**** STEP 4 - Build the NY Ingredient Factories (p 147) ****/
    public class NYPizzaIngredientFactory : IPizzaIngredientFactory
    {
        public override IDough CreateDough()
        {
            return new ThinCrustDough();
        }

        public override ISauce CreateSauce()
        {
            return new MarinaraSauce();
        }

        public override ICheese CreateCheese()
        {
            return new ReggianoCheese();
        }

        public override IPepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public override IClams CreateClam()
        {
            return new FreshClams();
        }

        public override IVeggies[] CreateVeggies()
        {
            IVeggies[] veggies = { new Garlic(), new Onion(), new Mushroom(), new RedPepper() };
            return veggies;
        }      
    }

    public class ChicagoIngredientFactory : IPizzaIngredientFactory
    {
        public override IDough CreateDough()
        {
            return new ThickCrustDough();
        }

        public override ISauce CreateSauce()
        {
            return new PlumTomatoeSauce();
        }

        public override ICheese CreateCheese()
        {
            return new MozzarellaCheese();
        }

        public override IPepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public override IClams CreateClam()
        {
            return new FrozenClams();
        }

        public override IVeggies[] CreateVeggies()
        {
            IVeggies[] veggies = { new Garlic(), new Onion(), new Mushroom(), new RedPepper() };
            return veggies;
        }      
    }

    /**** STEP 5 - Build the general Pizza class (p 149) ****/
    public abstract class Pizza
    {
        public string Name { get; set; }

        public IDough Dough { get; protected set; }
        public ISauce Sauce { get; protected set; }
        public IVeggies[] Veggies { get; protected set; }
        public ICheese Cheese { get; protected set; }
        public IPepperoni Pepperoni { get; protected set; }
        public IClams Clam { get; protected set; }

        public abstract void Prepare(); // Now abstract

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

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"{Name}");

            if (Dough is not null)
                result.AppendLine($" - {Dough.ToString()}");
            if (Sauce is not null)
                result.AppendLine($" - {Sauce.ToString()}");
            if (Cheese is not null)
                result.AppendLine($" - {Cheese.ToString()}");
            if (Veggies != null)
            {
                foreach (var veggie in Veggies)
                    result.Append($"{veggie.ToString()} ");
            }
            if (Clam is not null)
                result.AppendLine($" - {Clam.ToString()}");
            if (Pepperoni is not null)
                result.AppendLine($" - {Pepperoni.ToString()}");

            return result.ToString();
        }
    }
    
    /**** STEP 6 - Build specific pizzas (p 150) ****/
    public class CheesePizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;

        public CheesePizza(IPizzaIngredientFactory pizzaIngredientFactory)
        {
            ingredientFactory = pizzaIngredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine($"Preparing {Name}");

            Dough = ingredientFactory.CreateDough();
            Sauce = ingredientFactory.CreateSauce();
            Cheese = ingredientFactory.CreateCheese();
        }
    }

    public class ClamPizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;

        public ClamPizza(IPizzaIngredientFactory pizzaIngredientFactory)
        {
            ingredientFactory = pizzaIngredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine($"Preparing {Name}");

            Dough = ingredientFactory.CreateDough();
            Sauce = ingredientFactory.CreateSauce();
            Cheese = ingredientFactory.CreateCheese();
            Clam = ingredientFactory.CreateClam();
        }
    }

    /**** STEP 7 - Build Ingridients ****/
    public interface IDough
    {
        string ToString();
    }    
    
    public interface ISauce
    {
        string ToString();
    }    
    
    public interface ICheese
    {
        string ToString();
    }    
    
    public interface IPepperoni
    {
        string ToString();
    }    
    
    public interface IClams
    {
        string ToString();
    }    
    
    public interface IVeggies
    {
        string ToString();
    }

    public class ThinCrustDough : IDough
    {
        string IDough.ToString()
        {
            return "Thin Crust Dough";
        }
    }

    public class ThickCrustDough : IDough
    {
        string IDough.ToString()
        {
            return "Thick Crust Dough";
        }
    }

    public class MarinaraSauce : ISauce
    {
        string ISauce.ToString()
        {
            return "Marinara Sauce";
        }
    }

    public class PlumTomatoeSauce : ISauce
    {
        string ISauce.ToString()
        {
            return "Plum Tomatoe Sauce";
        }
    }

    public class MozzarellaCheese : ICheese
    {
        string ICheese.ToString()
        {
            return "Mozzarella Cheese";
        }
    }

    public class ReggianoCheese : ICheese
    {
        string ICheese.ToString()
        {
            return "Reggiano Cheese";
        }
    }

    public class SlicedPepperoni : IPepperoni
    {
        string IPepperoni.ToString()
        {
            return "Sliced Pepperoni";
        }
    }

    public class FreshClams : IClams
    {
        string IClams.ToString()
        {
            return "Fresh Clams";
        }
    }

    public class FrozenClams : IClams
    {
        string IClams.ToString()
        {
            return "Frozen Clams";
        }
    }

    public class Garlic : IVeggies
    {
        string IVeggies.ToString()
        {
            return "Garlic";
        }
    }

    public class Onion : IVeggies
    {
        string IVeggies.ToString()
        {
            return "Onion";
        }
    }

    public class Mushroom : IVeggies
    {
        string IVeggies.ToString()
        {
            return "Mushroom";
        }
    }

    public class RedPepper : IVeggies
    {
        string IVeggies.ToString()
        {
            return "Red Pepper";
        }
    }

    /**** STEP 8 ****/
    class Program
    {
        static void Main(string[] args)
        {
            PizzaStore nyStore = new NYPizzaStore();
            PizzaStore chicagoStore = new ChicagoPizzaStore();

            Pizza pizza = nyStore.OrderPizza("cheese");
            Console.WriteLine($"Ethan ordered a {pizza}");

            pizza = chicagoStore.OrderPizza("cheese");
            Console.WriteLine($"Joel ordered a {pizza}");

            pizza = nyStore.OrderPizza("clam");
            Console.WriteLine($"Ethan ordered a {pizza}");

            pizza = chicagoStore.OrderPizza("clam");
            Console.WriteLine($"Joel ordered a {pizza}");
        }
    }
}