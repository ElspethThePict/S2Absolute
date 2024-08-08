using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.WFZ
{
	class BeltPlatformS : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(2, 456);
			bitmap.DrawLine(6, 0, 0, 0, 199); // LevelData.ColorWhite
			debug[2] = new Sprite(bitmap);
			debug[0] = new Sprite(debug[2], 0, -199);
			
			bitmap.DrawLine(6, 0, 0, 0, 455); // LevelData.ColorWhite
			debug[3] = new Sprite(bitmap);
			debug[1] = new Sprite(debug[3], 0, -455);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far the platforms should go.", null, new Dictionary<string, int>
				{
					{ "199 px", 0 },
					{ "455 px", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(bool), "Extended",
				"Which direction the platforms should go.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string name = ((subtype & 2) == 0) ? "Upwards" : "Downwards";
			name += ((subtype & 1) == 0) ? " (199 px)" : " (455 px)";
			return name;
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
			// update all the following platforms' sprites too, if possible
			// (we're always updating 8 and not 4 on purpose, in case distance (platform count) is different from before)
			foreach (var entity in LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) + 1).Take(8))
				entity.UpdateSprite();
			
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 3];
		}
	}
}