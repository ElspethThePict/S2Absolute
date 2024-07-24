using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class RingPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[6];
			
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Items.gif").GetSection(1, 1, 16, 16), -8, -8);
			for (int i = 1; i < 6; i++)
				sprites[i] = new Sprite(sprites[0], 0, i * 16);
			
			sprite = new Sprite(sprites);
			
			BitmapBits bitmap = new BitmapBits(161, 161);
			bitmap.DrawCircle(6, 80, 80, 80); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -80, -80);
			
			// not sure if this was a value intended to be set from editor or not, but hey if it works then it's fine
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Rings should rotate.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone)) ? 0 : 1,
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
			return debug;
		}
	}
}