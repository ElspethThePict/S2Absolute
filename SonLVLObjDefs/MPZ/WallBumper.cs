using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class WallBumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(93, 113, 16, 16), -8, -8);
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 8; i++)
			{
				sprs.Add(new Sprite(sprites[2], 0, -56 + (i * 16)));
			}
			
			sprites[0] = new Sprite(sprs); // 4 bumpers
			sprites[1] = new Sprite(new Sprite(sprites[0], 0, -64), new Sprite(sprites[0], 0, 64)); // double it to make 8 bumpers
			
			// First bit is unused, it seems?
			// it's set in the scene sometimes, but it doesn't do anything
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How large the Wall Bumper will be.", null, new Dictionary<string, int>
				{
					{ "4 Bumpers", 0 }, // 8 nodes
					{ "8 Bumpers", 0x10 } // 16 nodes
				},
				(obj) => obj.PropertyValue & 0x70,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x70) | (int)value));
			
			properties[1] = new PropertySpec("Bounce Direction", typeof(int), "Extended",
				"Which way Sonic will be bounced by this wall.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (int)((V4ObjectEntry)obj).Direction,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 0x11}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype & 0x70) == 0) ? "4 Bumpers" : "8 Bumpers";
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
			return sprites[((obj.PropertyValue & 0x70) == 0) ? 0 : 1];
		}
	}
}