using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class SpeedBooster : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(91, 25, 48, 16), -24, -8);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(474, 321, 48, 16), -24, -8);
			}
			
			properties[0] = new PropertySpec("Speed", typeof(int), "Extended",
				"The amount of speed the player gets after being launched.", null, new Dictionary<string, int>
				{
					{ "Fast", 0 },
					{ "Slow", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction the player should be launched in.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)((int)value));
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
			return (subtype == 0) ? "Fast Booster" : "Slow Booster";
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
	}
}