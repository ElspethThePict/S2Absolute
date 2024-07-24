using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class ButtonBridge : ObjectDefinition
	{
		private Sprite[,] sprites = new Sprite[2, 2]; // subtype, dir
		private Sprite[,] debug = new Sprite[2, 2]; // same as sprites
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(133, 126, 24, 24), -12, 8);
			frames[1] = new Sprite(sheet.GetSection(350, 182, 128, 24), -64, -12);
			
			sprites[0, 0] = new Sprite(new Sprite(frames[0], -76, 0), new Sprite(frames[1]));          // prop val 0, dir 0
			sprites[0, 1] = new Sprite(new Sprite(frames[0],  76, 0), new Sprite(frames[1]));          // prop val 0, dir 1
			
			sprites[1, 0] = new Sprite(new Sprite(frames[0], -76, 0), new Sprite(frames[1], -128, 0)); // prop val 1, dir 0
			sprites[1, 1] = new Sprite(new Sprite(frames[0],  76, 0), new Sprite(frames[1],  128, 0)); // prop val 1, dir 1
			
			BitmapBits bitmap = new BitmapBits(129, 25);
			bitmap.DrawRectangle(6, 0, 0, 128, 24); // LevelData.ColorWhite
			debug[0, 0] = new Sprite(bitmap, -64 - 128, -12); // prop val 0, dir 0
			debug[0, 1] = new Sprite(bitmap, -64 + 128, -12); // prop val 0, dir 1
			
			debug[1, 0] = new Sprite(bitmap, -64, -12); // prop val 1, dir 0
			debug[1, 1] = new Sprite(bitmap, -64, -12); // prop val 1, dir 1
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this bridge should behave upon button[+1] being pressed.", null, new Dictionary<string, int>
				{
					{ "Show On Activate", 0 },
					{ "Hide On Activate", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Bridge is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
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
			return (subtype == 1) ? "Show On Activation" : "Hide On Activation";
		}

		public override Sprite Image
		{
			get { return sprites[0, 0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype, 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((obj.PropertyValue == 1) ? 1 : 0), ((((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 1 : 0)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[((obj.PropertyValue == 1) ? 1 : 0), ((((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 1 : 0)];
		}
	}
}