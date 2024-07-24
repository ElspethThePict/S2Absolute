using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class TargetBumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[5];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[3];
			BitmapBits sheet = LevelData.GetSpriteSheet("CNZ/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(1, 166, 32, 12), -16, -6);
			frames[1] = new Sprite(sheet.GetSection(1, 229, 24, 24), -12, -12);
			frames[2] = new Sprite(sheet.GetSection(60, 140, 12, 32), -6, -16);
			
			sprites[0] = new Sprite(frames[0]);
			sprites[1] = new Sprite(frames[1]);
			sprites[2] = new Sprite(frames[1], true, false);
			sprites[3] = new Sprite(frames[2]);
			sprites[4] = new Sprite(frames[2], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Bumper is facing.", null, new Dictionary<string, int>
				{
					{ "Vertical", 0 },
					{ "Up Right", 1 },
					{ "Up Left", 2 },
					{ "Right", 3 },
					{ "Left", 4 }
				},
				(obj) => obj.PropertyValue & 0x3f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x3f) | (int)value));
			
			properties[1] = new PropertySpec("Combo", typeof(int), "Extended",
				"Which other objects are needed for this Bumper's combo reward.", null, new Dictionary<string, int>
				{
					{ "obj[+1] & obj[+2]", 0x00 },
					{ "obj[-1] & obj[+1]", 0x40 },
					{ "obj[-1] & obj[-2]", 0x80 },
					{ "Standalone", 0xc0 }
				},
				(obj) => obj.PropertyValue & 0xc0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xc0) | (int)value));
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
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((obj.PropertyValue & 0x3f) > 4) ? 0 : (obj.PropertyValue & 0x3f)];
		}
		
		// For the combo variable, maybe we could draw lines to the connecting combo objects but i think that'd be really cluttered..
	}
}