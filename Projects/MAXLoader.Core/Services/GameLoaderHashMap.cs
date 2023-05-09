using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		public void WriteHashMap(Stream stream, HashMap map)
		{
			WriteUShort(stream, map.HashSize);
			WriteShort(stream, map.XShift);

			for (var i = 1; i <= map.HashSize; i++)
			{
				var hos = map.Map[i - 1];
				WriteUShort(stream, hos.MapHashCount);

				for (var j = 1; j <= hos.MapHashCount; j++)
				{
					var ho = hos.Objects[j - 1];
					WritePoint(stream, ho.Coordinates);
					WriteUShort(stream, ho.UnitInfoCount);

					for (var z = 1; z <= ho.UnitInfoCount; z++)
					{
						WriteUShort(stream, ho.ObjectIndex[z - 1]);
					}
				}
			}
		}

		public HashMap LoadHashMap(Stream stream)
		{
			var hm = new HashMap
			{
				HashSize = ReadUShort(stream),
				XShift = ReadShort(stream)
			};

			for (var i = 1; i <= hm.HashSize; i++)
			{
				var hos = new MapHashObjects
				{
					MapHashCount = ReadUShort(stream)
				};

				for (var j = 1; j <= hos.MapHashCount; j++)
				{
					var ho = new MapHashObject
					{
						Coordinates = ReadPoint(stream),
						UnitInfoCount = ReadUShort(stream)
					};

					for (var z = 1; z <= ho.UnitInfoCount; z++)
					{
						ho.ObjectIndex.Add(ReadUShort(stream));
					}

					hos.Objects.Add(ho);
				}

				hm.Map.Add(hos);
			}

			return hm;
		}

	}
}
