using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[] sprites = new Sprite[2];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(381, 178, 48, 24), -24, -16);
				sprites[1] = new Sprite(sheet.GetSection(1, 146, 64, 24), -32, -16);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(529, 999, 48, 24), -24, -16);
				sprites[1] = new Sprite(sheet.GetSection(464, 999, 64, 24), -32, -16);
			}
			
			int radius = 64;
			BitmapBits overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(6, radius, radius, radius); // LevelData.ColorWhite
			debug = new Sprite(overlay, -radius, -radius - 4);
			
			// hitbox size doesn't match with sprite size (small sprite has large hitbox, large sprite has small hitbox), but let's stick with what we see
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
				{
					{ "Small", 0 },
					{ "Large", 1 }
				},
				(obj) => ((obj.PropertyValue & 0x0F) == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0F) | (int)value));
			
			properties[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"The angle from which the Platform will start.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Bottom", 0x10 },
					{ "Right", 0x20 },
					{ "Top", 0x30 }
				},
				(obj) => (obj.PropertyValue & 0x30) ^ (((obj.PropertyValue & 0x40) == 0x40) ? 0x20 : 0x00), // (we need to flip 'em around, depending on the direction of the platform)
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x30) | ((int)value ^ (((obj.PropertyValue & 0x40) == 0x40) ? 0x20 : 0x00))));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction in which the Platform moves.", null, new Dictionary<string, int>
				{
					{ "Counter-clockwise", 0 },
					{ "Clockwise", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => {
						int val = (int)value;
						if ((obj.PropertyValue & 0x40) != val) // if we're switching dir, let's preserve starting position too
							obj.PropertyValue ^= 0x20;
						
						obj.PropertyValue = (byte)((obj.PropertyValue & ~0x40) | val);
					}
				);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x60, 0x70, 0x40, 0x50, 0x00, 0x10, 0x20, 0x30}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] directions = {"Left", "Bottom", "Right", "Top"};
			string name = "Start From " + directions[((subtype & 0x30) ^ (((subtype & 0x40) == 0x40) ? 0x20 : 0x00)) >> 4];
			name += (((subtype & 0x40) == 0x40) ? " (Clockwise)" : " (Counter-clockwise)");
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[((subtype & 0x0F) == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int radius = ((obj.PropertyValue & 0x40) == 0) ? -64 : 64;
			double angle = ((obj.PropertyValue & 0x30) >> 4) * -Math.PI / 2;
			
			return new Sprite(sprites[((obj.PropertyValue & 0x0F) == 0) ? 0 : 1], (int)(Math.Cos(angle) * radius), (int)(Math.Sin(angle) * radius));
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}