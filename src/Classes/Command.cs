using Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Commands.Classes
{
	public class Command
	{
		public string Name { get; }
		public string[] Aliases { get; }
		public string Summary { get; }
		public IEnumerable<Command> SubCommands { get; }
		public MethodInfo MethodInfo { get; }
		public Paramaters Paramaters { get; }

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, MethodInfo methodInfo, Paramaters paramaters)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			MethodInfo = methodInfo;
			Paramaters = paramaters;
		}

		public void Execute(ICommandHandler handler, IExecuter executer, string[] args)
		{
			CommandContext context = new CommandContext(handler, executer);
			throw new NotImplementedException();
		}
	}
}
