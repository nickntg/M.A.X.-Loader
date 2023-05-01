using MAXLoader.Core.Types;
using Xunit;

namespace MAXLoader.Core.Tests.Types
{
	public class UnitInfoTests
	{
		[Fact]
		public void SomeFlags()
		{
			var ui = new UnitInfo { Flags = 0x00405201 };

			Assert.True(ui.RequiresSlab);
			Assert.False(ui.TurretSprite);
			Assert.False(ui.SentryUnit);
			Assert.False(ui.SpinningTurret);
			Assert.False(ui.Hovering);
			Assert.True(ui.HasFiringSprite);
			Assert.False(ui.FiresMissiles);
			Assert.False(ui.ConstructorUnit);
			Assert.False(ui.ElectronicUnit);
			Assert.True(ui.Selectable);
			Assert.False(ui.StandAlone);
			Assert.False(ui.MobileLandUnit);
			Assert.False(ui.Stationary);
			Assert.True(ui.Upgradeable);
			Assert.False(ui.GroundCover);
			Assert.False(ui.Exploding);
			Assert.False(ui.Animated);
			Assert.False(ui.ConnectorUnit);
			Assert.False(ui.Building);
			Assert.False(ui.MissileUnit);
			Assert.False(ui.MobileAirUnit);
			Assert.False(ui.MobileSeaUnit);
		}

		[Fact]
		public void AllFlags()
		{
			var ui = new UnitInfo { Flags = 0xffffffff };

			Assert.True(ui.RequiresSlab);
			Assert.True(ui.TurretSprite);
			Assert.True(ui.SentryUnit);
			Assert.True(ui.SpinningTurret);
			Assert.True(ui.Hovering);
			Assert.True(ui.HasFiringSprite);
			Assert.True(ui.FiresMissiles);
			Assert.True(ui.ConstructorUnit);
			Assert.True(ui.ElectronicUnit);
			Assert.True(ui.Selectable);
			Assert.True(ui.StandAlone);
			Assert.True(ui.MobileLandUnit);
			Assert.True(ui.Stationary);
			Assert.True(ui.Upgradeable);
			Assert.True(ui.GroundCover);
			Assert.True(ui.Exploding);
			Assert.True(ui.Animated);
			Assert.True(ui.ConnectorUnit);
			Assert.True(ui.Building);
			Assert.True(ui.MissileUnit);
			Assert.True(ui.MobileAirUnit);
			Assert.True(ui.MobileSeaUnit);
		}
	}
}
