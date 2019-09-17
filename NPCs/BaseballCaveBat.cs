
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballCaveBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 5;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.CaveBat;
			animationType = NPCID.CaveBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(SpawnCondition.Cavern.Chance > 0f)
				return SpawnCondition.Cavern.Chance / 100f;
			
			return SpawnCondition.Underground.Chance / 100f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(250) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.ChainKnife, prefixGiven: -1);
			if(Main.rand.Next(100) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.DepthMeter, prefixGiven: -1);
			if(Main.rand.Next(Main.expertMode ? 2 : 4) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballBat>(), prefixGiven: -1);
			if(Main.rand.Next(3) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballCap>());
		}
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if(Main.expertMode && Main.rand.Next(10) == 0)
				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
		}
	}
}
