using SonicRetro.SonLVL.API;
using System;

// mostly just projeciles and other basic renders which only need MBZ checks
// sorted alphabetically

namespace S2ObjectDefinitions.Enemies
{
	class AquisShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(99, 18, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(933, 323, 8, 8), -4, -4);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Asteron : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(223, 1, 32, 28), -16, -14);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(843, 289, 32, 28), -16, -14); // (SCZ mission ends up here too)
		}
	}
	
	class AsteronSpike : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(182, 1, 7, 14), -4, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(968, 322, 7, 14), -4, -8); // (SCZ mission ends up here too)
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Ball : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12); // broken frame btw
		}
	}
	
	class BigTurtloid : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(72, 42, 56, 31), -28, -15);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(72, 42, 56, 31), -28, -15); // broken frame btw
		}
	}
	
	class Bubbler : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(141, 33, 24, 15), -12, -15);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12); // broken frame btw
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class BeeShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 50, 8, 10), -12, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 50, 8, 10), -12, -3); // broken frame btw
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class BuzzerShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 50, 8, 10), -12, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(66, 302, 8, 10), -12, -3);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Clucker : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(9, 223, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(1, 246, 8, 7), -24, 7);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(845, 256, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(837, 279, 8, 7), -24, 7);
			}
			
			return new Sprite(sprites);
		}
	}
	
	class CluckerBase : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 206, 48, 16), -24, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(576, 289, 48, 16), -24, -8);
		}
	}
	
	class CluckerShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(50, 214, 6, 8), -3, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(660, 289, 6, 8), -3, -4);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Coconut : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(82, 95, 12, 13), -6, -7);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(258, 297, 12, 13), -6, -7);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Coconuts : Enemies.Generic
	{
		// Coconuts' subtype is normally 30, but this value isn't used in any proper way
		// (property value is treated as direction, but it gets reset to face the player in-game and a subtype of 30 doesn't exactly sound like a direction either)
		// MBZ Coconuts don't have anything in their prop val either anyways, so let's just gloss over it here
		
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 63, 26, 45), -8, -14);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(50, 256, 26, 45), -8, -14);
		}
	}
	
	class Crawl : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(1, 1, 47, 32), -23, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(436, 256, 47, 32), -23, -16);
		}
	}
	
	class Crawlton : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			else
			{
				// broken frames btw
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			
			return new Sprite(sprites);
		}
	}
	
	class Flasher : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
				return new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(1, 1, 23, 15), -16, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 1, 23, 15), -16, -8); // broken frame btw
		}
	}
	
	class GrabberShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(46, 83, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(84, 302, 8, 8), -4, -4);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class NebulaBomb : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(220, 1, 14, 13), -7, -7);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(220, 1, 14, 13), -7, -7);  // broken frame btw
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class OctusShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(92, 32, 6, 6), -3, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(910, 332, 6, 6), -3, -3); // (SCZ mission ends up here too)
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Rexon : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[3];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6); // head
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8); // body piece
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8); // shell
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				
				// broken frames btw
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6); // head
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8); // body piece
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8); // shell
			}
			
			// (offset values pulled from in-game, there's not much of a reason to translate the entire drawing code to C#)
			Sprite[] sprs = new Sprite[] {
				new Sprite(sprites[1], -31,   4), // piece 4, spr 1
				new Sprite(sprites[1], -29, -11), // piece 3, spr 1
				new Sprite(sprites[1], -25, -25), // piece 2, spr 1
				new Sprite(sprites[1], -20, -39), // piece 1, spr 1
				new Sprite(sprites[0], -16, -54), // head, spr 0
				sprites[2]}; // shell, spr 2
			
			return new Sprite(sprs);
		}
	}
	
	class RexonShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(36, 54, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(36, 54, 8, 8), -4, -4); // broken frame btw
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class SlicerArm : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(75, 51, 16, 16), 0, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(926, 306, 16, 16), 0, -16);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class SmallTurtloid : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(186, 42, 24, 23), -12, -11);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(186, 42, 24, 23), -12, -11); // broken frame btw
		}
	}
	
	class SpinyShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(73, 25, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(84, 302, 8, 8), -4, -4);
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class TurtloidShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(220, 29, 6, 6), -3, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(815, 310, 6, 6), -3, -3); // broken frame btw
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class Whisp : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(34, 42, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(34, 58, 21, 6), -9, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(111, 317, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(110, 302, 21, 6), -9, -8);
			}
			
			return new Sprite(sprites);
		}
	}
	
	abstract class Generic : ObjectDefinition
	{
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
		}
		
		public override System.Collections.ObjectModel.ReadOnlyCollection<byte> Subtypes
		{
			get { return new System.Collections.ObjectModel.ReadOnlyCollection<byte>(new System.Collections.Generic.List<byte>()); }
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