using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Class)]
	public class KommandGroupAttribute : Attribute
	{
		public string Name { get; }

		public KommandGroupAttribute(string name) { Name = name.ToLowerInvariant(); }
	}
}
