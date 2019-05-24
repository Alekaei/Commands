using System.Drawing;
using System.Threading.Tasks;

namespace Kommands.Handlers
{
	public interface ICommandHandler
	{
		void HandleCommand(IExecuter executer, string command);
		Task HandleCommandAsync(IExecuter executer, string command);

		void WriteLine(string text, params object[] args);
		void WriteLine(Color color, string text, params object[] args);

		Task WriteLineAsync(string text, params object[] args);
		Task WriteLineAsync(Color color, string text, params object[] args);

		void Write(string text, params object[] args);
		void Write(Color color, string text, params object[] args);

		Task WriteAsync(string text, params object[] args);
		Task WriteAsync(Color color, string Text, params object[] args);
	}
}
