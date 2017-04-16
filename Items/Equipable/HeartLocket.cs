
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class HeartLocket : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Neck);
			return base.Autoload(ref name, ref texture, equips);
		}
		
		public override void SetDefaults()
		{
			item.name = "Heart Locket";
			item.width = 26;
			item.height = 38;
			AddTooltip("Increases movement speed and length of invincibility after being struck");
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
