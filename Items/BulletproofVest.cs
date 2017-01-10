
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items
{
	public class BulletproofVest : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Body);
			return false;
		}
		
		public override void SetDefaults()
		{
			item.name = "Reinforced Vest";
			item.width = 20;
			item.height = 20;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.SWATHelmet;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MiscPlayer>(mod).ExplosionResistant = true;
		}
	}
}
