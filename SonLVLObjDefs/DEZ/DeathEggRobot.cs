using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.DEZ
{
	class DeathEggRobot : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(399, 183, 112, 72), -44, -36);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(911, 183, 112, 72), -44, -36);
			}
			
			properties[0] = new PropertySpec("Skip Cutscene", typeof(int), "Extended",
				"If the Death Egg Robot should skip the ending cutscene after it is defeated.", null,
				(obj) => obj.PropertyValue != 0,
				(obj, value) => obj.PropertyValue = ((byte)((bool)value ? 1 : 0)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
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
			return (subtype != 0) ? "Skip Cutscene" : "Trigger Cutscene";
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
	}
}