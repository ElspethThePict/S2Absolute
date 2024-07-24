using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class SpikedLedge : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(1, 99, 128, 32), -64, -16);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Ledge is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			BitmapBits bitmap = new BitmapBits(129, 33);
			bitmap.DrawRectangle(6, 0, 0, 128, 32); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -192, -16);
			debug[1] = new Sprite(bitmap, 64, -16);
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
			return ((subtype & 1) == 1) ? "Facing Right" : "Facing Left";
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
			return sprites[obj.PropertyValue & 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 1];
		}
	}
}