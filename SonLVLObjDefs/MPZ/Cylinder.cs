using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class Cylinder : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			// Small hitbox
			BitmapBits bitmap = new BitmapBits(384, 128);
			bitmap.DrawRectangle(6, 0, 0, 383, 127); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -(384 / 2), -64);
			
			// Large hitbox
			bitmap = new BitmapBits(768, 128);
			bitmap.DrawRectangle(6, 0, 0, 767, 127); // LevelData.ColorWhite
			debug[1] = new Sprite(bitmap, -(768 / 2), -64);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the Corkscrew Cylinder.", null, new Dictionary<string, int>
				{
					{ "Small", 0 },
					{ "Large", 1 }
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
			return (subtype == 0) ? "Small Cylinder" : "Large Cylinder";
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
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}