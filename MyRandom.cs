using System;

// Token: 0x0200000F RID: 15
public class MyRandom
{
	// Token: 0x0600006A RID: 106 RVA: 0x00004414 File Offset: 0x00002614
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004427 File Offset: 0x00002627
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004434 File Offset: 0x00002634
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00004442 File Offset: 0x00002642
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000026 RID: 38
	public Random r;
}
