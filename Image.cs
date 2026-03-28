using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Image
{
	// Token: 0x06000032 RID: 50 RVA: 0x0000422A File Offset: 0x0000242A
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004231 File Offset: 0x00002431
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004239 File Offset: 0x00002439
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00004241 File Offset: 0x00002441
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004250 File Offset: 0x00002450
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000099A0 File Offset: 0x00007BA0
	public static Image createImage(Image img)
	{
		Image image = Image.createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000099D8 File Offset: 0x00007BD8
	public static Image createImage(sbyte[] imageData, int offset, int lenght)
	{
		if (offset + lenght > imageData.Length)
		{
			return null;
		}
		byte[] array = new byte[lenght];
		for (int i = 0; i < lenght; i++)
		{
			array[i] = Image.convertSbyteToByte(imageData[i + offset]);
		}
		return Image.createImage(array);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004259 File Offset: 0x00002459
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00009A20 File Offset: 0x00007C20
	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00009A70 File Offset: 0x00007C70
	public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
	{
		Image image = Image.createImage(w, h);
		Color[] array = new Color[rbg.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = Image.setColorFromRBG(rbg[i]);
		}
		image.texture.SetPixels(0, 0, w, h, array);
		image.texture.Apply();
		return image;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00009AD4 File Offset: 0x00007CD4
	public static Color setColorFromRBG(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		Color result = new Color(r, g, b);
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00009B2C File Offset: 0x00007D2C
	public static void update()
	{
		if (Image.status == 2)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
		}
		else if (Image.status == 3)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.filenametemp);
			Image.status = 0;
		}
		else if (Image.status == 4)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.datatemp);
			Image.status = 0;
		}
		else if (Image.status == 5)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
			Image.status = 0;
		}
		else if (Image.status == 6)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
			Image.status = 0;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00009C24 File Offset: 0x00007E24
	private static Image _createEmptyImage()
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00009CA0 File Offset: 0x00007EA0
	private static Image _createImage(string filename)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.filenametemp = filename;
		Image.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00009D34 File Offset: 0x00007F34
	private static Image _createImage(byte[] imageData)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.datatemp = imageData;
		Image.status = 4;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00009DB4 File Offset: 0x00007FB4
	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.imgSrcTemp = src;
		Image.xtemp = x;
		Image.ytemp = y;
		Image.wtemp = w;
		Image.htemp = h;
		Image.transformtemp = transform;
		Image.status = 5;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00009E54 File Offset: 0x00008054
	private static Image _createImage(int w, int h)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.wtemp = w;
		Image.htemp = h;
		Image.status = 6;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00009EDC File Offset: 0x000080DC
	public static byte[] loadData(string filename)
	{
		Image image = new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		sbyte[] array = ArrayCast.cast(textAsset.bytes);
		Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length);
		return textAsset.bytes;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00009F64 File Offset: 0x00008164
	private static Image __createImage(string filename)
	{
		Image image = new Image();
		Texture2D x = Resources.Load(filename) as Texture2D;
		if (x == null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = x;
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00009FD0 File Offset: 0x000081D0
	private static Image __createImage(byte[] imageData)
	{
		if (imageData == null || imageData.Length == 0)
		{
			Cout.LogError("Create Image from byte array fail");
			return null;
		}
		Image image = new Image();
		try
		{
			image.texture.LoadImage(imageData);
			image.w = image.texture.width;
			image.h = image.texture.height;
			Image.setTextureQuality(image);
		}
		catch (Exception ex)
		{
			Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
		}
		return image;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000A064 File Offset: 0x00008264
	private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		y = src.texture.height - y - h;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int num = i;
				if (transform == 2)
				{
					num = w - i;
				}
				int num2 = j;
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
			}
		}
		image.texture.Apply();
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000426F File Offset: 0x0000246F
	private static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000A120 File Offset: 0x00008320
	public static Image __createImage(int w, int h)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h, TextureFormat.RGBA32, false);
		Image.setTextureQuality(image);
		image.w = w;
		image.h = h;
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00004276 File Offset: 0x00002476
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000427E File Offset: 0x0000247E
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004286 File Offset: 0x00002486
	public int getWidth()
	{
		return this.w / mGraphics.zoomLevel;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004294 File Offset: 0x00002494
	public int getHeight()
	{
		return this.h / mGraphics.zoomLevel;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000042A2 File Offset: 0x000024A2
	private static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000042AF File Offset: 0x000024AF
	public static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000042D1 File Offset: 0x000024D1
	public Color[] getColor()
	{
		return this.texture.GetPixels();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000042DE File Offset: 0x000024DE
	public int getRealImageWidth()
	{
		return this.w;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000042E6 File Offset: 0x000024E6
	public int getRealImageHeight()
	{
		return this.h;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000A164 File Offset: 0x00008364
	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}

	// Token: 0x04000010 RID: 16
	private const int INTERVAL = 5;

	// Token: 0x04000011 RID: 17
	private const int MAXTIME = 500;

	// Token: 0x04000012 RID: 18
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x04000013 RID: 19
	public static Image imgTemp;

	// Token: 0x04000014 RID: 20
	public static string filenametemp;

	// Token: 0x04000015 RID: 21
	public static byte[] datatemp;

	// Token: 0x04000016 RID: 22
	public static Image imgSrcTemp;

	// Token: 0x04000017 RID: 23
	public static int xtemp;

	// Token: 0x04000018 RID: 24
	public static int ytemp;

	// Token: 0x04000019 RID: 25
	public static int wtemp;

	// Token: 0x0400001A RID: 26
	public static int htemp;

	// Token: 0x0400001B RID: 27
	public static int transformtemp;

	// Token: 0x0400001C RID: 28
	public int w;

	// Token: 0x0400001D RID: 29
	public int h;

	// Token: 0x0400001E RID: 30
	public static int status;

	// Token: 0x0400001F RID: 31
	public Color colorBlend = Color.black;
}
