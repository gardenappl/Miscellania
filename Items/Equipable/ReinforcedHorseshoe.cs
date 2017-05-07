
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class ReinforcedHorseshoe : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.ReinforcedVest;
		}
		
		public override void SetDefaults()
		{
			item.name = "Reinforced Horseshoe";
			item.width = 24;
			item.height = 24;
			item.rare = 4;
			item.defense = 4;
			item.accessory = true;
			item.value = Item.buyPrice(0, 20);
			AddTooltip("Negates fall damage");
			AddTooltip("Grants immunity to fire blocks and your own explosions");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.noFallDmg = true;
			player.fireWalk = true;
			player.GetModPlayer<MiscPlayer>(mod).ExplosionResistant = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ObsidianHorseshoe);
			recipe.AddIngredient(mod.ItemType<ReinforcedVest>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
