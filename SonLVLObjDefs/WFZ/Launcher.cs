using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class Launcher : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(152, 74, 32, 16), -16, -31);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(445, 403, 32, 16), -16, -31);
			}
			
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How many pixels this Launcher should push the player before launching them. Note that actual launch velocity is unaffected by this value.", null,
				(obj) => obj.PropertyValue << 4,
				(obj, value) => obj.PropertyValue = (byte)((int)value >> 4));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Launcher should launch the player.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone) ? 0 : 1,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			return sprites[(((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone) ? 0 : 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int distance = (obj.PropertyValue << 4);
			BitmapBits bitmap = new BitmapBits(distance + 1, 2);
			bitmap.DrawLine(6, 0, 0, distance, 0); // LevelData.ColorWhite
			return new Sprite(bitmap, distance * ((((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone) ? -1 : 0), -26);
		}
	}
}