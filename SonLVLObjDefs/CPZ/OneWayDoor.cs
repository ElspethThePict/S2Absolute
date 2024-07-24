using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.DEZ
{
	class OneWayDoor : S2ObjectDefinitions.CPZ.OneWayDoor
	{
		// #Absolute - they removed the object's sprite flip, so
		public override bool flip { get { return false; } }
	}
}

namespace S2ObjectDefinitions.CPZ
{
	class OneWayDoor : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual bool flip { get { return true; } }
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02") || LevelData.StageInfo.folder.EndsWith("Zone12")) // yeah DEZ loads the CPZ sheet too
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(206, 142, 16, 64), -8, -32);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(93, 312, 16, 64), -8, -32);
			}
			
			sprites[1] = new Sprite(sprites[0], flip, false);
				
			properties[0] = new PropertySpec("Open From", typeof(int), "Extended",
				"Which direction this Door should be opened from.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1, // in-game any non-0/1 values are treated as opening from the right but they don't flip their sprite
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
			switch (subtype)
			{
				case 0:
					return "Open From Left";
				case 1:
					return "Open From Right";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 1) ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
	}
}