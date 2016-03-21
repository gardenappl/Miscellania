
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace GoldensMisc.Global
{
	public class MiscGlobalNPC : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			switch(type)
			{
				case NPCID.GoblinTinkerer:
					if(Main.hardMode && !Main.dayTime)
					{
						shop.item[nextSlot].SetDefaults(mod.ItemType("UniversalMagnet"));
						nextSlot++;
					}
					break;
			}
		}
		
		public override void NPCLoot(NPC npc)
		{
			switch(npc.type)
			{  
				case NPCID.AngryBones:
				case NPCID.AngryBonesBig:
				case NPCID.AngryBonesBigHelmet:
				case NPCID.AngryBonesBigMuscle:
				case NPCID.DarkCaster:
					if(Main.rand.Next(250) == 0)
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientMuramasa"), pfix: -1);
					break;
			}
		}
	}
}
