# Commands
Commands is a .Net Standard attribute command handler designed to be extendable to your desires.

> The project is still in early development and multiple features arent implemented yet or only partially so

## Features
* Command Aliases
* Command Summaries
* Text string to parameter parsing
* params support
* Uses type converter to convert from string to desired type meaning its easy to add your own converters
* IExecuter that can be inherited to include info such as the user executing the command and features available on said user such as inventory, health, etc
* Exceptions for invalid formatting on attributes

## Example creating default CommandHandler

```csharp
// Pass an output stream such as the console
ICommandHandler commandHandler = new CommandHandlerBuilder()
	.UseDefault(OutputStream); 
```

## Goals
1. Create a base that will contain all the necessary features to handle attribute commands and parsing
2. Create handlers for various platforms such as a unity dev console

## License
MIT @ Aleksei Ivanov