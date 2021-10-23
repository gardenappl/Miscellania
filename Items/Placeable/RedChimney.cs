
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class RedChimney : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RedBrickFurniture;
		}
		
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Chimney);
			Item.createTile = ModContent.TileType<Tiles.RedChimney>();
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.RedBrick, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
