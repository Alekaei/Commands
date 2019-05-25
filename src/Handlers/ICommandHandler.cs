using System.Drawing;
using System.Threading.Tasks;

namespace Commands.Handlers
{
	public interface ICommandHandler
	{
		void HandleCommand(IExecuter executer, string commandText);
		Task HandleCommandAsync(IExecuter executer, string commandText);

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
