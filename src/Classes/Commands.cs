using Kommands.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kommands.Classes
{
	public class Commands
	{
		public IEnumerable<Kommand> kommands { get; }

		public Commands()
		{
			IEnumerable<Type> types = Assembly.GetEntryAssembly().GetTypes()
				.Where(t => t.IsDefined(typeof(KommandGroupAttribute), false) && !t.IsNested);

			IEnumerable<MethodInfo> methods = Assembly.GetEntryAssembly().GetTypes()
				.SelectMany(t => t.GetMethods())
				.Where(m => m.IsDefined(typeof(KommandAttribute), false)
					&& !m.DeclaringType.IsDefined(typeof(KommandGroupAttribute), false));
		}

		private Kommand HandleKommandGroup(Type type)
		{
			KommandGroupAttribute kommandGroupAttribute = type.GetCustomAttribute<KommandGroupAttribute>(false);
			AliasAttribute aliasAttribute = type.GetCustomAttribute<AliasAttribute>(false);
			SummaryAttribute summaryAttribute = type.GetCustomAttribute<SummaryAttribute>(false);

			// Get all methods that have a KommandAttribute
			IEnumerable<MethodInfo> methods = type.GetMethods()
				.Where(m => m.IsDefined(typeof(KommandAttribute), false));
			// Get all nested classes with a KommandGroupAttribute
			IEnumerable<Type> subTypes = type.GetNestedTypes()
				.Where(t => t.IsDefined(typeof(KommandGroupAttribute), false));

			// Default command is when a method in a group has a no name meaning its the default method to call
			Kommand defaultCommand = null;

			List<Kommand> subCommands = new List<Kommand>();
			foreach (MethodInfo method in methods)
			{
				Kommand subCommand = HandleKommand(method);
				// Check if its the default command
				if (subCommand.Name == null && defaultCommand == null)
					defaultCommand = subCommand;
				// Throw exception if second default command is found
				else if (subCommand.Name == null && defaultCommand != null)
					throw new MultipleDefaultCommandsException(type);
				else subCommands.Add(subCommand);
			}

			foreach (Type subType in subTypes)
			{
				subCommands.Add(HandleKommandGroup(subType));
			}

			return new Kommand(
				name: kommandGroupAttribute.Name,
				aliases: aliasAttribute?.Aliases ?? defaultCommand?.Aliases,
				summary: summaryAttribute?.Summary ?? defaultCommand?.Summary,
				subCommands: subCommands,
				methodInfo: defaultCommand?.MethodInfo,
				paramaters: defaultCommand?.Paramaters);
		}

		private Kommand HandleKommand(MethodInfo methodInfo)
		{
			if (!methodInfo.IsStatic)
				throw new MethodNotStaticException(methodInfo);
			if (methodInfo.ContainsGenericParameters)
				throw new GenericParametersException(methodInfo);

			KommandAttribute kommandAttribute = methodInfo.GetCustomAttribute<KommandAttribute>(false);
			AliasAttribute aliasAttribute = methodInfo.GetCustomAttribute<AliasAttribute>(false);
			SummaryAttribute summaryAttribute = methodInfo.GetCustomAttribute<SummaryAttribute>(false);

			return new Kommand(
				name: kommandAttribute.Name,
				aliases: aliasAttribute?.Aliases,
				summary: summaryAttribute?.Summary,
				subCommands: new List<Kommand>(),
				methodInfo: methodInfo,
				paramaters: new Paramaters(methodInfo));
		}
	}
}
