using GoldensMisc.Tiles;
using static GoldensMisc.MiscUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Chat;

namespace GoldensMisc.Projectiles
{
    public class AutofisherBobber : ModProjectile
    {
        static readonly Color FishingLineColor = new(250, 90, 70, 100);

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BobberMechanics;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().Autofisher;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = -1;
            DrawOriginOffsetY = -8;
            //Projectile.bobber = true;
        }

        //ai[0] == 0 - fishing
        //ai[0] > 0 - reeling in item, < 0 reeling in NPC, ai[0] - ItemID , NPCID
        //ai[1] - tile entity ID
        public override void AI()
        {
            if (!TileEntity.ByID.ContainsKey((int)Projectile.ai[1]) || TileEntity.ByID[(int)Projectile.ai[1]] is not AutofisherTE)
            {
                Projectile.Kill();
                return;
            }
            var te = (AutofisherTE)TileEntity.ByID[(int)Projectile.ai[1]];
            if (te.GetCurrentBait() == null)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 60;
            if (Projectile.ai[0] != 0.0f)
            {

                if (Projectile.frameCounter == 0)
                {
                    Projectile.frameCounter = 1;
                    AutofisherReduceRemainingChumsInPool();
                }
                Projectile.tileCollide = false;
                const double productDivisor = 15.8999996185303;
                const int ten = 10;
                Vector2 Origin = new((float)(Projectile.position.X + Projectile.width * 0.5), (float)(Projectile.position.Y + Projectile.height * 0.5));
                double OriginXWorldCoord = (te.Position.X + 1.5f) * 16f - Origin.X;
                double OriginYWorldCoord = (te.Position.Y + 1) * 16f - Origin.Y;
                float positionSqrt = (float)Math.Sqrt(OriginXWorldCoord * OriginXWorldCoord + OriginYWorldCoord * OriginYWorldCoord);
                if (positionSqrt > 3000.0f)
                {
                    Projectile.Kill();
                }

                float divdSqr = (float)(productDivisor / (double)positionSqrt);
                float divdPosX = (float)OriginXWorldCoord * divdSqr;
                float divdPosY = (float)OriginYWorldCoord * divdSqr;
                Projectile.velocity.X = (float)((Projectile.velocity.X * (double)(ten - 1) + (double)divdPosX) / ten);
                Projectile.velocity.Y = (float)((Projectile.velocity.Y * (double)(ten - 1) + (double)divdPosY) / ten);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Rectangle projHitbox = new((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle teHitbox = new(te.Position.X * 16, te.Position.Y * 16, 48, 32);
                    if ((projHitbox).Intersects(teHitbox))
                    {
                        if (Projectile.ai[0] > 0.0f)
                        {
                            int Type = (int)Projectile.ai[0];
                            Item newItem = new();
                            newItem.SetDefaults(Type, true);
                            if (Type == ItemID.BombFish)
                            {
                                int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
                                int minValue = (FinalFishingLevel / 20 + 3) / 2;
                                int maxValue = (FinalFishingLevel / 10 + 6) / 2;
                                if (Main.rand.Next(50) < FinalFishingLevel)
                                    ++maxValue;
                                if (Main.rand.Next(100) < FinalFishingLevel)
                                    ++maxValue;
                                if (Main.rand.Next(150) < FinalFishingLevel)
                                    ++maxValue;
                                if (Main.rand.Next(200) < FinalFishingLevel)
                                    ++maxValue;
                                int count = Main.rand.Next(minValue, maxValue + 1);
                                newItem.stack = count;
                            }
                            if (Type == ItemID.FrostDaggerfish)
                            {
                                int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
                                int minValue = (FinalFishingLevel / 4 + 15) / 2;
                                int maxValue = (FinalFishingLevel / 2 + 30) / 2;
                                if (Main.rand.Next(50) < FinalFishingLevel)
                                    maxValue += 4;
                                if (Main.rand.Next(100) < FinalFishingLevel)
                                    maxValue += 4;
                                if (Main.rand.Next(150) < FinalFishingLevel)
                                    maxValue += 4;
                                if (Main.rand.Next(200) < FinalFishingLevel)
                                    maxValue += 4;
                                int count = Main.rand.Next(minValue, maxValue + 1);
                                newItem.stack = count;
                            }
                            ItemLoader.CaughtFishStack(newItem);
                            newItem.newAndShiny = true;
                            Item.NewItem(new EntitySource_TileEntity(te), te.Position.ToWorldCoordinates(0f, 0f), 48, 32, newItem.type, newItem.stack);
                        }
                        else
                        {
                            if (Projectile.ai[0] < 0)
                            {
                                int type = (int)(Projectile.ai[0] * -1);
                                Point p = new((int)Projectile.position.X, (int)Projectile.position.Y);
                                if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    NetMessage.SendData(MessageID.FishOutNPC, -1, -1, null, p.X / 16, (float)(p.Y / 16), (float)type, 0f, 0, 0, 0);
                                }
                                else
                                {
                                    int nPCi = NPC.NewNPC(new EntitySource_TileEntity(te), p.X, p.Y, type);
                                    if (Main.npc[nPCi].boss)
                                    {
                                        if (Main.netMode == NetmodeID.SinglePlayer)
                                        {
                                            Main.NewText(Language.GetTextValue("Announcement.HasAwoken", Main.npc[nPCi].GetTypeNetName()), 175, 75, byte.MaxValue);
                                        }
                                        else
                                        {
                                            if (Main.netMode == NetmodeID.Server)
                                            {
                                                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[] {
                                                Main.npc[nPCi].GetTypeNetName()}), new Color(175, 75, 255), -1);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        Projectile.Kill();
                    }
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }
            else
            {

                if (Projectile.wet)
                {
                    Projectile.rotation = 0.0f;
                    Projectile.velocity.X *= 0.9f;
                    if (Projectile.velocity.Y > 0.0f)
                    {
                        Projectile.velocity.Y *= 0.5f;
                    }
                    int BobberCenterTileX = (int)(Projectile.Center.X / 16f);
                    int BobberCenterTileY = (int)(Projectile.Center.Y / 16f);
                    float waterLine = AutoFisherBobber_GetWaterLine(BobberCenterTileX, BobberCenterTileY);
                    if (Projectile.Center.Y > waterLine)
                    {
                        Projectile.velocity.Y -= 0.1f;
                        if (Projectile.velocity.Y < -8.0f)
                            Projectile.velocity.Y = -8.0f;
                        if (Projectile.Center.Y + Projectile.velocity.Y < waterLine)
                            Projectile.velocity.Y = waterLine - Projectile.Center.Y;
                    }
                    else
                        Projectile.velocity.Y = waterLine - Projectile.Center.Y;
                }
                else
                {
                    if (Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X *= 0.95f;
                    }
                    Projectile.velocity.X *= 0.98f;
                    Projectile.velocity.Y += 0.2f;
                    if (Projectile.velocity.Y > 15.9f)
                        Projectile.velocity.Y = 15.9f;
                }
            }
        }

        public int FishingCheck(bool catchRealFish)
        {
            AutofisherTE te = (AutofisherTE)TileEntity.ByID[(int)Projectile.ai[1]];
            AutoFishingAttempt autoFisher = new();
            autoFisher.baitItem = te.GetCurrentBait();
            autoFisher.canFishInLava = ItemID.Sets.IsLavaBait[autoFisher.baitItem.type];
            int bobberTileX = (int)(Projectile.Center.X / 16f);
            int bobberTileY = (int)(Projectile.Center.Y / 16f);
            autoFisher.position.X = bobberTileX;
            autoFisher.position.Y = bobberTileY;
            GetAutoFishingPondState(autoFisher.position.X, autoFisher.position.Y, out autoFisher.inLava, out autoFisher.inHoney, out autoFisher.poolSize, out autoFisher.chumsInWater);
            if (autoFisher.poolSize < 75)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    te.DisplayedFishingInfo = Language.GetTextValue("GameUI.NotEnoughWater");
            }
            else
            {
                autoFisher.fishingPower = te.GetFishingLevel(autoFisher.baitItem);
                if (autoFisher.fishingPower == 0)
                    return -1;
                if (autoFisher.chumsInWater == 0)
                {
                    //do nothing
                }
                else if (autoFisher.chumsInWater == 1)
                {
                    autoFisher.fishingPower += 11;
                }
                else if (autoFisher.chumsInWater == 2)
                {
                    autoFisher.fishingPower += 17;
                }
                else if (autoFisher.chumsInWater > 2)
                {
                    autoFisher.fishingPower += 20;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingPower", (object)autoFisher.fishingPower);

                if (autoFisher.baitItem.type == ItemID.TruffleWorm)
                {
                    te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
                    if (!((autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000 && !NPC.AnyNPCs(NPCID.DukeFishron)))
                    {
                        return -1;
                    }

                }
                const float maxPoolSize = 300f;
                float worldSize = ((float)Main.maxTilesX / 4200f);
                autoFisher.atmosphere = (float)((Projectile.position.Y / 16.0f - (60.0f + 10.0f * (worldSize * worldSize))) / (Main.worldSurface / 6.0f));
                autoFisher.atmosphere = Math.Clamp(autoFisher.atmosphere, 0.25f, 1f);
                autoFisher.waterNeededToFish = (int)(maxPoolSize * autoFisher.atmosphere);
                autoFisher.waterQuality = autoFisher.poolSize / (float)autoFisher.waterNeededToFish;
                if (autoFisher.waterQuality < 1.0f)
                    autoFisher.fishingPower = (int)(autoFisher.fishingPower * autoFisher.waterQuality);
                float invertedWaterQuality = 1f - autoFisher.waterQuality;
                if (autoFisher.poolSize < autoFisher.waterNeededToFish && Main.netMode != NetmodeID.MultiplayerClient)
                    te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FullFishingPower", autoFisher.fishingPower, -Math.Round(invertedWaterQuality * 100.0));
                int odds = (autoFisher.fishingPower + 75) / 2;

                if (!catchRealFish)
                    return -1;

                if (Main.rand.Next(3) > odds)
                    return -1;
                bool isSky = autoFisher.position.Y < Main.worldSurface * 0.5;
                if (isSky)
                {
                    autoFisher.heightLevel = 0;
                }
                else
                {
                    bool isSurface = autoFisher.position.Y < Main.worldSurface;
                    if (isSurface)
                    {
                        autoFisher.heightLevel = 1;
                    }
                    else
                    {
                        bool isUnderground = autoFisher.position.Y < Main.rockLayer;
                        if (isUnderground)
                        {
                            autoFisher.heightLevel = 2;
                        }
                        else
                        {
                            bool isCavern = autoFisher.position.Y < Main.maxTilesY - 300;
                            if (isCavern)
                            {
                                autoFisher.heightLevel = 3;
                            }
                            else
                            {
                                //Welcome to Hell
                                autoFisher.heightLevel = 4;
                            }
                        }
                    }
                }
                autoFisher.zone = MiscUtils.GetZoneInLocation(te.Position.X, te.Position.Y);
                autoFisher.isJunk = false;
                // Vanilla Fishing Algorithm
                AutofisherCheck_RollDropLevels(ref autoFisher);
                AutofisherCheck_ProbeForQuestFish(ref autoFisher);
                AutofisherCheck_RollEnemySpawns(ref autoFisher);
                AutofisherCheck_RollItemDrop(ref autoFisher);
                AutofisherHooks.CatchFish(te, autoFisher.zone, autoFisher.baitItem, autoFisher.fishingPower, autoFisher.inLava ? 1 : (autoFisher.inHoney ? 2 : 0), autoFisher.poolSize, autoFisher.heightLevel, ref autoFisher.caughtItem, ref autoFisher.isJunk);
                AutofisherHooks.CatchEnemy(te, autoFisher.zone, autoFisher.baitItem, autoFisher.fishingPower, autoFisher.inLava ? 1 : (autoFisher.inHoney ? 2 : 0), autoFisher.poolSize, autoFisher.heightLevel, ref autoFisher.caughtEnemy);
                if (autoFisher.caughtItem <= 0 && autoFisher.caughtEnemy <= 0)
                    return -1;

                if (autoFisher.caughtEnemy == 0)
                {
                    Projectile.ai[0] = autoFisher.caughtItem;
                    Projectile.netUpdate = true;
                    return autoFisher.isJunk ? -2 : autoFisher.caughtItem;
                }
                else
                {
                    Projectile.ai[0] = (autoFisher.caughtEnemy * -1);
                    Projectile.netUpdate = true;
                    return autoFisher.caughtEnemy;
                }

            }
            return -1;
        }

        static void AutofisherCheck_RollDropLevels(ref AutoFishingAttempt autoFisher)
        {
            int commonChance = 150 / autoFisher.fishingPower;
            int uncommonChance = 300 / autoFisher.fishingPower;
            int rareChance = 1050 / autoFisher.fishingPower;
            int veryRareChance = 2250 / autoFisher.fishingPower;
            int legendaryChance = 4500 / autoFisher.fishingPower;
            const int crateChance = 10;
            commonChance = Math.Max(commonChance, 2);
            uncommonChance = Math.Max(uncommonChance, 3);
            rareChance = Math.Max(rareChance, 4);
            veryRareChance = Math.Max(veryRareChance, 5);
            legendaryChance = Math.Max(legendaryChance, 6);
            autoFisher.common = Main.rand.NextBool(commonChance);
            autoFisher.uncommon = Main.rand.NextBool(uncommonChance);
            autoFisher.rare = Main.rand.NextBool(rareChance);
            autoFisher.veryrare = Main.rand.NextBool(veryRareChance);
            autoFisher.legendary = Main.rand.NextBool(legendaryChance);
            autoFisher.crate = (Main.rand.Next(100) < crateChance);
        }

        static void AutofisherCheck_ProbeForQuestFish(ref AutoFishingAttempt autoFisher)
        {
            autoFisher.questFish = Main.anglerQuestItemNetIDs[Main.anglerQuest];
            bool isAnglerActive = NPC.AnyNPCs(NPCID.Angler);
            if (!isAnglerActive)
                autoFisher.questFish = -1;
            bool anglerQuestFinished = Main.anglerQuestFinished;
            if (anglerQuestFinished)
                autoFisher.questFish = -1;
        }

        static void AutofisherCheck_RollEnemySpawns(ref AutoFishingAttempt autoFisher)
        {
            if (!(autoFisher.inLava || autoFisher.inHoney))
            {
                if (Main.bloodMoon && !Main.dayTime)
                {
                    if (!NPC.unlockedSlimeRedSpawn && Main.rand.NextBool(5))
                    {
                        autoFisher.caughtEnemy = NPCID.TownSlimeRed;
                        return;
                    }
                    const int enemyOdds = 6;
                    if (Main.rand.NextBool(enemyOdds))
                    {
                        if (Main.hardMode)
                        {
                            autoFisher.caughtEnemy = Utils.SelectRandom<short>(Main.rand, new short[]
                            {
                            NPCID.GoblinShark,
                            NPCID.BloodEelHead,
                            NPCID.ZombieMerman,
                            NPCID.EyeballFlyingFish
                            });
                            if (Main.rand.NextBool(10))
                            {
                                autoFisher.caughtEnemy = NPCID.BloodNautilus;
                            }
                        }
                        else
                        {
                            autoFisher.caughtEnemy = Utils.SelectRandom<short>(Main.rand, new short[]
                            {
                            NPCID.EyeballFlyingFish,
                            NPCID.ZombieMerman
                            });
                        }
                    }
                }
                if (autoFisher.baitItem.type == ItemID.TruffleWorm && (autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000 && !NPC.AnyNPCs(NPCID.DukeFishron))
                {
                    autoFisher.caughtEnemy = NPCID.DukeFishron;
                }
            }

        }
        static void AutofisherCheck_RollItemDrop(ref AutoFishingAttempt autoFisher)
        {
            if (!(autoFisher.caughtEnemy > 0))
            {
                bool remixedSky = Main.remixWorld && autoFisher.heightLevel == 0;
                bool notNotTheBees = !(Main.notTheBeesWorld && !Main.remixWorld && Main.rand.NextBool(2));
                bool isCorrupt = !remixedSky && autoFisher.zone.HasFlag(Zone.Corrupt);
                bool isCrimson = !remixedSky && autoFisher.zone.HasFlag(Zone.Crimson);
                bool isJungle = notNotTheBees && autoFisher.zone.HasFlag(Zone.Jungle);
                bool isSnow = autoFisher.zone.HasFlag(Zone.Snow);
                bool isDungeon = NPC.downedBoss3 && autoFisher.zone.HasFlag(Zone.Dungeon);
                bool isShroom = autoFisher.zone.HasFlag(Zone.Shroom);
                bool isDesert = autoFisher.zone.HasFlag(Zone.Desert);
                bool isBeach = autoFisher.zone.HasFlag(Zone.Beach);
                bool isHallowed = autoFisher.zone.HasFlag(Zone.Hallow);
                if (isCorrupt && isCrimson)
                    if (Main.rand.NextBool(2))
                        isCrimson = false;
                    else
                        isCorrupt = false;
                if (autoFisher.inLava)
                {
                    if (autoFisher.canFishInLava)
                    {
                        if (autoFisher.crate && Main.rand.NextBool(6))
                        {
                            autoFisher.caughtItem = (Main.hardMode ? ItemID.LavaCrateHard : ItemID.LavaCrate);
                            return;
                        }
                        if (autoFisher.legendary && Main.hardMode && Main.rand.NextBool(3))
                        {
                            autoFisher.caughtItem = Main.rand.NextFromList(new short[]
                            {
                                ItemID.DemonConch,
                                ItemID.BottomlessLavaBucket,
                                ItemID.LavaAbsorbantSponge,
                                ItemID.ObsidianSwordfish
                            });
                            return;
                        }
                        if (autoFisher.legendary && !Main.hardMode && Main.rand.NextBool(3))
                        {
                            autoFisher.caughtItem = Main.rand.NextFromList(new short[]
                            {
                                ItemID.DemonConch,
                                ItemID.BottomlessLavaBucket,
                                ItemID.LavaAbsorbantSponge
                            });
                            return;
                        }
                        if (autoFisher.veryrare)
                        {
                            autoFisher.caughtItem = ItemID.FlarefinKoi;
                            return;
                        }
                        if (autoFisher.rare)
                        {
                            autoFisher.caughtItem = ItemID.Obsidifish;
                        }
                    }
                    return;
                }
                if (autoFisher.inHoney)
                {
                    if (autoFisher.rare || (autoFisher.uncommon && Main.rand.NextBool(2)))
                    {
                        autoFisher.caughtItem = ItemID.Honeyfin;
                        return;
                    }
                    if (autoFisher.uncommon && autoFisher.questFish == ItemID.BumblebeeTuna)
                    {
                        autoFisher.caughtItem = ItemID.BumblebeeTuna;
                    }
                    return;
                }
                else
                {
                    if (Main.rand.Next(50) > autoFisher.fishingPower && Main.rand.Next(50) > autoFisher.fishingPower && autoFisher.poolSize < autoFisher.waterNeededToFish)
                    {
                        autoFisher.caughtItem = Main.rand.Next(ItemID.OldShoe, ItemID.TinCan + 1);
                        if (Main.rand.NextBool(8))
                        {
                            autoFisher.caughtItem = ItemID.JojaCola;
                        }
                        return;
                    }
                    if (autoFisher.crate)
                    {
                        bool hardMode = Main.hardMode;
                        if (autoFisher.rare && isDungeon)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.DungeonFishingCrateHard : ItemID.DungeonFishingCrate);
                            return;
                        }
                        if (autoFisher.rare && (isBeach || (Main.remixWorld && autoFisher.heightLevel == 1 && autoFisher.position.Y >= Main.rockLayer && Main.rand.NextBool(2))))
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.OceanCrateHard : ItemID.OceanCrate);
                            return;
                        }
                        if (autoFisher.rare && isCorrupt)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.CorruptFishingCrateHard : ItemID.CorruptFishingCrate);
                            return;
                        }
                        if (autoFisher.rare && isCrimson)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.CrimsonFishingCrateHard : ItemID.CrimsonFishingCrate);
                            return;
                        }
                        if (autoFisher.rare && isHallowed)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.HallowedFishingCrateHard : ItemID.HallowedFishingCrate);
                            return;
                        }
                        if (autoFisher.rare && isJungle)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.JungleFishingCrateHard : ItemID.JungleFishingCrate);
                            return;
                        }
                        if (autoFisher.rare && isSnow)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.FrozenCrateHard : ItemID.FrozenCrate);
                            return;
                        }
                        if (autoFisher.rare && isDesert)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.OasisCrateHard : ItemID.OasisCrate);
                            return;
                        }
                        if (autoFisher.rare && autoFisher.heightLevel == 0)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.FloatingIslandFishingCrateHard : ItemID.FloatingIslandFishingCrate);
                            return;
                        }
                        if (autoFisher.veryrare || autoFisher.legendary)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.GoldenCrateHard : ItemID.GoldenCrate);
                            return;
                        }
                        if (autoFisher.uncommon)
                        {
                            autoFisher.caughtItem = (hardMode ? ItemID.IronCrateHard : ItemID.IronCrate);
                            return;
                        }
                        autoFisher.caughtItem = (hardMode ? ItemID.WoodenCrateHard : ItemID.WoodenCrate);
                        return;
                    }
                    else
                    {
                        if (!NPC.combatBookWasUsed && Main.bloodMoon && autoFisher.legendary && Main.rand.NextBool(3))
                        {
                            autoFisher.caughtItem = ItemID.CombatBook;
                            return;
                        }
                        if (Main.bloodMoon && autoFisher.legendary && Main.rand.NextBool(2))
                        {
                            autoFisher.caughtItem = ItemID.DreadoftheRedSea;
                            return;
                        }
                        if (autoFisher.legendary && Main.rand.NextBool(5))
                        {
                            autoFisher.caughtItem = ItemID.FrogLeg;
                            return;
                        }
                        if (autoFisher.legendary && Main.rand.NextBool(5))
                        {
                            autoFisher.caughtItem = ItemID.BalloonPufferfish;
                            return;
                        }
                        if (autoFisher.legendary && Main.rand.NextBool(10))
                        {
                            autoFisher.caughtItem = ItemID.ZephyrFish;
                            return;
                        }
                        if (!autoFisher.legendary && !autoFisher.veryrare && autoFisher.uncommon && Main.rand.NextBool(5))
                        {
                            autoFisher.caughtItem = ItemID.BombFish;
                            return;
                        }
                        if (isDungeon)
                        {
                            isDesert = false;
                            if (autoFisher.caughtItem == 0 && autoFisher.veryrare && Main.rand.NextBool(7))
                            {
                                autoFisher.caughtItem = ItemID.AlchemyTable;
                            }
                        }
                        else
                        {
                            if (isCorrupt)
                            {
                                if (autoFisher.legendary && Main.hardMode && isSnow && autoFisher.heightLevel == 3 && !Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.ScalyTruffle;
                                }
                                else if (autoFisher.legendary && Main.hardMode && Main.rand.NextBool(2))
                                {
                                    autoFisher.caughtItem = ItemID.Toxikarp;
                                }
                                else if (autoFisher.rare)
                                {
                                    autoFisher.caughtItem = ItemID.PurpleClubberfish;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.Cursedfish)
                                {
                                    autoFisher.caughtItem = ItemID.Cursedfish;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.InfectedScabbardfish)
                                {
                                    autoFisher.caughtItem = ItemID.InfectedScabbardfish;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.EaterofPlankton)
                                {
                                    autoFisher.caughtItem = ItemID.EaterofPlankton;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.Ebonkoi;
                                }
                            }
                            else if (isCrimson)
                            {
                                if (autoFisher.legendary && Main.hardMode && isSnow && autoFisher.heightLevel == 3 && !Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.ScalyTruffle;
                                }
                                else if (autoFisher.legendary && Main.hardMode && Main.rand.NextBool(2))
                                {
                                    autoFisher.caughtItem = ItemID.Bladetongue;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.BloodyManowar)
                                {
                                    autoFisher.caughtItem = ItemID.BloodyManowar;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.Ichorfish)
                                {
                                    autoFisher.caughtItem = ItemID.Ichorfish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.Hemopiranha;
                                }
                                else if (autoFisher.common)
                                {
                                    autoFisher.caughtItem = ItemID.CrimsonTigerfish;
                                }
                            }
                            else if (isHallowed)
                            {
                                if (isDesert && Main.rand.NextBool(2))
                                {
                                    if (autoFisher.uncommon && autoFisher.questFish == ItemID.ScarabFish)
                                    {
                                        autoFisher.caughtItem = ItemID.ScarabFish;
                                    }
                                    else if (autoFisher.uncommon && autoFisher.questFish == ItemID.ScorpioFish)
                                    {
                                        autoFisher.caughtItem = ItemID.ScorpioFish;
                                    }
                                    else if (autoFisher.uncommon)
                                    {
                                        autoFisher.caughtItem = ItemID.Oyster;
                                    }
                                    else if (Main.rand.NextBool(3))
                                    {
                                        autoFisher.caughtItem = ItemID.RockLobster;
                                    }
                                    else
                                    {
                                        autoFisher.caughtItem = ItemID.Flounder;
                                    }
                                }
                                else if (autoFisher.legendary && Main.hardMode && isSnow && autoFisher.heightLevel == 3 && !Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.ScalyTruffle;
                                }
                                else if (autoFisher.legendary && Main.hardMode && !Main.rand.NextBool(2))
                                {
                                    autoFisher.caughtItem = ItemID.CrystalSerpent;
                                }
                                else if (autoFisher.legendary && Main.hardMode && !Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.LadyOfTheLake;
                                }
                                else if (autoFisher.heightLevel > 1 && autoFisher.veryrare)
                                {
                                    autoFisher.caughtItem = ItemID.ChaosFish;
                                }
                                else if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.MirageFish)
                                {
                                    autoFisher.caughtItem = ItemID.MirageFish;
                                }
                                else if (autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Pixiefish)
                                {
                                    autoFisher.caughtItem = ItemID.Pixiefish;
                                }
                                else if (autoFisher.rare)
                                {
                                    autoFisher.caughtItem = ItemID.Prismite;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.UnicornFish)
                                {
                                    autoFisher.caughtItem = ItemID.UnicornFish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.PrincessFish;
                                }
                            }
                            if (autoFisher.caughtItem == 0 && isShroom && autoFisher.uncommon && autoFisher.questFish == ItemID.AmanitaFungifin)
                            {
                                autoFisher.caughtItem = ItemID.AmanitaFungifin;
                            }
                            if (isSnow && isJungle && Main.rand.NextBool(2))
                            {
                                isSnow = false;
                            }
                            if (autoFisher.caughtItem == 0 && isSnow)
                            {
                                if (autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Pengfish)
                                {
                                    autoFisher.caughtItem = ItemID.Pengfish;
                                }
                                else if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.TundraTrout)
                                {
                                    autoFisher.caughtItem = ItemID.TundraTrout;
                                }
                                else if (autoFisher.heightLevel >= 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Fishron)
                                {
                                    autoFisher.caughtItem = ItemID.Fishron;
                                }
                                else if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.MutantFlinxfin)
                                {
                                    autoFisher.caughtItem = ItemID.MutantFlinxfin;
                                }
                                else if ((autoFisher.common && Main.rand.NextBool(12)) || (autoFisher.uncommon && Main.rand.NextBool(6)))
                                {
                                    autoFisher.caughtItem = ItemID.FrostDaggerfish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.FrostMinnow;
                                }
                                else if (autoFisher.common)
                                {
                                    autoFisher.caughtItem = ItemID.AtlanticCod;
                                }
                                else if (autoFisher.heightLevel > 1 && Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.SpecularFish;
                                }
                            }
                            if (autoFisher.caughtItem == 0 && isJungle)
                            {
                                if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Catfish)
                                {
                                    autoFisher.caughtItem = ItemID.Catfish;
                                }
                                else if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Derpfish)
                                {
                                    autoFisher.caughtItem = ItemID.Derpfish;
                                }
                                else if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.TropicalBarracuda)
                                {
                                    autoFisher.caughtItem = ItemID.TropicalBarracuda;
                                }
                                else if (autoFisher.heightLevel >= 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Mudfish)
                                {
                                    autoFisher.caughtItem = ItemID.Mudfish;
                                }
                                else if (autoFisher.heightLevel > 1 && autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.VariegatedLardfish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.DoubleCod;
                                }
                                else if (autoFisher.common)
                                {
                                    autoFisher.caughtItem = ItemID.NeonTetra;
                                }
                            }
                        }
                        if (autoFisher.caughtItem == 0)
                        {
                            if ((Main.remixWorld && autoFisher.heightLevel == 1 && autoFisher.position.Y >= Main.rockLayer && Main.rand.NextBool(3)) || (autoFisher.heightLevel <= 1 && (autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000))
                            {
                                if (autoFisher.veryrare && Main.rand.NextBool(2))
                                {
                                    autoFisher.caughtItem = ItemID.ReaverShark;
                                }
                                else if (autoFisher.veryrare)
                                {
                                    autoFisher.caughtItem = ItemID.SawtoothShark;
                                }
                                else if (autoFisher.rare && Main.rand.NextBool(5))
                                {
                                    autoFisher.caughtItem = ItemID.PinkJellyfish;
                                }
                                else if (autoFisher.rare && Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.Swordfish;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.CapnTunabeard)
                                {
                                    autoFisher.caughtItem = ItemID.CapnTunabeard;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.Clownfish)
                                {
                                    autoFisher.caughtItem = ItemID.Clownfish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.Shrimp;
                                }
                                else if (autoFisher.common && Main.rand.NextBool(2))
                                {
                                    autoFisher.caughtItem = ItemID.RedSnapper;
                                }
                                else if (autoFisher.common)
                                {
                                    autoFisher.caughtItem = ItemID.Tuna;
                                }
                                else
                                {
                                    autoFisher.caughtItem = ItemID.Trout;
                                }
                            }
                            else if (isDesert)
                            {
                                if (autoFisher.uncommon && autoFisher.questFish == ItemID.ScarabFish)
                                {
                                    autoFisher.caughtItem = ItemID.ScarabFish;
                                }
                                else if (autoFisher.uncommon && autoFisher.questFish == ItemID.ScorpioFish)
                                {
                                    autoFisher.caughtItem = ItemID.ScorpioFish;
                                }
                                else if (autoFisher.uncommon)
                                {
                                    autoFisher.caughtItem = ItemID.Oyster;
                                }
                                else if (Main.rand.NextBool(3))
                                {
                                    autoFisher.caughtItem = ItemID.RockLobster;
                                }
                                else
                                {
                                    autoFisher.caughtItem = ItemID.Flounder;
                                }
                            }
                        }
                        if (autoFisher.caughtItem != 0)
                        {
                            return;
                        }
                        if (autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Harpyfish)
                        {
                            autoFisher.caughtItem = ItemID.Harpyfish;
                            return;
                        }
                        if (autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == ItemID.Cloudfish)
                        {
                            autoFisher.caughtItem = ItemID.Cloudfish;
                            return;
                        }
                        if (autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == ItemID.Wyverntail)
                        {
                            autoFisher.caughtItem = ItemID.Wyverntail;
                            return;
                        }
                        if (autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == ItemID.Angelfish)
                        {
                            autoFisher.caughtItem = ItemID.Angelfish;
                            return;
                        }
                        if (autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.FallenStarfish)
                        {
                            autoFisher.caughtItem = ItemID.FallenStarfish;
                            return;
                        }
                        if (autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.TheFishofCthulu)
                        {
                            autoFisher.caughtItem = ItemID.TheFishofCthulu;
                            return;
                        }
                        if (autoFisher.heightLevel == 0 && autoFisher.uncommon)
                        {
                            autoFisher.caughtItem = ItemID.Damselfish;
                            return;
                        }
                        if (autoFisher.heightLevel > 0 && autoFisher.heightLevel < 3 && autoFisher.uncommon && autoFisher.questFish == ItemID.Dirtfish)
                        {
                            autoFisher.caughtItem = ItemID.Dirtfish;
                            return;
                        }
                        if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Bunnyfish)
                        {
                            autoFisher.caughtItem = ItemID.Bunnyfish;
                            return;
                        }
                        if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.DynamiteFish)
                        {
                            autoFisher.caughtItem = ItemID.DynamiteFish;
                            return;
                        }
                        if (autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.ZombieFish)
                        {
                            autoFisher.caughtItem = ItemID.ZombieFish;
                            return;
                        }
                        if (autoFisher.heightLevel > 1 && autoFisher.rare && Main.rand.NextBool(5))
                        {
                            if (Main.hardMode && Main.rand.NextBool(2))
                            {
                                autoFisher.caughtItem = ItemID.GreenJellyfish;
                                return;
                            }
                            autoFisher.caughtItem = ItemID.BlueJellyfish;
                            return;
                        }
                        else
                        {
                            if (autoFisher.heightLevel > 1 && autoFisher.legendary && !Main.rand.NextBool(3))
                            {
                                autoFisher.caughtItem = ItemID.GoldenCarp;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.veryrare && Main.rand.NextBool(2))
                            {
                                autoFisher.caughtItem = ItemID.Rockfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.rare)
                            {
                                autoFisher.caughtItem = ItemID.Stinkfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Bonefish)
                            {
                                autoFisher.caughtItem = ItemID.Bonefish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Batfish)
                            {
                                autoFisher.caughtItem = ItemID.Batfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Jewelfish)
                            {
                                autoFisher.caughtItem = ItemID.Jewelfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == ItemID.Spiderfish)
                            {
                                autoFisher.caughtItem = ItemID.Spiderfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Hungerfish)
                            {
                                autoFisher.caughtItem = ItemID.Hungerfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.DemonicHellfish)
                            {
                                autoFisher.caughtItem = ItemID.DemonicHellfish;
                                return;
                            }
                            if (autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.GuideVoodooFish)
                            {
                                autoFisher.caughtItem = ItemID.GuideVoodooFish;
                                return;
                            }
                            if (autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == ItemID.Fishotron)
                            {
                                autoFisher.caughtItem = ItemID.Fishotron;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && autoFisher.uncommon && !Main.rand.NextBool(4))
                            {
                                autoFisher.caughtItem = ItemID.ArmoredCavefish;
                                return;
                            }
                            if (autoFisher.heightLevel > 1 && (autoFisher.uncommon || autoFisher.common || Main.rand.NextBool(4)))
                            {
                                if (Main.rand.NextBool(4))
                                {
                                    autoFisher.caughtItem = ItemID.ArmoredCavefish;
                                    return;
                                }
                                autoFisher.caughtItem = ItemID.SpecularFish;
                                return;
                            }
                            else
                            {
                                if (autoFisher.uncommon && autoFisher.questFish == ItemID.Slimefish)
                                {
                                    autoFisher.caughtItem = ItemID.Slimefish;
                                    return;
                                }
                                if (autoFisher.poolSize > 1000 && autoFisher.common)
                                {
                                    autoFisher.caughtItem = ItemID.Salmon;
                                    return;
                                }
                                autoFisher.caughtItem = ItemID.Bass;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static void GetAutoFishingPondWidth(int x, int y, out int minX, out int maxX)
        {
            minX = x;
            maxX = x;
            while (minX > 9 && Main.tile[(minX - 1), y].LiquidAmount > 0 && !WorldGen.SolidTile((minX - 1), y, false))
            {
                minX--;
            }
            while (maxX < Main.maxTilesX - 9 && Main.tile[(maxX + 1), y].LiquidAmount > 0 && !WorldGen.SolidTile((maxX + 1), y, false))
            {
                maxX++;
            }
        }


        private static void GetAutoFishingPondState(int x, int y, out bool lava, out bool honey, out int numWaters, out int chumCount)
        {
            lava = false;
            honey = false;
            numWaters = 0;
            chumCount = 0;
            Point tileCoords = new(0, 0);
            GetAutoFishingPondWidth(x, y, out int minX, out int maxX);
            for (int i = minX; i <= maxX; i++)
            {
                int num = y;
                while (Main.tile[i, num].LiquidAmount > 0 && !WorldGen.SolidTile(i, num, false) && num < Main.maxTilesY - 10)
                {
                    numWaters++;
                    num++;
                    if (HasLava(Main.tile[i, num]))
                    {
                        lava = true;
                    }
                    else
                    {
                        if (HasHoney(Main.tile[i, num]))
                        {
                            honey = true;
                        }
                    }
                    tileCoords.X = i;
                    tileCoords.Y = num;

                    chumCount += Main.instance.ChumBucketProjectileHelper.GetChumsInLocation(tileCoords);
                }
            }
            if (honey)// boost tile count in honey
            {
                numWaters = (int)(numWaters * 1.5f);
            }
        }

        private float AutoFisherBobber_GetWaterLine(int X, int Y)
        {
            float result = Projectile.position.Y + Projectile.height;
            Tile tile1 = Main.tile[X, Y - 1];
            Tile tile2 = Main.tile[X, Y];
            Tile tile3 = Main.tile[X, Y + 1];
            if (tile1 != null && tile1.LiquidAmount > 0)
            {
                result = Y * 16;
                result -= Main.tile[X, Y - 1].LiquidAmount / 16;
            }
            else
            {
                if (tile2 != null && tile2.LiquidAmount > 0)
                {
                    result = ((Y + 1) * 16);
                    result -= (Main.tile[X, Y].LiquidAmount / 16);
                }
                else
                {
                    if (tile3 != null && tile3.LiquidAmount > 0)
                    {
                        result = ((Y + 2) * 16);
                        result -= (Main.tile[X, Y + 1].LiquidAmount / 16);
                    }
                }
            }
            return result;
        }

        private void AutofisherReduceRemainingChumsInPool()
        {

            int bobberTileX = (int)((Projectile.Center.X / 16f) + 1);
            int bobberTileY = (int)((Projectile.Center.Y / 16f) + 1);
            List<Tuple<int, Point>> chums = new();
            List<int> chumsInPool = new();
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.owner == Main.myPlayer && projectile.timeLeft > 60 && projectile.type == ProjectileID.ChumBucket)
                {
                    Tuple<int, Point> tple = new(i, (projectile.Center + Vector2.UnitY * 16f).ToTileCoordinates());
                    chums.Add(tple);
                }
            }
            if (!(chums.Count == 0))
            {
                GetAutoFishingPondWidth(bobberTileX, bobberTileY, out int minX, out int maxX);
                Point p = default;
                for (int j = minX; j <= maxX; j++)
                {
                    p.X = j;
                    int index = bobberTileY - 1;
                    while (Main.tile[j, index].LiquidAmount > 0 && !WorldGen.SolidTile(j, index, false) && index < Main.maxTilesY - 10)
                    {

                        index++;
                        p.Y = index;
                        for (int l = chums.Count - 1; l >= 0; l--)
                        {
                            if (chums[l].Item2 == p)
                            {
                                chumsInPool.Add(chums[l].Item1);
                                chums.RemoveAt(l);
                            }
                        }
                        if (chums.Count == 0)
                        {
                            break;
                        }
                    }
                    if (chums.Count == 0)
                    {
                        break;
                    }
                }
                for (int k = 0; k < chumsInPool.Count; k++)
                {
                    Projectile obj = Main.projectile[chumsInPool[k]];
                    obj.ai[0] += 1f;
                    obj.netUpdate = true;
                }
            }
        }

        public struct AutoFishingAttempt
        {
            public Point position;
            public bool common;
            public bool uncommon;
            public bool rare;
            public bool veryrare;
            public bool legendary;
            public bool crate;
            public bool isJunk;
            public bool canFishInLava;
            public bool inLava;
            public bool inHoney;
            public int poolSize;
            public int waterNeededToFish;
            public float waterQuality;
            public int chumsInWater;
            public int fishingPower;
            public Item baitItem;
            public Zone zone;
            public int bobberType;
            public float atmosphere;
            public int questFish;
            public int heightLevel;
            public int caughtItem;
            public int caughtEnemy;
        }
        public override bool PreDrawExtras()
        {
            if (!TileEntity.ByID.ContainsKey((int)Projectile.ai[1]))
                return false;
            if (TileEntity.ByID[(int)Projectile.ai[1]] is not AutofisherTE te)
                return false;

            bool facingRight = Main.tile[te.Position.X, te.Position.Y].TileFrameX == 0;
            Vector2 value = te.Position.ToWorldCoordinates(2, 0);
            value -= new Vector2(6f, 4f); //brute-force fix for weird offset
            if (facingRight)
                value.X += 46f;


            //value = Main.player[Projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
            float projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
            float projPosY = Projectile.position.Y + (float)Projectile.height * 0.5f - value.Y;
            Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
            bool isInPlace = true;
            if (projPosX == 0f && projPosY == 0f)
            {
                isInPlace = false;
            }
            else
            {
                float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                projPosXY = 12f / projPosXY;
                projPosX *= projPosXY;
                projPosY *= projPosXY;
                value.X -= projPosX;
                value.Y -= projPosY;
                projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
                projPosY = Projectile.position.Y + (float)Projectile.height * 0.5f - value.Y;
            }
            while (isInPlace)
            {
                float num = 12f;
                float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                float num3 = num2;
                if (float.IsNaN(num2) || float.IsNaN(num3))
                {
                    isInPlace = false;
                }
                else
                {
                    if (num2 < 20f)
                    {
                        num = num2 - 8f;
                        isInPlace = false;
                    }
                    num2 = 12f / num2;
                    projPosX *= num2;
                    projPosY *= num2;
                    value.X += projPosX;
                    value.Y += projPosY;
                    projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
                    projPosY = Projectile.position.Y + (float)Projectile.height * 0.1f - value.Y;
                    if (num3 > 12f)
                    {
                        float num4 = 0.3f;
                        float num5 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
                        if (num5 > 16f)
                        {
                            num5 = 16f;
                        }
                        num5 = 1f - num5 / 16f;
                        num4 *= num5;
                        num5 = num3 / 80f;
                        if (num5 > 1f)
                        {
                            num5 = 1f;
                        }
                        num4 *= num5;
                        if (num4 < 0f)
                        {
                            num4 = 0f;
                        }
                        num5 = 1f - Projectile.localAI[0] / 100f;
                        num4 *= num5;
                        if (projPosY > 0f)
                        {
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                        else
                        {
                            num5 = Math.Abs(Projectile.velocity.X) / 3f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num5 -= 0.5f;
                            num4 *= num5;
                            if (num4 > 0f)
                            {
                                num4 *= 2f;
                            }
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                    }
                    float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                    Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), FishingLineColor);
                    Main.spriteBatch.Draw(TextureAssets.FishingLine.Value, new Vector2(value.X - Main.screenPosition.X + (float)TextureAssets.FishingLine.Width() * 0.5f, value.Y - Main.screenPosition.Y + (float)TextureAssets.FishingLine.Height() * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.FishingLine.Width(), (int)num)), color2, rotation2, new Vector2((float)TextureAssets.FishingLine.Width() * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}
