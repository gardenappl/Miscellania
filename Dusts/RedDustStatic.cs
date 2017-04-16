
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Dusts
{
	public class RedDustStatic : ModDust
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "GoldensMisc/Dusts/RedDust";
			return base.Autoload(ref name, ref texture);
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
