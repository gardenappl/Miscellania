
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class RubyStaff : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.mana = 8;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 22;
			Item.useAnimation = 27;
			Item.useTime = 27;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.RubyBolt;
			Item.shootSpeed = 9.25f;
			Item.knockBack = 5f;
			Item.value = Item.sellPrice(0, 0, 45);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PlatinumBar, 10)
				.AddIngredient(ItemID.Ruby, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
