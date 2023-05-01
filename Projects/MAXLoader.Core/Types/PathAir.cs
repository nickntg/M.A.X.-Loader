namespace MAXLoader.Core.Types
{
	public class PathAir : PathClass
	{
		public short Length { get; set; }
		public byte Angle { get; set; }
		public Point PixelStart { get; set; }
		public Point PixelEnd { get; set; }
		public int XStep { get; set; }
		public int YStep { get; set; }
		public int DeltaX { get; set; }
		public int DeltaY { get; set; }
	}
}