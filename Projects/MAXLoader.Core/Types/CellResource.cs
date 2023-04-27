using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
	public class CellResource
	{
		public int Amount { get; set; }
		public ResourceType ResourceType { get; set; }
		public bool RedTeamVisible { get; set; }
		public bool GreenTeamVisible { get; set; }
		public bool BlueTeamVisible { get; set; }
		public bool GreyTeamVisible { get; set; }
	}
}
