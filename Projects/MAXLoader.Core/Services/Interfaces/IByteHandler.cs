﻿using System.IO;

namespace MAXLoader.Core.Services.Interfaces
{
	public interface IByteHandler
	{
		short ReadShort(Stream stream);
		byte ReadByte(Stream stream);
		string ReadCharArray(Stream stream, int size);
		uint ReadUInt32(Stream stream);
		void Skip(Stream stream, int size);
	}
}
