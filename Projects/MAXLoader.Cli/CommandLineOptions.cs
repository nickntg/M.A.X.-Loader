using CommandLine;

namespace MAXLoader.Cli
{
	public class CommandLineOptions
	{
		[Option('i', "input", Required = true, HelpText = "M.A.X. game file to read")]
		public string GameFile { get; set; }
		[Option('o', "output", Required = false, HelpText = "Name of file to save")]
		public string GameOutputFile { get; set; }
	}
}
