using System;
using System.Net.Sockets;
using System.Text;

namespace EMud.Networking
{
	public class Client
	{
		private Socket socket;

		public const int MaxLoginAttempts = 3;
		public int LoginAttempts = 0;

		public Client(Socket socket)
		{
			this.socket = socket;
		}

		public void SendLine(params string[] data) 
		{
			foreach (var s in data) {
				Send (s + "\r\n");
			}
		}

		public void Send(string data)
		{
			socket.Send (Encoding.UTF8.GetBytes(data));
		}

		public string ReadLine()
		{
			byte[] buffer = new byte[1024];
			int numRecv = socket.Receive (buffer);

			return Encoding.UTF8.GetString (buffer, 0, numRecv).Trim();
		}

		public string RequestLine(string prompt)
		{
			Send (prompt);

			return ReadLine ();
		}

		public void Close()
		{
			socket.Close ();
		}
	}
}

