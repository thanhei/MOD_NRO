using System;

// Token: 0x02000096 RID: 150
public class ServerListScreen : mScreen, IActionListener
{
	// Token: 0x060007D3 RID: 2003 RVA: 0x00078538 File Offset: 0x00076738
	public ServerListScreen()
	{
		int num = 4;
		int num2 = num * 32 + 23 + 33;
		int w = GameCanvas.w;
		this.initCommand();
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (this.cmdCallHotline == null)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				this.cmdCallHotline.y = 8;
			}
		}
		ServerListScreen.cmdUpdateServer = new Command();
		ServerListScreen.cmdUpdateServer.actionChat = delegate(string str)
		{
			string text = str;
			string text2 = str;
			if (text == null)
			{
				text = ServerListScreen.linkDefault;
				return;
			}
			if (text == null && text2 != null)
			{
				if (text2.Equals(string.Empty) || text2.Length < 20)
				{
					text2 = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text2);
			}
			if (text != null && text2 == null)
			{
				if (text.Equals(string.Empty) || text.Length < 20)
				{
					text = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text);
			}
			if (text != null && text2 != null)
			{
				if (text.Length > text2.Length)
				{
					ServerListScreen.getServerList(text);
					return;
				}
				ServerListScreen.getServerList(text2);
			}
		};
		this.setLinkDefault(mSystem.LANGUAGE);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00078634 File Offset: 0x00076834
	public static void createDeleteRMS()
	{
		if (ServerListScreen.cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
			ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00078694 File Offset: 0x00076894
	internal void initCommand()
	{
		this.nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		if (text == null)
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else if (text.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else
		{
			this.nCmdPlay = 1;
		}
		this.cmd = new Command[(mGraphics.zoomLevel <= 1) ? (4 + this.nCmdPlay) : (3 + this.nCmdPlay)];
		int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
		for (int i = 0; i < this.cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
				this.cmd[0] = new Command(string.Empty, this, 3, null);
				if (text == null)
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else if (text.Equals(string.Empty))
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else
				{
					this.cmd[0].caption = mResources.playAcc + ": " + text;
					if (this.cmd[0].caption.Length > 23)
					{
						this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
						Command command = this.cmd[0];
						command.caption += "...";
					}
				}
				break;
			case 1:
				if (this.nCmdPlay == 1)
				{
					this.cmd[1] = new Command(string.Empty, this, 10100, null);
					this.cmd[1].caption = mResources.playNew;
				}
				else
				{
					this.cmd[1] = new Command(mResources.change_account, this, 7, null);
				}
				break;
			case 2:
				if (this.nCmdPlay == 1)
				{
					this.cmd[2] = new Command(mResources.change_account, this, 7, null);
				}
				else
				{
					this.cmd[2] = new Command(string.Empty, this, 17, null);
				}
				break;
			case 3:
				if (this.nCmdPlay == 1)
				{
					this.cmd[3] = new Command(string.Empty, this, 17, null);
				}
				else
				{
					this.cmd[3] = new Command(mResources.option, this, 8, null);
				}
				break;
			case 4:
				this.cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			this.cmd[i].y = num;
			this.cmd[i].setType();
			this.cmd[i].x = (GameCanvas.w - this.cmd[i].w) / 2;
			num += 30;
		}
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x000789CB File Offset: 0x00076BCB
	public static void doUpdateServer()
	{
		if (ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x000789F4 File Offset: 0x00076BF4
	public static void getServerList(string str)
	{
		ServerListScreen.lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz(">>> getServerList= " + str);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		ServerListScreen.nameServer = new string[array.Length - 2];
		ServerListScreen.address = new string[array.Length - 2];
		ServerListScreen.port = new short[array.Length - 2];
		ServerListScreen.language = new sbyte[array.Length - 2];
		ServerListScreen.typeSv = new sbyte[array.Length - 2];
		ServerListScreen.isNew = new sbyte[array.Length - 2];
		ServerListScreen.hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			ServerListScreen.nameServer[i] = array2[0];
			ServerListScreen.address[i] = array2[1];
			ServerListScreen.port[i] = short.Parse(array2[2]);
			ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
			try
			{
				ServerListScreen.typeSv[i] = sbyte.Parse(array2[4].Trim());
			}
			catch (Exception)
			{
				ServerListScreen.typeSv[i] = 0;
			}
			try
			{
				ServerListScreen.isNew[i] = sbyte.Parse(array2[5].Trim());
			}
			catch (Exception)
			{
				ServerListScreen.isNew[i] = 0;
			}
			ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
		}
		ServerListScreen.serverPriority = sbyte.Parse(array[array.Length - 1]);
		ServerListScreen.saveIP();
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00078B90 File Offset: 0x00076D90
	public override void paint(mGraphics g)
	{
		if (!ServerListScreen.loadScreen)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (ServerListScreen.bigOk)
			{
			}
		}
		else
		{
			GameCanvas.paintBGGameScr(g);
		}
		int num = 2;
		mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
		{
			"v",
			GameMidlet.VERSION,
			"(",
			mGraphics.zoomLevel.ToString(),
			")"
		}), GameCanvas.w - 2, num + 15, 1, mFont.tahoma_7_grey);
		string text = string.Empty;
		text = ((ServerListScreen.testConnect != 0) ? (text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " connected") : (text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " disconnect"));
		if (mSystem.isTest)
		{
			mFont.tahoma_7_white.drawString(g, text, GameCanvas.w - 2, num + 15 + 15, 1, mFont.tahoma_7_grey);
		}
		if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
		{
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
			}
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
		}
		int w = GameCanvas.w;
		if (ServerListScreen.cmdDeleteRMS != null)
		{
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		if (GameCanvas.currentDialog == null)
		{
			if (!ServerListScreen.loadScreen)
			{
				if (!ServerListScreen.bigOk)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					if (!ServerListScreen.isGetData)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
					}
					else
					{
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + ServerListScreen.percent.ToString() + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
					}
				}
			}
			else
			{
				int num2 = GameCanvas.hh - 15 * this.cmd.Length - 15;
				if (num2 < 25)
				{
					num2 = 25;
				}
				if (LoginScr.imgTitle != null)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num2, 3);
				}
				for (int i = 0; i < this.cmd.Length; i++)
				{
					this.cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (ServerListScreen.testConnect == -1)
				{
					if (GameCanvas.gameTick % 20 > 10)
					{
						g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 10, 0);
					}
				}
				else
				{
					g.drawRegion(GameScr.imgRoomStat, 0, ServerListScreen.testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 9, 0);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00078F94 File Offset: 0x00077194
	public void selectServer()
	{
		ServerListScreen.flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		Session_ME.gI().close();
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		GameMidlet.LANGUAGE = (int)ServerListScreen.language[ServerListScreen.ipSelect];
		Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
		if (ServerListScreen.language[ServerListScreen.ipSelect] != mResources.language)
		{
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.initCommand();
		ServerListScreen.loadScreen = true;
		ServerListScreen.countDieConnect = 0;
		Controller.isConnectOK = false;
		ServerListScreen.testConnect = -1;
		ServerListScreen.isAutoConect = true;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00079054 File Offset: 0x00077254
	public override void update()
	{
		if (ServerListScreen.waitToLogin)
		{
			ServerListScreen.tWaitToLogin++;
			if (ServerListScreen.tWaitToLogin == 50)
			{
				GameCanvas.serverScreen.selectServer();
			}
			if (ServerListScreen.tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				ServerListScreen.waitToLogin = false;
			}
		}
		if (ServerListScreen.flagServer > 0)
		{
			ServerListScreen.flagServer--;
			if (ServerListScreen.flagServer == 0)
			{
				GameCanvas.endDlg();
			}
			if (ServerListScreen.testConnect == 2)
			{
				ServerListScreen.flagServer = 0;
				GameCanvas.endDlg();
			}
		}
		if (ServerListScreen.flagServer <= 0 && ServerListScreen.isAutoConect)
		{
			ServerListScreen.countDieConnect++;
			if (ServerListScreen.countDieConnect > 100000)
			{
				ServerListScreen.countDieConnect = 0;
			}
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			if (i == ServerListScreen.selected)
			{
				this.cmd[i].isFocus = true;
			}
			else
			{
				this.cmd[i].isFocus = false;
			}
		}
		GameScr.cmx++;
		if (!ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100))
		{
			ServerListScreen.cmdDownload = null;
		}
		base.update();
		if (global::Char.isLoadingMap || !ServerListScreen.loadScreen || !ServerListScreen.isAutoConect || GameCanvas.currentScreen != this || ServerListScreen.testConnect == 2)
		{
			return;
		}
		if (ServerListScreen.countDieConnect < ((mSystem.clientType != 1) ? 5 : 2))
		{
			if (ServerListScreen.flagServer <= 0)
			{
				ServerListScreen.flagServer = 30;
				GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
				GameCanvas.connect();
				return;
			}
		}
		else if (!Session_ME.gI().isConnected())
		{
			if (ServerListScreen.flagServer <= 0)
			{
				Command command = new Command(mResources.YES, GameCanvas.serverScreen, 18, null);
				Command command2 = new Command(mResources.NO, GameCanvas.serverScreen, 19, null);
				GameCanvas.startYesNoDlg(mResources.maychutathoacmatsong + "." + mResources.confirmChangeServer, command, command2);
				ServerListScreen.flagServer = 30;
				return;
			}
		}
		else if (ServerListScreen.flagServer <= 0)
		{
			ServerListScreen.countDieConnect = 0;
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00079249 File Offset: 0x00077449
	internal void processInput()
	{
		if (ServerListScreen.loadScreen)
		{
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
			return;
		}
		this.center = ServerListScreen.cmdDownload;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00079281 File Offset: 0x00077481
	public static void updateDeleteData()
	{
		if (ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside())
		{
			ServerListScreen.cmdDeleteRMS.performAction();
		}
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x000792A0 File Offset: 0x000774A0
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			ServerListScreen.updateDeleteData();
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
			if (!ServerListScreen.loadScreen)
			{
				if (ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside())
				{
					ServerListScreen.cmdDownload.performAction();
				}
				base.updateKey();
				return;
			}
			for (int i = 0; i < this.cmd.Length; i++)
			{
				if (this.cmd[i] != null && this.cmd[i].isPointerPressInside())
				{
					if (ServerListScreen.testConnect == -1 || ServerListScreen.testConnect == 0)
					{
						if (this.cmd[i].caption.IndexOf(mResources.server) != -1)
						{
							this.cmd[i].performAction();
						}
					}
					else
					{
						this.cmd[i].performAction();
					}
				}
			}
		}
		else if (ServerListScreen.loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[8] = false;
				ServerListScreen.selected++;
				if (ServerListScreen.selected > num)
				{
					ServerListScreen.selected = 0;
				}
				this.processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num2 = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[2] = false;
				ServerListScreen.selected--;
				if (ServerListScreen.selected < 0)
				{
					ServerListScreen.selected = num2;
				}
				this.processInput();
			}
		}
		if (!ServerListScreen.isWait)
		{
			base.updateKey();
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00079410 File Offset: 0x00077610
	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)ServerListScreen.nameServer.Length);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(ServerListScreen.nameServer[i]);
				dataOutputStream.writeUTF(ServerListScreen.address[i]);
				dataOutputStream.writeShort(ServerListScreen.port[i]);
				dataOutputStream.writeByte(ServerListScreen.language[i]);
				try
				{
					dataOutputStream.writeByte(ServerListScreen.typeSv[i]);
				}
				catch (Exception)
				{
					dataOutputStream.writeByte(0);
				}
				try
				{
					dataOutputStream.writeByte(ServerListScreen.isNew[i]);
				}
				catch (Exception)
				{
					dataOutputStream.writeByte(0);
				}
			}
			ServerListScreen.serverPriority = ((!mSystem.isTest) ? ServerListScreen.serverPriority : (ServerListScreen.serverPriority + 5));
			dataOutputStream.writeByte(ServerListScreen.serverPriority);
			Rms.saveRMS(ServerListScreen.RMS_NRlink, dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00079524 File Offset: 0x00077724
	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!ServerListScreen.hasConnected[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0007954C File Offset: 0x0007774C
	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS(ServerListScreen.RMS_NRlink);
		if (array == null)
		{
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			ServerListScreen.lengthServer = new int[3];
			mResources.loadLanguague(dataInputStream.readByte());
			sbyte b = dataInputStream.readByte();
			ServerListScreen.nameServer = new string[(int)b];
			ServerListScreen.address = new string[(int)b];
			ServerListScreen.port = new short[(int)b];
			ServerListScreen.language = new sbyte[(int)b];
			ServerListScreen.typeSv = new sbyte[(int)b];
			ServerListScreen.isNew = new sbyte[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				ServerListScreen.nameServer[i] = dataInputStream.readUTF();
				ServerListScreen.address[i] = dataInputStream.readUTF();
				ServerListScreen.port[i] = dataInputStream.readShort();
				ServerListScreen.language[i] = dataInputStream.readByte();
				try
				{
					ServerListScreen.typeSv[i] = dataInputStream.readByte();
				}
				catch (Exception)
				{
					ServerListScreen.typeSv[i] = 0;
				}
				try
				{
					ServerListScreen.isNew[i] = dataInputStream.readByte();
				}
				catch (Exception)
				{
					ServerListScreen.isNew[i] = 0;
				}
				ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
			}
			ServerListScreen.serverPriority = dataInputStream.readByte();
			dataInputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x000796D8 File Offset: 0x000778D8
	public override void switchToMe()
	{
		EffectManager.remove();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		global::Char.isLoadingMap = false;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000797E8 File Offset: 0x000779E8
	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00004887 File Offset: 0x00002A87
	public void connectOk()
	{
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x000798EC File Offset: 0x00077AEC
	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		ServerListScreen.demPercent = 0;
		ServerListScreen.percent = 0;
		ServerListScreen.stopDownload = true;
		GameCanvas.serverScreen.show2();
		ServerListScreen.isGetData = false;
		mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData.ToString());
		ServerListScreen.cmdDownload.isFocus = true;
		this.center = new Command(string.Empty, this, 2, null);
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00079964 File Offset: 0x00077B64
	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction.ToString());
		if (idAction == 1000)
		{
			GameCanvas.connect();
		}
		if (idAction == 1 || idAction == 4)
		{
			Session_ME.gI().close();
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.loadScreen = true;
			ServerListScreen.testConnect = 0;
			ServerListScreen.isGetData = false;
			mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData.ToString());
			Rms.clearAll();
			this.switchToMe();
		}
		if (idAction == 2)
		{
			ServerListScreen.stopDownload = false;
			ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
			this.right = null;
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			this.center = new Command(string.Empty, this, 4, null);
			if (!ServerListScreen.isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					ServerListScreen.cmdDownload.isFocus = true;
					this.center = new Command(string.Empty, this, 4, null);
					mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData.ToString());
				}
				ServerListScreen.isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			bool flag = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty);
			if (!flag && !flag2)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
				if (text == null || text.Equals(string.Empty))
				{
					Service.gI().login2(string.Empty);
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				if (Session_ME.connected)
				{
					GameCanvas.startWaitDlg();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
			}
			else
			{
				GameCanvas.loginScr.doLogin();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 5)
		{
			ServerListScreen.doUpdateServer();
			if (ServerListScreen.nameServer.Length == 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
			{
				GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
			}
		}
		if (idAction == 6)
		{
			ServerListScreen.ipSelect = GameCanvas.menu.menuSelectedItem;
			this.selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool flag3 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (flag3)
			{
				GameCanvas.menu.menuSelectedItem = 0;
			}
			else
			{
				GameCanvas.menu.menuSelectedItem = 1;
			}
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			if (text2 == null || text2.Equals(string.Empty))
			{
				Service.gI().login2(string.Empty);
			}
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
			}
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
		{
			GameMidlet.instance.exit();
		}
		if (idAction == 13 && (!ServerListScreen.isGetData || ServerListScreen.loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			}
		}
		if (idAction == 14)
		{
			Command command = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command command2 = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, command, command2);
		}
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		if (idAction == 17)
		{
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 18)
		{
			GameCanvas.endDlg();
			InfoDlg.hide();
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 19)
		{
			if (mSystem.clientType == 1)
			{
				InfoDlg.hide();
				GameCanvas.currentDialog = null;
				return;
			}
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.testConnect = 0;
			ServerListScreen.isAutoConect = true;
		}
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00079FAC File Offset: 0x000781AC
	public void init()
	{
		if (!ServerListScreen.loadScreen)
		{
			ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			ServerListScreen.cmdDownload.isFocus = true;
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
			if (ServerListScreen.cmdDownload.y > GameCanvas.h - 26)
			{
				ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
			}
		}
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0007A044 File Offset: 0x00078244
	public void show2()
	{
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		this.initCommand();
		ServerListScreen.loadScreen = false;
		ServerListScreen.percent = 0;
		ServerListScreen.bigOk = false;
		ServerListScreen.isGetData = false;
		mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData.ToString());
		ServerListScreen.p = 0;
		ServerListScreen.demPercent = 0;
		ServerListScreen.strWait = mResources.PLEASEWAIT;
		global::Char.isLoadingMap = false;
		this.init();
		base.switchToMe();
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00004887 File Offset: 0x00002A87
	public void setLinkDefault(sbyte language)
	{
	}

	// Token: 0x04000EC3 RID: 3779
	public static string[] nameServer;

	// Token: 0x04000EC4 RID: 3780
	public static string[] address;

	// Token: 0x04000EC5 RID: 3781
	public static sbyte serverPriority;

	// Token: 0x04000EC6 RID: 3782
	public static bool[] hasConnected;

	// Token: 0x04000EC7 RID: 3783
	public static short[] port;

	// Token: 0x04000EC8 RID: 3784
	public static int selected;

	// Token: 0x04000EC9 RID: 3785
	public static bool isWait;

	// Token: 0x04000ECA RID: 3786
	public static Command cmdUpdateServer;

	// Token: 0x04000ECB RID: 3787
	public static sbyte[] language;

	// Token: 0x04000ECC RID: 3788
	public static sbyte[] typeSv;

	// Token: 0x04000ECD RID: 3789
	public static sbyte[] isNew;

	// Token: 0x04000ECE RID: 3790
	internal Command[] cmd;

	// Token: 0x04000ECF RID: 3791
	internal Command cmdCallHotline;

	// Token: 0x04000ED0 RID: 3792
	internal int nCmdPlay;

	// Token: 0x04000ED1 RID: 3793
	public static Command cmdDeleteRMS;

	// Token: 0x04000ED2 RID: 3794
	internal int lY;

	// Token: 0x04000ED3 RID: 3795
	public static string smartPhoneVN = "Vũ trụ 1:dragon1.teamobi.com:14445:0:0:0,Vũ trụ 2:dragon2.teamobi.com:14445:0:0:0,Vũ trụ 3:dragon3.teamobi.com:14445:0:0:0,Vũ trụ 4:dragon4.teamobi.com:14445:0:0:0,Vũ trụ 5:dragon5.teamobi.com:14445:0:0:0,Vũ trụ 6:dragon6.teamobi.com:14445:0:0:0,Vũ trụ 7:dragon7.teamobi.com:14445:0:0:0,Vũ trụ 8:dragon10.teamobi.com:14446:0:0:0,Vũ trụ 9:dragon10.teamobi.com:14447:0:0:0,Vũ trụ 10:dragon10.teamobi.com:14445:0:0:0,Vũ trụ 11:dragon11.teamobi.com:14445:0:0:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0:0:0,Universe 1:dragon.indonaga.com:14445:1:0:0,Naga:dragon.indonaga.com:14446:2:0:0,0,0";

	// Token: 0x04000ED4 RID: 3796
	public static string javaVN = "Vũ trụ 1:112.213.94.23:14445:0:0:0,Vũ trụ 2:210.211.109.199:14445:0:0:0,Vũ trụ 3:112.213.85.88:14445:0:0:0,Vũ trụ 4:27.0.12.164:14445:0:0:0,Vũ trụ 5:27.0.12.16:14445:0:0:0,Vũ trụ 6:27.0.12.173:14445:0:0:0,Vũ trụ 7:112.213.94.223:14445:0:0:0,Vũ trụ 8:27.0.14.66:14446:0:0:0,Vũ trụ 9:27.0.14.66:14447:0:0:0,Vũ trụ 10:27.0.14.66:14445:0:0:0,Vũ trụ 11:112.213.85.35:14445:0:0:0,Võ đài liên vũ trụ:27.0.12.173:20000:0:0:0,Universe 1:52.74.230.22:14445:1:0:0,Naga:52.74.230.22:14446:2:0:0,0,0";

	// Token: 0x04000ED5 RID: 3797
	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2:0:0,2,0";

	// Token: 0x04000ED6 RID: 3798
	public static string javaIn = "Naga:52.74.230.22:14446:2:0:0,2,0";

	// Token: 0x04000ED7 RID: 3799
	public static string smartPhoneE = "Universe 1:dragon.indonaga.com:14445:1:0:0,1,0";

	// Token: 0x04000ED8 RID: 3800
	public static string javaE = "Universe 1:52.74.230.22:14445:1:0:0,1,0";

	// Token: 0x04000ED9 RID: 3801
	public static string linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";

	// Token: 0x04000EDA RID: 3802
	public static string linkDefault = ServerListScreen.javaVN;

	// Token: 0x04000EDB RID: 3803
	public const sbyte languageVersion = 2;

	// Token: 0x04000EDC RID: 3804
	public new int keyTouch = -1;

	// Token: 0x04000EDD RID: 3805
	internal int tam;

	// Token: 0x04000EDE RID: 3806
	public static bool stopDownload;

	// Token: 0x04000EDF RID: 3807
	public static string linkweb = "http://ngocrongonline.com";

	// Token: 0x04000EE0 RID: 3808
	public static int countDieConnect;

	// Token: 0x04000EE1 RID: 3809
	public static bool waitToLogin;

	// Token: 0x04000EE2 RID: 3810
	public static int tWaitToLogin;

	// Token: 0x04000EE3 RID: 3811
	public static string RMS_NRlink = "NRlink3";

	// Token: 0x04000EE4 RID: 3812
	public static int[] lengthServer = new int[3];

	// Token: 0x04000EE5 RID: 3813
	public static int ipSelect;

	// Token: 0x04000EE6 RID: 3814
	public static int flagServer;

	// Token: 0x04000EE7 RID: 3815
	public static bool bigOk;

	// Token: 0x04000EE8 RID: 3816
	public static int percent;

	// Token: 0x04000EE9 RID: 3817
	public static string strWait;

	// Token: 0x04000EEA RID: 3818
	public static int nBig;

	// Token: 0x04000EEB RID: 3819
	public static int nBg;

	// Token: 0x04000EEC RID: 3820
	public static int demPercent;

	// Token: 0x04000EED RID: 3821
	public static int maxBg;

	// Token: 0x04000EEE RID: 3822
	public static bool isGetData = false;

	// Token: 0x04000EEF RID: 3823
	public static Command cmdDownload;

	// Token: 0x04000EF0 RID: 3824
	internal Command cmdStart;

	// Token: 0x04000EF1 RID: 3825
	public string dataSize;

	// Token: 0x04000EF2 RID: 3826
	public static int p;

	// Token: 0x04000EF3 RID: 3827
	public static int testConnect = -1;

	// Token: 0x04000EF4 RID: 3828
	public static bool loadScreen;

	// Token: 0x04000EF5 RID: 3829
	public static bool isAutoConect = true;
}
