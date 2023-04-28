using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Services.Interfaces
{
	public interface IGameLoader
	{
		GameFile LoadGameFile(SaveFileType saveFileType, string fileName);
		void SaveGameFile(GameFile game, string fileName);
	}
}
