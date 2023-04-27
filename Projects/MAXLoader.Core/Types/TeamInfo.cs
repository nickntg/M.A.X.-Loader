using System.Collections.Generic;
using MAXLoader.Core.Types.Constants;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class TeamInfo
	{
		public Point[] Markers { get; set; } = new Point[Globals.MarkersSize];
		public TeamType TeamType { get; set; }
		public byte Field41 { get; set; }
		public TeamClan TeamClan { get; set; }
		public Dictionary<ResearchTopic, ResearchTopicInfo> ResearchTopics { get; set; } = new();
		public uint VictoryPoints { get; set; }
		public ushort LastUnitId { get; set; }
		public Dictionary<UnitType, int> UnitCounters { get; set; } = new();
		public ScreenLocation[] ScreenLocations { get; set; } = new ScreenLocation[Globals.ScreenLocationSize];
		public short[] ScoreGraph { get; set; } = new short[Globals.ScoreGraphSize];
		public uint SelectedUnit { get; set; }
		public ushort ZoomLevel { get; set; }
		public Point ScreenPosition { get; set; }
		public bool GuiButtonStateRange { get; set; }
		public bool GuiButtonStateScan { get; set; }
		public bool GuiButtonStateStatus { get; set; }
		public bool GuiButtonStateColors { get; set; }
		public bool GuiButtonStateHits { get; set; }
		public bool GuiButtonStateAmmo { get; set; }
		public bool GuiButtonStateMinimap2X { get; set; }
		public bool GuiButtonStateMinimapTnt { get; set; }
		public bool GuiButtonStateGrid { get; set; }
		public bool GuiButtonStateNames { get; set; }
		public bool GuiButtonStateSurvey { get; set; }
		public short StatsFactoriesBuilt { get; set; }
		public short StatsMinesBuilt { get; set; }
		public short StatsBuildingsBuilt { get; set; }
		public short StatsUnitsBuilt { get; set; }
		public Dictionary<UnitType, ushort> Casualties { get; set; } = new();
		public short StatsGoldSpentOnUpgrades { get; set; }
	}
}
