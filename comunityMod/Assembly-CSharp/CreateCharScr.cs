using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class CreateCharScr : mScreen, IActionListener
{
	// Token: 0x0600021A RID: 538 RVA: 0x0002C908 File Offset: 0x0002AB08
	public CreateCharScr()
	{
		try
		{
			if (!GameCanvas.lowGraphic)
			{
				CreateCharScr.loadMapFromResource(new sbyte[] { 39, 40, 41 });
			}
			this.loadMapTableFromResource(new sbyte[] { 39, 40, 41 });
		}
		catch (Exception ex)
		{
			Cout.LogError("Tao char loi " + ex.ToString());
		}
		if (GameCanvas.w <= 200)
		{
			GameScr.setPopupSize(128, 100);
			GameScr.popupX = (GameCanvas.w - 128) / 2;
			GameScr.popupY = 10;
			this.cy += 15;
			this.dy -= 15;
		}
		CreateCharScr.indexGender = 1;
		CreateCharScr.tAddName = new TField();
		CreateCharScr.tAddName.width = GameCanvas.loginScr.tfUser.width;
		if (GameCanvas.w < 200)
		{
			CreateCharScr.tAddName.width = 60;
		}
		CreateCharScr.tAddName.height = mScreen.ITEM_HEIGHT + 2;
		if (GameCanvas.w < 200)
		{
			CreateCharScr.tAddName.x = GameScr.popupX + 45;
			CreateCharScr.tAddName.y = GameScr.popupY + 12;
		}
		else
		{
			CreateCharScr.tAddName.x = GameCanvas.w / 2 - CreateCharScr.tAddName.width / 2;
			CreateCharScr.tAddName.y = 35;
		}
		if (!GameCanvas.isTouch)
		{
			CreateCharScr.tAddName.isFocus = true;
		}
		CreateCharScr.tAddName.setIputType(TField.INPUT_TYPE_ANY);
		CreateCharScr.tAddName.showSubTextField = false;
		CreateCharScr.tAddName.strInfo = mResources.char_name;
		if (CreateCharScr.tAddName.getText().Equals("@"))
		{
			CreateCharScr.tAddName.setText(GameCanvas.loginScr.tfUser.getText().Substring(0, GameCanvas.loginScr.tfUser.getText().IndexOf("@")));
		}
		CreateCharScr.tAddName.name = mResources.char_name;
		CreateCharScr.indexGender = 1;
		CreateCharScr.indexHair = 0;
		this.center = new Command(mResources.NEWCHAR, this, 8000, null);
		this.left = new Command(mResources.BACK, this, 8001, null);
		if (!GameCanvas.isTouch)
		{
			this.right = CreateCharScr.tAddName.cmdClear;
		}
		this.yBegin = CreateCharScr.tAddName.y;
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0002CBA4 File Offset: 0x0002ADA4
	public static CreateCharScr gI()
	{
		if (CreateCharScr.instance == null)
		{
			CreateCharScr.instance = new CreateCharScr();
		}
		return CreateCharScr.instance;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00004887 File Offset: 0x00002A87
	public static void init()
	{
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0002CBBC File Offset: 0x0002ADBC
	public static void loadMapFromResource(sbyte[] mapID)
	{
		Res.outz("newwwwwwwwww =============");
		for (int i = 0; i < mapID.Length; i++)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i].ToString());
			MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
			MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
			Cout.LogError("Thong TIn : " + MapTemplate.tmw[i].ToString() + "::" + MapTemplate.tmh[i].ToString());
			MapTemplate.maps[i] = new int[dataInputStream.available()];
			Cout.LogError("lent= " + MapTemplate.maps[i].Length.ToString());
			for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
			{
				MapTemplate.maps[i][j] = dataInputStream.read();
			}
			MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
	public void loadMapTableFromResource(sbyte[] mapID)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		DataInputStream dataInputStream = null;
		try
		{
			for (int i = 0; i < mapID.Length; i++)
			{
				dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i].ToString());
				Cout.LogError("mapTable : " + mapID[i].ToString());
				short num = dataInputStream.readShort();
				MapTemplate.vCurrItem[i] = new MyVector();
				Res.outz("nItem= " + num.ToString());
				for (int j = 0; j < (int)num; j++)
				{
					short num2 = dataInputStream.readShort();
					short num3 = dataInputStream.readShort();
					short num4 = dataInputStream.readShort();
					if (TileMap.getBIById((int)num2) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)num2);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)num2;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)(num3 * (short)TileMap.size);
						bgItem.y = (int)(num4 * (short)TileMap.size);
						bgItem.layer = bibyId.layer;
						MapTemplate.vCurrItem[i].addElement(bgItem);
						if (!BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty))
						{
							try
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
								if (image == null)
								{
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								else
								{
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
								}
							}
							catch (Exception)
							{
								Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
								if (image2 == null)
								{
									image2 = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image2);
							}
							BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						bgItem.changeColor();
					}
					else
					{
						Res.outz("item null");
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("LOI TAI loadMapTableFromResource" + ex.ToString());
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
	public override void switchToMe()
	{
		LoginScr.isContinueToLogin = false;
		GameCanvas.menu.showMenu = false;
		GameCanvas.endDlg();
		base.switchToMe();
		CreateCharScr.indexGender = Res.random(0, 3);
		CreateCharScr.indexHair = Res.random(0, 3);
		this.doChangeMap();
		global::Char.isLoadingMap = false;
		CreateCharScr.tAddName.setFocusWithKb(true);
		ServerListScreen.countDieConnect = 0;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0002D044 File Offset: 0x0002B244
	public void doChangeMap()
	{
		TileMap.maps = new int[MapTemplate.maps[CreateCharScr.indexGender].Length];
		for (int i = 0; i < MapTemplate.maps[CreateCharScr.indexGender].Length; i++)
		{
			TileMap.maps[i] = MapTemplate.maps[CreateCharScr.indexGender][i];
		}
		TileMap.types = MapTemplate.types[CreateCharScr.indexGender];
		TileMap.pxh = MapTemplate.pxh[CreateCharScr.indexGender];
		TileMap.pxw = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tileID = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tmw = MapTemplate.tmw[CreateCharScr.indexGender];
		TileMap.tmh = MapTemplate.tmh[CreateCharScr.indexGender];
		TileMap.tileID = this.bgID[CreateCharScr.indexGender] + 1;
		TileMap.loadMainTile();
		TileMap.loadTileCreatChar();
		GameCanvas.loadBG(this.bgID[CreateCharScr.indexGender]);
		GameScr.loadCamera(false, this.cx, this.cy);
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0002D134 File Offset: 0x0002B334
	public override void keyPress(int keyCode)
	{
		CreateCharScr.tAddName.keyPressed(keyCode);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0002D144 File Offset: 0x0002B344
	public override void update()
	{
		this.cp1++;
		if (this.cp1 > 30)
		{
			this.cp1 = 0;
		}
		if (this.cp1 % 15 < 5)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		CreateCharScr.tAddName.update();
		if (CreateCharScr.selected != 0)
		{
			CreateCharScr.tAddName.isFocus = false;
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
	public override void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			CreateCharScr.selected--;
			if (CreateCharScr.selected < 0)
			{
				CreateCharScr.selected = mResources.MENUNEWCHAR.Length - 1;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			CreateCharScr.selected++;
			if (CreateCharScr.selected >= mResources.MENUNEWCHAR.Length)
			{
				CreateCharScr.selected = 0;
			}
		}
		if (CreateCharScr.selected == 0)
		{
			if (!GameCanvas.isTouch)
			{
				this.right = CreateCharScr.tAddName.cmdClear;
			}
			CreateCharScr.tAddName.update();
		}
		if (CreateCharScr.selected == 1)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				CreateCharScr.indexGender--;
				if (CreateCharScr.indexGender < 0)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				this.doChangeMap();
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				CreateCharScr.indexGender++;
				if (CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1)
				{
					CreateCharScr.indexGender = 0;
				}
				this.doChangeMap();
			}
			this.right = null;
		}
		if (CreateCharScr.selected == 2)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				CreateCharScr.indexHair--;
				if (CreateCharScr.indexHair < 0)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				CreateCharScr.indexHair++;
				if (CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1)
				{
					CreateCharScr.indexHair = 0;
				}
			}
			this.right = null;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			int num = 110;
			int num2 = 60;
			int num3 = 78;
			if (GameCanvas.w > GameCanvas.h)
			{
				num = 100;
				num2 = 40;
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, 15, num3 * 3, 80))
			{
				CreateCharScr.selected = 0;
				CreateCharScr.tAddName.isFocus = true;
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30, num3 * 3, num2 + 5))
			{
				CreateCharScr.selected = 1;
				int num4 = CreateCharScr.indexGender;
				CreateCharScr.indexGender = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				if (CreateCharScr.indexGender < 0)
				{
					CreateCharScr.indexGender = 0;
				}
				if (CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				if (num4 != CreateCharScr.indexGender)
				{
					this.doChangeMap();
				}
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30 + num2 + 5, num3 * 3, 65))
			{
				CreateCharScr.selected = 2;
				int num5 = CreateCharScr.indexHair;
				CreateCharScr.indexHair = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				if (CreateCharScr.indexHair < 0)
				{
					CreateCharScr.indexHair = 0;
				}
				if (CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
				if (num5 != CreateCharScr.selected)
				{
					this.doChangeMap();
				}
			}
		}
		if (!TouchScreenKeyboard.visible)
		{
			base.updateKey();
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0002D4B4 File Offset: 0x0002B6B4
	public override void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameCanvas.paintBGGameScr(g);
		g.translate(-GameScr.cmx, -GameScr.cmy);
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); i++)
			{
				BgItem bgItem = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(i);
				if (bgItem.idImage != -1 && bgItem.layer == 1)
				{
					bgItem.paint(g);
				}
			}
		}
		if (mSystem.clientType == 5)
		{
			GameCanvas.paint_ios_bg(g);
		}
		else
		{
			TileMap.paintTilemap(g);
		}
		int num = 30;
		if (GameCanvas.w == 128)
		{
			num = 20;
		}
		int num2 = CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair];
		int num3 = CreateCharScr.defaultLeg[CreateCharScr.indexGender];
		int num4 = CreateCharScr.defaultBody[CreateCharScr.indexGender];
		g.drawImage(TileMap.bong, this.cx, this.cy + this.dy, 3);
		Part part = GameScr.parts[num2];
		Part part2 = GameScr.parts[num3];
		Part part3 = GameScr.parts[num4];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + this.dy, 0, 0);
		SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy + this.dy, 0, 0);
		SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy + this.dy, 0, 0);
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); j++)
			{
				BgItem bgItem2 = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(j);
				if (bgItem2.idImage != -1 && bgItem2.layer == 3)
				{
					bgItem2.paint(g);
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (GameCanvas.w < 200)
		{
			GameCanvas.paintz.paintFrame(GameScr.popupX, GameScr.popupY, GameScr.popupW, GameScr.popupH, g);
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy, 0, 0);
			for (int k = 0; k < mResources.MENUNEWCHAR.Length; k++)
			{
				if (CreateCharScr.selected == k)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, GameScr.popupX + 10 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, GameScr.popupX + GameScr.popupW - 10 - ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUNEWCHAR[k], GameScr.popupX + 20, GameScr.popupY + 30 + k * num, 0);
			}
			mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[CreateCharScr.indexGender], GameScr.popupX + 70, GameScr.popupY + 30 + num, mFont.LEFT);
			mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][CreateCharScr.indexHair], GameScr.popupX + 55, GameScr.popupY + 30 + 2 * num, mFont.LEFT);
			CreateCharScr.tAddName.paint(g);
		}
		else
		{
			if (!Main.isPC)
			{
				if (mGraphics.addYWhenOpenKeyBoard != 0)
				{
					this.yButton = 110;
					this.disY = 60;
					if (GameCanvas.w > GameCanvas.h)
					{
						this.yButton = GameScr.popupY + 30 + 3 * num + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy - 15;
						this.disY = 35;
					}
				}
				else
				{
					this.yButton = 110;
					this.disY = 60;
					if (GameCanvas.w > GameCanvas.h)
					{
						this.yButton = 100;
						this.disY = 45;
					}
				}
				CreateCharScr.tAddName.y = this.yButton - CreateCharScr.tAddName.height - this.disY + 5;
			}
			else
			{
				this.yButton = 110;
				this.disY = 60;
				if (GameCanvas.w > GameCanvas.h)
				{
					this.yButton = 100;
					this.disY = 45;
				}
				CreateCharScr.tAddName.y = this.yBegin;
			}
			for (int l = 0; l < 3; l++)
			{
				int num5 = 78;
				if (l != CreateCharScr.indexGender)
				{
					g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
				}
				else
				{
					if (CreateCharScr.selected == 1)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num5 + l * num5, this.yButton - 20 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), StaticObj.VCENTER_HCENTER);
					}
					g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[l], GameCanvas.w / 2 - num5 + l * num5, this.yButton - 5, mFont.CENTER);
			}
			for (int m = 0; m < 3; m++)
			{
				int num6 = 78;
				if (m != CreateCharScr.indexHair)
				{
					g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
				}
				else
				{
					if (CreateCharScr.selected == 2)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 20 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), StaticObj.VCENTER_HCENTER);
					}
					g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][m], GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 5, mFont.CENTER);
			}
			CreateCharScr.tAddName.paint(g);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		mFont.tahoma_7b_white.drawString(g, mResources.server + " " + LoginScr.serverName, 5, 5, 0, mFont.tahoma_7b_dark);
		if (!TouchScreenKeyboard.visible)
		{
			base.paint(g);
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0002DE20 File Offset: 0x0002C020
	public void perform(int idAction, object p)
	{
		if (idAction != 8000)
		{
			if (idAction != 8001)
			{
				if (idAction == 10019)
				{
					Session_ME.gI().close();
					GameCanvas.endDlg();
					GameCanvas.serverScreen.switchToMe();
					return;
				}
				if (idAction == 10020)
				{
					GameCanvas.endDlg();
					return;
				}
			}
			else
			{
				if (GameCanvas.loginScr.isLogin2)
				{
					GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
					return;
				}
				if (Main.isWindowsPhone)
				{
					GameMidlet.isBackWindowsPhone = true;
				}
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				return;
			}
		}
		else
		{
			if (CreateCharScr.tAddName.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.char_name_blank);
				return;
			}
			if (CreateCharScr.tAddName.getText().Length < 5)
			{
				GameCanvas.startOKDlg(mResources.char_name_short);
				return;
			}
			if (CreateCharScr.tAddName.getText().Length > 15)
			{
				GameCanvas.startOKDlg(mResources.char_name_long);
				return;
			}
			InfoDlg.showWait();
			Service.gI().createChar(CreateCharScr.tAddName.getText(), CreateCharScr.indexGender, CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair]);
		}
	}

	// Token: 0x040004D3 RID: 1235
	public static CreateCharScr instance;

	// Token: 0x040004D4 RID: 1236
	internal PopUp p;

	// Token: 0x040004D5 RID: 1237
	public static bool isCreateChar = false;

	// Token: 0x040004D6 RID: 1238
	public static TField tAddName;

	// Token: 0x040004D7 RID: 1239
	public static int indexGender;

	// Token: 0x040004D8 RID: 1240
	public static int indexHair;

	// Token: 0x040004D9 RID: 1241
	public static int selected;

	// Token: 0x040004DA RID: 1242
	public static int[][] hairID = new int[][]
	{
		new int[] { 64, 30, 31 },
		new int[] { 9, 29, 32 },
		new int[] { 6, 27, 28 }
	};

	// Token: 0x040004DB RID: 1243
	public static int[] defaultLeg = new int[] { 2, 13, 8 };

	// Token: 0x040004DC RID: 1244
	public static int[] defaultBody = new int[] { 1, 12, 7 };

	// Token: 0x040004DD RID: 1245
	internal int yButton;

	// Token: 0x040004DE RID: 1246
	internal int disY;

	// Token: 0x040004DF RID: 1247
	internal int[] bgID = new int[] { 0, 4, 8 };

	// Token: 0x040004E0 RID: 1248
	public int yBegin;

	// Token: 0x040004E1 RID: 1249
	internal int curIndex;

	// Token: 0x040004E2 RID: 1250
	internal int cx = 168;

	// Token: 0x040004E3 RID: 1251
	internal int cy = 350;

	// Token: 0x040004E4 RID: 1252
	internal int dy = 45;

	// Token: 0x040004E5 RID: 1253
	internal int cp1;

	// Token: 0x040004E6 RID: 1254
	internal int cf;
}
