
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Neck)]
	public class HeartLocket : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().HeartLocket;
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 38;
			item.value = Item.sellPrice(0, 2);
			item.rare = 5;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.longInvince = true;
			player.panic = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PanicNecklace);
			recipe.AddIngredient(ItemID.CrossNecklace);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
