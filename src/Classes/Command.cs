using Commands.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

		public bool IsAsync { get; }

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, MethodInfo methodInfo, Paramaters paramaters)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			MethodInfo = methodInfo;
			Paramaters = paramaters;

			IsAsync = methodInfo.IsDefined(typeof(AsyncStateMachineAttribute), false);
		}

		public Command FindSubCommand(string name)
			=> SubCommands.FirstOrDefault(sc => sc.Name == name.ToLowerInvariant()
				|| sc.Aliases.Contains(name.ToLowerInvariant()));

		public bool Execute(ICommandHandler handler, IExecuter executer, string[] args)
		{
			CommandContext context = new CommandContext(handler, executer);
			return Execute(context, args);
		}

		public bool Execute(ICommandContext context, string[] args)
		{
			Command subCommand = FindSubCommand(args[0]);
			if (subCommand != null)
				return subCommand.Execute(context, args.Skip(1).ToArray());

			if (!Paramaters.TryParseStringArgs(context, args, out object[] commandparams))
				return false;

			try
			{
				MethodInfo.Invoke(null, commandparams);
				return true;
			}
			catch { return false; }
		}

		public async Task<bool> ExecuteAsync(ICommandHandler handler, IExecuter executer, string[] args)
		{
			CommandContext context = new CommandContext(handler, executer);
			return await ExecuteAsync(context, args);
		}

		public async Task<bool> ExecuteAsync(ICommandContext context, string[] args)
		{
			Command subCommand = FindSubCommand(args[0]);
			if (subCommand != null)
				return subCommand.Execute(context, args.Skip(1).ToArray());

			if (!Paramaters.TryParseStringArgs(context, args, out object[] commandparams))
				return false;

			try
			{
				if (IsAsync)
					await (dynamic)MethodInfo.Invoke(null, commandparams);
				else
					MethodInfo.Invoke(null, commandparams);
				return true;
			}
			catch { return false; }
		}
	}
}
