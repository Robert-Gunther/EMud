using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Collections.Generic;

namespace EMud.Networking
{
	public class StateObject 
	{
		public Socket socket;
		public const int BufferSize = 1024;
		public byte[] buffer = new byte[BufferSize];
		public StringBuilder builder = new StringBuilder();
	}

	public class Server
	{
		private ushort port;
		private Socket socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private List<Client> connectedClients = new List<Client>();

		public Server (ushort port)
		{
			this.port = port;
		}

		public void Start()
		{
			IPAddress address = IPAddress.Loopback;
			IPEndPoint endPoint = new IPEndPoint (address, port);

			try {
				socket.Bind(endPoint);
				socket.Listen(50);
				BeginListening();
			} catch(Exception e) {
				Console.WriteLine ("Error: {0}", e.ToString ());
			}
		}

		public void Stop()
		{
			socket.Close ();
		}

		private void BeginListening() {
			socket.BeginAccept(ConnectionAccepted, null);
		}

		private void ConnectionAccepted(IAsyncResult result) 
		{
			var acceptedSocket = socket.EndAccept (result);

			// TODO: Detect bans and other possible issues here.
			// TODO: Connection throttling to fight against possible (D)DoS attacks.
			// TODO: Other security features.
			connectedClients.Add (new Client (acceptedSocket));

			Console.WriteLine ("Socket Accepted");
			BeginListening ();
		}
	}
}

