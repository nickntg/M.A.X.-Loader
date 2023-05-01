using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private static TheRest LoadTheRest(Stream stream)
		{
			Log.Debug($"LoadTheRest: start, stream position {stream.Position:X}");

			if (stream.Position >= stream.Length)
			{
				return null;
			}

			var theRest = new TheRest { TheRestOfTheData = new byte[stream.Length - stream.Position] };
			var _ = stream.Read(theRest.TheRestOfTheData, 0, (int)(stream.Length - stream.Position));

			Log.Debug($"LoadTheRest: end, stream position {stream.Position:X}");

			return theRest;
		}

		private static void WriteTheRest(Stream stream, TheRest rest)
		{
			if (rest == null)
			{
				return;
			}

			stream.Write(rest.TheRestOfTheData, 0, rest.TheRestOfTheData.Length);
		}
	}
}
