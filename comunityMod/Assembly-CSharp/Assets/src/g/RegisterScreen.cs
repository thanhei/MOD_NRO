using System;

namespace Assets.src.g
{
	// Token: 0x020001B9 RID: 441
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x000C5E18 File Offset: 0x000C4018
		public RegisterScreen(sbyte haveName)
		{
			this.yLog = 130;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
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
			this.tfSodt = new TField();
			this.tfSodt.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfSodt.width = 220;
			this.tfSodt.height = mScreen.ITEM_HEIGHT + 2;
			this.tfSodt.name = "Số điện thoại/ địa chỉ email";
			if (haveName == 1)
			{
				this.tfSodt.setText("01234567890");
			}
			this.tfUser = new TField();
			this.tfUser.width = 220;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.name = "Họ và tên";
			if (haveName == 1)
			{
				this.tfUser.setText("Nguyễn Văn A");
			}
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgay = new TField();
			this.tfNgay.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgay.width = 70;
			this.tfNgay.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgay.name = "Ngày sinh";
			if (haveName == 1)
			{
				this.tfNgay.setText("01");
			}
			this.tfThang = new TField();
			this.tfThang.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfThang.width = 70;
			this.tfThang.height = mScreen.ITEM_HEIGHT + 2;
			this.tfThang.name = "Tháng sinh";
			if (haveName == 1)
			{
				this.tfThang.setText("01");
			}
			this.tfNam = new TField();
			this.tfNam.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNam.width = 70;
			this.tfNam.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNam.name = "Năm sinh";
			if (haveName == 1)
			{
				this.tfNam.setText("1990");
			}
			this.tfDiachi = new TField();
			this.tfDiachi.setIputType(TField.INPUT_TYPE_ANY);
			this.tfDiachi.width = 220;
			this.tfDiachi.height = mScreen.ITEM_HEIGHT + 2;
			this.tfDiachi.name = "Địa chỉ đăng ký thường trú";
			if (haveName == 1)
			{
				this.tfDiachi.setText("123 đường số 1, Quận 1, TP.HCM");
			}
			this.tfCMND = new TField();
			this.tfCMND.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfCMND.width = 220;
			this.tfCMND.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCMND.name = "Số Chứng minh nhân dân hoặc số hộ chiếu";
			if (haveName == 1)
			{
				this.tfCMND.setText("123456789");
			}
			this.tfNgayCap = new TField();
			this.tfNgayCap.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgayCap.width = 220;
			this.tfNgayCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgayCap.name = "Ngày cấp";
			if (haveName == 1)
			{
				this.tfNgayCap.setText("01/01/2005");
			}
			this.tfNoiCap = new TField();
			this.tfNoiCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNoiCap.width = 220;
			this.tfNoiCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNoiCap.name = "Nơi cấp";
			if (haveName == 1)
			{
				this.tfNoiCap.setText("TP.HCM");
			}
			this.yt += 35;
			this.isCheck = true;
			this.focus = 0;
			this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
			this.cmdCheck = new Command(mResources.remember, this, 2001, null);
			this.cmdRes = new Command(mResources.register, this, 2002, null);
			this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
			this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
			if (GameCanvas.isTouch)
			{
				this.cmdLogin.x = GameCanvas.w / 2 - 100;
				this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
				if (GameCanvas.h >= 200)
				{
					this.cmdLogin.y = GameCanvas.h / 2 - 40;
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
			int num = 4;
			int num2 = num * 32 + 23 + 33;
			if (num2 >= GameCanvas.w)
			{
				num2 = (num - 1) * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num2 / 2;
			this.yLog = 5;
			this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null);
			this.cmdOK.x = 260;
			this.cmdOK.y = GameCanvas.h - 60;
			this.cmdFogetPass = new Command("Thoát", this, 1003, null);
			this.cmdFogetPass.x = 260;
			this.cmdFogetPass.y = GameCanvas.h - 30;
			if (GameCanvas.w < 250)
			{
				this.cmdOK.x = GameCanvas.w / 2 - 80;
				this.cmdFogetPass.x = GameCanvas.w / 2 + 10;
				this.cmdFogetPass.y = (this.cmdOK.y = GameCanvas.h - 25);
			}
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000C65BC File Offset: 0x000C47BC
		public new void switchToMe()
		{
			Res.outz("Res switch");
			SoundMn.gI().stopAll();
			this.focus = 0;
			this.tfUser.isFocus = true;
			this.tfNgay.isFocus = false;
			if (GameCanvas.isTouch)
			{
				this.tfUser.isFocus = false;
				this.focus = -1;
			}
			base.switchToMe();
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000C661C File Offset: 0x000C481C
		protected void doMenu()
		{
			MyVector myVector = new MyVector("vMenu Login");
			myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
			if (!this.isLogin2)
			{
				myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
			}
			myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
			myVector.addElement(new Command(mResources.website, this, 1005, null));
			if (Rms.loadRMSInt("lowGraphic") == 1)
			{
				myVector.addElement(new Command(mResources.increase_vga, this, 10041, null));
			}
			else
			{
				myVector.addElement(new Command(mResources.decrease_vga, this, 10042, null));
			}
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000C66FC File Offset: 0x000C48FC
		protected void doRegister()
		{
			if (this.tfUser.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.userBlank);
				return;
			}
			this.tfUser.getText().ToCharArray();
			if (this.tfNgay.getText().Equals(string.Empty))
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
					this.tfNgay.getText()
				}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
			}
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00004887 File Offset: 0x00002A87
		protected void doRegister(string user)
		{
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000C6938 File Offset: 0x000C4B38
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

		// Token: 0x060012DD RID: 4829 RVA: 0x000C6978 File Offset: 0x000C4B78
		protected void doSelectServer()
		{
			MyVector myVector = new MyVector("vServer");
			if (RegisterScreen.isLocal)
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

		// Token: 0x060012DE RID: 4830 RVA: 0x0005038E File Offset: 0x0004E58E
		protected void saveIndexServer(int index)
		{
			Rms.saveRMSInt("indServer", index);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0005039B File Offset: 0x0004E59B
		protected int loadIndexServer()
		{
			return Rms.loadRMSInt("indServer");
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00004887 File Offset: 0x00002A87
		public void doLogin()
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00004887 File Offset: 0x00002A87
		public void savePass()
		{
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000C6A20 File Offset: 0x000C4C20
		public override void update()
		{
			this.tfUser.update();
			this.tfNgay.update();
			this.tfThang.update();
			this.tfNam.update();
			this.tfDiachi.update();
			this.tfCMND.update();
			this.tfNoiCap.update();
			this.tfSodt.update();
			this.tfNgayCap.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				((Effect2)Effect2.vEffect2.elementAt(i)).update();
			}
			if (RegisterScreen.isUpdateAll && !RegisterScreen.isUpdateData && !RegisterScreen.isUpdateItem && !RegisterScreen.isUpdateMap && !RegisterScreen.isUpdateSkill)
			{
				RegisterScreen.isUpdateAll = false;
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
			if (GameCanvas.isTouch)
			{
				if (this.isRes)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
					return;
				}
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
				return;
			}
			else
			{
				if (this.isRes)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
					return;
				}
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
				return;
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000C6C7C File Offset: 0x000C4E7C
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

		// Token: 0x060012E4 RID: 4836 RVA: 0x000C6CDB File Offset: 0x000C4EDB
		public void updateLogo()
		{
			if (this.defYL != this.yL)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000C6D08 File Offset: 0x000C4F08
		public override void keyPress(int keyCode)
		{
			if (this.tfUser.isFocus)
			{
				this.tfUser.keyPressed(keyCode);
			}
			else if (this.tfNgay.isFocus)
			{
				this.tfNgay.keyPressed(keyCode);
			}
			else if (this.tfThang.isFocus)
			{
				this.tfThang.keyPressed(keyCode);
			}
			else if (this.tfNam.isFocus)
			{
				this.tfNam.keyPressed(keyCode);
			}
			else if (this.tfDiachi.isFocus)
			{
				this.tfDiachi.keyPressed(keyCode);
			}
			else if (this.tfCMND.isFocus)
			{
				this.tfCMND.keyPressed(keyCode);
			}
			else if (this.tfNoiCap.isFocus)
			{
				this.tfNoiCap.keyPressed(keyCode);
			}
			else if (this.tfSodt.isFocus)
			{
				this.tfSodt.keyPressed(keyCode);
			}
			else if (this.tfNgayCap.isFocus)
			{
				this.tfNgayCap.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00050B6E File Offset: 0x0004ED6E
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000C6E24 File Offset: 0x000C5024
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
			if (ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null)
			{
				return;
			}
			if (GameCanvas.currentDialog == null)
			{
				this.xLog = 5;
				int num2 = 233;
				if (GameCanvas.w < 260)
				{
					this.xLog = (GameCanvas.w - 240) / 2;
				}
				this.yLog = (GameCanvas.h - num2) / 2;
				int w = GameCanvas.w;
				PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num2, -1, true);
				if (GameCanvas.h > 160 && RegisterScreen.imgTitle != null)
				{
					g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, num, 3);
				}
				GameCanvas.debug("PLG4", 1);
				int num3 = 4;
				int num4 = num3 * 32 + 23 + 33;
				int w2 = GameCanvas.w;
				this.tfSodt.x = this.xLog + 10;
				this.tfSodt.y = this.yLog + 15;
				this.tfUser.x = this.tfSodt.x;
				this.tfUser.y = this.tfSodt.y + 30;
				this.tfNgay.x = this.xLog + 10;
				this.tfNgay.y = this.tfUser.y + 30;
				this.tfThang.x = this.tfNgay.x + 75;
				this.tfThang.y = this.tfNgay.y;
				this.tfNam.x = this.tfThang.x + 75;
				this.tfNam.y = this.tfThang.y;
				this.tfDiachi.x = this.tfUser.x;
				this.tfDiachi.y = this.tfNgay.y + 30;
				this.tfCMND.x = this.tfUser.x;
				this.tfCMND.y = this.tfDiachi.y + 30;
				this.tfNgayCap.x = this.tfUser.x;
				this.tfNgayCap.y = this.tfCMND.y + 30;
				this.tfNoiCap.x = this.tfUser.x;
				this.tfNoiCap.y = this.tfNgayCap.y + 30;
				this.tfUser.paint(g);
				this.tfNgay.paint(g);
				this.tfThang.paint(g);
				this.tfNam.paint(g);
				this.tfDiachi.paint(g);
				this.tfCMND.paint(g);
				this.tfNgayCap.paint(g);
				this.tfNoiCap.paint(g);
				this.tfSodt.paint(g);
				if (GameCanvas.w < 176)
				{
					mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfNgay.x - 35, this.tfNgay.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.server + ": " + RegisterScreen.serverName, GameCanvas.w / 2, this.tfNgay.y + 32, 2);
					bool flag = this.isRes;
				}
			}
			string version = GameMidlet.VERSION;
			g.setColor(GameCanvas.skyColor);
			g.fillRect(GameCanvas.w - 40, 4, 36, 11);
			mFont.tahoma_7_grey.drawString(g, version, GameCanvas.w - 22, 4, mFont.CENTER);
			GameCanvas.resetTrans(g);
			if (GameCanvas.currentDialog == null)
			{
				if (GameCanvas.w > 250)
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi", 260, 10, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "chỉ có thể chơi", 260, 25, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", 260, 40, 0, mFont.tahoma_7b_dark);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi chỉ có thể chơi", GameCanvas.w / 2, 5, 2, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", GameCanvas.w / 2, 15, 2, mFont.tahoma_7b_dark);
				}
			}
			base.paint(g);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000C72E4 File Offset: 0x000C54E4
		internal void turnOffFocus()
		{
			this.tfUser.isFocus = false;
			this.tfNgay.isFocus = false;
			this.tfThang.isFocus = false;
			this.tfNam.isFocus = false;
			this.tfDiachi.isFocus = false;
			this.tfCMND.isFocus = false;
			this.tfNgayCap.isFocus = false;
			this.tfNoiCap.isFocus = false;
			this.tfSodt.isFocus = false;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000C7360 File Offset: 0x000C5560
		internal void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUser.isFocus = true;
				return;
			case 1:
				this.tfNgay.isFocus = true;
				return;
			case 2:
				this.tfThang.isFocus = true;
				return;
			case 3:
				this.tfNam.isFocus = true;
				return;
			case 4:
				this.tfDiachi.isFocus = true;
				return;
			case 5:
				this.tfCMND.isFocus = true;
				return;
			case 6:
				this.tfNgayCap.isFocus = true;
				return;
			case 7:
				this.tfNoiCap.isFocus = true;
				return;
			case 8:
				this.tfSodt.isFocus = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000C741C File Offset: 0x000C561C
		public override void updateKey()
		{
			if (RegisterScreen.isContinueToLogin)
			{
				return;
			}
			if (!GameCanvas.isTouch)
			{
				if (this.tfUser.isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else if (this.tfNgay.isFocus)
				{
					this.right = this.tfNgay.cmdClear;
				}
				else if (this.tfThang.isFocus)
				{
					this.right = this.tfThang.cmdClear;
				}
				else if (this.tfNam.isFocus)
				{
					this.right = this.tfNam.cmdClear;
				}
				else if (this.tfDiachi.isFocus)
				{
					this.right = this.tfDiachi.cmdClear;
				}
				else if (this.tfCMND.isFocus)
				{
					this.right = this.tfCMND.cmdClear;
				}
				else if (this.tfNgayCap.isFocus)
				{
					this.right = this.tfNgayCap.cmdClear;
				}
				else if (this.tfNoiCap.isFocus)
				{
					this.right = this.tfNoiCap.cmdClear;
				}
				else if (this.tfSodt.isFocus)
				{
					this.right = this.tfSodt.cmdClear;
				}
			}
			if (GameCanvas.keyPressed[21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 8;
				}
				this.processFocus();
			}
			else if (GameCanvas.keyPressed[22])
			{
				this.focus++;
				if (this.focus > 8)
				{
					this.focus = 0;
				}
				this.processFocus();
			}
			if (GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22])
			{
				GameCanvas.clearKeyPressed();
				if (!this.isLogin2 || this.isRes)
				{
					if (this.focus == 1)
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = true;
					}
					else if (this.focus == 0)
					{
						this.tfUser.isFocus = true;
						this.tfNgay.isFocus = false;
					}
					else
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = false;
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
			if (GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgay.x, this.tfNgay.y, this.tfNgay.width, this.tfNgay.height))
				{
					this.focus = 1;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfThang.x, this.tfThang.y, this.tfThang.width, this.tfThang.height))
				{
					this.focus = 2;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNam.x, this.tfNam.y, this.tfNam.width, this.tfNam.height))
				{
					this.focus = 3;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfDiachi.x, this.tfDiachi.y, this.tfDiachi.width, this.tfDiachi.height))
				{
					this.focus = 4;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfCMND.x, this.tfCMND.y, this.tfCMND.width, this.tfCMND.height))
				{
					this.focus = 5;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgayCap.x, this.tfNgayCap.y, this.tfNgayCap.width, this.tfNgayCap.height))
				{
					this.focus = 6;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNoiCap.x, this.tfNoiCap.y, this.tfNoiCap.width, this.tfNoiCap.height))
				{
					this.focus = 7;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfSodt.x, this.tfSodt.y, this.tfSodt.width, this.tfSodt.height))
				{
					this.focus = 8;
					this.processFocus();
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000C7940 File Offset: 0x000C5B40
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000C794C File Offset: 0x000C5B4C
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1000:
				try
				{
					GameMidlet.instance.platformRequest((string)p);
				}
				catch (Exception ex)
				{
					ex.StackTrace.ToString();
				}
				GameCanvas.endDlg();
				return;
			case 1001:
				GameCanvas.endDlg();
				this.isRes = false;
				return;
			case 1002:
				return;
			case 1003:
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
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
				catch (Exception ex2)
				{
					ex2.StackTrace.ToString();
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
				if (this.tfNgay.getText().Equals(string.Empty) || this.tfThang.getText().Equals(string.Empty) || this.tfNam.getText().Equals(string.Empty) || this.tfDiachi.getText().Equals(string.Empty) || this.tfCMND.getText().Equals(string.Empty) || this.tfNgayCap.getText().Equals(string.Empty) || this.tfNoiCap.getText().Equals(string.Empty) || this.tfSodt.getText().Equals(string.Empty) || this.tfUser.getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
					return;
				}
				GameCanvas.startOKDlg(mResources.PLEASEWAIT);
				Service.gI().charInfo(this.tfNgay.getText(), this.tfThang.getText(), this.tfNam.getText(), this.tfDiachi.getText(), this.tfCMND.getText(), this.tfNgayCap.getText(), this.tfNoiCap.getText(), this.tfSodt.getText(), this.tfUser.getText());
				return;
			}
			if (idAction == 10041 || idAction == 10042)
			{
				return;
			}
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
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000C7C10 File Offset: 0x000C5E10
		public void actRegisterLeft()
		{
			if (this.isLogin2)
			{
				this.doLogin();
				return;
			}
			this.isRes = false;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000C7C4C File Offset: 0x000C5E4C
		public void actRegister()
		{
			GameCanvas.endDlg();
			GameCanvas.startOKDlg(mResources.regNote);
			this.isRes = true;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000C7C7C File Offset: 0x000C5E7C
		public void backToRegister()
		{
			if (GameCanvas.loginScr.isLogin2)
			{
				GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
				return;
			}
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
			Session_ME.gI().close();
		}

		// Token: 0x04001A0A RID: 6666
		public TField tfUser;

		// Token: 0x04001A0B RID: 6667
		public TField tfNgay;

		// Token: 0x04001A0C RID: 6668
		public TField tfThang;

		// Token: 0x04001A0D RID: 6669
		public TField tfNam;

		// Token: 0x04001A0E RID: 6670
		public TField tfDiachi;

		// Token: 0x04001A0F RID: 6671
		public TField tfCMND;

		// Token: 0x04001A10 RID: 6672
		public TField tfNgayCap;

		// Token: 0x04001A11 RID: 6673
		public TField tfNoiCap;

		// Token: 0x04001A12 RID: 6674
		public TField tfSodt;

		// Token: 0x04001A13 RID: 6675
		public static bool isContinueToLogin = false;

		// Token: 0x04001A14 RID: 6676
		internal int focus;

		// Token: 0x04001A15 RID: 6677
		internal int wC;

		// Token: 0x04001A16 RID: 6678
		internal int yL;

		// Token: 0x04001A17 RID: 6679
		internal int defYL;

		// Token: 0x04001A18 RID: 6680
		public bool isCheck;

		// Token: 0x04001A19 RID: 6681
		public bool isRes;

		// Token: 0x04001A1A RID: 6682
		internal Command cmdLogin;

		// Token: 0x04001A1B RID: 6683
		internal Command cmdCheck;

		// Token: 0x04001A1C RID: 6684
		internal Command cmdFogetPass;

		// Token: 0x04001A1D RID: 6685
		internal Command cmdRes;

		// Token: 0x04001A1E RID: 6686
		internal Command cmdMenu;

		// Token: 0x04001A1F RID: 6687
		internal Command cmdBackFromRegister;

		// Token: 0x04001A20 RID: 6688
		public string listFAQ = string.Empty;

		// Token: 0x04001A21 RID: 6689
		public string titleFAQ;

		// Token: 0x04001A22 RID: 6690
		public string subtitleFAQ;

		// Token: 0x04001A23 RID: 6691
		internal string numSupport = string.Empty;

		// Token: 0x04001A24 RID: 6692
		internal string strUser;

		// Token: 0x04001A25 RID: 6693
		internal string strPass;

		// Token: 0x04001A26 RID: 6694
		public static bool isLocal = false;

		// Token: 0x04001A27 RID: 6695
		public static bool isUpdateAll;

		// Token: 0x04001A28 RID: 6696
		public static bool isUpdateData;

		// Token: 0x04001A29 RID: 6697
		public static bool isUpdateMap;

		// Token: 0x04001A2A RID: 6698
		public static bool isUpdateSkill;

		// Token: 0x04001A2B RID: 6699
		public static bool isUpdateItem;

		// Token: 0x04001A2C RID: 6700
		public static string serverName;

		// Token: 0x04001A2D RID: 6701
		public static Image imgTitle;

		// Token: 0x04001A2E RID: 6702
		public int plX;

		// Token: 0x04001A2F RID: 6703
		public int plY;

		// Token: 0x04001A30 RID: 6704
		public int lY;

		// Token: 0x04001A31 RID: 6705
		public int lX;

		// Token: 0x04001A32 RID: 6706
		public int logoDes;

		// Token: 0x04001A33 RID: 6707
		public int lineX;

		// Token: 0x04001A34 RID: 6708
		public int lineY;

		// Token: 0x04001A35 RID: 6709
		public static int[] bgId = new int[] { 0, 8, 2, 6, 9 };

		// Token: 0x04001A36 RID: 6710
		public static bool isTryGetIPFromWap;

		// Token: 0x04001A37 RID: 6711
		public static short timeLogin;

		// Token: 0x04001A38 RID: 6712
		public static long lastTimeLogin;

		// Token: 0x04001A39 RID: 6713
		public static long currTimeLogin;

		// Token: 0x04001A3A RID: 6714
		internal int yt;

		// Token: 0x04001A3B RID: 6715
		internal Command cmdSelect;

		// Token: 0x04001A3C RID: 6716
		internal Command cmdOK;

		// Token: 0x04001A3D RID: 6717
		internal int xLog;

		// Token: 0x04001A3E RID: 6718
		internal int yLog;

		// Token: 0x04001A3F RID: 6719
		internal int xP;

		// Token: 0x04001A40 RID: 6720
		internal int yP;

		// Token: 0x04001A41 RID: 6721
		internal int wP;

		// Token: 0x04001A42 RID: 6722
		internal int hP;

		// Token: 0x04001A43 RID: 6723
		internal string passRe = string.Empty;

		// Token: 0x04001A44 RID: 6724
		public bool isFAQ;

		// Token: 0x04001A45 RID: 6725
		internal int tipid = -1;

		// Token: 0x04001A46 RID: 6726
		public bool isLogin2;

		// Token: 0x04001A47 RID: 6727
		internal int v = 2;

		// Token: 0x04001A48 RID: 6728
		internal int g;

		// Token: 0x04001A49 RID: 6729
		internal int ylogo = -40;

		// Token: 0x04001A4A RID: 6730
		internal int dir = 1;

		// Token: 0x04001A4B RID: 6731
		public static bool isLoggingIn;
	}
}
