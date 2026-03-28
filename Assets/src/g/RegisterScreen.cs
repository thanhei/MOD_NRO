using System;

namespace Assets.src.g
{
	// Token: 0x020000C2 RID: 194
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x000935D0 File Offset: 0x000917D0
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
			int num = (GameCanvas.w < 200) ? 140 : 160;
			this.wC = num;
			this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
			if (GameCanvas.h <= 160)
			{
				this.yt = 20;
			}
			this.tfSodt = new TField();
			this.tfSodt.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfSodt.width = 220;
			this.tfSodt.height = mScreen.ITEM_HEIGHT + 2;
			this.tfSodt.name = "Số điện thoại";
			if ((int)haveName == 1)
			{
				this.tfSodt.setText("01234567890");
			}
			this.tfUser = new TField();
			this.tfUser.width = 220;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.name = "Họ và tên";
			if ((int)haveName == 1)
			{
				this.tfUser.setText("Nguyễn Văn A");
			}
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgay = new TField();
			this.tfNgay.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgay.width = 70;
			this.tfNgay.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgay.name = "Ngày sinh";
			if ((int)haveName == 1)
			{
				this.tfNgay.setText("01");
			}
			this.tfThang = new TField();
			this.tfThang.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfThang.width = 70;
			this.tfThang.height = mScreen.ITEM_HEIGHT + 2;
			this.tfThang.name = "Tháng sinh";
			if ((int)haveName == 1)
			{
				this.tfThang.setText("01");
			}
			this.tfNam = new TField();
			this.tfNam.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNam.width = 70;
			this.tfNam.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNam.name = "Năm sinh";
			if ((int)haveName == 1)
			{
				this.tfNam.setText("1990");
			}
			this.tfDiachi = new TField();
			this.tfDiachi.setIputType(TField.INPUT_TYPE_ANY);
			this.tfDiachi.width = 220;
			this.tfDiachi.height = mScreen.ITEM_HEIGHT + 2;
			this.tfDiachi.name = "Địa chỉ đăng ký thường trú";
			if ((int)haveName == 1)
			{
				this.tfDiachi.setText("123 đường số 1, Quận 1, TP.HCM");
			}
			this.tfCMND = new TField();
			this.tfCMND.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfCMND.width = 220;
			this.tfCMND.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCMND.name = "Số Chứng minh nhân dân hoặc số hộ chiếu";
			if ((int)haveName == 1)
			{
				this.tfCMND.setText("123456789");
			}
			this.tfNgayCap = new TField();
			this.tfNgayCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgayCap.width = 220;
			this.tfNgayCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgayCap.name = "Ngày cấp";
			if ((int)haveName == 1)
			{
				this.tfNgayCap.setText("01/01/2005");
			}
			this.tfNoiCap = new TField();
			this.tfNoiCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNoiCap.width = 220;
			this.tfNoiCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNoiCap.name = "Nơi cấp";
			if ((int)haveName == 1)
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
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
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
			this.cmdOK.x = GameCanvas.w / 2 - 80;
			this.cmdFogetPass.x = GameCanvas.w / 2 + 10;
			this.cmdFogetPass.y = (this.cmdOK.y = GameCanvas.h - 25);
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00093DB4 File Offset: 0x00091FB4
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

		// Token: 0x060009B3 RID: 2483 RVA: 0x00093E18 File Offset: 0x00092018
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
			int num = Rms.loadRMSInt("lowGraphic");
			if (num == 1)
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

