using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class HDropBridge : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite log = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(135, 131, 16, 16), -8, -8);
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 4; i++)
			{
				sprs.Add(new Sprite(log, (i * 16) - 56, 0));
				sprs.Add(new Sprite(log, 56 - (i * 16), 0));
			}
			
			sprite = new Sprite(sprs.ToArray());
			
			// tagging this area with LevelData.ColorWhite
			
			BitmapBits rect = new BitmapBits(16, 64);
			rect.DrawRectangle(6, 0, 0, 15, 63);
			
			BitmapBits circle = new BitmapBits(3 * 16 + 1, 3 * 16 + 1);
			circle.DrawCircle(6, 0, 0, 3 * 16);
			debug = new Sprite(new Sprite(rect, -64, -8), new Sprite(circle, -(3 * 16) - 8, 0));
			
			debug = new Sprite(debug, new Sprite(debug, true, false));
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