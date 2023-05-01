using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private GameFileHeader LoadGameFileHeader(Stream stream)
		{
			Log.Debug($"LoadGameFileHeader: start, stream position {stream.Position:X}");

			var header = new GameFileHeader
			{
				Version = (FileFormatVersion)ReadShort(stream),
				SaveFileType = (SaveFileType)ReadByte(stream),
				SaveGameName = _byteHandler.ReadCharArray(stream, 30).Trim('\0'),
				PlanetType = (PlanetType)ReadByte(stream),
				MissionIndex = ReadShort(stream)
			};

			for (var i = 1; i <= 4; i++)
			{
				header.TeamNames.Add(_byteHandler.ReadCharArray(stream, 30).Trim('\0'));
			}

			for (var i = 1; i <= 5; i++)
			{
				header.TeamTypes.Add((TeamType)ReadByte(stream));
			}

			for (var i = 1; i <= 5; i++)
			{
				header.TeamClans.Add((TeamClan)ReadByte(stream));
			}

			header.RngSeed = ReadUInt32(stream);
			header.OpponentType = (OpponentType)ReadByte(stream);
			header.TurnTimer = ReadShort(stream);
			header.EndTurn = ReadShort(stream);
			header.PlayMode = (PlayMode)ReadByte(stream);

			Log.Debug($"LoadGameFileHeader: end, stream position {stream.Position:X}");

			return header;
		}

		private void WriteGameFileHeader(Stream stream, GameFileHeader header)
		{
			WriteShort(stream, (short)header.Version);
			WriteByte(stream, (byte)header.SaveFileType);
			_byteHandler.WriteCharArray(stream, header.SaveGameName, 30);
			WriteByte(stream, (byte)header.PlanetType);
			WriteShort(stream, header.MissionIndex);

			for (var i = 1; i <= 4; i++)
			{
				_byteHandler.WriteCharArray(stream, header.TeamNames[i - 1], 30);
			}

			for (var i = 1; i <= 5; i++)
			{
				WriteByte(stream, (byte)header.TeamTypes[i - 1]);
			}

			for (var i = 1; i <= 5; i++)
			{
				WriteByte(stream, (byte)header.TeamClans[i - 1]);
			}

			WriteUInt32(stream, header.RngSeed);
			WriteByte(stream, (byte)header.OpponentType);
			WriteShort(stream, header.TurnTimer);
			WriteShort(stream, header.EndTurn);
			WriteByte(stream, (byte)header.PlayMode);
		}
	}
}
