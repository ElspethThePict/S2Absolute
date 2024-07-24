using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.MCZ
{
	class SwingPlatform : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(152, 131, 23, 33), -12, -12); // post
			sprites[1] = new Sprite(sheet.GetSection(135, 148, 16, 16), -8, -8); // chain
			sprites[2] = new Sprite(sheet.GetSection(141, 165, 48, 16), -24, -8);  // platform
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains the Platform should hang off of.", null,
				(obj) => (Math.Max((int)obj.PropertyValue, 1) - 1) & 7,
				(obj, value) => obj.PropertyValue = (byte)((((Math.Max(obj.PropertyValue, (byte)1) - 1) & ~7) | (int)value) + 1));
			
			properties[1] = new PropertySpec("Swing Direction", typeof(int), "Extended",
				"Which direction the Platform should swing in.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 8 }
				},
				(obj) => (Math.Max(obj.PropertyValue, (byte)1) - 1) & 8,
				(obj, value) => obj.PropertyValue = (byte)((((Math.Max(obj.PropertyValue, (byte)1) - 1) & ~8) | (int)value) + 1));
			
			properties[2] = new PropertySpec("Inverted", typeof(bool), "Extended",
				"If the Swinging Platform's movement should be inverted, compared to other Swing Platforms.", null,
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)((bool)value ? 1 : 0)); // could be more direct instead of bool>int>Direction but the whole class name is p long, so..
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x16; }
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
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			int length = ((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7);
			int py = 16;
			for (int i = 0; i < length + 1; i++)
			{
				sprs.Add(new Sprite(sprites[1], 0, (py+=16)-16));
			}
			
			sprs.Add(new Sprite(sprites[0]));
			sprs.Add(new Sprite(sprites[2], 0, py-8));
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7) + 1) * 16 + 8;
			
			BitmapBits overlay = new BitmapBits(length + 1, length + 1);
			overlay.DrawCircle(6, length, 0, length); // LevelData.ColorWhite
			if ((((Math.Max(obj.PropertyValue, (byte)1) - 1) & 8) == 8) != (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)))
			{
				overlay.Flip(true, false);
				length = 0;
			}
			return new Sprite(overlay, -length, 0);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int length = (((Math.Max(obj.PropertyValue, (byte)1) - 1) & 7) + 1);
			
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X, obj.Y + (length * 16) + 8);
			return bounds;
		}
	}
}