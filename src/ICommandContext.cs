using Commands.Handlers;
using System.Drawing;
using System.Threading.Tasks;

namespace Commands
{
	public interface ICommandContext
	{
		ICommandHandler Handler { get; }
		IExecuter Executer { get; }

		void WriteLine(string text, params object[] args);
		void WriteLine(Color color, string text, params object[] args);

		Task WriteLineAsync(string text, params object[] args);
		Task WriteLineAsync(Color color, string text, params object[] args);

		void Write(string text, params object[] args);
		void Write(Color color, string text, params object[] args);

		Task WriteAsync(string text, params object[] args);
		Task WriteAsync(Color color, string text, params object[] args);
	}
}
