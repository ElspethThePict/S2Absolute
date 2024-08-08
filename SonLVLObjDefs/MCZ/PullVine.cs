using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class PullVine : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(232, 176, 24, 80), -12, -40);
			sprites[1] = new Sprite(sheet.GetSection(232, 0, 24, 256), -12, -40);
			
			BitmapBits overlay = new BitmapBits(2, 177);
			overlay.DrawLine(6, 0, 0, 0, 176); // LevelData.ColorWhite
			debug = new Sprite(overlay, 0, 40);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Vine should pull the player.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 1 }
				},
				(obj) => ((obj.PropertyValue == 0) ? 0 : 1),
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
			return (subtype == 0) ? "Fall Downwards" : "Rise Upwards";
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug; // range is same between both variants btw, only direction changes
		}
	}
}