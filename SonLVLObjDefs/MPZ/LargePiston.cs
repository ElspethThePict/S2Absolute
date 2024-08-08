using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class LargePiston : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(1, 135, 128, 120), -64, -12);
			
			sprites[0] = new Sprite(sprites[2], 0, 64); // (let's show it in its retracted position, looks better imo)
			sprites[1] = new Sprite(sprites[2], false, true);
			
			BitmapBits bitmap = new BitmapBits(128, 120);
			bitmap.DrawRectangle(6, 0, 0, 127, 119);
			debug[0] = new Sprite(bitmap, -64, -12);
			debug[1] = new Sprite(bitmap, -64, -108 + 64);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the piston is facing.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 2 }
				},
				(obj) => ((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY) ? 2 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
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
			get { return sprites[2]; }
		}
		
		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY) ? 1 : 0];
		}
	}
}