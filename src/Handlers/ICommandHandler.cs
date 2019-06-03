using System.Drawing;
using System.Threading.Tasks;

namespace Commands.Handlers
{
	public interface ICommandHandler
	{
		/// <summary>
		/// Tries to find a command that matches the commantText and executes it synchronously
		/// </summary>
		/// <param name="executer">The execute is used in commands to know who is trying to run this command</param>
		/// <param name="commandText">The command to be parsed</param>
		/// <returns>return true if it successfully ran the command, else false</returns>
		bool HandleCommand(IExecuter executer, string commandText);
		/// <summary>
		/// Tries to find a command that matches the commantText and executes it asynchronously
		/// </summary>
		/// <param name="executer">The execute is used in commands to know who is trying to run this command</param>
		/// <param name="commandText">The command to be parsed</param>
		/// <returns>return true if it successfully ran the command, else false</returns>
		Task<bool> HandleCommandAsync(IExecuter executer, string commandText);

		/// <summary>
		///  Write a line to the output source synchronously
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		void WriteLine(string text, params object[] args);
		/// <summary>
		///  Write a line to the output source synchronously
		/// </summary>
		/// <param name="color">The color used to write in</param>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		void WriteLine(Color color, string text, params object[] args);

		/// <summary>
		///  Write a line to the output source asynchronously
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		Task WriteLineAsync(string text, params object[] args);
		/// <summary>
		///  Write a line to the output source asynchronously
		/// </summary>
		/// <param name="color">The color used to write in</param>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		Task WriteLineAsync(Color color, string text, params object[] args);

		/// <summary>
		///  Write to the output source synchronously
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		void Write(string text, params object[] args);
		/// <summary>
		///  Write to the output source synchronously
		/// </summary>
		/// <param name="color">The color used to write in</param>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		void Write(Color color, string text, params object[] args);

		/// <summary>
		///  Write to the output source asynchronously
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		Task WriteAsync(string text, params object[] args);
		/// <summary>
		///  Write to the output source asynchronously
		/// </summary>
		/// <param name="color">The color used to write in</param>
		/// <param name="text">The text to write</param>
		/// <param name="args">Arguments to insert into the text</param>
		Task WriteAsync(Color color, string text, params object[] args);
	}
}
