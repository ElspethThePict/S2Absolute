using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.HPZ
{
	class PipeValve : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[4];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("HPZ/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(418, 1, 24, 24), -12, -12); // valve
			frames[1] = new Sprite(sheet.GetSection(443, 1, 32, 32), -16, -16); // back
			frames[2] = new Sprite(sheet.GetSection(484, 0, 28, 135), -14, 0 - 135); // water geyser (not part of this obj in-game, but it 
			frames[3] = new Sprite(sheet.GetSection(174, 231, 48, 24), -24, -20 - 135); // geyser splash
			
			sprites[0] = new Sprite(frames[1], frames[0]);
			sprites[1] = new Sprite(frames[2], frames[3], sprites[0]);
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Rectangle bounds = sprites[0].Bounds;
			bounds.Offset(obj.X, obj.Y);
			return bounds;
		}
	}
}