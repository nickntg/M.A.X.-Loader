using System;
using System.IO;
using MAXLoader.Core.Helpers;
using MAXLoader.Core.Services.Interfaces;

namespace MAXLoader.Core.Services
{
	public class ByteHandler : IByteHandler
	{
		public short ReadShort(Stream stream)
		{
			return Read(stream, 1)[0];
		}

		public byte ReadByte(Stream stream)
		{
			return Read(stream, 1)[0];
		}

		public string ReadCharArray(Stream stream, int length)
		{
			return Read(stream, length).AsAscii();
		}

		public uint ReadUInt32(Stream stream)
		{
			var b = Read(stream, 2);
			return BitConverter.ToUInt32(b, 0);
		}

		private static byte[] Read(Stream stream, int size)
		{
			var buffer = new byte[size];
			stream.ReadExactly(buffer, 0, size);
			return buffer;
		}
	}
}
