using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public class FlagAttribute : Attribute
	{
		public char ShortName { get; }
		public string LongName { get; }

		public FlagAttribute(char shortName, string longName) { ShortName = shortName; LongName = longName; }
	}
}
