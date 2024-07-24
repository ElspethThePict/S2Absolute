using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Spinner_H : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private readonly Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			sprites[0] = new Sprite(sheet.GetSection(127, 113, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(144, 113, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How wide the Spinner is.", null, new Dictionary<string, int>
				{
					{ "4 Nodes", 0 },
					{ "8 Nodes", 1 },
					{ "16 Nodes", 2 },
					{ "32 Nodes", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | (int)value));
			
			properties[1] = new PropertySpec("Enter From", typeof(int), "Extended",
				"Which side the Spinner should have effect on.", null, new Dictionary<string, int>
				{
					{ "Top", 0 },
					{ "Bottom", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~4) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2, 4, 6, 1, 3, 5, 7}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (1 << ((subtype & 3) + 2)) + " Nodes, " + (((subtype & 4) == 0) ? "Enter From Top" : "Enter From Bottom");
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
			int count = 1 << ((obj.PropertyValue & 3) + 2);
			int sx = -(((count) * 16) / 2) + 8;
			int frame = (obj.PropertyValue & 4) >> 2;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < count; i++)
			{
				sprs.Add(new Sprite(sprites[frame], sx + (i * 16), 0));
			}
			return new Sprite(sprs.ToArray());
		}
	}
}