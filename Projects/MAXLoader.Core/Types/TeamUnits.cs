using System.Collections.Generic;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class TeamUnits
	{
		public short Gold { get; set; }
		public Dictionary<ushort, UnitValue> BaseUnitValues { get; set; } = new();
		public Dictionary<ushort, UnitValue> CurrentUnitValues { get; set; } = new();
		public ushort ComplexCount { get; set; }
		public List<Complex> Complexes { get; set; } = new();
	}
}
