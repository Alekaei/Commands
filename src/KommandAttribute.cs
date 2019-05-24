using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Method)]
	public class KommandAttribute : Attribute
	{
		public string Name { get; }

		public KommandAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
