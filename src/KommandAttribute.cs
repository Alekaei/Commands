using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class KommandAttribute : Attribute
	{
		public string Name { get; }

		public KommandAttribute() { }
		public KommandAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
