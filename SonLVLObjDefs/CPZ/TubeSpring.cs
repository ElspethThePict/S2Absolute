using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.CPZ
{
	class TubeSpring : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(191, 1, 32, 16), -16, -16);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(99, 377, 32, 16), -16, -16);
			}
			
			// #Absolute - they dummied this out in favour of using the setting instead
			/*
			properties[0] = new PropertySpec("Use Twirl Anim", typeof(bool), "Extended",
				"If this Spring should trigger the Twirling animation upon launch.", null,
				(obj) => (obj.PropertyValue & 1) == 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | ((bool)value ? 1 : 0)));
			*/
			
			properties[0] = new PropertySpec("Strength", typeof(int), "Extended",
				"This Spring's launch velocity.", null, new Dictionary<string, int>
				{
					{ "Weak", 0 },
					{ "Strong", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (int)value));
			
			properties[1] = new PropertySpec("Collision Plane", typeof(int), "Extended",
				"Which Collision Plane this Spring should set the Player too upon launch.", null, new Dictionary<string, int>
				{
					{ "Don't Set", 0 },
					{ "Plane A", 4 },
					{ "Plane B", 8 }
				},
				(obj) => obj.PropertyValue & 12,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~12) | (int)value));
			
			properties[2] = new PropertySpec("Reset XVel", typeof(bool), "Extended",
				"If this Spring should reset the Player's X Velocity upon launch.", null,
				(obj) => (obj.PropertyValue & 0x80) == 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0x00)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2}); } // the spring always has a prop val of 2 everywhere it appears in the game, but let's stick with spring strength for this
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype & 2) == 2) ? "Strong Launch" : "Weak Launch";
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