using System;
using System.Runtime.CompilerServices;
using Assets.src.e;
using Assets.src.g;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class GameCanvas : IActionListener
{
	// Token: 0x060002E3 RID: 739 RVA: 0x000346A8 File Offset: 0x000328A8
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		if (num == -1)
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		else if (num != 2)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		if (GameCanvas.clearOldData != 1)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00034758 File Offset: 0x00032958
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00034760 File Offset: 0x00032960
	public void initGame()
	{
		MotherCanvas.instance.setChildCanvas(this);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.isTouch = true;
		if (GameCanvas.w >= 240)
		{
			GameCanvas.isTouchControl = true;
		}
		if (GameCanvas.w < 320)
		{
			GameCanvas.isTouchControlSmallScreen = true;
		}
		if (GameCanvas.w >= 320)
		{
			GameCanvas.isTouchControlLargeScreen = true;
		}
		GameCanvas.msgdlg = new MsgDlg();
		if (GameCanvas.h <= 160)
		{
			Paint.hTab = 15;
			mScreen.cmdH = 17;
		}
		GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
		GameCanvas.instance = this;
		mFont.init();
		mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
		this.initPaint();
		this.loadDust();
		this.loadWaterSplash();
		GameCanvas.panel = new Panel();
		GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
		int num = Rms.loadRMSInt("clienttype");
		if (num != -1)
		{
			if (num > 7)
			{
				Rms.saveRMSInt("clienttype", mSystem.clientType);
			}
			else
			{
				mSystem.clientType = num;
			}
		}
		if (mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty))
		{
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
		}
		GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		GameCanvas.debugUpdate = new MyVector();
		GameCanvas.debugPaint = new MyVector();
		GameCanvas.debugSession = new MyVector();
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i.ToString() + ".png");
		}
		GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
		GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
		GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
		GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
		Panel.graphics = Rms.loadRMSInt("lowGraphic");
		GameCanvas.lowGraphic = Rms.loadRMSInt("lowGraphic") == 1;
		GameScr.isPaintChatVip = Rms.loadRMSInt("serverchat") != 1;
		global::Char.isPaintAura = Rms.loadRMSInt("isPaintAura") == 1;
		global::Char.isPaintAura2 = Rms.loadRMSInt("isPaintAura2") == 1;
		Res.init();
		SmallImage.loadBigImage();
		Panel.WIDTH_PANEL = 176;
		if (Panel.WIDTH_PANEL > GameCanvas.w)
		{
			Panel.WIDTH_PANEL = GameCanvas.w;
		}
		InfoMe.gI().loadCharId();
		Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
		Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
		Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
		Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
		Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
		Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
		GameCanvas.serverScreen = new ServerListScreen();
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		for (int j = 0; j < 7; j++)
		{
			GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j.ToString() + ".png");
			GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j.ToString() + ".png");
		}
		ServerListScreen.createDeleteRMS();
		GameCanvas.serverScr = new ServerScr();
		GameCanvas.loginScr = new LoginScr();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00034AF0 File Offset: 0x00032CF0
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00034AF7 File Offset: 0x00032CF7
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00034B03 File Offset: 0x00032D03
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00034B18 File Offset: 0x00032D18
	public void update()
	{
		int num = GameCanvas.gameTick % 100;
		if (GameCanvas.isRequestMapID == 2 && GameCanvas.waitingTimeChangeMap < mSystem.currentTimeMillis() && GameCanvas.gameTick % 2 == 0 && GameCanvas.currentScreen != null)
		{
			if (GameCanvas.currentScreen == GameScr.gI() && ServerListScreen.waitToLogin)
			{
				ServerListScreen.waitToLogin = false;
			}
			if (SmallImage.vt_images_watingDowload.size() > 0)
			{
				Small small = (Small)SmallImage.vt_images_watingDowload.elementAt(0);
				Service.gI().requestIcon(small.id);
				SmallImage.vt_images_watingDowload.removeElementAt(0);
			}
			Effect.dowloadEff.size();
		}
		if (mSystem.currentTimeMillis() > this.timefps)
		{
			this.timefps += 1000L;
			GameCanvas.max = GameCanvas.fps;
			GameCanvas.fps = 0;
		}
		GameCanvas.fps++;
		if (GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null)
		{
			GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
			GameCanvas.messageServer.removeElementAt(0);
		}
		if (GameCanvas.gameTick % 5 == 0)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			if (TouchScreenKeyboard.visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				if (GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num2 = mSystem.currentTimeMillis();
			if (num2 - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1)
			{
				GameCanvas.timeTickEff1 = num2;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			if (num2 - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2)
			{
				GameCanvas.timeTickEff2 = num2;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			if (GameCanvas.taskTick > 0)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			if (GameCanvas.gameTick > 10000)
			{
				if (mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameMidlet.instance.exit();
				}
				GameCanvas.gameTick = 0;
			}
			if (GameCanvas.currentScreen != null)
			{
				if (ChatPopup.serverChatPopUp != null)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else if (ChatPopup.currChatPopup != null)
				{
					ChatPopup.currChatPopup.update();
					ChatPopup.currChatPopup.updateKey();
				}
				else if (GameCanvas.currentDialog != null)
				{
					GameCanvas.debug("B", 0);
					GameCanvas.currentDialog.update();
				}
				else if (GameCanvas.menu.showMenu)
				{
					GameCanvas.debug("C", 0);
					GameCanvas.menu.updateMenu();
					GameCanvas.debug("D", 0);
					GameCanvas.menu.updateMenuKey();
				}
				else if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.update();
					if (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H))
					{
						GameCanvas.isFocusPanel2 = false;
					}
					if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
					{
						GameCanvas.panel2.update();
						if (GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
						{
							GameCanvas.isFocusPanel2 = true;
						}
					}
					if (GameCanvas.panel2 != null)
					{
						if (GameCanvas.isFocusPanel2)
						{
							GameCanvas.panel2.updateKey();
						}
						else
						{
							GameCanvas.panel.updateKey();
						}
					}
					else
					{
						GameCanvas.panel.updateKey();
					}
					if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
					{
						GameCanvas.panel.chatTFUpdateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
					{
						GameCanvas.panel2.chatTFUpdateKey();
					}
					else if ((GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null)
					{
						GameCanvas.panel.updateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
					{
						GameCanvas.panel2.updateKey();
					}
					if (GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine)
					{
						GameCanvas.panel.hide();
					}
				}
				GameCanvas.debug("E", 0);
				if (!GameCanvas.isLoading)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				if (!GameCanvas.panel.isShow && (GameCanvas.panel2 == null || !GameCanvas.panel2.isShow) && ChatPopup.serverChatPopUp == null)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			if (this.resetToLoginScr)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.loginScr);
			}
			GameCanvas.debug("Zzz", 0);
			if ((GameCanvas.currentScreen != GameCanvas.serverScr || !GameCanvas.serverScr.isPaintNewUi) && Controller.isConnectOK)
			{
				if (Controller.isMain)
				{
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
					ServerListScreen.testConnect = 2;
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					Rms.saveIP(GameMidlet.IP + ":" + GameMidlet.PORT.ToString());
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			if (Controller.isDisconnected)
			{
				Debug.Log("disconnect");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession)
					{
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			if (Controller.isConnectionFail)
			{
				Debug.Log("connect fail");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
					{
						ServerListScreen.testConnect = 0;
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onConnectionFail();
					}
				}
				else if (Session_ME.gI().isCompareIPConnect())
				{
					this.onConnectionFail();
				}
				Controller.isConnectionFail = false;
			}
			if (Main.isResume)
			{
				Main.isResume = false;
				if (GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr)
			{
				GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
				if (GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60)
				{
					GameCanvas.dir_ = 0;
					this.tickWaitThongBao++;
					if (this.tickWaitThongBao > 150)
					{
						this.tickWaitThongBao = 0;
						GameCanvas.thongBaoTest = null;
					}
				}
			}
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen.Equals(GameScr.gI()))
			{
				if (GameScr.info1 != null)
				{
					GameScr.info1.update();
				}
				if (GameScr.info2 != null)
				{
					GameScr.info2.update();
				}
			}
			GameCanvas.isPointerSelect = false;
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00035340 File Offset: 0x00033540
	public void onDisconnected()
	{
		if (Controller.isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Controller.isLoadingData)
		{
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
			return;
		}
		if (GameCanvas.currentScreen != GameCanvas.serverScreen)
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
		}
		else
		{
			GameCanvas.endDlg();
		}
		global::Char.isLoadingMap = false;
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		GameCanvas.instance.resetToLoginScrz();
		mSystem.endKey();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x000353F0 File Offset: 0x000335F0
	public void onConnectionFail()
	{
		if (GameCanvas.currentScreen.Equals(SplashScr.instance))
		{
			if (ServerListScreen.hasConnected == null)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				return;
			}
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
			if (!ServerListScreen.hasConnected[0])
			{
				ServerListScreen.hasConnected[0] = true;
				ServerListScreen.ipSelect = 0;
				GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
				GameCanvas.connect();
				return;
			}
			if (!ServerListScreen.hasConnected[2])
			{
				ServerListScreen.hasConnected[2] = true;
				ServerListScreen.ipSelect = 2;
				GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
				GameCanvas.connect();
				return;
			}
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			return;
		}
		else
		{
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			if (Controller.isLoadingData)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isConnectionFail = false;
				return;
			}
			GameCanvas.isResume = true;
			LoginScr.isContinueToLogin = false;
			if (GameCanvas.loginScr != null)
			{
				GameCanvas.instance.resetToLoginScrz();
			}
			else
			{
				GameCanvas.loginScr = new LoginScr();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			if (GameCanvas.currentScreen != GameCanvas.serverScreen)
			{
				ServerListScreen.countDieConnect = 0;
			}
			else
			{
				GameCanvas.endDlg();
				ServerListScreen.loadScreen = true;
				GameCanvas.serverScreen.switchToMe();
			}
			global::Char.isLoadingMap = false;
			if (Controller.isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			mSystem.endKey();
			return;
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00035571 File Offset: 0x00033771
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000355A8 File Offset: 0x000337A8
	public static void connect()
	{
		Debug.LogError(">>connect:" + GameMidlet.IP + ":" + GameMidlet.PORT.ToString());
		if (!Session_ME.gI().isConnected())
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x000355F8 File Offset: 0x000337F8
	public static void connect2()
	{
		if (!Session_ME2.gI().isConnected())
		{
			Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2.ToString());
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00035648 File Offset: 0x00033848
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00035670 File Offset: 0x00033870
	public static void resetTransGameScr(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-GameScr.cmx, -GameScr.cmy);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x000356C0 File Offset: 0x000338C0
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00004887 File Offset: 0x00002A87
	public void start()
	{
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x000357D1 File Offset: 0x000339D1
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000357D9 File Offset: 0x000339D9
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00004887 File Offset: 0x00002A87
	public static void debug(string s, int type)
	{
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000357E4 File Offset: 0x000339E4
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = (GameCanvas.isTouch ? (-1) : 0);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			if (GameCanvas.panel.tabIcon != null)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
			screen.switchToMe();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
		ServerListScreen.isAutoConect = true;
		ServerListScreen.countDieConnect = 0;
		ServerListScreen.testConnect = -1;
		ServerListScreen.loadScreen = true;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00004887 File Offset: 0x00002A87
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00004887 File Offset: 0x00002A87
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00004887 File Offset: 0x00002A87
	public static void updateBG()
	{
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000359A8 File Offset: 0x00033BA8
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		if (cmy > GameCanvas.h)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY != 0) ? (cmy >> detalY) : 0), w, h + ((detalY != 0) ? (cmy >> detalY) : 0));
	}

	// Token: 0x060002FB RID: 763 RVA: 0x000359FC File Offset: 0x00033BFC
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			if (num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks))
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				if (GameScr.gI().isFireWorks && !GameCanvas.lowGraphic)
				{
					FireWorkEff.paint(g);
				}
			}
			else if (GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null)
			{
				if (GameCanvas.moveX[num] != 0)
				{
					GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
				}
				int cmy = GameScr.cmy;
				if (cmy > GameCanvas.h)
				{
					cmy = GameCanvas.h;
				}
				if (GameCanvas.layerSpeed[num] != 0)
				{
					for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
					}
				}
				else
				{
					for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
					}
				}
				if (color1 != -1)
				{
					if (num == GameCanvas.nBg - 1)
					{
						GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
					}
				}
				if (color2 != -1)
				{
					if (num == 0)
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
					}
				}
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					if (layer == 1 && GameCanvas.typeBg == 11)
					{
						g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
					}
					if (layer == 1 && GameCanvas.typeBg == 13)
					{
						g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
					}
					if (layer == 3 && TileMap.mapID == 1)
					{
						for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
						{
							g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
						}
					}
				}
				EffecMn.paintBackGroundUnderLayer(g, -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]), GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00035E14 File Offset: 0x00034014
	public static void drawSun1(mGraphics g)
	{
		if (GameCanvas.imgSun != null)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		if (!GameCanvas.isBoltEff)
		{
			return;
		}
		if (GameCanvas.gameTick % 200 == 0)
		{
			GameCanvas.boltActive = true;
		}
		if (GameCanvas.boltActive)
		{
			GameCanvas.tBolt++;
			if (GameCanvas.tBolt == 10)
			{
				GameCanvas.tBolt = 0;
				GameCanvas.boltActive = false;
			}
			if (GameCanvas.tBolt % 2 == 0)
			{
				g.setColor(16777215);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00035EA7 File Offset: 0x000340A7
	public static void drawSun2(mGraphics g)
	{
		if (GameCanvas.imgSun2 != null)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00035EC6 File Offset: 0x000340C6
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00035ED4 File Offset: 0x000340D4
	public static void paint_ios_bg(mGraphics g)
	{
		if (mSystem.clientType != 5)
		{
			return;
		}
		if (GameCanvas.imgBgIOS != null)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			for (int i = 0; i < 3; i++)
			{
				g.drawImage(GameCanvas.imgBgIOS, GameCanvas.imgBgIOS.getWidth() * i, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			return;
		}
		GameCanvas.imgBgIOS = mSystem.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00035F70 File Offset: 0x00034170
	public static void paintBGGameScr(mGraphics g)
	{
		if (!GameCanvas.isLoadBGok)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		if (global::Char.isLoadingMap)
		{
			return;
		}
		int gW = GameScr.gW;
		int gH = GameScr.gH;
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		try
		{
			if (GameCanvas.paintBG)
			{
				if (GameCanvas.currentScreen == GameScr.gI())
				{
					if (TileMap.mapID != 172 && (TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble))
					{
						g.setColor(0);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						return;
					}
					if (TileMap.mapID == 138)
					{
						g.setColor(6776679);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						return;
					}
				}
				if (GameCanvas.typeBg == 0)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 1)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
					GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
					GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 2)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 3)
				{
					int num = GameScr.cmy - (325 - GameScr.gH23);
					g.translate(0, -num);
					GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
					GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
					g.translate(0, -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 4)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 5)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 15, GameCanvas.colorTop[3], -1);
					GameCanvas.drawSun1(g);
					g.translate(100, 10);
					GameCanvas.drawSun1(g);
					g.translate(-100, -10);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 10, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 6, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 4, -1, -1);
					g.translate(0, 27);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, -1);
					g.translate(0, 20);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					g.translate(-g.getTranslateX(), -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 6)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					g.translate(60, 40);
					GameCanvas.drawSun2(g);
					g.translate(-60, -40);
					GameCanvas.paintBackgroundtLayer(g, 4, 7, -1, GameCanvas.colorBotton[3]);
					BackgroudEffect.paintFarAll(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 7)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 8)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					if (((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					}
				}
				else if (GameCanvas.typeBg == 9)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					g.translate(-80, 20);
					GameCanvas.drawSun2(g);
					g.translate(80, -20);
					BackgroudEffect.paintFarAll(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 10)
				{
					int num2 = GameScr.cmy - (380 - GameScr.gH23);
					g.translate(0, -num2);
					GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien) ? GameCanvas.colorTop[1] : GameScr.gI().mautroi, 0, num2 - (GameScr.cmy >> 2), gW, GameCanvas.yb[1] - num2 + (GameScr.cmy >> 2) + 100, 2);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, -1);
					g.translate(0, -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 11)
				{
					GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
					GameCanvas.drawSun1(g);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 12)
				{
					g.setColor(9161471);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 13)
				{
					g.setColor(15268088);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
				}
				else if (GameCanvas.typeBg == 15)
				{
					g.setColor(2631752);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 16)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					for (int i = 0; i < GameCanvas.imgSunSpec.Length; i++)
					{
						g.drawImage(GameCanvas.imgSunSpec[i], GameCanvas.cloudX[i], GameCanvas.cloudY[i], 33);
					}
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 19)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					GameCanvas.paintCloud(g);
				}
				else
				{
					GameCanvas.fillRect(g, GameCanvas.colorBotton[3], 0, GameCanvas.yb[3] + GameCanvas.bgH[3], GameScr.gW, GameCanvas.yb[2] + GameCanvas.bgH[2], 6);
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
			}
			else
			{
				g.setColor(2315859);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.tam != null)
				{
					for (int j = -((GameScr.cmx >> 2) % mGraphics.getImageWidth(GameCanvas.tam)); j < GameScr.gW; j += mGraphics.getImageWidth(GameCanvas.tam))
					{
						g.drawImage(GameCanvas.tam, j, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50, 0);
					}
				}
				g.setColor(5084791);
				g.fillRect(0, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50 + mGraphics.getImageHeight(GameCanvas.tam), gW, GameCanvas.h);
			}
		}
		catch (Exception)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00004887 File Offset: 0x00002A87
	public static void resetBg()
	{
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00036964 File Offset: 0x00034B64
	public static void getYBackground(int typeBg)
	{
		try
		{
			int gH = GameScr.gH23;
			switch (typeBg)
			{
			case 0:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_067A;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_067A;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_067A;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_067A;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_067A;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_067A;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_067A;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_067A;
			case 8:
				GameCanvas.yb[0] = gH - 103 + 150;
				if (TileMap.mapID == 103)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_067A;
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_067A;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_067A;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_067A;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_067A;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_067A;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_067A;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_067A;
			case 19:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_067A;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_067A:;
		}
		catch (Exception)
		{
			int gH2 = GameScr.gH23;
			for (int i = 0; i < GameCanvas.yb.Length; i++)
			{
				GameCanvas.yb[i] = 1;
			}
		}
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0003702C File Offset: 0x0003522C
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			if (GameCanvas.typeBg == 12)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			if ((TileMap.lastBgID != typeBG || TileMap.lastType != TileMap.bgType) && typeBG != -1)
			{
				GameCanvas.transY = 12;
				TileMap.lastBgID = (int)((sbyte)typeBG);
				TileMap.lastType = (int)((sbyte)TileMap.bgType);
				GameCanvas.layerSpeed = new int[] { 1, 2, 3, 7, 8 };
				GameCanvas.moveX = new int[5];
				GameCanvas.moveXSpeed = new int[5];
				GameCanvas.typeBg = typeBG;
				GameCanvas.isBoltEff = false;
				GameScr.firstY = GameScr.cmy;
				GameCanvas.imgBG = null;
				GameCanvas.imgCloud = null;
				GameCanvas.imgSun = null;
				GameCanvas.imgCaycot = null;
				GameScr.firstY = -1;
				switch (GameCanvas.typeBg)
				{
				case 0:
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[] { 1, 3, 5, 7 };
					GameCanvas.nBg = 4;
					if (TileMap.bgType == 2)
					{
						GameCanvas.transY = 8;
						goto IL_031E;
					}
					goto IL_031E;
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_031E;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_031E;
				case 4:
				{
					BackgroudEffect.addEffect(3);
					int[] array3 = new int[5];
					array3[1] = 1;
					GameCanvas.moveX = array3;
					int[] array4 = new int[5];
					array4[1] = 1;
					GameCanvas.moveXSpeed = array4;
					GameCanvas.nBg = 4;
					goto IL_031E;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_031E;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_031E;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_031E;
				case 12:
				{
					int[] array7 = new int[5];
					array7[0] = 1;
					array7[1] = 1;
					GameCanvas.moveX = array7;
					int[] array8 = new int[5];
					array8[0] = 2;
					array8[1] = 1;
					GameCanvas.moveXSpeed = array8;
					GameCanvas.nBg = 3;
					goto IL_031E;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_031E;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_031E;
				case 16:
					GameCanvas.layerSpeed = new int[] { 1, 3, 5, 7 };
					GameCanvas.nBg = 4;
					goto IL_031E;
				case 19:
				{
					int[] array9 = new int[5];
					array9[1] = 2;
					array9[2] = 1;
					GameCanvas.moveX = array9;
					int[] array10 = new int[5];
					array10[1] = 2;
					array10[2] = 1;
					GameCanvas.moveXSpeed = array10;
					GameCanvas.nBg = 5;
					goto IL_031E;
				}
				}
				GameCanvas.layerSpeed = new int[] { 1, 3, 5, 7 };
				GameCanvas.nBg = 4;
				IL_031E:
				if (typeBG <= 16)
				{
					GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
				}
				else
				{
					try
					{
						string text = "/bg/b" + GameCanvas.typeBg.ToString() + 3.ToString() + ".png";
						if (TileMap.bgType != 0)
						{
							text = string.Concat(new string[]
							{
								"/bg/b",
								GameCanvas.typeBg.ToString(),
								3.ToString(),
								"-",
								TileMap.bgType.ToString(),
								".png"
							});
						}
						int[] array11 = new int[1];
						Image image = GameCanvas.loadImageRMS(text);
						image.getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
						GameCanvas.skyColor = array11[0];
					}
					catch (Exception)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
					}
				}
				GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
				GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
				for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
				{
					GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
					GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
				}
				if (GameCanvas.lowGraphic)
				{
					GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
				}
				else
				{
					GameCanvas.imgBG = new Image[GameCanvas.nBg];
					GameCanvas.bgW = new int[GameCanvas.nBg];
					GameCanvas.bgH = new int[GameCanvas.nBg];
					GameCanvas.colorBotton = new int[GameCanvas.nBg];
					GameCanvas.colorTop = new int[GameCanvas.nBg];
					if (TileMap.bgType == 100)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							if (GameCanvas.imgBG[j] != null)
							{
								int[] array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
								GameCanvas.colorTop[j] = array12[0];
								array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
								GameCanvas.colorBotton[j] = array12[0];
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
						}
					}
					else
					{
						for (int k = 0; k < GameCanvas.nBg; k++)
						{
							string text2 = "/bg/b" + GameCanvas.typeBg.ToString() + k.ToString() + ".png";
							if (TileMap.bgType != 0)
							{
								text2 = string.Concat(new string[]
								{
									"/bg/b",
									GameCanvas.typeBg.ToString(),
									k.ToString(),
									"-",
									TileMap.bgType.ToString(),
									".png"
								});
							}
							GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(text2);
							if (GameCanvas.imgBG[k] != null)
							{
								int[] array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
								GameCanvas.colorTop[k] = array13[0];
								array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
								GameCanvas.colorBotton[k] = array13[0];
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
						}
					}
					GameCanvas.getYBackground(GameCanvas.typeBg);
					GameCanvas.cloudX = new int[]
					{
						GameScr.gW / 2 - 40,
						GameScr.gW / 2 + 40,
						GameScr.gW / 2 - 100,
						GameScr.gW / 2 - 80,
						GameScr.gW / 2 - 120
					};
					GameCanvas.cloudY = new int[] { 130, 100, 150, 140, 80 };
					GameCanvas.imgSunSpec = null;
					if (GameCanvas.typeBg != 0)
					{
						if (GameCanvas.typeBg == 2)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
						}
						else if (GameCanvas.typeBg == 19)
						{
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
						}
						else if (GameCanvas.typeBg == 4)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
							GameCanvas.sunX = GameScr.gW / 2 + 30;
							GameCanvas.sunY = GameCanvas.yb[3];
						}
						else if (GameCanvas.typeBg == 7)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[3] - 80;
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 6)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4];
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
						}
						else if (typeBG == 5)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 50;
							GameCanvas.sunY = GameCanvas.yb[3] + 20;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 8 && TileMap.mapID < 90)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[3] + 60;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
						}
						else if (typeBG == 9)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4] + 20;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
						}
						else if (typeBG == 10)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[1] - 30;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[1];
						}
						else if (typeBG == 11)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
						else if (typeBG == 12)
						{
							GameCanvas.cloudY = new int[] { 200, 170, 220, 150, 250 };
						}
						else if (typeBG == 16)
						{
							GameCanvas.cloudX = new int[] { 90, 170, 250, 320, 400, 450, 500 };
							GameCanvas.cloudY = new int[]
							{
								GameCanvas.yb[2] + 5,
								GameCanvas.yb[2] - 20,
								GameCanvas.yb[2] - 50,
								GameCanvas.yb[2] - 30,
								GameCanvas.yb[2] - 50,
								GameCanvas.yb[2],
								GameCanvas.yb[2] - 40
							};
							GameCanvas.imgSunSpec = new Image[7];
							for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
							{
								int num = 161;
								if (l == 0 || l == 2 || l == 3 || l == 2 || l == 6)
								{
									num = 160;
								}
								GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num.ToString() + ".png");
							}
						}
						else if (typeBG == 19)
						{
							int[] array14 = new int[5];
							array14[1] = 2;
							array14[2] = 1;
							GameCanvas.moveX = array14;
							int[] array15 = new int[5];
							array15[1] = 2;
							array15[2] = 1;
							GameCanvas.moveXSpeed = array15;
							GameCanvas.nBg = 5;
						}
						else
						{
							GameCanvas.imgCloud = null;
							GameCanvas.imgSun = null;
							GameCanvas.imgSun2 = null;
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun" + typeBG.ToString() + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
					}
					GameCanvas.paintBG = false;
					if (!GameCanvas.paintBG)
					{
						GameCanvas.paintBG = true;
					}
				}
			}
		}
		catch (Exception)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0003805C File Offset: 0x0003625C
	internal static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			if (typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0)
			{
				BackgroudEffect.addEffect(0);
				return;
			}
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00038095 File Offset: 0x00036295
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.keyAsciiPress = keyCode;
		this.mapKeyPress(keyCode);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000380B0 File Offset: 0x000362B0
	public void mapKeyPress(int keyCode)
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		GameCanvas.currentScreen.keyPress(keyCode);
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = true;
			GameCanvas.keyPressed[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = true;
				GameCanvas.keyPressed[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = true;
				GameCanvas.keyPressed[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = true;
				GameCanvas.keyPressed[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = true;
				GameCanvas.keyPressed[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = true;
				GameCanvas.keyPressed[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = true;
				GameCanvas.keyPressed[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = true;
			GameCanvas.keyPressed[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = true;
				GameCanvas.keyPressed[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = true;
			GameCanvas.keyPressed[9] = true;
			return;
		default:
			switch (keyCode)
			{
			case -8:
				GameCanvas.keyHold[14] = true;
				GameCanvas.keyPressed[14] = true;
				return;
			case -7:
				goto IL_0453;
			case -6:
				goto IL_0440;
			case -5:
				goto IL_03EC;
			case -4:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[24] = true;
				GameCanvas.keyPressed[24] = true;
				return;
			case -3:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[23] = true;
				GameCanvas.keyPressed[23] = true;
				return;
			case -2:
				goto IL_0326;
			case -1:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_0326;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_0453;
					}
					if (keyCode == -21)
					{
						goto IL_0440;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_03EC;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					}
					if (keyCode == 113)
					{
						GameCanvas.keyHold[17] = true;
						GameCanvas.keyPressed[17] = true;
						return;
					}
					return;
				}
				break;
			}
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[21] = true;
			GameCanvas.keyPressed[21] = true;
			return;
			IL_0326:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[22] = true;
			GameCanvas.keyPressed[22] = true;
			return;
			IL_03EC:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[25] = true;
			GameCanvas.keyPressed[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_0440:
			GameCanvas.keyHold[12] = true;
			GameCanvas.keyPressed[12] = true;
			return;
			IL_0453:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
			return;
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00038535 File Offset: 0x00036735
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00038544 File Offset: 0x00036744
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = false;
			GameCanvas.keyReleased[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyReleased[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = false;
				GameCanvas.keyReleased[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyReleased[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = false;
				GameCanvas.keyReleased[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = false;
				GameCanvas.keyReleased[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = false;
				GameCanvas.keyReleased[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = false;
			GameCanvas.keyReleased[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = false;
				GameCanvas.keyReleased[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = false;
			GameCanvas.keyReleased[9] = true;
			return;
		default:
			switch (keyCode)
			{
			case -8:
				GameCanvas.keyHold[14] = false;
				return;
			case -7:
				goto IL_0311;
			case -6:
				goto IL_02FE;
			case -5:
				goto IL_02D9;
			case -4:
				GameCanvas.keyHold[24] = false;
				return;
			case -3:
				GameCanvas.keyHold[23] = false;
				return;
			case -2:
				goto IL_02BB;
			case -1:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_02BB;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_0311;
					}
					if (keyCode == -21)
					{
						goto IL_02FE;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = false;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_02D9;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					}
					if (keyCode == 113)
					{
						GameCanvas.keyHold[17] = false;
						GameCanvas.keyReleased[17] = true;
						return;
					}
					return;
				}
				break;
			}
			GameCanvas.keyHold[21] = false;
			return;
			IL_02BB:
			GameCanvas.keyHold[22] = false;
			return;
			IL_02D9:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_02FE:
			GameCanvas.keyHold[12] = false;
			GameCanvas.keyReleased[12] = true;
			return;
			IL_0311:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
			return;
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0003887E File Offset: 0x00036A7E
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0003888C File Offset: 0x00036A8C
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		if (GameCanvas.panel != null && GameCanvas.panel.isShow)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
		if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
		{
			GameCanvas.panel2.updateScroolMouse(a);
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x000388DC File Offset: 0x00036ADC
	public void pointerDragged(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		if (Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		if (GameCanvas.curPos > 3)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00038959 File Offset: 0x00036B59
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00038974 File Offset: 0x00036B74
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerMove = false;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x000389D3 File Offset: 0x00036BD3
	public void pointerReleased(int x, int y)
	{
		if (!GameCanvas.isPointerMove)
		{
			GameCanvas.isPointerSelect = true;
		}
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerMove = false;
		GameCanvas.isPointerClick = true;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00038A0C File Offset: 0x00036C0C
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00038A45 File Offset: 0x00036C45
	public static bool isPointSelect(int x, int y, int w, int h)
	{
		return GameCanvas.isPointerSelect && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00038A77 File Offset: 0x00036C77
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00038AA0 File Offset: 0x00036CA0
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00038AD0 File Offset: 0x00036CD0
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00038AF8 File Offset: 0x00036CF8
	public static void checkBackButton()
	{
		if (ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00038B48 File Offset: 0x00036D48
	public void paintChangeMap(mGraphics g)
	{
		string empty = string.Empty;
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin.ToString() + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00038BF8 File Offset: 0x00036DF8
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			if (GameCanvas.currentScreen != null)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (GameCanvas.panel.isShow)
			{
				GameCanvas.panel.paint(this.g);
				if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.paint(this.g);
				}
				if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else if (GameCanvas.menu.showMenu)
			{
				GameCanvas.debug("PD", 1);
				GameCanvas.resetTrans(this.g);
				GameCanvas.menu.paintMenu(this.g);
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			if (GameScr.gI().popUpYesNo != null)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			if (ChatPopup.currChatPopup != null)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			if (ChatPopup.serverChatPopUp != null)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
				{
					effect.paint(this.g);
				}
			}
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.currentDialog.paint(this.g);
			}
			if (global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait)
			{
				this.paintChangeMap(this.g);
				if (GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0)
				{
					GameCanvas.startWaitDlg();
					if (mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L)
					{
						GameCanvas.timeLoading--;
						Res.outz("[COUNT] == " + GameCanvas.timeLoading.ToString());
						if (GameCanvas.timeLoading == 0)
						{
							GameCanvas.timeLoading = 15;
						}
						GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
					}
				}
				if (mSystem.currentTimeMillis() > GameCanvas.timeBreakLoading)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					if (GameCanvas.currentScreen != null && !(GameCanvas.currentScreen is GameScr) && !(GameCanvas.currentScreen is SplashScr))
					{
						LoginScr loginScr = GameCanvas.currentScreen as LoginScr;
					}
				}
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			if (GameCanvas.open3Hour && !GameCanvas.isLoading)
			{
				if (GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				if (GameCanvas.currentScreen == CreateCharScr.instance)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
			GameCanvas.resetTrans(this.g);
			int num = GameCanvas.h / 4;
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null)
			{
				this.g.setClip(60, num, GameCanvas.w - 120, mFont.tahoma_7_white.getHeight() + 2);
				mFont.tahoma_7_grey.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num + 1, 0);
				mFont.tahoma_7_yellow.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num, 0);
				this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0003909C File Offset: 0x0003729C
	public static void endDlg()
	{
		if (GameCanvas.inputDlg != null)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x06000318 RID: 792 RVA: 0x000390C4 File Offset: 0x000372C4
	public static void startOKDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x000390F7 File Offset: 0x000372F7
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x000390F7 File Offset: 0x000372F7
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00039135 File Offset: 0x00037335
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00039142 File Offset: 0x00037342
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00039175 File Offset: 0x00037375
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x000391A4 File Offset: 0x000373A4
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000391FF File Offset: 0x000373FF
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0003921E File Offset: 0x0003741E
	public static void startserverThongBao(string msgSv)
	{
		GameCanvas.thongBaoTest = msgSv;
		GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
		GameCanvas.dir_ = -1;
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0003923C File Offset: 0x0003743C
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m < 1000)
			{
				text = m.ToString() + text;
				break;
			}
			int num2 = m % 1000;
			text = ((num2 != 0) ? ((num2 >= 10) ? ((num2 >= 100) ? ("." + num2.ToString() + text) : (".0" + num2.ToString() + text)) : (".00" + num2.ToString() + text)) : (".000" + text));
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x000392EC File Offset: 0x000374EC
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x06000323 RID: 803 RVA: 0x000392F7 File Offset: 0x000374F7
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00004887 File Offset: 0x00002A87
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00039302 File Offset: 0x00037502
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00039308 File Offset: 0x00037508
	public static Image loadImageRMS(string path)
	{
		path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
		path = GameCanvas.cutPng(path);
		Image image = null;
		try
		{
			image = Image.createImage(path);
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				sbyte[] array2 = Rms.loadRMS("x" + mGraphics.zoomLevel.ToString() + array[array.Length - 1]);
				if (array2 != null)
				{
					image = Image.createImage(array2, 0, array2.Length);
				}
			}
			catch (Exception)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
			}
		}
		return image;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x000393BC File Offset: 0x000375BC
	public static Image loadImage(string path)
	{
		path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
		path = GameCanvas.cutPng(path);
		Image image = null;
		try
		{
			image = Image.createImage(path);
		}
		catch (Exception)
		{
		}
		return image;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0003940C File Offset: 0x0003760C
	public static string cutPng(string str)
	{
		string text = str;
		if (str.Contains(".png"))
		{
			text = str.Replace(".png", string.Empty);
		}
		return text;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0003943A File Offset: 0x0003763A
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0003944C File Offset: 0x0003764C
	public bool startDust(int dir, int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = ((dir != 1) ? 1 : 0);
		if (this.dustState[num] != -1)
		{
			return false;
		}
		this.dustState[num] = 0;
		this.dustX[num] = x;
		this.dustY[num] = y;
		return true;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00039494 File Offset: 0x00037694
	public void loadWaterSplash()
	{
		if (!GameCanvas.lowGraphic)
		{
			GameCanvas.imgWS = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i.ToString() + ".png");
			}
			GameCanvas.wsX = new int[2];
			GameCanvas.wsY = new int[2];
			GameCanvas.wsState = new int[2];
			GameCanvas.wsF = new int[2];
			GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00039520 File Offset: 0x00037720
	public bool startWaterSplash(int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = ((GameCanvas.wsState[0] != -1) ? 1 : 0);
		if (GameCanvas.wsState[num] != -1)
		{
			return false;
		}
		GameCanvas.wsState[num] = 0;
		GameCanvas.wsX[num] = x;
		GameCanvas.wsY[num] = y;
		return true;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0003956C File Offset: 0x0003776C
	public void updateWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (GameCanvas.wsState[i] != -1)
			{
				GameCanvas.wsY[i]--;
				if (GameCanvas.gameTick % 2 == 0)
				{
					GameCanvas.wsState[i]++;
					if (GameCanvas.wsState[i] > 2)
					{
						GameCanvas.wsState[i] = -1;
					}
					else
					{
						GameCanvas.wsF[i] = GameCanvas.wsState[i];
					}
				}
			}
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000395E4 File Offset: 0x000377E4
	public void updateDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1)
			{
				this.dustState[i]++;
				if (this.dustState[i] >= 5)
				{
					this.dustState[i] = -1;
				}
				if (i == 0)
				{
					this.dustX[i]--;
				}
				else
				{
					this.dustX[i]++;
				}
				this.dustY[i]--;
			}
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0003966D File Offset: 0x0003786D
	public static bool isPaint(int x, int y)
	{
		return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000396A8 File Offset: 0x000378A8
	public void paintDust(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]))
			{
				g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
			}
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00039714 File Offset: 0x00037914
	public void loadDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (GameCanvas.imgDust == null)
		{
			GameCanvas.imgDust = new Image[2][];
			for (int i = 0; i < GameCanvas.imgDust.Length; i++)
			{
				GameCanvas.imgDust[i] = new Image[5];
			}
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					GameCanvas.imgDust[j][k] = GameCanvas.loadImage("/e/d" + j.ToString() + k.ToString() + ".png");
				}
			}
		}
		this.dustX = new int[2];
		this.dustY = new int[2];
		this.dustState = new int[2];
		this.dustState[0] = (this.dustState[1] = -1);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000397D8 File Offset: 0x000379D8
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0003980B File Offset: 0x00037A0B
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00038A0C File Offset: 0x00036C0C
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00039814 File Offset: 0x00037A14
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 88810:
		{
			int num = (int)p;
			GameCanvas.endDlg();
			Service.gI().acceptInviteTrade(num);
			return;
		}
		case 88811:
			GameCanvas.endDlg();
			Service.gI().cancelInviteTrade();
			return;
		case 88812:
		case 88813:
		case 88816:
		case 88830:
		case 88831:
		case 88832:
		case 88833:
		case 88834:
		case 88835:
		case 88838:
			goto IL_03E4;
		case 88814:
		{
			Item[] array = (Item[])p;
			GameCanvas.endDlg();
			Service.gI().crystalCollectLock(array);
			return;
		}
		case 88815:
			return;
		case 88817:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88818:
		{
			short num2 = (short)p;
			Service.gI().textBoxId(num2, GameCanvas.inputDlg.tfInput.getText());
			GameCanvas.endDlg();
			return;
		}
		case 88819:
		{
			short num3 = (short)p;
			Service.gI().menuId(num3);
			return;
		}
		case 88820:
		{
			string[] array2 = (string[])p;
			if (global::Char.myCharz().npcFocus == null)
			{
				return;
			}
			int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
			if (array2.Length > 1)
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < array2.Length - 1; i++)
				{
					myVector.addElement(new Command(array2[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
				}
				GameCanvas.menu.startAt(myVector, 3);
				return;
			}
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
			return;
		}
		case 88821:
		{
			int num4 = (int)p;
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, num4, GameCanvas.menu.menuSelectedItem);
			return;
		}
		case 88822:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88823:
			GameCanvas.startOKDlg(mResources.SENTMSG);
			return;
		case 88824:
			GameCanvas.startOKDlg(mResources.NOSENDMSG);
			return;
		case 88825:
			GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
			return;
		case 88826:
			GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
			return;
		case 88827:
			GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
			return;
		case 88828:
			GameCanvas.startOKDlg(mResources.sendMsgFail);
			return;
		case 88829:
		{
			string text = GameCanvas.inputDlg.tfInput.getText();
			if (!text.Equals(string.Empty))
			{
				Service.gI().changeName(text, (int)p);
				InfoDlg.showWait();
			}
			return;
		}
		case 88836:
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
			GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
			return;
		case 88837:
		{
			string text2 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			try
			{
				Service.gI().openLockAccProtect(int.Parse(text2.Trim()));
				return;
			}
			catch (Exception ex)
			{
				Cout.println("Loi tai 88837 " + ex.ToString());
				return;
			}
			break;
		}
		case 88839:
			break;
		default:
			goto IL_03E4;
		}
		string text3 = GameCanvas.inputDlg.tfInput.getText();
		GameCanvas.endDlg();
		if (text3.Length < 6 || text3.Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			return;
		}
		try
		{
			GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text3, 8882, null);
			return;
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
			return;
		}
		IL_03E4:
		switch (idAction)
		{
		case 8881:
		{
			string text4 = (string)p;
			try
			{
				GameMidlet.instance.platformRequest(text4);
			}
			catch (Exception)
			{
			}
			GameCanvas.currentDialog = null;
			return;
		}
		case 8882:
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			return;
		case 8884:
			GameCanvas.endDlg();
			GameCanvas.loginScr.switchToMe();
			return;
		case 8885:
			GameMidlet.instance.exit();
			return;
		case 8886:
		{
			GameCanvas.endDlg();
			string text5 = (string)p;
			Service.gI().addFriend(text5);
			return;
		}
		case 8887:
		{
			GameCanvas.endDlg();
			int num5 = (int)p;
			Service.gI().addPartyAccept(num5);
			return;
		}
		case 8888:
		{
			int num6 = (int)p;
			Service.gI().addPartyCancel(num6);
			GameCanvas.endDlg();
			return;
		}
		case 8889:
		{
			string text6 = (string)p;
			GameCanvas.endDlg();
			Service.gI().acceptPleaseParty(text6);
			return;
		}
		}
		switch (idAction)
		{
		case 888391:
		{
			string text7 = (string)p;
			GameCanvas.endDlg();
			Service.gI().clearAccProtect(int.Parse(text7));
			return;
		}
		case 888392:
			Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 888393:
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.doLogin();
			Main.closeKeyBoard();
			return;
		case 888394:
			GameCanvas.endDlg();
			return;
		case 888395:
			GameCanvas.endDlg();
			return;
		case 888396:
			GameCanvas.endDlg();
			return;
		case 888397:
		{
			string text8 = (string)p;
			return;
		}
		default:
			switch (idAction)
			{
			case 100001:
				Service.gI().getFlag(0, -1);
				InfoDlg.showWait();
				return;
			case 100002:
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.backToRegister();
				return;
			case 100003:
				return;
			case 100004:
				return;
			case 100005:
				if (global::Char.myCharz().statusMe == 14)
				{
					GameCanvas.startOKDlg(mResources.can_not_do_when_die);
					return;
				}
				Service.gI().openUIZone();
				return;
			case 100006:
				mSystem.onDisconnected();
				return;
			default:
				switch (idAction)
				{
				case 101023:
					Main.numberQuit = 0;
					return;
				case 101024:
					Res.outz("output 101024");
					GameCanvas.endDlg();
					return;
				case 101025:
					GameCanvas.endDlg();
					if (ServerListScreen.loadScreen)
					{
						GameCanvas.serverScreen.switchToMe();
						return;
					}
					GameCanvas.serverScreen.show2();
					return;
				case 101026:
					mSystem.onDisconnected();
					return;
				default:
					if (idAction != 999)
					{
						if (idAction != 9000)
						{
							if (idAction == 9999)
							{
								GameCanvas.endDlg();
								GameCanvas.connect();
								Service.gI().setClientType();
								if (GameCanvas.loginScr == null)
								{
									GameCanvas.loginScr = new LoginScr();
								}
								GameCanvas.loginScr.doLogin();
								return;
							}
							if (idAction == 100016)
							{
								ServerListScreen.ipSelect = 17;
								GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
								ServerListScreen.waitToLogin = true;
								GameCanvas.endDlg();
								return;
							}
							if (idAction != 888361)
							{
								return;
							}
							string text9 = GameCanvas.inputDlg.tfInput.getText();
							GameCanvas.endDlg();
							if (text9.Length >= 6 && !text9.Equals(string.Empty))
							{
								try
								{
									Service.gI().activeAccProtect(int.Parse(text9));
									break;
								}
								catch (Exception ex2)
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
									Cout.println("Loi tai 888361 Gamescavas " + ex2.ToString());
									break;
								}
							}
							GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
							return;
						}
						else
						{
							GameCanvas.endDlg();
							SplashScr.imgLogo = null;
							SmallImage.loadBigRMS();
							mSystem.gcc();
							ServerListScreen.bigOk = true;
							ServerListScreen.loadScreen = true;
							GameScr.gI().loadGameScr();
							if (GameCanvas.currentScreen != GameCanvas.loginScr)
							{
								GameCanvas.serverScreen.switchToMe2();
								return;
							}
						}
					}
					else
					{
						mSystem.closeBanner();
						GameCanvas.endDlg();
					}
					break;
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00039FE8 File Offset: 0x000381E8
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerSelect = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00004887 File Offset: 0x00002A87
	public static void backToRegister()
	{
	}

	// Token: 0x04000610 RID: 1552
	public static long timeNow = 0L;

	// Token: 0x04000611 RID: 1553
	public static bool open3Hour;

	// Token: 0x04000612 RID: 1554
	public static bool lowGraphic = false;

	// Token: 0x04000613 RID: 1555
	public static bool serverchat = false;

	// Token: 0x04000614 RID: 1556
	public static bool isMoveNumberPad = true;

	// Token: 0x04000615 RID: 1557
	public static bool isLoading;

	// Token: 0x04000616 RID: 1558
	public static bool isTouch = false;

	// Token: 0x04000617 RID: 1559
	public static bool isTouchControl;

	// Token: 0x04000618 RID: 1560
	public static bool isTouchControlSmallScreen;

	// Token: 0x04000619 RID: 1561
	public static bool isTouchControlLargeScreen;

	// Token: 0x0400061A RID: 1562
	public static bool isConnectFail;

	// Token: 0x0400061B RID: 1563
	public static GameCanvas instance;

	// Token: 0x0400061C RID: 1564
	public static bool bRun;

	// Token: 0x0400061D RID: 1565
	public static bool[] keyPressed = new bool[30];

	// Token: 0x0400061E RID: 1566
	public static bool[] keyReleased = new bool[30];

	// Token: 0x0400061F RID: 1567
	public static bool[] keyHold = new bool[30];

	// Token: 0x04000620 RID: 1568
	public static bool isPointerDown;

	// Token: 0x04000621 RID: 1569
	public static bool isPointerClick;

	// Token: 0x04000622 RID: 1570
	public static bool isPointerJustRelease;

	// Token: 0x04000623 RID: 1571
	public static bool isPointerSelect;

	// Token: 0x04000624 RID: 1572
	public static bool isPointerMove;

	// Token: 0x04000625 RID: 1573
	public static int px;

	// Token: 0x04000626 RID: 1574
	public static int py;

	// Token: 0x04000627 RID: 1575
	public static int pxFirst;

	// Token: 0x04000628 RID: 1576
	public static int pyFirst;

	// Token: 0x04000629 RID: 1577
	public static int pxLast;

	// Token: 0x0400062A RID: 1578
	public static int pyLast;

	// Token: 0x0400062B RID: 1579
	public static int pxMouse;

	// Token: 0x0400062C RID: 1580
	public static int pyMouse;

	// Token: 0x0400062D RID: 1581
	public static Position[] arrPos = new Position[4];

	// Token: 0x0400062E RID: 1582
	public static int gameTick;

	// Token: 0x0400062F RID: 1583
	public static int taskTick;

	// Token: 0x04000630 RID: 1584
	public static bool isEff1;

	// Token: 0x04000631 RID: 1585
	public static bool isEff2;

	// Token: 0x04000632 RID: 1586
	public static long timeTickEff1;

	// Token: 0x04000633 RID: 1587
	public static long timeTickEff2;

	// Token: 0x04000634 RID: 1588
	public static int w;

	// Token: 0x04000635 RID: 1589
	public static int h;

	// Token: 0x04000636 RID: 1590
	public static int hw;

	// Token: 0x04000637 RID: 1591
	public static int hh;

	// Token: 0x04000638 RID: 1592
	public static int wd3;

	// Token: 0x04000639 RID: 1593
	public static int hd3;

	// Token: 0x0400063A RID: 1594
	public static int w2d3;

	// Token: 0x0400063B RID: 1595
	public static int h2d3;

	// Token: 0x0400063C RID: 1596
	public static int w3d4;

	// Token: 0x0400063D RID: 1597
	public static int h3d4;

	// Token: 0x0400063E RID: 1598
	public static int wd6;

	// Token: 0x0400063F RID: 1599
	public static int hd6;

	// Token: 0x04000640 RID: 1600
	public static mScreen currentScreen;

	// Token: 0x04000641 RID: 1601
	public static Menu menu = new Menu();

	// Token: 0x04000642 RID: 1602
	public static Panel panel;

	// Token: 0x04000643 RID: 1603
	public static Panel panel2;

	// Token: 0x04000644 RID: 1604
	public static ChooseCharScr chooseCharScr;

	// Token: 0x04000645 RID: 1605
	public static LoginScr loginScr;

	// Token: 0x04000646 RID: 1606
	public static RegisterScreen registerScr;

	// Token: 0x04000647 RID: 1607
	public static Dialog currentDialog;

	// Token: 0x04000648 RID: 1608
	public static MsgDlg msgdlg;

	// Token: 0x04000649 RID: 1609
	public static InputDlg inputDlg;

	// Token: 0x0400064A RID: 1610
	public static MyVector currentPopup = new MyVector();

	// Token: 0x0400064B RID: 1611
	public static int requestLoseCount;

	// Token: 0x0400064C RID: 1612
	public static MyVector listPoint;

	// Token: 0x0400064D RID: 1613
	public static Paint paintz;

	// Token: 0x0400064E RID: 1614
	public static bool isGetResFromServer;

	// Token: 0x0400064F RID: 1615
	public static Image[] imgBG;

	// Token: 0x04000650 RID: 1616
	public static int skyColor;

	// Token: 0x04000651 RID: 1617
	public static int curPos = 0;

	// Token: 0x04000652 RID: 1618
	public static int[] bgW;

	// Token: 0x04000653 RID: 1619
	public static int[] bgH;

	// Token: 0x04000654 RID: 1620
	public static int planet = 0;

	// Token: 0x04000655 RID: 1621
	internal mGraphics g = new mGraphics();

	// Token: 0x04000656 RID: 1622
	public static Image img12;

	// Token: 0x04000657 RID: 1623
	public static Image[] imgBlue = new Image[7];

	// Token: 0x04000658 RID: 1624
	public static Image[] imgViolet = new Image[7];

	// Token: 0x04000659 RID: 1625
	public static MyHashTable danhHieu = new MyHashTable();

	// Token: 0x0400065A RID: 1626
	public static MyVector messageServer = new MyVector(string.Empty);

	// Token: 0x0400065B RID: 1627
	public static bool isPlaySound = true;

	// Token: 0x0400065C RID: 1628
	internal static int clearOldData;

	// Token: 0x0400065D RID: 1629
	public static int timeOpenKeyBoard;

	// Token: 0x0400065E RID: 1630
	public static bool isFocusPanel2;

	// Token: 0x0400065F RID: 1631
	public static int fps = 0;

	// Token: 0x04000660 RID: 1632
	public static int max;

	// Token: 0x04000661 RID: 1633
	public static int up;

	// Token: 0x04000662 RID: 1634
	public static int upmax;

	// Token: 0x04000663 RID: 1635
	internal long timefps = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x04000664 RID: 1636
	internal long timeup = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x04000665 RID: 1637
	public static int isRequestMapID = -1;

	// Token: 0x04000666 RID: 1638
	public static long waitingTimeChangeMap;

	// Token: 0x04000667 RID: 1639
	internal static int dir_ = -1;

	// Token: 0x04000668 RID: 1640
	internal int tickWaitThongBao;

	// Token: 0x04000669 RID: 1641
	public bool isPaintCarret;

	// Token: 0x0400066A RID: 1642
	public static MyVector debugUpdate;

	// Token: 0x0400066B RID: 1643
	public static MyVector debugPaint;

	// Token: 0x0400066C RID: 1644
	public static MyVector debugSession;

	// Token: 0x0400066D RID: 1645
	internal static bool isShowErrorForm = false;

	// Token: 0x0400066E RID: 1646
	public static bool paintBG;

	// Token: 0x0400066F RID: 1647
	public static int gsskyHeight;

	// Token: 0x04000670 RID: 1648
	public static int gsgreenField1Y;

	// Token: 0x04000671 RID: 1649
	public static int gsgreenField2Y;

	// Token: 0x04000672 RID: 1650
	public static int gshouseY;

	// Token: 0x04000673 RID: 1651
	public static int gsmountainY;

	// Token: 0x04000674 RID: 1652
	public static int bgLayer0y;

	// Token: 0x04000675 RID: 1653
	public static int bgLayer1y;

	// Token: 0x04000676 RID: 1654
	public static Image imgCloud;

	// Token: 0x04000677 RID: 1655
	public static Image imgSun;

	// Token: 0x04000678 RID: 1656
	public static Image imgSun2;

	// Token: 0x04000679 RID: 1657
	public static Image imgClear;

	// Token: 0x0400067A RID: 1658
	public static Image[] imgBorder = new Image[3];

	// Token: 0x0400067B RID: 1659
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x0400067C RID: 1660
	public static int borderConnerW;

	// Token: 0x0400067D RID: 1661
	public static int borderConnerH;

	// Token: 0x0400067E RID: 1662
	public static int borderCenterW;

	// Token: 0x0400067F RID: 1663
	public static int borderCenterH;

	// Token: 0x04000680 RID: 1664
	public static int[] cloudX;

	// Token: 0x04000681 RID: 1665
	public static int[] cloudY;

	// Token: 0x04000682 RID: 1666
	public static int sunX;

	// Token: 0x04000683 RID: 1667
	public static int sunY;

	// Token: 0x04000684 RID: 1668
	public static int sunX2;

	// Token: 0x04000685 RID: 1669
	public static int sunY2;

	// Token: 0x04000686 RID: 1670
	public static int[] layerSpeed;

	// Token: 0x04000687 RID: 1671
	public static int[] moveX;

	// Token: 0x04000688 RID: 1672
	public static int[] moveXSpeed;

	// Token: 0x04000689 RID: 1673
	public static bool isBoltEff;

	// Token: 0x0400068A RID: 1674
	public static bool boltActive;

	// Token: 0x0400068B RID: 1675
	public static int tBolt;

	// Token: 0x0400068C RID: 1676
	public static Image imgBgIOS;

	// Token: 0x0400068D RID: 1677
	public static int typeBg = -1;

	// Token: 0x0400068E RID: 1678
	public static int transY;

	// Token: 0x0400068F RID: 1679
	public static int[] yb = new int[5];

	// Token: 0x04000690 RID: 1680
	public static int[] colorTop;

	// Token: 0x04000691 RID: 1681
	public static int[] colorBotton;

	// Token: 0x04000692 RID: 1682
	public static int yb1;

	// Token: 0x04000693 RID: 1683
	public static int yb2;

	// Token: 0x04000694 RID: 1684
	public static int yb3;

	// Token: 0x04000695 RID: 1685
	public static int nBg = 0;

	// Token: 0x04000696 RID: 1686
	public static int lastBg = -1;

	// Token: 0x04000697 RID: 1687
	public static int[] bgRain = new int[] { 1, 4, 11 };

	// Token: 0x04000698 RID: 1688
	public static int[] bgRainFont = new int[] { -1 };

	// Token: 0x04000699 RID: 1689
	public static Image imgCaycot;

	// Token: 0x0400069A RID: 1690
	public static Image tam;

	// Token: 0x0400069B RID: 1691
	public static int typeBackGround = -1;

	// Token: 0x0400069C RID: 1692
	public static int saveIDBg = -10;

	// Token: 0x0400069D RID: 1693
	public static bool isLoadBGok;

	// Token: 0x0400069E RID: 1694
	internal static long lastTimePress = 0L;

	// Token: 0x0400069F RID: 1695
	public static int keyAsciiPress;

	// Token: 0x040006A0 RID: 1696
	public static int pXYScrollMouse;

	// Token: 0x040006A1 RID: 1697
	internal static Image imgSignal;

	// Token: 0x040006A2 RID: 1698
	public static MyVector flyTexts = new MyVector();

	// Token: 0x040006A3 RID: 1699
	public int longTime;

	// Token: 0x040006A4 RID: 1700
	public static long timeBreakLoading;

	// Token: 0x040006A5 RID: 1701
	internal static string thongBaoTest;

	// Token: 0x040006A6 RID: 1702
	public static int xThongBaoTranslate = GameCanvas.w - 60;

	// Token: 0x040006A7 RID: 1703
	public static bool isPointerJustDown = false;

	// Token: 0x040006A8 RID: 1704
	internal int count = 1;

	// Token: 0x040006A9 RID: 1705
	public static bool csWait;

	// Token: 0x040006AA RID: 1706
	public static MyRandom r = new MyRandom();

	// Token: 0x040006AB RID: 1707
	public static bool isBlackScreen;

	// Token: 0x040006AC RID: 1708
	public static int[] bgSpeed;

	// Token: 0x040006AD RID: 1709
	public static int cmdBarX;

	// Token: 0x040006AE RID: 1710
	public static int cmdBarY;

	// Token: 0x040006AF RID: 1711
	public static int cmdBarW;

	// Token: 0x040006B0 RID: 1712
	public static int cmdBarH;

	// Token: 0x040006B1 RID: 1713
	public static int cmdBarLeftW;

	// Token: 0x040006B2 RID: 1714
	public static int cmdBarRightW;

	// Token: 0x040006B3 RID: 1715
	public static int cmdBarCenterW;

	// Token: 0x040006B4 RID: 1716
	public static int hpBarX;

	// Token: 0x040006B5 RID: 1717
	public static int hpBarY;

	// Token: 0x040006B6 RID: 1718
	public static int hpBarW;

	// Token: 0x040006B7 RID: 1719
	public static int expBarW;

	// Token: 0x040006B8 RID: 1720
	public static int lvPosX;

	// Token: 0x040006B9 RID: 1721
	public static int moneyPosX;

	// Token: 0x040006BA RID: 1722
	public static int hpBarH;

	// Token: 0x040006BB RID: 1723
	public static int girlHPBarY;

	// Token: 0x040006BC RID: 1724
	public int timeOut;

	// Token: 0x040006BD RID: 1725
	public int[] dustX;

	// Token: 0x040006BE RID: 1726
	public int[] dustY;

	// Token: 0x040006BF RID: 1727
	public int[] dustState;

	// Token: 0x040006C0 RID: 1728
	public static int[] wsX;

	// Token: 0x040006C1 RID: 1729
	public static int[] wsY;

	// Token: 0x040006C2 RID: 1730
	public static int[] wsState;

	// Token: 0x040006C3 RID: 1731
	public static int[] wsF;

	// Token: 0x040006C4 RID: 1732
	public static Image[] imgWS;

	// Token: 0x040006C5 RID: 1733
	public static Image imgShuriken;

	// Token: 0x040006C6 RID: 1734
	public static Image[][] imgDust;

	// Token: 0x040006C7 RID: 1735
	public static bool isResume;

	// Token: 0x040006C8 RID: 1736
	public static ServerListScreen serverScreen;

	// Token: 0x040006C9 RID: 1737
	public static ServerScr serverScr;

	// Token: 0x040006CA RID: 1738
	public bool resetToLoginScr;

	// Token: 0x040006CB RID: 1739
	public static long TIMEOUT;

	// Token: 0x040006CC RID: 1740
	public static int timeLoading = 15;
}
