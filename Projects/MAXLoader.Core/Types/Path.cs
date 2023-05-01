using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class Path
	{
		public ushort ObjectIndex { get; set; }
		public ClassType ClassType { get; set; }
		public PathClass PathClass { get; set; }
		public bool IsEmpty { get; set; } = false;
	}
}
