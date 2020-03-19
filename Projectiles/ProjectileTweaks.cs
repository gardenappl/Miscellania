
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class ProjectileTweaks : GlobalProjectile
	{
		public override void SetDefaults(Projectile projectile)
		{
			if(ModContent.GetInstance<ServerConfig>().AltStaffs)
			{
				switch(projectile.type)
				{
					case ProjectileID.SapphireBolt:
					case ProjectileID.EmeraldBolt:
						projectile.penetrate = 1;
						break;
					case ProjectileID.AmberBolt:
					case ProjectileID.RubyBolt:
						projectile.penetrate = 2;
						break;
				}
			}
		}
	}
}
