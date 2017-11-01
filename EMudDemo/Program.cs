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

				if (username == "hello" && password == "okay") {
					client.SendLine ("Login successful!");
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


		public static void Main (string[] args)
		{
			Server server = new Server (12345);

			server.HandleConnection += ProcessLogin;
			server.Start ();

			Console.WriteLine ("Press enter to shutdown . . .");
			Console.ReadLine ();
			server.Stop ();
		}
	}
}
