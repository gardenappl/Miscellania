﻿
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
		public override bool Autoload(ref string name)
		{
			return Config.AncientForges;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.Russian, "Древняя адская кузня");
			DisplayName.AddTranslation(GameCulture.Chinese, "远古地狱熔炉");
		}
		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 26;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.sellPrice(silver: 60);
			item.createTile = mod.TileType<Tiles.AncientHellforge>();
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
