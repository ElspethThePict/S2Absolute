using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.ARZ
{
	class Brick : ARZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(18, 128, 32, 16), -16, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(436, 306, 32, 16), -16, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Eggman : ARZ.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[4];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("Global/EggMobile.gif");
				sprites[0] = new Sprite(sheet.GetSection(5, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(1, 64, 64, 29), -32, -8);
				
				// Eggman Hammer frames
				sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
				sprites[2] = new Sprite(sheet.GetSection(1, 147, 76, 52), -44, -28); // eggmobile addon
				sprites[3] = new Sprite(sheet.GetSection(1, 202, 54, 53), -50 - 40, -49 + 4); // hammer (idle)
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(415, 170, 64, 29), -32, -8);
				
				// Eggman Hammer frames
				sprites[2] = new Sprite(sheet.GetSection(222, 5, 76, 52), -44, -28); // eggmobile addon
				sprites[3] = new Sprite(sheet.GetSection(255, 58, 54, 53), -50 - 40, -49 + 4); // hammer (idle)
			}
			
			return new Sprite(sprites);
		}
	}
	
	class EggmanHammer : ARZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(1, 147, 76, 52), -44, -28);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(222, 5, 76, 52), -44, -28);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanTotem : ARZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(223, 1, 32, 160), 0, -64);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 95, 32, 160), 0, -64);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanArrow : ARZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(194, 1, 29, 6), -16, -3);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(298, 31, 29, 6), -16, -3);
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