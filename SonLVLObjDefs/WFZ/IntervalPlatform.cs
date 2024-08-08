using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class TurretPlatform : WFZ.IntervalPlatform
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(130, 165, 64, 24), -32, -24);
		}
	}
	
	class TiltPlatformH : WFZ.IntervalPlatform
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(320, 99, 48, 8), -24, -4);
		}
	}
	
	class TiltPlatformV : WFZ.IntervalPlatform
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(311, 99, 8, 48), -4, -24);
		}
	}
	
	class TiltPlatformL : WFZ.IntervalPlatform
	{
		public override Sprite GetFrame()
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
			return new Sprite(new Sprite(sheet.GetSection(464, 0, 48, 256), -24, -112), new Sprite(sheet.GetSection(320, 99, 48, 8), -24, -4)); // in-game the laser is drawn above the platform, but in the editor let's show that the platform's still there (to avoid confusion with Large Laser)
		}
	}
	
	// TiltPlatformM doesn't use its prop val so we can leave it as a basic render
	
	abstract class IntervalPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			properties[0] = new PropertySpec("Interval Offset", typeof(int), "Extended",
				"The offset this object's interval should be from the global interval.", null, new Dictionary<string, int>
				{
					{ "0%", 0 },
					{ "6.25%", 1 },
					{ "12.5%", 2 },
					{ "18.75%", 3 },
					{ "25%", 4 },
					{ "31.25%", 5 },
					{ "37.5%", 6 },
					{ "43.75%", 7 },
					{ "50%", 8 },
					{ "56.25%", 9 },
					{ "62.5%", 10 },
					{ "68.75%", 11 },
					{ "75%", 12 },
					{ "81.25%", 13 },
					{ "87.5%", 14 },
					{ "93.75%", 15 }
				},
				(obj) => obj.PropertyValue >> 4,
				(obj, value) => obj.PropertyValue = (byte)((int)value << 4)); // (we're discarding the bottom four bits on purpose, having them set means the platform will never move)
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xa0, 0xb0, 0xc0, 0xd0, 0xe0, 0xf0}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype >> 4) + " Offset From Global Interval";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}