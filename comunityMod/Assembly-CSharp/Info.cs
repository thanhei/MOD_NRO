using System;

// Token: 0x0200004D RID: 77
public class Info : IActionListener
{
	// Token: 0x06000466 RID: 1126 RVA: 0x0004B4ED File Offset: 0x000496ED
	public void hide()
	{
		this.says = null;
		this.infoWaitToShow.removeAllElements();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0004B504 File Offset: 0x00049704
	public void paint(mGraphics g, int x, int y, int dir)
	{
		if (this.infoWaitToShow.size() == 0)
		{
			return;
		}
		g.translate(x, y);
		if (this.says != null && this.says.Length != 0 && this.type != 1)
		{
			if (this.outSide)
			{
				this.cx -= GameScr.cmx;
				this.cy -= GameScr.cmy;
				this.cy += 35;
			}
			int num = -5;
			if (this.info.charInfo == null)
			{
				PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H, 16777215, false);
			}
			else
			{
				mSystem.paintPopUp2(g, this.X - 23, this.Y - num / 2, this.W + 15, this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num);
			}
			if (this.info.charInfo == null)
			{
				g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : (-15)), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
			}
			int num2 = -1;
			for (int i = 0; i < this.says.Length; i++)
			{
				mFont mFont = mFont.tahoma_7;
				string text = this.says[i];
				int num3;
				if (this.says[i].StartsWith("|"))
				{
					string[] array = Res.split(this.says[i], "|", 0);
					if (array.Length == 3)
					{
						text = array[2];
					}
					if (array.Length == 4)
					{
						text = array[3];
						int.Parse(array[2]);
					}
					num3 = int.Parse(array[1]);
					num2 = num3;
				}
				else
				{
					num3 = num2;
				}
				switch (num3)
				{
				case -1:
					mFont = mFont.tahoma_7;
					break;
				case 0:
					mFont = mFont.tahoma_7b_dark;
					break;
				case 1:
					mFont = mFont.tahoma_7b_green;
					break;
				case 2:
					mFont = mFont.tahoma_7b_blue;
					break;
				case 3:
					mFont = mFont.tahoma_7_red;
					break;
				case 4:
					mFont = mFont.tahoma_7_green;
					break;
				case 5:
					mFont = mFont.tahoma_7_blue;
					break;
				case 7:
					mFont = mFont.tahoma_7b_red;
					break;
				}
				if (this.info.charInfo == null)
				{
					mFont.drawString(g, text, this.cx, this.cy - this.ch - 15 + this.sayRun + i * 12 - this.says.Length * 12 - 9, 2);
				}
				else
				{
					int num4 = this.X - 23;
					int num5 = this.Y - num / 2;
					int num6 = ((mSystem.clientType != 1) ? (this.W + 25) : (this.W + 28));
					int num7 = this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num;
					g.setColor(4465169);
					g.fillRect(num4, num5 + num7, num6, 2);
					int num8 = this.info.timeCount * num6 / this.info.maxTime;
					if (num8 < 0)
					{
						num8 = 0;
					}
					g.setColor(43758);
					g.fillRect(num4, num5 + num7, num8, 2);
					if (this.info.timeCount == 0)
					{
						return;
					}
					this.info.charInfo.paintHead(g, this.X + 5, this.Y + this.H / 2, 0);
					((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y + 3, 0);
					if (!GameCanvas.isTouch)
					{
						if (!TField.isQwerty)
						{
							mFont.tahoma_7b_green2Small.drawString(g, "Nhấn # để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7b_green2Small.drawString(g, "Nhấn Y để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
						}
					}
					TextInfo.paint(g, text, this.X + 14, this.Y + this.H / 2 + 2, this.W - 16, this.H, mFont.tahoma_7_whiteSmall);
				}
			}
		}
		g.translate(-x, -y);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0004B970 File Offset: 0x00049B70
	public void update()
	{
		if (this.infoWaitToShow.size() == 0 || this.info.timeCount != 0)
		{
			return;
		}
		this.time++;
		if (this.time >= this.info.speed)
		{
			this.time = 0;
			this.infoWaitToShow.removeElementAt(0);
			if (this.infoWaitToShow.size() != 0)
			{
				this.info = (InfoItem)this.infoWaitToShow.firstElement();
				this.getInfo();
			}
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0004B9F8 File Offset: 0x00049BF8
	public void getInfo()
	{
		this.sayWidth = 100;
		if (GameCanvas.w == 128)
		{
			this.sayWidth = 128;
		}
		int num;
		if (this.info.charInfo != null)
		{
			this.says = new string[] { this.info.s };
			num = this.says.Length;
		}
		else
		{
			this.says = mFont.tahoma_7.splitFontArray(this.info.s, this.sayWidth - 10);
			num = this.says.Length;
		}
		this.sayRun = 7;
		this.X = this.cx - this.sayWidth / 2 - 1;
		this.Y = this.cy - this.ch - 15 + this.sayRun - num * 12 - 15;
		this.W = this.sayWidth + 2 + ((this.info.charInfo != null) ? 30 : 0);
		this.H = (num + 1) * 12 + 1 + ((this.info.charInfo != null) ? 5 : 0);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0004BB08 File Offset: 0x00049D08
	public void addInfo(string s, int Type, global::Char cInfo, bool isChatServer)
	{
		this.type = Type;
		if (GameCanvas.w == 128)
		{
			this.limLeft = 1;
		}
		if (this.infoWaitToShow.size() > 10)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		if (this.infoWaitToShow.size() > 0)
		{
			s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s);
		}
		InfoItem infoItem = new InfoItem(s);
		if (this.type == 0)
		{
			infoItem.speed = s.Length;
		}
		if (infoItem.speed < 70)
		{
			infoItem.speed = 70;
		}
		if (this.type == 1)
		{
			infoItem.speed = 10000000;
		}
		if (this.type == 3)
		{
			infoItem.speed = 300;
			infoItem.last = mSystem.currentTimeMillis();
			infoItem.timeCount = s.Length;
			if (infoItem.timeCount < 15)
			{
				infoItem.timeCount = 15;
			}
			if (infoItem.timeCount > 100)
			{
				infoItem.timeCount = 100;
			}
			infoItem.maxTime = infoItem.timeCount;
		}
		if (cInfo != null)
		{
			infoItem.charInfo = cInfo;
			infoItem.isChatServer = isChatServer;
			GameCanvas.panel.addChatMessage(infoItem);
			if (GameCanvas.isTouch && GameCanvas.panel.isViewChatServer)
			{
				GameScr.info2.cmdChat = new Command(mResources.CHAT, this, 1000, infoItem);
			}
		}
		if ((cInfo != null && GameCanvas.panel.isViewChatServer) || cInfo == null)
		{
			this.infoWaitToShow.addElement(infoItem);
		}
		if (this.infoWaitToShow.size() == 1)
		{
			this.info = (InfoItem)this.infoWaitToShow.firstElement();
			this.getInfo();
		}
		if (GameCanvas.isTouch && cInfo != null && GameCanvas.panel.isViewChatServer && GameCanvas.w - 50 > 155 + this.W)
		{
			GameScr.info2.cmdChat.x = GameCanvas.w - this.W - 50;
			GameScr.info2.cmdChat.y = 35;
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0004BD00 File Offset: 0x00049F00
	public void addInfo(string s, int speed, mFont f)
	{
		if (GameCanvas.w == 128)
		{
			this.limLeft = 1;
		}
		if (this.infoWaitToShow.size() > 10)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		this.infoWaitToShow.addElement(new InfoItem(s, f, speed));
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0004BD4E File Offset: 0x00049F4E
	public bool isEmpty()
	{
		return this.p1 == 5 && this.infoWaitToShow.size() == 0;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0004BD69 File Offset: 0x00049F69
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00004887 File Offset: 0x00002A87
	public void onCancelChat()
	{
	}

	// Token: 0x04000909 RID: 2313
	public MyVector infoWaitToShow = new MyVector();

	// Token: 0x0400090A RID: 2314
	public InfoItem info;

	// Token: 0x0400090B RID: 2315
	public int p1 = 5;

	// Token: 0x0400090C RID: 2316
	public int p2;

	// Token: 0x0400090D RID: 2317
	public int p3;

	// Token: 0x0400090E RID: 2318
	public int x;

	// Token: 0x0400090F RID: 2319
	public int strWidth;

	// Token: 0x04000910 RID: 2320
	public int limLeft = 2;

	// Token: 0x04000911 RID: 2321
	public int hI = 20;

	// Token: 0x04000912 RID: 2322
	public int xChar;

	// Token: 0x04000913 RID: 2323
	public int yChar;

	// Token: 0x04000914 RID: 2324
	public int sayWidth = 100;

	// Token: 0x04000915 RID: 2325
	public int sayRun;

	// Token: 0x04000916 RID: 2326
	public string[] says;

	// Token: 0x04000917 RID: 2327
	public int cx;

	// Token: 0x04000918 RID: 2328
	public int cy;

	// Token: 0x04000919 RID: 2329
	public int ch;

	// Token: 0x0400091A RID: 2330
	public bool outSide;

	// Token: 0x0400091B RID: 2331
	public int f;

	// Token: 0x0400091C RID: 2332
	public int tF;

	// Token: 0x0400091D RID: 2333
	public Image img;

	// Token: 0x0400091E RID: 2334
	public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

	// Token: 0x0400091F RID: 2335
	public int time;

	// Token: 0x04000920 RID: 2336
	public int timeW;

	// Token: 0x04000921 RID: 2337
	public int type;

	// Token: 0x04000922 RID: 2338
	public int X;

	// Token: 0x04000923 RID: 2339
	public int Y;

	// Token: 0x04000924 RID: 2340
	public int W;

	// Token: 0x04000925 RID: 2341
	public int H;
}
