using System;
using Mod.ModHelper;
using Mod.R;
using Mod.Xmap;
using UnityEngine;

namespace Mod.Auto
{
	// Token: 0x0200017D RID: 381
	internal class AutoLogin
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000BA46C File Offset: 0x000B866C
		internal static bool IsRunning
		{
			get
			{
				return AutoLogin.isEnabled && AutoLogin.steps > 0;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000BA480 File Offset: 0x000B8680
		internal static void Update()
		{
			if (!AutoLogin.isEnabled)
			{
				return;
			}
			switch (AutoLogin.steps)
			{
			default:
				AutoLogin.CheckForDisconnected();
				return;
			case 1:
				if (mSystem.currentTimeMillis() - AutoLogin.lastTimeUpdate <= 750L)
				{
					return;
				}
				AutoLogin.lastTimeUpdate = mSystem.currentTimeMillis();
				AutoLogin.AttemptLogin();
				return;
			case 2:
				if (mSystem.currentTimeMillis() - AutoLogin.lastTimeUpdate <= 750L)
				{
					return;
				}
				AutoLogin.lastTimeUpdate = mSystem.currentTimeMillis();
				AutoLogin.GotoLastMapAndZone();
				return;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000BA4FC File Offset: 0x000B86FC
		private static void CheckForDisconnected()
		{
			if (!(GameCanvas.currentScreen is GameScr) || !Session_ME.gI().isConnected())
			{
				AutoLogin.lastTimeAttemptLogin = mSystem.currentTimeMillis();
				GameCanvas.serverScreen.switchToMe();
				GameCanvas.startOKDlg(string.Format(Strings.autoLoginReattemptLoginIn, 30) + "!");
				AutoLogin.steps = 1;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000BA55C File Offset: 0x000B875C
		private static void AttemptLogin()
		{
			if (GameCanvas.currentScreen is GameScr)
			{
				AutoLogin.steps = 2;
				return;
			}
			GameCanvas.startOKDlg(string.Format(Strings.autoLoginReattemptLoginIn, 30L - (mSystem.currentTimeMillis() - AutoLogin.lastTimeAttemptLogin) / 1000L) + "!");
			if (mSystem.currentTimeMillis() - AutoLogin.lastTimeAttemptLogin < 30000L)
			{
				return;
			}
			AutoLogin.lastTimeAttemptLogin = mSystem.currentTimeMillis();
			if (GameCanvas.currentScreen is LoginScr)
			{
				GameCanvas.serverScreen.switchToMe();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.connect();
			GameCanvas.loginScr.switchToMe();
			Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
			GameCanvas.startWaitDlg();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000BA644 File Offset: 0x000B8844
		private static void GotoLastMapAndZone()
		{
			if (TileMap.mapID != AutoLogin.lastMapID)
			{
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(AutoLogin.lastMapID);
					return;
				}
			}
			else
			{
				if (TileMap.zoneID != AutoLogin.lastZoneID)
				{
					Service.gI().requestChangeZone(AutoLogin.lastZoneID, 0);
					return;
				}
				if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)AutoLogin.lastX, (double)AutoLogin.lastY) > 15.0)
				{
					Utils.TeleportMyChar(AutoLogin.lastX, AutoLogin.lastY);
					return;
				}
				global::Char.chatPopup = null;
				ChatPopup.currChatPopup = null;
				AutoLogin.steps = 0;
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000BA6E8 File Offset: 0x000B88E8
		internal static void OnGameScrUpdate()
		{
			if (!AutoLogin.isEnabled)
			{
				return;
			}
			if (AutoLogin.steps == 0 && (float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
			{
				AutoLogin.lastMapID = TileMap.mapID;
				AutoLogin.lastZoneID = TileMap.zoneID;
				AutoLogin.lastX = global::Char.myCharz().cx;
				AutoLogin.lastY = Utils.GetYGround(global::Char.myCharz().cx);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000BA754 File Offset: 0x000B8954
		internal static void SetState(bool state)
		{
			AutoLogin.isEnabled = state;
		}

		// Token: 0x0400189C RID: 6300
		internal static bool isEnabled;

		// Token: 0x0400189D RID: 6301
		private static long lastTimeAttemptLogin;

		// Token: 0x0400189E RID: 6302
		private static long lastTimeUpdate;

		// Token: 0x0400189F RID: 6303
		private static int lastMapID;

		// Token: 0x040018A0 RID: 6304
		private static int lastZoneID;

		// Token: 0x040018A1 RID: 6305
		private static int lastX;

		// Token: 0x040018A2 RID: 6306
		private static int lastY;

		// Token: 0x040018A3 RID: 6307
		private static int steps;
	}
}
