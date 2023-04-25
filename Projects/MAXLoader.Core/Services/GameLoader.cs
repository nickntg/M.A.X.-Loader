using System;
using System.IO;
using MAXLoader.Core.Services.Interfaces;
using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Services
{
	public class GameLoader : IGameLoader
	{
		private readonly IByteHandler _byteHandler;

		public GameLoader(IByteHandler byteHandler)
		{
			_byteHandler = byteHandler;
		}

		public GameFile LoadGameFile(SaveFileType saveFileType, string fileName)
		{
			ValidateInput(saveFileType, fileName);

			var gameFile = new GameFile();

			using (var sr = new StreamReader(fileName))
			{
				gameFile.Header = LoadGameFileHeader(sr.BaseStream);
			}

			return gameFile;
		}

		private GameFileHeader LoadGameFileHeader(Stream stream)
		{
			var header = new GameFileHeader
			{
				Version = (FileFormatVersion)_byteHandler.ReadByte(stream),
				SaveFileType = (SaveFileType)_byteHandler.ReadByte(stream),
				SaveGameName = _byteHandler.ReadCharArray(stream, 30),
				PlanetType = (PlanetType)_byteHandler.ReadByte(stream),
				MissionIndex = _byteHandler.ReadShort(stream)
			};

			for (var i = 1; i <= 4; i++)
			{
				header.TeamNames.Add(_byteHandler.ReadCharArray(stream, 30));
			}

			for (var i = 1; i <= 4; i++)
			{
				header.TeamTypes.Add((TeamType)_byteHandler.ReadByte(stream));
			}

			for (var i = 1; i <= 4; i++)
			{
				header.TeamClans.Add((TeamClan)_byteHandler.ReadByte(stream));
			}

			header.RngSeed = _byteHandler.ReadUInt32(stream);
			header.OpponentType = (OpponentType)_byteHandler.ReadByte(stream);
			header.TurnTimer = _byteHandler.ReadShort(stream);
			header.EndTurn = _byteHandler.ReadShort(stream);
			header.PlayMode = (PlayMode)_byteHandler.ReadByte(stream);

			return header;
		}

		private static void ValidateInput(SaveFileType saveFileType, string fileName)
		{
			if (saveFileType != SaveFileType.SinglePlayerCustomGame)
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}

			if (fileName.ToLower().EndsWith(".dta"))
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}
		}
	}
}
