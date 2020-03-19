
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class MagnetismRing : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.Magnet;
		}
		
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.value = Item.sellPrice(0, 10);
			item.rare = 6;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MiscPlayer>().Magnet = true;
			player.goldRing = true;
			player.manaMagnet = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<UniversalMagnet>());
			recipe.AddIngredient(ItemID.CelestialMagnet);
			recipe.AddIngredient(ItemID.GoldRing);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
