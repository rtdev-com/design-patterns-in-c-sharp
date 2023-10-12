# Chapter 11

## Proxy Pattern
Provides a surrogate or placeholder for another object to control access to it. (p 455)

## Program Output:
```
Client requesting money from a real bank:
Client: requesting $100.5 from a bank.
RealBank processing request for $100.5.

Client requesting money from a bank proxy:
Client: requesting $99.77 from a bank.
BankProxy: checking if you have access to this bank account.
RealBank processing request for $99.77.
```