
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent.Creative;

namespace GoldensMisc.Items.Consumable
{
	public class ManaStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().MagicStones;
		}
		
		byte uses = 0;
		const byte maxUses = 60;

		public override ModItem Clone(Item item)
		{
			ManaStone clone = (ManaStone)base.Clone(item);
			clone.uses = 0;
			return clone;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.healMana = 100;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item29;
			Item.consumable = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(0, 8);
		}
		
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
		
		public override bool? UseItem(Player player)
		{
			uses++;
			if(uses >= maxUses)
			{
				Item.SetDefaults(ModContent.ItemType<InertStone>());
			}
			return true;
		}
		
		public override void HoldStyle(Player player, Rectangle heldItemFrame)
		{
			player.itemLocation.X -= 10 * player.direction;
			player.itemLocation.Y += 10 * player.gravDir;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<InertStone>())
				.AddIngredient(ItemID.ManaCrystal, 3)
				.AddIngredient(ItemID.CrystalShard, 5)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
		
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(uses);
		}
		
		public override void NetReceive(BinaryReader reader)
		{
			uses = reader.ReadByte();
		}

		public override void SaveData(TagCompound tag)
		{
			tag["u"] = uses;
		}

		public override void LoadData(TagCompound tag)
		{
			uses = tag.GetByte("u");
		}
	}
}
