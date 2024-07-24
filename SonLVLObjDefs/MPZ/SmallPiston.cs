using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class SmallPiston : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(1, 70, 32, 64), -16, -32);
			
			BitmapBits bitmap = new BitmapBits(sprite.Width, sprite.Height);
			bitmap.DrawRectangle(6, 0, 0, sprite.Width-1, sprite.Height-1); // LevelData.ColorWhite
			debug = new Sprite(bitmap, sprite.X, sprite.Y + 72);
			
			bitmap = new BitmapBits(2, 73);
			bitmap.DrawLine(6, 0, 0, 0, 72); // LevelData.ColorWhite
			debug = new Sprite(debug, new Sprite(bitmap));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
			return debug;
		}
	}
}