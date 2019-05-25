using Commands.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commands.Handlers
{
	public class CommandHandlerBase : ICommandHandler
	{
		public CommandHandlerOptions Options { get; }
		public CommandsList Commands { get; }

		public CommandHandlerBase(CommandHandlerOptions options)
		{
			Options = options;
			Commands = new CommandsList();
		}

		public virtual bool HandleCommand(IExecuter executer, string commandText)
		{
			Regex re = new Regex("\"(?<arg>.*?)\\\"|'(?<arg>.*?)'|(?<arg>[^\\s]+)");
			MatchCollection matches = re.Matches(commandText.Trim());
			string[] split = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				split[i] = matches[i].Groups[1].Value;
			}

			string commandName = split[0];
			string[] args = split.Skip(1).ToArray();

			Command command = Commands.FindCommand(commandName);
			if (command == null) return false;

			return command.Execute(this, executer, args);
		}

		public virtual async Task<bool> HandleCommandAsync(IExecuter executer, string commandText)
		{
			Regex re = new Regex("\"(?<arg>.*?)\\\"|'(?<arg>.*?)'|(?<arg>[^\\s]+)");
			MatchCollection matches = re.Matches(commandText.Trim());
			string[] split = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				split[i] = matches[i].Groups[1].Value;
			}

			string commandName = split[0];
			string[] args = split.Skip(1).ToArray();

			Command command = Commands.FindCommand(commandName);
			if (command == null) return false;

			return await command.ExecuteAsync(this, executer, args);
		}

		public virtual void Write(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void Write(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteAsync(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteAsync(Color color, string Text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void WriteLine(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void WriteLine(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteLineAsync(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteLineAsync(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}
	}
}
