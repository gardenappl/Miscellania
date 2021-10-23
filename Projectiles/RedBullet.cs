
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
			get { return (int)Projectile.localAI[0]; }
			set { Projectile.localAI[0] = value; }
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().DemonCrown;
		}

		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RubyBolt;
		
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.RubyBolt);
			AIType = ProjectileID.RubyBolt;
		}
		
		public override void AI()
		{
			DustDelay--;
			if(DustDelay <= 0)
			{
				var dustVel = Projectile.velocity * 0.5f + new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<RedDust>(), dustVel.X, dustVel.Y, Scale: 1.2f);
				DustDelay = 8;
			}
		}
	}
}
