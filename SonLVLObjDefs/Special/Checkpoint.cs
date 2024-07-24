using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Special
{
	class StartMessage : Special.Checkpoint
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Special/Objects.gif").GetSection(367, 91, 144, 30), -72, -15); // start frame
		}
	}
	
	class Checkpoint : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Special/Objects.gif").GetSection(199, 165, 32, 16), -16, -8); // checkpoint sprite
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			// #Absolute (kinda) - i'm half sure amy's gonna be added in the next update, so i'm leaving mentions of her here in case she is
			
			properties[0] = new PropertySpec("Rings - 2P", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint when two players are active (S&T, K&T, A&T).", null,
				(obj) => ((V4ObjectEntry)obj).Value0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = (int)value);
			
			properties[1] = new PropertySpec("Rings - STA", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint if the current player is just Sonic/Tails/Amy alone.", null,
				(obj) => ((V4ObjectEntry)obj).Value1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (int)value);
			
			properties[2] = new PropertySpec("Rings - K", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint if the current player is Knuckles alone.", null,
				(obj) => ((V4ObjectEntry)obj).Value2,
				(obj, value) => ((V4ObjectEntry)obj).Value2 = (int)value);
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