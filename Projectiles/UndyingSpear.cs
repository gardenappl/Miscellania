
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class UndyingSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spear of Undying Justice");
			DisplayName.AddTranslation(GameCulture.Russian, "Копьё бессмертного правосудия");
		}
		
		public override void SetDefaults()
		{
			projectile.scale = 1.3f;
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.magic = true;
			projectile.ignoreWater = true;
			projectile.glowMask = MiscGlowMasks.UndyingSpearProjectile;
			aiType = ProjectileID.JavelinFriendly;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int type = Main.rand.Next(2) == 0 ? mod.ProjectileType("MagicSpearMini") : mod.ProjectileType("MagicSpearMiniAlt");
			
			switch(Main.rand.Next(4))
			{
				case 0: //Shoot right
					Projectile.NewProjectile(target.Left.X - 100, target.Left.Y, 5f, 0f, type, projectile.damage / 2, 1f, projectile.owner, 1, 1);
					return;
				case 1: //Shoot down
					Projectile.NewProjectile(target.Top.X, target.Top.Y - 100, 0f, 5f, type, projectile.damage / 2, 1f, projectile.owner, 1, 1);
					return;
				case 2: //Shoot left
					Projectile.NewProjectile(target.Right.X + 100, target.Right.Y, -5f, 0f, type, projectile.damage / 2, 1f, projectile.owner, 1, 1);
					return;
				case 3: //Shoot up
					Projectile.NewProjectile(target.Bottom.X, target.Bottom.Y + 100, 0f, -5f, type, projectile.damage / 2, 1f, projectile.owner, 1, 1);
					return;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, projectile.position, 10);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 59, projectile.velocity.X, projectile.velocity.Y);
				Main.dust[dust].scale = 1.5f;
			}
		}
	}
}
