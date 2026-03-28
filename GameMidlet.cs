using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class GameMidlet
{
	// Token: 0x06000ACE RID: 2766 RVA: 0x00008FB8 File Offset: 0x000071B8
	public GameMidlet()
	{
		this.initGame();
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x000A1690 File Offset: 0x0009F890
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

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00008FC6 File Offset: 0x000071C6
	public void exit()
	{
		if (Main.typeClient == 6)
		{
			mSystem.exitWP();
		}
		else
		{
			GameCanvas.bRun = false;
			mSystem.gcc();
			this.notifyDestroyed();
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x00008FEE File Offset: 0x000071EE
	public static void sendSMS(string data, string to, Command successAction, Command failAction)
	{
		Cout.println("SEND SMS");
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x00008FFA File Offset: 0x000071FA
	public static void flatForm(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00009012 File Offset: 0x00007212
	public void notifyDestroyed()
	{
		Main.exit();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00009019 File Offset: 0x00007219
	public void platformRequest(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x04001483 RID: 5251
	public static string IP = "112.213.94.23";

	// Token: 0x04001484 RID: 5252
	public static int PORT = 14445;

	// Token: 0x04001485 RID: 5253
	public static string IP2;

	// Token: 0x04001486 RID: 5254
	public static int PORT2;

	// Token: 0x04001487 RID: 5255
	public static sbyte PROVIDER;

	// Token: 0x04001488 RID: 5256
	public static int LANGUAGE;

	// Token: 0x04001489 RID: 5257
	public static string VERSION = "2.4.7";

	// Token: 0x0400148A RID: 5258
	public static int intVERSION = 247;

	// Token: 0x0400148B RID: 5259
	public static GameCanvas gameCanvas;

	// Token: 0x0400148C RID: 5260
	public static GameMidlet instance;

	// Token: 0x0400148D RID: 5261
	public static bool isConnect2;

	// Token: 0x0400148E RID: 5262
	public static bool isBackWindowsPhone;
}
