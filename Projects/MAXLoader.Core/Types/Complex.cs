namespace MAXLoader.Core.Types
{
	public class Complex
	{
		public ushort ObjectIndex { get; set; }
		public ushort ClassType { get; set; }
		public short Material { get; set; }
		public short Fuel { get; set; }
		public short Gold { get; set; }
		public short Power { get; set; }
		public short Workers { get; set; }
		public short Buildings { get; set; }
		public short Id { get; set; }
		public bool IsEmpty { get; set; } = false;
	}
}
