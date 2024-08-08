using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Snail : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(44, 27, 32, 33), -16, -16);
				frames[1] = new Sprite(sheet.GetSection(110, 44, 16, 16), -23, -6);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(44, 27, 32, 33), -16, -16); // using broken frames
				frames[1] = new Sprite(sheet.GetSection(110, 44, 16, 16), -23, -6);
			}
			
			sprites[0] = new Sprite(frames);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction the Snail will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1, // in-game is a bit iffy, startup only looks at first bit but oob reset looks at entire number (== 0) - we're just gonna stick with 1st bit here
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
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
			return sprites[subtype & 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1];
		}
	}
}