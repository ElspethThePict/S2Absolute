using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class TippingFloor : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(1, 222, 32, 32), -16, -16);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(34, 692, 32, 32), -16, -16);
			}
			
			properties[0] = new PropertySpec("Interval Offset", typeof(int), "Extended",
				"How much this Tipping Floor's interval should be offset from the global interval.", null, new Dictionary<string, int>
				{
					{ "0%", 0 },
					{ "6.25%", 1 },
					{ "12.5%", 2 },
					{ "18.75%", 3 },
					{ "25%", 4 },
					{ "31.25%", 5 },
					{ "37.5%", 6 },
					{ "43.75%", 7 },
					{ "50%", 8 },
					{ "56.25%", 9 },
					{ "62.5%", 10 },
					{ "68.75%", 11 },
					{ "75%", 12 },
					{ "81.25%", 13 },
					{ "87.5%", 14 },
					{ "93.75%", 15 }
				},
				(obj) => obj.PropertyValue & 0x0F,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0F) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Duration", typeof(int), "Extended",
				"How long this floor should face up for.", null, new Dictionary<string, int>
				{
					{ "15 Frames", 0 },
					{ "31 Frames", 1 },
					{ "47 Frames", 2 },
					{ "63 Frames", 3 }
				},
				(obj) => (obj.PropertyValue >> 4) & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x70) | (byte)((int)value << 4)));
			
			properties[2] = new PropertySpec("VS Disable", typeof(bool), "Extended",
				"If this floor should always remain upright in 2P VS mode.", null,
				(obj) => obj.PropertyValue > 0x7F,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (byte)((bool)value ? 0x80 : 0x00)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
	}
}