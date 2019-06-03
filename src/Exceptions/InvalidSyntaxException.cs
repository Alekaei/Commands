using Commands.Classes;
using System;

namespace Commands.Exceptions
{
	public class InvalidSyntaxException : Exception
	{
		public Command Command { get; }

		public override string Message {
			get {
				return Command.Syntax;
			}
		}

		public InvalidSyntaxException(Command command)
		{
			Command = command;
		}
	}
}
