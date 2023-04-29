using MAXLoader.Core.Types.Constants;

namespace MAXLoader.Core.Types
{
	public class GameSurfaceMap
	{
		public GameSurfaceCell[,] Surfaces { get; set; } = new GameSurfaceCell[Globals.MaxMapWidth,Globals.MaxMapHeight];
	}
}
