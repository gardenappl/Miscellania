
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Tiles
{
	public class SandstoneSlabWall : ModWall
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.BuildingMaterials;
		}
		
		public override void SetDefaults()
		{
			Main.wallLargeFrames[Type] = (byte) 1;
			Main.wallHouse[Type] = true;
			dustType = DustID.Stone;
			drop = mod.ItemType(GetType().Name);
			AddMapEntry(Color.Brown);
		}
		
		public override bool CreateDust(int i, int j, ref int type)
		{
			Dust.NewDust(new Vector2(i * 16f, j * 16f), 16, 16, dustType);
			return false;
		}
	}
}
