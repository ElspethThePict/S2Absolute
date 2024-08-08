using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace S2ObjectDefinitions.MBZ
{
	public class MBZSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			// MBZ's tile palette uses an orange for boss blacks while in-game uses, well, black, so
			// (let's just do a little correction here, only set it if the current colour there is the same orange as in the original palette)
			// (yeah i'm iffy on this because an obj def shouldn't really be editing the stage, but i feel like it's kind of warranted here? not sure yet, may change this later..)
			if (LevelData.BmpPal.Entries[192] == Color.FromArgb(192, 96, 0))
			{
				LevelData.BmpPal.Entries[192] = Color.Black;
				LevelData.NewPalette[192] = Color.Black;
			}
			
			base.Init(data);
		}
	}
}