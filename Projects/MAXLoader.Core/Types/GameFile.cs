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
		public TheRest TheRest { get; set; }
	}
}