using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class HorizontalDoor : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(383, 207, 64, 24), -32, -12);
			sprites[1] = new Sprite(sprites[0], -64, 0);
			
			BitmapBits bitmap = new BitmapBits(129, 25);
			bitmap.DrawRectangle(6, 0, 0, 65, 24); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -32 - 64, -12);
			debug[1] = new Sprite(bitmap, -32, -12);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Door should open.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction != (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			// it would be nice to have default subtypes.. but this object doesn't even use its property value, so
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 0 : 1)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[((((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 0 : 1)];
		}
	}
}