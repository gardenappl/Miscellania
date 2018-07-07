
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class UniversalMagnet : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.Magnets;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increased pickup range for items");

			DisplayName.AddTranslation(GameCulture.Russian, "Универсальный магнит");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает дистанцию взятия предметов");

			DisplayName.AddTranslation(GameCulture.Chinese, "普通的磁铁");
			Tooltip.AddTranslation(GameCulture.Chinese, "增加物品拾取范围");
		}
		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 32;
			item.value = Item.buyPrice(0, 30);
			item.rare = 4;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MiscPlayer>(mod).Magnet = true;
		}
	}
}
