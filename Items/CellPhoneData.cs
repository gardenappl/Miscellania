
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GoldensMisc.Items
{
	public class CellPhoneData : GlobalItem
	{
		public override bool Autoload(ref string name)
		{
			return false; //Unused
		}
		
		public override bool InstancePerEntity
		{
			get { return true; }
		}
		
		public override bool CloneNewInstances
		{
			get { return true; }
		}
		
		public bool CanWormhole;
		
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if(item.type == ItemID.CellPhone)
			{
				if(CanWormhole)
				{
					int index = tooltips.FindIndex(x => x.mod == "Terraria" && x.Name == "Tooltip1");
					if(index != -1)
						tooltips[index].text = Language.GetTextValue("Mods.GoldensMisc.NewPhone");
				}
				else
				{
					var line = new TooltipLine(mod, "GoldensMisc: Old Phone", Language.GetTextValue("Mods.GoldensMisc.OldPhone"));
					line.overrideColor = Colors.RarityRed;
					tooltips.Add(line);
				}
			}
		}
		
		public override bool NeedsSaving(Item item)
		{
			return item.type == ItemID.CellPhone;
		}
		
		public override TagCompound Save(Item item)
		{
			var tag = new TagCompound();
			tag.Add("w", CanWormhole);
			return tag;
		}
		
		public override void Load(Item item, TagCompound tag)
		{
			CanWormhole = tag.GetBool("w");
		}
	}
}
