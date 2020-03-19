
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Body)]
	public class ReinforcedVest : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.ReinforcedVest;
		}
		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 4;
			item.defense = 15;
			item.value = Item.buyPrice(0, 15);
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MiscPlayer>().ExplosionResistant = true;
		}
		
		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}
	}
}
