using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class Res
{
	// Token: 0x0600077E RID: 1918 RVA: 0x0007681C File Offset: 0x00074A1C
	public static void init()
	{
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			if (Res.cosz[i] == 0)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00076890 File Offset: 0x00074A90
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.sinz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)Res.sinz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.sinz[a - 180]);
		}
		return (int)(-(int)Res.sinz[360 - a]);
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00076900 File Offset: 0x00074B00
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.cosz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)(-(int)Res.cosz[180 - a]);
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.cosz[a - 180]);
		}
		return (int)Res.cosz[360 - a];
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00076970 File Offset: 0x00074B70
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return Res.tanz[a];
		}
		if (a >= 90 && a < 180)
		{
			return -Res.tanz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return Res.tanz[a - 180];
		}
		return -Res.tanz[360 - a];
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x000769E0 File Offset: 0x00074BE0
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (Res.tanz[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00076A08 File Offset: 0x00074C08
	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			num = Res.atan(Math2.abs((dy << 10) / dx));
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00076A6A File Offset: 0x00074C6A
	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle -= 360;
		}
		if (angle < 0)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0002DFE2 File Offset: 0x0002C1E2
	public static sbyte[] TakeSnapShot()
	{
		return null;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0002AFDB File Offset: 0x000291DB
	public static void outz(string s)
	{
		if (mSystem.isTest)
		{
			Debug.Log(s);
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00076A8B File Offset: 0x00074C8B
	public static void outz(string s, int logIndex)
	{
		if (mSystem.isTest)
		{
			Debug.Log(Res.LOG_CAT[logIndex] + s);
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0002AFEA File Offset: 0x000291EA
	public static void err(string s)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(s);
		}
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00004887 File Offset: 0x00002A87
	public static void outz2(string s)
	{
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00004887 File Offset: 0x00002A87
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00004887 File Offset: 0x00002A87
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00004887 File Offset: 0x00002A87
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00076AA6 File Offset: 0x00074CA6
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00058DC8 File Offset: 0x00056FC8
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00076AA9 File Offset: 0x00074CA9
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00076ABB File Offset: 0x00074CBB
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00076ACD File Offset: 0x00074CCD
	public static int random(int a, int b)
	{
		if (a == b)
		{
			return a;
		}
		return a + Res.r.nextInt(b - a);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00076AE4 File Offset: 0x00074CE4
	public static int random(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00076AF4 File Offset: 0x00074CF4
	public static int random_Am(int a, int b)
	{
		int num = a + Res.r.nextInt(b - a);
		if (Res.random(2) == 0)
		{
			num = -num;
		}
		return num;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00076B20 File Offset: 0x00074D20
	public static int random_Am_0(int a)
	{
		int num;
		for (num = 0; num == 0; num = Res.r.nextInt() % a)
		{
		}
		return num;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00076B44 File Offset: 0x00074D44
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		if (currentTimeMillis * 16 % 1000 >= 5)
		{
			num++;
		}
		return num;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00076B70 File Offset: 0x00074D70
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00076B86 File Offset: 0x00074D86
	public static int getDistance(int x, int y)
	{
		return Res.sqrt(x * x + y * y);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00076B94 File Offset: 0x00074D94
	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (Math2.abs(num2 - num) > 1);
		return num;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00076AE4 File Offset: 0x00074CE4
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0000DADD File Offset: 0x0000BCDD
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00076BC5 File Offset: 0x00074DC5
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00076BE4 File Offset: 0x00074DE4
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		string[] array;
		if (num >= 0)
		{
			array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00076C34 File Offset: 0x00074E34
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number.ToString() + string.Empty;
			if (num > 0L)
			{
				return text + "," + num.ToString() + text2;
			}
			return text + text2;
		}
		else
		{
			if (number < 1000000L)
			{
				return number.ToString() + string.Empty;
			}
			text2 = mResources.million;
			long num2 = number % 1000000L / 100000L;
			number /= 1000000L;
			text = number.ToString() + string.Empty;
			if (num2 > 0L)
			{
				return text + "," + num2.ToString() + text2;
			}
			return text + text2;
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00076D1C File Offset: 0x00074F1C
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number.ToString() + string.Empty;
			if (num >= 10L)
			{
				if (num % 10L == 0L)
				{
					num /= 10L;
				}
				return text + "," + num.ToString() + text2;
			}
			if (num > 0L)
			{
				return text + ",0" + num.ToString() + text2;
			}
			return text + text2;
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 10000L;
			number /= 1000000L;
			text = number.ToString() + string.Empty;
			if (num2 >= 10L)
			{
				if (num2 % 10L == 0L)
				{
					num2 /= 10L;
				}
				return text + "," + num2.ToString() + text2;
			}
			if (num2 > 0L)
			{
				return text + ",0" + num2.ToString() + text2;
			}
			return text + text2;
		}
		else
		{
			if (number < 10000L)
			{
				return number.ToString() + string.Empty;
			}
			text2 = "k";
			long num3 = number % 1000L / 10L;
			number /= 1000L;
			text = number.ToString() + string.Empty;
			if (num3 >= 10L)
			{
				if (num3 % 10L == 0L)
				{
					num3 /= 10L;
				}
				return text + "," + num3.ToString() + text2;
			}
			if (num3 > 0L)
			{
				return text + ",0" + num3.ToString() + text2;
			}
			return text + text2;
		}
	}

	// Token: 0x04000E89 RID: 3721
	internal static short[] sinz = new short[]
	{
		0, 18, 36, 54, 71, 89, 107, 125, 143, 160,
		178, 195, 213, 230, 248, 265, 282, 299, 316, 333,
		350, 367, 384, 400, 416, 433, 449, 465, 481, 496,
		512, 527, 543, 558, 573, 587, 602, 616, 630, 644,
		658, 672, 685, 698, 711, 724, 737, 749, 761, 773,
		784, 796, 807, 818, 828, 839, 849, 859, 868, 878,
		887, 896, 904, 912, 920, 928, 935, 943, 949, 956,
		962, 968, 974, 979, 984, 989, 994, 998, 1002, 1005,
		1008, 1011, 1014, 1016, 1018, 1020, 1022, 1023, 1023, 1024,
		1024
	};

	// Token: 0x04000E8A RID: 3722
	internal static short[] cosz;

	// Token: 0x04000E8B RID: 3723
	internal static int[] tanz;

	// Token: 0x04000E8C RID: 3724
	public static string[] LOG_CAT = new string[]
	{
		"<color=#ff0000ff>[  LOG_CAT  ]</color>",
		"<color=#ff0000ff>[LOG_SESSION]</color>",
		"<color=#ffff00ff>[LOG_SESSION]</color>",
		"<color=#ff0000ff>[LOG_MOBILE ]</color>",
		string.Empty
	};

	// Token: 0x04000E8D RID: 3725
	public static int count;

	// Token: 0x04000E8E RID: 3726
	public static bool isIcon;

	// Token: 0x04000E8F RID: 3727
	public static bool isBig;

	// Token: 0x04000E90 RID: 3728
	public static MyVector debug = new MyVector();

	// Token: 0x04000E91 RID: 3729
	public static MyRandom r = new MyRandom();
}
