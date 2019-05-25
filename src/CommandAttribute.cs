using System;

namespace Commands
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class CommandAttribute : Attribute
	{
		public string Name { get; }

		public CommandAttribute() { }
		public CommandAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
