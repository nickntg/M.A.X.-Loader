using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class UnitInfoHash
	{
		public ushort UnitInfoCount { get; set; }
		public List<ushort> ObjectIndexes { get; set; } = new();
	}
}
