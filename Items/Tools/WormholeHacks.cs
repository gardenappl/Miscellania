using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Tools
{
    public static class WormholeHacks
    {
        public static void Load()
        {
            //Very convenient methods, thank you, Terraria devs!
            Terraria.On_Player.HasUnityPotion += PlayerHasUnityPotion;
            Terraria.On_Player.TakeUnityPotion += PlayerTakeUnityPotion;

            //But why the fuck do you not use them sometimes?!
            //Terraria.On_Player.HasItemInInventoryOrOpenVoidBag += PlayerHasItem;
        }

        public static void Unload()
        {
            Terraria.On_Player.HasUnityPotion -= PlayerHasUnityPotion;
            Terraria.On_Player.TakeUnityPotion -= PlayerTakeUnityPotion;
            //Terraria.On_Player.HasItemInInventoryOrOpenVoidBag -= PlayerHasItem;
        }

        public static bool HasWormholeItem(Player player)
        {
            return player.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<WormholeMirror>()) ||
               player.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<WormholeDoubleMirror>()) ||
               player.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<WormholeIceMirror>()) ||
               player.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<WormholeCellPhone>());
        }

        private static bool PlayerHasUnityPotion(Terraria.On_Player.orig_HasUnityPotion orig, Player self)
        {
            if (HasWormholeItem(self))
                return true;
            else
                return orig(self);
        }

        private static void PlayerTakeUnityPotion(Terraria.On_Player.orig_TakeUnityPotion orig, Player self)
        {
            if (HasWormholeItem(self))
                return;
            else
                orig(self);
        }

        /*private static bool PlayerHasItem(Terraria.On_Player.orig_HasItemInInventoryOrOpenVoidBag orig, Player self, int id)
        {
            if (id != ItemID.WormholePotion)
                return orig(self, id);

            if (HasWormholeItem(self))
            {
                var stackTrace = new StackTrace();
                for (int i = 0; i < stackTrace.FrameCount; i++)
                {
                    var method = stackTrace.GetFrame(i).GetMethod();
                    if (method.Name == "ComposeInstructionsForGamepad" &&
                            method.DeclaringType.Assembly.GetName().Name == "Terraria")
                        return true;
                }
            }
            return orig(self, id);
        }*/
    }
}
