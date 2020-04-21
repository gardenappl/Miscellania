using System;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;

namespace GoldensMisc.Tiles
{
    public class ChestVacuumTE : ModTileEntity
    {
        public bool smartStack = false;
        const float PickupRadiusSq = 160f * 160f;
        public bool smartStackChanged = false;
        private int timer = 0;
        private int setupTimer = 0;


        public override bool Autoload(ref string name)
        {
            return ModContent.GetInstance<ServerConfig>().ChestVacuum;
        }

        public override bool ValidTile(int i, int j)
        {
            var tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<ChestVacuum>() && tile.frameX == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
                return -1;
            }
            return Place(i, j);
        }

        public override void Update()
        {
            try
            {

                base.Update();
                if (smartStackChanged)
                {
                    NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
                    smartStackChanged = false;
                }

                timer++;
                if (timer >= GetPickupTime())
                {
                    timer = 0;
                    FindAndAddItemToChest();
                }
            }
            catch (Exception e)
            {
                Main.NewText("chest vacuum error. look at Logs.txt");
                GoldensMisc.Log(e);
            }
        }

        public override void NetReceive(BinaryReader reader, bool lightReceive)
        {
            smartStack = reader.ReadBoolean();
        }

        public override void NetSend(BinaryWriter writer, bool lightSend)
        {
            writer.Write(smartStack);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"smartStack", smartStack}
            };
        }

        public override void Load(TagCompound tag)
        {
            smartStack = tag.GetBool("smartStack");
        }

        public void FindAndAddItemToChest()
        {
            var chest = Main.chest[Chest.FindChest(Position.X, Position.Y + 1)];
            Vector2 myPos = Position.ToWorldCoordinates(16f, 8f);
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item item = null;
                item = Main.item[i];
                if ((item.active) && (item.stack > 0) && (item.DistanceSQ(myPos) < PickupRadiusSq) && (item != null))
                {
                    Vector2 itemPos = item.position;
                    int itemWidth = item.width;
                    int itemHeight = item.height;
                    //string itemName = item.Name;
                    int oldStack = item.stack;
                    MiscUtils.PutItem(chest, item, smartStack);
                    if (item.type == 0 || oldStack - item.stack > 0)
                    {
                        if (item.stack == 0 || item.type == 0)
                        {
                            item.active = false;
                        }
                        if (Main.netMode != NetmodeID.Server)
                        {
                            Main.PlaySound(SoundID.Item8, itemPos);
                            Main.PlaySound(SoundID.Grab, myPos);
                            for (int j = 0; j < 10; j++)
                            {
                                Dust.NewDust(itemPos, itemWidth, itemHeight, 159);
                                Dust.NewDust(Position.ToWorldCoordinates(0, 0), 32, 16, 159);
                            }
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            var te = (ChestVacuumTE)TileEntity.ByPosition[Position];
                            var netMessage = mod.GetPacket();
                            netMessage.Write((byte)MiscMessageType.AddItemByWayOfVacuum);
                            netMessage.Write(te.ID);
                            netMessage.Send();
                        }
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i);
                    }

                }
            }
        }


        int GetPickupTime()
        {
            return (((Position.X * 10) + (Position.Y * 5)) / 50);
        }

    }


}
