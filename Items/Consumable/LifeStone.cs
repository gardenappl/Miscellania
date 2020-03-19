
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GoldensMisc.Items.Consumable
{
	public class LifeStone : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.MagicStones;
		}
		
		byte uses = 0;
		const byte maxUses = 90;
		
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.healLife = 120;
			item.potion = true;
			item.useStyle = 4;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item29;
			item.consumable = true;
			item.rare = 4;
			item.value = Item.sellPrice(0, 8);
		}
		
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
		
		public override bool UseItem(Player player)
		{
			uses++;
			if(uses >= maxUses)
			{
				item.SetDefaults(ModContent.ItemType<InertStone>());
			}
			return true;
		}
		
		public override void HoldStyle(Player player)
		{
			player.itemLocation.X -= 10 * player.direction;
			player.itemLocation.Y += 10 * player.gravDir;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<InertStone>());
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddIngredient(ItemID.CrystalShard, 5);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override bool CloneNewInstances
		{
			get { return true; }
		}
		
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(uses);
		}
		
		public override void NetRecieve(BinaryReader reader)
		{
			uses = reader.ReadByte();
		}
		
		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"u", uses}
			};
		}
		
		public override void Load(TagCompound tag)
		{
			uses = tag.GetByte("u");
		}
	}
}
