using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class Lift : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private readonly Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(102, 1, 56, 90), -28, -63);
				frames[1] = new Sprite(sheet.GetSection(109, 212, 64, 21), -32, 27);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(73, 848, 56, 90), -28, -63);
				frames[1] = new Sprite(sheet.GetSection(1, 953, 64, 21), -32, 27);
			}
			
			sprites[0] = new Sprite(frames);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far this Lift will go, in pixels.", null,
				(obj) => obj.PropertyValue << 4,
				(obj, value) => obj.PropertyValue = (byte)((int)value >> 4));
			
			properties[1] = new PropertySpec("Travel Direction", typeof(int), "Extended",
				"Which way the Lift will go.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x08, 0x10, 0x18, 0x20, 0x28}); } // it can be any value but let's give some starters, if only we could put direction in here too..
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x10; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return "Travel " + (subtype << 4) + " Pixels";
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
			return sprites[(((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int dist = obj.PropertyValue << 3;
			
			int sx = dist * 2;
			int sy = dist * 1;
			
			var bitmap = new BitmapBits(sx + 1, sy + 1);
			bitmap.DrawLine(6, 0, 0, sx, sy); // LevelData.ColorWhite
			bitmap.Flip((((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)), false);
			return new Sprite(bitmap, (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? -sx : 0, 0);
		}
	}
}