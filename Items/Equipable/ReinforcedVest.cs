
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class ReinforcedVest : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Body);
			return Config.ReinforcedVest;
		}
		
		public override void AutoloadEquip(EquipType equip, ref string texture, ref string armTexture, ref string femaleTexture)
		{
			femaleTexture = texture;
		}
		
		public override void SetDefaults()
		{
			item.name = "Reinforced Vest";
			item.width = 20;
			item.height = 20;
			item.rare = 4;
			item.defense = 15;
			item.value = Item.buyPrice(0, 15);
			AddTooltip("Grants immunity to your own explosions");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MiscPlayer>(mod).ExplosionResistant = true;
		}
		
		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}
	}
}
