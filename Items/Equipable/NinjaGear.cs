
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class NinjaGear : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.NinjaGear;
		}
		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 26;
			item.value = Item.sellPrice(0, 5);
			item.rare = 8;
			item.accessory = true;
			item.shoeSlot = 3; //Tabi equipped texture
			item.waistSlot = 10; //Black Belt equipped texture
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.blackBelt = true;
			player.dash = 1;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Tabi);
			recipe.AddIngredient(ItemID.BlackBelt);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
