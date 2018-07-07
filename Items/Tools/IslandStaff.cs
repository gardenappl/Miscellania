
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Laputa");
            Tooltip.SetDefault("Creates a floating island\n" +
                               "Shatters after one use");

            DisplayName.AddTranslation(GameCulture.Russian, "Посох Лапуты");
            Tooltip.AddTranslation(GameCulture.Russian, "Создаёт парящий остров\n" +
                                   "Разрушается после одного использования");

            DisplayName.AddTranslation(GameCulture.Chinese, "空岛法杖");
            Tooltip.AddTranslation(GameCulture.Chinese, "创造一个浮动岛\n" +
                                   "使用一次后将损坏");

            var text = mod.CreateTranslation("LaputaCannotUse");
            text.SetDefault("Perhaps it's not a good idea to do this here.");
            text.AddTranslation(GameCulture.Russian, "Возможно, не стоит это делать здесь.");
            text.AddTranslation(GameCulture.Chinese, "也许在这里这么做并不是个好主意.");

            mod.AddTranslation(text);
            text = mod.CreateTranslation("LaputaTooHigh");
            text.SetDefault("You're too high above the ground.");
            text.AddTranslation(GameCulture.Russian, "Вы находитесь слишком высоко над землёй.");
            text.AddTranslation(GameCulture.Chinese, "你距离地面太高了.");

            mod.AddTranslation(text);
            text = mod.CreateTranslation("LaputaBlocked");
            text.SetDefault("Something is in the way.");
            text.AddTranslation(GameCulture.Russian, "Что-то закрывает над вами небо.");
            text.AddTranslation(GameCulture.Chinese, "似乎被什么东西挡住了");
        }

        public override void SetDefaults()
        {
            item.consumable = true;
            item.width = 42;
            item.height = 42;
            item.value = Item.sellPrice(0, 10);
            item.useTurn = false;
            item.useStyle = 4;
            item.useAnimation = 100;
            item.useTime = 100;
            item.rare = 10;
            item.UseSound = SoundID.Item4;
            //			item.shoot = mod.ProjectileType("Laputa");
            //			item.shootSpeed = 20f;
            item.noMelee = true;
        }

        public override void UseStyle(Player player)
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
                    if (Main.tile[x, y].active() || Main.tile[x, y].wall != 0)
                    {
                        Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaCannotUse"), 30, 200, 255);
                        Main.NewText(Language.GetTextValue("Mods.GoldensMisc.LaputaBlocked"), 30, 200, 255);
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y - 1, 0, -20f, mod.ProjectileType("Laputa"), 0, 0, player.whoAmI, player.Center.Y, cloudY);
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
                    if (!Main.tile[x, dirt.Y].active() || Main.tile[x, dirt.Y].type != TileID.Grass)
                    {
                        canPlaceTree = false;
                    }
                }

                for (int x = dirt.X - 2; x <= dirt.X + 2; x++)
                {
                    if (Main.tile[x, dirt.Y - 1].active())
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
                if (!Main.tile[x, y].active())
                {
                    attempts++;
                    continue;
                }
                while (Main.tile[x, y].active())
                {
                    y--;
                }

                if (Main.tile[x, y + 1].type == dirt && !Main.tile[x, y].active())
                {
                    return new Point(x, y + 1);
                }

                attempts++;
            }

            return Point.Zero;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
