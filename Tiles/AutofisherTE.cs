using GoldensMisc.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Tiles
{
	public class AutofisherTE : ModTileEntity
	{
		public string DisplayedFishingInfo
		{
			get
			{
				if(_fishingInfo != null)
					return _fishingInfo;
				GetDefaultFishingInfo();
				string horribleHack = _fishingInfo;
				_fishingInfo = null;
				return horribleHack ?? "Error";
			}
			set
			{
				_fishingInfo = value;
			}
		}
		string _fishingInfo = null;
		int bobberProj = -1;
		int FishingCooldown = 300;

		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.Autofisher;
		}

		public override bool ValidTile(int i, int j)
		{
			var tile = Main.tile[i, j];
			return tile.active() && tile.type == ModContent.TileType<Autofisher>() && (tile.frameX == 0 || tile.frameX == 54) && tile.frameY == 0;
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if(Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i - 1, j - 1, Type);
				return -1;
			}
			return Place(i - 1, j - 1);
		}

		public override void Update()
		{
			base.Update();

			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				bool facingRight = Main.tile[Position.X, Position.Y].frameX == 0;
				var baitItem = GetCurrentBait();
				if(baitItem == null)
				{
					DisplayedFishingInfo = Language.GetTextValue("Mods.GoldensMisc.Autofisher.NoBait");
				}
				else if(bobberProj == -1 || !Main.projectile[bobberProj].active ||
					Main.projectile[bobberProj].type != ModContent.ProjectileType<AutofisherBobber>())
				{
					var bobberPos = new Point(Position.X + (facingRight ? 2 : 0), Position.Y).ToWorldCoordinates();
					bobberProj = Projectile.NewProjectile(bobberPos, new Vector2(facingRight ? 3f : -3f, 0f), ModContent.ProjectileType<AutofisherBobber>(), 0, 0, ai1: this.ID);
					Main.projectile[bobberProj].ai[1] = this.ID;
					Main.projectile[bobberProj].netUpdate = true;
					_fishingInfo = null;
				}
				if(FishingCooldown > 0)
				{
					FishingCooldown--;
				}
				else
				{
					if(Main.rand.Next(150) == 1)
					{
						FishingCooldown = 500;
						int itemType = 0;
						try
						{
							if(baitItem == null)
								return;
							var projectile = Main.projectile[bobberProj];
							itemType = ((AutofisherBobber)projectile.modProjectile).FishingCheck(true);

							if(itemType > 0)
							{
								TryConsumeBait();
							}
						}
						catch(Exception e)
						{
							GoldensMisc.Log(e);
							Main.NewText("autofisher error! look at Logs.txt");
						}
					}
				}
			}
		}


		public int GetFishingLevel(Item baitItem)
		{
			var item = new Item();
			item.SetDefaults(ItemID.MechanicsRod, true);

			int baitPower = 0;
			//int fishingPole = 30;
			int fishingPole = item.fishingPole;

			//num1 = 15;
			if(baitItem.type == ItemID.TruffleWorm)
				return -1;
			baitPower = baitItem.bait;

			if(baitPower == 0 || fishingPole == 0)
				return 0;
			int power = baitPower + fishingPole;
			if(Main.raining)
				power = (int)((double)power * 1.20000004768372);
			if((double)Main.cloudBGAlpha > 0.0)
				power = (int)((double)power * 1.10000002384186);
			if(Main.dayTime && (Main.time < 5400.0 || Main.time > 48600.0))
				power = (int)((double)power * 1.29999995231628);
			if(Main.dayTime && Main.time > 16200.0 && Main.time < 37800.0)
				power = (int)((double)power * 0.800000011920929);
			if(!Main.dayTime && Main.time > 6480.0 && Main.time < 25920.0)
				power = (int)((double)power * 0.800000011920929);
			if(Main.moonPhase == 0)
				power = (int)((double)power * 1.10000002384186);
			if(Main.moonPhase == 1 || Main.moonPhase == 7)
				power = (int)((double)power * 1.04999995231628);
			if(Main.moonPhase == 3 || Main.moonPhase == 5)
				power = (int)((double)power * 0.949999988079071);
			if(Main.moonPhase == 4)
				power = (int)((double)power * 0.899999976158142);
			AutofisherHooks.GetFishingLevel(this, baitItem, ref power);
			return power;
		}

		public Item GetCurrentBait()
		{
			bool facingRight = Main.tile[Position.X, Position.Y].frameX == 0;

			int chestX = facingRight ? Position.X - 1 : Position.X + 3;
			int chestY = Position.Y;
			if(Main.tile[chestX, chestY].frameX % 36 != 0)
				chestX--;
			int chestID = Chest.FindChest(chestX, chestY);
			if(chestID == -1)
				return null;
			var chest = Main.chest[chestID];
			for(int i = 0; i < Chest.maxItems; i++)
			{
				if(chest.item[i].stack > 0)
				{
					if(chest.item[i].bait > 0 || chest.item[i].type == ItemID.TruffleWorm)
						return chest.item[i];
				}
			}
			return null;
		}

		void TryConsumeBait()
		{
			var item = GetCurrentBait();
			if(item == null)
				return;
			//Vanilla chance: item.bait / 5 + 1
			int consumeChance = item.bait / 10 + 1;
			if(Main.rand.Next(consumeChance) == 0)
				item.stack--;
			if(item.stack <= 0)
				item.SetDefaults(0);
		}

		void GetDefaultFishingInfo()
		{
			if(bobberProj != -1 && Main.projectile[bobberProj].active &&
					Main.projectile[bobberProj].type == ModContent.ProjectileType<AutofisherBobber>())
			{
				var projectile = Main.projectile[bobberProj];
				((AutofisherBobber)projectile.modProjectile).FishingCheck(false);
			}
		}
	}
}
		
