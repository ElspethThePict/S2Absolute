using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HBlock : CNZ.MovingBlock
	{
		public override Point offset { get { return new Point(96, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VBlock : CNZ.MovingBlock
	{
		public override Point offset { get { return new Point(0, 96); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	abstract class MovingBlock : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public abstract Point offset { get; }
		public abstract Dictionary<string, int> names { get; }
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(82, 34, 64, 64), -32, -32);
			sprites[0] = new Sprite(sprites[2],  offset.X,  offset.Y);
			sprites[1] = new Sprite(sprites[2], -offset.X, -offset.Y);
			
			BitmapBits bitmap = new BitmapBits((offset.X * 2) + 1, (offset.Y * 2) + 1);
			bitmap.DrawLine(6, 0, 0, offset.X * 2, offset.Y * 2); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -offset.X, -offset.Y);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this block should start from.", null, names,
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
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
}