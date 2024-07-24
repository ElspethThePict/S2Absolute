using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class BumpingPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private readonly Sprite[] sprites = new Sprite[2];
		private readonly Sprite[] debug = new Sprite[4];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(6, 204, 48, 14), -24, -8);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(469, 692, 48, 14), -24, -8);
			}
			
			sprites[1] = new Sprite(new Sprite(sprites[0], -24, 0), new Sprite(sprites[0], 25, 0));

			BitmapBits overlay = new BitmapBits(1024, 2);
			
			overlay.DrawLine(6, 0, 0, 255, 0); // LevelData.ColorWhite
			debug[0] = new Sprite(overlay, -128, -2);
			
			overlay.DrawLine(6, 0, 0, 383, 0); // LevelData.ColorWhite
			debug[1] = new Sprite(overlay, -192, -2);
			
			overlay.DrawLine(6, 0, 0, 511, 0); // LevelData.ColorWhite
			debug[2] = new Sprite(overlay, -256, -2);

			overlay.DrawLine(6, 0, 0, 383, 0); // LevelData.ColorWhite
			debug[3] = new Sprite(overlay, -192, -2);
			
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
				"The way this platform moves.", null, new Dictionary<string, int>
				{
					{ "One platform, 256px", 0 },
					{ "Two platforms, 384px", 1 },
					{ "Two platforms, 512px", 2 },
					{ "One platform, 384px", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
				default:
					return "One platform, 256px";
				case 1:
					return "Two platforms, 384px";
				case 2:
					return "Two platforms, 512px";
				case 3:
					return "One platform, 384px";
			}
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
			return sprites[(obj.PropertyValue == 1 | obj.PropertyValue == 2) ? 1 : 0];
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 1 | obj.PropertyValue == 2) ? obj.PropertyValue : 0];
		}
	}
}
