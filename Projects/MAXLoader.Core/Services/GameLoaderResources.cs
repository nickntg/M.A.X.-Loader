using MAXLoader.Core.Types.Constants;
using MAXLoader.Core.Types;
using System.IO;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private GameSurfaceResourcesMap LoadResources(Stream stream)
		{
			Log.Debug($"LoadResources: start, stream position {stream.Position:X}");

			var resources = new GameSurfaceResourcesMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					var word = ReadUShort(stream);
					var resource = new CellResource
					{
						X = x,
						Y = y,
						Amount = word & 0x1f,
						ResourceType = (ResourceType)((word & 0xe0) >> 5),
						RedTeamVisible = (word & 0x2000) != 0,
						GreenTeamVisible = (word & 0x1000) != 0,
						BlueTeamVisible = (word & 0x800) != 0,
						GreyTeamVisible = (word & 0x400) != 0
					};

					if (resource.Amount == 0)
					{
						resource.ResourceType = ResourceType.None;
					}

					resources.Resources[x - 1, y - 1] = resource;
				}
			}

			Log.Debug($"LoadResources: end, stream position {stream.Position:X}");

			return resources;
		}

		private void WriteResources(Stream stream, GameSurfaceResourcesMap resources)
		{
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					var word = PackResource(resources.Resources[x - 1, y - 1]);

					WriteUShort(stream, (ushort)word);
				}
			}
		}

		private static int PackResource(CellResource resource)
		{
			var word = resource.Amount;

			if (resource.Amount == 0)
			{
				resource.ResourceType = ResourceType.Raw;
			}

			word = word | (((byte)resource.ResourceType) << 5);
			if (resource.RedTeamVisible)
			{
				word = word | 0x2000;
			}

			if (resource.GreenTeamVisible)
			{
				word = word | 0x1000;
			}

			if (resource.BlueTeamVisible)
			{
				word = word | 0x800;
			}

			if (resource.GreyTeamVisible)
			{
				word = word | 0x400;
			}

			return word;
		}
	}
}
