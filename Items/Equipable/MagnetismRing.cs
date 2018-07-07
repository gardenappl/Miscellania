
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
			return Config.Magnets;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases pickup range for items, coins and mana stars\n" +
			                   "'You're really attractive'"); //not necessary to translate the shitty pun

			DisplayName.AddTranslation(GameCulture.Russian, "Кольцо магнетизма");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает дистанцию взятия предметов, монет и звёзд маны\n" +
			                       "'Вы очень притягательны'");

			DisplayName.AddTranslation(GameCulture.Chinese, "磁力戒指");
			Tooltip.AddTranslation(GameCulture.Chinese, "增加物品,钱币及魔力星的拾取范围\n" +
								  "你真的很有吸引力");
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
