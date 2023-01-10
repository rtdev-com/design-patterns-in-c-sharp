# Chapter 4

## Factory Pattern
Defines an interface for creating an object, but lets subclasses decide which class to instantiate. It lets a class defer instantiation to subclasses (p 134)

## Design Principle:
* Depend on abstractions. Do not depend upon concrete classes (p 139)
  * No variable should hold a reference to a concrete class (use factory instead of the `new` keyword) (p 143)
  * No class should derive from a concrete class. Derive from an abstraction (p 143)
  * No method should override an implemented method of any of its base classes (p 143)

## Program Output:
```
...
```