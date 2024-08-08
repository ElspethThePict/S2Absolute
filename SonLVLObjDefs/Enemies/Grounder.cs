using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Grounder2 : Enemies.Grounder
	{
		public override Sprite[] GetFrames()
		{
			// Overloading the red Grounder sprites with our new green Grounder sprites
			
			Sprite[] frames = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects4.gif");
				frames[0] = new Sprite(sheet.GetSection(32, 41, 28, 32), -14, -12);
				frames[1] = new Sprite(sheet.GetSection(0, 0, 32, 40), -16, -20);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(374, 289, 28, 32), -14, -12);
				frames[1] = new Sprite(sheet.GetSection(275, 297, 32, 40), -16, -20);
			}
			
			return frames;
		}
	}
	
	class Grounder : ObjectDefinition
	{
		private Sprite[] sprites;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual Sprite[] GetFrames()
		{
			Sprite[] frames = new Sprite[2];
			
			// Default - red Grounder sprites
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(133, 1, 28, 32), -14, -12);
				frames[1] = new Sprite(sheet.GetSection(34, 1, 32, 40), -16, -20);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(374, 256, 28, 32), -14, -12);
				frames[1] = new Sprite(sheet.GetSection(275, 256, 32, 40), -16, -20);
			}
			
			return frames;
		}
		
		public override void Init(ObjectData data)
		{
			sprites = GetFrames();
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Grounder should initially act.", null, new Dictionary<string, int>
				{
					{ "Start Hidden", 0 },
					{ "Start Walking", 1 }
				},
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
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Start Walking" : "Start Hidden";
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype == 1 ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue == 1 ? 1 : 0];
		}
	}
}