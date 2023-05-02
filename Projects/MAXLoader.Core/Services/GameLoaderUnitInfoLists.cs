using MAXLoader.Core.Types.Enums;
using MAXLoader.Core.Types;
using System;
using System.IO;
using Path = MAXLoader.Core.Types.Path;

namespace MAXLoader.Core.Services
{
	public partial class GameLoader
	{
		private void WriteUnitInfoList(Stream stream, UnitInfoList uil)
		{
			WriteUShort(stream, uil.UnitInfoCount);

			for (var i = 1; i <= uil.UnitInfoCount; i++)
			{
				WriteUnitInfo(stream, uil.Units[i - 1]);
			}
		}

		private UnitInfoList LoadUnitInfoList(Stream stream)
		{
			Log.Debug($"LoadUnitInfoList: start, stream position {stream.Position:X}");

			var uil = new UnitInfoList
			{
				UnitInfoCount = ReadUShort(stream)
			};

			for (var i = 1; i <= uil.UnitInfoCount; i++)
			{
				Log.Debug($"LoadUnitInfoList: start #{i - 1}, stream position {stream.Position:X}");

				uil.Units.Add(LoadUnitInfo(stream));

				Log.Debug($"LoadUnitInfoList: end #{i - 1}, stream position {stream.Position:X}");
			}

			Log.Debug($"LoadUnitInfoList: end, stream position {stream.Position:X}");

			return uil;
		}

		private void WriteUnitInfo(Stream stream, UnitInfo ui)
		{
			WriteUShort(stream, ui.ObjectIndex);
			if (ui.IsEmpty)
			{
				return;
			}

			WriteUShort(stream, (ushort)ui.ClassType);
			WriteUShort(stream, (ushort)ui.UnitType);
			WriteUShort(stream, ui.HashId);
			WriteUInt32(stream, ui.Flags);
			WritePoint(stream, ui.PixelPosition);
			WritePoint(stream, ui.GridPosition);
			WriteUShort(stream, ui.NameLength);
			_byteHandler.WriteCharArray(stream, ui.Name, ui.NameLength);
			WritePoint(stream, ui.ShadowOffset);
			WriteByte(stream, (byte)ui.TeamIndex);
			WriteByte(stream, ui.NameIndex);
			WriteByte(stream, ui.Brightness);
			WriteByte(stream, ui.Angle);
			for (var i = 1; i <= 5; i++)
			{
				WriteByte(stream, ui.VisibleToTeam[i - 1]);
			}
			for (var i = 1; i <= 5; i++)
			{
				WriteByte(stream, ui.SpottedByTeam[i - 1]);
			}

			WriteByte(stream, ui.MaxVelocity);
			WriteByte(stream, ui.Velocity);
			WriteByte(stream, ui.Sound);
			WriteByte(stream, ui.ScalerAdjust);
			WriteRect(stream, ui.SpriteBounds);
			WriteRect(stream, ui.ShadowBounds);
			WriteByte(stream, ui.TurretAngle);
			WriteByte(stream, ui.TurretOffsetX);
			WriteByte(stream, ui.TurretOffsetY);
			WriteUShort(stream, ui.TotalImages);
			WriteUShort(stream, ui.ImageBase);
			WriteUShort(stream, ui.TurretImageBase);
			WriteUShort(stream, ui.FiringImageBase);
			WriteUShort(stream, ui.ConnectorImageBase);
			WriteUShort(stream, ui.ImageIndex);
			WriteUShort(stream, ui.TurretImageIndex);
			WriteUShort(stream, ui.ImageIndexMax);
			WriteByte(stream, (byte)ui.Orders);
			WriteByte(stream, ui.State);
			WriteByte(stream, (byte)ui.PriorOrders);
			WriteByte(stream, ui.PriorState);
			WriteByte(stream, ui.LayingState);
			WritePoint(stream, ui.TargetGrid);
			WriteByte(stream, ui.BuildTime);
			WriteByte(stream, ui.TotalMining);
			WriteByte(stream, ui.RawMining);
			WriteByte(stream, ui.FuelMining);
			WriteByte(stream, ui.GoldMining);
			WriteByte(stream, ui.RawMiningMax);
			WriteByte(stream, ui.GoldMiningMax);
			WriteByte(stream, ui.FuelMiningMax);
			WriteByte(stream, ui.Hits);
			WriteByte(stream, ui.Speed);
			WriteByte(stream, ui.Shots);
			WriteByte(stream, ui.MoveAndFire);
			WriteUShort(stream, ui.Storage);
			WriteByte(stream, ui.Ammo);
			WriteByte(stream, ui.TargetingMode);
			WriteByte(stream, ui.EnterMode);
			WriteByte(stream, ui.Cursor);
			WriteByte(stream, ui.RecoilDelay);
			WriteByte(stream, ui.DelayedReaction);
			WriteByte(stream, ui.DamagedThisTurn);
			WriteByte(stream, ui.ResearchTopic);
			WriteByte(stream, ui.Moved);
			WriteByte(stream, ui.Bobbed);
			WriteByte(stream, ui.ShakeEffectState);
			WriteByte(stream, ui.Engine);
			WriteByte(stream, ui.Weapon);
			WriteByte(stream, ui.Comm);
			WriteByte(stream, ui.FuelDistance);
			WriteByte(stream, ui.MoveFraction);
			WriteByte(stream, ui.Energized);
			WriteByte(stream, ui.RepeatBuild);
			WriteUShort(stream, ui.BuildRate);
			WriteByte(stream, ui.DisabledReactionFire);
			WriteByte(stream, ui.AutoSurvey);
			WriteUInt32(stream, ui.Field221);
			WritePath(stream, ui.Path);
			WriteUShort(stream, ui.Connectors);
			WriteUnitValue(stream, ui.UnitValue);
			WriteComplex(stream, ui.Complex);
			WriteUnitInfo(stream, ui.ParentUnit);
			WriteUnitInfo(stream, ui.EnemyUnit);
			WriteUnitTypeArray(stream, ui.UnitTypeArray);
		}

