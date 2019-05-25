using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class KommandGroupAttribute : Attribute
	{
		public string Name { get; }

		public KommandGroupAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
