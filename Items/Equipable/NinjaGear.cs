
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class NinjaGear : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().NinjaGear;
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 26;
			Item.value = Item.sellPrice(0, 5);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
			Item.shoeSlot = 3; //Tabi equipped texture
			Item.waistSlot = 10; //Black Belt equipped texture
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.blackBelt = true;
			player.dashType = 1;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Tabi)
				.AddIngredient(ItemID.BlackBelt)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
