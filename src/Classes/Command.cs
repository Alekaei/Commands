using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Commands.Classes
{
	public class Command
	{
		public string Name { get; }
		public string[] Aliases { get; }
		public string Summary { get; set; }
		public IEnumerable<Command> SubCommands { get; }
		public List<Method> Methods { get; set; }

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, MethodInfo methodInfo)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			Methods = new List<Method> { new Method(methodInfo) };
		}

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, List<Method> methods)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			Methods = methods;
		}

		private IEnumerable<Command> GetMatchingCommands(string name)
		{
			foreach (Command subCommand in SubCommands)
			{
				// If the first argument doesnt match the sub command name or isnt an alias skip to next sub command
				if (subCommand.Name != name && !subCommand.Aliases.Contains(name.ToLowerInvariant()))
					continue;

				yield return subCommand;
			}
		}

		public Command FindSubCommand(string name)
			=> SubCommands.FirstOrDefault(sc => sc.Name == name.ToLowerInvariant()
				|| sc.Aliases.Contains(name.ToLowerInvariant()));

		public bool Execute(ICommandContext context, string[] args)
		{
			//Command subCommand = (args.Length > 0) ? FindSubCommand(args[0]) : null;
			//if (subCommand != null)
			//	return subCommand.Execute(context, args.Skip(1).ToArray());

			//if (!Paramaters.TryParseStringArgs(context, args, out object[] commandparams))
			//	return false;

			//try
			//{
			//	MethodInfo.Invoke(null, commandparams);
			//	return true;
			//}
			//catch { return false; }
			if (args.Length > 0)
			{
				foreach (Command command in GetMatchingCommands(args[0]))
				{
					bool res = command.Execute(context, args.Skip(1).ToArray());
					if (res) return true;
				}
			}

			foreach (Method method in Methods)
			{
				if (!method.Parameters.TryParseStringArgs(context, args, out object[] commandParams)) continue;
				bool res = method.TryExecute(commandParams);
				if (res) return true;
			}
			return false;
		}

		public async Task<bool> ExecuteAsync(ICommandContext context, string[] args)
		{
			//Command subCommand = (args.Length > 0) ? FindSubCommand(args[0]) : null;
			//if (subCommand != null)
			//	return subCommand.Execute(context, args.Skip(1).ToArray());

			//if (!Paramaters.TryParseStringArgs(context, args, out object[] commandparams))
			//	return false;

			//try
			//{
			//	if (IsAsync)
			//		await (Task)MethodInfo.Invoke(null, commandparams);
			//	else
			//		MethodInfo.Invoke(null, commandparams);
			//	return true;
			//}
			//catch { return false; }
			if (args.Length > 0)
			{
				foreach (Command command in GetMatchingCommands(args[0]))
				{
					bool res = await command.ExecuteAsync(context, args.Skip(1).ToArray());
					if (res) return true;
				}
			}

			foreach (Method method in Methods)
			{
				if (!method.Parameters.TryParseStringArgs(context, args, out object[] commandParams)) continue;
				bool res = await method.TryExecuteAsync(commandParams);
				if (res) return true;
			}
			return false;
		}
	}
}
