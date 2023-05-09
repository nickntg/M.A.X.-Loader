using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class MapHashObjects
	{
		public ushort MapHashCount { get; set; }
		public List<MapHashObject> Objects { get; set; } = new();
	}
}