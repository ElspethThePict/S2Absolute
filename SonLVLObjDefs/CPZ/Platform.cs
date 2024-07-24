using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class HPlatform : CPZ.Platform
	{
		public override Point offset { get { return new Point(-96, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VPlatform : CPZ.Platform
	{
		public override Point offset { get { return new Point(0, -128); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	class VPlatform2 : CPZ.Platform
	{
		public override Point offset { get { return new Point(0, -191); } }
		public override Dictionary<string, int> names { get { return null; } }
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
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
		
		public abstract Point offset { get; }
		public abstract Dictionary<string, int> names { get; }
		
		public virtual PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[2];
			props[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			props[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, names,
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)((int)value));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("CPZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(136, 155, 64, 27), -32, -16);
			sprites[1] = new Sprite(sheet.GetSection(136, 183, 48, 26), -24, -16);
			
			sprites[2] = new Sprite(sprites[0], offset);
			sprites[3] = new Sprite(sprites[1], offset);
			
			BitmapBits overlay = new BitmapBits(-offset.X + 1, -offset.Y + 1);
			overlay.DrawLine(6, 0, 0, -offset.X, -offset.Y); // LevelData.ColorWhite
			debug = new Sprite(overlay, offset.X, offset.Y);
			
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
			return (subtype > 0) ? "Small Platform" : "Large Platform";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 0) ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((obj.PropertyValue > 0) ? 1 : 0) | (((((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 2 : 0)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}