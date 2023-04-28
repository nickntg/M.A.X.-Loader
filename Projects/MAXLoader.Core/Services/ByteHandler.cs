using System;
using System.IO;
using MAXLoader.Core.Helpers;
using MAXLoader.Core.Services.Interfaces;

namespace MAXLoader.Core.Services
{
	public class ByteHandler : IByteHandler
	{
		public ushort ReadUShort(Stream stream)
		{
			return BitConverter.ToUInt16(Read(stream, 2));
		}

		public void WriteUShort(Stream stream, ushort value)
		{
			stream.Write(BitConverter.GetBytes(value));
		}

		public void Skip(Stream stream, int size)
		{
			Read(stream, size);
		}

		public short ReadShort(Stream stream)
		{
			return BitConverter.ToInt16(Read(stream, 2));
		}

		public void WriteShort(Stream stream, short value)
		{
			stream.Write(BitConverter.GetBytes(value));
		}

		public int ReadInt(Stream stream)
		{
			return BitConverter.ToInt32(Read(stream, 4));
		}

		public void WriteInt(Stream stream, int value)
		{
			stream.Write(BitConverter.GetBytes(value));
		}

		public byte ReadByte(Stream stream)
		{
			return Read(stream, 1)[0];
		}

		public void WriteByte(Stream stream, byte value)
		{
			stream.WriteByte(value);
		}

		public string ReadCharArray(Stream stream, int size)
		{
			return Read(stream, size).AsAscii();
		}

		public void WriteCharArray(Stream stream, string str, int size)
		{
			var b = new byte[size];

			var strBytes = System.Text.Encoding.ASCII.GetBytes(str);

			for (var i = 0; i < size; i++)
			{
				b[i] = i < strBytes.Length ? strBytes[i] : (byte)0;
			}
			stream.Write(b, 0, size);
		}

		public uint ReadUInt32(Stream stream)
		{
			return BitConverter.ToUInt32(Read(stream, 4));
		}

		public void WriteUInt32(Stream stream, uint value)
		{
			stream.Write(BitConverter.GetBytes(value));
		}

		private static byte[] Read(Stream stream, int size)
		{
			var buffer = new byte[size];
			stream.ReadExactly(buffer, 0, size);
			return buffer;
		}
	}
}
