using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class Image
{
	// Token: 0x0600043C RID: 1084 RVA: 0x0004AA57 File Offset: 0x00048C57
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0004AA5E File Offset: 0x00048C5E
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0004AA66 File Offset: 0x00048C66
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0004AA6E File Offset: 0x00048C6E
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0004AA7D File Offset: 0x00048C7D
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0004AA86 File Offset: 0x00048C86
	public static Image createImage(Image img)
	{
		Image image = Image.createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0004AAB0 File Offset: 0x00048CB0
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

	// Token: 0x06000443 RID: 1091 RVA: 0x0004AAED File Offset: 0x00048CED
	public static byte convertSbyteToByte(sbyte var)
	{
		if (var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0004AB00 File Offset: 0x00048D00
	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if (var[i] > 0)
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

	// Token: 0x06000445 RID: 1093 RVA: 0x0004AB44 File Offset: 0x00048D44
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

	// Token: 0x06000446 RID: 1094 RVA: 0x0004ABA0 File Offset: 0x00048DA0
	public static Color setColorFromRBG(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		float num3 = (float)((rgb >> 16) & 255);
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color(num3 / 256f, num5, num4);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0004ABEC File Offset: 0x00048DEC
	public static void update()
	{
		if (Image.status == 2)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
			return;
		}
		if (Image.status == 3)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.filenametemp);
			Image.status = 0;
			return;
		}
		if (Image.status == 4)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.datatemp);
			Image.status = 0;
			return;
		}
		if (Image.status == 5)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
			Image.status = 0;
			return;
		}
		if (Image.status == 6)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
			Image.status = 0;
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0004ACC8 File Offset: 0x00048EC8
	internal static Image _createEmptyImage()
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

	// Token: 0x06000449 RID: 1097 RVA: 0x0004AD30 File Offset: 0x00048F30
	internal static Image _createImage(string filename)
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

	// Token: 0x0600044A RID: 1098 RVA: 0x0004ADB0 File Offset: 0x00048FB0
	internal static Image _createImage(byte[] imageData)
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

	// Token: 0x0600044B RID: 1099 RVA: 0x0004AE1C File Offset: 0x0004901C
	internal static Image _createImage(Image src, int x, int y, int w, int h, int transform)
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

	// Token: 0x0600044C RID: 1100 RVA: 0x0004AEA8 File Offset: 0x000490A8
	internal static Image _createImage(int w, int h)
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

	// Token: 0x0600044D RID: 1101 RVA: 0x0004AF1C File Offset: 0x0004911C
	public static byte[] loadData(string filename)
	{
		new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + ArrayCast.cast(textAsset.bytes).Length.ToString());
		return textAsset.bytes;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0004AF9C File Offset: 0x0004919C
	internal static Image __createImage(string filename)
	{
		Image image = new Image();
		Texture2D texture2D = Resources.Load(filename) as Texture2D;
		if (texture2D == null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = texture2D;
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0004B004 File Offset: 0x00049204
	internal static Image __createImage(byte[] imageData)
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
		catch (Exception)
		{
			Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
		}
		return image;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0004B08C File Offset: 0x0004928C
	internal static Image __createImage(Image src, int x, int y, int w, int h, int transform)
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
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + j));
			}
		}
		image.texture.Apply();
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0004B135 File Offset: 0x00049335
	internal static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0004B13C File Offset: 0x0004933C
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

	// Token: 0x06000453 RID: 1107 RVA: 0x0004B171 File Offset: 0x00049371
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0004B179 File Offset: 0x00049379
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0004B181 File Offset: 0x00049381
	public int getWidth()
	{
		return this.w / mGraphics.zoomLevel;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0004B18F File Offset: 0x0004938F
	public int getHeight()
	{
		return this.h / mGraphics.zoomLevel;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0004B19D File Offset: 0x0004939D
	internal static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0004B1AA File Offset: 0x000493AA
	public static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0004B1CC File Offset: 0x000493CC
	public Color[] getColor()
	{
		return this.texture.GetPixels();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0004B1D9 File Offset: 0x000493D9
	public int getRealImageWidth()
	{
		return this.w;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0004B1E1 File Offset: 0x000493E1
	public int getRealImageHeight()
	{
		return this.h;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0004B1EC File Offset: 0x000493EC
	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}

	// Token: 0x040008F1 RID: 2289
	internal const int INTERVAL = 5;

	// Token: 0x040008F2 RID: 2290
	internal const int MAXTIME = 500;

	// Token: 0x040008F3 RID: 2291
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x040008F4 RID: 2292
	public static Image imgTemp;

	// Token: 0x040008F5 RID: 2293
	public static string filenametemp;

	// Token: 0x040008F6 RID: 2294
	public static byte[] datatemp;

	// Token: 0x040008F7 RID: 2295
	public static Image imgSrcTemp;

	// Token: 0x040008F8 RID: 2296
	public static int xtemp;

	// Token: 0x040008F9 RID: 2297
	public static int ytemp;

	// Token: 0x040008FA RID: 2298
	public static int wtemp;

	// Token: 0x040008FB RID: 2299
	public static int htemp;

	// Token: 0x040008FC RID: 2300
	public static int transformtemp;

	// Token: 0x040008FD RID: 2301
	public int w;

	// Token: 0x040008FE RID: 2302
	public int h;

	// Token: 0x040008FF RID: 2303
	public static int status;

	// Token: 0x04000900 RID: 2304
	public Color colorBlend = Color.black;
}
