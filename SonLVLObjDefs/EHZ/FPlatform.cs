using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class FPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(127, 98, 64, 32), -32, -12);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 402, 64, 32), -32, -8);
			}
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(1, 40);
			for (int i = 0; i < bitmap.Height; i += 8)
				bitmap.DrawLine(6, 0, i, 0, i + 3);
			
			debug = new Sprite(bitmap);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should act upon player contact.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Hover", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Hover Platform" : "Fall Platform";
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
			if (obj.PropertyValue == 1) return null;
			return debug;
		}
	}
}
