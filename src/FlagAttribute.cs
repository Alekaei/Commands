using System;

namespace Commands
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class FlagAttribute : Attribute
	{
		public char? ShortName { get; }
		public string LongName { get; }
		public bool DefaultValue { get; }

		public FlagAttribute(char shortName) { ShortName = shortName; }
		public FlagAttribute(char shortName, bool defaultValue) { ShortName = shortName; DefaultValue = defaultValue; }
		public FlagAttribute(string longName) { LongName = longName; }
		public FlagAttribute(string longName, bool defaultValue) { LongName = longName; DefaultValue = defaultValue; }
		public FlagAttribute(char shortName, string longName) { ShortName = shortName; LongName = longName; }
		public FlagAttribute(char shortName, string longName, bool defaultValue)
		{
			ShortName = shortName;
			LongName = longName;
			DefaultValue = defaultValue;
		}
	}
}
