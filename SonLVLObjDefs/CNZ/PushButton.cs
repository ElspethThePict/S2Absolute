using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.CNZ
{
	class PushButton : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];

		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(219, 120, 32, 16), -16, -8);
			
			// so glad this object only uses 90 degree rotations, or else i have no idea what i would've done-
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawSprite(sprites[0], 16, 8);
			bitmap.Rotate(3);
			sprites[1] = new Sprite(bitmap, -24, -16);
			
			sprites[2] = new Sprite(sprites[0], 1, 0, false, true);
			
			sprites[3] = new Sprite(sprites[1], 0, 1, true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this button should face.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 1 },
					{ "Downwards", 2 },
					{ "Left", 3 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue];
		}
	}
}