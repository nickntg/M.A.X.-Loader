namespace MAXLoader.Core.Types
{
	public class GameFile
	{
		public GameFileHeader Header { get; set; }
		public GameOptionsSection Options { get; set; }
		public GameSurfaceMap Surface { get; set; }
	}
}