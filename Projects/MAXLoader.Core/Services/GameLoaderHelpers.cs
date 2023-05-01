using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private Rect ReadRect(Stream stream)
		{
			return new Rect
			{
				Ulx = ReadInt(stream),
				Uly = ReadInt(stream),
				Lrx = ReadInt(stream),
				Lry = ReadInt(stream),
			};
		}

		private void WriteRect(Stream stream, Rect rect)
		{
			WriteInt(stream, rect.Ulx);
			WriteInt(stream, rect.Uly);
			WriteInt(stream, rect.Lrx);
			WriteInt(stream, rect.Lry);
		}

		private Point ReadPoint(Stream stream)
		{
			return new Point
			{
				X = ReadShort(stream),
				Y = ReadShort(stream),
			};
		}

		private void WritePoint(Stream stream, Point point)
		{
			WriteShort(stream, point.X);
			WriteShort(stream, point.Y);
		}

		private byte ReadByte(Stream stream)
		{
			return _byteHandler.ReadByte(stream);
		}

		private void WriteByte(Stream stream, byte value)
		{
			_byteHandler.WriteByte(stream, value);
		}

		private ushort ReadUShort(Stream stream)
		{
			return _byteHandler.ReadUShort(stream);
		}

		private void WriteUShort(Stream stream, ushort value)
		{
			_byteHandler.WriteUShort(stream, value);
		}

		private uint ReadUInt32(Stream stream)
		{
			return _byteHandler.ReadUInt32(stream);
		}

		private void WriteUInt32(Stream stream, uint value)
		{
			_byteHandler.WriteUInt32(stream, value);
		}

		private short ReadShort(Stream stream)
		{
			return _byteHandler.ReadShort(stream);
		}

		private void WriteShort(Stream stream, short value)
		{
			_byteHandler.WriteShort(stream, value);
		}

		private int ReadInt(Stream stream)
		{
			return _byteHandler.ReadInt(stream);
		}

		private void WriteInt(Stream stream, int value)
		{
			_byteHandler.WriteInt(stream, value);
		}
	}
}
