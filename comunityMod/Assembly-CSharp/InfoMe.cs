using System;

// Token: 0x02000050 RID: 80
public class InfoMe
{
	// Token: 0x0600047A RID: 1146 RVA: 0x0004BF9C File Offset: 0x0004A19C
	public InfoMe()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0004BFEE File Offset: 0x0004A1EE
	public static InfoMe gI()
	{
		if (InfoMe.me == null)
		{
			InfoMe.me = new InfoMe();
		}
		return InfoMe.me;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0004C008 File Offset: 0x0004A208
	public void loadCharId()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0004C038 File Offset: 0x0004A238
	public void paint(mGraphics g)
	{
		if ((this.Equals(GameScr.info2) && GameScr.gI().isVS()) || (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || (!GameScr.isPaint || (GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI())) || ChatPopup.serverChatPopUp != null || !this.isUpdate || global::Char.ischangingMap || (GameCanvas.panel.isShow && this.Equals(GameScr.info2)))
		{
			return;
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (this.info != null)
		{
			this.info.paint(g, this.cmx, this.cmy, this.dir);
			if (this.info.info != null && this.info.info.charInfo != null && this.cmdChat == null)
			{
				bool isTouch = GameCanvas.isTouch;
			}
			if (this.info.info != null && this.info.info.charInfo != null)
			{
				Command command = this.cmdChat;
			}
		}
		if (this.info.info != null && this.info.info.charInfo == null && this.charId != null)
		{
			SmallImage.drawSmallImage(g, this.charId[global::Char.myCharz().cgender][this.f], this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0004C1EE File Offset: 0x0004A3EE
	public void hide()
	{
		this.info.hide();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0004C1FC File Offset: 0x0004A3FC
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
				return;
			}
			this.f = 0;
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
	public void doClick(int t)
	{
		this.timeDelay = t;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0004C2FC File Offset: 0x0004A4FC
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
		if ((this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || !this.isUpdate)
		{
			return;
		}
		this.moveCamera();
		if (this.info == null || (this.info != null && this.info.info == null))
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
		if (this.info.info == null)
		{
			return;
		}
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
				return;
			}
			this.info.info.curr = mSystem.currentTimeMillis();
			if (this.info.info.curr - this.info.info.last >= 100L)
			{
				this.info.info.last = mSystem.currentTimeMillis();
				this.info.info.timeCount--;
			}
			if (this.info.info.timeCount == 0)
			{
				this.info.infoWaitToShow.removeElementAt(0);
				if (this.info.infoWaitToShow.size() != 0)
				{
					InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
					this.info.info = infoItem2;
					this.info.getInfo();
					return;
				}
			}
		}
		else
		{
			if (this.info.infoWaitToShow.size() != 1)
			{
				return;
			}
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
					this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : (-20));
				}
				if (this.info.time >= this.info.info.speed + 20)
				{
					this.info.time = 0;
					this.info.infoWaitToShow.removeAllElements();
					this.info.says = null;
					this.info.timeW = 200;
					return;
				}
			}
			else
			{
				this.info.info.curr = mSystem.currentTimeMillis();
				if (this.info.info.curr - this.info.info.last >= 100L)
				{
					this.info.info.last = mSystem.currentTimeMillis();
					this.info.info.timeCount--;
				}
				if (this.info.info.timeCount == 0)
				{
					this.isDone = true;
					this.cmtoY = -40;
					this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : (-20));
					this.info.time = 0;
					this.info.infoWaitToShow.removeAllElements();
					this.info.says = null;
					this.cmdChat = null;
				}
			}
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0004C997 File Offset: 0x0004AB97
	public void addInfoWithChar(string s, global::Char c, bool isChatServer)
	{
		this.playerID = c.charID;
		this.info.addInfo(s, 3, c, isChatServer);
		this.isDone = false;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0004C9BC File Offset: 0x0004ABBC
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
		this.info.addInfo(s, Type, null, false);
		if (this.info.infoWaitToShow.size() == 1)
		{
			this.cmy = 0;
			this.cmx = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : (-20));
		}
		this.isDone = false;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0002E1CB File Offset: 0x0002C3CB
	internal void addInfo(object disableAutoChat, int v)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000935 RID: 2357
	public static InfoMe me;

	// Token: 0x04000936 RID: 2358
	public int[][] charId = new int[3][];

	// Token: 0x04000937 RID: 2359
	public Info info = new Info();

	// Token: 0x04000938 RID: 2360
	public int dir;

	// Token: 0x04000939 RID: 2361
	public int f;

	// Token: 0x0400093A RID: 2362
	public int tF;

	// Token: 0x0400093B RID: 2363
	public int cmtoY;

	// Token: 0x0400093C RID: 2364
	public int cmy;

	// Token: 0x0400093D RID: 2365
	public int cmdy;

	// Token: 0x0400093E RID: 2366
	public int cmvy;

	// Token: 0x0400093F RID: 2367
	public int cmyLim;

	// Token: 0x04000940 RID: 2368
	public int cmtoX;

	// Token: 0x04000941 RID: 2369
	public int cmx;

	// Token: 0x04000942 RID: 2370
	public int cmdx;

	// Token: 0x04000943 RID: 2371
	public int cmvx;

	// Token: 0x04000944 RID: 2372
	public int cmxLim;

	// Token: 0x04000945 RID: 2373
	public bool isDone;

	// Token: 0x04000946 RID: 2374
	public bool isUpdate = true;

	// Token: 0x04000947 RID: 2375
	public int timeDelay;

	// Token: 0x04000948 RID: 2376
	public int playerID;

	// Token: 0x04000949 RID: 2377
	public int timeCount;

	// Token: 0x0400094A RID: 2378
	public Command cmdChat;

	// Token: 0x0400094B RID: 2379
	public bool isShow;
}
