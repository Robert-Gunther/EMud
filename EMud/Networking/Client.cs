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

		public string Username { get; set; }

		public Client(Socket socket)
		{
			this.socket = socket;
		}

		public void SendLine(String format, params Object[] data) 
		{
			Send (format + "\r\n", data);
		}

		public void Send(string format, params Object[] data)
		{
			socket.Send (Encoding.UTF8.GetBytes(String.Format(format, data)));
		}

		public void Send(params byte[] data)
		{
			socket.Send (data);
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

		public void DisableEcho()
		{
			Send (Telnet.IAC, Telnet.DONT, Telnet.ECHO);
		}

		public void EnableEcho()
		{
			Send (Telnet.IAC, Telnet.DO, Telnet.ECHO);
		}

		public void ClearScreen()
		{
			Send ("\u001B[1J\u001B[H");
		}

		public void Close()
		{
			socket.Close ();
		}
	}
}

