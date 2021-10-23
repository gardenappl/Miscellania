
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Dusts
{
	public class RedDust : ModDust
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().DemonCrown;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
		
		public override void OnSpawn(Dust dust)
		{
			dust.frame = new Rectangle(0, Main.rand.Next(2) * 6, 6, 6);
			dust.noGravity = true;
		}
		
		public override bool Update(Dust dust)
		{
			dust.velocity.Y -= 0.02f;
			dust.velocity.X *= 0.98f;
			dust.position += dust.velocity;
			
			dust.alpha += 4;
			if(dust.alpha >= 200)
			{
				dust.active = false;
			}
			if(dust.alpha % 8 == 0)
			{
				dust.frame.Y = (dust.frame.Y == 0) ? 6 : 0;
			}
			if(!dust.noLight)
			{
				Lighting.AddLight(dust.position, Color.Green.ToVector3() * 0.3f);
			}
			return false;
		}
	}
}
