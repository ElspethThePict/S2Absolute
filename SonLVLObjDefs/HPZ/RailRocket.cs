using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HPZ
{
	class RailRocket : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private readonly Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("HPZ/Objects.gif");
			Sprite[] frames = new Sprite[4];
			frames[0] = new Sprite(sheet.GetSection(66, 168, 40, 54), -20, -27);
			frames[1] = new Sprite(sheet.GetSection(198, 50, 6, 6), -3, -3);
			frames[2] = new Sprite(sheet.GetSection(193, 57, 16, 16), -8, -3);
			
			List<Sprite> sprs = new List<Sprite>();
			
			for (int i = 0; i <= 7; i++)
				sprs.Add(new Sprite(frames[(i == 7) ? 2 : 1], 0, ((i+1) * 6)));
			
			frames[3] = new Sprite(sprs.ToArray());
			
			sprites[0] = new Sprite(frames[3], frames[0]);
			sprites[1] = new Sprite(frames[3], new Sprite(frames[0], true, false));
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far this Rocket will go, in pixels.", null,
				(obj) => ((obj.PropertyValue & 0x7f) == 0) ? 256 : ((obj.PropertyValue & 0x7f) << 5),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | ((int)value >> 5)));
			
			properties[1] = new PropertySpec("Travel Direction", typeof(int), "Extended",
				"Which way the Lift will travel in.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x80 }
				},
				(obj) => (obj.PropertyValue & 0x80),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((int)value)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x0c, 0x8c}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x0c; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 0x80) ? "Travel Left" : "Travel Right";
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
			return sprites[(obj.PropertyValue >> 7) & 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int sy = ((obj.PropertyValue & 0x7f) == 0) ? 256 : ((obj.PropertyValue & 0x7f) << 5);
			int sx = (sy / 8) * 4;
			
			BitmapBits bitmap = new BitmapBits(sx + 1, sy + 1);
			bitmap.DrawLine(6, 0, 0, sx, sy); // LevelData.ColorWhite
			bitmap.Flip(obj.PropertyValue >= 0x80, false);
			Sprite debug = new Sprite(bitmap, (obj.PropertyValue <= 0x7f) ? -sx : 0, -sy);
			debug = new Sprite(new Sprite(debug, -1, 0), debug, new Sprite(debug, 1, 0)); // let's make the line a bit thicker, tried an arrow out but this feels better
			return debug;
		}
	}
}