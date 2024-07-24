using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class YellowSpring : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[4];
		private Sprite[] sprites = new Sprite[8];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			sprites[0] = new Sprite(sheet.GetSection(52, 17, 32, 16), -16, -8);
			sprites[1] = new Sprite(sheet.GetSection(101, 1, 16, 32), -8, -16);
			sprites[2] = new Sprite(sheet.GetSection(134, 67, 16, 32), -8, -16);
			sprites[3] = new Sprite(sheet.GetSection(157, 132, 32, 16), -16, -8);
			sprites[4] = new Sprite(sheet.GetSection(118, 34, 32, 32), -16, -16);
			sprites[5] = new Sprite(sheet.GetSection(157, 83, 32, 32), -16, -16);
			sprites[6] = new Sprite(sheet.GetSection(118, 34, 32, 32), -16, -16);
			sprites[7] = new Sprite(sheet.GetSection(157, 83, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Spring should face.", null, new Dictionary<string, int>
				{
					{ "Up", 0 },
					{ "Right", 1 },
					{ "Left", 2 },
					{ "Down", 3 },
					{ "Up Right", 4 },
					{ "Up Left", 5 },
					{ "Down Right", 6 },
					{ "Down Left", 7 }
				},
				(obj) => (obj.PropertyValue & 0x7f),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | (int)value));;
			
			properties[1] = new PropertySpec("Twirl", typeof(int), "Extended",
				"If this Spring should trigger the Twirl animation upon launch. Only affects upwards springs.", null,
				(obj) => (obj.PropertyValue >= 0x80),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0x00)));
			
			properties[2] = new PropertySpec("Override XVel", typeof(int), "Extended",
				"The override x velocity this spring should use. Up right springs only.", null,
				(obj) => ((V4ObjectEntry)obj).Value0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value));
			
			properties[3] = new PropertySpec("Override YVel", typeof(int), "Extended",
				"The override y velocity this spring should use. Up right springs only.", null,
				(obj) => ((V4ObjectEntry)obj).Value1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = ((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 0x80, 0x84, 0x85}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] directions = {"Upwards", "Right", "Left", "Down", "Up Right", "Up Left", "Down Right", "Down Left"};
			string name = directions[subtype & 7];
			if (subtype >= 0x80) name += " (Twirl)";
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 7];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 7];
		}
	}
}