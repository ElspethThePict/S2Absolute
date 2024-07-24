using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class Spikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[] sprites = new Sprite[4];
		private readonly Sprite[] debug   = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items2.gif");
			sprites[0] = new Sprite(sheet.GetSection(182, 99, 32, 32), -16, -16);
			sprites[1] = new Sprite(sheet.GetSection(182, 132, 32, 32), -16, -16);
			sprites[2] = new Sprite(sheet.GetSection(215, 132, 32, 32), -16, -16);
			sprites[3] = new Sprite(sheet.GetSection(215, 99, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Spikes are facing.", null, new Dictionary<string, int>
				{
					{ "Up", 0 },
					{ "Right", 1 },
					{ "Left", 2 },
					{ "Down", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | (int)value));
			
			properties[1] = new PropertySpec("Moving", typeof(bool), "Extended",
				"If the Spikes should peek in and out.", null,
				(obj) => obj.PropertyValue > 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 3) | ((bool)value ? 0x80 : 0))); // see note in Subtypes - the bit used for moving types isn't consistent but let's use the top bit
			
			properties[2] = new PropertySpec("Parent Offset", typeof(int), "Extended",
				"The object slot offset of this Spikes' parent, used for series of vertically retracting spikes.", null,
				(obj) => (((V4ObjectEntry)obj).State != 5) ? 0 : ((V4ObjectEntry)obj).Value2,
				(obj, value) =>
				{
					((V4ObjectEntry)obj).Value2 = (int)value;
					((V4ObjectEntry)obj).State = ((int)value == 0) ? 0 : 5; // if non-zero value, then set state to 5, otherwise reset it
				});
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -16,  16);
			debug[1] = new Sprite(bitmap, -48, -16);
			debug[2] = new Sprite(bitmap,  16, -16);
			debug[3] = new Sprite(bitmap, -16, -48);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			// moving spikes are iffy, in ported zones they use the top bit while in new scenes (hpz, probably some origins missions) they use the 3rd bit
			// since most levels use the top bit, let's stick with that
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 0x80, 0x81, 0x82, 0x83}); }
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] directions = {"Up", "Right", "Left", "Down"};
			string name = "Facing " + directions[subtype & 3];
			
			if (subtype > 3) name += " (Moving)";
			
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 3) ? debug[obj.PropertyValue & 3] : null;
		}
	}
}