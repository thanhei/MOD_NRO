using System;
using Assets.src.e;

// Token: 0x02000088 RID: 136
public class SmallImage
{
	// Token: 0x06000466 RID: 1126 RVA: 0x00006612 File Offset: 0x00004812
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000299BC File Offset: 0x00027BBC
	public static void loadBigRMS()
	{
		if (SmallImage.imgbig == null)
		{
			SmallImage.imgbig = new Image[]
			{
				GameCanvas.loadImageRMS("/img/Big0.png"),
				GameCanvas.loadImageRMS("/img/Big1.png"),
				GameCanvas.loadImageRMS("/img/Big2.png"),
				GameCanvas.loadImageRMS("/img/Big3.png"),
				GameCanvas.loadImageRMS("/img/Big4.png")
			};
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00006620 File Offset: 0x00004820
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0000662D File Offset: 0x0000482D
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00006642 File Offset: 0x00004842
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x000045ED File Offset: 0x000027ED
	public void readData(byte[] data)
	{
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00029A1C File Offset: 0x00027C1C
	public void readImage()
	{
		int num = 0;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NR_image"));
			short num2 = dataInputStream.readShort();
			SmallImage.smallImg = new int[(int)num2][];
			for (int i = 0; i < SmallImage.smallImg.Length; i++)
			{
				SmallImage.smallImg[i] = new int[5];
			}
			for (int j = 0; j < (int)num2; j++)
			{
				num++;
				SmallImage.smallImg[j][0] = dataInputStream.readUnsignedByte();
				SmallImage.smallImg[j][1] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][2] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][3] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][4] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError3(string.Concat(new object[]
			{
				"Loi readImage: ",
				ex.ToString(),
				"i= ",
				num
			}));
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x000045ED File Offset: 0x000027ED
	public static void clearHastable()
	{
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00029B1C File Offset: 0x00027D1C
	public static void createImage(int id)
	{
		Image image = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
		if (image != null)
		{
			SmallImage.imgNew[id] = new Small(image, id);
			return;
		}
		bool flag = false;
		string text = mGraphics.zoomLevel + "Small" + id;
		sbyte[] array = Rms.loadRMS(text);
		if (array == null)
		{
			text = "1Small" + id;
			array = Rms.loadRMS(text);
		}
		if (array != null)
		{
			bool flag2 = text.StartsWith("1Small");
			if (!flag2 && SmallImage.newSmallVersion != null)
			{
				int num = (int)SmallImage.newSmallVersion[id];
				if (array.Length % 127 != num)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Image image2 = Image.createImage(array, 0, array.Length);
				if (image2 != null)
				{
					SmallImage.imgNew[id] = new Small(image2, id);
					if (flag2)
					{
						Rms.saveRMS(mGraphics.zoomLevel + "Small" + id, array);
					}
					return;
				}
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
			if (GameCanvas.currentScreen == GameCanvas._SelectCharScr)
			{
				Service.gI().requestIcon(id);
				return;
			}
			SmallImage.vt_images_watingDowload.addElement(SmallImage.imgNew[id]);
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00029C50 File Offset: 0x00027E50
	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		if (SmallImage.imgbig != null)
		{
			if (SmallImage.smallImg != null)
			{
				if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
				{
					Small small = SmallImage.imgNew[id];
					if (small == null)
					{
						SmallImage.createImage(id);
						return;
					}
					small.paint(g, transform, x, y, anchor);
					return;
				}
				else if (SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
				{
					g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
					return;
				}
			}
			else if (GameCanvas.currentScreen != GameScr.gI())
			{
				Small small2 = SmallImage.imgNew[id];
				if (small2 == null)
				{
					SmallImage.createImage(id);
					return;
				}
				small2.paint(g, transform, x, y, anchor);
			}
			return;
		}
		Small small3 = SmallImage.imgNew[id];
		if (small3 == null)
		{
			SmallImage.createImage(id);
			return;
		}
		g.drawRegion(small3, 0, 0, mGraphics.getImageWidth(small3.img), mGraphics.getImageHeight(small3.img), transform, x, y, anchor);
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00029D8C File Offset: 0x00027F8C
	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		if (SmallImage.imgbig == null)
		{
			Small small = SmallImage.imgNew[id];
			if (small == null)
			{
				SmallImage.createImage(id);
				return;
			}
			g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
			return;
		}
		else
		{
			if (SmallImage.smallImg == null)
			{
				if (GameCanvas.currentScreen != GameScr.gI())
				{
					Small small2 = SmallImage.imgNew[id];
					if (small2 == null)
					{
						SmallImage.createImage(id);
						return;
					}
					small2.paint(g, transform, f, x, y, w, h, anchor);
				}
				return;
			}
			if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
			{
				Small small3 = SmallImage.imgNew[id];
				if (small3 == null)
				{
					SmallImage.createImage(id);
					return;
				}
				small3.paint(g, transform, f, x, y, w, h, anchor);
				return;
			}
			else
			{
				if (SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
				{
					g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
					return;
				}
				Small small4 = SmallImage.imgNew[id];
				if (small4 == null)
				{
					SmallImage.createImage(id);
					return;
				}
				small4.paint(g, transform, f, x, y, w, h, anchor);
				return;
			}
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00029EEC File Offset: 0x000280EC
	public static void update()
	{
		int num = 0;
		if (GameCanvas.gameTick % 1000 == 0)
		{
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				if (SmallImage.imgNew[i] != null)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			if (num > 200 && GameCanvas.lowGraphic)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}
	}

	// Token: 0x040007DE RID: 2014
	public static int[][] smallImg;

	// Token: 0x040007DF RID: 2015
	public static SmallImage instance;

	// Token: 0x040007E0 RID: 2016
	public static Image[] imgbig;

	// Token: 0x040007E1 RID: 2017
	public static Small[] imgNew;

	// Token: 0x040007E2 RID: 2018
	public static MyVector vKeys = new MyVector();

	// Token: 0x040007E3 RID: 2019
	public static Image imgEmpty = null;

	// Token: 0x040007E4 RID: 2020
	public static sbyte[] newSmallVersion;

	// Token: 0x040007E5 RID: 2021
	public static MyVector vt_images_watingDowload = new MyVector();

	// Token: 0x040007E6 RID: 2022
	public static int smallCount;

	// Token: 0x040007E7 RID: 2023
	public static short maxSmall;
}
