
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace GoldensMisc.Items.Tools
{
    //EXPERIMENTAL STUFF
    //This will probably end up in the Elerium mod

    public class IslandStaff : ModItem
    {
        public override bool IsLoadingEnabled (Mod mod)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.width = 42;
            Item.height = 42;
            Item.value = Item.sellPrice(0, 10);
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 100;
            Item.useTime = 100;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item4;
            //			Item.shoot = Mod.ProjectileType("Laputa");
            //			Item.shootSpeed = 20f;
            Item.noMelee = true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemRotation = -(float)Math.PI / 4f * player.direction;
        }

        int cloudX;
        int cloudY;

        public override bool CanUseItem(Player player)
        {
            if (player.position.Y / 16 < 170)
            {
                Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaCannotUse"), 30, 200, 255);
                Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaTooHigh"), 30, 200, 255);
                return false;
            }
            cloudX = (int)(MathHelper.Clamp(player.position.X / 16, 100, Main.maxTilesX - 100));
            cloudY = (int)(player.position.Y / 16) - Main.rand.Next(100, 120);
            for (int x = cloudX - 100; x < cloudX + 100; x++)
            {
                for (int y = cloudY - 50; y < cloudY + 30; y++)
                {
                    if (Main.tile[x, y].IsActive || Main.tile[x, y].wall != 0)
                    {
                        Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaCannotUse"), 30, 200, 255);
                        Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaBlocked"), 30, 200, 255);
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(null , player.Center.X, player.Center.Y - 1, 0, -20f, ModContent.ProjectileType<Projectiles.Laputa>(), 0, 0, player.whoAmI, player.Center.Y, cloudY);
            if (!Main.dedServ)
            {
                SkyManager.Instance.Activate("GoldensMisc:Laputa");
            }
            WorldGen.CloudIsland(cloudX, cloudY);

            var dirt = FindDirt(cloudX, cloudY);
            if (dirt != Point.Zero)
                WorldGen.SpreadGrass(dirt.X, dirt.Y);

            int trees = Main.rand.Next(40, 50);
            for (int i = 0; i < trees; i++)
            {
                dirt = FindDirt(cloudX, cloudY, TileID.Grass);
                if (dirt == Point.Zero)
                {
                    return true;
                }

                bool canPlaceTree = true;
                for (int x = dirt.X - 1; x <= dirt.X + 1; x++)
                {
                    if (!Main.tile[x, dirt.Y].IsActive || Main.tile[x, dirt.Y].type != TileID.Grass)
                    {
                        canPlaceTree = false;
                    }
                }

                for (int x = dirt.X - 2; x <= dirt.X + 2; x++)
                {
                    if (Main.tile[x, dirt.Y - 1].IsActive)
                    {
                        canPlaceTree = false;
                    }
                }

                if (canPlaceTree)
                {
                    WorldGen.PlaceObject(dirt.X, dirt.Y - 1, TileID.Saplings, true);
                    WorldGen.GrowTree(dirt.X, dirt.Y - 1);
                }
            }

            //			WorldGen.IslandHouse((int)(player.position.X / 16), (int)(player.position.Y / 16) - 100);
            return true;
        }

        static Point FindDirt(int cloudX, int cloudY, ushort dirt = TileID.Dirt)
        {
            int attempts = 0;

            while (attempts < 1000)
            {
                int x = cloudX + Main.rand.Next(-30, 30);
                int y = cloudY;
                if (!Main.tile[x, y].IsActive)
                {
                    attempts++;
                    continue;
                }
                while (Main.tile[x, y].IsActive)
                {
                    y--;
                }

                if (Main.tile[x, y + 1].type == dirt && !Main.tile[x, y].IsActive)
                {
                    return new Point(x, y + 1);
                }

                attempts++;
            }

            return Point.Zero;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentSolar, 15)
                .AddIngredient(ItemID.FragmentVortex, 15)
                .AddIngredient(ItemID.FragmentNebula, 15)
                .AddIngredient(ItemID.FragmentStardust, 15)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
