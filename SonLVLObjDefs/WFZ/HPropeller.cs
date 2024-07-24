using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.SCZ
{
	class HPropeller : WFZ.HPropeller
	{
		// in WFZ we're just a deco sprite with no interaction at all, so we don't need the debug vis
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return null;
		}
	}
}

namespace S2ObjectDefinitions.WFZ
{
	class HPropeller : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			frames[0] = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 171, 124, 5), -62, -2);
			frames[1] = new Sprite(frames[0], 0, 16);
			sprite = new Sprite(frames);
			
			BitmapBits overlay = new BitmapBits(129, 169);
			overlay.DrawRectangle(6, 0, 0, 128, 168); // LevelData.ColorWhite
			debug = new Sprite(overlay, -64, -112);
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
