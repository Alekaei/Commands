using Commands.Handlers;
using System.Drawing;
using System.Threading.Tasks;

namespace Commands
{
	public interface ICommandContext
	{
		/// <summary>
		/// The ICommandHandler currently executing this command
		/// </summary>
		ICommandHandler Handler { get; }
		/// <summary>
		/// The IExecuter trying to execute this command
		/// </summary>
		IExecuter Executer { get; }

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
