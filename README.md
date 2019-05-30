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

## Example Command
All command attributes must be on **static void** methods.
Methods can also be async.
You also need to include a ICommandContext parameter

```csharp
[Command("giveitem")]
[Alias("give")]
[Summary("Give items to players.")]
public static void Give(ICommandContext context, User user, int itemId, int quantity)
{
	...
}
```

## Executing Commands

```csharp
// First create an instance of a command handler
// In this case im going to use the default handler with the console output stream
ICommandHandler handler = new CommandHandlerFactory()
	.UseDefault(Console.OpenStandardOutput(), Console.OutputEncoding);

// To execute commands just run this method and pass in a IExecuter and the command 
//if you use IExecuter in your commands its recommened you 
//check that its not null before using it
handler.HandleCommand(users.CurrentUser, "give John 1015 300");
// This should give john the item with id 1015 and a quantity of 300
```

## Goals
1. Create a base that will contain all the necessary features to handle attribute commands and parsing
2. Create handlers for various platforms such as a unity dev console

## License
MIT @ Aleksei Ivanov