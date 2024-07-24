using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class HPlatform : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(141, 165, 48, 16), -24, -8);
			sprites[0] = new Sprite(sprites[2],  104, 0);
			sprites[1] = new Sprite(sprites[2], -104, 0);
			
			BitmapBits overlay = new BitmapBits(209, 2);
			overlay.DrawLine(6, 0, 0, 208, 0); // LevelData.ColorWhite
			debug = new Sprite(overlay, -104, 0);
			
			properties[0] = new PropertySpec("Start From", typeof(bool), "Extended",
				"If this Platform's movement cycle should be flipped or not.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
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
			return (subtype == 0) ? "Start From Right" : "Start From Left";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}