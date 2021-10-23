
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballLavaBat : ModNPC
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
			NPC.CloneDefaults(NPCID.Lavabat);
			NPC.defense += 8;
			NPC.rarity = 2;
			AIType = NPCID.Lavabat;
			AnimationType = NPCID.Lavabat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Main.hardMode ? SpawnCondition.Underworld.Chance / 100f : 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.MagmaStone, 50));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BaseballBat>(), 4, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BaseballCap>(), 3));
		}

		//		public override void OnHitPlayer(Player target, int damage, bool crit)
		//		{
		//			if(Main.expertMode && Main.rand.Next(10) == 0)
		//				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
		//		}
	}
}
