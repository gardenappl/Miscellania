
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using GoldensMisc.Items;
using GoldensMisc.Items.Consumable;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Materials;
using GoldensMisc.Items.Weapons;
using GoldensMisc.Items.Placeable;

namespace GoldensMisc
{
	public class MiscGlobalNPC : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			switch(type)
			{
				case NPCID.GoblinTinkerer:
					if(ModContent.GetInstance<ServerConfig>().Magnet && Main.hardMode && !Main.dayTime)
					{
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<UniversalMagnet>());
						nextSlot++;
					}
					break;
				case NPCID.Demolitionist:
					if(ModContent.GetInstance<ServerConfig>().ReinforcedVest && Main.hardMode)
					{
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<ReinforcedVest>());
						nextSlot++;
					}
					break;
				case NPCID.Wizard:
					if(ModContent.GetInstance<ServerConfig>().MagicStones && NPC.downedMechBossAny)
					{
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<InertStone>());
						nextSlot++;
					}
					break;
				case NPCID.Mechanic:
					if(ModContent.GetInstance<ServerConfig>().MechanicsRodOften && Main.hardMode && Main.moonPhase <= 4 && NPC.AnyNPCs(NPCID.Angler))
					{
						if(!MiscUtils.ChestHasItem(shop, ItemID.MechanicsRod))
						{
							shop.item[nextSlot].SetDefaults(ItemID.MechanicsRod);
							nextSlot++;
						}
					}
					break;
				case NPCID.Steampunker:
					if(ModContent.GetInstance<ServerConfig>().ChestVacuum)
					{
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<ChestVacuum>());
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
					if(ModContent.GetInstance<ServerConfig>().AncientMuramasa && Main.rand.Next(250) == 0)
						Item.NewItem(npc.position, npc.Size, ModContent.ItemType<AncientMuramasa>(), prefixGiven: -1);
					break;
				case NPCID.Demon:
					if(ModContent.GetInstance<ServerConfig>().DemonCrown && Main.hardMode && Main.rand.Next(100) == 0)
						Item.NewItem(npc.position, npc.Size, ModContent.ItemType<DemonCrown>(), prefixGiven: -1);
					break;
				case NPCID.VoodooDemon:
					if(ModContent.GetInstance<ServerConfig>().DemonCrown && Main.hardMode && Main.rand.Next(15) == 0)
						Item.NewItem(npc.position, npc.Size, ModContent.ItemType<DemonCrown>(), prefixGiven: -1);
					break;
				//case NPCID.Necromancer:
				//case NPCID.NecromancerArmored:
				//case NPCID.RaggedCaster:
				//case NPCID.RaggedCasterOpenCoat:
				//case NPCID.DiabolistRed:
				//case NPCID.DiabolistWhite:
				//	if((Config.WormholeMirror || Config.RodofWarping) && Main.rand.Next(8) == 0)
				//		Item.NewItem(npc.position, npc.Size, ModContent.ItemType<WormholeCrystal>());
				//	break;
			}
		}
	}
}
