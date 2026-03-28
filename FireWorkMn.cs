using System;

// Token: 0x02000040 RID: 64
public class FireWorkMn
{
	// Token: 0x06000292 RID: 658 RVA: 0x00019D38 File Offset: 0x00017F38
	public FireWorkMn(int x, int y, int goc, int n)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
		this.n = n;
		for (int i = 0; i < n; i++)
		{
			this.fw.addElement(new Firework(x, y, global::Math.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[global::Math.abs(this.rd.nextInt() % this.color.Length)]));
		}
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00019E04 File Offset: 0x00018004
	public void paint(mGraphics g)
	{
		for (int i = 0; i < this.fw.size(); i++)
		{
			Firework firework = (Firework)this.fw.elementAt(i);
			if (firework.y < -200)
			{
				this.fw.removeElementAt(i);
			}
			firework.paint(g);
		}
	}

	// Token: 0x04000317 RID: 791
	private int x;

	// Token: 0x04000318 RID: 792
	private int y;

	// Token: 0x04000319 RID: 793
	private int goc = 1;

	// Token: 0x0400031A RID: 794
	private int n = 360;

	// Token: 0x0400031B RID: 795
	private MyRandom rd = new MyRandom();

	// Token: 0x0400031C RID: 796
	private MyVector fw = new MyVector();

	// Token: 0x0400031D RID: 797
	private int[] color = new int[]
	{
		16711680,
		16776960,
		65280,
		16777215,
		255,
		65535,
		15790320,
		12632256
	};
}
