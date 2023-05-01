using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class UnitInfoList
	{
		public ushort UnitInfoCount { get; set; }
		public List<UnitInfo> Units { get; set; } = new();
	}
}
