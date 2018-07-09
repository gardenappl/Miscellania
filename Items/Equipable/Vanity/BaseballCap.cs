
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class BaseballCap : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}
	}
}
