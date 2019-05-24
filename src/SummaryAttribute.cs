using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class SummaryAttribute : Attribute
	{
		public string Summary { get; }

		public SummaryAttribute(string summary) { Summary = summary; }
	}
}
