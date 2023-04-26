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
	}
}
