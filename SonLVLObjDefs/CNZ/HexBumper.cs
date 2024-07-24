using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HexBumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
				sprite = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(99, 99, 48, 32), -24, -16);
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(581, 343, 48, 32), -24, -16);
			
			BitmapBits bitmap = new BitmapBits(193, 2);
			bitmap.DrawLine(6, 0, 0, 192, 0); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -96, 0);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Bumper should behave.", null, new Dictionary<string, int>
				{
					{ "Stationary", 0 },
					{ "Moving", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			return (subtype == 1) ? "Moving Bumper" : "Stationary Bumper";
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

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 1) ? debug : null;
		}
	}
}