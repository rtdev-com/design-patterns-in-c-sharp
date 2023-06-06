# Chapter 4
Chapter 4 contains two types of factory patterns and one programming idiom called Simple Factory.

* [Simple Factory Idiom](SimpleFactory/)
* [Factory Pattern](Factory/)
* [Abstract Factory Patten](AbstractFactory/)

## Design Principles:
* Depend on abstractions. Do not depend upon concrete classes (p 139)
  * No variable should hold a reference to a concrete class (use factory instead of the `new` keyword) (p 143)
  * No class should derive from a concrete class. Derive from an abstraction (p 143)
  * No method should override an implemented method of any of its base classes (p 143)