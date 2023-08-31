using TestingGumballMachine;

namespace TestingGumballMachine
{
    /**** STEP 1 - Define States ****/
    public interface IState
    {
        public void InsertQuarter();
        public void EjectQuarter();
        public void TurnCrank();
        public void Dispense();
    }

    public class NoQuarterState : IState
    {
        GumballMachine gumballMachine;

        public NoQuarterState(GumballMachine gm)
        {
            gumballMachine = gm;
        }

        public void InsertQuarter()
        {
            gumballMachine.State = gumballMachine.HasQuarterStateMember;
        }

        public void EjectQuarter()
        {
            Console.WriteLine("You haven't inserted a quarter.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("You turned, but there's no quarter.");
        }

        public void Dispense()
        {
            Console.WriteLine("You need to pay first.");
        }

        public override string ToString()
        {
            return "awaiting quarter";
        }
    }

    public class HasQuarterState : IState
    {
        GumballMachine gumballMachine;
        Random randomWinner = new Random();

        public HasQuarterState(GumballMachine gm)
        {
            gumballMachine = gm;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("You can't insert another quarter.");
        }

        public void EjectQuarter()
        {
            gumballMachine.State = gumballMachine.NoQuarterStateMember;
            Console.WriteLine("Quarter returned.");
        }

        public void TurnCrank()
        {
            gumballMachine.State = gumballMachine.SoldOutStateMember;
            Console.WriteLine("You turned...");
            int winner = randomWinner.Next(1, 11);

            if ((winner == 0) && (gumballMachine.Count > 1))
            {
                gumballMachine.State = gumballMachine.WinnerStateMember;
            }
            else
            {
                gumballMachine.State = gumballMachine.SoldOutStateMember;
            }
        }

        public void Dispense()
        {
            Console.WriteLine("No gumball dispensed.");
        }

        public override string ToString()
        {
            return "has a quarter";
        }
    }

    public class SoldState : IState
    {
        GumballMachine gumballMachine;

        public SoldState(GumballMachine gm)
        {
            gumballMachine = gm;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("Please wait, we're already giving you a gumball.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("Sorry, you've already turned the crank");
        }

        public void TurnCrank()
        {
            Console.WriteLine("Turning twice doesn't get you another gumball.");
        }

        public void Dispense()
        {
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count > 0)
            {
                gumballMachine.State = gumballMachine.NoQuarterStateMember;
            }
            else
            {
                Console.WriteLine("Oops, out of gumballs!");
                gumballMachine.State = gumballMachine.SoldOutStateMember;
            }
        }

        public override string ToString()
        {
            return "sold";
        }
    }

    public class SoldOutState : IState
    {
        GumballMachine gumballMachine;

        public SoldOutState(GumballMachine gm)
        {
            gumballMachine = gm;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("You can't insert a quarter, the machine is sold out.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("You can't eject, you haven't inserted a quarter yet.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("You turned, but there are no gumballs.");
        }

        public void Dispense()
        {
            Console.WriteLine("No gumball dispensed.");
        }

        public override string ToString()
        {
            return "sold out";
        }
    }

    public class WinnerState : IState
    {
        GumballMachine gumballMachine;

        public WinnerState(GumballMachine gm)
        {
            gumballMachine = gm;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("Please wait, we're already giving you a gumball.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("Sorry, you already turned the crank.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("Turning twice doesn't get you another gumball.");
        }

        public void Dispense()
        {
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count == 0)
            {
                gumballMachine.State = gumballMachine.SoldOutStateMember;
            }
            else
            {
                gumballMachine.ReleaseBall();
                Console.WriteLine("YOU'RE WINNER! You got two gumballs for your quarter.");
                if (gumballMachine.Count > 0)
                {
                    gumballMachine.State = gumballMachine.NoQuarterStateMember;
                }
                else
                {
                    Console.WriteLine("Oops, out of gumballs!");  
                    gumballMachine.State = gumballMachine.SoldOutStateMember;                
                }
            }
        }

        public override string ToString()
        {
            return "winner";
        }
    }

    /**** STEP 2 - Define Gumball Machine ****/
    public class GumballMachine
    {
        public IState SoldOutStateMember { get; }
        public IState NoQuarterStateMember { get; }
        public IState HasQuarterStateMember { get; }
        public IState SoldStateMember { get; }
        public IState WinnerStateMember { get; }

        public IState State { get; set; }
        public int Count { get; set; } = 0;

        public GumballMachine(int numberGumballs)
        {
            SoldOutStateMember = new SoldOutState(this);
            NoQuarterStateMember = new NoQuarterState(this);
            HasQuarterStateMember = new HasQuarterState(this);
            SoldStateMember = new SoldState(this);
            WinnerStateMember = new WinnerState(this);

            State = SoldOutStateMember;
            Count = numberGumballs;

            if (numberGumballs > 0)
            {
                State = NoQuarterStateMember;
            }
            else
            {
                State = SoldOutStateMember;
            }
        }

        public void InsertQuarter()
        {
            State.InsertQuarter();
        }
        
        public void EjectQuarter()
        {
            State.EjectQuarter();
        }

        public void TurnCrank()
        {
            State.TurnCrank();
        }

        public void SetState(IState s)
        {
            State = s;
        }

        public void ReleaseBall()
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            if (Count > 0) Count--;
        }

        public override string ToString()
        {
            return $"Gumball Model #2023 with {Count} gumballs in a \"{State}\" state.";
        }
    }

    /**** STEP 3 - Test Drive ****/
    class Program
    {
        static void Main(string[] args)
        {
            GumballMachine gumballMachine = new GumballMachine(5);
            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();

            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();

            Console.WriteLine(gumballMachine);
        }
    }
}