		private UnitInfo LoadUnitInfo(Stream stream)
		{
			var index = ReadUShort(stream);
			var ui = new UnitInfo { ObjectIndex = index };

			if (index > _lastIndex)
			{
				_lastIndex = index;

				ui.ClassType = (ClassType)ReadUShort(stream);
				ui.UnitType = (UnitType)ReadUShort(stream);
				ui.HashId = ReadUShort(stream);
				ui.Flags = ReadUInt32(stream);
				ui.PixelPosition = ReadPoint(stream);
				ui.GridPosition = ReadPoint(stream);
				ui.NameLength = ReadUShort(stream);
				ui.Name = _byteHandler.ReadCharArray(stream, ui.NameLength);
				ui.ShadowOffset = ReadPoint(stream);
				ui.TeamIndex = (Team)ReadByte(stream);
				ui.NameIndex = ReadByte(stream);
				ui.Brightness = ReadByte(stream);
				ui.Angle = ReadByte(stream);
				for (var i = 1; i <= 5; i++)
				{
					ui.VisibleToTeam[i - 1] = ReadByte(stream);
				}
				for (var i = 1; i <= 5; i++)
				{
					ui.SpottedByTeam[i - 1] = ReadByte(stream);
				}

				ui.MaxVelocity = ReadByte(stream);
				ui.Velocity = ReadByte(stream);
				ui.Sound = ReadByte(stream);
				ui.ScalerAdjust = ReadByte(stream);
				ui.SpriteBounds = ReadRect(stream);
				ui.ShadowBounds = ReadRect(stream);
				ui.TurretAngle = ReadByte(stream);
				ui.TurretOffsetX = ReadByte(stream);
				ui.TurretOffsetY = ReadByte(stream);
				ui.TotalImages = ReadUShort(stream);
				ui.ImageBase = ReadUShort(stream);
				ui.TurretImageBase = ReadUShort(stream);
				ui.FiringImageBase = ReadUShort(stream);
				ui.ConnectorImageBase = ReadUShort(stream);
				ui.ImageIndex = ReadUShort(stream);
				ui.TurretImageIndex = ReadUShort(stream);
				ui.ImageIndexMax = ReadUShort(stream);
				ui.Orders = (OrderType)ReadByte(stream);
				ui.State = ReadByte(stream);
				ui.PriorOrders = (OrderType)ReadByte(stream);
				ui.PriorState = ReadByte(stream);
				ui.LayingState = ReadByte(stream);
				ui.TargetGrid = ReadPoint(stream);
				ui.BuildTime = ReadByte(stream);
				ui.TotalMining = ReadByte(stream);
				ui.RawMining = ReadByte(stream);
				ui.FuelMining = ReadByte(stream);
				ui.GoldMining = ReadByte(stream);
				ui.RawMiningMax = ReadByte(stream);
				ui.GoldMiningMax = ReadByte(stream);
				ui.FuelMiningMax = ReadByte(stream);
				ui.Hits = ReadByte(stream);
				ui.Speed = ReadByte(stream);
				ui.Shots = ReadByte(stream);
				ui.MoveAndFire = ReadByte(stream);
				ui.Storage = ReadUShort(stream);
				ui.Ammo = ReadByte(stream);
				ui.TargetingMode = ReadByte(stream);
				ui.EnterMode = ReadByte(stream);
				ui.Cursor = ReadByte(stream);
				ui.RecoilDelay = ReadByte(stream);
				ui.DelayedReaction = ReadByte(stream);
				ui.DamagedThisTurn = ReadByte(stream);
				ui.ResearchTopic = ReadByte(stream);
				ui.Moved = ReadByte(stream);
				ui.Bobbed = ReadByte(stream);
				ui.ShakeEffectState = ReadByte(stream);
				ui.Engine = ReadByte(stream);
				ui.Weapon = ReadByte(stream);
				ui.Comm = ReadByte(stream);
				ui.FuelDistance = ReadByte(stream);
				ui.MoveFraction = ReadByte(stream);
				ui.Energized = ReadByte(stream);
				ui.RepeatBuild = ReadByte(stream);
				ui.BuildRate = ReadUShort(stream);
				ui.DisabledReactionFire = ReadByte(stream);
				ui.AutoSurvey = ReadByte(stream);
				ui.Field221 = ReadUInt32(stream);
				ui.Path = LoadPath(stream);
				ui.Connectors = ReadUShort(stream);
				ui.UnitValue = LoadUnitValue(stream);
				ui.Complex = LoadComplex(stream);
				ui.ParentUnit = LoadUnitInfo(stream);
				ui.EnemyUnit = LoadUnitInfo(stream);
				ui.UnitTypeArray = LoadUnitTypeArray(stream);
			}
			else
			{
				ui.IsEmpty = true;
			}

			return ui;
		}

