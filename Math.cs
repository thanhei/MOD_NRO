using System;

// Token: 0x0200000B RID: 11
public class Math
{
	// Token: 0x06000057 RID: 87 RVA: 0x0000430E File Offset: 0x0000250E
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000431F File Offset: 0x0000251F
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000432F File Offset: 0x0000252F
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000A1B8 File Offset: 0x000083B8
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000020 RID: 32
	public const double PI = 3.141592653589793;
}
