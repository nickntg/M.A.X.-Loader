using System.Collections.Generic;
using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System.IO;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private void WriteTeamUnits(Stream stream, Dictionary<Team, TeamUnits> tus)
		{
			for (var t = Team.Red; t <= Team.Gray; t++)
			{
				var tu = tus[t];

				if (tu == null)
				{
					continue;
				}

				WriteShort(stream, tu.Gold);
				WriteUnitValueDictionary(stream, tu.BaseUnitValues);
				WriteUnitValueDictionary(stream, tu.CurrentUnitValues);
				WriteUShort(stream, tu.ComplexCount);

				for (var i = 1; i <= tu.ComplexCount; i++)
				{
					WriteComplex(stream, tu.Complexes[i - 1]);
				}
			}
		}

		private void WriteUnitValueDictionary(Stream stream, Dictionary<ushort, UnitValue> dict)
		{
			foreach (var kvp in dict)
			{
				WriteUShort(stream, kvp.Key);
				if (kvp.Value.IsEmpty)
				{
					continue;
				}

				WriteUShort(stream, kvp.Value.ClassType);
				WriteUShort(stream, kvp.Value.Turns);
				WriteUShort(stream, kvp.Value.Hits);
				WriteUShort(stream, kvp.Value.Armor);
				WriteUShort(stream, kvp.Value.Attack);
				WriteUShort(stream, kvp.Value.Speed);
				WriteUShort(stream, kvp.Value.Range);
				WriteUShort(stream, kvp.Value.Rounds);
				WriteByte(stream, kvp.Value.MoveAndFire);
				WriteUShort(stream, kvp.Value.Scan);
				WriteUShort(stream, kvp.Value.Storage);
				WriteUShort(stream, kvp.Value.Ammo);
				WriteUShort(stream, kvp.Value.AttackRadius);
				WriteUShort(stream, kvp.Value.AgentAdjust);
				WriteUShort(stream, kvp.Value.Version);
				WriteByte(stream, kvp.Value.UnitsBuilt);
			}
		}

		private Dictionary<Team, TeamUnits> LoadTeamUnits(Stream stream)
		{
			Log.Debug($"LoadTeamUnits: start, stream position {stream.Position:X}");

			var d = new Dictionary<Team, TeamUnits>();
			for (var t = Team.Red; t <= Team.Gray; t++)
			{
				Log.Debug($"LoadTeamUnits: start team {t}, stream position {stream.Position:X}");

				var tu = new TeamUnits
				{
					Gold = ReadShort(stream),
					BaseUnitValues = LoadUnitValueDictionary(stream),
					CurrentUnitValues = LoadUnitValueDictionary(stream),
					ComplexCount = ReadUShort(stream)
				};

				for (var i = 1; i <= tu.ComplexCount; i++)
				{
					tu.Complexes.Add(LoadComplex(stream));
				}

				Log.Debug($"LoadTeamUnits: end team {t}, stream position {stream.Position:X}");

				d.Add(t, tu);
			}

			Log.Debug($"LoadTeamUnits: end, stream position {stream.Position:X}");

			return d;
		}

		private void WriteComplex(Stream stream, Complex complex)
		{
			WriteUShort(stream, complex.ObjectIndex);
			if (complex.IsEmpty)
			{
				return;
			}

			WriteUShort(stream, complex.ClassType);
			WriteShort(stream, complex.Material);
			WriteShort(stream, complex.Fuel);
			WriteShort(stream, complex.Gold);
			WriteShort(stream, complex.Power);
			WriteShort(stream, complex.Workers);
			WriteShort(stream, complex.Buildings);
			WriteShort(stream, complex.Id);
		}

		private Complex LoadComplex(Stream stream)
		{
			Log.Debug($"LoadComplex: start, stream position {stream.Position:X}");

			var complex = new Complex
			{
				ObjectIndex = ReadUShort(stream)
			};

			if (complex.ObjectIndex >= _lastIndex)
			{
				_lastIndex = complex.ObjectIndex;
				complex.ClassType = ReadUShort(stream);
				complex.Material = ReadShort(stream);
				complex.Fuel = ReadShort(stream);
				complex.Gold = ReadShort(stream);
				complex.Power = ReadShort(stream);
				complex.Workers = ReadShort(stream);
				complex.Buildings = ReadShort(stream);
				complex.Id = ReadShort(stream);
			}
			else
			{
				complex.IsEmpty = true;
			}

			Log.Debug($"LoadComplex: end, stream position {stream.Position:X}");

			return complex;
		}

		private Dictionary<ushort, UnitValue> LoadUnitValueDictionary(Stream stream)
		{
			var d = new Dictionary<ushort, UnitValue>();

			for (var i = UnitType.GoldRefinery; i <= UnitType.DeadWaldo; i++)
			{
				var uv = LoadUnitValue(stream);
				d.Add(uv.ObjectIndex, uv);
			}

			return d;
		}

		private void WriteUnitValue(Stream stream, UnitValue v)
		{
			WriteUShort(stream, v.ObjectIndex);
			if (v.IsEmpty)
			{
				return;
			}

			WriteUShort(stream, v.ClassType);
			WriteUShort(stream, v.Turns);
			WriteUShort(stream, v.Hits);
			WriteUShort(stream, v.Armor);
			WriteUShort(stream, v.Attack);
			WriteUShort(stream, v.Speed);
			WriteUShort(stream, v.Range);
			WriteUShort(stream, v.Rounds);
			WriteByte(stream, v.MoveAndFire);
			WriteUShort(stream, v.Scan);
			WriteUShort(stream, v.Storage);
			WriteUShort(stream, v.Ammo);
			WriteUShort(stream, v.AttackRadius);
			WriteUShort(stream, v.AgentAdjust);
			WriteUShort(stream, v.Version);
			WriteByte(stream, v.UnitsBuilt);
		}

		private UnitValue LoadUnitValue(Stream stream)
		{
			var index = ReadUShort(stream);

			var v = new UnitValue { ObjectIndex = index };

			if (index >= _lastIndex)
			{
				_lastIndex = index;
				v.ClassType = ReadUShort(stream);
				v.Turns = ReadUShort(stream);
				v.Hits = ReadUShort(stream);
				v.Armor = ReadUShort(stream);
				v.Attack = ReadUShort(stream);
				v.Speed = ReadUShort(stream);
				v.Range = ReadUShort(stream);
				v.Rounds = ReadUShort(stream);
				v.MoveAndFire = ReadByte(stream);
				v.Scan = ReadUShort(stream);
				v.Storage = ReadUShort(stream);
				v.Ammo = ReadUShort(stream);
				v.AttackRadius = ReadUShort(stream);
				v.AgentAdjust = ReadUShort(stream);
				v.Version = ReadUShort(stream);
				v.UnitsBuilt = ReadByte(stream);
			}
			else
			{
				v.IsEmpty = true;
			}

			return v;
		}

	}
}
