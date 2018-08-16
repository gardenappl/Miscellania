
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using GoldensMisc.Items.Tools;
using GoldensMisc.Projectiles;
using GoldensMisc.Items.Equipable.Vanity;

namespace GoldensMisc
{
	public class MiscPlayer : ModPlayer
	{
		public bool ExplosionResistant;
		public bool DemonCrown;
		public bool Magnet;
		public bool OrbofLight;
		
		public override void ResetEffects()
		{
			ExplosionResistant = false;
			DemonCrown = false;
			Magnet = false;
			OrbofLight = false;
		}
		
		//bool shake;
		//Vector2 prevOffset;
		//Vector2 targetOffset;
		//int tick;
		//int nextShake = 5;
		//float intensity = 5f;
		
		//public override void ModifyScreenPosition()
		//{
		//	float offsetX = MathHelper.Lerp(prevOffset.X, targetOffset.X, 0.2f);
		//	float offsetY = MathHelper.Lerp(prevOffset.Y, targetOffset.Y, 0.2f);
		//	var offset = new Vector2(offsetX, offsetY);
		//	prevOffset = offset;
			
		//	Main.screenPosition += offset;
			
		//	if(shake)
		//	{
		//		tick++;
		//		if(tick >= nextShake)
		//		{
		//			tick = 0;
		//			nextShake = Main.rand.Next(3, 8);
		//			intensity += 0.1f;
		//			targetOffset = Main.rand.NextVector2Unit() * Main.rand.Next((int)intensity, (int)intensity * 2);
		//		}
		//	}
		//	else
		//		targetOffset = new Vector2();
		//}
		
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(ExplosionResistant)
			{
				int projType = damageSource.SourceProjectileType;
				var proj = new Projectile();
				proj.SetDefaults(projType);
				if(proj.aiStyle == 16 && damageSource.SourcePlayerIndex == player.whoAmI)
					return false;
			}
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}
		
		public override void PostUpdateEquips()
		{
			if(DemonCrown && player.ownedProjectileCounts[mod.ProjectileType<RedCrystal>()] == 0)
				Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType<RedCrystal>(), 60, 8, player.whoAmI);
		}
		
//		//stolen from Elemental Unleash
//		//tbh this is not necessary but switching the sprite to the normal Recall Mirror is a nice detail I hope
//		public override void ModifyDrawLayers(List<PlayerLayer> layers)
//		{
////			PreHeldItem.visible = true;
////			PostHeldItem.visible = true;
//			for(int i = 0; i < layers.Count; i++)
//			{
//				if(layers[i] == PlayerLayer.HeldItem)
//				{
//					layers.Insert(i, PreHeldItem);
//					i += 2;
//					layers.Insert(i, PostHeldItem);
//				}
//			}
//		}
		
//		static readonly PlayerLayer PreHeldItem = new PlayerLayer("GoldensMisc", "PreHeldItem", PlayerLayer.HeldItem, delegate(PlayerDrawInfo drawInfo)
//		{
//			var mod = GoldensMisc.Instance;
//			Main.itemTexture[mod.ItemType<WormholeIceMirror>()] = Main.itemTexture[ItemID.IceMirror];
//			Main.itemTexture[mod.ItemType<WormholeDoubleMirror>()] = Main.itemTexture[ItemID.MagicMirror];
//			Main.itemTexture[mod.ItemType<WormholeCellPhone>()] = Main.itemTexture[ItemID.CellPhone];
//		});
		
//		static readonly PlayerLayer PostHeldItem = new PlayerLayer("GoldensMisc", "PostHeldItem", PlayerLayer.HeldItem, delegate(PlayerDrawInfo drawInfo)
//		{
//			var mod = GoldensMisc.Instance;
//			Main.itemTexture[mod.ItemType<WormholeIceMirror>()] = mod.GetTexture("Items/Tools/WormholeIceMirror");
//			Main.itemTexture[mod.ItemType<WormholeDoubleMirror>()] = mod.GetTexture("Items/Tools/WormholeDoubleMirror");
//			Main.itemTexture[mod.ItemType<WormholeCellPhone>()] = mod.GetTexture("Items/Tools/WormholeCellPhone");
//		});


		public override void GetDyeTraderReward(List<int> rewardPool)
		{
			if(!Config.ExtraDyes)
				return;

			if(Main.hardMode)
			{
				rewardPool.Add(mod.ItemType<MatrixDye>());
				rewardPool.Add(mod.ItemType<VirtualDye>());
				rewardPool.Add(mod.ItemType<CobaltDye>());
				rewardPool.Add(mod.ItemType<PalladiumDye>());
				rewardPool.Add(mod.ItemType<MythrilDye>());
				rewardPool.Add(mod.ItemType<OrichalcumDye>());
				rewardPool.Add(mod.ItemType<AdamantiteDye>());
				rewardPool.Add(mod.ItemType<TitaniumDye>());
				if(NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					rewardPool.Add(mod.ItemType<ChlorophyteDye>());
				}
			}
		}
	}
}
