using System.Collections;

namespace TestingComposite
{
    /**** STEP 1 - Define Menu Pieces ****/
    public abstract class MenuComponent
    {
        public virtual string Name
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string Description
        {
            get { throw new NotImplementedException(); }
        }

        public virtual double Price
        {
            get { throw new NotImplementedException(); }
        }

        public virtual bool IsVegetarian
        {
            get { throw new NotImplementedException(); }
        }

        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotImplementedException();
        }
        public virtual MenuComponent GetChild(int i)
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerator<MenuComponent> CreateIterator();

        public virtual void Print()
        {
            throw new NotImplementedException();
        }
    }

    public class Menu : MenuComponent
    {
        private readonly List<MenuComponent> _menuComponents = new List<MenuComponent>();
        private CompositeIterator _iterator;

        public Menu(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string Name { get; }

        public override string Description { get; }

        public override void Add(MenuComponent menuComponent)
        {
            _menuComponents.Add(menuComponent);
        }

        public override void Remove(MenuComponent menuComponent)
        {
            _menuComponents.Remove(menuComponent);
        }

        public override MenuComponent GetChild(int i)
        {
            return _menuComponents[i];
        }

        public override IEnumerator<MenuComponent> CreateIterator()
        {
            return _iterator ?? (_iterator = new CompositeIterator(_menuComponents.GetEnumerator()));
        }

        public override void Print()
        {
            Console.Write("\n" + Name);
            Console.WriteLine(", " + Description);
            Console.WriteLine("---------------------");

            var iterator = _menuComponents.GetEnumerator();
            while (iterator.MoveNext())
            {
                MenuComponent menuComponent = iterator.Current;
                menuComponent?.Print();
            }
        }
    }

    public class MenuItem : MenuComponent
    {
        public MenuItem(string name, string description, bool vegetarian, double price)
        {
            Name = name;
            Description = description;
            IsVegetarian = vegetarian;
            Price = price;
        }

        public override string Name { get; }
        public override string Description { get; }
        public override bool IsVegetarian { get; }
        public override double Price { get; }

        public override IEnumerator<MenuComponent> CreateIterator()
        {
            return new NullIterator();
        }

        public override void Print()
        {
            Console.Write(" " + Name);
            if (IsVegetarian)
            {
                Console.Write("(v)");
            }
            Console.WriteLine(", " + Price.ToString());
            Console.WriteLine("    --" + Description);
        }
    }

    /**** STEP 2 - Define Iterators ****/
    public class NullIterator : IEnumerator<MenuComponent>
    {
        public void Dispose() { }

        public bool MoveNext() => false;

        public void Reset() { }

        public MenuComponent Current => null;

        object IEnumerator.Current => Current;
    }

    public class CompositeIterator : IEnumerator<MenuComponent>
    {
        private readonly IEnumerator<MenuComponent> _iterator;
        private readonly Stack<IEnumerator<MenuComponent>> _stack = new Stack<IEnumerator<MenuComponent>>();
        private MenuComponent _current;

        public CompositeIterator(IEnumerator<MenuComponent> iterator)
        {
            _iterator = iterator;
            _stack.Push(iterator);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (!_stack.Any())
            {
                _current = null;
                return false;
            }

            var iterator = _stack.Peek();
            if (!iterator.MoveNext())
            {
                _stack.Pop();
                return MoveNext();
            }

            _current = iterator.Current;

            if (_current is Menu)
            {
                _stack.Push(_current.CreateIterator());
            }

            return true;
        }

        public void Reset()
        {
            _current = null;
            _stack.Clear();
            _stack.Push(_iterator);
        }

        public MenuComponent Current => _current;

        object IEnumerator.Current => Current;
    }

    /**** STEP 3 - Define Waitress ****/
        public class Waitress
    {
        private readonly MenuComponent _allMenus;

        public Waitress(MenuComponent allMenus)
        {
            _allMenus = allMenus;
        }

        public void PrintMenu()
        {
            _allMenus.Print();
        }

        public void PrintVegetarianMenu()
        {
            var iterator = _allMenus.CreateIterator();

            Console.WriteLine("\nVEGETARIAN MENU");
            Console.WriteLine("-----------------");
            while (iterator.MoveNext())
            {
                MenuComponent menuComponent = iterator.Current;
                try
                {
                    if (menuComponent.IsVegetarian)
                    {
                        menuComponent.Print();
                    }
                }
                catch (NotImplementedException) { }
            }
        }
    }

    /**** STEP 4 - Test ****/
    class Program
    {
        static void Main(string[] args)
        {
            MenuComponent pancakeHouseMenu = new Menu("PANCAKE HOUSE MENU", "Breakfast");
            MenuComponent dinerMenu = new Menu("DINER MENU", "Lunch");
            MenuComponent cafeMenu = new Menu("CAFE MENU", "Dinner");
            MenuComponent dessertMenu = new Menu("DESSERT MENU", "Dessert of course!");

            MenuComponent allMenus = new Menu("ALL MENUS", "All menus combined");

            allMenus.Add(pancakeHouseMenu);
            allMenus.Add(dinerMenu);
            allMenus.Add(cafeMenu);

            pancakeHouseMenu.Add(new MenuItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", true, 2.99));
            pancakeHouseMenu.Add(new MenuItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99));
            pancakeHouseMenu.Add(new MenuItem("Blueberry Pancakes", "Pancakes made with fresh blueberries, and blueberry syrup", true, 3.49));
            pancakeHouseMenu.Add(new MenuItem("Waffles", "Waffles, with your choice of blueberries or strawberries", true, 3.59));

            dinerMenu.Add(new MenuItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99));
            dinerMenu.Add(new MenuItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99));
            dinerMenu.Add(new MenuItem("Soup of the day", "A bowl of the soup of the day, with a side of potato salad", false, 3.29));
            dinerMenu.Add(new MenuItem("Hotdog", "A hot dog, with sauerkraut, relish, onions, topped with cheese", false, 3.05));
            dinerMenu.Add(new MenuItem("Steamed Veggies and Brown Rice", "A medly of steamed vegetables over brown rice", true, 3.99));

            dinerMenu.Add(new MenuItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89));

            dinerMenu.Add(dessertMenu);

            dessertMenu.Add(new MenuItem("Apple Pie", "Apple pie with a flakey crust, topped with vanilla icecream", true, 1.59));
            dessertMenu.Add(new MenuItem("Cheesecake", "Creamy New York cheesecake, with a chocolate graham crust", true, 1.99));
            dessertMenu.Add(new MenuItem("Sorbet", "A scoop of raspberry and a scoop of lime", true, 1.89));

            cafeMenu.Add(new MenuItem("Veggie Burger and Air Fries", "Veggie burger on a whole wheat bun, lettuce, tomato, and fries", true, 3.99));
            cafeMenu.Add(new MenuItem("Soup of the day", "A cup of the soup of the day, with a side salad", false, 3.69));
            cafeMenu.Add(new MenuItem("Burrito", "A large burrito, with whole pinto beans, salsa, guacamole", true, 4.29));

            Waitress waitress = new Waitress(allMenus);

            waitress.PrintMenu();
            waitress.PrintVegetarianMenu();
        }
    }
}