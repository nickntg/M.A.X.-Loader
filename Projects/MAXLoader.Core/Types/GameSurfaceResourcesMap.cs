using MAXLoader.Core.Types.Constants;

namespace MAXLoader.Core.Types
{
	public class GameSurfaceResourcesMap
	{
		public CellResource[,] Resources { get; set; } = new CellResource[Globals.MaxMapWidth, Globals.MaxMapHeight];
	}
}
