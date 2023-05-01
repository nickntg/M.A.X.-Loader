using MAXLoader.Core.Types.Constants;
using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private GameSurfaceMap LoadSurface(Stream stream)
		{
			Log.Debug($"LoadSurface: start, stream position {stream.Position:X}");

			var surface = new GameSurfaceMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					surface.Surfaces[x - 1, y - 1] = new GameSurfaceCell
					{
						X = x,
						Y = y,
						SurfaceType = (SurfaceType)ReadByte(stream)
					};
				}
			}

			Log.Debug($"LoadSurface: end, stream position {stream.Position:X}");

			return surface;
		}

		private void WriteSurface(Stream stream, GameSurfaceMap surface)
		{
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					WriteByte(stream, (byte)surface.Surfaces[x - 1, y - 1].SurfaceType);
				}
			}
		}
	}
}
