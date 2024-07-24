using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Nebula : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(72, 1, 48, 40), -24, -20);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(72, 1, 48, 40), -24, -20); // broken frame btw
			}
			
			properties[0] = new PropertySpec("Initial State", typeof(int), "Extended",
				"How the Nebula will start off. If Inactive, a Object Activator should be used to trigger this Nebula.", null, new Dictionary<string, int>
				{
					{ "Active", 0 },
					{ "Inactive", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			return (subtype > 0) ? "Start Inactive" : "Start Active";
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