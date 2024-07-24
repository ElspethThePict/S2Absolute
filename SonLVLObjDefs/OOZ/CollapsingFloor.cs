using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class CFloor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(1, 207, 128, 48), -64, -24);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			BitmapBits bitmap = new BitmapBits(128, 48);
			bitmap.DrawRectangle(6, 0, 0, 127, 47); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -64, -24);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the object is facing.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 0 : 1,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
			
			// i don't think this is ever even used? the hitbox doesn't really line up either, but may as well..
			properties[1] = new PropertySpec("Collision", typeof(int), "Extended",
				"What collision type this platform should have.", null, new Dictionary<string, int>
				{
					{ "Top Solid", 0 },
					{ "All Solid", 2 },
				},
				(obj) => (obj.PropertyValue < 2) ? 0 : 2,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 2) ? "Platform" : "Solid";
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
			return sprites[(((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 0 : 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}