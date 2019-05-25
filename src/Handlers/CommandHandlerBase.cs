using Commands.Classes;
using System;
using System.Drawing;
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

		public virtual void HandleCommand(IExecuter executer, string command)
		{
			throw new NotImplementedException();
		}

		public virtual async Task HandleCommandAsync(IExecuter executer, string command)
		{
			throw new NotImplementedException();
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
