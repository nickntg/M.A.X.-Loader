namespace MAXLoader.Core.Types
{
	public class UnitValue
	{
		public ushort ObjectIndex { get; set; }
		public ushort ClassType { get; set; }
		public ushort Turns { get; set; }
		public ushort Hits { get; set; }
		public ushort Armor { get; set; }
		public ushort Attack { get; set; }
		public ushort Speed { get; set; }
		public ushort Range { get; set; }
		public ushort Rounds { get; set; }
		public byte MoveAndFire { get; set; }
		public ushort Scan { get; set; }
		public ushort Storage { get; set; }
		public ushort Ammo { get; set; }
		public ushort AttackRadius { get; set; }
		public ushort AgentAdjust { get; set; }
		public ushort Version { get; set; }
		public byte UnitsBuilt { get; set; }
	}
}
