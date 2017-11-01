using System;
using EMud.Networking;
namespace EMudDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Server server = new Server (12345);
			server.Start ();

			Console.Write ("Press enter to shutdown . . .");
			Console.ReadLine ();
			server.Stop ();
		}
	}
}
