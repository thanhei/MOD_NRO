using System;

// Token: 0x020000AE RID: 174
public class SplashScr : mScreen
{
	// Token: 0x06000956 RID: 2390 RVA: 0x00083650 File Offset: 0x00081850
	public SplashScr()
	{
		SplashScr.instance = this;
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00083666 File Offset: 0x00081866
	public static void loadSplashScr()
	{
		SplashScr.splashScrStat = 0;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00083670 File Offset: 0x00081870
	public override void update()
	{
		if (SplashScr.splashScrStat == 30 && !this.isCheckConnect)
		{
			this.isCheckConnect = true;
			if (Rms.loadRMSInt("serverchat") != -1)
			{
				GameScr.isPaintChatVip = Rms.loadRMSInt("serverchat") == 0;
			}
			if (Rms.loadRMSInt("isPlaySound") != -1)
			{
				GameCanvas.isPlaySound = Rms.loadRMSInt("isPlaySound") == 1;
			}
			if (GameCanvas.isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
			}
			SoundMn.gI().getStrOption();
			if (Rms.loadRMSInt("svselect") == -1)
			{
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				GameCanvas.serverScr.switchToMe();
			}
			else
			{
				ServerListScreen.loadIP();
			}
		}
		SplashScr.splashScrStat++;
		ServerListScreen.updateDeleteData();
		if (SplashScr.splashScrStat >= 150)
		{
			Res.outz("cho man hinh nay qa lau");
			if (Session_ME.gI().isConnected())
			{
				ServerListScreen.loadScreen = true;
				GameCanvas.serverScreen.switchToMe();
				return;
			}
			mSystem.onDisconnected();
		}
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0008376C File Offset: 0x0008196C
	public static void loadIP()
	{
		if (Rms.loadRMSInt("svselect") == -1)
		{
			Res.err(">>>loadIP:  svselect == -1");
			int num = 0;
			if (mResources.language > 0)
			{
				for (int i = 0; i < (int)mResources.language; i++)
				{
					num += ServerListScreen.lengthServer[i];
				}
			}
			if (ServerListScreen.serverPriority == -1)
			{
				ServerListScreen.ipSelect = num + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
			}
			else
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
			}
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
			return;
		}
		ServerListScreen.ipSelect = Rms.loadRMSInt("svselect");
		Res.err(">>>loadIP:  ipSelect == " + ServerListScreen.ipSelect.ToString());
		if (ServerListScreen.ipSelect > ServerListScreen.nameServer.Length - 1)
		{
			ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
		}
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		GameCanvas.connect();
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000838D0 File Offset: 0x00081AD0
	public override void paint(mGraphics g)
	{
		if (SplashScr.imgLogo != null && SplashScr.splashScrStat < 30)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(SplashScr.imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		if (SplashScr.nData != -1)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + (SplashScr.nData * 100 / SplashScr.maxData).ToString() + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
			return;
		}
		if (SplashScr.splashScrStat >= 30)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
			if (ServerListScreen.cmdDeleteRMS != null)
			{
				mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00083A1C File Offset: 0x00081C1C
	public static void loadImg()
	{
		SplashScr.imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}

	// Token: 0x0400101D RID: 4125
	public static int splashScrStat;

	// Token: 0x0400101E RID: 4126
	internal bool isCheckConnect;

	// Token: 0x0400101F RID: 4127
	internal bool isSwitchToLogin;

	// Token: 0x04001020 RID: 4128
	public static int nData = -1;

	// Token: 0x04001021 RID: 4129
	public static int maxData = -1;

	// Token: 0x04001022 RID: 4130
	public static SplashScr instance;

	// Token: 0x04001023 RID: 4131
	public static Image imgLogo;

	// Token: 0x04001024 RID: 4132
	internal int timeLoading = 10;

	// Token: 0x04001025 RID: 4133
	public long TIMEOUT;
}
