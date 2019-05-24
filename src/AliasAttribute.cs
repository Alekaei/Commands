using System;
using System.Linq;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class AliasAttribute : Attribute
	{
		public string[] Aliases { get; }

		public AliasAttribute(params string[] aliases) { Aliases = aliases.Select(s => s.ToLowerInvariant()).ToArray(); }
	}
}
