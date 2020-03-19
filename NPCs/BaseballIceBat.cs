
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballIceBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.IceBat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.IceBat;
			animationType = NPCID.IceBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(spawnInfo.player.position.Y / 16f > Main.worldSurface && spawnInfo.player.ZoneSnow)
				return 0.005f;
			return 0f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(100) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.DepthMeter, prefixGiven: -1);
			if(Main.rand.Next(Main.expertMode ? 2 : 4) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ModContent.ItemType<BaseballBat>(), prefixGiven: -1);
			if(Main.rand.Next(3) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ModContent.ItemType<BaseballCap>());
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
