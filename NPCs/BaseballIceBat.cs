
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballIceBat : ModNPC
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
			NPC.CloneDefaults(NPCID.IceBat);
			NPC.defense += 8;
			NPC.rarity = 2;
			AIType = NPCID.IceBat;
			AnimationType = NPCID.IceBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(spawnInfo.player.position.Y / 16f > Main.worldSurface && spawnInfo.player.ZoneSnow)
				return 0.005f;
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.DepthMeter, 100));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BaseballBat>(), 4, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BaseballCap>(), 3));
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if(Main.rand.Next(2) == 0)
				target.AddBuff(BuffID.Chilled, 20 * 60);
			if(Main.rand.Next(10) == 0 && target.FindBuffIndex(BuffID.Frozen) >= 0)
				target.AddBuff(BuffID.Frozen, 1 * 60);
		}
	}
}
