using System.IO;
using MAXLoader.Core.Services.Interfaces;
using MAXLoader.Core.Types;
using MAXLoader.Core.Types.Enums;
using NLog;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader : IGameLoader
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IByteHandler _byteHandler;
		private          int          _lastIndex;

		public GameLoader(IByteHandler byteHandler)
		{
			_byteHandler = byteHandler;
		}

		public GameFile LoadGameFile(SaveFileType saveFileType, string fileName)
		{
			_lastIndex = 0;
			ValidateInput(saveFileType, fileName);

			var gameFile = new GameFile();

			using (var sr = new StreamReader(fileName))
			{
				gameFile.Header = LoadGameFileHeader(sr.BaseStream);
				gameFile.Options = LoadGameOptions(sr.BaseStream);
				gameFile.Surface = LoadSurface(sr.BaseStream);
				gameFile.GameResources = LoadResources(sr.BaseStream);
				gameFile.TeamInfos = LoadTeamInfos(sr.BaseStream);
				gameFile.GameManagerState = LoadGameManagerState(sr.BaseStream);
				gameFile.TeamUnits = LoadTeamUnits(sr.BaseStream);
				Log.Debug("== Ground cover units ==");
				gameFile.GroundCoverUnits = LoadUnitInfoList(sr.BaseStream);
				Log.Debug("== Mobile land sea units ==");
				gameFile.MobileLandSeaUnits = LoadUnitInfoList(sr.BaseStream);
				Log.Debug("== Stationary units ==");
				gameFile.StationaryUnits = LoadUnitInfoList(sr.BaseStream);
				Log.Debug("== Mobile air units ==");
				gameFile.MobileAirUnits = LoadUnitInfoList(sr.BaseStream);
				Log.Debug("== Particles ==");
				gameFile.Particles = LoadUnitInfoList(sr.BaseStream);
				gameFile.MapUnitInfo = LoadUnitInfoHashMap(sr.BaseStream);
				gameFile.TheRest = LoadTheRest(sr.BaseStream);
			}

			return gameFile;
		}

		public void SaveGameFile(GameFile game, string fileName)
		{
			ValidateFileName(fileName);

			using (var sw = new StreamWriter(fileName))
			{
				WriteGameFileHeader(sw.BaseStream, game.Header);
				WriteGameOptions(sw.BaseStream, game.Options);
				WriteSurface(sw.BaseStream, game.Surface);
				WriteResources(sw.BaseStream, game.GameResources);
				WriteTeamInfos(sw.BaseStream, game.TeamInfos);
				WriteGameManagerState(sw.BaseStream, game.GameManagerState);
				WriteTeamUnits(sw.BaseStream, game.TeamUnits);
				WriteUnitInfoList(sw.BaseStream, game.GroundCoverUnits);
				WriteUnitInfoList(sw.BaseStream, game.MobileLandSeaUnits);
				WriteUnitInfoList(sw.BaseStream, game.StationaryUnits);
				WriteUnitInfoList(sw.BaseStream, game.MobileAirUnits);
				WriteUnitInfoList(sw.BaseStream, game.Particles);
				WriteUnitInfoHashMap(sw.BaseStream, game.MapUnitInfo);
				WriteTheRest(sw.BaseStream, game.TheRest);
			}
		}
	}
}
