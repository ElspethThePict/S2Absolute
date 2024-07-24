using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	// various boss-related objects are in here
	
	class Eggman : CPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[6];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("Global/EggMobile.gif");
				sprites[0] = new Sprite(sheet.GetSection(5, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(1, 64, 64, 29), -32, -8);
				
				// Chemical Dropper frames
				sheet = LevelData.GetSpriteSheet("CPZ/Objects.gif");
				sprites[2] = new Sprite(sheet.GetSection(55, 74, 37, 52), -5, -68); // chemical machine
				sprites[3] = new Sprite(sheet.GetSection(70, 131, 22, 24), 1 - 16, 0 - 56); // chemicals (filled)
				sprites[4] = new Sprite(sheet.GetSection(93, 131, 48, 24), 0 - 16, 0 - 56); // chemicals dropper
				sprites[5] = new Sprite(sheet.GetSection(74, 189, 22, 2), 1 - 16, 24 - 56); // dropper hatch (closed)
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(415, 170, 64, 29), -32, -8);
				
				// Chemical Dropper frames
				sprites[2] = new Sprite(sheet.GetSection(34, 138, 37, 52), -5, -68); // chemical machine
				sprites[3] = new Sprite(sheet.GetSection(47, 113, 22, 24), 1 - 16, 0 - 56); // chemicals (filled)
				sprites[4] = new Sprite(sheet.GetSection(70, 113, 48, 24), 0 - 16, 0 - 56); // chemicals dropper
				sprites[5] = new Sprite(sheet.GetSection(81, 163, 22, 2), 1 - 16, 24 - 56); // dropper hatch (closed)
			}
			
			return new Sprite(sprites);
		}
	}
	
	class ChemicalDropper : CPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(55, 74, 37, 52), -5, -68);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(34, 138, 37, 52), -5, -68);
			}
		}
		
		public override bool Hidden
		{
			get { return true; }
		}
	}
	
	class ChemicalDrop : CPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(97, 189, 18, 29), -9, -18);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 64, 18, 29), -9, -18);
			}
		}
		
		public override bool Hidden
		{
			get { return true; }
		}
	}
	
	class ChemicalSplash : CPZ.EggmanShared
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(93, 122, 8, 8), -4, -4);
			}
			else // base game doesn't use else (for some reason) but we want a fallback in case neither folders match
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(104, 155, 8, 8), -4, -4);
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