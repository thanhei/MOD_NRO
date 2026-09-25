using System;
using Mod.Constants;
using Mod.ModHelper;
using Mod.PickMob;
using Mod.R;
using Mod.Xmap;

namespace Mod.Auto
{
	// Token: 0x0200017F RID: 383
	internal class AutoSellTrashItems
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000BA847 File Offset: 0x000B8A47
		internal static bool IsRunning
		{
			get
			{
				return AutoSellTrashItems.isEnabled && AutoSellTrashItems.steps > 0;
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000BA85C File Offset: 0x000B8A5C
		internal static void Update()
		{
			if (!AutoSellTrashItems.isEnabled)
			{
				return;
			}
			if (mSystem.currentTimeMillis() - AutoSellTrashItems.lastTimeUpdate <= 750L)
			{
				return;
			}
			AutoSellTrashItems.lastTimeUpdate = mSystem.currentTimeMillis();
			switch (AutoSellTrashItems.steps)
			{
			default:
				AutoSellTrashItems.CheckShouldSellTrashItems();
				return;
			case 1:
				AutoSellTrashItems.GoHome();
				return;
			case 2:
				AutoSellTrashItems.MoveVipItemsToChest();
				return;
			case 3:
				AutoSellTrashItems.GotoSpaceshipStation();
				return;
			case 4:
				AutoSellTrashItems.SellTrashItems();
				return;
			case 5:
				AutoSellTrashItems.ThrowTrashItems();
				return;
			case 6:
				AutoSellTrashItems.GotoLastMapAndZone();
				return;
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		private static void CheckShouldSellTrashItems()
		{
			if (!GameScr.gI().isBagFull())
			{
				return;
			}
			AutoSellTrashItems.PausePickMob();
			AutoSellTrashItems.lastMapID = TileMap.mapID;
			AutoSellTrashItems.lastZoneID = TileMap.zoneID;
			AutoSellTrashItems.lastX = global::Char.myCharz().cx;
			AutoSellTrashItems.lastY = global::Char.myCharz().cy;
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && AutoSellTrashItems.ShouldMoveItemToChest(item))
				{
					AutoSellTrashItems.steps = 1;
					return;
				}
			}
			AutoSellTrashItems.steps = 3;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000BA96D File Offset: 0x000B8B6D
		private static void GoHome()
		{
			if (TileMap.mapID != global::Char.myCharz().cgender + 21)
			{
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(global::Char.myCharz().cgender + 21);
					return;
				}
			}
			else
			{
				AutoSellTrashItems.steps = 2;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000BA9A8 File Offset: 0x000B8BA8
		private static void MoveVipItemsToChest()
		{
			if (global::Char.myCharz().cgender == 0 && Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 85.0, 336.0) > 15.0)
			{
				Utils.TeleportMyChar(85, 336);
				return;
			}
			if (global::Char.myCharz().cgender == 2 && Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 94.0, 336.0) > 15.0)
			{
				Utils.TeleportMyChar(94, 336);
				return;
			}
			if (global::Char.myCharz().cgender == 1 && Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 638.0, 336.0) > 15.0)
			{
				Utils.TeleportMyChar(638, 336);
				return;
			}
			if (GameCanvas.panel.hasUse >= global::Char.myCharz().arrItemBox.Length)
			{
				GameCanvas.startOKDlg(Strings.autoSellTrashItemsBoxFull + "!");
				AutoSellTrashItems.steps = 3;
				return;
			}
			int i;
			for (i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && AutoSellTrashItems.ShouldMoveItemToChest(item))
				{
					Service.gI().getItem(1, (sbyte)i);
					break;
				}
			}
			if (i == -1)
			{
				AutoSellTrashItems.steps = 3;
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000BAB2A File Offset: 0x000B8D2A
		private static void GotoSpaceshipStation()
		{
			if (TileMap.mapID != global::Char.myCharz().cgender + 24)
			{
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(global::Char.myCharz().cgender + 24);
					return;
				}
			}
			else
			{
				AutoSellTrashItems.steps = 4;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000BAB68 File Offset: 0x000B8D68
		private static void SellTrashItems()
		{
			if (TileMap.mapID == 24)
			{
				if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 389.0, 336.0) > 15.0)
				{
					Utils.TeleportMyChar(389, 336);
					return;
				}
			}
			else if (TileMap.mapID == 25)
			{
				if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 508.0, 336.0) > 15.0)
				{
					Utils.TeleportMyChar(508, 336);
					return;
				}
			}
			else if (TileMap.mapID == 26 && Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, 511.0, 336.0) > 15.0)
			{
				Utils.TeleportMyChar(511, 336);
				return;
			}
			if (!GameCanvas.panel.isShow)
			{
				Service.gI().openMenu(16);
				return;
			}
			if (GameCanvas.currentDialog != null && AutoSellTrashItems.lastRemoveItemIndex > -1)
			{
				Service.gI().saleItem(1, 1, (short)AutoSellTrashItems.lastRemoveItemIndex);
				GameCanvas.endDlg();
				return;
			}
			int i = global::Char.myCharz().arrItemBag.Length - 1;
			if (AutoSellTrashItems.lastRemoveItemIndex != -1)
			{
				i = AutoSellTrashItems.lastRemoveItemIndex;
			}
			while (i >= 0)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && !AutoSellTrashItems.ShouldKeepItem(item))
				{
					Service.gI().saleItem(0, 1, (short)i);
					if (i == AutoSellTrashItems.lastRemoveItemIndex)
					{
						AutoSellTrashItems.removeAttempts++;
					}
					else
					{
						AutoSellTrashItems.removeAttempts = 0;
					}
					if (AutoSellTrashItems.removeAttempts >= 5)
					{
						AutoSellTrashItems.lastRemoveItemIndex = i - 1;
						break;
					}
					AutoSellTrashItems.lastRemoveItemIndex = i;
					break;
				}
				else
				{
					i--;
				}
			}
			if (i < 0)
			{
				Panel panel = GameCanvas.panel;
				if (panel != null)
				{
					panel.hide();
				}
				Panel panel2 = GameCanvas.panel2;
				if (panel2 != null)
				{
					panel2.hide();
				}
				AutoSellTrashItems.removeAttempts = 0;
				AutoSellTrashItems.lastRemoveItemIndex = -1;
				AutoSellTrashItems.steps = 5;
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000BAD64 File Offset: 0x000B8F64
		private static void ThrowTrashItems()
		{
			if (global::Char.myCharz().arrItemBag.Length == 0)
			{
				AutoSellTrashItems.steps = 6;
				return;
			}
			if (global::Char.myCharz().cPower < 1500000L)
			{
				AutoSellTrashItems.steps = 6;
				return;
			}
			int i = global::Char.myCharz().arrItemBag.Length - 1;
			if (AutoSellTrashItems.lastRemoveItemIndex != -1)
			{
				i = AutoSellTrashItems.lastRemoveItemIndex;
			}
			while (i >= 0)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && !AutoSellTrashItems.ShouldKeepItem(item))
				{
					Service.gI().useItem(1, 1, (sbyte)i, -1);
					if (i == AutoSellTrashItems.lastRemoveItemIndex)
					{
						AutoSellTrashItems.removeAttempts++;
					}
					else
					{
						AutoSellTrashItems.removeAttempts = 0;
					}
					if (AutoSellTrashItems.removeAttempts >= 5)
					{
						AutoSellTrashItems.lastRemoveItemIndex = i - 1;
						break;
					}
					AutoSellTrashItems.lastRemoveItemIndex = i;
					break;
				}
				else
				{
					i--;
				}
			}
			if (i < 0)
			{
				AutoSellTrashItems.removeAttempts = 0;
				AutoSellTrashItems.lastRemoveItemIndex = -1;
				AutoSellTrashItems.steps = 6;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000BAE38 File Offset: 0x000B9038
		private static void GotoLastMapAndZone()
		{
			if (TileMap.mapID != AutoSellTrashItems.lastMapID)
			{
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(AutoSellTrashItems.lastMapID);
					return;
				}
			}
			else
			{
				if (TileMap.zoneID != AutoSellTrashItems.lastZoneID)
				{
					Service.gI().requestChangeZone(AutoSellTrashItems.lastZoneID, 0);
					return;
				}
				if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)AutoSellTrashItems.lastX, (double)AutoSellTrashItems.lastY) > 15.0)
				{
					Utils.TeleportMyChar(AutoSellTrashItems.lastX, AutoSellTrashItems.lastY);
					return;
				}
				global::Char.chatPopup = null;
				ChatPopup.currChatPopup = null;
				AutoSellTrashItems.ResumePickMob();
				AutoSellTrashItems.steps = 0;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000BAEE1 File Offset: 0x000B90E1
		private static void PausePickMob()
		{
			AutoSellTrashItems.lastPickMobState = Pk9rPickMob.IsTanSat;
			Pk9rPickMob.IsTanSat = false;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000BAEF3 File Offset: 0x000B90F3
		private static void ResumePickMob()
		{
			Pk9rPickMob.IsTanSat = AutoSellTrashItems.lastPickMobState;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000BAF00 File Offset: 0x000B9100
		private static bool ShouldMoveItemToChest(Item item)
		{
			return item.IsWearableAndVip() || item.template.type == ItemTemplateType.FlyPlatform || item.template.type == ItemTemplateType.VIPFlyPlatform || item.template.type == ItemTemplateType.Backpack || item.template.type == ItemTemplateType.AvatarAndDisguise || item.template.type == ItemTemplateType.UpgradeStone || item.template.type == ItemTemplateType.DragonBall || item.template.type == ItemTemplateType.ConsumableBuffItem || (item.template.type == ItemTemplateType.Miscellaneous && item.template.id != 521);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000BAFC7 File Offset: 0x000B91C7
		private static bool ShouldKeepItem(Item item)
		{
			return AutoSellTrashItems.ShouldMoveItemToChest(item) || item.template.type == ItemTemplateType.SenzuBean || item.template.id == 521;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000BAFF7 File Offset: 0x000B91F7
		internal static void SetState(bool value)
		{
			AutoSellTrashItems.isEnabled = value;
			if (AutoSellTrashItems.isEnabled)
			{
				AutoSellTrashItems.steps = 0;
			}
		}

		// Token: 0x040018A7 RID: 6311
		internal static bool isEnabled;

		// Token: 0x040018A8 RID: 6312
		private static int steps;

		// Token: 0x040018A9 RID: 6313
		private static long lastTimeUpdate;

		// Token: 0x040018AA RID: 6314
		private static int lastRemoveItemIndex = -1;

		// Token: 0x040018AB RID: 6315
		private static int lastMapID;

		// Token: 0x040018AC RID: 6316
		private static int lastZoneID;

		// Token: 0x040018AD RID: 6317
		private static int lastX;

		// Token: 0x040018AE RID: 6318
		private static int lastY;

		// Token: 0x040018AF RID: 6319
		private static bool lastPickMobState;

		// Token: 0x040018B0 RID: 6320
		private static int removeAttempts;
	}
}
