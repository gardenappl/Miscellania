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
            On.Terraria.Player.HasUnityPotion += PlayerHasUnityPotion;
            On.Terraria.Player.TakeUnityPotion += PlayerTakeUnityPotion;

            //But why the fuck do you not use them sometimes?!
            On.Terraria.Player.HasItem += PlayerHasItem;
        }

        public static void Unload()
        {
            On.Terraria.Player.HasUnityPotion -= PlayerHasUnityPotion;
            On.Terraria.Player.TakeUnityPotion -= PlayerTakeUnityPotion;
            On.Terraria.Player.HasItem -= PlayerHasItem;
        }

        public static bool HasWormholeItem(Player player)
        {
            return player.HasItem(ModContent.ItemType<WormholeMirror>()) ||
               player.HasItem(ModContent.ItemType<WormholeDoubleMirror>()) ||
               player.HasItem(ModContent.ItemType<WormholeIceMirror>()) ||
               player.HasItem(ModContent.ItemType<WormholeCellPhone>());
        }

        private static bool PlayerHasUnityPotion(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
        {
            if (HasWormholeItem(self))
                return true;
            else
                return orig(self);
        }

        private static void PlayerTakeUnityPotion(On.Terraria.Player.orig_TakeUnityPotion orig, Player self)
        {
            if (HasWormholeItem(self))
                return;
            else
                orig(self);
        }

        private static bool PlayerHasItem(On.Terraria.Player.orig_HasItem orig, Player self, int id)
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
        }
    }
}
