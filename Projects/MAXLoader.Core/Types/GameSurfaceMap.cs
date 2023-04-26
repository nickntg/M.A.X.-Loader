using MAXLoader.Core.Types.Constants;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class GameSurfaceMap
	{
		public SurfaceType[,] Surfaces { get; set; } = new SurfaceType[Globals.MaxMapWidth,Globals.MaxMapHeight];
	}
}
