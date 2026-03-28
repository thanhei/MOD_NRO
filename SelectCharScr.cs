using System;
using Assets.src.e;

// Token: 0x020000C5 RID: 197
public class SelectCharScr : mScreen, IActionListener
{
	// Token: 0x060009F5 RID: 2549 RVA: 0x00095E6C File Offset: 0x0009406C
	public SelectCharScr()
	{
		try
		{
			if (!GameCanvas.lowGraphic)
			{
				SelectCharScr.loadMapFromResource(new sbyte[]
				{
					39,
					40,
					41
				});
			}
			this.loadMapTableFromResource(new sbyte[]
			{
				39,
				40,
				41
			});
		}
		catch (Exception ex)
		{
			Cout.LogError("Tao char loi " + ex.ToString());
		}
		this.cx = 168;
		this.cy = 350;
		short num = 32000;
		SmallImage.imgNew = new Small[(int)num];
		SmallImage.newSmallVersion = new sbyte[(int)num];
		SmallImage.maxSmall = num;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0000886A File Offset: 0x00006A6A
	public static SelectCharScr gI()
	{
		if (SelectCharScr.instance == null)
		{
			SelectCharScr.instance = new SelectCharScr();
		}
		return SelectCharScr.instance;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0005F920 File Offset: 0x0005DB20
	public static void loadMapFromResource(sbyte[] mapID)
	{
		Res.outz("newwwwwwwwww =============");
		for (int i = 0; i < mapID.Length; i++)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i]);
			MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
			MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
			Cout.LogError(string.Concat(new object[]
			{
				"Thong TIn : ",
				MapTemplate.tmw[i],
				"::",
				MapTemplate.tmh[i]
			}));
			MapTemplate.maps[i] = new int[dataInputStream.available()];
			Cout.LogError("lent= " + MapTemplate.maps[i].Length);
			for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
			{
				MapTemplate.maps[i][j] = dataInputStream.read();
			}
			MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00095F54 File Offset: 0x00094154
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
				dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i]);
				Cout.LogError("mapTable : " + mapID[i]);
				short num = dataInputStream.readShort();
				MapTemplate.vCurrItem[i] = new MyVector();
				Res.outz("nItem= " + num);
				for (int j = 0; j < (int)num; j++)
				{
					short id = dataInputStream.readShort();
					short num2 = dataInputStream.readShort();
					short num3 = dataInputStream.readShort();
					if (TileMap.getBIById((int)id) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)id);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)id;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)num2 * (int)TileMap.size;
						bgItem.y = (int)num3 * (int)TileMap.size;
						bgItem.layer = bibyId.layer;
						MapTemplate.vCurrItem[i].addElement(bgItem);
						if (!BgItem.imgNew.containsKey(bgItem.idImage + string.Empty))
						{
							try
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									BgItem.imgNew.put(bgItem.idImage + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								else
								{
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
								}
							}
							catch (Exception ex)
							{
								Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image2 == null)
								{
									image2 = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image2);
							}
							BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
						}
						bgItem.changeColor();
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Cout.println("LOI TAI loadMapTableFromResource" + ex2.ToString());
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0009627C File Offset: 0x0009447C
	public void doChangeMap()
	{
		TileMap.maps = new int[MapTemplate.maps[this.indexGender].Length];
		for (int i = 0; i < MapTemplate.maps[this.indexGender].Length; i++)
		{
			TileMap.maps[i] = MapTemplate.maps[this.indexGender][i];
		}
		TileMap.types = MapTemplate.types[this.indexGender];
		TileMap.pxh = MapTemplate.pxh[this.indexGender];
		TileMap.pxw = MapTemplate.pxw[this.indexGender];
		TileMap.tileID = MapTemplate.pxw[this.indexGender];
		TileMap.tmw = MapTemplate.tmw[this.indexGender];
		TileMap.tmh = MapTemplate.tmh[this.indexGender];
		TileMap.tileID = this.bgID[this.indexGender] + 1;
		TileMap.loadMainTile();
		TileMap.loadTileCreatChar();
		GameCanvas.loadBG(this.bgID[this.indexGender]);
		GameScr.loadCamera(true, this.cx, this.cy);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00096380 File Offset: 0x00094580
	public void SetInfoChar(global::Char temp)
	{
		this.mychar = new global::Char();
		this.indexGender = (this.mychar.cgender = temp.cgender);
		this.mychar.head = temp.head;
		this.mychar.headICON = temp.headICON;
		this.mychar.body = temp.body;
		this.mychar.leg = temp.leg;
		this.mychar.bag = temp.bag;
		this.mychar.cName = temp.cName;
		this.switchToMe();
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00096420 File Offset: 0x00094620
	public override void switchToMe()
	{
		GameCanvas.menu.showMenu = false;
		GameCanvas.endDlg();
		GameScr.gI().initSelectChar();
		base.switchToMe();
		this.doChangeMap();
		global::Char.isLoadingMap = false;
		ServerListScreen.countDieConnect = 0;
		this.center = new Command(mResources.SELECT, this, 100, null);
		this.left = new Command(mResources.BACK, this, 101, null);
		this.cmdSelectSv = new Command(ServerListScreen.nameServer[ServerListScreen.ipSelect], this, 102, null);
		this.cmdSelectSv.x = 1;
		this.cmdSelectSv.y = 3;
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000964BC File Offset: 0x000946BC
	public override void paint(mGraphics g)
	{
		if (!Controller.isGet_CLIENT_INFO)
		{
			return;
		}
		if (SelectCharScr.isWait)
		{
			return;
		}
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameCanvas.paintBGGameScr(g);
		g.translate(-GameScr.cmx, -GameScr.cmy);
		for (int i = 0; i < MapTemplate.vCurrItem[this.indexGender].size(); i++)
		{
			BgItem bgItem = (BgItem)MapTemplate.vCurrItem[this.indexGender].elementAt(i);
			if (bgItem.idImage != -1 && (int)bgItem.layer == 1)
			{
				bgItem.paint(g);
			}
		}
		TileMap.paintTilemap(g);
		g.drawImage(TileMap.bong, GameScr.cmx + GameCanvas.hw, this.cy + this.dy + 1, 3);
		if (this.mychar != null)
		{
			this.mychar.paintCharBody(g, GameScr.cmx + GameCanvas.hw, this.cy + this.dy, 1, this.f[this.count], true);
			mFont.tahoma_7b_yellow.drawString(g, this.mychar.cName, GameScr.cmx + GameCanvas.hw, this.cy - 15, mFont.CENTER, mFont.tahoma_7_greySmall);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		base.paint(g);
		this.cmdSelectSv.paint(g);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00096620 File Offset: 0x00094820
	public override void update()
	{
		base.update();
		if (!Session_ME.gI().isConnected())
		{
			SelectCharScr.isWait = true;
			this.count++;
			if (this.count > 50)
			{
				ServerListScreen.ConnectIP();
				this.count = 0;
			}
			return;
		}
		SelectCharScr.isWait = false;
		this.count++;
		if (this.count > this.f.Length - 1)
		{
			this.count = 0;
		}
		if (this.cmdSelectSv != null && this.cmdSelectSv.isPointerPressInside())
		{
			this.cmdSelectSv.performAction();
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x000966C8 File Offset: 0x000948C8
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 100:
			GameCanvas.serverScreen.Login_New();
			break;
		case 101:
			ServerListScreen.isAutoLogin = false;
			GameCanvas.serverScreen.switchToMe();
			break;
		case 102:
			ServerListScreen.SetIpSelect(-1, true);
			ServerScr.isShowSv_HaveChar = false;
			Controller.isEXTRA_LINK = false;
			GameCanvas.serverScr.switchToMe();
			break;
		}
	}

	// Token: 0x04001280 RID: 4736
	public static bool isWait;

	// Token: 0x04001281 RID: 4737
	public static SelectCharScr instance;

	// Token: 0x04001282 RID: 4738
	public global::Char mychar;

	// Token: 0x04001283 RID: 4739
	private int indexGender;

	// Token: 0x04001284 RID: 4740
	private int cx;

	// Token: 0x04001285 RID: 4741
	private int cy;

	// Token: 0x04001286 RID: 4742
	private int dy = 45;

	// Token: 0x04001287 RID: 4743
	private Command cmdSelectSv;

	// Token: 0x04001288 RID: 4744
	private int[] bgID = new int[]
	{
		0,
		4,
		8
	};

	// Token: 0x04001289 RID: 4745
	private int[] f = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x0400128A RID: 4746
	private int count;
}
