
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class ReinforcedHorseshoe : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().ReinforcedVest;
		}
		
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 4;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 20);
		}
		
		public override void UpdateEquip(Player player)
		{
			player.noFallDmg = true;
			player.fireWalk = true;
			player.GetModPlayer<MiscPlayer>().ExplosionResistant = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ObsidianHorseshoe)
				.AddIngredient(ModContent.ItemType<ReinforcedVest>())
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
