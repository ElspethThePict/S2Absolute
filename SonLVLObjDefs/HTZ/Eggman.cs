using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[3];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(66, 108, 17, 7), -15, -10);
				sprites[1] = new Sprite(sheet.GetSection(1, 210, 64, 45), -32, -12);
				sprites[2] = new Sprite(sheet.GetSection(9, 186, 40, 24), -24, -36);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1007, 34, 17, 7), -15, -10);
				sprites[1] = new Sprite(sheet.GetSection(415, 154, 64, 45), -32, -12);
				sprites[2] = new Sprite(sheet.GetSection(423, 130, 40, 24), -24, -36);
			}
			
			sprite = new Sprite(sprites);
			
			// let's show the position of the pits Eggman will jump out of
			// (LevelData.ColorWhite tag)
			BitmapBits bitmap = new BitmapBits(257, 163);
			bitmap.DrawLine(6, 0, 162, 0, 23); // Lava->Sky on left lava pit
			bitmap.DrawLine(6, 256, 140, 256, 0); // Lava->Sky on right lava pit
			bitmap.DrawLine(6, 0, 162, 256, 140); // Connect the two pits
			debug = new Sprite(bitmap, -257, -125);
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}