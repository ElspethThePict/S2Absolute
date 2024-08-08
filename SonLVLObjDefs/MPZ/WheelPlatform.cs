using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class WheelPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[5];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			
			Sprite[] frames = new Sprite[4];
			frames[0] = new Sprite(sheet.GetSection(479, 182, 32, 24), -16, -12);
			frames[1] = new Sprite(sheet.GetSection(479, 133, 32, 24), -16, -12);
			frames[2] = new Sprite(sheet.GetSection(349, 165, 80, 16), -40, -8);
			frames[3] = new Sprite(sheet.GetSection(319, 207, 192, 48), -96, -24);
			
			sprites[4] = new Sprite(frames[3], 
				new Sprite(frames[2], -136, 12),
				new Sprite(frames[0], -192, 12),
				new Sprite(frames[2],  136, 12),
				new Sprite(frames[1],  192, 12));
			
			int radius = 56;
			BitmapBits overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(6, radius, radius, radius); // LevelData.ColorWhite
			debug = new Sprite(overlay, -radius - 192, -radius + 12);
			debug = new Sprite(debug, new Sprite(debug, true, false));
			
			sprites[0] = new Sprite(sprites[4], -radius, 0);
			sprites[1] = new Sprite(sprites[4],  radius, 0);
			sprites[2] = new Sprite(sprites[4], 0,  radius);
			sprites[3] = new Sprite(sprites[4], 0, -radius);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"The angle from which the Platform will start.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Bottom", 2 },
					{ "Right", 1 },
					{ "Top", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2, 1, 3}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] directions = {"Left", "Right", "Bottom", "Top"};
			return "Start From " + directions[subtype & 3];
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}