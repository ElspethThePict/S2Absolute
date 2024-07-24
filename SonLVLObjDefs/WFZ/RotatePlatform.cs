using SonicRetro.SonLVL.API;
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
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform. Note that the sprite's size doesn't match its hitbox, this is an issue within the game itself.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => ((obj.PropertyValue & 0x0F) == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0F) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Starting Position", typeof(int), "Extended",
				"The position (or angle) from where the Platform will start.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },    // names made for counter-clockwise (default) - they get flipped when the plat is moving clockwise
					{ "Down", 0x10 }, // i can't really think of a good solution around this, so i hope leaving it like that is fine...
					{ "Right", 0x20 },
					{ "Up", 0x30 }
				},
				(obj) => (obj.PropertyValue & 0x30),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x30) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction in which the Platform moves.", null, new Dictionary<string, int>
				{
					{ "Counter-clockwise", 0 },
					{ "Clockwise", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x40) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[((subtype & 0x0F) == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int radius = ((obj.PropertyValue & 0x40) != 0) ? -64 : 64, x = 0, y = 0;
			
			if ((obj.PropertyValue & 0x30) == 0x30)
			{
				y = -radius;
			}
			else if ((obj.PropertyValue & 0x20) == 0x20)
			{
				x = radius;
			}
			else if ((obj.PropertyValue & 0x10) == 0x10)
			{
				y = radius;
			}
			else
			{
				x = -radius;
			}
			
			return new Sprite(SubtypeImage(obj.PropertyValue), x, y);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}