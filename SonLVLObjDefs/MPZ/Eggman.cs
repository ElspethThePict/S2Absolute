using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	// various boss-related objects are in here
	
	class Eggman : MPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[3];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(195, 151, 44, 16), -28, -24);
				sprites[1] = new Sprite(sheet.GetSection(220, 206, 56, 49), -24, -28);
				sprites[2] = new Sprite(sheet.GetSection(190, 69, 8, 23), -32, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(255, 206, 56, 49), -24, -28);
				sprites[2] = new Sprite(sheet.GetSection(316, 232, 8, 23), -32, -8);
			}
			
			return new Sprite(sprites);
		}
	}
	
	class EggmanBalloon : MPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
			{
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(430, 133, 48, 47), -24, -24);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(486, 208, 48, 47), -24, -24);
			}
		}
		
		public override bool Hidden
		{
			get { return true; }
		}
	}
	
	class EggmanLaser : MPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
			{
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(220, 193, 64, 12), -32, -6);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(247, 185, 64, 12), -32, -6);
			}
		}
		
		public override bool Hidden
		{
			get { return true; }
		}
	}
	
	abstract class EggmanShared : ObjectDefinition
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