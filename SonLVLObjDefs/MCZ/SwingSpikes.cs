using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.MCZ
{
	class SwingSpikes : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(152, 131, 23, 33), -12, -12); // post
			sprites[1] = new Sprite(sheet.GetSection(135, 148, 16, 16), -8, -8); // chain
			sprites[2] = new Sprite(sheet.GetSection(30, 32, 62, 16), -31, -8);  // platform
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains the Spike Platform should hang off of.", null,
				(obj) => (Math.Max(obj.PropertyValue, (byte)1) - 1) & 7,
				(obj, value) => obj.PropertyValue = (byte)((((Math.Max(obj.PropertyValue, (byte)1) - 1) & ~7) | (int)value) + 1));
			
			properties[1] = new PropertySpec("Swing Direction", typeof(int), "Extended",
				"Which direction the Platform should swing in.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 8 }
				},
				(obj) => (Math.Max(obj.PropertyValue, (byte)1) - 1) & 8,
				(obj, value) => obj.PropertyValue = (byte)((((Math.Max(obj.PropertyValue, (byte)1) - 1) & ~8) | (int)value) + 1));
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			int length = ((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7);
			int px = 16;
			int m = ((((Math.Max(obj.PropertyValue, (byte)1) - 1) & 8) == 8) ? 1 : -1);
			for (int i = 0; i < length + 1; i++)
			{
				sprs.Add(new Sprite(sprites[1], px * m, 0));
				px += 16;
			}
			
			sprs.Add(new Sprite(sprites[0]));
			sprs.Add(new Sprite(sprites[2], (px-8) * m, 0));
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7) + 1) * 16 + 8;
			
			BitmapBits overlay = new BitmapBits(length + 1, length + 1);
			overlay.DrawCircle(6, length, 0, length); // LevelData.ColorWhite
			if (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 8) == 8)
			{
				overlay.Flip(true, false);
				length = 0;
			}
			return new Sprite(overlay, -length, 0);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int sx = (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7) + 1) * 16 + 8;
			
			if (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 8) == 8)
				sx = -sx;
			
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X - sx, obj.Y);
			return bounds;
		}
	}
}