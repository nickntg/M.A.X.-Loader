namespace MAXLoader.Core.Helpers
{
	public static class ByteHelpers
	{
		public static string AsAscii(this byte[] bytes)
		{
			return System.Text.Encoding.ASCII.GetString(bytes);
		}
	}
}
