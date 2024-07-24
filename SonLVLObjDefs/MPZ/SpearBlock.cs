using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class SpearBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private readonly Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[5];
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(34, 102, 8, 32), -4, -16);
				frames[1] = new Sprite(sheet.GetSection(34, 84, 32, 8), -16, -4);
				frames[2] = new Sprite(sheet.GetSection(43, 102, 8, 32), -4, -16);
				frames[3] = new Sprite(sheet.GetSection(34, 93, 32, 8), -16, -4);
				frames[4] = new Sprite(sheet.GetSection(52, 102, 32, 32), -16, -16);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(859, 615, 8, 32), -4, -16);
				frames[1] = new Sprite(sheet.GetSection(893, 648, 32, 8), -16, -4);
				frames[2] = new Sprite(sheet.GetSection(868, 615, 8, 32), -4, -16);
				frames[3] = new Sprite(sheet.GetSection(893, 657, 32, 8), -16, -4);
				frames[4] = new Sprite(sheet.GetSection(926, 541, 32, 32), -16, -16);
			}
			
			for (int i = 0; i < 4; i++)
			{
				Sprite sprite = new Sprite(frames[i]);
				
				switch (i)
				{
					case 0:
						sprite.Offset(0, -32);
						break;
					case 1:
						sprite.Offset(32, 0);
						break;
					case 2:
						sprite.Offset(0, 32);
						break;
					case 3:
						sprite.Offset(-32, 0);
						break;
				}
				
				sprites[i] = new Sprite(frames[4], sprite);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Where the Spear will point initially.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 1 },
					{ "Downwards", 2 },
					{ "Left", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype & 3)
			{
				default:
				case 0: return "Upwards";
				case 1: return "Right";
				case 2: return "Downwards";
				case 3: return "Left";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
	}
}