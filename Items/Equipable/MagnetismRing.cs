
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class MagnetismRing : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.Magnets;
		}
		
		public override void SetDefaults()
		{
			item.name = "Magnetism Ring";
			item.width = 28;
			item.height = 20;
			AddTooltip("Increased pickup range for items, mana stars and coins");
			AddTooltip2("'You're pretty attractive, huh?'");
			item.value = Item.sellPrice(0, 10);
			item.rare = 6;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MiscPlayer>(mod).Magnet = true;
			player.goldRing = true;
			player.manaMagnet = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType<UniversalMagnet>());
			recipe.AddIngredient(ItemID.CelestialMagnet);
			recipe.AddIngredient(ItemID.GoldRing);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
