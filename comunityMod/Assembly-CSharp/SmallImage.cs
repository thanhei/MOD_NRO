using System;
using Assets.src.e;

// Token: 0x020000A8 RID: 168
public class SmallImage
{
	// Token: 0x060008E7 RID: 2279 RVA: 0x00081B78 File Offset: 0x0007FD78
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00081B88 File Offset: 0x0007FD88
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

	// Token: 0x060008E9 RID: 2281 RVA: 0x00081BE8 File Offset: 0x0007FDE8
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00081BF5 File Offset: 0x0007FDF5
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00081C0A File Offset: 0x0007FE0A
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00004887 File Offset: 0x00002A87
	public void readData(byte[] data)
	{
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00081C1C File Offset: 0x0007FE1C
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
			Cout.LogError3("Loi readImage: " + ex.ToString() + "i= " + num.ToString());
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00004887 File Offset: 0x00002A87
	public static void clearHastable()
	{
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00081D08 File Offset: 0x0007FF08
	public static void createImage(int id)
	{
		Res.outz("is request =" + id.ToString() + " zoom=" + mGraphics.zoomLevel.ToString());
		if (mGraphics.zoomLevel == 1)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
			if (image != null)
			{
				SmallImage.imgNew[id] = new Small(image, id);
				return;
			}
			SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
			SmallImage.vt_images_watingDowload.addElement(SmallImage.imgNew[id]);
			return;
		}
		else
		{
			Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
			if (image2 != null)
			{
				SmallImage.imgNew[id] = new Small(image2, id);
				return;
			}
			bool flag = false;
			sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "Small" + id.ToString());
			if (array != null)
			{
				if (SmallImage.newSmallVersion != null && array.Length % 127 != (int)SmallImage.newSmallVersion[id])
				{
					flag = true;
				}
				if (!flag)
				{
					Image image3 = Image.createImage(array, 0, array.Length);
					if (image3 != null)
					{
						SmallImage.imgNew[id] = new Small(image3, id);
					}
					else
					{
						flag = true;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
				SmallImage.vt_images_watingDowload.addElement(SmallImage.imgNew[id]);
			}
			return;
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00081E50 File Offset: 0x00080050
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

	// Token: 0x060008F1 RID: 2289 RVA: 0x00081F8C File Offset: 0x0008018C
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

	// Token: 0x060008F2 RID: 2290 RVA: 0x000820EC File Offset: 0x000802EC
	public static void update()
	{
		int num = 0;
		if (GameCanvas.gameTick % 1000 != 0)
		{
			return;
		}
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

	// Token: 0x04000FAC RID: 4012
	public static int[][] smallImg;

	// Token: 0x04000FAD RID: 4013
	public static SmallImage instance;

	// Token: 0x04000FAE RID: 4014
	public static Image[] imgbig;

	// Token: 0x04000FAF RID: 4015
	public static Small[] imgNew;

	// Token: 0x04000FB0 RID: 4016
	public static MyVector vKeys = new MyVector();

	// Token: 0x04000FB1 RID: 4017
	public static Image imgEmpty = null;

	// Token: 0x04000FB2 RID: 4018
	public static sbyte[] newSmallVersion;

	// Token: 0x04000FB3 RID: 4019
	public static MyVector vt_images_watingDowload = new MyVector();

	// Token: 0x04000FB4 RID: 4020
	public static int smallCount;

	// Token: 0x04000FB5 RID: 4021
	public static short maxSmall;
}
