using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class Elevator : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(127, 1, 128, 24), -64, -12);
			BitmapBits overlay = new BitmapBits(128, 24);
			overlay.DrawRectangle(6, 0, 0, 127, 23); // LevelData.ColorWhite
			debug = new Sprite(overlay, -64, -12);

			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far, in pixels, the Elevator will go.", null,
				(obj) => (obj.PropertyValue & 0x7f) << 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | ((int)value >> 3)));
			
			properties[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction the Elevator will start from.", null, new Dictionary<string, int>
				{
					{ "Bottom", 0 },
					{ "Top", 0x80 }
				},
				(obj) => obj.PropertyValue & 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 24; }
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
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int dist = (obj.PropertyValue & 0x7f) << 2;
			if ((obj.PropertyValue & 0x80) == 0)
				dist *= -1;
			
			return new Sprite(sprite, 0, -dist + 4);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int dist = (obj.PropertyValue & 0x7f) << 2;
			BitmapBits bitmap = new BitmapBits(2, (2 * dist) + 1);
			bitmap.DrawLine(6, 0, 0, 0, (2 * dist)); // LevelData.ColorWhite
			
			if ((obj.PropertyValue & 0x80) == 0)
				dist *= -1;
			
			return new Sprite(new Sprite(bitmap, 0, -Math.Abs(dist) + 4), new Sprite(debug, 0, dist + 4));
		}
	}
}