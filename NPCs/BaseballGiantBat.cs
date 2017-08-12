
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballGiantBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.Russian, "Бейсбольная гигантская летучая мышь");
			Main.npcFrameCount[npc.type] = 4;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.GiantBat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.GiantBat;
			animationType = NPCID.GiantBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(!Main.hardMode)
				return 0f;
			if(SpawnCondition.Cavern.Chance > 0f)
				return SpawnCondition.Cavern.Chance / 100f;
			return SpawnCondition.Underground.Chance / 100f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(Main.expertMode ? 100 : 200) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.TrifoldMap, prefixGiven: -1);
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
			if(Main.rand.Next(14) == 0)
				target.AddBuff(BuffID.Confused, 7 * 60);
		}
	}
}
