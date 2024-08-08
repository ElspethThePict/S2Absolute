using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Grabber : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[3];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("CPZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(5, 74, 40, 32), -27, -8);
				frames[1] = new Sprite(sheet.GetSection(46, 74, 8, 8), -4, -16);
				frames[2] = new Sprite(sheet.GetSection(5, 140, 23, 16), -6, 8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(131, 280, 40, 32), -27, -8);
				frames[1] = new Sprite(sheet.GetSection(145, 334, 8, 8), -4, -16);
				frames[2] = new Sprite(sheet.GetSection(178, 313, 30, 15), -6, 8);
			}
			
			sprites[0] = new Sprite(frames);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Spiny will move.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			return (subtype == 0) ? "Facing Left" : "Facing Right";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}
