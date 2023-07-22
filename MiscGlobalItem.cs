
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public class MiscGlobalItem : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if(item.type == ItemID.MechanicsRod && ModContent.GetInstance<ServerConfig>().Autofisher)
			{
				for(int i = 0; i < tooltips.Count; i++)
				{
					if(tooltips[i].Name == "Material")
					{
						tooltips.Insert(i + 1, new TooltipLine(Mod, "AutofisherMaterial", Language.GetTextValue("Mods.GoldensMisc.ItemTooltip.MechanicsRod")));
						return;
					}
				}
			}
		}
	}
}