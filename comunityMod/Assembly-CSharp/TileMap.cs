using System;

// Token: 0x020000BB RID: 187
public class TileMap
{
	// Token: 0x060009A7 RID: 2471 RVA: 0x0008BD03 File Offset: 0x00089F03
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
		{
			TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0008BD3C File Offset: 0x00089F3C
	public static bool isVoDaiMap()
	{
		return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0008BD88 File Offset: 0x00089F88
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0008BDA8 File Offset: 0x00089FA8
	public static bool mapPhuBang()
	{
		return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0008BDC8 File Offset: 0x00089FC8
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			if (bgItem.id == id)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0008BE08 File Offset: 0x0008A008
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.offlineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0008BE38 File Offset: 0x0008A038
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.highterId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0008BE68 File Offset: 0x0008A068
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.toOfflineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0008BE98 File Offset: 0x0008A098
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00004887 File Offset: 0x00002A87
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0008BEA8 File Offset: 0x0008A0A8
	public static bool isExistMoreOne(int id)
	{
		if (id == 156 || id == 330 || id == 345 || id == 334)
		{
			return false;
		}
		if (TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103)
		{
			return false;
		}
		int num = 0;
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			if (((BgItem)TileMap.vCurrItem.elementAt(i)).id == id)
			{
				num++;
			}
		}
		return num > 2;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0008BF54 File Offset: 0x0008A154
	public static void loadTileImage()
	{
		if (TileMap.imgWaterfall == null)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		if (TileMap.imgTopWaterfall == null)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		if (TileMap.imgWaterflow == null)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		if (TileMap.imgWaterlowN == null)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		if (TileMap.imgWaterlowN2 == null)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
		mSystem.gcc();
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0008BFD4 File Offset: 0x0008A1D4
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			if (TileMap.maps[index] == mapsArr[i])
			{
				TileMap.types[index] |= type;
				return;
			}
		}
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0008C00C File Offset: 0x0008A20C
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID.ToString());
		int num = tileId - 1;
		try
		{
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				for (int j = 0; j < TileMap.tileType[num].Length; j++)
				{
					TileMap.setTile(i, TileMap.tileIndex[num][j], TileMap.tileType[num][j]);
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Error Load Map");
			GameMidlet.instance.exit();
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0008C0C4 File Offset: 0x0008A2C4
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0008C0E4 File Offset: 0x0008A2E4
	public static bool isDoubleMap()
	{
		return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0008C1A8 File Offset: 0x0008A3A8
	public static void getTile()
	{
		if (Main.typeClient == 3 || Main.typeClient == 5)
		{
			if (mGraphics.zoomLevel == 1)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID.ToString() + ".png");
				return;
			}
			TileMap.imgTile = new Image[100];
			for (int i = 0; i < TileMap.imgTile.Length; i++)
			{
				TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new string[]
				{
					"/t/",
					TileMap.tileID.ToString(),
					"/",
					(i + 1).ToString(),
					".png"
				}));
			}
			return;
		}
		else
		{
			if (mGraphics.zoomLevel == 1)
			{
				if (TileMap.imgTile != null)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						if (TileMap.imgTile[j] != null)
						{
							TileMap.imgTile[j].texture = null;
							TileMap.imgTile[j] = null;
						}
					}
					mSystem.gcc();
				}
				TileMap.imgTile = new Image[100];
				string text = string.Empty;
				for (int k = 0; k < TileMap.imgTile.Length; k++)
				{
					text = ((k >= 9) ? ("/t/" + TileMap.tileID.ToString() + "/t_" + (k + 1).ToString()) : ("/t/" + TileMap.tileID.ToString() + "/t_0" + (k + 1).ToString()));
					TileMap.imgTile[k] = GameCanvas.loadImage(text);
				}
				return;
			}
			if (GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + "$1.png") != null)
			{
				Rms.DeleteStorage("x" + mGraphics.zoomLevel.ToString() + "t" + TileMap.tileID.ToString());
				TileMap.imgTile = new Image[100];
				for (int l = 0; l < TileMap.imgTile.Length; l++)
				{
					TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new string[]
					{
						"/t/",
						TileMap.tileID.ToString(),
						"$",
						(l + 1).ToString(),
						".png"
					}));
				}
				return;
			}
			Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + ".png");
			if (image != null)
			{
				Rms.DeleteStorage("$");
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = image;
			}
			return;
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0008C438 File Offset: 0x0008A638
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		if (TileMap.imgTile != null)
		{
			if (TileMap.imgTile.Length == 1)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
				return;
			}
			g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		if (TileMap.imgTile != null)
		{
			if (TileMap.imgTile.Length == 1)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
				return;
			}
			g.drawImage(TileMap.imgTile[frame], x, y, 0);
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0008C4F0 File Offset: 0x0008A6F0
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				if (num != -1)
				{
					TileMap.paintTile(g, num, i, j);
				}
				if ((TileMap.tileTypeAt(i, j) & 32) == 32)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					g.drawRegion((TileMap.tileID == 5) ? TileMap.imgWaterlowN : ((TileMap.tileID != 8) ? TileMap.imgWaterflow : TileMap.imgWaterlowN2), 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				if ((TileMap.tileTypeAt(i, j) & 2048) == 2048)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0008C6D0 File Offset: 0x0008A8D0
	public static void paintTilemap(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameScr.gI().paintBgItem(g, 1);
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
		}
		for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
		{
			for (int k = GameScr.gssy; k < GameScr.gssye; k++)
			{
				if (j != 0 && j != TileMap.tmw - 1)
				{
					int num = TileMap.maps[k * TileMap.tmw + j] - 1;
					if ((TileMap.tileTypeAt(j, k) & 256) != 256)
					{
						if ((TileMap.tileTypeAt(j, k) & 32) == 32)
						{
							g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
						}
						else if ((TileMap.tileTypeAt(j, k) & 128) == 128)
						{
							g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
						}
						else if (TileMap.tileID != 13 || num == -1)
						{
							if (TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1)
							{
								TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
								TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
							}
							int num2 = TileMap.tileID;
							if ((TileMap.tileTypeAt(j, k) & 16) == 16)
							{
								TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
								TileMap.dbx = TileMap.bx - GameScr.gW2;
								TileMap.dfx = (int)(TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
								TileMap.fx = TileMap.dfx + GameScr.gW2;
								TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
							}
							else if ((TileMap.tileTypeAt(j, k) & 512) == 512)
							{
								if (num != -1)
								{
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
								}
							}
							else if (num != -1)
							{
								TileMap.paintTile(g, num, j, k);
							}
						}
					}
				}
			}
		}
		if (GameScr.cmx < 24)
		{
			for (int l = GameScr.gssy; l < GameScr.gssye; l++)
			{
				int num3 = TileMap.maps[l * TileMap.tmw + 1] - 1;
				if (num3 != -1)
				{
					TileMap.paintTile(g, num3, 0, l);
				}
			}
		}
		if (GameScr.cmx <= GameScr.cmxLim)
		{
			return;
		}
		int num4 = TileMap.tmw - 2;
		for (int m = GameScr.gssy; m < GameScr.gssye; m++)
		{
			int num5 = TileMap.maps[m * TileMap.tmw + num4] - 1;
			if (num5 != -1)
			{
				TileMap.paintTile(g, num5, num4 + 1, m);
			}
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0008C9F0 File Offset: 0x0008ABF0
	public static bool isWaterEff()
	{
		return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0008CA3C File Offset: 0x0008AC3C
	public static void paintOutTilemap(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		int num = 0;
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				num++;
				if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					Image image = ((TileMap.tileID == 5) ? TileMap.imgWaterlowN : ((TileMap.tileID != 8) ? TileMap.imgWaterflow : TileMap.imgWaterlowN2));
					if (!TileMap.isWaterEff())
					{
						g.drawRegion(image, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
						g.drawRegion(image, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
					}
					g.drawRegion(image, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
					if (TileMap.yWater == 0 && TileMap.isWaterEff())
					{
						TileMap.yWater = j * (int)TileMap.size - 12;
						int num2 = 16777215;
						if (GameCanvas.typeBg == 2)
						{
							num2 = 10871287;
						}
						else if (GameCanvas.typeBg == 4)
						{
							num2 = 8111470;
						}
						else if (GameCanvas.typeBg == 7)
						{
							num2 = 5693125;
						}
						else if (GameCanvas.typeBg == 19)
						{
							num2 = 16711680;
						}
						BackgroudEffect.addWater(num2, TileMap.yWater + 15);
					}
				}
			}
		}
		BackgroudEffect.paintWaterAll(g);
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0008CBB0 File Offset: 0x0008ADB0
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID.ToString());
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0008CC34 File Offset: 0x0008AE34
	public static int tileAt(int x, int y)
	{
		int num;
		try
		{
			num = TileMap.maps[y * TileMap.tmw + x];
		}
		catch (Exception)
		{
			num = 1000;
		}
		return num;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0008CC70 File Offset: 0x0008AE70
	public static int tileTypeAt(int x, int y)
	{
		int num;
		try
		{
			num = TileMap.types[y * TileMap.tmw + x];
		}
		catch (Exception)
		{
			num = 1000;
		}
		return num;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0008CCAC File Offset: 0x0008AEAC
	public static int tileTypeAtPixel(int px, int py)
	{
		int num;
		try
		{
			num = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
		}
		catch (Exception)
		{
			num = 1000;
		}
		return num;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0008CCF4 File Offset: 0x0008AEF4
	public static bool tileTypeAt(int px, int py, int t)
	{
		bool flag;
		try
		{
			flag = (TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t;
		}
		catch (Exception)
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0008CD3C File Offset: 0x0008AF3C
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0008CD62 File Offset: 0x0008AF62
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0008CD74 File Offset: 0x0008AF74
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0008CD9B File Offset: 0x0008AF9B
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0008CD9B File Offset: 0x0008AF9B
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0008CDAA File Offset: 0x0008AFAA
	public static void loadMainTile()
	{
		if (TileMap.lastTileID != TileMap.tileID)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x040010EE RID: 4334
	public const int T_EMPTY = 0;

	// Token: 0x040010EF RID: 4335
	public const int T_TOP = 2;

	// Token: 0x040010F0 RID: 4336
	public const int T_LEFT = 4;

	// Token: 0x040010F1 RID: 4337
	public const int T_RIGHT = 8;

	// Token: 0x040010F2 RID: 4338
	public const int T_TREE = 16;

	// Token: 0x040010F3 RID: 4339
	public const int T_WATERFALL = 32;

	// Token: 0x040010F4 RID: 4340
	public const int T_WATERFLOW = 64;

	// Token: 0x040010F5 RID: 4341
	public const int T_TOPFALL = 128;

	// Token: 0x040010F6 RID: 4342
	public const int T_OUTSIDE = 256;

	// Token: 0x040010F7 RID: 4343
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x040010F8 RID: 4344
	public const int T_BRIDGE = 1024;

	// Token: 0x040010F9 RID: 4345
	public const int T_UNDERWATER = 2048;

	// Token: 0x040010FA RID: 4346
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x040010FB RID: 4347
	public const int T_BOTTOM = 8192;

	// Token: 0x040010FC RID: 4348
	public const int T_DIE = 16384;

	// Token: 0x040010FD RID: 4349
	public const int T_HEBI = 32768;

	// Token: 0x040010FE RID: 4350
	public const int T_BANG = 65536;

	// Token: 0x040010FF RID: 4351
	public const int T_JUM8 = 131072;

	// Token: 0x04001100 RID: 4352
	public const int T_NT0 = 262144;

	// Token: 0x04001101 RID: 4353
	public const int T_NT1 = 524288;

	// Token: 0x04001102 RID: 4354
	public const int T_CENTER = 1;

	// Token: 0x04001103 RID: 4355
	public static int tmw;

	// Token: 0x04001104 RID: 4356
	public static int tmh;

	// Token: 0x04001105 RID: 4357
	public static int pxw;

	// Token: 0x04001106 RID: 4358
	public static int pxh;

	// Token: 0x04001107 RID: 4359
	public static int tileID;

	// Token: 0x04001108 RID: 4360
	public static int lastTileID = -1;

	// Token: 0x04001109 RID: 4361
	public static int[] maps;

	// Token: 0x0400110A RID: 4362
	public static int[] types;

	// Token: 0x0400110B RID: 4363
	public static Image[] imgTile;

	// Token: 0x0400110C RID: 4364
	public static Image imgTileSmall;

	// Token: 0x0400110D RID: 4365
	public static Image imgMiniMap;

	// Token: 0x0400110E RID: 4366
	public static Image imgWaterfall;

	// Token: 0x0400110F RID: 4367
	public static Image imgTopWaterfall;

	// Token: 0x04001110 RID: 4368
	public static Image imgWaterflow;

	// Token: 0x04001111 RID: 4369
	public static Image imgWaterlowN;

	// Token: 0x04001112 RID: 4370
	public static Image imgWaterlowN2;

	// Token: 0x04001113 RID: 4371
	public static Image imgWaterF;

	// Token: 0x04001114 RID: 4372
	public static Image imgLeaf;

	// Token: 0x04001115 RID: 4373
	public static sbyte size = 24;

	// Token: 0x04001116 RID: 4374
	internal static int bx;

	// Token: 0x04001117 RID: 4375
	internal static int dbx;

	// Token: 0x04001118 RID: 4376
	internal static int fx;

	// Token: 0x04001119 RID: 4377
	internal static int dfx;

	// Token: 0x0400111A RID: 4378
	public static string[] instruction;

	// Token: 0x0400111B RID: 4379
	public static int[] iX;

	// Token: 0x0400111C RID: 4380
	public static int[] iY;

	// Token: 0x0400111D RID: 4381
	public static int[] iW;

	// Token: 0x0400111E RID: 4382
	public static int iCount;

	// Token: 0x0400111F RID: 4383
	public static bool isMapDouble = false;

	// Token: 0x04001120 RID: 4384
	public static string mapName = string.Empty;

	// Token: 0x04001121 RID: 4385
	public static sbyte versionMap = 1;

	// Token: 0x04001122 RID: 4386
	public static int mapID;

	// Token: 0x04001123 RID: 4387
	public static int lastBgID = -1;

	// Token: 0x04001124 RID: 4388
	public static int zoneID;

	// Token: 0x04001125 RID: 4389
	public static int bgID;

	// Token: 0x04001126 RID: 4390
	public static int bgType;

	// Token: 0x04001127 RID: 4391
	public static int lastType = -1;

	// Token: 0x04001128 RID: 4392
	public static int typeMap;

	// Token: 0x04001129 RID: 4393
	public static sbyte planetID;

	// Token: 0x0400112A RID: 4394
	public static sbyte lastPlanetId = -1;

	// Token: 0x0400112B RID: 4395
	public static long timeTranMini;

	// Token: 0x0400112C RID: 4396
	public static MyVector vGo = new MyVector();

	// Token: 0x0400112D RID: 4397
	public static MyVector vItemBg = new MyVector();

	// Token: 0x0400112E RID: 4398
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x0400112F RID: 4399
	public static string[] mapNames;

	// Token: 0x04001130 RID: 4400
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x04001131 RID: 4401
	public static Image bong;

	// Token: 0x04001132 RID: 4402
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x04001133 RID: 4403
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x04001134 RID: 4404
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x04001135 RID: 4405
	public const int TRAIDAT_DADO = 3;

	// Token: 0x04001136 RID: 4406
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x04001137 RID: 4407
	public const int NAMEK_DOINUI = 4;

	// Token: 0x04001138 RID: 4408
	public const int NAMEK_RUNG = 6;

	// Token: 0x04001139 RID: 4409
	public const int NAMEK_DAO = 7;

	// Token: 0x0400113A RID: 4410
	public const int SAYAI_DOINUI = 8;

	// Token: 0x0400113B RID: 4411
	public const int SAYAI_RUNG = 9;

	// Token: 0x0400113C RID: 4412
	public const int SAYAI_CITY = 10;

	// Token: 0x0400113D RID: 4413
	public const int SAYAI_NIGHT = 11;

	// Token: 0x0400113E RID: 4414
	public const int KAMISAMA = 12;

	// Token: 0x0400113F RID: 4415
	public const int TIME_ROOM = 13;

	// Token: 0x04001140 RID: 4416
	public const int HELL = 15;

	// Token: 0x04001141 RID: 4417
	public const int BEERUS = 16;

	// Token: 0x04001142 RID: 4418
	public const int THE_HELL = 19;

	// Token: 0x04001143 RID: 4419
	public static Image[] bgItem = new Image[8];

	// Token: 0x04001144 RID: 4420
	public static MyVector vObject = new MyVector();

	// Token: 0x04001145 RID: 4421
	public static int[] offlineId = new int[] { 21, 22, 23, 39, 40, 41 };

	// Token: 0x04001146 RID: 4422
	public static int[] highterId = new int[] { 21, 22, 23, 24, 25, 26 };

	// Token: 0x04001147 RID: 4423
	public static int[] toOfflineId = new int[] { 0, 7, 14 };

	// Token: 0x04001148 RID: 4424
	public static int[][] tileType;

	// Token: 0x04001149 RID: 4425
	public static int[][][] tileIndex;

	// Token: 0x0400114A RID: 4426
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x0400114B RID: 4427
	public static int sizeMiniMap = 2;

	// Token: 0x0400114C RID: 4428
	public static int gssx;

	// Token: 0x0400114D RID: 4429
	public static int gssxe;

	// Token: 0x0400114E RID: 4430
	public static int gssy;

	// Token: 0x0400114F RID: 4431
	public static int gssye;

	// Token: 0x04001150 RID: 4432
	public static int countx;

	// Token: 0x04001151 RID: 4433
	public static int county;

	// Token: 0x04001152 RID: 4434
	internal static int[] colorMini = new int[] { 5257738, 8807192 };

	// Token: 0x04001153 RID: 4435
	public static int yWater = 0;
}
