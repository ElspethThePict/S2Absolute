using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Slicer : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(29, 1, 47, 32), -16, -16);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(880, 256, 47, 32), -16, -16);
			}
			
			sprites[0].Flip(true, false);
			sprites[1] = new Sprite(sprites[0], true, false);
			sprites[2] = new Sprite(sprites[0], false, true);
			sprites[3] = new Sprite(sprites[0], true, true);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Slicer is facing.", null, new Dictionary<string, int>
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