using System;
using System.Collections.Generic;
using System.IO;
using MAXLoader.Core.Services.Interfaces;
using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Constants;
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
				gameFile.Options = LoadGameOptions(sr.BaseStream);
				gameFile.Surface = LoadSurface(sr.BaseStream);
				gameFile.GameResources = LoadResources(sr.BaseStream);
				gameFile.TeamInfos = LoadTeamInfos(sr.BaseStream);
				gameFile.GameManagerState = LoadGameManagerState(sr.BaseStream);
				gameFile.TheRest = LoadTheRest(sr.BaseStream);
			}

			return gameFile;
		}

		public void SaveGameFile(GameFile game, string fileName)
		{
			ValidateFileName(fileName);

			using (var sw = new StreamWriter(fileName))
			{
				WriteGameFileHeader(sw.BaseStream, game.Header);
				WriteGameOptions(sw.BaseStream, game.Options);
				WriteSurface(sw.BaseStream, game.Surface);
				WriteResources(sw.BaseStream, game.GameResources);
				WriteTeamInfos(sw.BaseStream, game.TeamInfos);
				WriteGameManagerState(sw.BaseStream, game.GameManagerState);
				WriteTheRest(sw.BaseStream, game.TheRest);
			}
		}

		private void WriteGameManagerState(Stream stream, GameManagerState gms)
		{
			_byteHandler.WriteByte(stream, (byte)gms.ActiveTurnTeam);
			_byteHandler.WriteByte(stream, (byte)gms.PlayerTeam);
			_byteHandler.WriteInt(stream, gms.TurnCounter);
			_byteHandler.WriteShort(stream, gms.GameState);
			_byteHandler.WriteShort(stream, gms.TurnTimer);
			_byteHandler.WriteInt(stream, gms.Effects);
			_byteHandler.WriteInt(stream, gms.ClickScroll);
			_byteHandler.WriteInt(stream, gms.QuickScroll);
			_byteHandler.WriteInt(stream, gms.FastMovement);
			_byteHandler.WriteInt(stream, gms.FollowUnit);
			_byteHandler.WriteInt(stream, gms.AutoSelect);
			_byteHandler.WriteInt(stream, gms.EnemyHalt);
		}

		private GameManagerState LoadGameManagerState(Stream stream)
		{
			return new GameManagerState
			{
				ActiveTurnTeam = (Team)_byteHandler.ReadByte(stream),
				PlayerTeam = (Team)_byteHandler.ReadByte(stream),
				TurnCounter = _byteHandler.ReadInt(stream),
				GameState = _byteHandler.ReadShort(stream),
				TurnTimer = _byteHandler.ReadShort(stream),
				Effects = _byteHandler.ReadInt(stream),
				ClickScroll = _byteHandler.ReadInt(stream),
				QuickScroll = _byteHandler.ReadInt(stream),
				FastMovement = _byteHandler.ReadInt(stream),
				FollowUnit = _byteHandler.ReadInt(stream),
				AutoSelect = _byteHandler.ReadInt(stream),
				EnemyHalt = _byteHandler.ReadInt(stream)
			};
		}

		private static TheRest LoadTheRest(Stream stream)
		{
			if (stream.Position >= stream.Length)
			{
				return null;
			}

			var theRest = new TheRest { TheRestOfTheData = new byte[stream.Length - stream.Position] };
			var _ = stream.Read(theRest.TheRestOfTheData, 0, (int)(stream.Length - stream.Position));
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

		private Dictionary<Team, TeamInfo> LoadTeamInfos(Stream stream)
		{
			return new Dictionary<Team, TeamInfo>
			{
				{ Team.Red, LoadTeamInfo(stream) },
				{ Team.Green, LoadTeamInfo(stream) },
				{ Team.Blue, LoadTeamInfo(stream) },
				{ Team.Gray, LoadTeamInfo(stream) }
			};
		}

		private TeamInfo LoadTeamInfo(Stream stream)
		{
			var ti = new TeamInfo();

			for (var i = 1; i <= Globals.MarkersSize; i++)
			{
				ti.Markers[i - 1] = new Point
				{
					X = _byteHandler.ReadShort(stream),
					Y = _byteHandler.ReadShort(stream)
				};
			}

			ti.TeamType = (TeamType)_byteHandler.ReadByte(stream);
			ti.Field41 = _byteHandler.ReadByte(stream);
			ti.TeamClan = (TeamClan)_byteHandler.ReadByte(stream);

			for (var i = ResearchTopic.Attack; i <= ResearchTopic.Cost; i++)
			{
				var researchTopic = new ResearchTopicInfo
				{
					ResearchLevel = _byteHandler.ReadUInt32(stream),
					TurnsToComplete = _byteHandler.ReadUInt32(stream),
					Allocation = _byteHandler.ReadUInt32(stream)
				};
				ti.ResearchTopics.Add(i, researchTopic);
			}

			ti.VictoryPoints = _byteHandler.ReadUInt32(stream);
			ti.LastUnitId = _byteHandler.ReadUShort(stream);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				ti.UnitCounters.Add(i, _byteHandler.ReadByte(stream));
			}

			for (var i = 1; i <= Globals.ScreenLocationSize; i++)
			{
				var point = new ScreenLocation
				{
					X = _byteHandler.ReadByte(stream),
					Y = _byteHandler.ReadByte(stream)
				};
				ti.ScreenLocations[i - 1] = point;
			}

			for (var i = 1; i <= Globals.ScoreGraphSize; i++)
			{
				ti.ScoreGraph[i-1] = _byteHandler.ReadShort(stream);
			}

			ti.SelectedUnit = _byteHandler.ReadUShort(stream);
			ti.ZoomLevel = _byteHandler.ReadUShort(stream);
			ti.ScreenPosition = new Point
			{
				X = _byteHandler.ReadShort(stream),
				Y = _byteHandler.ReadShort(stream)
			};
			ti.GuiButtonStateRange = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateScan = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateStatus = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateColors = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateHits = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateAmmo = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateMinimap2X = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateMinimapTnt = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateGrid = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateNames = _byteHandler.ReadByte(stream) != 0;
			ti.GuiButtonStateSurvey = _byteHandler.ReadByte(stream) != 0;
			ti.StatsFactoriesBuilt = _byteHandler.ReadShort(stream);
			ti.StatsMinesBuilt = _byteHandler.ReadShort(stream);
			ti.StatsBuildingsBuilt = _byteHandler.ReadShort(stream);
			ti.StatsUnitsBuilt = _byteHandler.ReadShort(stream);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				ti.Casualties.Add(i, _byteHandler.ReadUShort(stream));
			}

			ti.StatsGoldSpentOnUpgrades = _byteHandler.ReadShort(stream);

			return ti;
		}

		private void WriteTeamInfos(Stream stream, Dictionary<Team, TeamInfo> teamInfos)
		{
			WriteTeamInfo(stream, teamInfos[Team.Red]);
			WriteTeamInfo(stream, teamInfos[Team.Green]);
			WriteTeamInfo(stream, teamInfos[Team.Blue]);
			WriteTeamInfo(stream, teamInfos[Team.Gray]);
		}

		private void WriteTeamInfo(Stream stream, TeamInfo ti)
		{
			for (var i = 1; i <= Globals.MarkersSize; i++)
			{
				_byteHandler.WriteShort(stream, ti.Markers[i - 1].X);
				_byteHandler.WriteShort(stream, ti.Markers[i - 1].Y);
			}

			_byteHandler.WriteByte(stream, (byte)ti.TeamType);
			_byteHandler.WriteByte(stream, ti.Field41);
			_byteHandler.WriteByte(stream, (byte)ti.TeamClan);

			for (var i = ResearchTopic.Attack; i <= ResearchTopic.Cost; i++)
			{
				_byteHandler.WriteUInt32(stream, ti.ResearchTopics[i].ResearchLevel);
				_byteHandler.WriteUInt32(stream, ti.ResearchTopics[i].TurnsToComplete);
				_byteHandler.WriteUInt32(stream, ti.ResearchTopics[i].Allocation);
			}

			_byteHandler.WriteUInt32(stream, ti.VictoryPoints);
			_byteHandler.WriteUShort(stream, ti.LastUnitId);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				_byteHandler.WriteByte(stream, ti.UnitCounters[i]);
			}

			for (var i = 1; i <= Globals.ScreenLocationSize; i++)
			{
				_byteHandler.WriteByte(stream, ti.ScreenLocations[i - 1].X);
				_byteHandler.WriteByte(stream, ti.ScreenLocations[i - 1].Y);
			}

			for (var i = 1; i <= Globals.ScoreGraphSize; i++)
			{
				_byteHandler.WriteShort(stream, ti.ScoreGraph[i-1]);
			}

			_byteHandler.WriteUShort(stream, ti.SelectedUnit);
			_byteHandler.WriteUShort(stream, ti.ZoomLevel);
			_byteHandler.WriteShort(stream, ti.ScreenPosition.X);
			_byteHandler.WriteShort(stream, ti.ScreenPosition.Y);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateRange ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateScan ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateStatus ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateColors ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateHits ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateAmmo ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateMinimap2X ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateMinimapTnt ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateGrid ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateNames ? (byte)1 : (byte)0);
			_byteHandler.WriteByte(stream, ti.GuiButtonStateSurvey ? (byte)1 : (byte)0);
			_byteHandler.WriteShort(stream, ti.StatsFactoriesBuilt);
			_byteHandler.WriteShort(stream, ti.StatsMinesBuilt);
			_byteHandler.WriteShort(stream, ti.StatsBuildingsBuilt);
			_byteHandler.WriteShort(stream, ti.StatsUnitsBuilt);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				_byteHandler.WriteUShort(stream, ti.Casualties[i]);
			}

			_byteHandler.WriteShort(stream, ti.StatsGoldSpentOnUpgrades);
		}

		private GameSurfaceResourcesMap LoadResources(Stream stream)
		{
			var resources = new GameSurfaceResourcesMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					var word = _byteHandler.ReadUShort(stream);
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

					resources.Resources[x-1,y-1] = resource;
				}
			}

			return resources;
		}

		private void WriteResources(Stream stream, GameSurfaceResourcesMap resources)
		{
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					var word = PackResource(resources.Resources[x - 1, y - 1]);

					_byteHandler.WriteUShort(stream, (ushort)word);
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

		private GameSurfaceMap LoadSurface(Stream stream)
		{
			var surface = new GameSurfaceMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					surface.Surfaces[x-1, y-1] = new GameSurfaceCell
					{
						X = x,
						Y = y,
						SurfaceType = (SurfaceType)_byteHandler.ReadByte(stream)
					};
				}
			}

			return surface;
		}

		private void WriteSurface(Stream stream, GameSurfaceMap surface)
		{
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					_byteHandler.WriteByte(stream, (byte)surface.Surfaces[x-1, y-1].SurfaceType);
				}
			}
		}

		private GameOptionsSection LoadGameOptions(Stream stream)
		{
			return new GameOptionsSection
			{
				World = (PlanetType)_byteHandler.ReadInt(stream),
				TurnTimer = _byteHandler.ReadInt(stream),
				EndTurn = _byteHandler.ReadInt(stream),
				StartGold = _byteHandler.ReadInt(stream),
				PlayMode = (PlayMode)_byteHandler.ReadInt(stream),
				VictoryType = (VictoryType)_byteHandler.ReadInt(stream),
				VictoryLimit = _byteHandler.ReadInt(stream),
				OpponentType = (OpponentType)_byteHandler.ReadInt(stream),
				RawResource = (ResourceLevelType)_byteHandler.ReadInt(stream),
				FuelResource = (ResourceLevelType)_byteHandler.ReadInt(stream),
				GoldResource = (ResourceLevelType)_byteHandler.ReadInt(stream),
				AlienDerelicts = (AlienDerelictsType)_byteHandler.ReadInt(stream)
			};
		}

		private void WriteGameOptions(Stream stream, GameOptionsSection gameOptions)
		{
			_byteHandler.WriteInt(stream, (int)gameOptions.World);
			_byteHandler.WriteInt(stream, gameOptions.TurnTimer);
			_byteHandler.WriteInt(stream, gameOptions.EndTurn);
			_byteHandler.WriteInt(stream, gameOptions.StartGold);
			_byteHandler.WriteInt(stream, (int)gameOptions.PlayMode);
			_byteHandler.WriteInt(stream, (int)gameOptions.VictoryType);
			_byteHandler.WriteInt(stream, gameOptions.VictoryLimit);
			_byteHandler.WriteInt(stream, (int)gameOptions.OpponentType);
			_byteHandler.WriteInt(stream, (int)gameOptions.RawResource);
			_byteHandler.WriteInt(stream, (int)gameOptions.FuelResource);
			_byteHandler.WriteInt(stream, (int)gameOptions.GoldResource);
			_byteHandler.WriteInt(stream, (int)gameOptions.AlienDerelicts);
		}

		private GameFileHeader LoadGameFileHeader(Stream stream)
		{
			var header = new GameFileHeader
			{
				Version = (FileFormatVersion)_byteHandler.ReadShort(stream),
				SaveFileType = (SaveFileType)_byteHandler.ReadByte(stream),
				SaveGameName = _byteHandler.ReadCharArray(stream, 30).Trim('\0'),
				PlanetType = (PlanetType)_byteHandler.ReadByte(stream),
				MissionIndex = _byteHandler.ReadShort(stream)
			};

			for (var i = 1; i <= 4; i++)
			{
				header.TeamNames.Add(_byteHandler.ReadCharArray(stream, 30).Trim('\0'));
			}

			for (var i = 1; i <= 5; i++)
			{
				header.TeamTypes.Add((TeamType)_byteHandler.ReadByte(stream));
			}

			for (var i = 1; i <= 5; i++)
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

		private void WriteGameFileHeader(Stream stream, GameFileHeader header)
		{
			_byteHandler.WriteShort(stream, (short)header.Version);
			_byteHandler.WriteByte(stream, (byte)header.SaveFileType);
			_byteHandler.WriteCharArray(stream, header.SaveGameName, 30);
			_byteHandler.WriteByte(stream, (byte)header.PlanetType);
			_byteHandler.WriteShort(stream, header.MissionIndex);

			for (var i = 1; i <= 4; i++)
			{
				_byteHandler.WriteCharArray(stream, header.TeamNames[i - 1], 30);
			}

			for (var i = 1; i <= 5; i++)
			{
				_byteHandler.WriteByte(stream, (byte)header.TeamTypes[i - 1]);
			}

			for (var i = 1; i <= 5; i++)
			{
				_byteHandler.WriteByte(stream, (byte)header.TeamClans[i - 1]);
			}

			_byteHandler.WriteUInt32(stream, header.RngSeed);
			_byteHandler.WriteByte(stream, (byte)header.OpponentType);
			_byteHandler.WriteShort(stream, header.TurnTimer);
			_byteHandler.WriteShort(stream, header.EndTurn);
			_byteHandler.WriteByte(stream, (byte)header.PlayMode);
		}

		private static void ValidateInput(SaveFileType saveFileType, string fileName)
		{
			if (saveFileType != SaveFileType.SinglePlayerCustomGame)
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}

			ValidateFileName(fileName);
		}

		private static void ValidateFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || !fileName.ToLower().EndsWith(".dta"))
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}
		}
	}
}
