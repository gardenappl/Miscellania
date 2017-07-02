
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class AncientHellforge : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.AncientForges;
		}
		
		public override void SetDefaults()
		{
			Main.tileObsidianKill[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
//			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 18 };
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			var name = CreateMapEntryName();
			name.SetDefault("Ancient Hellforge");
			name.AddTranslation(GameCulture.Russian, "Древняя адская кузня");
			AddMapEntry(Color.Red, name);
			disableSmartCursor = true;
			adjTiles = new int[]{ TileID.Hellforge };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
		}
	}
}
