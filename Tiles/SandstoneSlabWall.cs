
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Tiles
{
	public class SandstoneSlabWall : ModWall
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BuildingMaterials;
		}
		
		public override void SetStaticDefaults()
		{
			Main.wallLargeFrames[Type] = (byte) 1;
			Main.wallHouse[Type] = true;
			DustType = DustID.Stone;
			ItemDrop = ModContent.ItemType<Items.Placeable.SandstoneSlabWall>();
			AddMapEntry(Color.Brown);
		}
		
		public override bool CreateDust(int i, int j, ref int type)
		{
			Dust.NewDust(new Vector2(i * 16f, j * 16f), 16, 16, DustType);
			return false;
		}
	}
}
