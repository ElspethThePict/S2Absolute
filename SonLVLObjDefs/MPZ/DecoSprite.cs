using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class TransSprite : MPZ.DecoSprite
	{
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Left Frame", 0 }, { "Right Frame", 1 }}; } }
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override Sprite[] GetFrames()
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			Sprite[] sprites = new Sprite[2];
			sprites[0] = new Sprite(sheet.GetSection(111, 83, 32, 8), -8, -32);
			sprites[1] = new Sprite(sheet.GetSection(100, 126, 32, 8), -8, -32);
			return sprites;
		}
	}
	
	class BoltEnd : MPZ.DecoSprite
	{
		public override Sprite[] GetFrames()
		{
			return new Sprite[] { new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(144, 83, 28, 8), -14, -4) };
		}
	}
	
	class SmallCog : MPZ.DecoSprite
	{
		public override Sprite[] GetFrames()
		{
			return new Sprite[] { new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(133, 126, 24, 24), -12, -12) };
		}
	}
	
	class Guage : MPZ.DecoSprite
	{
		public override Sprite[] GetFrames()
		{
			return new Sprite[] { new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(330, 157, 16, 16), -8, -8) };
		}
	}
	
	class PlatConveyor : MPZ.DecoSprite
	{
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Frame 1", 0 }, { "Frame 2", 1 }}; } }
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override Sprite[] GetFrames()
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			Sprite[] sprites = new Sprite[2];
			sprites[0] = new Sprite(sheet.GetSection(173, 53, 4, 16), -2, -8);
			sprites[1] = new Sprite(sheet.GetSection(178, 53, 4, 16), -2, -8);
			return sprites;
		}
	}
	
	abstract class DecoSprite : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites;
		private Sprite[] debug;
		
		public virtual Dictionary<string, int> names { get { return null; } }
		public abstract Sprite[] GetFrames();
		
		public override void Init(ObjectData data)
		{
			sprites = GetFrames();
			
			debug = new Sprite[sprites.Length];
			
			for (int i = 0; i < sprites.Length; i++)
			{
				Rectangle bounds = sprites[i].Bounds;
				BitmapBits overlay = new BitmapBits(bounds.Size);
				overlay.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1); // LevelData.ColorWhite
				debug[i] = new Sprite(overlay, bounds.X, bounds.Y);
			}
			
			if (sprites.Length > 1)
			{
				properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
					"Which sprite this object should display.", null, names,
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return (sprites.Length > 1) ? properties : null; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (sprites.Length > 1) ? names.GetKey(subtype) : null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(sprites.Length > 1) ? obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue];
		}
	}
}