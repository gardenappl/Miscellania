
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class MagnetismRing : ModItem
	{

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().MagnetismRing;
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.value = Item.sellPrice(0, 10);
			Item.rare = ItemRarityID.LightPurple;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.treasureMagnet = true;
			player.goldRing = true;
			player.manaMagnet = true;
		}
		
		public override void AddRecipes()
		{
			if (ModContent.GetInstance<ServerConfig>().Magnet)
			{
				CreateRecipe()
					.AddIngredient(ModContent.ItemType<UniversalMagnet>())
					.AddIngredient(ItemID.CelestialMagnet)
					.AddIngredient(ItemID.GoldRing)
					.AddTile(TileID.TinkerersWorkbench)
					.Register();
			}

				CreateRecipe()
						.AddIngredient(ItemID.TreasureMagnet)
						.AddIngredient(ItemID.CelestialMagnet)
						.AddIngredient(ItemID.GoldRing)
						.AddTile(TileID.TinkerersWorkbench)
						.Register();

		}
	}
}
