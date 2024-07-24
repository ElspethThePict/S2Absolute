using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class ConveyorBelt : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How long, in tiles, the Conveyor Belt is.", null, // maybe pixels would be cleaner? but it's gonna be even larger intervals then, as opposed to just being even numbers, so i'm not sure..
				(obj) => (obj.PropertyValue << 2),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Conveyor Belt should push the player.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
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
			if (obj.PropertyValue == 0) return null;
			int width = (obj.PropertyValue << 4) * 2;
			BitmapBits bitmap = new BitmapBits(width + 1, 21);
			bitmap.DrawRectangle(6, 0, 0, width, 20); // LevelData.ColorWhite
			return new Sprite(bitmap, -(width / 2), -20);
		}
	}
}