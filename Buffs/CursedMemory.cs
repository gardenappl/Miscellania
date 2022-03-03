
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Buffs
{
	public class CursedMemory : ModBuff
	{

		public override void SetStaticDefaults()
		{
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
			Main.debuff[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = Math.Max(player.statLifeMax / 2, 100);
		}
	}
}
