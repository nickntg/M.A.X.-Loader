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
			}

			return gameFile;
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

		private GameSurfaceResourcesMap LoadResources(Stream stream)
		{
			var resources = new GameSurfaceResourcesMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					var word = _byteHandler.ReadWord(stream);
					var resource = new CellResource
					{
						Amount = word & 0x1f,
						ResourceType = (ResourceType)((word & 0xe0) >> 5),
						RedTeamVisible = (word & 0x2000) != 0,
						GreenTeamVisible = (word & 0x1000) != 0,
						BlueTeamVisible = (word & 0x800) != 0,
						GreyTeamVisible = (word & 0x800) != 0
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

		private GameSurfaceMap LoadSurface(Stream stream)
		{
			var surface = new GameSurfaceMap();
			for (var y = 1; y <= Globals.MaxMapWidth; y++)
			{
				for (var x = 1; x <= Globals.MaxMapHeight; x++)
				{
					surface.Surfaces[x-1, y-1] = (SurfaceType)_byteHandler.ReadByte(stream);
				}
			}

			return surface;
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

		private static void ValidateInput(SaveFileType saveFileType, string fileName)
		{
			if (saveFileType != SaveFileType.SinglePlayerCustomGame)
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}

			if (string.IsNullOrEmpty(fileName) || !fileName.ToLower().EndsWith(".dta"))
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}
		}
	}
}
