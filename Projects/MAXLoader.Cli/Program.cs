using System;
using System.Linq;
using CommandLine;
using MAXLoader.Core.Services;
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

			if (results.Errors != null && results.Errors.Any())
			{
				foreach (var error in results.Errors)
				{
					if (error.Tag == ErrorType.MissingRequiredOptionError)
					{
						Log.Error("Missing required options");
						return;
					}
				}
			}

			var loader = new GameLoader(new ByteHandler());
			GameFile game;

			try
			{
				game = loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, results.Value.GameFile);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error reading game file: {ex.Message}");
				return;
			}

			Console.WriteLine(JsonConvert.SerializeObject(game, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter()));
		}

		private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			Log.Error($"Global exception handler caught unexpected error: {e.ExceptionObject}");

			Environment.Exit(1);
		}
	}
}