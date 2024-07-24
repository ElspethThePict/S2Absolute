using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.HTZ
{
	class EggmanFireball1 : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(158, 95, 8, 8), -4, -4);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(480, 179, 8, 8), -4, -4);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanFireball2 : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(191, 94, 16, 16), -8, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1008, 1, 16, 16), -8, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanSmokePuff : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(52, 1, 16, 13), -8, -6);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(903, 71, 16, 13), -8, -6);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class GroundFlame : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(1, 1, 16, 31), -8, -15);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(869, 68, 16, 31), -8, -19);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class LavaBubble : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(124, 110, 16, 11), -8, -2);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(125, 110, 16, 11), -8, -2);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class LavaJump : HTZ.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(91, 123, 15, 15), -8, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(121, 653, 15, 15), -8, -8); // using fixed frame
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