
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class AncientForge : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return ModContent.GetInstance<ServerConfig>().AncientForges;
		}
		
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 18 };
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Gray, CreateMapEntryName());
			//AddMapEntry(Color.Gray, mod.GetItem(GetType().Name).DisplayName);
			disableSmartCursor = true;
			adjTiles = new int[]{ TileID.Furnaces };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
		}

		public override void RandomUpdate(int i, int j)
		{
			int amount = Main.rand.Next(1, 4);
			for(int a = 0; a < amount; a++)
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Fire);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			float rand = 0f;
			//float rand = (float)Main.rand.Next(28, 42) * 0.005f;
			//rand += (float)(270 - (int)Main.mouseTextColor) / 700f;
			r = 0.83f + rand;
			g = 0.6f + rand;
			b = 0.5f + rand;
		}
	}
}
