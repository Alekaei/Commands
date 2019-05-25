using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class FlagAttribute : Attribute
	{
		public char? ShortName { get; }
		public string LongName { get; }

		public FlagAttribute(char shortName) { ShortName = shortName; }
		public FlagAttribute(string longName) { LongName = longName; }
		public FlagAttribute(char shortName, string longName) { ShortName = shortName; LongName = longName; }
	}
}
