using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Handlers
{
	public class DefaultCommandHandler : CommandHandlerBase
	{
		public readonly Stream OutputStream;
		private readonly Encoding _encoding;
		private readonly int _bufferSize;

		public DefaultCommandHandler(CommandHandlerOptions options, Stream outputStream, Encoding encoding = null, int bufferSize = 512) : base(options)
		{
			OutputStream = outputStream;
			_encoding = encoding ?? Encoding.UTF8;
			_bufferSize = bufferSize;
		}

		public override void Write(string text, params object[] args)
		{
			using (StreamWriter writer = new StreamWriter(OutputStream, _encoding, _bufferSize, true))
			{
				writer.Write(text, args);
				writer.Flush();
			}
		}

		public override void Write(Color color, string text, params object[] args)
		{
			Write(text, args);
		}

		public override async Task WriteAsync(string text, params object[] args)
		{
			using (StreamWriter writer = new StreamWriter(OutputStream, _encoding, _bufferSize, true))
			{
				await writer.WriteAsync(string.Format(text, args));
				await writer.FlushAsync();
			}
		}

		public override async Task WriteAsync(Color color, string Text, params object[] args)
		{
			await WriteAsync(Text, args);
		}

		public override void WriteLine(string text, params object[] args)
		{
			using (StreamWriter writer = new StreamWriter(OutputStream, _encoding, _bufferSize, true))
			{
				writer.WriteLine(text, args);
				writer.Flush();
			}
		}

		public override void WriteLine(Color color, string text, params object[] args)
		{
			WriteLine(text, args);
		}

		public override async Task WriteLineAsync(string text, params object[] args)
		{
			using (StreamWriter writer = new StreamWriter(OutputStream, _encoding, _bufferSize, true))
			{
				await writer.WriteLineAsync(string.Format(text, args));
				await writer.FlushAsync();
			}
		}

		public override async Task WriteLineAsync(Color color, string text, params object[] args)
		{
			await WriteLineAsync(text, args);
		}
	}
}
