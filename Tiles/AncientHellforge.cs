
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class AncientHellforge : ModTile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientForges;
		}
		
		public override void SetStaticDefaults()
		{
			Main.tileObsidianKill[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 18 };
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Red, CreateMapEntryName());
			//disableSmartCursor = true;
			AdjTiles = new int[]{ TileID.Hellforge, TileID.Furnaces };
		}

		public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.AncientHellforge>());
		}

		public override void RandomUpdate(int i, int j)
		{
			int amount = Main.rand.Next(1, 4);
			for(int a = 0; a < amount; a++)
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Torch);
		}

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
			MinPick = 0;
			if (j > Main.maxTilesY - 300)
				MinPick = 60;
			return true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			float rand = 0f;
			//float rand = (float)Main.rand.Next(28, 42) * 0.005f;
			//rand += (float)(270 - (int)Main.mouseTextColor) / 700f;
			r = 0.75f + rand;
			g = 0.45f + rand;
			b = 0.25f + rand;
		}
	}
}
