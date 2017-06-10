
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
		
		public override string Texture
		{
			get
			{
				return "Terraria/Projectile_" + ProjectileID.RubyBolt;
			}
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby Bolt");
			DisplayName.AddTranslation(GameCulture.Russian, "Красный снаряд");
		}
		
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.RubyBolt);
			aiType = ProjectileID.RubyBolt;
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
