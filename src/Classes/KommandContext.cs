using Kommands.Handlers;
using System.Drawing;
using System.Threading.Tasks;

namespace Kommands.Classes
{
	public class KommandContext : IKommandContext
	{
		public ICommandHandler Handler { get; }

		public IExecuter Executer { get; }

		public KommandContext(ICommandHandler handler, IExecuter executer)
		{
			Handler = handler;
			Executer = executer;
		}

		// Shorten Writing to output experience from context

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
