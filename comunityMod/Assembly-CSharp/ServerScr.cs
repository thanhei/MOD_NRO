using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class ServerScr : mScreen, IActionListener
{
	// Token: 0x060007ED RID: 2029 RVA: 0x0007A1E8 File Offset: 0x000783E8
	public ServerScr()
	{
		TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0007A2C4 File Offset: 0x000784C4
	public override void switchToMe()
	{
		Debug.LogError(">>>>>>switchToMe: ");
		SoundMn.gI().stopAll();
		base.switchToMe();
		this.Load_NewUI();
		if (!this.isPaintNewUi && !this.isChooseArea)
		{
			this.cmdGlobal = new Command(this.strArea[0], this, 98, null);
			this.cmdGlobal.x = 0;
			this.cmdGlobal.y = 0;
			this.cmdVietNam = new Command(this.strArea[1], this, 97, null);
			this.cmdVietNam.x = 50;
			this.cmdVietNam.y = 0;
			this.vecServer = new MyVector();
			this.vecServer.addElement(this.cmdGlobal);
			this.vecServer.addElement(this.cmdVietNam);
			this.sort();
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0007A39C File Offset: 0x0007859C
	internal void sort()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 2;
		if (this.vecServer.size() > 2)
		{
			this.numw = GameCanvas.w / (this.wc + this.w2c);
		}
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (command != null)
			{
				int num = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2 + i % this.numw * (this.wc + this.w2c);
				int num2 = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2 + i / this.numw * (this.hc + this.w2c);
				command.x = num;
				command.y = num2;
				command.w = this.wc;
			}
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0007A4E4 File Offset: 0x000786E4
	internal void sort_newUI()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 1;
		int num = this.xsub + this.wsub / 2 + 3;
		this.ysubpaint = this.ysub + 5;
		this.numw = this.wsub / (this.wc + this.w2c);
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		this.xsubpaint = num - this.numw * (this.wc + this.w2c) / 2;
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (command != null)
			{
				int num2 = this.xsubpaint + i % this.numw * (this.wc + this.w2c);
				int num3 = this.ysubpaint + i / this.numw * (this.hc + this.w2c);
				command.x = num2;
				command.y = num3;
				command.w = this.wc;
			}
		}
		this.list = new ListNew(this.xsub, this.ysub, this.wsub, this.hsub, 0, 0, 0, true);
		this.list.setMaxCamera(this.numh * (this.hc + this.w2c) - this.hsub);
		this.list.resetList();
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0007A684 File Offset: 0x00078884
	public override void update()
	{
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (!this.isPaintNewUi)
		{
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				Command command = (Command)this.vecServer.elementAt(i);
				if (!GameCanvas.isTouch)
				{
					if (i == this.mainSelect)
					{
						if (GameCanvas.gameTick % 10 < 4)
						{
							command.isFocus = true;
						}
						else
						{
							command.isFocus = false;
						}
						this.cmdCheck = new Command(mResources.SELECT, this, command.idAction, null);
						this.center = this.cmdCheck;
					}
					else
					{
						command.isFocus = false;
					}
				}
				else if (command != null && command.isPointerPressInside())
				{
					command.performAction();
				}
			}
		}
		this.UpdTouch_NewUI();
		this.UpdTouch_NewUI_Popup();
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0007A768 File Offset: 0x00078968
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		if (this.isChooseArea)
		{
			this.paintChooseArea(g);
		}
		else if (this.isPaintNewUi)
		{
			this.paintNewSelectMenu(g);
		}
		else
		{
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				if (this.vecServer.elementAt(i) != null)
				{
					((Command)this.vecServer.elementAt(i)).paint(g);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0007A7E0 File Offset: 0x000789E0
	public override void updateKey()
	{
		base.updateKey();
		int num = this.mainSelect % ((this.numw == 0) ? 1 : this.numw);
		int num2 = this.mainSelect / ((this.numw == 0) ? 1 : this.numw);
		if (GameCanvas.keyPressed[4])
		{
			if (num > 0)
			{
				this.mainSelect--;
			}
			GameCanvas.keyPressed[4] = false;
		}
		else if (GameCanvas.keyPressed[6])
		{
			if (num < this.numw - 1)
			{
				this.mainSelect++;
			}
			GameCanvas.keyPressed[6] = false;
		}
		else if (GameCanvas.keyPressed[2])
		{
			if (num2 > 0)
			{
				this.mainSelect -= this.numw;
			}
			GameCanvas.keyPressed[2] = false;
		}
		else if (GameCanvas.keyPressed[8])
		{
			if (num2 < this.numh - 1)
			{
				this.mainSelect += this.numw;
			}
			GameCanvas.keyPressed[8] = false;
		}
		if (this.mainSelect < 0)
		{
			this.mainSelect = 0;
		}
		if (this.mainSelect >= this.vecServer.size())
		{
			this.mainSelect = this.vecServer.size() - 1;
		}
		if (GameCanvas.keyPressed[5])
		{
			((Command)this.vecServer.elementAt(num)).performAction();
			GameCanvas.keyPressed[5] = false;
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0007A934 File Offset: 0x00078B34
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 97:
			if (!this.isPaintNewUi)
			{
				this.vecServer.removeAllElements();
				for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
				{
					if (ServerListScreen.language[i] != 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[i], this, 100 + i, null));
					}
				}
				this.sort();
				return;
			}
			break;
		case 98:
			if (!this.isPaintNewUi)
			{
				this.vecServer.removeAllElements();
				for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
				{
					if (ServerListScreen.language[j] == 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[j], this, 100 + j, null));
					}
				}
				this.sort();
				return;
			}
			break;
		case 99:
			Session_ME.gI().clearSendingMessage();
			ServerListScreen.ipSelect = this.mainSelect;
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		default:
			if (idAction == 999)
			{
				this.Save_RMS_Area();
				this.SetNewSelectMenu((int)this.select_Area, 0);
				return;
			}
			ServerListScreen.ipSelect = idAction - 100;
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			return;
		}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0007AA60 File Offset: 0x00078C60
	internal void SetNewSelectMenu(int area, int typeSv)
	{
		this.isChooseArea = false;
		this.isPaintNewUi = true;
		this.w = GameCanvas.w / 3 * 2;
		this.h = GameCanvas.h / 3 * 2;
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = (GameCanvas.h - this.h) / 2 + 20;
		this.xName = GameCanvas.w / 2;
		this.yName = this.y - 30;
		this.wsub = this.w / 3 * 2;
		this.wPop = this.w - this.wsub - 15;
		if (this.wPop < 80)
		{
			this.wPop = 80;
			this.wsub = this.w - this.wPop - 15;
		}
		this.hsub = this.h - 10;
		this.xsub = this.x + this.w - this.wsub - 5;
		this.ysub = this.y + 5;
		this.xPop = this.x + 5;
		this.yPop = this.y + 5;
		this.hPop = 20;
		this.xinfo = this.x + 5;
		this.yinfo = this.y + this.strTypeSV.Length * (this.hPop + 5) + 5;
		this.winfo = this.wPop;
		this.hinfo = this.h - (5 + this.strTypeSV.Length * (this.hPop + 5) + 5);
		this.yBox = 10;
		this.wBox = 70;
		this.hBox = 20;
		this.GetVecTypeSv((sbyte)area, (sbyte)typeSv);
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0007AC0C File Offset: 0x00078E0C
	internal void GetVecTypeSv(sbyte area, sbyte typeSv)
	{
		this.vecServer.removeAllElements();
		this.ntypeSv = 1;
		this.select_Area = area;
		mResources.loadLanguague(area);
		for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
		{
			if (area == 1)
			{
				if (ServerListScreen.language[i] != 0 && ServerListScreen.typeSv[i] == 1)
				{
					this.ntypeSv = 2;
				}
			}
			else if (ServerListScreen.typeSv[i] == 1)
			{
				this.ntypeSv = 2;
			}
		}
		if (typeSv > (sbyte)(this.ntypeSv - 1))
		{
			typeSv = (sbyte)(this.ntypeSv - 1);
		}
		this.select_typeSv = typeSv;
		for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
		{
			if (area == 1)
			{
				if (ServerListScreen.language[j] != 0)
				{
					if (ServerListScreen.typeSv[j] == 1)
					{
						this.ntypeSv = 2;
					}
					if (ServerListScreen.typeSv[j] == typeSv)
					{
						Command command = new Command(ServerListScreen.nameServer[j], this, 100 + j, null);
						command.isPaintNew = ServerListScreen.isNew[j] == 1;
						this.vecServer.addElement(command);
					}
				}
			}
			else
			{
				if (ServerListScreen.typeSv[j] == 1)
				{
					this.ntypeSv = 2;
				}
				if (ServerListScreen.language[j] == 0 && ServerListScreen.typeSv[j] == typeSv)
				{
					Command command2 = new Command(ServerListScreen.nameServer[j], this, 100 + j, null);
					command2.isPaintNew = ServerListScreen.isNew[j] == 1;
					this.vecServer.addElement(command2);
				}
			}
		}
		this.Sort_NewSv();
		this.sort_newUI();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0007AD70 File Offset: 0x00078F70
	internal void paintChooseArea(mGraphics g)
	{
		if (this.isChooseArea)
		{
			this.paint_Area(g, GameCanvas.hw - this.wBox / 2, this.yBox);
			this.paint_Lang(g, GameCanvas.hw + 20, this.yBox);
			this.cmdChooseArea.paint(g);
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0007ADC4 File Offset: 0x00078FC4
	internal void paintNewSelectMenu(mGraphics g)
	{
		if (!this.isPaintNewUi)
		{
			return;
		}
		g.setColor(14601141);
		g.fillRect(this.x, this.y, this.w, this.h);
		PopUp.paintPopUp(g, this.xName - 50, this.yName, 100, 20, 0, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.selectServer2, this.xName, this.yName + 5, 2);
		for (int i = 0; i < this.ntypeSv; i++)
		{
			int num = this.yPop + i * (this.hPop + 5);
			PopUp.paintPopUp(g, this.xPop, num, this.wPop, this.hPop, ((int)this.select_typeSv == i) ? 1 : 0, true);
			mFont.tahoma_7b_dark.drawString(g, this.strTypeSV[i], this.xPop + this.wPop / 2, num + 5, 2);
		}
		g.setColor(10254674);
		g.fillRect(this.xinfo, this.yinfo, this.winfo, this.hinfo);
		string[] array = mFont.tahoma_7.splitFontArray(this.strTypeSV_info[(int)this.select_typeSv], this.winfo - 10);
		for (int j = 0; j < array.Length; j++)
		{
			mFont.tahoma_7_white.drawString(g, array[j], this.xinfo + 5, this.yinfo + 5 + j * 11, 0);
		}
		this.paint_Area(g, 10, this.yBox);
		this.paint_Lang(g, GameCanvas.w - this.wBox - 10, this.yBox);
		g.setColor(10254674);
		g.fillRect(this.xsub, this.ysub, this.wsub, this.hsub);
		g.setClip(this.xsub, this.ysub, this.wsub, this.hsub);
		g.translate(0, -this.list.cmx);
		for (int k = 0; k < this.vecServer.size(); k++)
		{
			Command command = (Command)this.vecServer.elementAt(k);
			if (command != null)
			{
				command.paint(g);
				if (command.isPaintNew && GameCanvas.gameTick % 10 > 1)
				{
					g.drawImage(Panel.imgNew, command.x + 60, command.y, 0);
				}
			}
		}
		GameCanvas.resetTrans(g);
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0007B020 File Offset: 0x00079220
	internal void paint_Area(mGraphics g, int x, int y)
	{
		this.xPopUp_Area = x;
		PopUp.paintPopUp(g, x, y, this.wBox, this.hBox, 0, true);
		mFont.tahoma_7b_dark.drawString(g, this.strArea[(int)this.select_Area], x + (this.wBox - 10) / 2, y + 5, 2);
		g.drawRegion(Mob.imgHP, 0, 30, 9, 6, 0, x + this.wBox - 10, y + 14, mGraphics.BOTTOM | mGraphics.HCENTER);
		if (!this.isPaint_select_area)
		{
			return;
		}
		this.yPopUp_Area = y + this.hBox + 5;
		g.setColor(10254674);
		g.fillRect(x, this.yPopUp_Area, this.wBox, this.strArea.Length * this.htext + 1);
		for (int i = 0; i < this.strArea.Length; i++)
		{
			mFont.tahoma_7_white.drawString(g, this.strArea[i], x + this.wBox / 2, this.yPopUp_Area + i * this.htext + 2, 2);
			if ((int)this.select_Area == i)
			{
				g.setColor(15591444);
				g.drawRect(x + 2, this.yPopUp_Area + i * this.htext + 1, this.wBox - 4, this.htext - 2);
			}
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paint_Lang(mGraphics g, int x, int y)
	{
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0007B168 File Offset: 0x00079368
	internal void UpdTouch_NewUI()
	{
		if (!this.isPaintNewUi)
		{
			return;
		}
		int num = 0;
		if (this.list != null)
		{
			this.list.moveCamera();
			if (GameCanvas.isPointer(this.xsub, 0, this.wsub, GameCanvas.h))
			{
				this.list.update_Pos_UP_DOWN();
			}
			num = this.list.cmx;
		}
		if (GameCanvas.isPointSelect(this.xsub, this.ysub, this.wsub, this.hsub))
		{
			int num2 = (GameCanvas.px - this.xsubpaint) / (this.wc + this.w2c) + (GameCanvas.py - this.ysubpaint + num) / (this.hc + this.w2c) * this.numw;
			int num3 = this.vecServer.size();
			if (num2 >= 0 && num2 < num3)
			{
				this.mainSelect = num2;
				Command command = (Command)this.vecServer.elementAt(this.mainSelect);
				if (command != null)
				{
					command.isFocus = true;
					command.performAction();
				}
			}
		}
		if (this.ntypeSv == 1)
		{
			return;
		}
		sbyte b = 0;
		while ((int)b < this.ntypeSv)
		{
			int num4 = this.yPop + (int)b * (this.hPop + 5);
			if (GameCanvas.isPointerHoldIn(this.xPop, num4, this.wPop, this.hPop) && GameCanvas.isPointerDown)
			{
				this.GetVecTypeSv(this.select_Area, b);
				return;
			}
			b += 1;
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0007B2CC File Offset: 0x000794CC
	internal void UpdTouch_NewUI_Popup()
	{
		if (GameCanvas.isPointer(this.xPopUp_Area, this.yBox, this.wBox, this.hBox) && GameCanvas.isPointerJustRelease)
		{
			this.isPaint_select_area = !this.isPaint_select_area;
			this.isPaint_select_lang = false;
			GameCanvas.isPointerJustRelease = false;
		}
		if (!this.isPaint_select_area)
		{
			return;
		}
		sbyte b = 0;
		while ((int)b < this.strArea.Length)
		{
			int num = this.yPopUp_Area + (int)b * this.htext;
			if (GameCanvas.isPointerHoldIn(this.xPopUp_Area, num, this.wBox, this.htext) && GameCanvas.isPointerDown)
			{
				if (this.isChooseArea)
				{
					this.select_Area = b;
				}
				else
				{
					this.SetNewSelectMenu((int)b, (int)this.select_typeSv);
				}
				this.isPaint_select_lang = (this.isPaint_select_area = false);
				return;
			}
			b += 1;
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0007B398 File Offset: 0x00079598
	internal void Load_NewUI()
	{
		if (GameCanvas.isTouch)
		{
			if (Rms.loadRMS("area_select") == null)
			{
				this.isChooseArea = true;
				this.cmdChooseArea = new Command(mResources.OK, this, 999, null);
				this.cmdChooseArea.x = GameCanvas.hw - 38;
				this.cmdChooseArea.y = GameCanvas.hh + 50;
				this.vecServer = new MyVector();
				this.vecServer.addElement(this.cmdChooseArea);
				this.yBox = GameCanvas.hh - 30;
				this.wBox = 70;
				this.hBox = 20;
				return;
			}
			this.isChooseArea = false;
			this.Load_RMS_Area();
			this.SetNewSelectMenu((int)this.select_Area, (int)this.select_typeSv);
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0007B459 File Offset: 0x00079659
	internal void Save_RMS_Area()
	{
		Rms.saveRMS("area_select", new sbyte[] { this.select_Area, this.select_Lang });
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0007B480 File Offset: 0x00079680
	internal void Load_RMS_Area()
	{
		sbyte[] array = Rms.loadRMS("area_select");
		try
		{
			this.select_Area = array[0];
			this.select_Lang = array[1];
		}
		catch (Exception)
		{
			this.select_Area = (this.select_Lang = 0);
		}
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0007B4D0 File Offset: 0x000796D0
	public void Sort_NewSv()
	{
		for (int i = 0; i < this.vecServer.size() - 1; i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			for (int j = i + 1; j < this.vecServer.size(); j++)
			{
				Command command2 = (Command)this.vecServer.elementAt(j);
				if (command2.isPaintNew && !command.isPaintNew)
				{
					Command command3 = command2;
					command2 = command;
					command = command3;
					this.vecServer.setElementAt(command, i);
					this.vecServer.setElementAt(command2, j);
				}
			}
		}
	}

	// Token: 0x04000EF8 RID: 3832
	internal int mainSelect;

	// Token: 0x04000EF9 RID: 3833
	internal MyVector vecServer = new MyVector();

	// Token: 0x04000EFA RID: 3834
	internal Command cmdCheck;

	// Token: 0x04000EFB RID: 3835
	public const int icmd = 100;

	// Token: 0x04000EFC RID: 3836
	internal int wc;

	// Token: 0x04000EFD RID: 3837
	internal int hc;

	// Token: 0x04000EFE RID: 3838
	internal int w2c;

	// Token: 0x04000EFF RID: 3839
	internal int numw;

	// Token: 0x04000F00 RID: 3840
	internal int numh;

	// Token: 0x04000F01 RID: 3841
	internal Command cmdGlobal;

	// Token: 0x04000F02 RID: 3842
	internal Command cmdVietNam;

	// Token: 0x04000F03 RID: 3843
	internal const string RMS_SELECT_AREA = "area_select";

	// Token: 0x04000F04 RID: 3844
	public bool isChooseArea;

	// Token: 0x04000F05 RID: 3845
	public bool isPaintNewUi;

	// Token: 0x04000F06 RID: 3846
	internal ListNew list;

	// Token: 0x04000F07 RID: 3847
	internal sbyte select_Area;

	// Token: 0x04000F08 RID: 3848
	internal sbyte select_Lang;

	// Token: 0x04000F09 RID: 3849
	internal sbyte select_typeSv;

	// Token: 0x04000F0A RID: 3850
	internal Command cmdChooseArea;

	// Token: 0x04000F0B RID: 3851
	internal bool isPaint_select_area;

	// Token: 0x04000F0C RID: 3852
	internal bool isPaint_select_lang;

	// Token: 0x04000F0D RID: 3853
	internal int x;

	// Token: 0x04000F0E RID: 3854
	internal int y;

	// Token: 0x04000F0F RID: 3855
	internal int w;

	// Token: 0x04000F10 RID: 3856
	internal int h;

	// Token: 0x04000F11 RID: 3857
	internal int xName;

	// Token: 0x04000F12 RID: 3858
	internal int yName;

	// Token: 0x04000F13 RID: 3859
	internal int xsub;

	// Token: 0x04000F14 RID: 3860
	internal int ysub;

	// Token: 0x04000F15 RID: 3861
	internal int wsub;

	// Token: 0x04000F16 RID: 3862
	internal int hsub;

	// Token: 0x04000F17 RID: 3863
	internal int xsubpaint;

	// Token: 0x04000F18 RID: 3864
	internal int ysubpaint;

	// Token: 0x04000F19 RID: 3865
	internal int xPop;

	// Token: 0x04000F1A RID: 3866
	internal int yPop;

	// Token: 0x04000F1B RID: 3867
	internal int wPop;

	// Token: 0x04000F1C RID: 3868
	internal int hPop;

	// Token: 0x04000F1D RID: 3869
	internal int xinfo;

	// Token: 0x04000F1E RID: 3870
	internal int yinfo;

	// Token: 0x04000F1F RID: 3871
	internal int winfo;

	// Token: 0x04000F20 RID: 3872
	internal int hinfo;

	// Token: 0x04000F21 RID: 3873
	internal int yBox;

	// Token: 0x04000F22 RID: 3874
	internal int wBox;

	// Token: 0x04000F23 RID: 3875
	internal int hBox;

	// Token: 0x04000F24 RID: 3876
	internal int ntypeSv;

	// Token: 0x04000F25 RID: 3877
	internal int xPopUp_Area;

	// Token: 0x04000F26 RID: 3878
	internal int yPopUp_Area;

	// Token: 0x04000F27 RID: 3879
	internal int xPopUp_Lang;

	// Token: 0x04000F28 RID: 3880
	internal int yPopUp_Lang;

	// Token: 0x04000F29 RID: 3881
	internal int htext = 15;

	// Token: 0x04000F2A RID: 3882
	internal string[] strLang = new string[] { "Tiếng Việt", "English", "Indo" };

	// Token: 0x04000F2B RID: 3883
	internal string[] strArea = new string[] { "VIỆT NAM", "GLOBAL" };

	// Token: 0x04000F2C RID: 3884
	internal string[] strTypeSV = new string[] { "Máy chủ tiêu chuẩn", "Máy chủ theo mùa" };

	// Token: 0x04000F2D RID: 3885
	internal string[] strTypeSV_info = new string[] { "Máy chủ tiêu chuẩn:\n-Không reset.\nTiến trình game bình thường.", "Máy chủ theo mùa:\n -Reset toàn bộ server và phát thưởng vào cuối mùa.\n x3 Sức mạnh\n x3 Tiềm năng\n x3 Vàng\n x3 Vật phẩm khác" };

	// Token: 0x04000F2E RID: 3886
	public int cmy;
}
