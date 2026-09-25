using System;

namespace Mod.DungPham.KoiOctiiu957
{
	// Đăng nhập lại khi mất kết nối, rồi quay về map, khu và vị trí cũ (theo cách của pk9r)
	public class AutoLogin
	{
		// Đang trong quá trình đăng nhập lại / quay về chỗ cũ: các auto khác nên tạm dừng
		public static bool IsRunning
		{
			get
			{
				return AutoLogin.isEnabled && AutoLogin.steps > 0;
			}
		}

		// Gọi mỗi frame ở mọi màn hình (GameCanvas.update)
		public static void Update()
		{
			if (!AutoLogin.isEnabled)
			{
				return;
			}
			switch (AutoLogin.steps)
			{
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
			default:
				AutoLogin.CheckForDisconnected();
				return;
			}
		}

		// Gọi mỗi frame khi đang ở GameScr (MainMod.Update): ghi nhớ map, khu, vị trí hiện tại
		public static void OnGameScrUpdate()
		{
			if (!AutoLogin.isEnabled)
			{
				return;
			}
			if (AutoLogin.steps == 0 && GameCanvas.gameTick % 60 == 0)
			{
				AutoLogin.lastMapID = TileMap.mapID;
				AutoLogin.lastZoneID = TileMap.zoneID;
				AutoLogin.lastX = global::Char.myCharz().cx;
				AutoLogin.lastY = AutoMap.GetYGround(global::Char.myCharz().cx);
				AutoLogin.hasLastPosition = true;
			}
		}

		private static void CheckForDisconnected()
		{
			if (!(GameCanvas.currentScreen is GameScr) || !Session_ME.gI().isConnected())
			{
				AutoLogin.lastTimeAttemptLogin = mSystem.currentTimeMillis();
				GameCanvas.serverScreen.switchToMe();
				GameCanvas.startOKDlg(AutoLogin.GetCountdownText(AutoLogin.delayLogin / 1000));
				AutoLogin.steps = 1;
			}
		}

		private static void AttemptLogin()
		{
			if (GameCanvas.currentScreen is GameScr)
			{
				AutoLogin.steps = 2;
				return;
			}
			long elapsed = mSystem.currentTimeMillis() - AutoLogin.lastTimeAttemptLogin;
			GameCanvas.startOKDlg(AutoLogin.GetCountdownText((AutoLogin.delayLogin - elapsed) / 1000L));
			if (elapsed < (long)AutoLogin.delayLogin)
			{
				return;
			}
			AutoLogin.lastTimeAttemptLogin = mSystem.currentTimeMillis();
			string acc = Rms.loadRMSString("acc");
			string pass = Rms.loadRMSString("pass");
			if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(pass))
			{
				GameCanvas.startOKDlg("Auto Login: chưa có tài khoản đã lưu, hãy đăng nhập thủ công một lần!");
				return;
			}
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
			Service.gI().login(acc, pass, GameMidlet.VERSION, 0);
			GameCanvas.startWaitDlg();
		}

		private static void GotoLastMapAndZone()
		{
			// Chưa từng ghi được vị trí (bật auto login khi chưa vào game) thì chỉ cần vào game là xong
			if (!AutoLogin.hasLastPosition)
			{
				AutoLogin.steps = 0;
				return;
			}
			if (TileMap.mapID != AutoLogin.lastMapID)
			{
				if (!AutoMap.isXmaping)
				{
					AutoMap.StartRunToMapId(AutoLogin.lastMapID);
				}
				return;
			}
			if (TileMap.zoneID != AutoLogin.lastZoneID)
			{
				Service.gI().requestChangeZone(AutoLogin.lastZoneID, 0);
				return;
			}
			int dx = global::Char.myCharz().cx - AutoLogin.lastX;
			int dy = global::Char.myCharz().cy - AutoLogin.lastY;
			if (dx * dx + dy * dy > 225)
			{
				AutoMap.TeleportTo(AutoLogin.lastX, AutoLogin.lastY);
				return;
			}
			global::Char.chatPopup = null;
			ChatPopup.currChatPopup = null;
			AutoLogin.steps = 0;
		}

		private static string GetCountdownText(long seconds)
		{
			if (seconds < 0L)
			{
				seconds = 0L;
			}
			return "Đăng nhập lại trong " + seconds.ToString() + " giây!";
		}

		public static void SetState(bool state)
		{
			AutoLogin.isEnabled = state;
			AutoLogin.steps = 0;
		}

		public static bool isEnabled;

		// Thời gian chờ giữa các lần thử đăng nhập (ms)
		public static int delayLogin = 60000;

		private static long lastTimeAttemptLogin;

		private static long lastTimeUpdate;

		private static int lastMapID;

		private static int lastZoneID;

		private static int lastX;

		private static int lastY;

		private static int steps;

		private static bool hasLastPosition;
	}
}
