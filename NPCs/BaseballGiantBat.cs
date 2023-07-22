﻿
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballGiantBat : ModNPC
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.GiantBat);
			NPC.defense += 8;
			NPC.rarity = 2;
			AIType = NPCID.GiantBat;
			AnimationType = NPCID.GiantBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(!Main.hardMode)
				return 0f;
			if(SpawnCondition.Cavern.Chance > 0f)
				return SpawnCondition.Cavern.Chance / 100f;
			return SpawnCondition.Underground.Chance / 100f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.ChainKnife, 250));
			npcLoot.Add(ItemDropRule.Common(ItemID.DepthMeter, 100));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BaseballBat>(), 4, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BaseballCap>(), 3));
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if(Main.expertMode && Main.rand.NextBool(10))
				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
			if(Main.rand.NextBool(14))
				target.AddBuff(BuffID.Confused, 7 * 60);
		}
	}
}
