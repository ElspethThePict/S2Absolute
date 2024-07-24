using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class BreakoffPillar : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(59, 42, 56, 56), -28, -32);
			sprites[1] = new Sprite(sheet.GetSection(140, 80, 32, 8), -16, 24);
			sprites[2] = new Sprite(sheet.GetSection(173, 38, 32, 37), -16, 32);

			// LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(32, 64);
			bitmap.DrawRectangle(6, 0, 0, 31, 36);
			bitmap.DrawLine(6, 16, 0x08 + 32, 16, 0x0B + 32);
			bitmap.DrawLine(6, 16, 0x10 + 32, 16, 0x13 + 32);
			bitmap.DrawLine(6, 16, 0x18 + 32, 16, 0x1B + 32);
			debug = new Sprite(bitmap, -16, 32);
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
			get { return new Sprite(sprites); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return new Sprite(sprites);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return new Sprite(sprites);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 0) ? debug : null;
		}
	}
}