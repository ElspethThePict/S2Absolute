using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class FallingPillar : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[3];
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(59, 42, 56, 56), -28, -32);
			sprites[1] = new Sprite(sheet.GetSection(140, 80, 32, 8), -16, 24);
			sprites[2] = new Sprite(sheet.GetSection(173, 38, 32, 37), -16, 32);
			sprite = new Sprite(sprites);
			
			BitmapBits bitmap = new BitmapBits(1, 0x1C);
			bitmap.DrawLine(6, 0, 0x00, 0, 0x03); // LevelData.ColorWhite
			bitmap.DrawLine(6, 0, 0x08, 0, 0x0B);
			bitmap.DrawLine(6, 0, 0x10, 0, 0x13);
			bitmap.DrawLine(6, 0, 0x18, 0, 0x1B);
			debug = new Sprite(bitmap, 0, 64);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Falling Pillar should behave.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Static", 1 }
				},
				(obj) => ((obj.PropertyValue == 1) ? 1 : 0), // odd way to structure it, but in-game only 1 is regarded as static - all other values are falling
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Static Platform" : "Falling Platform";
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
			return (obj.PropertyValue == 1) ? null : debug;
		}
	}
}