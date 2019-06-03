using System;

namespace Commands
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class NameAttribute : Attribute
	{
		public string Name { get; }

		public NameAttribute(string name) { Name = name; }
	}
}
