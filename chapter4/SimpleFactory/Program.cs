namespace simpleFactory
{
    /**** STEP 1 ****/
    public class SimplePizzaFactory
    {
        /* Creates Pizza for its clients */
        public Pizza CreatePizza(string type)
        {
            Pizza _pizza = null;

            if (type == "cheese")
            {
                _pizza = new CheesePizza();
            }
            else if (type == "pepperoni")
            {
                _pizza = new PepperoniPizza();
            }
            else if (type == "veggie")
            {
                _pizza = new VeggiePizza();
            }

            return _pizza;
        }
    }

    /**** STEP 2 ****/
    public class PizzaStore
    {
        /* Uses Pizza Factory to create a pizza */
        SimplePizzaFactory _factory;

        public PizzaStore()
        {
            _factory = new SimplePizzaFactory();
        }

        public Pizza OrderPizza(string type)
        {

            Pizza pizza = _factory.CreatePizza(type);

            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }
    }

    /**** STEP 3 ****/
    public abstract class Pizza
    {
        public abstract void Prepare();
        public abstract void Bake();
        public abstract void Cut();
        public abstract void Box();
    }
    
    public class CheesePizza : Pizza
    {
        public override void Prepare()
        {
            Console.WriteLine("Preparing cheese pizza");
        }
        public override void Bake()
        {
            Console.WriteLine("Baking cheese pizza");
        }
        public override void Cut()
        {
            Console.WriteLine("Cutting cheese pizza");
        }
        public override void Box()
        {
            Console.WriteLine("Preparing cheese pizza");
        }
    }

    public class PepperoniPizza : Pizza
    {
        public override void Prepare()
        {
            Console.WriteLine("Preparing pepperoni pizza");
        }
        public override void Bake()
        {
            Console.WriteLine("Baking pepperoni pizza");
        }
        public override void Cut()
        {
            Console.WriteLine("Cutting pepperoni pizza");
        }
        public override void Box()
        {
            Console.WriteLine("Preparing pepperoni pizza");
        }
    }

    public class VeggiePizza : Pizza
    {
        public override void Prepare()
        {
            Console.WriteLine("Preparing veggie pizza");
        }
        public override void Bake()
        {
            Console.WriteLine("Baking veggie pizza");
        }
        public override void Cut()
        {
            Console.WriteLine("Cutting veggie pizza");
        }
        public override void Box()
        {
            Console.WriteLine("Preparing veggie pizza");
        }
    }

    /**** STEP 4 ****/
    class Program
    {
        static void Main(string[] args)
        {
            PizzaStore pizzaStore = new PizzaStore();

            Pizza veggiePizza = pizzaStore.OrderPizza("veggie");
            Pizza pepperoniPizza = pizzaStore.OrderPizza("pepperoni");
            Pizza cheesePizza = pizzaStore.OrderPizza("cheese");
        }
    }
}