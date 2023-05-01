using MAXLoader.Core.Types.Constants;
using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.Collections.Generic;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private Dictionary<Team, TeamInfo> LoadTeamInfos(Stream stream)
		{
			Log.Debug($"LoadTeamInfos: start, stream position {stream.Position:X}");

			var tis = new Dictionary<Team, TeamInfo>
			{
				{ Team.Red, LoadTeamInfo(stream) },
				{ Team.Green, LoadTeamInfo(stream) },
				{ Team.Blue, LoadTeamInfo(stream) },
				{ Team.Gray, LoadTeamInfo(stream) }
			};

			Log.Debug($"LoadTeamInfos: end, stream position {stream.Position:X}");

			return tis;
		}

		private TeamInfo LoadTeamInfo(Stream stream)
		{
			Log.Debug($"LoadTeamInfo: start, stream position {stream.Position:X}");

			var ti = new TeamInfo();

			for (var i = 1; i <= Globals.MarkersSize; i++)
			{
				ti.Markers[i - 1] = ReadPoint(stream);
			}

			ti.TeamType = (TeamType)ReadByte(stream);
			ti.Field41 = ReadByte(stream);
			ti.TeamClan = (TeamClan)ReadByte(stream);

			for (var i = ResearchTopic.Attack; i <= ResearchTopic.Cost; i++)
			{
				var researchTopic = new ResearchTopicInfo
				{
					ResearchLevel = ReadUInt32(stream),
					TurnsToComplete = ReadUInt32(stream),
					Allocation = ReadUInt32(stream)
				};
				ti.ResearchTopics.Add(i, researchTopic);
			}

			ti.VictoryPoints = ReadUInt32(stream);
			ti.LastUnitId = ReadUShort(stream);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				ti.UnitCounters.Add(i, ReadByte(stream));
			}

			for (var i = 1; i <= Globals.ScreenLocationSize; i++)
			{
				var point = new ScreenLocation
				{
					X = ReadByte(stream),
					Y = ReadByte(stream)
				};
				ti.ScreenLocations[i - 1] = point;
			}

			for (var i = 1; i <= Globals.ScoreGraphSize; i++)
			{
				ti.ScoreGraph[i - 1] = ReadShort(stream);
			}

			ti.SelectedUnit = ReadUShort(stream);
			ti.ZoomLevel = ReadUShort(stream);
			ti.ScreenPosition = ReadPoint(stream);
			ti.GuiButtonStateRange = ReadByte(stream) != 0;
			ti.GuiButtonStateScan = ReadByte(stream) != 0;
			ti.GuiButtonStateStatus = ReadByte(stream) != 0;
			ti.GuiButtonStateColors = ReadByte(stream) != 0;
			ti.GuiButtonStateHits = ReadByte(stream) != 0;
			ti.GuiButtonStateAmmo = ReadByte(stream) != 0;
			ti.GuiButtonStateMinimap2X = ReadByte(stream) != 0;
			ti.GuiButtonStateMinimapTnt = ReadByte(stream) != 0;
			ti.GuiButtonStateGrid = ReadByte(stream) != 0;
			ti.GuiButtonStateNames = ReadByte(stream) != 0;
			ti.GuiButtonStateSurvey = ReadByte(stream) != 0;
			ti.StatsFactoriesBuilt = ReadShort(stream);
			ti.StatsMinesBuilt = ReadShort(stream);
			ti.StatsBuildingsBuilt = ReadShort(stream);
			ti.StatsUnitsBuilt = ReadShort(stream);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				ti.Casualties.Add(i, ReadUShort(stream));
			}

			ti.StatsGoldSpentOnUpgrades = ReadShort(stream);

			Log.Debug($"LoadTeamInfo: end, stream position {stream.Position:X}");

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
				WritePoint(stream, ti.Markers[i-1]);
			}

			WriteByte(stream, (byte)ti.TeamType);
			WriteByte(stream, ti.Field41);
			WriteByte(stream, (byte)ti.TeamClan);

			for (var i = ResearchTopic.Attack; i <= ResearchTopic.Cost; i++)
			{
				WriteUInt32(stream, ti.ResearchTopics[i].ResearchLevel);
				WriteUInt32(stream, ti.ResearchTopics[i].TurnsToComplete);
				WriteUInt32(stream, ti.ResearchTopics[i].Allocation);
			}

			WriteUInt32(stream, ti.VictoryPoints);
			WriteUShort(stream, ti.LastUnitId);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				WriteByte(stream, ti.UnitCounters[i]);
			}

			for (var i = 1; i <= Globals.ScreenLocationSize; i++)
			{
				WriteByte(stream, ti.ScreenLocations[i - 1].X);
				WriteByte(stream, ti.ScreenLocations[i - 1].Y);
			}

			for (var i = 1; i <= Globals.ScoreGraphSize; i++)
			{
				WriteShort(stream, ti.ScoreGraph[i - 1]);
			}

			WriteUShort(stream, ti.SelectedUnit);
			WriteUShort(stream, ti.ZoomLevel);
			WriteShort(stream, ti.ScreenPosition.X);
			WriteShort(stream, ti.ScreenPosition.Y);
			WriteByte(stream, ti.GuiButtonStateRange ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateScan ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateStatus ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateColors ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateHits ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateAmmo ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateMinimap2X ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateMinimapTnt ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateGrid ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateNames ? (byte)1 : (byte)0);
			WriteByte(stream, ti.GuiButtonStateSurvey ? (byte)1 : (byte)0);
			WriteShort(stream, ti.StatsFactoriesBuilt);
			WriteShort(stream, ti.StatsMinesBuilt);
			WriteShort(stream, ti.StatsBuildingsBuilt);
			WriteShort(stream, ti.StatsUnitsBuilt);

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				WriteUShort(stream, ti.Casualties[i]);
			}

			WriteShort(stream, ti.StatsGoldSpentOnUpgrades);
		}
	}
}
