using System;
using MAXLoader.Core.Services;
using MAXLoader.Core.Types.Enums;
using Xunit;

namespace MAXLoader.Core.Tests.Services
{
	public class GameLoaderTests
	{
		[Theory]
		[InlineData(SaveFileType.Text)]
		[InlineData(SaveFileType.CampaignGame)]
		[InlineData(SaveFileType.DebugModeGame)]
		[InlineData(SaveFileType.DemoGame)]
		[InlineData(SaveFileType.HotSeatGame)]
		[InlineData(SaveFileType.MultiPlayerGame)]
		[InlineData(SaveFileType.MultiPlayerScenarioGame)]
		[InlineData(SaveFileType.SinglePlayerScenarioGame)]
		[InlineData(SaveFileType.TutorialGame)]
		public void Validate_InvalidSaveFileType(SaveFileType saveFileType)
		{
			var loader = new GameLoader(null);

			Assert.Throws<NotImplementedException>(() => loader.LoadGameFile(saveFileType, null));
		}

		[Fact]
		public void Validate_NoFile()
		{
			var loader = new GameLoader(null);

			Assert.Throws<NotImplementedException>(() => loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, null));
		}

		[Fact]
		public void Validate_InvalidFileExtension()
		{
			var loader = new GameLoader(null);

			Assert.Throws<NotImplementedException>(() => loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, "file.123"));
		}

		[Fact]
		public void ParseHeader()
		{
			var loader = new GameLoader(new ByteHandler());

			var game = loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, "../../../../../data/save1.dta");

			Assert.Equal(FileFormatVersion.V70, game.Header.Version);
			Assert.Equal(SaveFileType.SinglePlayerCustomGame, game.Header.SaveFileType);
			Assert.Equal("rrrr2", game.Header.SaveGameName);
			Assert.Equal(PlanetType.FlashPoint, game.Header.PlanetType);
			Assert.Equal(0, game.Header.MissionIndex);
			Assert.Equal("Player 1", game.Header.TeamNames[0]);
			Assert.Equal("Green Team", game.Header.TeamNames[1]);
			Assert.Equal("Blue Team", game.Header.TeamNames[2]);
			Assert.Equal("Gray Team", game.Header.TeamNames[3]);
			Assert.Equal(TeamType.Human, game.Header.TeamTypes[0]);
			Assert.Equal(TeamType.Computer, game.Header.TeamTypes[1]);
			Assert.Equal(TeamType.Computer, game.Header.TeamTypes[2]);
			Assert.Equal(TeamType.Computer, game.Header.TeamTypes[3]);
			Assert.Equal(TeamType.None, game.Header.TeamTypes[4]);
			Assert.Equal(TeamClan.TheChosen, game.Header.TeamClans[0]);
			Assert.Equal(TeamClan.SacredEights, game.Header.TeamClans[1]);
			Assert.Equal(TeamClan.Musashi, game.Header.TeamClans[2]);
			Assert.Equal(TeamClan.SacredEights, game.Header.TeamClans[3]);
			Assert.Equal(TeamClan.None, game.Header.TeamClans[4]);
			Assert.Equal(0x63a41df5L, game.Header.RngSeed);
			Assert.Equal(0, game.Header.TurnTimer);
			Assert.Equal(0, game.Header.EndTurn);
			Assert.Equal(OpponentType.God, game.Header.OpponentType);
		}
	}
}
