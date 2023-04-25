using System.Collections.Generic;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class GameFileHeader
	{
		public FileFormatVersion Version { get; set; }
		public SaveFileType SaveFileType { get; set; }
		public string SaveGameName { get; set; }
		public PlanetType PlanetType { get; set; }
		public short MissionIndex { get; set; }
		public List<string> TeamNames { get; set; } = new();
		public List<TeamType> TeamTypes { get; set; } = new();
		public List<TeamClan> TeamClans { get; set; } = new();
		public uint RngSeed { get; set; }
		public OpponentType OpponentType { get; set; }
		public short TurnTimer { get; set; }
		public short EndTurn { get; set; }
		public PlayMode PlayMode { get; set; }
	}
}