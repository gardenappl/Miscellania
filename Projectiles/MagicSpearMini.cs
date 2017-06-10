
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class MagicSpearMini : ModProjectile
	{
		int timesSpawned
		{
			get { return (int)projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		
		bool undying
		{
			get { return projectile.ai[0] > 0; }
			set { projectile.ai[0] = value ? 1 : 0; }
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mini Spear of Justice");
			DisplayName.AddTranslation(GameCulture.Russian, "Мини-копье правосудия");
		}
		
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 18;
			projectile.scale = 1.3f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.maxPenetrate = -1;
			projectile.magic = true;
			projectile.light = 0.1f;
			projectile.tileCollide = false;
			projectile.timeLeft = 60;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(timesSpawned > 2 || timesSpawned == -1)
			{
				return;
			}
			int type = Main.rand.Next(2) == 0 ? mod.ProjectileType("MagicSpearMini") : mod.ProjectileType("MagicSpearMiniAlt");
			
			if(undying)
			{
				switch(Main.rand.Next(4))
				{
					case 0: //Shoot right
						Projectile.NewProjectile(target.Left.X - 100, target.Left.Y, 5f, 0f, type, projectile.damage, 1f, projectile.owner, 1, timesSpawned + 1);
						return;
					case 1: //Shoot down
						Projectile.NewProjectile(target.Top.X, target.Top.Y - 100, 0f, 5f, type, projectile.damage, 1f, projectile.owner, 1, timesSpawned + 1);
						return;
					case 2: //Shoot left
						Projectile.NewProjectile(target.Right.X + 100, target.Right.Y, -5f, 0f, type, projectile.damage, 1f, projectile.owner, 1, timesSpawned + 1);
						return;
					case 3: //Shoot up
						Projectile.NewProjectile(target.Bottom.X, target.Bottom.Y + 100, 0f, -5f, type, projectile.damage, 1f, projectile.owner, 1, timesSpawned + 1);
						return;
				}
			}
			else
			{
				switch(Main.rand.Next(4))
				{
					case 0: //Shoot right
						Projectile.NewProjectile(target.Left.X - 80, target.Left.Y, 4f, 0f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
						return;
					case 1: //Shoot down
						Projectile.NewProjectile(target.Top.X, target.Top.Y - 80, 0f, 4f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
						return;
					case 2: //Shoot left
						Projectile.NewProjectile(target.Right.X + 80, target.Right.Y, -4f, 0f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
						return;
					case 3: //Shoot up
						Projectile.NewProjectile(target.Bottom.X, target.Bottom.Y + 80, 0f, -4f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
						return;
				}
			}
			timesSpawned = -1; //A projectile should only spawn once
		}
	}
}
