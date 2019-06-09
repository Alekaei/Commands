# Commands
Commands is a .NET Standard 2.0 attribute based command framework.

> The project is still in early development

## Features
* Command Aliases
* Command Summaries
* Parameter Parsing
* params keyword support
* Parsers are extensible to custom types
* ICommandContext that stores information about relevant to the command such as the IExecuter who called the command, as well as the ICommandHandler that executed the command   

## Installation 
### From Visual Studio
Go to `Project>Manage Nuget Packages...` then navigate to the `Browse` tab and search for `Alekaei.Commands` and press the install button.
> Note: You may need to check the `Include prerelease` checkbox for that package to come up
### From Command Line
Navigate to the project directory and type the following command
```
dotnet add package Alekaei.Commands
```

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
1. Provide an easy to use command framework for various projects
2. Create a base that will contain all the necessary features to handle attribute commands and parsing
3. Create handlers for various platforms such as a unity dev console

## License
MIT @ Aleksei Ivanov