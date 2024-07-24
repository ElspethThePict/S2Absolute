using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.OOZ
{
	class OilFall : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[6];
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(182, 51, 6, 32), -2, -16);
			sprites[1] = new Sprite(sheet.GetSection(182, 148, 6, 32), -2, -16);
			sprites[2] = new Sprite(sheet.GetSection(182, 59, 6, 32), -2, -16);
			sprites[3] = new Sprite(sheet.GetSection(182, 156, 6, 32), -2, -16);
			sprites[4] = new Sprite(sheet.GetSection(189, 75, 16, 32), -8, -16);
			sprites[5] = new Sprite(sheet.GetSection(189, 107, 48, 32), -24, -16);
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How long this Oil Fall should run for.", null,
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | (int)value));
			
			properties[1] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this Oil Fall should display.", null, new Dictionary<string, int>
				{
					{ "Curve", 0 },
					{ "Thin", 1 },
					{ "Wide", 2 },
					{ "Wide (Double)", 3 }
				},
				(obj) => (obj.PropertyValue >> 3) & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~(3 << 3)) | ((int)value & 3) << 3));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x10; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int frame = obj.PropertyValue >> 3;
			
			if (obj.PropertyValue > 0)
				frame += 2;
			else
				frame &= 1; // original code does this, i don't think this really means anything though..? it's not like prop val can be less than 0, and (0 & 1) is still just.. zero
			
			int length = Math.Max((int)(obj.PropertyValue & 7), 1);
			int sy = -((length * 32) / 2) + 16;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < length; i++)
			{
				sprs.Add(new Sprite(sprites[frame], 0, sy + (i * 32)));
			}
			return new Sprite(sprs.ToArray());
		}
	}
}