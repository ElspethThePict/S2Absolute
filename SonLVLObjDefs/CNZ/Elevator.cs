using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Elevator : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(193, 34, 32, 16), -16, -8);
			BitmapBits bitmap = new BitmapBits(32, 16);
			bitmap.DrawRectangle(6, 0, 0, 31, 15); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -16, -8);
			
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
			get { return new ReadOnlyCollection<byte>(new byte[] {0x20, 0xA0}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x20; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 0x80) ? "Start From Bottom" : "Start From Top";
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
			
			return new Sprite(sprite, 0, -dist);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int dist = (obj.PropertyValue & 0x7f) << 2;
			BitmapBits bitmap = new BitmapBits(2, (2 * dist) + 1);
			bitmap.DrawLine(6, 0, 0, 0, (2 * dist)); // LevelData.ColorWhite
			
			if ((obj.PropertyValue & 0x80) == 0)
				dist *= -1;
			
			return new Sprite(new Sprite(bitmap, 0, -Math.Abs(dist)), new Sprite(debug, 0, dist));
		}
	}
}