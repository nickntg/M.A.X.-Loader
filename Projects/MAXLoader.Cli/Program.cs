using System;
using System.Linq;
using CommandLine;
using MAXLoader.Core.Services;
using MAXLoader.Core.Services.Interfaces;
using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Enums;
using Newtonsoft.Json;
using NLog;

namespace MAXLoader.Cli
{
	internal class Program
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			var results = Parser.Default.ParseArguments<CommandLineOptions>(args);

			ValidateParameters(results);

			var loader = new GameLoader(new ByteHandler());
			
			var game = ReadGameFile(loader, results.Value);
			
			var changed = PerformChanges(game, results.Value);

			if (changed)
			{
				SaveChanges(game, loader, results.Value);
			}

			ShowJson(game, results.Value);
		}

		private static GameFile ReadGameFile(IGameLoader loader, CommandLineOptions options)
		{
			GameFile game = null;

			try
			{
				game = loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, options.GameFile);
			}
			catch (Exception ex)
			{
				Log.Error($"Error reading game file: {ex.Message}");
				
				Environment.Exit(1);
			}

			return game;
		}

		private static void ShowJson(GameFile game, CommandLineOptions options)
		{
			if (options.Json)
			{
				Console.WriteLine(JsonConvert.SerializeObject(game, Formatting.Indented,
					new Newtonsoft.Json.Converters.StringEnumConverter()));
			}
		}

		private static void SaveChanges(GameFile game, 
			IGameLoader loader,
			CommandLineOptions options)
		{
			var outputFile = string.IsNullOrEmpty(options.GameOutputFile)
				? options.GameOutputFile
				: options.GameFile;

			try
			{
				loader.SaveGameFile(game, outputFile);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"Could not save game file to {outputFile}");
			}
		}

		private static bool PerformChanges(GameFile game, CommandLineOptions options)
		{
			var changed = false;

			if (options.Gold)
			{
				Log.Info("Giving 30k gold to the Red team");

				game.TeamUnits[Team.Red].Gold = 30000;

				changed = true;
			}

			if (options.Research)
			{
				Log.Info("Setting all topics to 90% researched with 1 round left to go to 100%");

				for (var i = ResearchTopic.Attack; i <= ResearchTopic.Cost; i++)
				{
					game.TeamInfos[Team.Red].ResearchTopics[i].ResearchLevel = 9;
					game.TeamInfos[Team.Red].ResearchTopics[i].TurnsToComplete = 1;
				}

				changed = true;
			}

			return changed;
		}

		private static void ValidateParameters(ParserResult<CommandLineOptions> results)
		{
			if (results.Errors != null && results.Errors.Any())
			{
				foreach (var error in results.Errors)
				{
					if (error.Tag == ErrorType.MissingRequiredOptionError)
					{
						Log.Error("Missing required options");
						Environment.Exit(1);
					}
				}
			}
		}

		private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			Log.Error($"Global exception handler caught unexpected error: {e.ExceptionObject}");

			Environment.Exit(1);
		}
	}
}