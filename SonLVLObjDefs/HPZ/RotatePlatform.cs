using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.HPZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[4];
		private Sprite[] sprites = new Sprite[6];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("HPZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(181, 73, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(181, 90, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(181, 107, 16, 16), -8, -8);
			sprites[3] = new Sprite(sheet.GetSection(181, 73, 16, 16), -8, -8);
			sprites[4] = new Sprite(sheet.GetSection(1, 183, 64, 16), -32, -8);
			sprites[5] = new Sprite(sheet.GetSection(402, 66, 40, 40), -20, -20);
			
			// tagging this area with LevelData.ColorWhite
			
			BitmapBits bitmap = new BitmapBits(161, 161);
			bitmap.DrawCircle(6, 80, 80, 80);
			debug[0] = new Sprite(bitmap, -80, -80);
			
			bitmap = new BitmapBits(193, 193);
			bitmap.DrawCircle(6, 96, 96, 96); // Outer circle
			bitmap.DrawCircle(6, 96, 96, 72); // Inner circle
			debug[1] = new Sprite(bitmap, -96, -96);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which angle this platform should start at.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Bottom Right", 1 },
					{ "Bottom Left", 2 },
					{ "Left", 3 },
					{ "Top Left", 4 },
					{ "Top Right", 5 },
					{ "Right (Up Lean)", 6 }, // the original plan was to not have 6/7 since they're like two degrees off from r/br, but then 7 is used once, so..
					{ "Bottom Right (Up Lean)", 7 }
				},
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | (int)value));
			
			properties[1] = new PropertySpec("Speed", typeof(int), "Extended",
				"What speed this Platform should spin at. Positive values are clockwise, negative values are counter-clockwise.", null,
				(obj) => {
						int speed = (obj.PropertyValue >> 3) & 0x0f;
						if (speed >= 8)
							speed = -(speed - 8);
						return speed;
					},
				(obj, value) => {
						int speed = Math.Min(Math.Max((int)value, -7), 7);
						if (speed < 0)
							speed = (-speed + 8);
						
						obj.PropertyValue = (byte)((obj.PropertyValue & ~0x78) | (speed << 3));
					}
				);
			
			properties[2] = new PropertySpec("Spiked Ball", typeof(bool), "Extended",
				"If this object should be a Spiked Ball, as opposed to a Platform.", null,
				(obj) => (obj.PropertyValue & 0x80) == 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0x00)));
			
			properties[3] = new PropertySpec("Dynamic", typeof(bool), "Extended",
				"If this platform's radius should expand and contract.", null,
				(obj) => (((V4ObjectEntry)obj).State == 1),
				(obj, value) => ((V4ObjectEntry)obj).State = ((bool)value ? 1 : 0));
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
			// yeahhhh this is kinda janky, but if it works it works...
			
			List<Sprite> sprs = new List<Sprite>();
			
			int length = (((V4ObjectEntry)obj).State == 1) ? 5 : 4;
			double angle = ((obj.PropertyValue & 7) * 0x2a)/128.0 * Math.PI;
			// double amplitude = (((V4ObjectEntry)obj).State == 1) ? 0.75 : 1.0;
			
			for (int i = 0; i <= length; i++)
			{
				int frame = (i < length) ? 3 : ((obj.PropertyValue & 0x80) == 0x80) ? 5 : 4;
				sprs.Add(new Sprite(sprites[frame], (int)(Math.Cos(angle) * ((i+1) * 16)), (int)(Math.Sin(angle) * ((i+1) * 16))));
			}
			
			sprs.Add(new Sprite(sprites[2]));
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(((V4ObjectEntry)obj).State == 1) ? 1 : 0];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int length = ((((V4ObjectEntry)obj).State == 1) ? 5 : 4) + 1; // + 1 cause we're looking at the spike ball/platform, not the last chain
			double angle = ((obj.PropertyValue & 7) * 0x2a)/128.0 * Math.PI;
			
			double xoffset = Math.Cos(angle) * (length * 16);
			double yoffset = Math.Sin(angle) * (length * 16);
			
			int frame = ((obj.PropertyValue & 0x80) == 0x80) ? 5 : 4;
			Rectangle bounds = sprites[frame].Bounds;
			bounds.Offset(obj.X + (int)xoffset, obj.Y + (int)yoffset);
			return bounds;
		}
	}
}