using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.EHZ
{
	class BridgeEnd : EHZ.Generic
	{
		// this object's subtype is set to 2, but unlike the original game where it was like "art index" or whatever,
		// in this game it doesn't matter
		
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(82, 61, 16, 16), -8, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(111, 333, 16, 16), -8, -8);
			}
		}
	}
	
	class BurningLog : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(82, 78, 16, 16), -8, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(137, 313, 16, 16), -8, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Eggman : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[7];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 - 44, -16 + 20); // back wheel
				sprites[1] = new Sprite(sheet.GetSection(70, 155, 60, 20), -28, -28); // eggman
				sprites[2] = new Sprite(sheet.GetSection(0, 209, 64, 29), -32, -8); // eggmobile
				sprites[3] = new Sprite(sheet.GetSection(0, 109, 93, 32), -48, -16 + 8); // car
				sprites[4] = new Sprite(sheet.GetSection(94, 131, 32, 23), -16 - 54, -12 + 16); // drill
				sprites[5] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 - 12, -16 + 20); // front wheel 1
				sprites[6] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 + 28, -16 + 20); // front wheel 1
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 - 44, -16 + 20); // back wheel
				sprites[1] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28); // eggman
				sprites[2] = new Sprite(sheet.GetSection(415, 170, 64, 29), -32, -8); // eggmobile
				sprites[3] = new Sprite(sheet.GetSection(123, 1, 93, 32), -48, -16 + 8); // car
				sprites[4] = new Sprite(sheet.GetSection(123, 34, 32, 23), -16 - 54, -12 + 16); // drill
				sprites[5] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 - 12, -16 + 20); // front wheel 1
				sprites[6] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 + 28, -16 + 20); // front wheel 1
			}
			
			return new Sprite(sprites);
		}
	}
	
	class EggmanCar : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(0, 109, 93, 32), -48, -16);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(123, 1, 93, 32), -48, -16);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanDrill : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(94, 131, 32, 23), -16, -12);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(123, 34, 32, 23), -16, -12);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanWheel : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 143, 32, 32), -16, -16);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(123, 58, 32, 32), -16, -16);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class ExhaustPuff : EHZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(49, 18, 8, 8), -4, -4);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(105, 81, 8, 8), -4, -4);
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