using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class BubblersMother : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(117, 102, 32, 28), -16, -12);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(117, 102, 32, 28), -16, -12); // broken frame (base game is like this)
			}
			
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction this badnik will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
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
			return (subtype == 0) ? "Facing Left" : "Facing Right";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}