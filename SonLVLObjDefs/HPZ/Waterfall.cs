using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.HPZ
{
	class Waterfall : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private Sprite[] sprites = new Sprite[32];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[17];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("HPZ/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(223, 0, 32, 16), -16, -16);
			frames[1] = new Sprite(sheet.GetSection(223, 0, 32, 16), -16, -16);
			frames[2] = new Sprite(sheet.GetSection(223, 0, 32, 32), -16, -16);
			frames[3] = new Sprite(sheet.GetSection(223, 0, 32, 48), -16, -16);
			frames[4] = new Sprite(sheet.GetSection(223, 0, 32, 64), -16, -16);
			frames[5] = new Sprite(sheet.GetSection(223, 0, 32, 80), -16, -16);
			frames[6] = new Sprite(sheet.GetSection(223, 0, 32, 96), -16, -16);
			frames[7] = new Sprite(sheet.GetSection(223, 0, 32, 112), -16, -16);
			frames[8] = new Sprite(sheet.GetSection(223, 32, 32, 16), -16, -16);
			frames[9] = new Sprite(sheet.GetSection(223, 32, 32, 16), -16, -16);
			frames[10] = new Sprite(sheet.GetSection(223, 32, 32, 32), -16, -16);
			frames[11] = new Sprite(sheet.GetSection(223, 32, 32, 48), -16, -16);
			frames[12] = new Sprite(sheet.GetSection(223, 32, 32, 64), -16, -16);
			frames[13] = new Sprite(sheet.GetSection(223, 32, 32, 80), -16, -16);
			frames[14] = new Sprite(sheet.GetSection(223, 32, 32, 96), -16, -16);
			frames[15] = new Sprite(sheet.GetSection(223, 32, 32, 112), -16, -16);
			frames[16] = new Sprite(sheet.GetSection(174, 231, 48, 24), -24, -40);
			
			for (int i = 0; i < 32; i++)
			{
				sprites[i] = new Sprite(frames[(i & 15)]);
				
				if ((i & 0x10) == 0x10) // (splash?)
					sprites[i] = new Sprite(sprites[i], new Sprite(frames[16], 0, Math.Max(i & 7, 1) << 4));
			}
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How long of a waterfall this object should be.", null, new Dictionary<string, int>
				{
					{ "Dynamic (Use WaterLevel)", 0 },
					{ "16 pixels", 1 },
					{ "32 pixels", 2 },
					{ "48 pixels", 3 },
					{ "64 pixels", 4 },
					{ "80 pixels", 5 },
					{ "96 pixels", 6 },
					{ "112 pixels", 7 }
				},
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | ((int)value)));
			
			properties[1] = new PropertySpec("Has Ridge", typeof(bool), "Extended",
				"If this Waterfall should have have a ridge at its top.", null,
				(obj) => (obj.PropertyValue & 8) == 0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~8) | ((bool)value ? 0 : 8)));
			
			properties[2] = new PropertySpec("Has Splash", typeof(bool), "Extended",
				"If this Waterfall should make a splash at its bottom.", null,
				(obj) => (obj.PropertyValue & 0x10) == 0x10,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x10) | ((bool)value ? 0x10 : 0)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			String str = ((subtype & 7) == 0) ? "Dynamic Height" : ((subtype * 16) + " pixels tall");
			
			// Yeah you can technically combine them
			if ((subtype & 8) == 8) str += " (No Ridge)";
			if ((subtype & 0x10) == 0x10) str += " (Has Splash)";
			
			return str;
		}

		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 31];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 31];
		}
	}
}