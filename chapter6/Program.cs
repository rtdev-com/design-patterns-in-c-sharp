using System.Text;

namespace Command
{
    /**** STEP 1 - Implementing the Command Interface ****/
    public interface ICommand
    {
       // Interface that all command classes implement
       public void Execute();
       public void Undo();
    }

    /**** STEP 2 - Implementing Commands ****/
    public class CeilingFanHighCommand : ICommand
    {
        private CeilingFan _ceilingFan;
        private int _prevSpeed;

        public CeilingFanHighCommand(CeilingFan ceilingFan)
        {
            _ceilingFan = ceilingFan;
        }
        
        public void Execute()
        {
            _prevSpeed = _ceilingFan.Speed;
            _ceilingFan.High();
        }

        public void Undo()
        {
            if (_prevSpeed == CeilingFan.HIGH) { _ceilingFan.High(); }
            else if (_prevSpeed == CeilingFan.MEDIUM) { _ceilingFan.Medium(); }
            else if (_prevSpeed == CeilingFan.LOW) { _ceilingFan.Low(); }
            else if (_prevSpeed == CeilingFan.OFF) { _ceilingFan.Off(); }
        }
    }

    public class CeilingFanOffCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private int _prevSpeed;

        public CeilingFanOffCommand(CeilingFan ceilingFan) 
        { 
            _ceilingFan = ceilingFan; 
        }

