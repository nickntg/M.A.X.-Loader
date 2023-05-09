using CommandLine;

namespace MAXLoader.Cli
{
	public class CommandLineOptions
	{
		[Option('i', "input", Required = true, HelpText = "M.A.X. game file to read.")]
		public string GameFile { get; set; }
		[Option('o', "output", Required = false, HelpText = "Name of file to save.")]
		public string GameOutputFile { get; set; }
		[Option('g', "gold", Required = false, HelpText = "Give 30k of gold to the Red team.")]
		public bool Gold { get; set; }
		[Option('r', "research", Required = false, HelpText = "Research topics for the Red team.")]
		public bool Research { get; set; }
		[Option('j', "json",Required  = false, HelpText = "Output the save file in JSON format.")]
		public bool Json { get; set; }
	}
}
