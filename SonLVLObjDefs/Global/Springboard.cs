using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class Springboard : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			// sprites support all four flips, but functionality-wise it only works with upright directions, not v flips
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Items.gif").GetSection(52, 160, 56, 16), -28, -24);
			sprites[1] = new Sprite(sprites[0], true, false);
			sprites[2] = new Sprite(sprites[0], false, true);
			sprites[3] = new Sprite(sprites[0], true, true);
			
			// (prop val is unused btw, there's some iffy parts in the script but that doesn't matter here-)
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Springboard is facing.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (int)((V4ObjectEntry)obj).Direction,
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(int)((V4ObjectEntry)obj).Direction];
		}
	}
}