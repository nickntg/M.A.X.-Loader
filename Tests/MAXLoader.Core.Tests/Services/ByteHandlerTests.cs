using System;
using System.IO;
using MAXLoader.Core.Services;
using Xunit;

namespace MAXLoader.Core.Tests.Services
{
	public class ByteHandlerTests
	{
		[Fact]
		public void Skip_ShouldReadBytesFromStream()
		{
			var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04 });
			var byteHandler = new ByteHandler();

			byteHandler.Skip(stream, 2);

			Assert.Equal(2, stream.Position);
		}

		[Fact]
		public void ReadShort_ShouldReadShortFromStream()
		{
			var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04 });
			var byteHandler = new ByteHandler();

			var result = byteHandler.ReadShort(stream);

			Assert.Equal(0x0201, result);
			Assert.Equal(2, stream.Position);
		}

		[Fact]
		public void ReadByte_ShouldReadByteFromStream()
		{
			var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04 });
			var byteHandler = new ByteHandler();

			var result = byteHandler.ReadByte(stream);

			Assert.Equal(0x01, result);
			Assert.Equal(1, stream.Position);
		}

		[Fact]
		public void ReadCharArray_ShouldReadCharArrayFromStream()
		{
			var stream = new MemoryStream("ABCD"u8.ToArray());
			var byteHandler = new ByteHandler();

			var result = byteHandler.ReadCharArray(stream, 4);

			Assert.Equal("ABCD", result);
			Assert.Equal(4, stream.Position);
		}

		[Fact]
		public void ReadUInt32_ShouldReadUInt32FromStream()
		{
			var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04 });
			var byteHandler = new ByteHandler();

			var result = byteHandler.ReadUInt32(stream);

			Assert.Equal(0x04030201L, result);
			Assert.Equal(4, stream.Position);
		}

		[Fact]
		public void ReadInt_WhenCalled_ReturnsCorrectValue()
		{
			var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x00, 0x03 });
			var byteHandler = new ByteHandler();

			var result = byteHandler.ReadInt(stream);

			Assert.Equal(50332161, result);
		}

		[Fact]
		public void WriteUShort_WritesCorrectValue()
		{
			const ushort expected = 0x1234;
			var stream = new MemoryStream();
			var byteHandler = new ByteHandler();

			byteHandler.WriteUShort(stream, expected);
			stream.Position = 0;
			var actual = BitConverter.ToUInt16(stream.ToArray(), 0);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteShort_WritesCorrectValue()
		{
			var stream = new MemoryStream();
			const short expectedValue = 123;
			var byteHandler = new ByteHandler();

			byteHandler.WriteShort(stream, expectedValue);
			stream.Position = 0;
			var actualValue = BitConverter.ToInt16(stream.ToArray(), 0);

			Assert.Equal(expectedValue, actualValue);
		}

		[Fact]
		public void WriteInt_WritesCorrectValue()
		{
			var stream = new MemoryStream();
			const int expectedValue = 123;
			var byteHandler = new ByteHandler();

			byteHandler.WriteInt(stream, expectedValue);
			stream.Position = 0;
			var actualValue = BitConverter.ToInt32(stream.ToArray(), 0);

			Assert.Equal(expectedValue, actualValue);
		}

		[Fact]
		public void WriteByte_WritesCorrectValue()
		{
			var stream = new MemoryStream();
			const byte expectedValue = 123;
			var byteHandler = new ByteHandler();

			byteHandler.WriteByte(stream, expectedValue);
			stream.Position = 0;
			var actualValue = stream.ReadByte();

			Assert.Equal(expectedValue, actualValue);
		}

		[Fact]
		public void WriteCharArray_WritesCorrectValue()
		{
			var stream = new MemoryStream();
			const string expectedValue = "abc\0\0";
			const int size = 5;
			var byteHandler = new ByteHandler();

			byteHandler.WriteCharArray(stream, "abc", size);
			stream.Position = 0;
			var actualValue = System.Text.Encoding.ASCII.GetString(stream.ToArray(), 0, size);

			Assert.Equal(expectedValue, actualValue);
		}

		[Fact]
		public void WriteUInt32_WritesCorrectValue()
		{
			var stream = new MemoryStream();
			const uint expectedValue = 123;
			var byteHandler = new ByteHandler();

			byteHandler.WriteUInt32(stream, expectedValue);
			stream.Position = 0;
			var actualValue = BitConverter.ToUInt32(stream.ToArray(), 0);

			Assert.Equal(expectedValue, actualValue);
		}
	}
}
