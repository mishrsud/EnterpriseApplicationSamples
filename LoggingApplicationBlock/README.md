##Synopsis

A Logging facade that abstracts away most common logging APIs, inspired from Common.Logging

Implementations should defer calling a message's object.ToString() until the message really needs
to be logged to avoid performance penalties.

Each ILogger log method offers to pass in an action of FormatMessageHandler instead of the actual message.
Using this style has the advantage to defer possibly expensive message argument evaluation and formatting (and formatting arguments!) until the message gets
actually logged. If the message is not logged at all (e.g. due to LogLevel settings), 
you won't have to pay the peformance penalty of creating the message.

Based on the ILog interface in Common.Logging authored by Mark Pollack, Bruno Baia and Erich Eichinger.

## Code Example
```csharp
Log.Debug( m=>m("result is {0}", random.NextDouble()) );
Log.Debug(delegate(m) { m("result is {0}", random.NextDouble()); });
```