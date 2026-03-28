using System;

// Token: 0x020000B0 RID: 176
public class InfoMe
{
	// Token: 0x060007E8 RID: 2024 RVA: 0x00071640 File Offset: 0x0006F840
	public InfoMe()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00007B67 File Offset: 0x00005D67
	public static InfoMe gI()
	{
		if (InfoMe.me == null)
		{
			InfoMe.me = new InfoMe();
		}
		return InfoMe.me;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00071698 File Offset: 0x0006F898
	public void loadCharId()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000716CC File Offset: 0x0006F8CC
	public void paint(mGraphics g)
	{
		if (this.Equals(GameScr.info2) && GameScr.gI().isVS())
		{
			return;
		}
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			return;
		}
		if (!GameScr.isPaint)
		{
			return;
		}
		if (GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI())
		{
			return;
		}
		if (ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (!this.isUpdate)
		{
			return;
		}
		if (global::Char.ischangingMap)
		{
			return;
		}
		if (GameCanvas.panel.isShow && this.Equals(GameScr.info2))
		{
			return;
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (this.info != null)
		{
			this.info.paint(g, this.cmx, this.cmy, this.dir);
			if (this.info.info == null || this.info.info.charInfo == null || this.cmdChat != null || !GameCanvas.isTouch)
			{
			}
			if (this.info.info == null || this.info.info.charInfo == null || this.cmdChat != null)
			{
			}
		}
		if (this.info.info != null && this.info.info.charInfo == null && this.charId != null)
		{
			SmallImage.drawSmallImage(g, this.charId[global::Char.myCharz().cgender][this.f], this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00007B82 File Offset: 0x00005D82
	public void hide()
	{
		this.info.hide();
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000718DC File Offset: 0x0006FADC
	public void moveCamera()
	{
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		if (this.cmx != this.cmtoX)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		this.tF++;
		if (this.tF == 5)
		{
			this.tF = 0;
			if (this.f == 0)
			{
				this.f = 1;
			}
			else
			{
				this.f = 0;
			}
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00007B8F File Offset: 0x00005D8F
	public void doClick(int t)
	{
		this.timeDelay = t;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000719E4 File Offset: 0x0006FBE4
	public void update()
	{
		if (this.info != null && this.info.infoWaitToShow != null && this.info.infoWaitToShow.size() == 0 && this.cmy != -40)
		{
			this.info.timeW--;
			if (this.info.timeW <= 0)
			{
				this.cmy = -40;
				this.info.time = 0;
				this.info.infoWaitToShow.removeAllElements();
				this.info.says = null;
				this.info.timeW = 200;
			}
		}
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			return;
		}
		if (!this.isUpdate)
		{
			return;
		}
		this.moveCamera();
		if (this.info == null)
		{
			return;
		}
		if (this.info != null && this.info.info == null)
		{
			return;
		}
		if (!this.isDone)
		{
			if (this.timeDelay > 0)
			{
				this.timeDelay--;
				if (this.timeDelay == 0)
				{
					GameCanvas.panel.setTypeMessage();
					GameCanvas.panel.show();
				}
			}
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					this.cmtoX = global::Char.myCharz().cx - 20 - GameScr.cmx;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					this.cmtoX = global::Char.myCharz().cx + 20 - GameScr.cmx;
				}
				if (this.cmtoX <= 24)
				{
					this.cmtoX += this.info.sayWidth / 2;
				}
				if (this.cmtoX >= GameCanvas.w - 24)
				{
					this.cmtoX -= this.info.sayWidth / 2;
				}
				this.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
				if (this.info.says != null && this.cmtoY < (this.info.says.Length + 1) * 12 + 10)
				{
					this.cmtoY = (this.info.says.Length + 1) * 12 + 10;
				}
				if (this.info.info.charInfo != null)
				{
					if (GameCanvas.w - 50 > 155 + this.info.W)
					{
						this.cmtoX = GameCanvas.w - 60 - this.info.W / 2;
						this.cmtoY = this.info.H + 10;
					}
					else
					{
						this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
						this.cmtoY = 45 + this.info.H;
						if (GameCanvas.w > GameCanvas.h || GameCanvas.w < 220)
						{
							this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
							this.cmtoY = this.info.H + 10;
						}
					}
				}
			}
			if (this.cmx > global::Char.myCharz().cx - GameScr.cmx)
			{
				this.dir = -1;
			}
			else
			{
				this.dir = 1;
			}
		}
		if (this.info.info != null)
		{
			if (this.info.infoWaitToShow.size() > 1)
			{
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeElementAt(0);
						InfoItem infoItem = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem;
						this.info.getInfo();
					}
				}
				else
				{
					this.info.info.curr = mSystem.currentTimeMillis();
					if (this.info.info.curr - this.info.info.last >= 1000L)
					{
						this.info.info.last = mSystem.currentTimeMillis();
						this.info.info.timeCount--;
					}
					if (this.info.info.timeCount == 0)
					{
						this.info.infoWaitToShow.removeElementAt(0);
						if (this.info.infoWaitToShow.size() == 0)
						{
							return;
						}
						InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem2;
						this.info.getInfo();
					}
				}
			}
			else if (this.info.infoWaitToShow.size() == 1)
			{
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.isDone = true;
					}
					if (this.info.time == this.info.info.speed)
					{
						this.cmtoY = -40;
						this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
					}
					if (this.info.time >= this.info.info.speed + 20)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						this.info.timeW = 200;
					}
				}
				else
				{
					this.info.info.curr = mSystem.currentTimeMillis();
					if (this.info.info.curr - this.info.info.last >= 1000L)
					{
						this.info.info.last = mSystem.currentTimeMillis();
						this.info.info.timeCount--;
					}
					if (this.info.info.timeCount == 0)
					{
						this.isDone = true;
						this.cmtoY = -40;
						this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						this.cmdChat = null;
					}
				}
			}
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00007B98 File Offset: 0x00005D98
	public void addInfoWithChar(string s, global::Char c, bool isChatServer)
	{
		this.playerID = c.charID;
		this.info.addInfo(s, 3, c, isChatServer);
		this.isDone = false;
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00072104 File Offset: 0x00070304
	public void addInfo(string s, int Type)
	{
		s = Res.changeString(s);
		if (this.info.infoWaitToShow.size() > 0 && s.Equals(((InfoItem)this.info.infoWaitToShow.lastElement()).s))
		{
			return;
		}
		if (this.info.infoWaitToShow.size() > 10)
		{
			for (int i = 0; i < 5; i++)
			{
				this.info.infoWaitToShow.removeElementAt(0);
			}
		}
		global::Char cInfo = null;
		this.info.addInfo(s, Type, cInfo, false);
		if (this.info.infoWaitToShow.size() == 1)
		{
			this.cmy = 0;
			this.cmx = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
		}
		this.isDone = false;
	}

	// Token: 0x04000F1B RID: 3867
	public static InfoMe me;

	// Token: 0x04000F1C RID: 3868
	public int[][] charId = new int[3][];

	// Token: 0x04000F1D RID: 3869
	public Info info = new Info();

	// Token: 0x04000F1E RID: 3870
	public int dir;

	// Token: 0x04000F1F RID: 3871
	public int f;

	// Token: 0x04000F20 RID: 3872
	public int tF;

	// Token: 0x04000F21 RID: 3873
	public int cmtoY;

	// Token: 0x04000F22 RID: 3874
	public int cmy;

	// Token: 0x04000F23 RID: 3875
	public int cmdy;

	// Token: 0x04000F24 RID: 3876
	public int cmvy;

	// Token: 0x04000F25 RID: 3877
	public int cmyLim;

	// Token: 0x04000F26 RID: 3878
	public int cmtoX;

	// Token: 0x04000F27 RID: 3879
	public int cmx;

	// Token: 0x04000F28 RID: 3880
	public int cmdx;

	// Token: 0x04000F29 RID: 3881
	public int cmvx;

	// Token: 0x04000F2A RID: 3882
	public int cmxLim;

	// Token: 0x04000F2B RID: 3883
	public bool isDone;

	// Token: 0x04000F2C RID: 3884
	public bool isUpdate = true;

	// Token: 0x04000F2D RID: 3885
	public int timeDelay;

	// Token: 0x04000F2E RID: 3886
	public int playerID;

	// Token: 0x04000F2F RID: 3887
	public int timeCount;

	// Token: 0x04000F30 RID: 3888
	public Command cmdChat;

	// Token: 0x04000F31 RID: 3889
	public bool isShow;
}
