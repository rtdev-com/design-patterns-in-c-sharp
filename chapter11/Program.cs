namespace ProxyPattern
{
    /**** STEP 1 - Define Bank Interface ****/
    public interface IBank
    {
        public void RequestMoney(double amount);
    }

    /**** STEP 2 - Define Real Bank ****/
    public class RealBank : IBank
    {
        public void RequestMoney(double amount)
        {
            Console.WriteLine($"RealBank processing request for ${amount}.");
        }
    }

    /**** STEP 3 - Define Proxy for a bank ****/
    public class BankProxy : IBank
    {
        private RealBank _realBank; // holds a reference to a real bank

        public BankProxy(RealBank realBank)
        {
            _realBank = realBank;
        }

        public void RequestMoney(double amount)
        {
            if (HasAccess())
            {
                _realBank.RequestMoney(amount);
            }
        }

        public bool HasAccess()
        {
            Console.WriteLine("BankProxy: checking if you have access to this bank account.");
            return true;
        }
    }

    /**** STEP 4 - Define a bank client ****/
    public class BankClient
    {
        // Client doesn't need to know if it is using a proxy or a real bank
        public void ClientMoneyRequest(IBank bank, double amount)
        {
            Console.WriteLine($"Client: requesting ${amount} from a bank.");
            bank.RequestMoney(amount);
        }
    }

    /**** STEP 5 - Test Drive ****/
    class Program
    {
        static void Main(string[] args)
        {
            BankClient client = new BankClient();

            Console.WriteLine("\nClient requesting money from a real bank:");
            RealBank realBank = new RealBank();
            client.ClientMoneyRequest(realBank, 100.50);

            Console.WriteLine("\nClient requesting money from a bank proxy:");
            BankProxy bankProxy = new BankProxy(realBank);
            client.ClientMoneyRequest(bankProxy, 99.77);
        }
    }
}