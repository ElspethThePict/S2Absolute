using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class GasBreakBlock : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(69, 141, 32, 32), -16, -16);
				sprites[1] = new Sprite(sheet.GetSection(69, 174, 32, 32), -16, -16);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(991, 843, 32, 32), -16, -16);
				sprites[1] = new Sprite(sheet.GetSection(991, 876, 32, 32), -16, -16);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way this object will launch the player.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)(int)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype & 1) == 0) ? "Upwards" : "Right";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1];
		}
	}
}