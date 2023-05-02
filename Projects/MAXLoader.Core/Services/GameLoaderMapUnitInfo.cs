using System.IO;
using MAXLoader.Core.Types;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		public void WriteUnitInfoHashMap(Stream stream, UnitInfoHashMap hashMap)
		{
			WriteUShort(stream, hashMap.HashSize);

			for (var i = 1; i <= hashMap.HashSize; i++)
			{
				WriteUShort(stream, hashMap.Hashes[i-1].UnitInfoCount);
				
				for (var j = 1; j <= hashMap.Hashes[i - 1].UnitInfoCount; j++)
				{
					WriteUShort(stream, hashMap.Hashes[i - 1].ObjectIndexes[j-1]);
				}
			}
		}

		public UnitInfoHashMap LoadUnitInfoHashMap(Stream stream)
		{
			Log.Debug($"LoadUnitInfoHashMap: start, stream position {stream.Position:X}");

			var hashMap = new UnitInfoHashMap
			{
				HashSize = ReadUShort(stream)
			};

			for (var i = 1; i <= hashMap.HashSize; i++)
			{
				var uih = new UnitInfoHash
				{
					UnitInfoCount = ReadUShort(stream)
				};
				for (var j = 1; j <= uih.UnitInfoCount; j++)
				{
					uih.ObjectIndexes.Add(ReadUShort(stream));
				}

				hashMap.Hashes.Add(uih);
			}

			Log.Debug($"LoadUnitInfoHashMap: end, stream position {stream.Position:X}");

			return hashMap;
		}
	}
}
