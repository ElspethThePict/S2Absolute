using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class PSwitch_V : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[21];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			int index = 0;
			for (int i = 0; i < 8; i++)
			{
				sprites[index++] = new Sprite(sheet.GetSection(42 + (i * 17), 130, 16, 16), -8, -8);
			}
			
			for (int i = 0; i < 8; i++)
			{
				sprites[index++] = new Sprite(sheet.GetSection(42 + (i * 17), 147, 16, 16), -8, -8);
			}
			
			sprites[index++] = new Sprite(sheet.GetSection(42, 164, 16, 16), -8, -8);
			sprites[index++] = new Sprite(sheet.GetSection(59, 164, 16, 16), -8, -8);
			sprites[index++] = new Sprite(sheet.GetSection(76, 164, 16, 16), -8, -8);
			sprites[index++] = new Sprite(sheet.GetSection(93, 164, 16, 16), -8, -8);
			sprites[index++] = new Sprite(sheet.GetSection(93, 113, 16, 16), -8, -8);
			
			properties = new PropertySpec[7];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How tall the Plane Switch will be.", null, new Dictionary<string, int>
				{
					{ "4 Nodes", 0 },
					{ "8 Nodes", 1 },
					{ "16 Nodes", 2 },
					{ "32 Nodes", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | (int)value));
			
			properties[1] = new PropertySpec("Left Collision Plane", typeof(int), "Extended",
				"Which plane is to the left.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~4) | (int)value));
			
			properties[2] = new PropertySpec("Right Collision Plane", typeof(int), "Extended",
				"Which plane is to the right.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 8 }
				},
				(obj) => obj.PropertyValue & 8,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~8) | (int)value));
			
			properties[3] = new PropertySpec("Left Draw Order", typeof(int), "Extended",
				"Which draw layer is to the left.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 16 }
				},
				(obj) => obj.PropertyValue & 16,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~16) | (int)value));
			
			properties[4] = new PropertySpec("Right Draw Order", typeof(int), "Extended",
				"Which draw layer is to the right.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 32 }
				},
				(obj) => obj.PropertyValue & 32,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~32) | (int)value));
			
			properties[5] = new PropertySpec("Only Draw Order", typeof(bool), "Extended",
				"If only Draw Order should be set, and not collision plane.", null,
				(obj) => ((obj.PropertyValue & 64) == 64),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~64) | ((bool)value ? 64 : 0)));
			
			properties[6] = new PropertySpec("Grounded", typeof(bool), "Extended",
				"If only grounded players should be affected.", null,
				(obj) => (obj.PropertyValue > 127),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~128) | ((bool)value ? 128 : 0)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); } // putting every single combination here would be kinda too much, but let's have Size here
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype & 3) + " Tall";
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
			int count = Math.Max((1 << ((obj.PropertyValue & 3) + 2)), 1);
			int sy = -(((count) * 16) / 2) + 8;
			
			int index = (obj.PropertyValue >> 2) & 15;
			if ((obj.PropertyValue & 64) == 64) // draw order only?
				index = (index >> 2) + 16;
			
			Sprite frame = new Sprite(sprites[index]);
			if (obj.PropertyValue > 0x7f) // Grounded, add back sprite
				frame = new Sprite(sprites[20], frame);
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < count; i++)
			{
				sprs.Add(new Sprite(frame, 0, sy + (i * 16)));
			}
			
			return new Sprite(sprs.ToArray());
		}
	}
}