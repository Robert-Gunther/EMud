using System;
using System.Net.Sockets;

namespace EMud
{
	public class Client
	{
		private Socket socket;

		public Client(Socket socket) 
		{
			this.socket = socket;
		}

		public void Send(string command, params string[] arguments) 
		{
			Packet packet = new Packet ();
			packet.Command = command;
			packet.Arguments.AddRange (arguments);
			packet.Send (socket);
		}
	}
}

