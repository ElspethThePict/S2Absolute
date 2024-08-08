using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class FPlatform : MPZ.Platform
	{
		public override Sprite SetupDebugOverlay()
		{
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(1, 40);
			for (int i = 0; i < bitmap.Height; i += 8)
				bitmap.DrawLine(6, 0, i, 0, i + 3);
			
			return new Sprite(bitmap);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should act upon player contact.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Hover", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Fall Platform" : "Hover Platform";
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 0) ? null : base.GetDebugOverlay(obj);
		}
	}
	
	class HPlatform : MPZ.Platform
	{
		public override Point offset { get { return new Point(-63, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class HPlatform2 : MPZ.Platform
	{
		public override Point offset { get { return new Point(-127, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VPlatform : MPZ.Platform
	{
		public override Point offset { get { return new Point(0, -63); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	class VPlatform2 : MPZ.Platform
	{
		public override Point offset { get { return new Point(0, -127); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	class EPlatform : MPZ.Platform
	{
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 226);
			overlay.DrawLine(6, 0, 0, 0, 225); // LevelData.ColorWhite
			return new Sprite(overlay);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			return null;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
	
	class MPlatform : MPZ.Platform
	{
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(129 + 10, 65);
			int[] points = {
				0, 0,
				0, 64,
				128, 0,
				128, 64,
				0, 0
			};
			for (int i = 2; i < points.Length; i += 2)
				overlay.DrawArrow(6, points[i-2] + 5, points[i-1], points[i] + 5, points[i+1]);
			return new Sprite(overlay, -5, 0);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			return null;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
	
	abstract class Platform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite[] sprites = new Sprite[2];
		private Sprite debug;
		
		public virtual Point offset { get { return new Point(0, 0); } }
		public virtual Dictionary<string, int> names { get { return new Dictionary<string, int>{}; } }
		
		public virtual Sprite SetupDebugOverlay()
		{
			if (offset.IsEmpty)
				return null;
			
			BitmapBits bitmap = new BitmapBits(-offset.X + 1, -offset.Y + 1);
			bitmap.DrawLine(6, 0, 0, -offset.X, -offset.Y); // LevelData.ColorWhite
			return new Sprite(bitmap, offset.X, offset.Y);
		}
		
		public virtual PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, names,
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(383, 207, 64, 24), -32, -12);
			sprites[1] = new Sprite(sprites[0], offset);
			
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
			return "Start From " + names.GetKey((subtype == 1) ? 1 : 0);
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
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
	
	public static class BitmapBitsExtensions
	{
		public static void DrawArrow(this BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
		}
	}
}