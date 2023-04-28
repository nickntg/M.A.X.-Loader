using System;
using System.IO;
using MAXLoader.Core.Services;
using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Enums;
using Xunit;

namespace MAXLoader.Core.Tests.Services
{
	public class GameLoaderTests
	{
		private const string GameFileLocation        = "../../../../../data/save1.dta";
		private const string GameWrittenFileLocation = "../../../../../data/save1.rewritten.dta";

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
			var game = LoadGameFile();

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

		[Fact]
		public void ParseOptions()
		{
			var game = LoadGameFile();

			Assert.Equal(PlanetType.FlashPoint, game.Options.World);
			Assert.Equal(0,game.Options.TurnTimer);
			Assert.Equal(0, game.Options.EndTurn);
			Assert.Equal(0, game.Options.StartGold);
			Assert.Equal(PlayMode.TurnBased, game.Options.PlayMode);
			Assert.Equal(VictoryType.Duration, game.Options.VictoryType);
			Assert.Equal(801, game.Options.VictoryLimit);
			Assert.Equal(OpponentType.God, game.Options.OpponentType);
			Assert.Equal(ResourceLevelType.Rich, game.Options.RawResource);
			Assert.Equal(ResourceLevelType.Rich, game.Options.FuelResource);
			Assert.Equal(ResourceLevelType.Rich, game.Options.GoldResource);
			Assert.Equal(AlienDerelictsType.None, game.Options.AlienDerelicts);
		}

		[Fact]
		public void ParseSurface()
		{
			var game = LoadGameFile();

			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[0, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[1, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[2, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[3, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[4, 0]);
			Assert.Equal(SurfaceType.Air, game.Surface.Surfaces[5, 0]);
			Assert.Equal(SurfaceType.Air, game.Surface.Surfaces[6, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[7, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[8, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[9, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[10, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[11, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[12, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[13, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[14, 0]);
			Assert.Equal(SurfaceType.Land, game.Surface.Surfaces[15, 0]);
			Assert.Equal(SurfaceType.Coast, game.Surface.Surfaces[1, 54]);
			Assert.Equal(SurfaceType.Water, game.Surface.Surfaces[1, 55]);
		}

		[Fact]
		public void ParseSurfaceResources()
		{
			var game = LoadGameFile();

			Assert.Equal(0, game.GameResources.Resources[27, 94].Amount);
			Assert.Equal(ResourceType.None, game.GameResources.Resources[27, 94].ResourceType);
			Assert.True(game.GameResources.Resources[27, 94].RedTeamVisible);
			Assert.False(game.GameResources.Resources[27, 94].BlueTeamVisible);
			Assert.False(game.GameResources.Resources[27, 94].GreyTeamVisible);
			Assert.False(game.GameResources.Resources[27, 94].GreenTeamVisible);

			Assert.Equal(4, game.GameResources.Resources[27, 95].Amount);
			Assert.Equal(ResourceType.Fuel, game.GameResources.Resources[27, 95].ResourceType);
			Assert.True(game.GameResources.Resources[27, 95].RedTeamVisible);
			Assert.False(game.GameResources.Resources[27, 95].BlueTeamVisible);
			Assert.False(game.GameResources.Resources[27, 95].GreyTeamVisible);
			Assert.False(game.GameResources.Resources[27, 95].GreenTeamVisible);

			Assert.Equal(14, game.GameResources.Resources[26, 96].Amount);
			Assert.Equal(ResourceType.Raw, game.GameResources.Resources[26, 96].ResourceType);
			Assert.True(game.GameResources.Resources[26, 96].RedTeamVisible);
			Assert.False(game.GameResources.Resources[26, 96].BlueTeamVisible);
			Assert.False(game.GameResources.Resources[26, 96].GreyTeamVisible);
			Assert.False(game.GameResources.Resources[26, 96].GreenTeamVisible);

			Assert.True(game.GameResources.Resources[99, 1].GreyTeamVisible);
		}

		[Fact]
		public void ParseTeamInfos()
		{
			var game = LoadGameFile();

			Assert.Equal(630, game.TeamInfos[Team.Red].StatsGoldSpentOnUpgrades);
			Assert.Equal(492, game.TeamInfos[Team.Green].StatsGoldSpentOnUpgrades);
			Assert.Equal(2564, game.TeamInfos[Team.Blue].StatsGoldSpentOnUpgrades);
			Assert.Equal(1294, game.TeamInfos[Team.Gray].StatsGoldSpentOnUpgrades);
		}

		[Fact]
		public void WriteSameAsRead()
		{
			var loader = new GameLoader(new ByteHandler());

			loader.SaveGameFile(loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, GameFileLocation),
				GameWrittenFileLocation);

			var bSource = File.ReadAllBytes(GameFileLocation);
			var bWritten = File.ReadAllBytes(GameWrittenFileLocation);

			Assert.Equal(bSource.Length, bWritten.Length);

			for (var i = 0; i < bSource.Length; i++)
			{
				if (bWritten[i] != bSource[i])
				{
					if (bSource[i] == 0x80 && bWritten[i] != 0)
					{
						Assert.Fail("Files different");
					}
				}
			}
		}

		private static GameFile LoadGameFile()
		{
			var loader = new GameLoader(new ByteHandler());

			return loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, GameFileLocation);
		}
	}
}
