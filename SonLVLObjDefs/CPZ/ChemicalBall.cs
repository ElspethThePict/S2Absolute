using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class ChemicalBall : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug   = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(207, 125, 16, 16), -8, -8);
			
			sprites[1] = new Sprite(sprites[0], 96, 0);
			
			BitmapBits bitmap = new BitmapBits(97, 112);
			bitmap.DrawEllipse(6, 0, 0, 96, 222); // somewhat off, but it's close enough
			debug[0] = new Sprite(bitmap, 0, -112);
			
			bitmap.Clear();
			bitmap.DrawLine(6, 0, 0, 0, 111);
			bitmap.DrawLine(6, 96, 0, 96, 111);
			debug[1] = new Sprite(bitmap, 0, -112);
			
			properties[0] = new PropertySpec("Interval", typeof(int), "Extended",
				"What interval this Chemical Ball should follow.", null,
				(obj) => obj.PropertyValue & 0x3F,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x3F) | (int)value));
			
			properties[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction this Chemical Ball should start from.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x40) | (int)value));
			
			properties[2] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Chemical Ball should act.", null, new Dictionary<string, int>
				{
					{ "Arc", 0x00 },
					{ "Straight", 0x80 }
				},
				(obj) => obj.PropertyValue & 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x00, 0x40, 0x80, 0xC0}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype < 0x80) ? "Arc" : "Straight") + (((subtype & 0x40) == 0x00) ? " (Start From Left)" : " (Start From Right)");
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 0x40) >> 6];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue >> 7];
		}
	}
}