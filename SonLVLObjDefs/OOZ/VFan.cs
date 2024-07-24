using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class VFan : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(206, 181, 32, 24), -16, -12);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(376, 820, 32, 24), -16, -12);
			}
			
			BitmapBits bitmap = new BitmapBits(129, 145);
			bitmap.DrawRectangle(6, 0, 0, 128, 144); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -64, -112);
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