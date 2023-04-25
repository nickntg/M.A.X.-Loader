using MAXLoader.Core.Services;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Scratch
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var loader = new GameLoader(new ByteHandler());
			loader.LoadGameFile(SaveFileType.SinglePlayerCustomGame, "../../../../../data/save1.dta");
		}
	}

}