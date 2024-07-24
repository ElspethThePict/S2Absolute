using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class RotatingStair : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private readonly Sprite[] debug   = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			Sprite sprite;
			
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects3.gif").GetSection(1, 62, 32, 32), -16, -16);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(130, 829, 32, 32), -16, -16);
			}
			
			sprites[0] = new Sprite(new Sprite(sprite, -48, -48),
			                        new Sprite(sprite, -16, -16),
			                        new Sprite(sprite,  16,  16),
			                        new Sprite(sprite,  48,  48));
									
			sprites[1] = new Sprite(new Sprite(sprite, -48, -48),
			                        new Sprite(sprite,  48,  48));
			
			BitmapBits bitmap = new BitmapBits(129, 129);
			bitmap.DrawRectangle(6, 0, 96, 31, 31); // bottom right
			bitmap.DrawRectangle(6, 96, 0, 31, 31); // top right
			debug[1] = new Sprite(bitmap, -64, -64);
			
			bitmap.DrawRectangle(6, 32, 64, 31, 31); // middle, bottom left
			bitmap.DrawRectangle(6, 64, 32, 31, 31); // middle, top right
			debug[0] = new Sprite(bitmap, -64, -64);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way these stairs should rotate.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
				
			properties[1] = new PropertySpec("Size", typeof(int), "Extended",
				"How many blocks this set of stairs should have.", null, new Dictionary<string, int>
				{
					{ "4 Blocks", 0 },
					{ "2 Blocks", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
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
				case 0:
					return "4 Blocks, Clockwise";
				case 1:
					return "4 Blocks, Counter-Clockwise";
				case 2:
					return "2 Blocks, Clockwise";
				case 3:
					return "2 Blocks, Counter-Clockwise";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype & 2) >> 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 2) >> 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue & 2) >> 1];
		}
	}
}