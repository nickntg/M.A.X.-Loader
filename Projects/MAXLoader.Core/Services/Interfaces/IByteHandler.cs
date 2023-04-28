using System.IO;

namespace MAXLoader.Core.Services.Interfaces
{
	public interface IByteHandler
	{
		short ReadShort(Stream stream);
		void WriteShort(Stream stream, short value);
		ushort ReadUShort(Stream stream);
		void WriteUShort(Stream stream, ushort value);
		byte ReadByte(Stream stream);
		void WriteByte(Stream stream, byte value);
		string ReadCharArray(Stream stream, int size);
		void WriteCharArray(Stream stream, string str, int size);
		uint ReadUInt32(Stream stream);
		void WriteUInt32(Stream stream, uint value);
		int ReadInt(Stream stream);
		void WriteInt(Stream stream, int value);
		void Skip(Stream stream, int size);
	}
}
