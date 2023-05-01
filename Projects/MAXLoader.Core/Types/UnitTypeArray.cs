using System.Collections.Generic;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class UnitTypeArray
	{
		public ushort ObjectCount { get; set; }
		public List<UnitType> Array { get; set; } = new();
	}
}
