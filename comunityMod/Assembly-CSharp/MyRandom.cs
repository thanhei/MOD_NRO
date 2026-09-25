using System;

// Token: 0x02000078 RID: 120
public class MyRandom
{
	// Token: 0x060005BE RID: 1470 RVA: 0x00057A5F File Offset: 0x00055C5F
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00057A72 File Offset: 0x00055C72
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00057A7F File Offset: 0x00055C7F
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00057A8D File Offset: 0x00055C8D
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000C06 RID: 3078
	public Random r;
}
