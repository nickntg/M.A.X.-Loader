using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private void WriteGameManagerState(Stream stream, GameManagerState gms)
		{
			WriteByte(stream, (byte)gms.ActiveTurnTeam);
			WriteByte(stream, (byte)gms.PlayerTeam);
			WriteInt(stream, gms.TurnCounter);
			WriteShort(stream, gms.GameState);
			WriteShort(stream, gms.TurnTimer);
			WriteInt(stream, gms.Effects);
			WriteInt(stream, gms.ClickScroll);
			WriteInt(stream, gms.QuickScroll);
			WriteInt(stream, gms.FastMovement);
			WriteInt(stream, gms.FollowUnit);
			WriteInt(stream, gms.AutoSelect);
			WriteInt(stream, gms.EnemyHalt);
		}

		private GameManagerState LoadGameManagerState(Stream stream)
		{
			Log.Debug($"LoadGameManagerState: start, stream position {stream.Position:X}");

			var gmState = new GameManagerState
			{
				ActiveTurnTeam = (Team)ReadByte(stream),
				PlayerTeam = (Team)ReadByte(stream),
				TurnCounter = ReadInt(stream),
				GameState = ReadShort(stream),
				TurnTimer = ReadShort(stream),
				Effects = ReadInt(stream),
				ClickScroll = ReadInt(stream),
				QuickScroll = ReadInt(stream),
				FastMovement = ReadInt(stream),
				FollowUnit = ReadInt(stream),
				AutoSelect = ReadInt(stream),
				EnemyHalt = ReadInt(stream)
			};

			Log.Debug($"LoadGameManagerState: start, stream position {stream.Position:X}");

			return gmState;
		}
	}
}
