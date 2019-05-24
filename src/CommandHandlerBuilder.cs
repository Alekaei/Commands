using Kommands.Handlers;
using System;
using System.IO;

namespace Kommands
{
	public class CommandHandlerBuilder
	{
		public CommandHandlerOptions handlerOptions;

		public CommandHandlerBuilder() { }

		public CommandHandlerBuilder Configure(Action<CommandHandlerOptions> options)
		{
			options.Invoke(handlerOptions);
			return this;
		}

		public ICommandHandler UseDefault(Stream outputStream)
		{
			return new DefaultCommandHandler(handlerOptions, outputStream);
		}
	}
}
