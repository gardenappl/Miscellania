
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballHellBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baseball Hellbat");
			DisplayName.AddTranslation(GameCulture.Russian, "Бейсбольная адская летучая мышь");
			Main.npcFrameCount[npc.type] = 5;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Hellbat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.Hellbat;
			animationType = NPCID.Hellbat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Underworld.Chance / 100f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(150) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.MagmaStone, prefixGiven: -1);
			if(Main.rand.Next(Main.expertMode ? 2 : 4) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballBat>(), prefixGiven: -1);
			if(Main.rand.Next(3) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballCap>());
		}
		
//		public override void OnHitPlayer(Player target, int damage, bool crit)
//		{
//			if(Main.expertMode && Main.rand.Next(10) == 0)
//				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
//		}
	}
}
