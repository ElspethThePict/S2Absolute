using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class MovingSpikes : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(206, 26, 48, 80), -24, -40);
			
			BitmapBits bitmap = new BitmapBits(209, 2);
			bitmap.DrawLine(6, 0, 0, 208, 0); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -104, 0);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How these Moving Spikes should act. Range of distance is the same between all variants.", null, new Dictionary<string, int>
				{
					{ "Start From Left", 0 },
					{ "Start From Right", 1 },
					{ "Start at Current Position", 2 }
				},
				(obj) => (obj.PropertyValue < 2) ? obj.PropertyValue : 2,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 2; }
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
					return "Start From Left";
				case 1:
					return "Start From Right";
				case 2:
				default:
					return "Start at Current Position";
			}
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
			return debug;
		}
	}
}