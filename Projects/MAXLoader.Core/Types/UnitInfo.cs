using MAXLoader.Core.Types.Enums;
using System.Collections.Generic;

namespace MAXLoader.Core.Types
{
	public class UnitInfo
	{
		public ushort ObjectIndex { get; set; }
		public ClassType ClassType { get; set; }
		public UnitType UnitType { get; set; }
		public ushort HashId { get; set; }
		public uint Flags { get; set; }
		public Point PixelPosition { get; set; }
		public Point GridPosition { get; set; }
		public ushort NameLength { get; set; }
		public string Name { get; set; }
		public Point ShadowOffset { get; set; }
		public Team TeamIndex { get; set; }
		public byte NameIndex { get; set; }
		public byte Brightness { get; set; }
		public byte Angle { get; set; }
		public byte[] VisibleToTeam { get; set; } = new byte[5];
		public byte[] SpottedByTeam { get; set; } = new byte[5];
		public byte MaxVelocity { get; set; }
		public byte Velocity { get; set; }
		public byte Sound { get; set; }
		public byte ScalerAdjust { get; set; }
		public Rect SpriteBounds { get; set; }
		public Rect ShadowBounds { get; set; }
		public byte TurretAngle { get; set; }
		public byte TurretOffsetX { get; set; }
		public byte TurretOffsetY { get; set; }
		public ushort TotalImages { get; set; }
		public ushort ImageBase { get; set; }
		public ushort TurretImageBase { get; set; }
		public ushort FiringImageBase { get; set; }
		public ushort ConnectorImageBase { get; set; }
		public ushort ImageIndex { get; set; }
		public ushort TurretImageIndex { get; set; }
		public ushort ImageIndexMax { get; set; }
		public OrderType Orders { get; set; }
		public byte State { get; set; }
		public OrderType PriorOrders { get; set; }
		public byte PriorState { get; set; }
		public byte LayingState { get; set; }
		public Point TargetGrid { get; set; }
		public byte BuildTime { get; set; }
		public byte TotalMining { get; set; }
		public byte RawMining { get; set; }
		public byte FuelMining { get; set; }
		public byte GoldMining { get; set; }
		public byte RawMiningMax { get; set; }
		public byte GoldMiningMax { get; set; }
		public byte FuelMiningMax { get; set; }
		public byte Hits { get; set; }
		public byte Speed { get; set; }
		public byte Shots { get; set; }
		public byte MoveAndFire { get; set; }
		public ushort Storage { get; set; }
		public byte Ammo { get; set; }
		public byte TargetingMode { get; set; }
		public byte EnterMode { get; set; }
		public byte Cursor { get; set; }
		public byte RecoilDelay { get; set; }
		public byte DelayedReaction { get; set; }
		public byte DamagedThisTurn { get; set; }
		public byte ResearchTopic { get; set; }
		public byte Moved { get; set; }
		public byte Bobbed { get; set; }
		public byte ShakeEffectState { get; set; }
		public byte Engine { get; set; }
		public byte Weapon { get; set; }
		public byte Comm { get; set; }
		public byte FuelDistance { get; set; }
		public byte MoveFraction { get; set; }
		public byte Energized { get; set; }
		public byte RepeatBuild { get; set; }
		public ushort BuildRate { get; set; }
		public byte DisabledReactionFire { get; set; }
		public byte AutoSurvey { get; set; }
		public uint Field221 { get; set; }
		public Path Path { get; set; }
		public ushort Connectors { get; set; }
		public UnitValue UnitValue { get; set; }
		public Complex Complex { get; set; }
		public UnitInfo ParentUnit { get; set; }
		public UnitInfo EnemyUnit { get; set; }
		public UnitTypeArray UnitTypeArray { get; set; }
		public bool IsEmpty { get; set; } = false;

		public bool RequiresSlab    => (Flags & 0b00000000000000000000000000000001) > 0;
		public bool TurretSprite    => (Flags & 0b00000000000000000000000000000010) > 0;
		public bool SentryUnit      => (Flags & 0b00000000000000000000000000000100) > 0;
		public bool SpinningTurret  => (Flags & 0b00000000000000000000000000001000) > 0;
		public bool Hovering        => (Flags & 0b00000000000000000000000100000000) > 0;
		public bool HasFiringSprite => (Flags & 0b00000000000000000000001000000000) > 0;
		public bool FiresMissiles   => (Flags & 0b00000000000000000000010000000000) > 0;
		public bool ConstructorUnit => (Flags & 0b00000000000000000000100000000000) > 0;
		public bool ElectronicUnit  => (Flags & 0b00000000000000000010000000000000) > 0;
		public bool Selectable      => (Flags & 0b00000000000000000100000000000000) > 0;
		public bool StandAlone      => (Flags & 0b00000000000000001000000000000000) > 0;
		public bool MobileLandUnit  => (Flags & 0b00000000000000010000000000000000) > 0;
		public bool Stationary      => (Flags & 0b00000000000000100000000000000000) > 0;
		public bool Upgradeable     => (Flags & 0b00000000010000000000000000000000) > 0;
		public bool GroundCover     => (Flags & 0b00000000100000000000000000000000) > 0;
		public bool Exploding       => (Flags & 0b00000010000000000000000000000000) > 0;
		public bool Animated        => (Flags & 0b00000100000000000000000000000000) > 0;
		public bool ConnectorUnit   => (Flags & 0b00001000000000000000000000000000) > 0;
		public bool Building        => (Flags & 0b00010000000000000000000000000000) > 0;
		public bool MissileUnit     => (Flags & 0b00100000000000000000000000000000) > 0;
		public bool MobileAirUnit   => (Flags & 0b01000000000000000000000000000000) > 0;
		public bool MobileSeaUnit   => (Flags & 0b10000000000000000000000000000000) > 0;
	}
}
