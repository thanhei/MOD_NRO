using System;

// Token: 0x02000017 RID: 23
public class ChatPopup : Effect2, IActionListener
{
	// Token: 0x0600019D RID: 413 RVA: 0x00017B30 File Offset: 0x00015D30
	public static void addNextPopUpMultiLine(string strNext, Npc next)
	{
		ChatPopup.nextMultiChatPopUp = strNext;
		ChatPopup.nextChar = next;
		if (ChatPopup.currChatPopup == null)
		{
			ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
			ChatPopup.nextMultiChatPopUp = null;
			ChatPopup.nextChar = null;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00017B68 File Offset: 0x00015D68
	public static void addBigMessage(string chat, int howLong, Npc c)
	{
		string[] array = new string[] { chat };
		if (c.charID != 5 && GameScr.info1.isDone)
		{
			GameScr.info1.isUpdate = false;
		}
		global::Char.isLockKey = true;
		ChatPopup.serverChatPopUp = ChatPopup.addChatPopup(array[0], howLong, c);
		ChatPopup.serverChatPopUp.strY = 5;
		ChatPopup.serverChatPopUp.cx = GameCanvas.w / 2 - ChatPopup.serverChatPopUp.sayWidth / 2 - 1;
		ChatPopup.serverChatPopUp.cy = GameCanvas.h - 20 - ChatPopup.serverChatPopUp.ch;
		ChatPopup.serverChatPopUp.currentLine = 0;
		ChatPopup.serverChatPopUp.lines = array;
		ChatPopup.scr = new Scroll();
		int num = ChatPopup.serverChatPopUp.says.Length;
		ChatPopup.scr.setStyle(num, 12, ChatPopup.serverChatPopUp.cx, ChatPopup.serverChatPopUp.cy - ChatPopup.serverChatPopUp.strY + 12, ChatPopup.serverChatPopUp.sayWidth + 2, ChatPopup.serverChatPopUp.ch - 25, true, 1);
		SoundMn.gI().openDialog();
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00017C80 File Offset: 0x00015E80
	public static void addChatPopupMultiLine(string chat, int howLong, Npc c)
	{
		string[] array = Res.split(chat, "\n", 0);
		global::Char.isLockKey = true;
		ChatPopup.currChatPopup = ChatPopup.addChatPopup(array[0], howLong, c);
		ChatPopup.currChatPopup.currentLine = 0;
		ChatPopup.currChatPopup.lines = array;
		string text = mResources.CONTINUE;
		if (array.Length == 1)
		{
			text = mResources.CLOSE;
		}
		ChatPopup.currChatPopup.cmdNextLine = new Command(text, ChatPopup.currChatPopup, 8000, null);
		ChatPopup.currChatPopup.cmdNextLine.x = GameCanvas.w / 2 - 35;
		ChatPopup.currChatPopup.cmdNextLine.y = GameCanvas.h - 35;
		SoundMn.gI().openDialog();
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00017D2C File Offset: 0x00015F2C
	public static ChatPopup addChatPopupWithIcon(string chat, int howLong, Npc c, int idIcon)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
		if (chatPopup.sayWidth > 320)
		{
			chatPopup.sayWidth = 320;
		}
		if (chat.Length < 10)
		{
			chatPopup.sayWidth = 64;
		}
		if (GameCanvas.w == 128)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		chatPopup.iconID = idIcon;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		if (chatPopup.ch > GameCanvas.h - 80)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		if (GameCanvas.menu.showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		if (c != null && c.charID == 5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		chatPopup.ch += 15;
		return chatPopup;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00017E90 File Offset: 0x00016090
	public static ChatPopup addChatPopup(string chat, int howLong, Npc c)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
		if (chatPopup.sayWidth > 320)
		{
			chatPopup.sayWidth = 320;
		}
		if (chat.Length < 10)
		{
			chatPopup.sayWidth = 64;
		}
		if (GameCanvas.w == 128)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		if (chatPopup.ch > GameCanvas.h - 80)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		if (GameCanvas.menu.showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		if (c != null && c.charID == 5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		return chatPopup;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00017FDC File Offset: 0x000161DC
	public override void update()
	{
		if (ChatPopup.scr != null)
		{
			GameScr.info1.isUpdate = false;
			ChatPopup.scr.updatecm();
		}
		else
		{
			GameScr.info1.isUpdate = true;
		}
		if (GameCanvas.menu.showMenu)
		{
			this.strY = 0;
			this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
			this.cy = GameCanvas.menu.menuY - this.ch;
		}
		else
		{
			this.strY = 0;
			if (GameScr.gI().right != null || GameScr.gI().left != null || GameScr.gI().center != null || this.cmdNextLine != null || this.cmdMsg1 != null)
			{
				this.strY = 5;
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 20 - this.ch;
			}
			else
			{
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 5 - this.ch;
			}
		}
		if (this.delay > 0)
		{
			this.delay--;
		}
		if (ChatPopup.performDelay > 0)
		{
			ChatPopup.performDelay--;
		}
		else
		{
			GameScr.info1.info.time = 0;
			for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
			{
				if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed != 70)
				{
					((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
				}
			}
		}
		if (this.sayRun > 1)
		{
			this.sayRun--;
		}
		if ((this.c != null && global::Char.chatPopup != null && global::Char.chatPopup != this) || (this.c != null && global::Char.chatPopup == null) || this.delay <= 0)
		{
			Effect2.vEffect2Outside.removeElement(this);
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000181F4 File Offset: 0x000163F4
	public override void paint(mGraphics g)
	{
		if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
		{
			return;
		}
		GameCanvas.resetTrans(g);
		int num = this.cx;
		int num2 = this.cy;
		int num3 = this.sayWidth + 2;
		int num4 = this.ch;
		if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
		{
			return;
		}
		if (this.c != null)
		{
			int num5 = ((GameCanvas.gameTick % 10 <= 2) ? 1 : 0);
			SmallImage.drawSmallImage(g, this.c.avatar, this.cx + 14, this.cy + num5, 0, StaticObj.BOTTOM_LEFT);
		}
		if (this.iconID != 0)
		{
			int num5 = ((GameCanvas.gameTick % 10 <= 2) ? 1 : 0);
			SmallImage.drawSmallImage(g, this.iconID, this.cx + num3 / 2, this.cy + this.ch - 15 + num5, 0, StaticObj.VCENTER_HCENTER);
		}
		PopUp.paintPopUp(g, num, num2, num3, num4, 16777215, false);
		if (ChatPopup.scr != null)
		{
			g.setClip(num, num2, num3, num4 - 16);
			g.translate(0, -ChatPopup.scr.cmy);
		}
		int num6 = 0;
		int num7 = 0;
		if (this.isClip)
		{
			num6 = g.getTranslateX();
			num7 = g.getTranslateY();
			g.setClip(num, num2 + 1, num3, num4 - 17);
			g.translate(0, -ChatPopup.cmyText);
		}
		int num8 = -1;
		for (int i = 0; i < this.says.Length; i++)
		{
			if (this.says[i].StartsWith("--"))
			{
				g.setColor(0);
				g.fillRect(num + 10, this.cy + this.sayRun + i * 12 + 6, num3 - 20, 1);
			}
			else
			{
				mFont mFont = mFont.tahoma_7;
				int num9 = 2;
				string text = this.says[i];
				int num10;
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
						num9 = int.Parse(array[2]);
					}
					num10 = int.Parse(array[1]);
					num8 = num10;
				}
				else
				{
					num10 = num8;
				}
				switch (num10)
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
				case 8:
					mFont = mFont.tahoma_7b_yellow;
					break;
				}
				if (this.says[i].StartsWith("<"))
				{
					string[] array2 = Res.split(Res.split(this.says[i], "<", 0)[1], ">", 1);
					if (this.second == 0)
					{
						this.second = int.Parse(array2[1]);
					}
					else
					{
						ChatPopup.curr = mSystem.currentTimeMillis();
						if (ChatPopup.curr - ChatPopup.last >= 1000L)
						{
							ChatPopup.last = ChatPopup.curr;
							this.second--;
						}
					}
					mFont.drawString(g, this.second.ToString() + " " + array2[2], this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num9);
				}
				else
				{
					if (num9 == 2)
					{
						mFont.drawString(g, text, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num9);
					}
					if (num9 == 1)
					{
						mFont.drawString(g, text, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY + 12, num9);
					}
				}
			}
		}
		if (this.isClip)
		{
			GameCanvas.resetTrans(g);
			g.translate(num6, num7);
		}
		if (this.maxStarSlot > 4)
		{
			this.nMaxslot_tren = (int)((this.maxStarSlot + 1) / 2);
			this.nMaxslot_duoi = (int)this.maxStarSlot - this.nMaxslot_tren;
			int[] array3 = new int[(int)this.maxStarSlot];
			int[] array4 = new int[(int)this.maxStarSlot];
			for (int j = 0; j < this.nMaxslot_tren; j++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 17, 3);
				array3[j] = num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar);
				array4[j] = num2 + num4 - 17;
			}
			for (int k = 0; k < this.nMaxslot_duoi; k++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 8, 3);
				array3[this.nMaxslot_tren + k] = num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar);
				array4[this.nMaxslot_tren + k] = num2 + num4 - 8;
			}
			Res.err(this.maxStarSlot.ToString() + "maxStarSlot");
			if (this.maxStarSlot >= 7)
			{
				for (int l = 7; l < (int)this.maxStarSlot; l++)
				{
					if (this.starCuongHoa[l])
					{
						g.drawImage(Panel.imgStarCuongHoa, array3[l], array4[l], 3);
					}
				}
			}
			if (this.starSlot > 0)
			{
				this.imgStar = Panel.imgStar;
				if ((int)this.starSlot >= this.nMaxslot_tren)
				{
					this.nslot_duoi = (int)this.starSlot - this.nMaxslot_tren;
					for (int m = 0; m < this.nMaxslot_tren; m++)
					{
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + m * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
					}
					for (int n = 0; n < this.nslot_duoi; n++)
					{
						if (n + this.nMaxslot_tren >= ChatPopup.numSlot)
						{
							this.imgStar = Panel.imgStar8;
						}
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + n * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 8, 3);
					}
				}
				else
				{
					for (int num11 = 0; num11 < (int)this.starSlot; num11++)
					{
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + num11 * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
					}
				}
			}
		}
		else
		{
			for (int num12 = 0; num12 < (int)this.maxStarSlot; num12++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num12 * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 13, 3);
			}
			if (this.starSlot > 0)
			{
				for (int num13 = 0; num13 < (int)this.starSlot; num13++)
				{
					g.drawImage(Panel.imgStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num13 * 20 + mGraphics.getImageWidth(Panel.imgStar), num2 + num4 - 13, 3);
				}
			}
		}
		this.paintCmd(g);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000189AC File Offset: 0x00016BAC
	public void paintRada(mGraphics g, int cmyText)
	{
		int num = this.cx;
		int num2 = this.cy;
		int num3 = this.sayWidth;
		int num4 = this.ch;
		int translateX = g.getTranslateX();
		int translateY = g.getTranslateY();
		g.translate(0, -cmyText);
		if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
		{
			return;
		}
		int num5 = -1;
		for (int i = 0; i < this.says.Length; i++)
		{
			if (this.says[i].StartsWith("--"))
			{
				g.setColor(16777215);
				g.fillRect(num + 10, this.cy + this.sayRun + i * 12 - 6, num3 - 20, 1);
			}
			else
			{
				mFont mFont = mFont.tahoma_7_white;
				int num6 = 2;
				string text = this.says[i];
				int num7;
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
						num6 = int.Parse(array[2]);
					}
					num7 = int.Parse(array[1]);
					num5 = num7;
				}
				else
				{
					num7 = num5;
				}
				switch (num7)
				{
				case -1:
					mFont = mFont.tahoma_7_white;
					break;
				case 0:
					mFont = mFont.tahoma_7b_white;
					break;
				case 1:
					mFont = mFont.tahoma_7b_green;
					break;
				case 2:
					mFont = mFont.tahoma_7b_red;
					break;
				}
				if (this.says[i].StartsWith("<"))
				{
					string[] array2 = Res.split(Res.split(this.says[i], "<", 0)[1], ">", 1);
					if (this.second == 0)
					{
						this.second = int.Parse(array2[1]);
					}
					else
					{
						ChatPopup.curr = mSystem.currentTimeMillis();
						if (ChatPopup.curr - ChatPopup.last >= 1000L)
						{
							ChatPopup.last = ChatPopup.curr;
							this.second--;
						}
					}
					mFont.drawString(g, this.second.ToString() + " " + array2[2], this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
				}
				else
				{
					if (num6 == 2)
					{
						mFont.drawString(g, text, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
					}
					if (num6 == 1)
					{
						mFont.drawString(g, text, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY, num6);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		g.translate(translateX, translateY);
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00018C84 File Offset: 0x00016E84
	internal void doKeyText(int type)
	{
		ChatPopup.cmyText += 12 * type;
		if (ChatPopup.cmyText < 0)
		{
			ChatPopup.cmyText = 0;
		}
		if (ChatPopup.cmyText > this.lim)
		{
			ChatPopup.cmyText = this.lim;
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00018CBC File Offset: 0x00016EBC
	public void updateKey()
	{
		if (this.isClip)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.doKeyText(1);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.doKeyText(-1);
			}
			if (GameCanvas.isPointerHoldIn(this.cx, 0, this.sayWidth + 2, this.ch))
			{
				if (GameCanvas.isPointerMove)
				{
					if (this.pyy == 0)
					{
						this.pyy = GameCanvas.py;
					}
					this.pxx = this.pyy - GameCanvas.py;
					if (this.pxx != 0)
					{
						ChatPopup.cmyText += this.pxx;
						this.pyy = GameCanvas.py;
					}
					if (ChatPopup.cmyText < 0)
					{
						ChatPopup.cmyText = 0;
					}
					if (ChatPopup.cmyText > this.lim)
					{
						ChatPopup.cmyText = this.lim;
					}
				}
				else
				{
					this.pyy = 0;
					this.pyy = 0;
				}
			}
		}
		if (ChatPopup.scr != null)
		{
			if (GameCanvas.isTouch)
			{
				ChatPopup.scr.updateKey();
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				ChatPopup.scr.cmtoY -= 12;
				if (ChatPopup.scr.cmtoY < 0)
				{
					ChatPopup.scr.cmtoY = 0;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				ChatPopup.scr.cmtoY += 12;
				if (ChatPopup.scr.cmtoY > ChatPopup.scr.cmyLim)
				{
					ChatPopup.scr.cmtoY = ChatPopup.scr.cmyLim;
				}
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			if (this.cmdNextLine != null)
			{
				this.cmdNextLine.performAction();
			}
			else if (this.cmdMsg1 != null)
			{
				this.cmdMsg1.performAction();
			}
			else if (this.cmdMsg2 != null)
			{
				this.cmdMsg2.performAction();
			}
		}
		if (ChatPopup.scr == null || !ChatPopup.scr.pointerIsDowning)
		{
			if (this.cmdMsg1 != null && (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.cmdMsg1)))
			{
				GameCanvas.keyPressed[12] = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerJustRelease = false;
				this.cmdMsg1.performAction();
				mScreen.keyTouch = -1;
			}
			if (this.cmdMsg2 != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.cmdMsg2)))
			{
				GameCanvas.keyPressed[13] = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerJustRelease = false;
				this.cmdMsg2.performAction();
				mScreen.keyTouch = -1;
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00018FD8 File Offset: 0x000171D8
	public void paintCmd(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		if (this.cmdNextLine != null)
		{
			GameCanvas.paintz.paintCmdBar(g, null, this.cmdNextLine, null);
		}
		if (this.cmdMsg1 != null)
		{
			GameCanvas.paintz.paintCmdBar(g, this.cmdMsg1, null, this.cmdMsg2);
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00019054 File Offset: 0x00017254
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception)
			{
			}
			if (!Main.isPC)
			{
				GameMidlet.instance.notifyDestroyed();
			}
			else
			{
				idAction = 1001;
			}
			GameCanvas.endDlg();
		}
		if (idAction == 1001)
		{
			ChatPopup.scr = null;
			global::Char.chatPopup = null;
			ChatPopup.serverChatPopUp = null;
			GameScr.info1.isUpdate = true;
			global::Char.isLockKey = false;
			if (ChatPopup.isHavePetNpc)
			{
				GameScr.info1.info.time = 0;
				GameScr.info1.info.info.speed = 10;
			}
		}
		if (idAction != 8000 || ChatPopup.performDelay > 0)
		{
			return;
		}
		int num = ChatPopup.currChatPopup.currentLine + 1;
		if (num < ChatPopup.currChatPopup.lines.Length)
		{
			ChatPopup chatPopup = ChatPopup.addChatPopup(ChatPopup.currChatPopup.lines[num], ChatPopup.currChatPopup.delay, ChatPopup.currChatPopup.c);
			chatPopup.currentLine = num;
			chatPopup.lines = ChatPopup.currChatPopup.lines;
			chatPopup.cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
			ChatPopup.currChatPopup = chatPopup;
			return;
		}
		global::Char.chatPopup = null;
		ChatPopup.currChatPopup = null;
		GameScr.info1.isUpdate = true;
		global::Char.isLockKey = false;
		if (ChatPopup.nextMultiChatPopUp != null)
		{
			ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
			ChatPopup.nextMultiChatPopUp = null;
			ChatPopup.nextChar = null;
			return;
		}
		if (!ChatPopup.isHavePetNpc)
		{
			return;
		}
		GameScr.info1.info.time = 0;
		for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
		{
			if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed == 10000000)
			{
				((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
			}
		}
	}

	// Token: 0x040002E4 RID: 740
	public int sayWidth = 100;

	// Token: 0x040002E5 RID: 741
	public int delay;

	// Token: 0x040002E6 RID: 742
	public int sayRun;

	// Token: 0x040002E7 RID: 743
	public string[] says;

	// Token: 0x040002E8 RID: 744
	public int cx;

	// Token: 0x040002E9 RID: 745
	public int cy;

	// Token: 0x040002EA RID: 746
	public int ch;

	// Token: 0x040002EB RID: 747
	public int cmx;

	// Token: 0x040002EC RID: 748
	public int cmy;

	// Token: 0x040002ED RID: 749
	public int lim;

	// Token: 0x040002EE RID: 750
	public Npc c;

	// Token: 0x040002EF RID: 751
	internal bool outSide;

	// Token: 0x040002F0 RID: 752
	public static long curr;

	// Token: 0x040002F1 RID: 753
	public static long last;

	// Token: 0x040002F2 RID: 754
	internal int currentLine;

	// Token: 0x040002F3 RID: 755
	internal string[] lines;

	// Token: 0x040002F4 RID: 756
	public Command cmdNextLine;

	// Token: 0x040002F5 RID: 757
	public Command cmdMsg1;

	// Token: 0x040002F6 RID: 758
	public Command cmdMsg2;

	// Token: 0x040002F7 RID: 759
	public static ChatPopup currChatPopup;

	// Token: 0x040002F8 RID: 760
	public static ChatPopup serverChatPopUp;

	// Token: 0x040002F9 RID: 761
	public static string nextMultiChatPopUp;

	// Token: 0x040002FA RID: 762
	public static Npc nextChar;

	// Token: 0x040002FB RID: 763
	public bool isShopDetail;

	// Token: 0x040002FC RID: 764
	public sbyte starSlot;

	// Token: 0x040002FD RID: 765
	public sbyte maxStarSlot;

	// Token: 0x040002FE RID: 766
	public static Scroll scr;

	// Token: 0x040002FF RID: 767
	public static bool isHavePetNpc;

	// Token: 0x04000300 RID: 768
	public int mH;

	// Token: 0x04000301 RID: 769
	public static int performDelay;

	// Token: 0x04000302 RID: 770
	public int dx;

	// Token: 0x04000303 RID: 771
	public int dy;

	// Token: 0x04000304 RID: 772
	public int second;

	// Token: 0x04000305 RID: 773
	internal Point[] saoPoint = new Point[14];

	// Token: 0x04000306 RID: 774
	internal int indexStar;

	// Token: 0x04000307 RID: 775
	internal int indexStar2;

	// Token: 0x04000308 RID: 776
	public bool[] starCuongHoa = new bool[20];

	// Token: 0x04000309 RID: 777
	public static int numSlot = 7;

	// Token: 0x0400030A RID: 778
	internal int nMaxslot_duoi;

	// Token: 0x0400030B RID: 779
	internal int nMaxslot_tren;

	// Token: 0x0400030C RID: 780
	internal int nslot_duoi;

	// Token: 0x0400030D RID: 781
	internal Image imgStar;

	// Token: 0x0400030E RID: 782
	public int strY;

	// Token: 0x0400030F RID: 783
	internal int iconID;

	// Token: 0x04000310 RID: 784
	public bool isClip;

	// Token: 0x04000311 RID: 785
	public static int cmyText;

	// Token: 0x04000312 RID: 786
	internal int pxx;

	// Token: 0x04000313 RID: 787
	internal int pyy;
}
