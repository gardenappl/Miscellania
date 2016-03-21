
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class MagicSpearMini : ModProjectile
	{
		float timesSpawned
		{
			get{ return projectile.ai[1]; }
			set{ projectile.ai[1] = value; }
		}
		
		public override void SetDefaults()
		{
			projectile.name = "Magic Mini Spear";
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
				return;
			
			int type = Main.rand.Next(2) == 0 ? mod.ProjectileType("MagicSpearMini") : mod.ProjectileType("MagicSpearMiniAlt");
			
			switch(Main.rand.Next(4))
			{
				case 0: //Shoot right
					Projectile.NewProjectile(target.position.X - 64, target.position.Y, 3f, 0f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
					return;
				case 1: //Shoot down
					Projectile.NewProjectile(target.position.X, target.position.Y - 64, 0f, 3f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
					return;
				case 2: //Shoot right
					Projectile.NewProjectile(target.position.X + 64, target.position.Y, -3f, 0f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
					return;
				case 3: //Shoot up
					Projectile.NewProjectile(target.position.X, target.position.Y + 64, 0f, -3f, type, projectile.damage, 0.5f, projectile.owner, 0, timesSpawned + 1);
					return;
			}
			
			timesSpawned = -1; //A projectile should only spawn once
		}
	}
}
