using Commands.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Commands.Examples.GameUser
{
	public class UserTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string sValue = value as string;
			// Will find an online user with a matching name
			User user = Users.Instance.OnlineUsers.FirstOrDefault(u => u.Name == sValue);
			return user ?? base.ConvertFrom(context, culture, value);
		}
	}

	// Assigning the above type converter to our user.
	// Our user inherits from IExecuter so that we can gain access to them in commands through the context
	[TypeConverter(typeof(UserTypeConverter))]
	public class User : IExecuter
	{
		public string Name { get; set; }
		public byte Health { get; set; }

		public bool IsAdmin { get; set; }

		public Dictionary<int, int> Items { get; set; }

		public User()
		{
			Health = 100;
			Items = new Dictionary<int, int>();
		}

		public void GiveItem(int id, int quantity)
		{
			if (Items.ContainsKey(id))
				Items[id] += quantity;
			else Items.Add(id, quantity);
		}

		public override string ToString()
			=> $"{(IsAdmin ? "[Admin] " : "")}{Name} has {Health} hp and the following items: {string.Join(", ", Items.Select(i => $"{i.Value}x {i.Key}"))}";
	}

	public class Users
	{
		public List<User> OnlineUsers { get; set; }
		public User CurrentUser { get; set; }
		public static Users Instance;

		public Users()
		{
			OnlineUsers = new List<User>
			{
				new User() {Name = "Mike", Health = 10, IsAdmin = true},
				new User() {Name = "John", Health = 40}
			};

			CurrentUser = OnlineUsers[0];
			Users.Instance = this;
		}

		// give <itemId>
		[Command("give")]
		public static void GiveItem(ICommandContext context, int itemId)
			=> GiveItem(context, null, itemId, 1);

		// give <itemId> <quantity>
		[Command("give")]
		public static void GiveItem(ICommandContext context, int itemId, int quantity)
			=> GiveItem(context, null, itemId, quantity);

		// give <user> <itemId>
		[Command("give")]
		public static void GiveItem(ICommandContext context, User user, int itemId)
			=> GiveItem(context, user, itemId, 1);

		// give <user> <itemId> <quantity>
		[Command("give")]
		[Summary("Give an item to yourself or another user. *requires admin")]
		public static void GiveItem(ICommandContext context, User user, int itemId, int quantity)
		{
			// if the user we want to give an item to is null, set that user to ourselves
			user = user ?? context.Executer as User;

			// If the executer isnt an admin they cant give items
			if ((context.Executer as User).IsAdmin)
			{
				user.GiveItem(itemId, quantity);
				context.WriteLine("Gave {0}x {1} to {2}", quantity, itemId, user.Name);
			}
			else
			{
				context.WriteLine("You are not an admin");
			}
		}

		[CommandGroup("user")]
		[Summary("manage users")]
		public static class UserCommands
		{
			// user
			[Command]
			public static void ListUser(ICommandContext context)
				=> ListUser(context, null);

			// user <user>
			[Command]
			public static void ListUser(ICommandContext context, User user)
			{
				user = user ?? context.Executer as User;
				context.WriteLine(user.ToString());
			}

			// user set <user>
			[Command("set")]
			[Alias("s")]
			[Summary("Set the current user")]
			public static void SetCurrentUser(ICommandContext context, User user)
			{
				Users.Instance.CurrentUser = user;
				context.WriteLine("Set current user to {0}", user.Name);
			}

			// user listall [-v]
			// user list [-v]
			// user ls [-v]
			[Command("listall")]
			[Alias("list", "ls")]
			[Summary("List all online users")]
			public static void ListAll(ICommandContext context, [Flag('v', false)] bool verbose)
			{
				if (verbose)
					foreach (User user in Users.Instance.OnlineUsers)
						context.WriteLine(user.ToString());
				else
					foreach (User user in Users.Instance.OnlineUsers)
						context.WriteLine("{0}{1} has {2} hp", user.IsAdmin ? "[Admin] " : "", user.Name, user.Health);
			}
		}
	}

	class Program
	{

		static void Main(string[] args)
		{
			Users users = new Users();

			ICommandHandler handler = new CommandHandlerBuilder()
				.Configure(options =>
				{
					options.Debug = true;
				})
				.UseDefault(
					 outputStream: Console.OpenStandardOutput(),
					 encoding: Console.OutputEncoding);

			while (true)
			{
				string input = System.Console.ReadLine();
				if (input == "q") break;
				if (input.StartsWith("/"))
				{
					string command = input.TrimStart('/');
					bool res = handler.HandleCommand(users.CurrentUser, command);
					if (res)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Successfully executed command");
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Failed to execute command");
					}
					Console.ResetColor();
					Console.WriteLine();
				}
			}
		}
	}
}
