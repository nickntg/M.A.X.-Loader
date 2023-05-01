using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class PathGround : PathClass
	{
		public Point PixelEnd { get; set; }
		public short Index { get; set; }
		public short StepsCount { get; set; }
		public List<PathStep> Steps { get; set; } = new();
	}
}