using System.Collections.Generic;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class GameFile
	{
		public GameFileHeader Header { get; set; }
		public GameOptionsSection Options { get; set; }
		public GameSurfaceMap Surface { get; set; }
		public GameSurfaceResourcesMap GameResources { get; set; }
		public Dictionary<Team, TeamInfo> TeamInfos { get; set; }
		public GameManagerState GameManagerState { get; set; }
		public Dictionary<Team, TeamUnits> TeamUnits { get; set; }
		public UnitInfoList GroundCoverUnits { get; set; }
		public UnitInfoList MobileLandSeaUnits { get; set; }
		public UnitInfoList StationaryUnits { get; set; }
		public UnitInfoList MobileAirUnits { get; set; }
		public UnitInfoList Particles { get; set; }
		public UnitInfoHashMap MapUnitInfo { get; set; }
		public HashMap HashMap { get; set; }
		public TheRest TheRest { get; set; }
	}
}