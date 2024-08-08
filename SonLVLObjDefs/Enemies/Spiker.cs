using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Spiker : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(66, 206, 23, 24), -12, -8);
				frames[1] = new Sprite(sheet.GetSection(66, 173, 24, 32), -12, -32);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(67, 781, 23, 24), -12, -8);
				frames[1] = new Sprite(sheet.GetSection(67, 748, 24, 32), -12, -32);
			}
			
			frames[0] = new Sprite(frames);
			
			for (int dir = 0; dir < 4; dir++)
				sprites[dir] = new Sprite(frames[0], 0, ((dir & 2) == 0) ? 8 : -8, (dir & 1) == 1, (dir & 2) == 2);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Spiker is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 },
					{ "Left (Roof)", 2 },
					{ "Right  (Roof)", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
					return "Facing Left";
				case 1:
					return "Facing Right";
				case 2:
					return "Facing Left, Roof";
				case 3:
					return "Facing Right, Roof";
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