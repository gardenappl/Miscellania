﻿
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable.Vanity
{
	public class OrichalcumDye : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.ExtraDyes;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reflective Orichalcum Dye");

			DisplayName.AddTranslation(GameCulture.Russian, "Светоотражающий орихалковый краситель");
			DisplayName.AddTranslation(GameCulture.Chinese, "反光山铜染料");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 1, 50);
			item.rare = 3;
		}
	}
}