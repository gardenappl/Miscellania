
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballLavaBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.Russian, "Бейсбольная лавовая летучая мышь");
			Main.npcFrameCount[npc.type] = 4;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Lavabat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.Lavabat;
			animationType = NPCID.Lavabat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Main.hardMode ? SpawnCondition.Underworld.Chance / 100f : 0f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(50) == 0)
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
