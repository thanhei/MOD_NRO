using System;

// Token: 0x0200006A RID: 106
public class Menu
{
	// Token: 0x06000539 RID: 1337 RVA: 0x00052363 File Offset: 0x00050563
	public static void loadBg()
	{
		Menu.imgMenu1 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu1.png");
		Menu.imgMenu2 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu2.png");
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00052383 File Offset: 0x00050583
	public void startWithoutCloseButton(MyVector menuItems, int pos)
	{
		this.startAt(menuItems, pos);
		this.disableClose = true;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00052394 File Offset: 0x00050594
	public void startAt(MyVector menuItems, int x, int y)
	{
		this.startAt(menuItems, 0);
		this.menuX = x;
		this.menuY = y;
		while (this.menuY + this.menuH > GameCanvas.h)
		{
			this.menuY -= 2;
		}
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x000523D0 File Offset: 0x000505D0
	public void startAt(MyVector menuItems, int pos)
	{
		if (this.showMenu)
		{
			return;
		}
		this.isClose = false;
		this.touch = false;
		this.close = false;
		this.tDelay = 0;
		if (menuItems.size() == 1)
		{
			this.menuSelectedItem = 0;
			Command command = (Command)menuItems.elementAt(0);
			if (command != null && command.caption.Equals(mResources.saying))
			{
				command.performAction();
				this.showMenu = false;
				InfoDlg.showWait();
				return;
			}
		}
		SoundMn.gI().openMenu();
		this.isNotClose = new bool[menuItems.size()];
		for (int i = 0; i < this.isNotClose.Length; i++)
		{
			this.isNotClose[i] = false;
		}
		this.disableClose = false;
		ChatPopup.currChatPopup = null;
		Effect2.vEffect2.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		InfoDlg.hide();
		if (menuItems.size() != 0)
		{
			this.menuItems = menuItems;
			this.menuW = 60;
			this.menuH = 60;
			for (int j = 0; j < menuItems.size(); j++)
			{
				Command command2 = (Command)menuItems.elementAt(j);
				command2.isPlaySoundButton = false;
				mFont.tahoma_7_yellow.getWidth(command2.caption);
				command2.subCaption = mFont.tahoma_7_yellow.splitFontArray(command2.caption, this.menuW - 10);
			}
			Menu.menuTemY = new int[menuItems.size()];
			this.menuX = (GameCanvas.w - menuItems.size() * this.menuW) / 2;
			if (this.menuX < 1)
			{
				this.menuX = 1;
			}
			this.menuY = GameCanvas.h - this.menuH - (Paint.hTab + 1) - 1;
			if (GameCanvas.isTouch)
			{
				this.menuY -= 3;
			}
			this.menuY += 27;
			for (int k = 0; k < Menu.menuTemY.Length; k++)
			{
				Menu.menuTemY[k] = GameCanvas.h;
			}
			this.showMenu = true;
			this.menuSelectedItem = 0;
			Menu.cmxLim = this.menuItems.size() * this.menuW - GameCanvas.w;
			if (Menu.cmxLim < 0)
			{
				Menu.cmxLim = 0;
			}
			Menu.cmtoX = 0;
			Menu.cmx = 0;
			Menu.xc = 50;
			this.w = menuItems.size() * this.menuW - 1;
			if (this.w > GameCanvas.w - 2)
			{
				this.w = GameCanvas.w - 2;
			}
			if (GameCanvas.isTouch && !Main.isPC)
			{
				this.menuSelectedItem = -1;
			}
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x0005264C File Offset: 0x0005084C
	public bool isScrolling()
	{
		return (!this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] > this.menuY) || (this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] < GameCanvas.h);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0005269C File Offset: 0x0005089C
	public void updateMenuKey()
	{
		if ((GameScr.gI().activeRongThan && GameScr.gI().isUseFreez) || !this.showMenu || this.isScrolling())
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			flag = true;
			this.menuSelectedItem--;
			if (this.menuSelectedItem < 0)
			{
				this.menuSelectedItem = this.menuItems.size() - 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
		{
			flag = true;
			this.menuSelectedItem++;
			if (this.menuSelectedItem > this.menuItems.size() - 1)
			{
				this.menuSelectedItem = 0;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
		{
			if (this.center != null)
			{
				if (this.center.idAction > 0)
				{
					if (this.center.actionListener == GameScr.gI())
					{
						GameScr.gI().actionPerform(this.center.idAction, this.center.p);
					}
					else
					{
						this.perform(this.center.idAction, this.center.p);
					}
				}
			}
			else
			{
				this.waitToPerform = 2;
			}
		}
		else if (GameCanvas.keyPressed[12] && !GameScr.gI().isRongThanMenu())
		{
			if (this.isScrolling())
			{
				return;
			}
			if (this.left.idAction > 0)
			{
				this.perform(this.left.idAction, this.left.p);
			}
			else
			{
				this.waitToPerform = 2;
			}
			SoundMn.gI().buttonClose();
		}
		else if (!GameScr.gI().isRongThanMenu() && !this.disableClose && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			if (this.isScrolling())
			{
				return;
			}
			if (!this.close)
			{
				this.close = true;
			}
			this.isClose = true;
			SoundMn.gI().buttonClose();
		}
		if (flag)
		{
			Menu.cmtoX = this.menuSelectedItem * this.menuW + this.menuW - GameCanvas.w / 2;
			if (Menu.cmtoX > Menu.cmxLim)
			{
				Menu.cmtoX = Menu.cmxLim;
			}
			if (Menu.cmtoX < 0)
			{
				Menu.cmtoX = 0;
			}
			if (this.menuSelectedItem == this.menuItems.size() - 1 || this.menuSelectedItem == 0)
			{
				Menu.cmx = Menu.cmtoX;
			}
		}
		bool flag2 = true;
		if (GameCanvas.panel.cp != null && GameCanvas.panel.cp.isClip)
		{
			if (!GameCanvas.isPointerHoldIn(GameCanvas.panel.cp.cx, 0, GameCanvas.panel.cp.sayWidth + 2, GameCanvas.panel.cp.ch))
			{
				flag2 = true;
			}
			else
			{
				flag2 = false;
				GameCanvas.panel.cp.updateKey();
			}
		}
		if (!this.disableClose && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH) && !this.pointerIsDowning && !GameScr.gI().isRongThanMenu() && flag2)
		{
			if (!this.isScrolling())
			{
				this.closeMenu();
			}
			return;
		}
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.isDownWhenRunning = this.cmRun != 0;
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
					this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
				}
				int num = GameCanvas.px - this.pointerDownLastX[0];
				if (num != 0 && this.menuSelectedItem != -1)
				{
					this.menuSelectedItem = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.px;
				Menu.cmtoX -= num;
				if (Menu.cmtoX < 0)
				{
					Menu.cmtoX = 0;
				}
				if (Menu.cmtoX > Menu.cmxLim)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				if (Menu.cmx < 0 || Menu.cmx > Menu.cmxLim)
				{
					num /= 2;
				}
				Menu.cmx -= num;
				if (Menu.cmx < -(GameCanvas.h / 3))
				{
					this.wantUpdateList = true;
				}
				else
				{
					this.wantUpdateList = false;
				}
			}
		}
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			int num2 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(num2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				Menu.cmtoX = Menu.cmx;
				this.pointerDownFirstX = -1000;
				this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
				this.pointerDownTime = 0;
				this.waitToPerform = 10;
			}
			else if (this.menuSelectedItem != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 1;
			}
			else if (this.menuSelectedItem == -1 && !this.isDownWhenRunning)
			{
				if (Menu.cmx < 0)
				{
					Menu.cmtoX = 0;
				}
				else if (Menu.cmx > Menu.cmxLim)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				else
				{
					int num3 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					this.cmRun = -((num3 > 10) ? 10 : ((num3 < -10) ? (-10) : 0)) * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00052D20 File Offset: 0x00050F20
	public void moveCamera()
	{
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			Menu.cmtoX += this.cmRun / 100;
			if (Menu.cmtoX < 0)
			{
				Menu.cmtoX = 0;
			}
			else if (Menu.cmtoX > Menu.cmxLim)
			{
				Menu.cmtoX = Menu.cmxLim;
			}
			else
			{
				Menu.cmx = Menu.cmtoX;
			}
			this.cmRun = this.cmRun * 9 / 10;
			if (this.cmRun < 100 && this.cmRun > -100)
			{
				this.cmRun = 0;
			}
		}
		if (Menu.cmx != Menu.cmtoX && !this.pointerIsDowning)
		{
			this.cmvx = Menu.cmtoX - Menu.cmx << 2;
			this.cmdx += this.cmvx;
			Menu.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00052E0C File Offset: 0x0005100C
	public void paintMenu(mGraphics g)
	{
		if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
		{
			return;
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-Menu.cmx, 0);
		for (int i = 0; i < this.menuItems.size(); i++)
		{
			if (i == this.menuSelectedItem)
			{
				g.drawImage(Menu.imgMenu2, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
			}
			else
			{
				g.drawImage(Menu.imgMenu1, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
			}
			Command command = (Command)this.menuItems.elementAt(i);
			string[] array = command.subCaption;
			if (array == null)
			{
				array = new string[] { ((Command)this.menuItems.elementAt(i)).caption };
			}
			int num = Menu.menuTemY[i] + (this.menuH - array.Length * 14) / 2 + 1;
			for (int j = 0; j < array.Length; j++)
			{
				if (i == this.menuSelectedItem)
				{
					mFont.tahoma_7b_green2.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
				}
				else if (command.isDisplay)
				{
					mFont.tahoma_7b_red.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
				}
				else
				{
					mFont.tahoma_7b_dark.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00052FF8 File Offset: 0x000511F8
	public void doCloseMenu()
	{
		Res.outz("CLOSE MENU");
		this.isClose = false;
		this.showMenu = false;
		InfoDlg.hide();
		if (this.close)
		{
			GameCanvas.panel.cp = null;
			global::Char.chatPopup = null;
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
				return;
			}
		}
		else
		{
			if (!this.touch)
			{
				return;
			}
			GameCanvas.panel.cp = null;
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
			if (this.menuSelectedItem >= 0)
			{
				Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
				if (command != null)
				{
					SoundMn.gI().buttonClose();
					command.performAction();
				}
			}
		}
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x000530BF File Offset: 0x000512BF
	public void performSelect()
	{
		InfoDlg.hide();
		if (this.menuSelectedItem >= 0)
		{
			Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
			if (command == null)
			{
				return;
			}
			command.performAction();
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x000530F0 File Offset: 0x000512F0
	public void updateMenu()
	{
		this.moveCamera();
		if (!this.isClose)
		{
			this.tDelay++;
			for (int i = 0; i < Menu.menuTemY.Length; i++)
			{
				if (Menu.menuTemY[i] > this.menuY)
				{
					int num = Menu.menuTemY[i] - this.menuY >> 1;
					if (num < 1)
					{
						num = 1;
					}
					if (this.tDelay > i)
					{
						Menu.menuTemY[i] -= num;
					}
				}
			}
			if (Menu.menuTemY[Menu.menuTemY.Length - 1] <= this.menuY)
			{
				this.tDelay = 0;
			}
		}
		else
		{
			this.tDelay++;
			for (int j = 0; j < Menu.menuTemY.Length; j++)
			{
				if (Menu.menuTemY[j] < GameCanvas.h)
				{
					int num2 = (GameCanvas.h - Menu.menuTemY[j] >> 1) + 2;
					if (num2 < 1)
					{
						num2 = 1;
					}
					if (this.tDelay > j)
					{
						Menu.menuTemY[j] += num2;
					}
				}
			}
			if (Menu.menuTemY[Menu.menuTemY.Length - 1] >= GameCanvas.h)
			{
				this.tDelay = 0;
				this.doCloseMenu();
			}
		}
		if (Menu.xc != 0)
		{
			Menu.xc >>= 1;
			if (Menu.xc < 0)
			{
				Menu.xc = 0;
			}
		}
		if (this.isScrolling() || this.waitToPerform <= 0)
		{
			return;
		}
		this.waitToPerform--;
		if (this.waitToPerform == 0)
		{
			if (this.menuSelectedItem >= 0 && !this.isNotClose[this.menuSelectedItem])
			{
				this.isClose = true;
				this.touch = true;
				GameCanvas.panel.cp = null;
				return;
			}
			this.performSelect();
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00004887 File Offset: 0x00002A87
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00053298 File Offset: 0x00051498
	internal void closeMenu()
	{
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		GameCanvas.clearAllPointerEvent();
		Res.outz("menu select= " + this.menuSelectedItem.ToString());
		this.isClose = true;
		this.close = true;
		SoundMn.gI().buttonClose();
	}

	// Token: 0x04000B11 RID: 2833
	public bool showMenu;

	// Token: 0x04000B12 RID: 2834
	public MyVector menuItems;

	// Token: 0x04000B13 RID: 2835
	public int menuSelectedItem;

	// Token: 0x04000B14 RID: 2836
	public int menuX;

	// Token: 0x04000B15 RID: 2837
	public int menuY;

	// Token: 0x04000B16 RID: 2838
	public int menuW;

	// Token: 0x04000B17 RID: 2839
	public int menuH;

	// Token: 0x04000B18 RID: 2840
	public static int[] menuTemY;

	// Token: 0x04000B19 RID: 2841
	public static int cmtoX;

	// Token: 0x04000B1A RID: 2842
	public static int cmx;

	// Token: 0x04000B1B RID: 2843
	public static int cmdy;

	// Token: 0x04000B1C RID: 2844
	public static int cmvy;

	// Token: 0x04000B1D RID: 2845
	public static int cmxLim;

	// Token: 0x04000B1E RID: 2846
	public static int xc;

	// Token: 0x04000B1F RID: 2847
	internal Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000B20 RID: 2848
	internal Command right = new Command(mResources.CLOSE, 0, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH + 1);

	// Token: 0x04000B21 RID: 2849
	internal Command center;

	// Token: 0x04000B22 RID: 2850
	public static Image imgMenu1;

	// Token: 0x04000B23 RID: 2851
	public static Image imgMenu2;

	// Token: 0x04000B24 RID: 2852
	internal bool disableClose;

	// Token: 0x04000B25 RID: 2853
	public int tDelay;

	// Token: 0x04000B26 RID: 2854
	public int w;

	// Token: 0x04000B27 RID: 2855
	internal int pa;

	// Token: 0x04000B28 RID: 2856
	internal bool trans;

	// Token: 0x04000B29 RID: 2857
	internal int pointerDownTime;

	// Token: 0x04000B2A RID: 2858
	internal int pointerDownFirstX;

	// Token: 0x04000B2B RID: 2859
	internal int[] pointerDownLastX = new int[3];

	// Token: 0x04000B2C RID: 2860
	internal bool pointerIsDowning;

	// Token: 0x04000B2D RID: 2861
	internal bool isDownWhenRunning;

	// Token: 0x04000B2E RID: 2862
	internal bool wantUpdateList;

	// Token: 0x04000B2F RID: 2863
	internal int waitToPerform;

	// Token: 0x04000B30 RID: 2864
	internal int cmRun;

	// Token: 0x04000B31 RID: 2865
	internal bool touch;

	// Token: 0x04000B32 RID: 2866
	internal bool close;

	// Token: 0x04000B33 RID: 2867
	internal int cmvx;

	// Token: 0x04000B34 RID: 2868
	internal int cmdx;

	// Token: 0x04000B35 RID: 2869
	internal bool isClose;

	// Token: 0x04000B36 RID: 2870
	public bool[] isNotClose;
}
