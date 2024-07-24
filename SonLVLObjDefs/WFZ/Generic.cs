using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.WFZ
{
	class Turret : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(431, 88, 32, 38), -16, -16);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(934, 574, 32, 38), -16, -16);
			}
		}
		
		// even if it's set, prop val doesn't seem to be used
	}
	
	class TurretBullet : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(442, 79, 8, 8), -4, -4);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(973, 677, 8, 8), -4, -4);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanBarrier : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(294, 1, 16, 128), -8, -64);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(463, 1, 16, 128), -8, -64);
			}
		}
		
		public override bool Hidden { get { return true; } }
		
		// prop val is used but this object shouldn't be placed in the scene in the first place
	}
	
	class EggmanDispenser : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(311, 82, 64, 16), -32, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(513, 163, 64, 16), -32, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanPlatform : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(376, 18, 32, 24), -16, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(397, 18, 32, 24), -16, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanLaser : WFZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(311, 1, 64, 23), -32, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(513, 139, 64, 23), -32, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	abstract class Generic : ObjectDefinition
	{
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}