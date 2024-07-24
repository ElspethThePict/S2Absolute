using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class HPlatform : WFZ.Platform
	{
		public override Point GetOffset() { return new Point(-96, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(97, 2);
			overlay.DrawLine(6, 0, 0, 96, 0); // LevelData.ColorWhite
			return new Sprite(overlay, -96, 0);
		}
	}
	
	class HPlatform2 : WFZ.Platform
	{
		public override Point GetOffset() { return new Point(-64, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(65, 2);
			overlay.DrawLine(6, 0, 0, 64, 0); // LevelData.ColorWhite
			return new Sprite(overlay, -64, 0);
		}
	}
	
	class VPlatform : WFZ.Platform
	{
		public override Point GetOffset() { return new Point(0, -128); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 129);
			overlay.DrawLine(6, 0, 0, 0, 128); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -128);
		}
	}
	
	class VPlatform2 : WFZ.Platform
	{
		public override Point GetOffset() { return new Point(0, -191); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 192);
			overlay.DrawLine(6, 0, 0, 0, 191); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -191);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform. Note that the sprite's size doesn't match its hitbox, this is an issue within the game itself.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			// FlipY was prolly intended to do something, but it doesn't really work
			
			return props;
		}
	}
	
	abstract class Platform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[4];
		private Sprite debug;
		
		public virtual Point GetOffset() { return new Point(0, 0); }
		public virtual Sprite SetupDebugOverlay() { return null; }
		
		public virtual PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[2];
			props[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform. Note that the sprite's size doesn't match its hitbox, this is an issue within the game itself.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			props[1] = new PropertySpec("Flip Movement", typeof(bool), "Extended",
				"Whether or not this Platform's movement cycle should be flipped or not.", null,
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)((bool)value ? 1 : 0));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
			
			sprites[0] = new Sprite(sheet.GetSection(381, 178, 48, 24), -24, -16);
			sprites[1] = new Sprite(sheet.GetSection(1, 146, 64, 24), -32, -16);
			
			sprites[2] = new Sprite(sprites[0], GetOffset());
			sprites[3] = new Sprite(sprites[1], GetOffset());
			
			debug = SetupDebugOverlay();
			properties = SetupProperties();
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
			return (subtype == 0) ? "Large Platform" : "Small Platform";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min(subtype, (byte)1)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(Math.Min(obj.PropertyValue, (byte)1)) + (((((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 2 : 0)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}