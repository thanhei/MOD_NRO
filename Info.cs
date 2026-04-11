using System;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x02000060 RID: 96
public class Info : IActionListener
{
	// Token: 0x06000376 RID: 886 RVA: 0x00005BAA File Offset: 0x00003DAA
	public void hide()
	{
		this.says = null;
		this.infoWaitToShow.removeAllElements();
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000221D4 File Offset: 0x000203D4
	public void paint(mGraphics g, int x, int y, int dir)
	{
		if (this.infoWaitToShow.size() == 0 || MainMod.hideServerChat)
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
			int num = (mGraphics.zoomLevel != 1) ? 10 : 0;
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
				g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : -15), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
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
					int num6 = (mSystem.clientType != 1) ? (this.W + 25) : (this.W + 28);
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
			global::Char charInfo = this.info.charInfo;
		}
		g.translate(-x, -y);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0002265C File Offset: 0x0002085C
	public void update()
	{
		if (this.infoWaitToShow.size() != 0 && this.info.timeCount == 0)
		{
			this.time++;
			if (this.time >= this.info.speed)
			{
				this.time = 0;
				this.infoWaitToShow.removeElementAt(0);
				if (this.infoWaitToShow.size() == 0)
				{
					return;
				}
				InfoItem infoItem = (InfoItem)this.infoWaitToShow.firstElement();
				this.info = infoItem;
				this.getInfo();
			}
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000226F4 File Offset: 0x000208F4
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
			this.says = new string[]
			{
				this.info.s
			};
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
		this.W = this.sayWidth + 2 + ((this.info.charInfo == null) ? 0 : 30);
		this.H = (num + 1) * 12 + 1 + ((this.info.charInfo == null) ? 0 : 5);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00022804 File Offset: 0x00020A04
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
		if (this.infoWaitToShow.size() <= 0 || s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s))
		{
		}
		InfoItem infoItem = new InfoItem(s);
		if (this.type == 0)
		{
			infoItem.speed = s.Length * 2;
		}
		if (infoItem.speed < 70)
		{
			infoItem.speed = 140;
		}
		if (this.type == 1)
		{
			infoItem.speed = 10000000;
		}
		if (this.type == 3)
		{
			infoItem.speed = 600;
			infoItem.last = mSystem.currentTimeMillis();
			infoItem.timeCount = s.Length / 3;
			if (infoItem.timeCount < 5)
			{
				infoItem.timeCount = 5;
			}
			if (infoItem.timeCount > 33)
			{
				infoItem.timeCount = 33;
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

	// Token: 0x0600037B RID: 891 RVA: 0x00022A3C File Offset: 0x00020C3C
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

	// Token: 0x0600037C RID: 892 RVA: 0x00005BBE File Offset: 0x00003DBE
	public bool isEmpty()
	{
		return this.p1 == 5 && this.infoWaitToShow.size() == 0;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00005BDD File Offset: 0x00003DDD
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
		}
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000045ED File Offset: 0x000027ED
	public void onCancelChat()
	{
	}

	// Token: 0x04000581 RID: 1409
	public MyVector infoWaitToShow = new MyVector();

	// Token: 0x04000582 RID: 1410
	public InfoItem info;

	// Token: 0x04000583 RID: 1411
	public int p1 = 5;

	// Token: 0x04000584 RID: 1412
	public int p2;

	// Token: 0x04000585 RID: 1413
	public int p3;

	// Token: 0x04000586 RID: 1414
	public int x;

	// Token: 0x04000587 RID: 1415
	public int strWidth;

	// Token: 0x04000588 RID: 1416
	public int limLeft = 2;

	// Token: 0x04000589 RID: 1417
	public int hI = 20;

	// Token: 0x0400058A RID: 1418
	public int xChar;

	// Token: 0x0400058B RID: 1419
	public int yChar;

	// Token: 0x0400058C RID: 1420
	public int sayWidth = 100;

	// Token: 0x0400058D RID: 1421
	public int sayRun;

	// Token: 0x0400058E RID: 1422
	public string[] says;

	// Token: 0x0400058F RID: 1423
	public int cx;

	// Token: 0x04000590 RID: 1424
	public int cy;

	// Token: 0x04000591 RID: 1425
	public int ch;

	// Token: 0x04000592 RID: 1426
	public bool outSide;

	// Token: 0x04000593 RID: 1427
	public int f;

	// Token: 0x04000594 RID: 1428
	public int tF;

	// Token: 0x04000595 RID: 1429
	public Image img;

	// Token: 0x04000596 RID: 1430
	public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

	// Token: 0x04000597 RID: 1431
	public int time;

	// Token: 0x04000598 RID: 1432
	public int timeW;

	// Token: 0x04000599 RID: 1433
	public int type;

	// Token: 0x0400059A RID: 1434
	public int X;

	// Token: 0x0400059B RID: 1435
	public int Y;

	// Token: 0x0400059C RID: 1436
	public int W;

	// Token: 0x0400059D RID: 1437
	public int H;
}
