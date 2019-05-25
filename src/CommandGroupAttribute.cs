using System;

namespace Commands
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class CommandGroupAttribute : Attribute
	{
		public string Name { get; }

		public CommandGroupAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
