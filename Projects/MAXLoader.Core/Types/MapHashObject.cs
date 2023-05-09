using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class MapHashObject
	{
		public Point Coordinates { get; set; }
		public ushort UnitInfoCount { get; set; }
		public List<ushort> ObjectIndex { get; set; } = new();
	}
}
