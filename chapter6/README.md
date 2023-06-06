# Chapter 6

## Command Pattern
Encapsulates a request as an object, thereby letting you parameterize other objects with different requests,
queue or log requests, and support undoable operations. (p 206)

## Program Output:
```
------------------ Remote Control ------------------
[slot 0] NoCommand                         NoCommand
[slot 1] NoCommand                         NoCommand
[slot 2] NoCommand                         NoCommand
[slot 3] NoCommand                         NoCommand
[slot 4] NoCommand                         NoCommand
[slot 5] NoCommand                         NoCommand
[slot 6] NoCommand                         NoCommand
[ undo ] NoCommand

------------------ Remote Control ------------------
[slot 0] LightOnCommand              LightOffCommand
[slot 1] LightOnCommand              LightOffCommand
[slot 2] CeilingFanHighCommand  CeilingFanOffCommand
[slot 3] NoCommand                         NoCommand
[slot 4] NoCommand                         NoCommand
[slot 5] NoCommand                         NoCommand
[slot 6] NoCommand                         NoCommand
[ undo ] NoCommand

Living Room light is ON
Kitchen light is ON
Kitchen ceiling fan is set to HIGH
------------------ Remote Control ------------------
[slot 0] LightOnCommand              LightOffCommand
[slot 1] LightOnCommand              LightOffCommand
[slot 2] CeilingFanHighCommand  CeilingFanOffCommand
[slot 3] NoCommand                         NoCommand
[slot 4] NoCommand                         NoCommand
[slot 5] NoCommand                         NoCommand
[slot 6] NoCommand                         NoCommand
[ undo ] CeilingFanHighCommand

Kitchen ceiling fan is set to OFF
Living Room light is OFF
Kitchen light is OFF
------------------ Remote Control ------------------
[slot 0] LightOnCommand              LightOffCommand
[slot 1] LightOnCommand              LightOffCommand
[slot 2] CeilingFanHighCommand  CeilingFanOffCommand
[slot 3] MacroCommand                   MacroCommand
[slot 4] NoCommand                         NoCommand
[slot 5] NoCommand                         NoCommand
[slot 6] NoCommand                         NoCommand
[ undo ] LightOffCommand

------------- Pressing Macro Command ON -------------
Living Room light is ON
Kitchen light is ON
Kitchen ceiling fan is set to HIGH
------------- Pressing Macro Command OFF ------------
Living Room light is OFF
Kitchen light is OFF
Kitchen ceiling fan is set to OFF
--------------- Pressing Undo Command --------------
Kitchen ceiling fan is set to HIGH
Kitchen light is ON
Living Room light is ON
```