using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[7];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 - 44, -16 + 20); // back wheel
				sprites[1] = new Sprite(sheet.GetSection(70, 155, 60, 20), -28, -28); // eggman
				sprites[2] = new Sprite(sheet.GetSection(0, 209, 64, 29), -32, -8); // eggmobile
				sprites[3] = new Sprite(sheet.GetSection(0, 109, 93, 32), -48, -16 + 8); // car
				sprites[4] = new Sprite(sheet.GetSection(94, 131, 32, 23), -16 - 54, -12 + 16); // drill
				sprites[5] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 - 12, -16 + 20); // front wheel 1
				sprites[6] = new Sprite(sheet.GetSection(1, 143, 32, 32), -16 + 28, -16 + 20); // front wheel 1
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 - 44, -16 + 20); // back wheel
				sprites[1] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28); // eggman
				sprites[2] = new Sprite(sheet.GetSection(415, 170, 64, 29), -32, -8); // eggmobile
				sprites[3] = new Sprite(sheet.GetSection(123, 1, 93, 32), -48, -16 + 8); // car
				sprites[4] = new Sprite(sheet.GetSection(123, 34, 32, 23), -16 - 54, -12 + 16); // drill
				sprites[5] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 - 12, -16 + 20); // front wheel 1
				sprites[6] = new Sprite(sheet.GetSection(123, 58, 32, 32), -16 + 28, -16 + 20); // front wheel 1
			}
			
			sprite = new Sprite(sprites);
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
			try
			{
				List<ObjectEntry> objs = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) - 2).TakeWhile(a => LevelData.Objects.IndexOf(a) <= (LevelData.Objects.IndexOf(obj) + 4)).ToList();
				
				short xmin = Math.Min(obj.X, objs.Min(a => a.X));
				short ymin = Math.Min(obj.Y, objs.Min(a => a.Y));
				short xmax = Math.Max(obj.X, objs.Max(a => a.X));
				short ymax = Math.Max(obj.Y, objs.Max(a => a.Y));
				BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				for (int i = 0; i < objs.Count - 1; i++)
					bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, objs[i + 1].X - xmin, objs[i + 1].Y - ymin); // LevelData.ColorWhite
				
				return new Sprite(bitmap, xmin - obj.X, ymin - obj.Y);
			}
			catch
			{
			}
			
			return null;
		}
	}
}