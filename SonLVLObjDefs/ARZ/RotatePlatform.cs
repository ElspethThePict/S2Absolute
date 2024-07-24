using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private readonly Sprite[] sprites = new Sprite[3];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(160, 235, 30, 20), -15, -12);
			sprites[1] = new Sprite(sheet.GetSection(174, 218, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(126, 191, 64, 16), -32, -8);

			int radius = 64;
			BitmapBits overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(6, radius, radius, radius); // LevelData.ColorWhite
			debug = new Sprite(overlay, -radius, -radius - 4);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which angle this platform should start at.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Top Right", 5 },
					{ "Top Left", 8 },
					{ "Left", 1 },
					{ "Bottom Left", 4 },
					{ "Bottom Right", 9 }
				},
				(obj) => obj.PropertyValue & 0x0f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | (int)value));
			
			properties[1] = new PropertySpec("Speed", typeof(int), "Extended",
				"The speed of this platform. Positive values are clockwise, negative values are counter-clockwise.", null,
				(obj) => {
						int speed = (obj.PropertyValue & 0xf0) >> 4;
						if (speed >= 8)
							speed = -(speed - 8);
						return speed;
					},
				(obj, value) => {
						int speed = (int)value;
						if (speed < 0)
							speed = (-speed + 8);
						
						speed &= 0x0f;
						
						obj.PropertyValue = (byte)((obj.PropertyValue & ~0xf0) | (speed << 4));
					}
				);
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
			return sprites[2];
		}
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			
			int length = 3;
			int angle = (obj.PropertyValue & 3) << 15;
			
			if ((obj.PropertyValue & 4) == 4)
				angle += 0x5500;
			
			if ((obj.PropertyValue & 8) == 8)
				angle -= 0x5500;
			
			angle = (angle & 0x1ffff) >> 8;
			
			double angleF = angle/128.0 * Math.PI;
			
			// cos:
			// 0 - 0x100
			// 0x40 - 0
			
			for (int i = 0; i <= length; i++)
			{
				int frame = (i < length) ? 1 : 2;
				sprs.Add(new Sprite(sprites[frame], (int)(Math.Cos(angleF) * ((i+1) * 16)), (int)(Math.Sin(angleF) * ((i+1) * 16))));
			}
			
			sprs.Add(new Sprite(sprites[0]));
			
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int length = 3 + 1; // + 1 because we want the platform, not the last chain
			int angle = (obj.PropertyValue & 3) << 15;
			
			if ((obj.PropertyValue & 4) == 4)
				angle += 0x5500;
			
			if ((obj.PropertyValue & 8) == 8)
				angle -= 0x5500;
			
			angle = (angle & 0x1ffff) >> 8;
			
			double angleF = angle/128.0 * Math.PI;
			
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X + (int)(Math.Cos(angleF) * (length * 16)), obj.Y + (int)(Math.Sin(angleF) * (length * 16)));
			return bounds;
		}
	}
}
