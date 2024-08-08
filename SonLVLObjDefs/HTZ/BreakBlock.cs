using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class BreakBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[5];
		private Sprite[] debug = new Sprite[5];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(69, 1, 32, 80), -16, -40);
				sprites[1] = new Sprite(sheet.GetSection(69, 17, 32, 64), -16, -32);
				sprites[2] = new Sprite(sheet.GetSection(69, 33, 32, 48), -16, -24);
				sprites[3] = new Sprite(sheet.GetSection(69, 49, 32, 32), -16, -16);
				sprites[4] = new Sprite(sheet.GetSection(69, 65, 32, 16), -16, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(105, 687, 32, 80), -16, -40);
				sprites[1] = new Sprite(sheet.GetSection(105, 703, 32, 64), -16, -32);
				sprites[2] = new Sprite(sheet.GetSection(105, 719, 32, 48), -16, -24);
				sprites[3] = new Sprite(sheet.GetSection(105, 735, 32, 32), -16, -16);
				sprites[4] = new Sprite(sheet.GetSection(105, 751, 32, 16), -16, -8);
			}
			
			for (int i = 0; i < sprites.Length; i++)
			{
				Rectangle bounds = sprites[i].Bounds;
				BitmapBits bitmap = new BitmapBits(bounds.Size);
				bitmap.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1); // LevelData.ColorWhite
				debug[i] = new Sprite(bitmap, bounds.X, bounds.Y);
			}
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How many blocks comprise this Break Block.", null, new Dictionary<string, int>
				{
					{ "5 Blocks", 0 },
					{ "4 Blocks", 1 },
					{ "3 Blocks", 2 },
					{ "2 Blocks", 3 },
					{ "1 Block", 4 }
				},
				(obj) => (obj.PropertyValue & 0x7f),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | (int)value));
			
			properties[1] = new PropertySpec("Collision Plane", typeof(int), "Extended",
				"Which Collision Plane this Break Block is for.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 0x80 }
				},
				(obj) => (obj.PropertyValue & 0x80),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 0x80, 0x81, 0x82, 0x83, 0x84}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string name = properties[0].Enumeration.GetKey(subtype & 0x7f);
			name += ((subtype > 0x7f) ? " (Plane B)" : " (Plane A)");
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min(subtype & 0x7f, 4)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[Math.Min(obj.PropertyValue & 0x7f, 4)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[Math.Min(obj.PropertyValue & 0x7f, 4)];
		}
	}
}