		private void WriteUnitTypeArray(Stream stream, UnitTypeArray uta)
		{
			WriteUShort(stream, uta.ObjectCount);

			for (var i = 1; i <= uta.ObjectCount; i++)
			{
				WriteUShort(stream, (ushort)uta.Array[i - 1]);
			}
		}

		private UnitTypeArray LoadUnitTypeArray(Stream stream)
		{
			var uta = new UnitTypeArray
			{
				ObjectCount = ReadUShort(stream)
			};

			for (var i = 1; i <= uta.ObjectCount; i++)
			{
				uta.Array.Add((UnitType)ReadUShort(stream));
			}

			return uta;
		}

		private void WritePath(Stream stream, Path path)
		{
			WriteUShort(stream, path.ObjectIndex);
			if (path.IsEmpty)
			{
				return;
			}

			WriteShort(stream, (short)path.ClassType);
			switch (path.ClassType)
			{
				case ClassType.AirPath:
					if (path.PathClass is not PathAir c)
					{
						throw new InvalidOperationException("Could not cast PathClass to PathAir");
					}

					WriteShort(stream, c.Length);
					WriteByte(stream, c.Angle);
					WritePoint(stream, c.PixelStart);
					WritePoint(stream, c.PixelEnd);
					WriteInt(stream, c.XStep);
					WriteInt(stream, c.YStep);
					WriteInt(stream, c.DeltaX);
					WriteInt(stream, c.DeltaY);
					break;
				case ClassType.GroundPath:
					if (path.PathClass is not PathGround g)
					{
						throw new InvalidOperationException("Could not cast PathClass to PathGround");
					}

					WritePoint(stream, g.PixelEnd);
					WriteShort(stream, g.Index);
					WriteShort(stream, g.StepsCount);

					for (var i = 1; i <= g.StepsCount; i++)
					{
						WriteByte(stream, g.Steps[i - 1].X);
						WriteByte(stream, g.Steps[i - 1].Y);
					}
					break;
				case ClassType.BuilderPath:
					if (path.PathClass is not PathBuilder b)
					{
						throw new InvalidOperationException("Could not cast PathClass to PathBuilder");
					}

					WritePoint(stream, b.Coordinate);
					break;
			}
		}

		private Path LoadPath(Stream stream)
		{
			var path = new Path
			{
				ObjectIndex = ReadUShort(stream)
			};

			if (path.ObjectIndex < _lastIndex)
			{
				path.IsEmpty = true;
				return path;
			}

			_lastIndex = path.ObjectIndex;

			path.ClassType = (ClassType)ReadShort(stream);
			switch (path.ClassType)
			{
				case ClassType.AirPath:
					path.PathClass = new PathAir
					{
						Length = ReadShort(stream),
						Angle = ReadByte(stream),
						PixelStart = ReadPoint(stream),
						PixelEnd = ReadPoint(stream),
						XStep = ReadInt(stream),
						YStep = ReadInt(stream),
						DeltaX = ReadInt(stream),
						DeltaY = ReadInt(stream)
					};
					break;
				case ClassType.GroundPath:
					var ground = new PathGround
					{
						PixelEnd = ReadPoint(stream),
						Index = ReadShort(stream),
						StepsCount = ReadShort(stream)
					};
					for (var i = 1; i <= ground.StepsCount; i++)
					{
						ground.Steps.Add(new PathStep
						{
							X = ReadByte(stream),
							Y = ReadByte(stream),
						});
					}
					path.PathClass = ground;
					break;
				case ClassType.BuilderPath:
					path.PathClass = new PathBuilder
					{
						Coordinate = ReadPoint(stream)
					};
					break;
				default:
					throw new InvalidOperationException($"Unknown path class {path.ClassType}");
			}

			return path;
		}
	}
}
