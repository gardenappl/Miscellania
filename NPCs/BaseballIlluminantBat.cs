

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;


namespace GoldensMisc.NPCs
{
	public class BaseballIlluminantBat : ModNPC
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.TrailCacheLength[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.IlluminantBat);
			NPC.defense += 8;
			NPC.rarity = 2;
			AIType = NPCID.IlluminantBat;
			AnimationType = NPCID.IlluminantBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(spawnInfo.player.ZoneHallow)
				return SpawnCondition.Underground.Chance / 100f + SpawnCondition.Cavern.Chance / 100f;
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.BlessedApple, 150, 200));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BaseballBat>(), 4, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BaseballCap>(), 3));
		}

		//		public override void OnHitPlayer(Player target, int damage, bool crit)
		//		{
		//			if(Main.expertMode && Main.rand.Next(10) == 0)
		//				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
		//			if(Main.rand.Next(14) == 0)
		//				target.AddBuff(BuffID.Confused, 7 * 60);
		//		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			var drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Width() * 0.5f, NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				var drawPos = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
				var color = NPC.GetAlpha(lightColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
