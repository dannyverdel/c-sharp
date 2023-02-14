using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.MediatorPattern
{
    /*
	* The Mediator design pattern is a behavioral pattern that allows objects to communicate with each other through a mediator object, rather than directly. 
	* This reduces the dependencies between objects, making it easier to maintain and modify the code. 
	* 
	* A simple example of the Mediator design pattern in C# is a chat room application where multiple users can send messages to each other. 
	* Instead of each user having a direct reference to every other user, they communicate through a mediator (the chat room), which acts as an intermediary and takes care of forwarding the messages.
	* 
	* In this example, the ChatRoom class acts as the mediator, allowing User objects to communicate with each other indirectly through the ChatRoom object. 
	* The Register method is used to add User objects to the chat room, and the Send method is used to send messages between User objects.
	*/

    public class InvokeMediatorPattern : IInvokeMethod
	{
        public void InvokeMethod()
		{
			ChatRoom chat_room = new ChatRoom();

			User danny = new User("Danny");
			User user = new User("User");

			chat_room.Register(danny);
			chat_room.Register(user);

			danny.Send("user", "Hello there");
			user.Send("danny", "Well hello there");
		}
	}

	public interface IChatRoom
	{
		void Register(User user);
		void Send(string from, string to, string message);
	}

	public class ChatRoom : IChatRoom
	{
		private List<User> _users = new List<User>();
		public void Register(User user)
		{
			_users.Add(user);
			user.ChatRoom = this;
		}
		public void Send(string from, string to, string message)
		{
			User? recipient = _users.Find(u => u.Name == to);
			recipient?.Receive(from, message);
		}
	}

	public class User
	{
		public string Name { get; set; }
		public IChatRoom? ChatRoom { get; set; }
		public User(string name) => Name = name;
		public void Send(string to, string message) => ChatRoom?.Send(Name, to, message);
		public void Receive(string from, string message) => $"{from} to {Name}: {message}".Dump();

	}
}