		// Token: 0x060009B4 RID: 2484 RVA: 0x00093F04 File Offset: 0x00092104
		protected void doRegister()
		{
			if (this.tfUser.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.userBlank);
				return;
			}
			char[] array = this.tfUser.getText().ToCharArray();
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
			if ((int)mResources.language == 2)
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
					long num2 = long.Parse(this.tfUser.getText());
					if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
					{
						text = mResources.phoneInvalid;
					}
					num = 1;
				}
				catch (Exception ex)
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

		// Token: 0x060009B5 RID: 2485 RVA: 0x000045ED File Offset: 0x000027ED
		protected void doRegister(string user)
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00007C58 File Offset: 0x00005E58
		protected int loadIndexServer()
		{
			return Rms.loadRMSInt("indServer");
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000045ED File Offset: 0x000027ED
		public void doLogin()
		{
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000045ED File Offset: 0x000027ED
		public void savePass()
		{
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00094174 File Offset: 0x00092374
		public override void update()
		{
			this.tfUser.update();
			this.tfSodt.update();
			this.tfNgay.update();
			this.tfThang.update();
			this.tfNam.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				effect.update();
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
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000943E4 File Offset: 0x000925E4
		private void doChangeTip()
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

		// Token: 0x060009BB RID: 2491 RVA: 0x0000864F File Offset: 0x0000684F
		public void updateLogo()
		{
			if (this.defYL != this.yL)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0009444C File Offset: 0x0009264C
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

		// Token: 0x060009BD RID: 2493 RVA: 0x00007C93 File Offset: 0x00005E93
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00094590 File Offset: 0x00092790
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
			if (ChatPopup.currChatPopup != null)
			{
				return;
			}
			if (ChatPopup.serverChatPopUp != null)
			{
				return;
			}
			if (GameCanvas.currentDialog == null)
			{
				int num2 = (GameCanvas.w < 200) ? 160 : 180;
				this.xLog = GameCanvas.hw - 120;
				int num3 = 110;
				this.yLog = (GameCanvas.h - num3) / 2;
				PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num3, -1, true);
				if (GameCanvas.h > 160 && RegisterScreen.imgTitle != null)
				{
					g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, num, 3);
				}
				GameCanvas.debug("PLG4", 1);
				int num4 = 4;
				int num5 = num4 * 32 + 23 + 33;
				if (num5 >= GameCanvas.w)
				{
					num4--;
					num5 = num4 * 32 + 23 + 33;
				}
				this.tfUser.x = this.xLog + 10;
				this.tfUser.y = this.yLog + 15;
				this.tfSodt.x = this.tfUser.x;
				this.tfSodt.y = this.tfUser.y + 30;
				this.tfNgay.x = this.xLog + 10;
				this.tfNgay.y = this.tfSodt.y + 30;
				this.tfThang.x = this.tfNgay.x + 75;
				this.tfThang.y = this.tfNgay.y;
				this.tfNam.x = this.tfThang.x + 75;
				this.tfNam.y = this.tfThang.y;
				mFont.tahoma_7b_focus.drawString(g, "Cập nhật thông tin", GameCanvas.hw, this.yLog + 2, 2);
				this.tfUser.paint(g);
				this.tfSodt.paint(g);
				this.tfNgay.paint(g);
				this.tfThang.paint(g);
				this.tfNam.paint(g);
			}
			GameCanvas.resetTrans(g);
			string version = GameMidlet.VERSION;
			g.setColor(GameCanvas.skyColor);
			g.fillRect(GameCanvas.w - 40, 4, 36, 11);
			mFont.tahoma_7_grey.drawString(g, version, GameCanvas.w - 22, 4, mFont.CENTER);
			g.drawImage(GameCanvas.img18, 10, 10, 0);
			base.paint(g);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00094850 File Offset: 0x00092A50
		private void turnOffFocus()
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

		// Token: 0x060009C0 RID: 2496 RVA: 0x000948CC File Offset: 0x00092ACC
		private void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUser.isFocus = true;
				break;
			case 1:
				this.tfNgay.isFocus = true;
				break;
			case 2:
				this.tfThang.isFocus = true;
				break;
			case 3:
				this.tfNam.isFocus = true;
				break;
			case 4:
				this.tfDiachi.isFocus = true;
				break;
			case 5:
				this.tfCMND.isFocus = true;
				break;
			case 6:
				this.tfNgayCap.isFocus = true;
				break;
			case 7:
				this.tfNoiCap.isFocus = true;
				break;
			case 8:
				this.tfSodt.isFocus = true;
				break;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000949B0 File Offset: 0x00092BB0
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

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000867E File Offset: 0x0000687E
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00094F5C File Offset: 0x0009315C
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
				break;
			case 1001:
				GameCanvas.endDlg();
				this.isRes = false;
				break;
			case 1002:
				break;
			case 1003:
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				break;
			case 1004:
				ServerListScreen.doUpdateServer();
				GameCanvas.serverScreen.switchToMe();
				break;
			case 1005:
				try
				{
					GameMidlet.instance.platformRequest("http://ngocrongonline.com");
				}
				catch (Exception ex2)
				{
					ex2.StackTrace.ToString();
				}
				break;
			default:
				switch (idAction)
				{
				case 2000:
					break;
				case 2001:
					if (this.isCheck)
					{
						this.isCheck = false;
					}
					else
					{
						this.isCheck = true;
					}
					break;
				case 2002:
					this.doRegister();
					break;
				case 2003:
					this.doMenu();
					break;
				case 2004:
					this.actRegister();
					break;
				default:
					if (idAction != 10041)
					{
						if (idAction != 10042)
						{
							if (idAction != 4000)
							{
								if (idAction == 10021)
								{
									this.actRegisterLeft();
								}
							}
							else
							{
								this.doRegister(this.tfUser.getText());
							}
						}
					}
					break;
				case 2008:
					if (this.tfNgay.getText().Equals(string.Empty) || this.tfThang.getText().Equals(string.Empty) || this.tfNam.getText().Equals(string.Empty) || this.tfSodt.getText().Equals(string.Empty) || this.tfUser.getText().Equals(string.Empty))
					{
						GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
					}
					else
					{
						GameCanvas.startOKDlg(mResources.PLEASEWAIT);
						Service.gI().charInfo(this.tfNgay.getText(), this.tfThang.getText(), this.tfNam.getText(), string.Empty, string.Empty, string.Empty, string.Empty, this.tfSodt.getText(), this.tfUser.getText());
					}
					break;
				}
				break;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00008688 File Offset: 0x00006888
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

		// Token: 0x060009C5 RID: 2501 RVA: 0x000086C7 File Offset: 0x000068C7
		public void actRegister()
		{
			GameCanvas.endDlg();
			GameCanvas.startOKDlg(mResources.regNote);
			this.isRes = true;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00095214 File Offset: 0x00093414
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

		// Token: 0x0400122D RID: 4653
		public TField tfUser;

		// Token: 0x0400122E RID: 4654
		public TField tfNgay;

		// Token: 0x0400122F RID: 4655
		public TField tfThang;

		// Token: 0x04001230 RID: 4656
		public TField tfNam;

		// Token: 0x04001231 RID: 4657
		public TField tfDiachi;

		// Token: 0x04001232 RID: 4658
		public TField tfCMND;

		// Token: 0x04001233 RID: 4659
		public TField tfNgayCap;

		// Token: 0x04001234 RID: 4660
		public TField tfNoiCap;

		// Token: 0x04001235 RID: 4661
		public TField tfSodt;

		// Token: 0x04001236 RID: 4662
		public static bool isContinueToLogin = false;

		// Token: 0x04001237 RID: 4663
		private int focus;

		// Token: 0x04001238 RID: 4664
		private int wC;

		// Token: 0x04001239 RID: 4665
		private int yL;

		// Token: 0x0400123A RID: 4666
		private int defYL;

		// Token: 0x0400123B RID: 4667
		public bool isCheck;

		// Token: 0x0400123C RID: 4668
		public bool isRes;

		// Token: 0x0400123D RID: 4669
		private Command cmdLogin;

		// Token: 0x0400123E RID: 4670
		private Command cmdCheck;

		// Token: 0x0400123F RID: 4671
		private Command cmdFogetPass;

		// Token: 0x04001240 RID: 4672
		private Command cmdRes;

		// Token: 0x04001241 RID: 4673
		private Command cmdMenu;

		// Token: 0x04001242 RID: 4674
		private Command cmdBackFromRegister;

		// Token: 0x04001243 RID: 4675
		public string listFAQ = string.Empty;

		// Token: 0x04001244 RID: 4676
		public string titleFAQ;

		// Token: 0x04001245 RID: 4677
		public string subtitleFAQ;

		// Token: 0x04001246 RID: 4678
		private string numSupport = string.Empty;

		// Token: 0x04001247 RID: 4679
		private string strUser;

		// Token: 0x04001248 RID: 4680
		private string strPass;

		// Token: 0x04001249 RID: 4681
		public static bool isLocal = false;

		// Token: 0x0400124A RID: 4682
		public static bool isUpdateAll;

		// Token: 0x0400124B RID: 4683
		public static bool isUpdateData;

		// Token: 0x0400124C RID: 4684
		public static bool isUpdateMap;

		// Token: 0x0400124D RID: 4685
		public static bool isUpdateSkill;

		// Token: 0x0400124E RID: 4686
		public static bool isUpdateItem;

		// Token: 0x0400124F RID: 4687
		public static string serverName;

		// Token: 0x04001250 RID: 4688
		public static Image imgTitle;

		// Token: 0x04001251 RID: 4689
		public int plX;

		// Token: 0x04001252 RID: 4690
		public int plY;

		// Token: 0x04001253 RID: 4691
		public int lY;

		// Token: 0x04001254 RID: 4692
		public int lX;

		// Token: 0x04001255 RID: 4693
		public int logoDes;

		// Token: 0x04001256 RID: 4694
		public int lineX;

		// Token: 0x04001257 RID: 4695
		public int lineY;

		// Token: 0x04001258 RID: 4696
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04001259 RID: 4697
		public static bool isTryGetIPFromWap;

		// Token: 0x0400125A RID: 4698
		public static short timeLogin;

		// Token: 0x0400125B RID: 4699
		public static long lastTimeLogin;

		// Token: 0x0400125C RID: 4700
		public static long currTimeLogin;

		// Token: 0x0400125D RID: 4701
		private int yt;

		// Token: 0x0400125E RID: 4702
		private Command cmdSelect;

		// Token: 0x0400125F RID: 4703
		private Command cmdOK;

		// Token: 0x04001260 RID: 4704
		private int xLog;

		// Token: 0x04001261 RID: 4705
		private int yLog;

		// Token: 0x04001262 RID: 4706
		private int xP;

		// Token: 0x04001263 RID: 4707
		private int yP;

		// Token: 0x04001264 RID: 4708
		private int wP;

		// Token: 0x04001265 RID: 4709
		private int hP;

		// Token: 0x04001266 RID: 4710
		private int tipid = -1;

		// Token: 0x04001267 RID: 4711
		public bool isLogin2;

		// Token: 0x04001268 RID: 4712
		private int v = 2;

		// Token: 0x04001269 RID: 4713
		private int g;

		// Token: 0x0400126A RID: 4714
		private int ylogo = -40;

		// Token: 0x0400126B RID: 4715
		private int dir = 1;

		// Token: 0x0400126C RID: 4716
		public static bool isLoggingIn;
	}
}
