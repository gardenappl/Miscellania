
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
			item.width = 28;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			item.SetDefaults(mod.ItemType<BaseballCapBackwards>());
		}
	}

	[AutoloadEquip(EquipType.Head)]
	public class BaseballCapBackwards : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			item.SetDefaults();
		}
	}
}
