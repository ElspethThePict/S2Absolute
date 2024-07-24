using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.DEZ
{
	class Eggman : S2ObjectDefinitions.DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(1, 139, 38, 51), -22, -25); // #Absolute - change sprite bounds
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(513, 58, 32, 51), -16, -25);
		}
	}
}

namespace S2ObjectDefinitions.DEZ
{
	class DERArm : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(487, 125, 24, 24), -12, -12);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(999, 125, 24, 24), -12, -12);
		}
	}
	
	class DERBomb : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(401, 125, 28, 32), -14, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(913, 125, 28, 32), -14, -16);
		}
	}
	
	class DERFoot : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(291, 88, 64, 60), -32, -28);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(803, 88, 64, 60), -32, -28);
		}
	}
	
	class DERHand : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(430, 100, 64, 24), -32, -12);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(942, 100, 64, 24), -32, -12);
		}
	}
	
	class DERLeg : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(356, 154, 32, 32), -16, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(868, 154, 32, 32), -16, -16);
		}
	}
	
	class DERShoulder : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(356, 123, 32, 30), -16, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(868, 123, 32, 30), -16, -16);
		}
	}
	
	class DERTarget : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(356, 213, 42, 42), -21, -21);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(868, 213, 42, 42), -21, -21);
		}
	}
	
	class EggmanWindow : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(1, 114, 32, 24), -16, -12);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(513, 114, 32, 24), -16, -12);
		}
	}
	
	class MechaSonic : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(1, 1, 40, 56), -20, -28);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(513, 1, 40, 56), -20, -28);
		}
		
		public override bool Hidden
		{
			get { return false; }
		}
	}
	
	class MechaSonicSpike : DEZ.DERGeneric
	{
		public override Sprite GetFrame()
		{
			// Even if the sprites for it exist on the sheet, this object skips MBZ sheet checks and *always* uses the DEZ sheet instead
			// if (LevelData.StageInfo.folder.EndsWith("Zone12"))
				return new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(296, 50, 7, 16), -4, -8);
			// else
			// 	return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(808, 50, 7, 16), -4, -8);
		}
	}
	
	abstract class DERGeneric : ObjectDefinition
	{
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override bool Hidden
		{
			get { return true; }
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