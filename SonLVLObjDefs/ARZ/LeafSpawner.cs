using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class LeafSpawner : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of this Leaf Spawner. Increases in powers of 2, based on this number.", null,
				(obj) => obj.PropertyValue + 1,
				(obj, value) => obj.PropertyValue = (byte)(Math.Max((int)value - 1, 0)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (32 << (subtype + 1)) + " pixels wide";
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
			int width = (32 << (obj.PropertyValue + 1));
			BitmapBits debug = new BitmapBits(width, 65);
			debug.DrawRectangle(6, 0, 0, width-1, 63); // LevelData.ColorWhite
			return new Sprite(debug, -(width/2), -32);
		}
	}
}