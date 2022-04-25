
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GoldensMisc.Projectiles;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Face)]
	public class DemonCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().DemonCrown;
		}
		
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 8);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 7;
			player.manaCost -= 0.1f;
			player.statManaMax2 += 40;
			player.GetModPlayer<MiscPlayer>().DemonCrown = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<RedCrystal>()] == 0)
				Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<RedCrystal>(), 60, 8, player.whoAmI);
		}
	}
}
