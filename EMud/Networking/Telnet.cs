using System;

namespace EMud
{
	public static class Telnet
	{
		public const byte IAC = 0xFF;
		public const byte WILL = 0xFB;
		public const byte WONT = 0xFC;
		public const byte DO = 0xFD;
		public const byte DONT = 0xFE;
		public const byte ECHO = 0x01;
	}
}

