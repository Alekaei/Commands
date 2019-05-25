using System;

namespace Kommands.Exceptions
{
	public class MultipleDefaultCommandsException : Exception
	{
		public Type Type { get; }
		public MultipleDefaultCommandsException(Type type)
			: base($"Multiple default commands detected in {type.FullName}")
		{ Type = Type; }
	}
}
