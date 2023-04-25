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
			return BitConverter.ToInt16(Read(stream, 2));
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
			return BitConverter.ToUInt32(Read(stream, 4));
		}

		private static byte[] Read(Stream stream, int size)
		{
			var buffer = new byte[size];
			stream.ReadExactly(buffer, 0, size);
			return buffer;
		}
	}
}
