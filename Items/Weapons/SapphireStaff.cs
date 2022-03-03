
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class SapphireStaff : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AltStaffs;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		
		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.mana = 6;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 18;
			Item.useAnimation = 33;
			Item.useTime = 33;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.SapphireBolt;
			Item.shootSpeed = 7.75f;
			Item.knockBack = 4f;
			Item.value = Item.sellPrice(0, 0, 25);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TungstenBar, 10)
				.AddIngredient(ItemID.Sapphire, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
