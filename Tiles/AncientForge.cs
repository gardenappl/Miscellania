
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
			return Config.AncientForges;
		}
		
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 18 };
			TileObjectData.addTile(Type);
//			var name = CreateMapEntryName();
//			name.SetDefault("Ancient Forge");
//			name.AddTranslation(GameCulture.Russian, "Древняя кузня");
//			AddMapEntry(Color.Gray, name);
			AddMapEntry(Color.Gray, mod.GetItem(GetType().Name).DisplayName);
			disableSmartCursor = true;
			adjTiles = new int[]{ TileID.Furnaces };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
		}
		
		public override void RandomUpdate(int i, int j)
		{
			base.RandomUpdate(i, j);
		}
	}
}
