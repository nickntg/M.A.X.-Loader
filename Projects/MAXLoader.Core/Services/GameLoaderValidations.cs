using MAXLoader.Core.Types.Enums;
using System;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private static void ValidateInput(SaveFileType saveFileType, string fileName)
		{
			if (saveFileType != SaveFileType.SinglePlayerCustomGame)
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}

			ValidateFileName(fileName);
		}

		private static void ValidateFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || !fileName.ToLower().EndsWith(".dta"))
			{
				throw new NotImplementedException("Currently only single player custom games can be loaded");
			}
		}
	}
}
