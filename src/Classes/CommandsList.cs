using Commands.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Commands.Classes
{
	public class CommandsList
	{
		public IEnumerable<Command> commands { get; }

		public CommandsList()
		{
			IEnumerable<Type> types = Assembly.GetEntryAssembly().GetTypes()
				.Where(t => t.IsDefined(typeof(CommandGroupAttribute), false)
					&& !(t.IsNested && (t.DeclaringType?.IsDefined(typeof(CommandGroupAttribute), false) ?? false)));

			IEnumerable<MethodInfo> methods = Assembly.GetEntryAssembly().GetTypes()
				.SelectMany(t => t.GetMethods())
				.Where(m => m.IsDefined(typeof(CommandAttribute), false)
					&& !(m.DeclaringType?.IsDefined(typeof(CommandGroupAttribute), false) ?? false));
		}

		public Command FindCommand(string name)
			=> commands.FirstOrDefault(c => c.Name == name.ToLowerInvariant()
				|| c.Aliases.Contains(name.ToLowerInvariant()));

		private Command HandleCommandGroup(Type type)
		{
			CommandGroupAttribute commandGroupAttribute = type.GetCustomAttribute<CommandGroupAttribute>(false);
			AliasAttribute aliasAttribute = type.GetCustomAttribute<AliasAttribute>(false);
			SummaryAttribute summaryAttribute = type.GetCustomAttribute<SummaryAttribute>(false);

			// Get all methods that have a CommandAttribute
			IEnumerable<MethodInfo> methods = type.GetMethods()
				.Where(m => m.IsDefined(typeof(CommandAttribute), false));
			// Get all nested classes with a CommandGroupAttribute
			IEnumerable<Type> subTypes = type.GetNestedTypes()
				.Where(t => t.IsDefined(typeof(CommandGroupAttribute), false));

			// Default command is when a method in a group has a no name meaning its the default method to call
			Command defaultCommand = null;

			List<Command> subCommands = new List<Command>();
			foreach (MethodInfo method in methods)
			{
				Command subCommand = HandleCommand(method);
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
				subCommands.Add(HandleCommandGroup(subType));
			}

			return new Command(
				name: commandGroupAttribute.Name,
				aliases: aliasAttribute?.Aliases ?? defaultCommand?.Aliases,
				summary: summaryAttribute?.Summary ?? defaultCommand?.Summary,
				subCommands: subCommands,
				methodInfo: defaultCommand?.MethodInfo,
				paramaters: defaultCommand?.Paramaters);
		}
		private Command HandleCommand(MethodInfo methodInfo)
		{
			if (!methodInfo.IsStatic)
				throw new MethodNotStaticException(methodInfo);
			if (methodInfo.ContainsGenericParameters)
				throw new GenericParametersException(methodInfo);

			CommandAttribute commandAttribute = methodInfo.GetCustomAttribute<CommandAttribute>(false);
			AliasAttribute aliasAttribute = methodInfo.GetCustomAttribute<AliasAttribute>(false);
			SummaryAttribute summaryAttribute = methodInfo.GetCustomAttribute<SummaryAttribute>(false);

			return new Command(
				name: commandAttribute.Name,
				aliases: aliasAttribute?.Aliases,
				summary: summaryAttribute?.Summary,
				subCommands: new List<Command>(),
				methodInfo: methodInfo,
				paramaters: new Parameters(methodInfo));
		}
	}
}
