
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballCaveBat : ModNPC
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.CaveBat);
			NPC.defense += 8;
			NPC.rarity = 2;
			AIType = NPCID.CaveBat;
			AnimationType = NPCID.CaveBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
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
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if(Main.expertMode && Main.rand.Next(10) == 0)
				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
		}
	}
}
