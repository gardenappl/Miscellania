﻿
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GoldensMisc.Dusts;

namespace GoldensMisc.Projectiles
{
	public class RedBullet : ModProjectile
	{	
		int DustDelay
		{
			get { return (int)projectile.localAI[0]; }
			set { projectile.localAI[0] = value; }
		}
		
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "Terraria/Projectile_" + ProjectileID.RubyBolt;
			return base.Autoload(ref name, ref texture);
		}
		
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.RubyBolt);
			aiType = ProjectileID.RubyBolt;
			projectile.name = "Red Bolt";
		}
		
		public override void AI()
		{
			DustDelay--;
			if(DustDelay <= 0)
			{
				var dustVel = projectile.velocity * 0.5f + new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
				Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<RedDust>(), dustVel.X, dustVel.Y, Scale: 1.2f);
				DustDelay = 8;
			}
		}
	}
}