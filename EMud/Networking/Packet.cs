using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace EMud
{
	public class Packet
	{
		public string Command { get; set; }
		public List<string> Arguments { get; private set; }

		public Packet()
		{
			Arguments = new List<string> ();
		}

		public void Send(Socket socket)
		{
			var buffer = new List<byte> ();

			WriteString (buffer, Command);
			buffer.Add (BitConverter.GetBytes (Arguments.Count));

			foreach (var argument in Arguments) {
				WriteString (buffer, argument);
			}

			socket.Send (buffer.ToArray ());
		}

		private void WriteString(List<byte> buffer, string s)
		{
			var data = Encoding.UTF8.GetBytes (s);

			buffer.Add (BitConverter.GetBytes (data.Length));
			buffer.AddRange (data);
		}
	}
}

