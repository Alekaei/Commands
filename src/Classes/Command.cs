using Commands.Exceptions;
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
		public Command Parent { get; set; }
		public IEnumerable<Command> SubCommands { get; }
		public List<Method> Methods { get; set; }

		public string Syntax => GenerateSyntax();

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, MethodInfo methodInfo)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			Methods = new List<Method> { new Method(methodInfo) };

			foreach (Command subCommand in SubCommands)
				subCommand.Parent = this;
		}

		public Command(string name, string[] aliases, string summary,
			IEnumerable<Command> subCommands, List<Method> methods)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			Methods = methods;

			foreach (Command subCommand in SubCommands)
				subCommand.Parent = this;
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
			if (args.Length > 0)
			{
				if (args[0] == "--help") throw new InvalidSyntaxException(this);
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
			throw new InvalidSyntaxException(this);
		}

		public async Task<bool> ExecuteAsync(ICommandContext context, string[] args)
		{
			if (args.Length > 0)
			{
				if (args[0] == "--help") throw new InvalidSyntaxException(this);
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
			throw new InvalidSyntaxException(this);
		}

		private string GenerateSyntax()
		{
			string syntax = "";

			string name = "";
			Command command = this;
			while (command != null)
			{
				name = $"{command.Name} {name}".Trim();
				command = command.Parent;
			}
			syntax = $"{name}\n";
			if (Summary != null)
				syntax += $"{new string(' ', name.Length)} {Summary}\n";
			foreach (Method method in Methods)
			{
				syntax += $"{new string(' ', name.Length)} {method.Parameters.Syntax}\n";
			}

			foreach (Command subComand in SubCommands)
			{
				syntax += $"{new string(' ', name.Length)} {subComand.Name}\t {subComand.Summary}\n";
			}
			return syntax;
		}
	}
}
