
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Face)]
	public class DemonCrown : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.DemonCrown;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants great magical powers\n" +
			                   "Summons a Red Crystal to fight for you");

			DisplayName.AddTranslation(GameCulture.Russian, "Демоническая корона");
			Tooltip.AddTranslation(GameCulture.Russian, "Даёт великую магическую силу\n" +
			                       "Призывает Красный кристалл, который сражается за вас");

			DisplayName.AddTranslation(GameCulture.Chinese, "恶魔皇冠");
			Tooltip.AddTranslation(GameCulture.Chinese, "赋予巨大的魔法力量\n" +
								  "召唤一个红水晶为你而战");
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 30;
			item.value = Item.sellPrice(0, 8);
			item.rare = 5;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicDamage += 0.1f;
			player.magicCrit += 7;
			player.manaCost -= 0.1f;
			player.statManaMax2 += 40;
			player.GetModPlayer<MiscPlayer>(mod).DemonCrown = true;
		}
	}
}
