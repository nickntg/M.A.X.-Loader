using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class GameOptionsSection
	{
		public PlanetType World { get; set; }
		public int TurnTimer { get; set; }
		public int EndTurn { get; set; }
		public int StartGold { get; set; }
		public PlayMode PlayMode { get; set; }
		public VictoryType VictoryType { get; set; }
		public int VictoryLimit { get; set; }
		public OpponentType OpponentType { get; set; }
		public ResourceLevelType RawResource { get; set; }
		public ResourceLevelType FuelResource { get; set; }
		public ResourceLevelType GoldResource { get; set; }
		public AlienDerelictsType AlienDerelicts { get; set; }
	}
}
