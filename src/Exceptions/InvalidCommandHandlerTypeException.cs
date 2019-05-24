using System;

namespace Kommands.Exceptions
{
	public class InvalidCommandHandlerTypeException : Exception
	{
		public InvalidCommandHandlerTypeException() : base("Attempted to create instance of invalid command handler") { }
	}
}
