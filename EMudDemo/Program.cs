using System;
using EMud.Networking;

namespace EMudDemo
{
	class MainClass
	{
		public static bool ProcessLogin(Client client) 
		{
			client.LoginAttempts++;

			try {
				var username = client.RequestLine ("Username: ");
				var password = client.RequestLine ("Password: ");
				client.DisableEcho();

				if (username == "hello" && password == "okay") {
					client.Username = username;
					return true;
				} else {
					if(client.LoginAttempts >= Client.MaxLoginAttempts) {
						client.SendLine("Too many failed login attempts.");
						return false;
					}

					client.SendLine (string.Format("Login failed! (Try {0}/{1})", client.LoginAttempts, Client.MaxLoginAttempts));

					return ProcessLogin (client);
				}
			} catch(Exception) {
				return false;
			}
		}

		private static Server server;

		public static void Main (string[] args)
		{
			server = new Server (12345);

			server.ConnectionEstablished += Server_Established;
			server.HandleConnection += ProcessLogin;
			server.OnLogin += Server_OnLogin;

			server.Start ();

			Console.WriteLine ("Press Ctrl-C to gracefully shutdown the server . . .");
			Console.CancelKeyPress += (sender, e) => {
				Console.WriteLine("Server is shutting down.");
				server.Stop ();
			};

			while (server.Running) {
				// Process game logic.
			}
		}

		static void Server_OnLogin (Server server, Client client)
		{
			Console.WriteLine("{0} has successfully connected", client.Username);
			client.SendLine ("Welcome to E-Mud!");

			client.ClearScreen ();
			int count = server.Clients.Count;
			client.SendLine ("There {0} currently {1} {2} online.", count > 1 ? "are" : "is", count, count > 1 ? "players" : "player");
		}

		static void Server_Established ()
		{
			Console.WriteLine ("E-Mud Example Server is successfully running.");
		}
	}
}
