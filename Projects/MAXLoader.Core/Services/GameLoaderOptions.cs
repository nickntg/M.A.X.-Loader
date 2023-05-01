using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private GameOptionsSection LoadGameOptions(Stream stream)
		{
			Log.Debug($"LoadGameOptions: start, stream position {stream.Position:X}");

			var options = new GameOptionsSection
			{
				World = (PlanetType)ReadInt(stream),
				TurnTimer = ReadInt(stream),
				EndTurn = ReadInt(stream),
				StartGold = ReadInt(stream),
				PlayMode = (PlayMode)ReadInt(stream),
				VictoryType = (VictoryType)ReadInt(stream),
				VictoryLimit = ReadInt(stream),
				OpponentType = (OpponentType)ReadInt(stream),
				RawResource = (ResourceLevelType)ReadInt(stream),
				FuelResource = (ResourceLevelType)ReadInt(stream),
				GoldResource = (ResourceLevelType)ReadInt(stream),
				AlienDerelicts = (AlienDerelictsType)ReadInt(stream)
			};

			Log.Debug($"LoadGameOptions: end, stream position {stream.Position:X}");

			return options;
		}

		private void WriteGameOptions(Stream stream, GameOptionsSection gameOptions)
		{
			WriteInt(stream, (int)gameOptions.World);
			WriteInt(stream, gameOptions.TurnTimer);
			WriteInt(stream, gameOptions.EndTurn);
			WriteInt(stream, gameOptions.StartGold);
			WriteInt(stream, (int)gameOptions.PlayMode);
			WriteInt(stream, (int)gameOptions.VictoryType);
			WriteInt(stream, gameOptions.VictoryLimit);
			WriteInt(stream, (int)gameOptions.OpponentType);
			WriteInt(stream, (int)gameOptions.RawResource);
			WriteInt(stream, (int)gameOptions.FuelResource);
			WriteInt(stream, (int)gameOptions.GoldResource);
			WriteInt(stream, (int)gameOptions.AlienDerelicts);
		}
	}
}
