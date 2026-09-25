using System;
using Mod.ModHelper;
using Mod.R;
using Mod.Xmap;

namespace Mod.Auto
{
	// Token: 0x0200017A RID: 378
	internal class AutoGoback
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000BA13A File Offset: 0x000B833A
		internal static bool IsGoingBack
		{
			get
			{
				return AutoGoback.isGoingBack;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000BA141 File Offset: 0x000B8341
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x000BA148 File Offset: 0x000B8348
		internal static AutoGoback.GoBackMode mode { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000BA150 File Offset: 0x000B8350
		internal static bool isEnabled
		{
			get
			{
				return AutoGoback.mode > AutoGoback.GoBackMode.Disabled;
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000BA15A File Offset: 0x000B835A
		internal static void setState(int value)
		{
			AutoGoback.mode = (AutoGoback.GoBackMode)value;
			if (AutoGoback.isEnabled)
			{
				AutoGoback.Enable();
				return;
			}
			AutoGoback.Disable();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000BA174 File Offset: 0x000B8374
		internal static void Enable()
		{
			if (AutoGoback.mode != AutoGoback.GoBackMode.GoBackToFixedLocation)
			{
				return;
			}
			AutoGoback.goingBackTo = new AutoGoback.InfoGoBack(TileMap.mapID, TileMap.zoneID, global::Char.myCharz().cx, global::Char.myCharz().cy);
			GameScr.info1.addInfo(string.Format(Strings.gobackTo, new object[]
			{
				TileMap.mapName,
				TileMap.zoneID,
				AutoGoback.goingBackTo.x,
				AutoGoback.goingBackTo.y
			}) + "!", 0);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000BA210 File Offset: 0x000B8410
		internal static void Disable()
		{
			AutoGoback.isGoingBack = false;
			XmapController.finishXmap();
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000BA220 File Offset: 0x000B8420
		internal static void Update()
		{
			if (!AutoGoback.isEnabled || ThreadAction<XmapController>.gI.IsActing)
			{
				return;
			}
			if (mSystem.currentTimeMillis() - AutoGoback.lastTimeUpdate < 1000L)
			{
				return;
			}
			AutoGoback.lastTimeUpdate = mSystem.currentTimeMillis();
			if (global::Char.myCharz().IsCharDead())
			{
				AutoGoback.HandleDeath();
				return;
			}
			if (AutoGoback.isGoingBack)
			{
				AutoGoback.HandleGoingBack();
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000BA280 File Offset: 0x000B8480
		private static void HandleGoingBack()
		{
			if (!Utils.IsMyCharHome())
			{
				if (TileMap.mapID == AutoGoback.goingBackTo.mapID)
				{
					if (TileMap.zoneID != AutoGoback.goingBackTo.zoneID)
					{
						Service.gI().requestChangeZone(AutoGoback.goingBackTo.zoneID, 0);
						return;
					}
					global::Char.chatPopup = null;
					if ((AutoGoback.mode != AutoGoback.GoBackMode.GoBackToWhereIDied && global::Char.myCharz().cx != AutoGoback.goingBackTo.x) || global::Char.myCharz().cy != AutoGoback.goingBackTo.y)
					{
						Utils.TeleportMyChar(AutoGoback.goingBackTo.x, AutoGoback.goingBackTo.y);
						return;
					}
					AutoGoback.isGoingBack = false;
				}
				return;
			}
			if (global::Char.myCharz().cHP > 1)
			{
				XmapController.start(AutoGoback.goingBackTo.mapID);
				return;
			}
			if (global::Char.myCharz().taskMaint.taskId > 2)
			{
				Service.gI().pickItem(-1);
				return;
			}
			GameScr.gI().doUseHP();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000BA370 File Offset: 0x000B8570
		private static void HandleDeath()
		{
			long num = mSystem.currentTimeMillis();
			long num2 = num - AutoGoback.lastTimeGoBack;
			if (num2 > 4000L)
			{
				AutoGoback.lastTimeGoBack = num;
				return;
			}
			if (num2 > 3000L)
			{
				if (AutoGoback.mode != AutoGoback.GoBackMode.GoBackToFixedLocation)
				{
					AutoGoback.goingBackTo = new AutoGoback.InfoGoBack(TileMap.mapID, TileMap.zoneID, global::Char.myCharz());
				}
				Service.gI().returnTownFromDead();
				AutoGoback.isGoingBack = true;
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000BA3D5 File Offset: 0x000B85D5
		private static bool HasChicken()
		{
			return GameScr.vItemMap.size() > 0;
		}

		// Token: 0x0400188F RID: 6287
		internal static AutoGoback.InfoGoBack goingBackTo;

		// Token: 0x04001890 RID: 6288
		private static bool isGoingBack;

		// Token: 0x04001892 RID: 6290
		private static long lastTimeGoBack;

		// Token: 0x04001893 RID: 6291
		private static long lastTimeUpdate;

		// Token: 0x0200017B RID: 379
		internal struct InfoGoBack
		{
			// Token: 0x06001139 RID: 4409 RVA: 0x000BA3E4 File Offset: 0x000B85E4
			internal InfoGoBack(int mapId, int zoneId, int x, int y)
			{
				this.mapID = mapId;
				this.zoneID = zoneId;
				this.x = x;
				this.y = (TileMap.tileTypeAt(x, y, 2) ? y : Utils.GetYGround(x));
			}

			// Token: 0x0600113A RID: 4410 RVA: 0x000BA418 File Offset: 0x000B8618
			internal InfoGoBack(int mapId, int zoneId, IMapObject mapObject)
			{
				this.mapID = mapId;
				this.zoneID = zoneId;
				this.x = mapObject.getX();
				this.y = (TileMap.tileTypeAt(this.x, mapObject.getY(), 2) ? mapObject.getY() : Utils.GetYGround(this.x));
			}

			// Token: 0x04001894 RID: 6292
			internal int mapID;

			// Token: 0x04001895 RID: 6293
			internal int zoneID;

			// Token: 0x04001896 RID: 6294
			internal int x;

			// Token: 0x04001897 RID: 6295
			internal int y;
		}

		// Token: 0x0200017C RID: 380
		internal enum GoBackMode
		{
			// Token: 0x04001899 RID: 6297
			Disabled,
			// Token: 0x0400189A RID: 6298
			GoBackToWhereIDied,
			// Token: 0x0400189B RID: 6299
			GoBackToFixedLocation
		}
	}
}
