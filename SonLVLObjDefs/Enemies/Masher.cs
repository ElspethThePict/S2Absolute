using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.Enemies
{
	class Masher : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				sprite = new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(106, 67, 20, 32), -10, -16); // #Absolute - change sprite bounds
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(66, 313, 20, 32), -10, -16);
			
			// it's kind of hard to guess how high the Masher will jump, so let's draw a debug vis for it
			BitmapBits bitmap = new BitmapBits(2, 136);
			bitmap.DrawLine(6, 0, 0, 0, 136); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -136);
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