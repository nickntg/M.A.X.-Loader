using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class HashMap
	{
		public ushort HashSize { get; set; }
		public short XShift { get; set; }
		public List<MapHashObjects> Map { get; set; } = new();
	}
}
