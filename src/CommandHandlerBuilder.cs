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

		public CommandHandlerBuilder Configure(Action<CommandHandlerOptions> options)
		{
			options.Invoke(handlerOptions);
			return this;
		}

		public CommandHandlerBuilder AddTypeConverterForType<TC, T>() where TC : TypeConverter
		{
			Attribute[] attribute = new Attribute[1];
			TypeConverterAttribute converterAttribute = new TypeConverterAttribute(typeof(TC));
			attribute[0] = converterAttribute;
			TypeDescriptor.AddAttributes(typeof(T), attribute);
			return this;
		}

		public ICommandHandler UseDefault(Stream outputStream)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream);
		}
		public ICommandHandler UseDefault(Stream outputStream, Encoding encoding)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream, encoding);
		}
		public ICommandHandler UseDefault(Stream outputStream, Encoding encoding, int bufferSize)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream, encoding, bufferSize);
		}
	}
}
