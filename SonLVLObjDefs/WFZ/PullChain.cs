using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.WFZ
{
	class PullChain : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[4];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet;
			int sprX = 0, sprY = 0;
			
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
				sprX = 236;
				sprY = 176;
			}
			else
			{
				sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprX = 901;
				sprY = 358 + 176; // original game does this too, idk why
			}
			
																	    // y offset: offset + ypos offset + origin offset (sorry if it's kinda weird...)
			sprites[0] = new Sprite(sheet.GetSection(sprX, sprY + 32,  24, 48), -12, -8 - 32);
			sprites[1] = new Sprite(sheet.GetSection(sprX, sprY + 32,  24, 48), -12, -8 - 32);
			sprites[2] = new Sprite(sheet.GetSection(sprX, sprY - 145, 24, 225), -12, -185 + 145 - 17);
			sprites[3] = new Sprite(sheet.GetSection(sprX, sprY - 81,  24, 161), -12, -121 + 81 - 17);
			
			BitmapBits overlay = new BitmapBits(2, 161);
			overlay.DrawLine(6, 0, 0, 0, 160); // LevelData.ColorWhite
			debug[0] = new Sprite(overlay);
			
			overlay = new BitmapBits(2, 97);
			overlay.DrawLine(6, 0, 0, 0, 96); // LevelData.ColorWhite
			debug[1] = new Sprite(overlay);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far this Pull Chain should go.", null, new Dictionary<string, int>
				{
					{ "Far", 0 },
					{ "Short", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(bool), "Extended",
				"Which direction this Chain should pull the player in.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 0x10 }
				},
				(obj) => obj.PropertyValue & 0x10,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x10) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 0x10, 0x11}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string name = ((subtype & 1) == 0) ? "Long" : "Short";
			name += ((subtype & 0x10) == 0) ? " (Start Upwards)" : " (Start Downwards)"; // yeah the properties are which direction to go while this is which direction to start from, dunno if it's confuisng so maybe i'll change this later?
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype & 1) + ((subtype & 0x10) >> 3)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 1) + ((obj.PropertyValue & 0x10) >> 3)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 1];
		}
	}
}