
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class AncientHellforge : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientForges;
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 26;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.sellPrice(silver: 60);
			Item.createTile = ModContent.TileType<Tiles.AncientHellforge>();
		}
		
		public override void AddRecipes()
		{
//			var recipe = new ModRecipe(mod);
//			recipe.SetResult(ItemID.AdamantiteForge);
//			recipe.AddIngredient(ItemID.AdamantiteOre, 30);
//			recipe.AddIngredient(this);
//			recipe.AddTile(TileID.MythrilAnvil);
//			recipe.AddRecipe();
//			
//			recipe = new ModRecipe(mod);
//			recipe.SetResult(ItemID.TitaniumForge);
//			recipe.AddIngredient(ItemID.TitaniumOre, 30);
//			recipe.AddIngredient(this);
//			recipe.AddTile(TileID.MythrilAnvil);
//			recipe.AddRecipe();
		}
	}
}
