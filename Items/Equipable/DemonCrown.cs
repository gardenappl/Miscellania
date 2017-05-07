
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class DemonCrown : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Face);
			return Config.DemonCrown;
		}
		
		public override void SetDefaults()
		{
			item.name = "Demon Crown";
			item.width = 22;
			item.height = 30;
			AddTooltip("Grants the wearer great magical powers");
			AddTooltip("Summons a Red Crystal to protect you");
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
