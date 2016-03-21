
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items
{
	public class AncientMuramasa : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Muramasa);
			item.name = "Ancient Muramasa";
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), (int)(hitbox.Width * 1.25F), (int)(hitbox.Height * 1.25F), 29);
			Main.dust[dust].noGravity = true;
		}
	}
}