        public void Execute() 
        { 
            _prevSpeed = _ceilingFan.Speed;
            _ceilingFan.Off(); 
        }
        public void Undo() 
        { 
            if (_prevSpeed == CeilingFan.HIGH) { _ceilingFan.High(); }
            else if (_prevSpeed == CeilingFan.MEDIUM) { _ceilingFan.Medium(); }
            else if (_prevSpeed == CeilingFan.LOW) { _ceilingFan.Low(); }
            else if (_prevSpeed == CeilingFan.OFF) { _ceilingFan.Off(); } 
        }
    }


    public class LightOnCommand : ICommand 
    {
        private Light _light;

        public LightOnCommand(Light light) { _light = light; }

        public void Execute() { _light.On(); }
        public void Undo() { _light.Off(); }
    }

    public class LightOffCommand : ICommand 
    {
        private Light _light;

        public LightOffCommand(Light light) { _light = light; }

        public void Execute() { _light.Off(); }
        public void Undo() { _light.On(); }
    }

    public class NoCommand : ICommand
    {
        public void Execute() { /* Do Nothing */ }
        public void Undo() { /* Do Nothing */ }

    }

    public class MacroCommand : ICommand
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                _commands[i].Execute();
            }
        }

        public void Undo()
        {
            for (int i = _commands.Length - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }
    }

    /**** STEP 3 - Implementing Object State & Undo ****/
    public class CeilingFan
    {
        public static readonly int HIGH = 3;
        public static readonly int MEDIUM = 2;
        public static readonly int LOW = 1;
        public static readonly int OFF = 0;

        public string Location;
        public int Speed;

        public CeilingFan(string location)
        {
            Location = location;
            Speed = OFF;
        }

        // Methods for setting the speed of the ceiling fan
        public void High() 
        { 
            Console.WriteLine($"{Location} ceiling fan is set to HIGH");
            Speed = HIGH; 
        }
        public void Medium() 
        { 
            Console.WriteLine($"{Location} ceiling fan is set to MEDIUM");
            Speed = MEDIUM;
        }
        public void Low() 
        { 
            Console.WriteLine($"{Location} ceiling fan is set to LOW");
            Speed = LOW; 
        }
        public void Off() 
        { 
            Console.WriteLine($"{Location} ceiling fan is set to OFF");
            Speed = OFF; 
        }
    }

    public class Light 
    { 
        public static readonly int ON = 1;
        public static readonly int OFF = 0;

        public string Location;
        public int State;

        public Light(string location)
        {
            Location = location;
        }

        public void On()
        {
            Console.WriteLine($"{Location} light is ON");
            State = ON;
        }

        public void Off()
        {
            Console.WriteLine($"{Location} light is OFF");
            State = OFF;
        }
    }

    /**** STEP 4 - Remote Control with the Undo Ability ****/
    public class RemoteControlWithUndo
    {
        // This remote has 7 command slots
        private static readonly int NUM_OF_COMMANDS = 7;
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _undoCommand;   // Where we stash the last command executed

        public RemoteControlWithUndo()
        {
            _onCommands = new ICommand[NUM_OF_COMMANDS];
            _offCommands = new ICommand[NUM_OF_COMMANDS];

            // Initialize commands
            ICommand noCommand = new NoCommand();

            for (int i = 0; i < NUM_OF_COMMANDS; i++)
            {
                _onCommands[i] = noCommand;
                _offCommands[i] = noCommand;
            }

            _undoCommand = noCommand;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonWasPushed(int slot)
        {
            _onCommands[slot].Execute();
            _undoCommand = _onCommands[slot];
        }        
        
        public void OffButtonWasPushed(int slot)
        {
            _offCommands[slot].Execute();
            _undoCommand = _offCommands[slot];
        }

        public void UndoButtonWasPushed()
        {
            _undoCommand.Undo();
        }

        public override String ToString()
        {
            var result = new StringBuilder();
            
            // Calculate the padding to make output pretty
            int max_length = 43; // 43 is a minimum width we want due to our header size
            for (int i = 0; i < NUM_OF_COMMANDS; i++)
            {
                int total_length = _onCommands[i].GetType().Name.Length + _offCommands[i].GetType().Name.Length;
                if (total_length > max_length)
                {
                    max_length = total_length + 1;
                }
            }

            result.AppendLine($"------------------ Remote Control ------------------");
            for (int i = 0; i < NUM_OF_COMMANDS; i++)
            {
                int padding_length = max_length - (_onCommands[i].GetType().Name.Length + _offCommands[i].GetType().Name.Length);
                string padding = new String(' ', padding_length);
                result.AppendLine($"[slot {i}] {_onCommands[i].GetType().Name}{padding}{_offCommands[i].GetType().Name}");
            }
            result.AppendLine($"[ undo ] {_undoCommand.GetType().Name}");

            return result.ToString();
        }
    }

    /**** STEP 5 - Test the Remote Control ****/
    class Program
    {
        static void Main(string[] args)
        {
            RemoteControlWithUndo remoteControl = new RemoteControlWithUndo();

            Console.WriteLine(remoteControl);

            Light livingRoomLight = new Light("Living Room");
            LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);

            Light kitchenRoomLight = new Light("Kitchen");
            LightOnCommand kitchenRoomLightOn = new LightOnCommand(kitchenRoomLight);
            LightOffCommand kitchenRoomLightOff = new LightOffCommand(kitchenRoomLight);
            remoteControl.SetCommand(1, kitchenRoomLightOn, kitchenRoomLightOff);

            CeilingFan kitchenCeilingFan = new CeilingFan("Kitchen");
            CeilingFanHighCommand ceilingFanHigh = new CeilingFanHighCommand(kitchenCeilingFan);
            CeilingFanOffCommand ceilingFanOff = new CeilingFanOffCommand(kitchenCeilingFan);
            remoteControl.SetCommand(2, ceilingFanHigh, ceilingFanOff);

            Console.WriteLine(remoteControl);

            remoteControl.OnButtonWasPushed(0); // Living Room Light ON
            remoteControl.OnButtonWasPushed(1); // Kitchen Light ON
            remoteControl.OnButtonWasPushed(2); // Ceiling fan HIGH

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed(); // Ceiling fan OFF
            remoteControl.OffButtonWasPushed(0); // Living Room Light OFF
            remoteControl.OffButtonWasPushed(1); // Kitchen Light OFF

            // Test macro command
            ICommand[] partyOn = {livingRoomLightOn, kitchenRoomLightOn, ceilingFanHigh};
            ICommand[] partyOff = {livingRoomLightOff, kitchenRoomLightOff, ceilingFanOff};
            MacroCommand partyOnMacro = new MacroCommand(partyOn); 
            MacroCommand partyOffMacro = new MacroCommand(partyOff);
            remoteControl.SetCommand(3, partyOnMacro, partyOffMacro);

            Console.WriteLine(remoteControl);
            
            Console.WriteLine($"------------- Pressing Macro Command ON -------------");
            remoteControl.OnButtonWasPushed(3);

            Console.WriteLine($"------------- Pressing Macro Command OFF ------------");
            remoteControl.OffButtonWasPushed(3);

            Console.WriteLine($"--------------- Pressing Undo Command --------------");
            remoteControl.UndoButtonWasPushed();
        }
    } 
}