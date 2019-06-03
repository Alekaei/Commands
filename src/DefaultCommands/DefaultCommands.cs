using Commands.Classes;
using Commands.Handlers;

namespace Commands.DefaultCommands
{
	public class DefaultCommands
	{
		[Command("listcommands")]
		[Summary("Get a list of all commands")]
		public static void ListCommand(ICommandContext context)
		{
			foreach (Command command in (context.Handler as CommandHandlerBase).Commands.Commands)
			{
				context.WriteLine($"{command.Syntax}");
			}
		}
	}
}
