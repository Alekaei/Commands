using Commands.Handlers;
using System.Drawing;
using System.Threading.Tasks;

namespace Commands.Classes
{
	public class CommandContext : ICommandContext
	{
		public ICommandHandler Handler { get; }

		public IExecuter Executer { get; }

		public CommandContext(ICommandHandler handler, IExecuter executer)
		{
			Handler = handler;
			Executer = executer;
		}

		public void WriteLine(string text, params object[] args)
			=> Handler?.WriteLine(text, args);
		public void WriteLine(Color color, string text, params object[] args)
			=> Handler?.WriteLine(color, text, args);

		public async Task WriteLineAsync(string text, params object[] args)
			=> await Handler?.WriteLineAsync(text, args);
		public async Task WriteLineAsync(Color color, string text, params object[] args)
			=> await Handler?.WriteLineAsync(color, text, args);

		public void Write(string text, params object[] args)
			=> Handler?.Write(text, args);
		public void Write(Color color, string text, params object[] args)
			=> Handler?.Write(color, text, args);

		public async Task WriteAsync(string text, params object[] args)
			=> await Handler?.WriteAsync(text, args);
		public async Task WriteAsync(Color color, string text, params object[] args)
			=> await Handler?.WriteAsync(color, text, args);
	}
}
