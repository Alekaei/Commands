using Commands.Handlers;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Commands
{
	public class CommandHandlerBuilder
	{
		public CommandHandlerOptions handlerOptions;

		public CommandHandlerBuilder()
		{
			handlerOptions = new CommandHandlerOptions();
		}

		/// <summary>
		/// Configuration options for the <see cref="ICommandHandler"/>
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public CommandHandlerBuilder Configure(Action<CommandHandlerOptions> options)
		{
			options.Invoke(handlerOptions);
			return this;
		}

		/// <summary>
		/// Add TypeConverter for a given Type
		/// </summary>
		/// <typeparam name="TC">The TypeConverter</typeparam>
		/// <typeparam name="T">The Type</typeparam>
		/// <returns></returns>
		public CommandHandlerBuilder AddTypeConverterForType<TC, T>() where TC : TypeConverter
		{
			Attribute[] attribute = new Attribute[1];
			TypeConverterAttribute converterAttribute = new TypeConverterAttribute(typeof(TC));
			attribute[0] = converterAttribute;
			TypeDescriptor.AddAttributes(typeof(T), attribute);
			return this;
		}

		/// <summary>
		/// Use the default <see cref="ICommandHandler"/>
		/// </summary>
		/// <param name="outputStream">A <see cref="Stream"/> that is writable</param>
		/// <returns><see cref="DefaultCommandHandler"/> as <see cref="ICommandHandler"/></returns>
		public ICommandHandler UseDefault(Stream outputStream)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream);
		}
		/// <summary>
		/// Use the default <see cref="ICommandHandler"/>
		/// </summary>
		/// <param name="outputStream">A <see cref="Stream"/> that is writable</param>
		/// <param name="encoding">The encoding type to use when writing to <paramref name="outputStream"/></param>
		/// <returns><see cref="DefaultCommandHandler"/> as <see cref="ICommandHandler"/></returns>
		public ICommandHandler UseDefault(Stream outputStream, Encoding encoding)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream, encoding);
		}
		/// <summary>
		/// Use the default <see cref="ICommandHandler"/>
		/// </summary>
		/// <param name="outputStream">A <see cref="Stream"/> that is writable</param>
		/// <param name="encoding">The encoding type to use when writing to <paramref name="outputStream"/></param>
		/// <param name="bufferSize">The buffer size to use when writing to <paramref name="outputStream"/></param>
		/// <returns><see cref="DefaultCommandHandler"/> as <see cref="ICommandHandler"/></returns>
		public ICommandHandler UseDefault(Stream outputStream, Encoding encoding, int bufferSize)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream, encoding, bufferSize);
		}
	}
}
