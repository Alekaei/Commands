using System;
using System.Linq;

namespace Commands
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AliasAttribute : Attribute
	{
		public string[] Aliases { get; }

		public AliasAttribute(params string[] aliases) { Aliases = aliases.Select(s => s.ToLowerInvariant()).ToArray(); }
	}
}
