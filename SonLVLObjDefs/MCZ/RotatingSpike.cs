using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.MCZ
{
	class RotatingSpike : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(135, 148, 16, 16), -8, -8); // post (same frame as chain)
			sprites[1] = new Sprite(sheet.GetSection(135, 148, 16, 16), -8, -8); // chain
			sprites[2] = new Sprite(sheet.GetSection(103, 0, 32, 32), -16, -16);  // spike ball
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains the Spike should hang off of.", null,
				(obj) => obj.PropertyValue & 0x0f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | ((int)value & 0x0f)));
			
			properties[1] = new PropertySpec("Speed", typeof(int), "Extended",
				"The speed of this Spike Ball. Positive values are clockwise, negative values are counter-clockwise.", null,
				(obj) => {
						int speed = (obj.PropertyValue & 0xf0) >> 4;
						if (speed >= 8)
							speed -= 16;
						return speed;
					},
				(obj, value) => {
						int speed = Math.Min(Math.Max((int)value, -8), 7);
						if (speed < 0)
							speed += 16;
						
						obj.PropertyValue = (byte)((obj.PropertyValue & ~0xf0) | (speed << 4));
					}
				);
			
			properties[2] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction this Rotating Spike should start from.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Bottom", 1 },
					{ "Left", 2 },
					{ "Top", 3 }
				},
				(obj) => (int)(((V4ObjectEntry)obj).Direction),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x34, 0x36, 0xd3, 0xd6}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x36; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype & 0x0f) + " chains" + ((subtype < 0x80) ? " (Clockwise)" : " (Counter-Clockwise)");
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
			
			int length = (obj.PropertyValue & 0x0f);
			double angle = (int)(((V4ObjectEntry)obj).Direction) * (Math.PI / 2.0);
			
			for (int i = 0; i < length + 1; i++)
			{
				int frame = (i == 0) ? 0 : (i == length) ? 2 : 1;
				sprs.Add(new Sprite(sprites[frame], (int)(Math.Cos(angle) * (i * 16)), (int)(Math.Sin(angle) * (i * 16))));
			}
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = (obj.PropertyValue & 0x0f) * 16;
			
			BitmapBits bitmap = new BitmapBits(2 * length + 1, 2 * length + 1);
			bitmap.DrawCircle(6, length, length, length); // LevelData.ColorWhite
			return new Sprite(bitmap, -length, -length);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int length = obj.PropertyValue & 0x0f;
			double angle = (int)(((V4ObjectEntry)obj).Direction) * (Math.PI / 2.0);
			
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X + (int)(Math.Cos(angle) * (length * 16)), obj.Y + (int)(Math.Sin(angle) * (length * 16)));
			return bounds;
		}
	}
}