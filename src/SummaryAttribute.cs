using System;

namespace Kommands
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class SummaryAttribute : Attribute
	{
		public string Summary { get; }

		public SummaryAttribute(string summary) { Summary = summary; }
	}
}
