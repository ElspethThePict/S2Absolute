using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.OOZ
{
	class Eggman : OOZ.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(365, 125, 96, 64), -48, -32);
				sprites[1] = new Sprite(sheet.GetSection(462, 118, 24, 12), -12, -12);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(313, 112, 96, 64), -48, -32);
				sprites[1] = new Sprite(sheet.GetSection(413, 91, 24, 12), -12, -12);
			}
			
			return new Sprite(sprites);
		}
	}
	
	class EggmanHarpoon : OOZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(379, 213, 16, 31), -8, -34);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(360, 50, 16, 31), -8, -34);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanCannon : OOZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(141, 224, 40, 16), -32, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(553, 180, 40, 16), -32, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanLaser : OOZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(149, 241, 16, 4), -16, -2);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(594, 176, 16, 4), -16, -2);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanFlame : OOZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(345, 231, 16, 24), -8, -24);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(453, 199, 16, 24), -8, -24);
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