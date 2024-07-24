using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class Staircase : ObjectDefinition
	{
		private Sprite sprite;
		private readonly Sprite[,] debug = new Sprite[2, 2]; // subtype, dir
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects3.gif").GetSection(1, 62, 128, 32), -16, -16);
			
			BitmapBits bitmap = new BitmapBits(129, 129);
			bitmap.DrawRectangle(6, 0, 96, 31, 31); // bottom right
			bitmap.DrawRectangle(6, 32, 64, 31, 31); // middle, bottom left
			bitmap.DrawRectangle(6, 64, 32, 31, 31); // middle, top right
			bitmap.DrawRectangle(6, 96, 0, 31, 31); // top right
			
			// down, no flip
			debug[0, 0] = new Sprite(bitmap, -16, -16);
			
			// up, flip
			debug[1, 1] = new Sprite(bitmap, -16, -112);
			
			// down, flip
			debug[0, 1] = new Sprite(debug[1, 1], false, true);
			
			// up, flip
			debug[1, 0] = new Sprite(debug[0, 0], false, true);
			
			properties[0] = new PropertySpec("Extend", typeof(int), "Extended",
				"Which way this staircase should extend upon contact.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 4 } // and idk why this is 4 in the scene, any non 0/2 values work fine but may as well
					// { "Blank", 2 } // ignoring this because it looks kinda useless, it doesn't do anything so may as well just use a blank obj at that point
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 4, // we turn 2's into 0's since 2's aren't accessible
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction the Staircase will expand.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => ((int)(((V4ObjectEntry)obj).Direction) == 0) ? 0 : 1,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 4}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Open Downwards" : "Open Upwards";
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
			return debug[(obj.PropertyValue == 0) ? 0 : 1, (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone) ? 0 : 1];
		}
	}
}