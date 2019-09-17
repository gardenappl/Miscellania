
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc.NPCs
{
	public class BaseballIlluminantBat : ModNPC
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.BaseballBats;
		}
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
			NPCID.Sets.TrailCacheLength[npc.type] = 4;
		}
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.IlluminantBat);
			npc.defense += 8;
			npc.rarity = 2;
			aiType = NPCID.IlluminantBat;
			animationType = NPCID.IlluminantBat;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(spawnInfo.player.ZoneHoly)
				return SpawnCondition.Underground.Chance / 100f + SpawnCondition.Cavern.Chance / 100f;
			return 0f;
		}
		
		public override void NPCLoot()
		{
			if(Main.rand.Next(Main.expertMode ? 150 : 200) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, ItemID.BlessedApple);
			if(Main.rand.Next(Main.expertMode ? 2 : 4) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballBat>(), prefixGiven: -1);
			if(Main.rand.Next(3) == 0)
				Item.NewItem(npc.position, npc.width, npc.height, mod.ItemType<BaseballCap>());
		}
		
//		public override void OnHitPlayer(Player target, int damage, bool crit)
//		{
//			if(Main.expertMode && Main.rand.Next(10) == 0)
//				target.AddBuff(BuffID.Rabies, Main.rand.Next(30, 90) * 60);
//			if(Main.rand.Next(14) == 0)
//				target.AddBuff(BuffID.Confused, 7 * 60);
//		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
			for (int i = 0; i < npc.oldPos.Length; i++)
			{
				var drawPos = npc.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
				var color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - i) / (float)npc.oldPos.Length);
				spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
