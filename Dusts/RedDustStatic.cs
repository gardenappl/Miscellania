
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Dusts
{
	public class RedDustStatic : ModDust
	{
		public override string Texture => "GoldensMisc/Dusts/RedDust";

		public override bool IsLoadingEnabled (Mod mod)
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
			dust.velocity.Y -= 0.015f;
			dust.velocity.X *= 0.97f;
			dust.position += dust.velocity;
			
			dust.scale -= 0.008f;
			if(dust.scale <= 0.7f)
			{
				dust.active = false;
			}
			if(!dust.noLight)
			{
				Lighting.AddLight(dust.position, Color.Green.ToVector3() * 0.3f);
			}
			return false;
		}
	}
}
