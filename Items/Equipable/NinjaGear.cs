
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
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows the ability to dash\n" +
			                   "Gives a chance to dodge attacks");

			DisplayName.AddTranslation(GameCulture.Russian, "Снаряжение ниндзя");
			Tooltip.AddTranslation(GameCulture.Russian, "Позволяет уклоняться от противников\n" +
			                       "Дает шанс уклониться от атак");

			DisplayName.AddTranslation(GameCulture.Chinese, "忍者装备");
			Tooltip.AddTranslation(GameCulture.Chinese, "允许冲撞敌人\n" +
								  "有几率躲避攻击");
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
