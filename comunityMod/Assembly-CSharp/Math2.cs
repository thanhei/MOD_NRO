using System;

// Token: 0x02000068 RID: 104
public class Math2
{
	// Token: 0x06000532 RID: 1330 RVA: 0x0000DADD File Offset: 0x0000BCDD
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x000522FD File Offset: 0x000504FD
	public static int min(int x, int y)
	{
		if (x < y)
		{
			return x;
		}
		return y;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00052306 File Offset: 0x00050506
	public static int max(int x, int y)
	{
		if (x > y)
		{
			return x;
		}
		return y;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00052310 File Offset: 0x00050510
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000B02 RID: 2818
	public const double PI = 3.141592653589793;
}
