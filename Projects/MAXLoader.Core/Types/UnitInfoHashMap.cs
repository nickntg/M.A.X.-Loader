using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class UnitInfoHashMap
	{
		public ushort HashSize { get; set; }
		public List<UnitInfoHash> Hashes { get; set; } = new();
	}
}
