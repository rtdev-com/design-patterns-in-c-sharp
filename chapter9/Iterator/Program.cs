using System.Collections;

namespace TestingIterator
{
    /**** STEP 1 - Define Menu Pieces ****/
    public interface IMenu
    {
        IEnumerator<MenuItem> CreateIterator();
    }

    public class MenuItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsVegetarian { get; }
        public double Price { get; }

        public MenuItem(string name, string description, bool vegetarian, double price)
        {
            Name = name;
            Description = description;
            IsVegetarian = vegetarian;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name}, ${Price.ToString()} -- {Description}";
        }
    }

    public class DinerMenuIterator : IEnumerator<MenuItem>
    {
        private readonly IEnumerator _enumerator;

        public DinerMenuIterator(MenuItem[] menuItems)
        {
            _enumerator = menuItems.GetEnumerator();
        }

        public void Dispose() { /* Do nothing */ }

        public bool MoveNext() 
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public MenuItem Current => (MenuItem)_enumerator.Current;

        /* explicit implementation of the IEnumerator.Current property, 
         * as required by the IEnumerator interface */
        object IEnumerator.Current
        {
            get { return Current; }
        }
    }

    /**** STEP 2 - Define Menus ****/
    public class PancakeHouseMenu : IMenu
    {
        private readonly List<MenuItem> _menuItems;

        public PancakeHouseMenu()
        {
            _menuItems = new List<MenuItem>();

            AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", true, 2.99);
            AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99);
            AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries", true, 3.49);
            AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", true, 3.59);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            var menuItem = new MenuItem(name, description, vegetarian, price);
            _menuItems.Add(menuItem);
        }

        public List<MenuItem> GetMenuItems()
        {
            return _menuItems;
        }

        public IEnumerator<MenuItem> CreateIterator()
        {
            return _menuItems.GetEnumerator();
        }

        public override string ToString()
        {
            return "Objectville Pancake House Menu";
        }
    }
    
    public class CafeMenu : IMenu
    {
        private readonly Dictionary<string, MenuItem> _menuItems = new Dictionary<string, MenuItem>();

        public CafeMenu()
        {
            AddItem("Veggie Burger and Air Fries", "Veggie burger on a whole wheat bun, lettuce, tomato, and fries", true, 3.99);
            AddItem("Soup of the day", "A cup of the soup of the day, with a side salad", false, 3.69);
            AddItem("Burrito", "A large burrito, with whole pinto beans, salsa, guacamole", true, 4.29);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            var menuItem = new MenuItem(name, description, vegetarian, price);
            _menuItems.Add(menuItem.Name, menuItem);
        }

        public Dictionary<string, MenuItem> GetItems()
        {
            return _menuItems;
        } 

        public IEnumerator<MenuItem> CreateIterator()
        {
            return _menuItems.Values.GetEnumerator();
        }
    }

    public class DinerMenu : IMenu
    {
        private const int MAX_ITEMS = 6;
        private int _numberOfItems;
        private readonly MenuItem[] _menuItems;

        public DinerMenu()
        {
            _menuItems = new MenuItem[MAX_ITEMS];

            AddItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99);
            AddItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99);
            AddItem("Soup of the day", "Soup of the day, with a side of potato salad", false, 3.29);
            AddItem("Hotdog", "A hot dog, with sauerkraut, relish, onions, topped with cheese", false, 3.05);
            AddItem("Steamed Veggies and Brown Rice", "Steamed vegetables over brown rice", true, 3.99);
            AddItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            var menuItem = new MenuItem(name, description, vegetarian, price);
            if (_numberOfItems >= MAX_ITEMS)
            {
                Console.WriteLine("Sorry, the menu is full! Can't add an item to the menu.");
            }
            else
            {
                _menuItems[_numberOfItems++] = menuItem;
            }
        }

        public MenuItem[] GetMenuItems()
        {
            return _menuItems;
        }

        public IEnumerator<MenuItem> CreateIterator()
        {
            return new DinerMenuIterator(_menuItems);
        }
    }

    /**** STEP 3 - Define Waitress ****/
    public class Waitress
    {
        private readonly IMenu _pancakeHouseMenu;
        private readonly IMenu _dinerMenu;
        private readonly IMenu _cafeMenu;

        public Waitress(IMenu pancakeHouseMenu, IMenu dinerMenu, IMenu cafeMenu)
        {
            _pancakeHouseMenu = pancakeHouseMenu;
            _dinerMenu = dinerMenu;
            _cafeMenu = cafeMenu;
        }

        public void PrintMenu()
        {
            var pancakeIterator = _pancakeHouseMenu.CreateIterator();
            var dinerIterator = _dinerMenu.CreateIterator();
            var cafeIterator = _cafeMenu.CreateIterator();

            Console.WriteLine("\nMENU\n---------------");
            Console.WriteLine("\nBREAKFAST:");
            PrintMenu(pancakeIterator);
            Console.WriteLine("\nLUNCH:");
            PrintMenu(dinerIterator);
            Console.WriteLine("\nDINNER:");
            PrintMenu(cafeIterator);
        }

        public void PrintVegetarianMenu()
        {
            Console.WriteLine("\nVEGETARIAN MENU\n---------------");
            PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator());
            PrintVegetarianMenu(_dinerMenu.CreateIterator());
            PrintVegetarianMenu(_cafeMenu.CreateIterator());
        }

        public bool IsItemVegetarian(string name)
        {
            IEnumerator<MenuItem> pancakeIterator = _pancakeHouseMenu.CreateIterator();
            if (IsVegetarian(name, pancakeIterator))
            {
                return true;
            }
            IEnumerator<MenuItem> dinerIterator = _dinerMenu.CreateIterator();
            if (IsVegetarian(name, dinerIterator))
            {
                return true;
            }
            IEnumerator<MenuItem> cafeIterator = _cafeMenu.CreateIterator();
            if (IsVegetarian(name, cafeIterator))
            {
                return true;
            }
            return false;
        }

        private void PrintMenu(IEnumerator<MenuItem> iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = iterator.Current;
                Console.WriteLine($"{menuItem.Name}, {menuItem.Price.ToString()} -- {menuItem.Description}");
            }
        }

        private void PrintVegetarianMenu(IEnumerator<MenuItem> iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = iterator.Current;
                if (menuItem.IsVegetarian)
                {
                    Console.WriteLine($"{menuItem.Name}, {menuItem.Price.ToString()} -- {menuItem.Description}");
                }
            }
        }

        private bool IsVegetarian(string name, IEnumerator<MenuItem> iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = iterator.Current;
                if (menuItem.Name == name && menuItem.IsVegetarian)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /**** STEP 4 - Test ****/
    class Program
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();
            CafeMenu cafeMenu = new CafeMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);

            waitress.PrintMenu();
            waitress.PrintVegetarianMenu();
        }
    }
}