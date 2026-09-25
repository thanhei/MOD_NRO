using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class GameMidlet
{
	// Token: 0x06000339 RID: 825 RVA: 0x0003A182 File Offset: 0x00038382
	public GameMidlet()
	{
		this.initGame();
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0003A190 File Offset: 0x00038390
	public void initGame()
	{
		GameMidlet.instance = this;
		MotherCanvas.instance = new MotherCanvas();
		Session_ME.gI().setHandler(Controller.gI());
		Session_ME2.gI().setHandler(Controller.gI());
		Session_ME2.isMainSession = false;
		GameMidlet.instance = this;
		GameMidlet.gameCanvas = new GameCanvas();
		GameMidlet.gameCanvas.start();
		SplashScr.loadImg();
		SplashScr.loadSplashScr();
		GameCanvas.currentScreen = new SplashScr();
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0003A1FF File Offset: 0x000383FF
	public void exit()
	{
		if (Main.typeClient == 6)
		{
			mSystem.exitWP();
			return;
		}
		GameCanvas.bRun = false;
		mSystem.gcc();
		this.notifyDestroyed();
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0003A220 File Offset: 0x00038420
	public static void sendSMS(string data, string to, Command successAction, Command failAction)
	{
		Cout.println("SEND SMS");
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0003A22C File Offset: 0x0003842C
	public static void flatForm(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0003A244 File Offset: 0x00038444
	public void notifyDestroyed()
	{
		Main.exit();
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0003A24B File Offset: 0x0003844B
	public void platformRequest(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x040006CD RID: 1741
	public static string IP = "112.213.94.23";

	// Token: 0x040006CE RID: 1742
	public static int PORT = 14445;

	// Token: 0x040006CF RID: 1743
	public static string IP2;

	// Token: 0x040006D0 RID: 1744
	public static int PORT2;

	// Token: 0x040006D1 RID: 1745
	public static sbyte PROVIDER;

	// Token: 0x040006D2 RID: 1746
	public static int LANGUAGE;

	// Token: 0x040006D3 RID: 1747
	public static string VERSION = "2.4.0";

	// Token: 0x040006D4 RID: 1748
	public static int intVERSION = 240;

	// Token: 0x040006D5 RID: 1749
	public static GameCanvas gameCanvas;

	// Token: 0x040006D6 RID: 1750
	public static GameMidlet instance;

	// Token: 0x040006D7 RID: 1751
	public static bool isConnect2;

	// Token: 0x040006D8 RID: 1752
	public static bool isBackWindowsPhone;
}
