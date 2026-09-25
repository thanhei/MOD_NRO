using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x0004F830 File Offset: 0x0004DA30
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		if (GameCanvas.h > 200)
		{
			this.defYL = GameCanvas.hh - 80;
		}
		else
		{
			this.defYL = GameCanvas.hh - 65;
		}
		this.resetLogo();
		this.wC = ((GameCanvas.w < 200) ? 140 : 160);
		this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
		if (GameCanvas.h <= 160)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num = Rms.loadRMSInt("check");
		if (num == 1)
		{
			this.isCheck = true;
		}
		else if (num == 2)
		{
			this.isCheck = false;
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
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
		this.focus = 0;
		this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
		this.cmdCheck = new Command(mResources.remember, this, 2001, null);
		this.cmdRes = new Command(mResources.register, this, 2002, null);
		this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
		this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
		this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
		if (GameCanvas.isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			if (GameCanvas.h >= 200)
			{
				this.cmdLogin.y = this.yLog + 110;
				this.cmdMenu.y = this.yLog + 110;
			}
			this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
			this.cmdBackFromRegister.y = this.yLog + 110;
			this.cmdRes.x = GameCanvas.w / 2 - 84;
			this.cmdRes.y = this.cmdMenu.y;
		}
		this.wP = 170;
		this.hP = ((!this.isRes) ? 100 : 110);
		this.xP = GameCanvas.hw - this.wP / 2;
		this.yP = this.tfUser.y - 15;
		int num2 = 4;
		int num3 = num2 * 32 + 23 + 33;
		if (num3 >= GameCanvas.w)
		{
			num3 = (num2 - 1) * 32 + 23 + 33;
		}
		this.xLog = GameCanvas.w / 2 - num3 / 2;
		this.yLog = GameCanvas.hh - 30;
		this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
		this.tfUser.x = this.xLog + 10;
		this.tfUser.y = this.yLog + 20;
		this.cmdOK = new Command(mResources.OK, this, 2008, null);
		this.cmdOK.x = GameCanvas.w / 2 - 84;
		this.cmdOK.y = this.cmdLogin.y;
		this.cmdFogetPass = new Command(mResources.forgetPass, this, 1003, null);
		this.cmdFogetPass.x = GameCanvas.w / 2 + 3;
		this.cmdFogetPass.y = this.cmdLogin.y;
		this.center = this.cmdOK;
		this.left = this.cmdFogetPass;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
	public static void getServerLink()
	{
		try
		{
			if (!LoginScr.isTryGetIPFromWap)
			{
				Command command = new Command();
				command.actionChat = delegate(string str)
				{
					try
					{
						if (str != null && !(str == string.Empty))
						{
							Rms.saveIP(str);
							if (str.Contains(":"))
							{
								int num = str.IndexOf(":");
								string text = str.Substring(0, num);
								string text2 = str.Substring(num + 1);
								GameMidlet.IP = text;
								GameMidlet.PORT = int.Parse(text2);
								Session_ME.gI().connect(text, int.Parse(text2));
								LoginScr.isTryGetIPFromWap = true;
							}
						}
					}
					catch (Exception)
					{
					}
				};
				Net.connectHTTP(ServerListScreen.linkGetHost, command);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0004FE5C File Offset: 0x0004E05C
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		if (GameCanvas.isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		base.switchToMe();
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		if (text != null && !text.Equals(string.Empty))
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		if (text2 != null && !text2.Equals(string.Empty))
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0004FF10 File Offset: 0x0004E110
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		if (!this.isLogin2)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		if (Main.isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0004FFB8 File Offset: 0x0004E1B8
	protected void doRegister()
	{
		if (this.tfUser.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.userBlank);
			return;
		}
		this.tfUser.getText().ToCharArray();
		if (this.tfPass.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.passwordBlank);
			return;
		}
		if (this.tfUser.getText().Length < 5)
		{
			GameCanvas.startOKDlg(mResources.accTooShort);
			return;
		}
		int num = 0;
		string text = null;
		if (mResources.language == 2)
		{
			if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
			{
				text = mResources.emailInvalid;
			}
			num = 0;
		}
		else
		{
			try
			{
				long.Parse(this.tfUser.getText());
				if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
				{
					text = mResources.phoneInvalid;
				}
				num = 1;
			}
			catch (Exception)
			{
				if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
				{
					text = mResources.emailInvalid;
				}
				num = 0;
			}
		}
		if (text != null)
		{
			GameCanvas.startOKDlg(text);
		}
		else
		{
			GameCanvas.msgdlg.setInfo(string.Concat(new string[]
			{
				mResources.plsCheckAcc,
				(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
				this.tfUser.getText(),
				"\n",
				mResources.password,
				": ",
				this.tfPass.getText()
			}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000501F4 File Offset: 0x0004E3F4
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect.ToString()), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000502AE File Offset: 0x0004E4AE
	public void doViewFAQ()
	{
		if (this.listFAQ.Equals(string.Empty))
		{
			this.listFAQ.Equals(string.Empty);
		}
		if (!Session_ME.connected)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000502EC File Offset: 0x0004E4EC
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		if (LoginScr.isLocal)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		if (this.loadIndexServer() != -1 && !GameCanvas.isTouch)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0005038E File Offset: 0x0004E58E
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0005039B File Offset: 0x0004E59B
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x000503A8 File Offset: 0x0004E5A8
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		if (text != null && !text.Equals(string.Empty))
		{
			this.isLogin2 = false;
		}
		else if (Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty))
		{
			this.isLogin2 = true;
		}
		else
		{
			this.isLogin2 = false;
		}
		if ((text == null || text.Equals(string.Empty)) && this.isLogin2)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			text2 = "a";
		}
		if (text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty))
		{
			return;
		}
		if (text2.Equals(string.Empty))
		{
			this.focus = 1;
			this.tfUser.isFocus = false;
			this.tfPass.isFocus = true;
			if (!GameCanvas.isTouch)
			{
				this.right = this.tfPass.cmdClear;
			}
			return;
		}
		if (!Session_ME.gI().isConnected())
		{
			GameCanvas.connect();
		}
		Res.outz(string.Concat(new string[]
		{
			"ccccccc ",
			text,
			" ",
			text2,
			" ",
			GameMidlet.VERSION,
			" ",
			(this.isLogin2 ? 1 : 0).ToString()
		}));
		Service.gI().login(text, text2, GameMidlet.VERSION, this.isLogin2 ? 1 : 0);
		if (Session_ME.connected)
		{
			GameCanvas.startWaitDlg();
		}
		else
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
		}
		this.focus = 0;
		if (!this.isLogin2)
		{
			this.actRegisterLeft();
		}
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00050594 File Offset: 0x0004E794
	public void savePass()
	{
		if (this.isCheck)
		{
			Rms.saveRMSInt("check", 1);
			Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
			return;
		}
		Rms.saveRMSInt("check", 2);
		Rms.saveRMSString("acc", string.Empty);
		Rms.saveRMSString("pass", string.Empty);
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0005061C File Offset: 0x0004E81C
	public override void update()
	{
		if (Main.isWindowsPhone && this.isRegistering)
		{
			if (this.t < 0)
			{
				GameCanvas.endDlg();
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				this.isRegistering = false;
			}
			else
			{
				this.t--;
			}
		}
		if (LoginScr.timeLogin > 0)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			if (LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L)
			{
				LoginScr.timeLogin -= 1;
				if (LoginScr.timeLogin == 0)
				{
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (TouchScreenKeyboard.visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			((Effect2)Effect2.vEffect2.elementAt(i)).update();
		}
		if (LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		GameCanvas.debug("LGU1", 0);
		GameCanvas.debug("LGU2", 0);
		GameCanvas.debug("LGU3", 0);
		this.updateLogo();
		GameCanvas.debug("LGU4", 0);
		GameCanvas.debug("LGU5", 0);
		if (this.g >= 0)
		{
			this.ylogo += this.dir * this.g;
			this.g += this.dir * this.v;
			if (this.g <= 0)
			{
				this.dir *= -1;
			}
			if (this.ylogo > 0)
			{
				this.dir *= -1;
				this.g -= 2 * this.v;
			}
		}
		GameCanvas.debug("LGU6", 0);
		if (this.tipid >= 0 && GameCanvas.gameTick % 100 == 0)
		{
			this.doChangeTip();
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (!Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone)
		{
			string text = this.tfUser.getText().ToLower().Trim();
			string text2 = this.tfPass.getText().ToLower().Trim();
			if (!text.Equals(string.Empty) && !text2.Equals(string.Empty))
			{
				this.doLogin();
			}
			Main.isMiniApp = true;
		}
		this.updateTfWhenOpenKb();
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00050AA4 File Offset: 0x0004ECA4
	internal void doChangeTip()
	{
		this.tipid++;
		if (this.tipid >= mResources.tips.Length)
		{
			this.tipid = 0;
		}
		if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00050B03 File Offset: 0x0004ED03
	public void updateLogo()
	{
		if (this.defYL != this.yL)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00050B2F File Offset: 0x0004ED2F
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfPass.isFocus)
		{
			this.tfPass.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00050B6E File Offset: 0x0004ED6E
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00050B78 File Offset: 0x0004ED78
	public override void paint(mGraphics g)
	{
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		if (GameCanvas.h <= 220)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		if (mSystem.clientType == 1 && !GameCanvas.isTouch)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		if (ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (GameCanvas.currentDialog == null)
		{
			int num2 = 105;
			int num3 = ((GameCanvas.w < 200) ? 160 : 180);
			PopUp.paintPopUp(g, this.xLog, this.yLog - 10, num3, num2, -1, true);
			if (GameCanvas.h > 160 && LoginScr.imgTitle != null)
			{
				g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
			}
			GameCanvas.debug("PLG4", 1);
			int num4 = 4;
			int num5 = num4 * 32 + 23 + 33;
			if (num5 >= GameCanvas.w)
			{
				num5 = (num4 - 1) * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num5 / 2;
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.tfPass.x = this.xLog + 10;
			this.tfPass.y = this.yLog + 55;
			this.tfUser.paint(g);
			this.tfPass.paint(g);
			if (GameCanvas.w < 176)
			{
				mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
			}
		}
		base.paint(g);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00050E14 File Offset: 0x0004F014
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
		}
		else if (mSystem.clientType == 1 && GameCanvas.keyPressed[13])
		{
			GameCanvas.keyPressed[13] = false;
			this.cmdCallHotline.performAction();
		}
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		if (!GameCanvas.isTouch)
		{
			if (this.tfUser.isFocus)
			{
				this.right = this.tfUser.cmdClear;
			}
			else
			{
				this.right = this.tfPass.cmdClear;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 0;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			GameCanvas.clearKeyPressed();
			if (!this.isLogin2 || this.isRes)
			{
				if (this.focus == 1)
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = true;
				}
				else if (this.focus == 0)
				{
					this.tfUser.isFocus = true;
					this.tfPass.isFocus = false;
				}
				else
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = false;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (GameCanvas.isPointerJustRelease && (!this.isLogin2 || this.isRes))
		{
			if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
			{
				this.focus = 0;
			}
			else if (GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height))
			{
				this.focus = 1;
			}
		}
		if (Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null)
		{
			this.right.performAction();
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0005110A File Offset: 0x0004F30A
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00051114 File Offset: 0x0004F314
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 1000:
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception)
			{
			}
			GameCanvas.endDlg();
			return;
		case 1001:
			GameCanvas.endDlg();
			this.isRes = false;
			return;
		case 1002:
		{
			GameCanvas.startWaitDlg();
			string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			if (text == null || text.Equals(string.Empty))
			{
				Service.gI().login2(string.Empty);
				return;
			}
			GameCanvas.loginScr.isLogin2 = true;
			GameCanvas.connect();
			Service.gI().setClientType();
			Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
			return;
		}
		case 1003:
			GameCanvas.startOKDlg(mResources.goToWebForPassword);
			return;
		case 1004:
			ServerListScreen.doUpdateServer();
			GameCanvas.serverScreen.switchToMe();
			return;
		case 1005:
			try
			{
				GameMidlet.instance.platformRequest("http://ngocrongonline.com");
				return;
			}
			catch (Exception)
			{
				return;
			}
			break;
		}
		switch (idAction)
		{
		case 2000:
			return;
		case 2001:
			if (this.isCheck)
			{
				this.isCheck = false;
				return;
			}
			this.isCheck = true;
			return;
		case 2002:
			this.doRegister();
			return;
		case 2003:
			this.doMenu();
			return;
		case 2004:
			this.actRegister();
			return;
		case 2008:
			Rms.saveRMSString("acc", this.tfUser.getText().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().Trim());
			if (ServerListScreen.loadScreen)
			{
				GameCanvas.serverScreen.switchToMe();
				return;
			}
			GameCanvas.serverScreen.show2();
			return;
		}
		if (idAction != 10041)
		{
			if (idAction == 10042)
			{
				Rms.saveRMSInt("lowGraphic", 1);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				return;
			}
			if (idAction != 13)
			{
				if (idAction != 4000)
				{
					if (idAction == 10021)
					{
						this.actRegisterLeft();
						return;
					}
				}
				else
				{
					this.doRegister(this.tfUser.getText());
				}
				return;
			}
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				return;
			case 2:
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				return;
			case 4:
				mSystem.callHotlinePC();
				return;
			case 6:
				mSystem.callHotlineWindowsPhone();
				return;
			default:
				return;
			}
		}
		else
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x000513A0 File Offset: 0x0004F5A0
	public void actRegisterLeft()
	{
		if (this.isLogin2)
		{
			this.doLogin();
			return;
		}
		this.isRes = false;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
		this.left = this.cmdMenu;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x000513DC File Offset: 0x0004F5DC
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00051404 File Offset: 0x0004F604
	public void backToRegister()
	{
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
		ServerListScreen.countDieConnect = 0;
		if (GameCanvas.loginScr.isLogin2)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
			return;
		}
		if (Main.isWindowsPhone)
		{
			GameMidlet.isBackWindowsPhone = true;
		}
		GameCanvas.instance.resetToLoginScr = false;
		GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
	}

	// Token: 0x04000A71 RID: 2673
	public TField tfUser;

	// Token: 0x04000A72 RID: 2674
	public TField tfPass;

	// Token: 0x04000A73 RID: 2675
	public static bool isContinueToLogin = false;

	// Token: 0x04000A74 RID: 2676
	internal int focus;

	// Token: 0x04000A75 RID: 2677
	internal int wC;

	// Token: 0x04000A76 RID: 2678
	internal int yL;

	// Token: 0x04000A77 RID: 2679
	internal int defYL;

	// Token: 0x04000A78 RID: 2680
	public bool isCheck;

	// Token: 0x04000A79 RID: 2681
	public bool isRes;

	// Token: 0x04000A7A RID: 2682
	public Command cmdLogin;

	// Token: 0x04000A7B RID: 2683
	public Command cmdCheck;

	// Token: 0x04000A7C RID: 2684
	public Command cmdFogetPass;

	// Token: 0x04000A7D RID: 2685
	public Command cmdRes;

	// Token: 0x04000A7E RID: 2686
	public Command cmdMenu;

	// Token: 0x04000A7F RID: 2687
	public Command cmdBackFromRegister;

	// Token: 0x04000A80 RID: 2688
	public string listFAQ = string.Empty;

	// Token: 0x04000A81 RID: 2689
	public string titleFAQ;

	// Token: 0x04000A82 RID: 2690
	public string subtitleFAQ;

	// Token: 0x04000A83 RID: 2691
	internal string numSupport = string.Empty;

	// Token: 0x04000A84 RID: 2692
	public static bool isLocal = false;

	// Token: 0x04000A85 RID: 2693
	public static bool isUpdateAll;

	// Token: 0x04000A86 RID: 2694
	public static bool isUpdateData;

	// Token: 0x04000A87 RID: 2695
	public static bool isUpdateMap;

	// Token: 0x04000A88 RID: 2696
	public static bool isUpdateSkill;

	// Token: 0x04000A89 RID: 2697
	public static bool isUpdateItem;

	// Token: 0x04000A8A RID: 2698
	public static string serverName;

	// Token: 0x04000A8B RID: 2699
	public static Image imgTitle;

	// Token: 0x04000A8C RID: 2700
	public int plX;

	// Token: 0x04000A8D RID: 2701
	public int plY;

	// Token: 0x04000A8E RID: 2702
	public int lY;

	// Token: 0x04000A8F RID: 2703
	public int lX;

	// Token: 0x04000A90 RID: 2704
	public int logoDes;

	// Token: 0x04000A91 RID: 2705
	public int lineX;

	// Token: 0x04000A92 RID: 2706
	public int lineY;

	// Token: 0x04000A93 RID: 2707
	public static int[] bgId = new int[] { 0, 8, 2, 6, 9 };

	// Token: 0x04000A94 RID: 2708
	public static bool isTryGetIPFromWap;

	// Token: 0x04000A95 RID: 2709
	public static short timeLogin;

	// Token: 0x04000A96 RID: 2710
	public static long lastTimeLogin;

	// Token: 0x04000A97 RID: 2711
	public static long currTimeLogin;

	// Token: 0x04000A98 RID: 2712
	internal int yt;

	// Token: 0x04000A99 RID: 2713
	internal Command cmdSelect;

	// Token: 0x04000A9A RID: 2714
	internal Command cmdOK;

	// Token: 0x04000A9B RID: 2715
	internal int xLog;

	// Token: 0x04000A9C RID: 2716
	internal int yLog;

	// Token: 0x04000A9D RID: 2717
	public static GameMidlet m;

	// Token: 0x04000A9E RID: 2718
	internal int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000A9F RID: 2719
	internal int freeAreaHeight;

	// Token: 0x04000AA0 RID: 2720
	internal int xP;

	// Token: 0x04000AA1 RID: 2721
	internal int yP;

	// Token: 0x04000AA2 RID: 2722
	internal int wP;

	// Token: 0x04000AA3 RID: 2723
	internal int hP;

	// Token: 0x04000AA4 RID: 2724
	internal int t = 20;

	// Token: 0x04000AA5 RID: 2725
	internal bool isRegistering;

	// Token: 0x04000AA6 RID: 2726
	internal string passRe = string.Empty;

	// Token: 0x04000AA7 RID: 2727
	public bool isFAQ;

	// Token: 0x04000AA8 RID: 2728
	internal int tipid = -1;

	// Token: 0x04000AA9 RID: 2729
	public bool isLogin2;

	// Token: 0x04000AAA RID: 2730
	internal int v = 2;

	// Token: 0x04000AAB RID: 2731
	internal int g;

	// Token: 0x04000AAC RID: 2732
	internal int ylogo = -40;

	// Token: 0x04000AAD RID: 2733
	internal int dir = 1;

	// Token: 0x04000AAE RID: 2734
	internal Command cmdCallHotline;

	// Token: 0x04000AAF RID: 2735
	public static bool isLoggingIn;
}
