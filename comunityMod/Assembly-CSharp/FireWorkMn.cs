using System;

// Token: 0x02000038 RID: 56
public class FireWorkMn
{
	// Token: 0x060002D5 RID: 725 RVA: 0x00034154 File Offset: 0x00032354
	public FireWorkMn(int x, int y, int goc, int n)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
		this.n = n;
		for (int i = 0; i < n; i++)
		{
			this.fw.addElement(new Firework(x, y, Math2.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[Math2.abs(this.rd.nextInt() % this.color.Length)]));
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0003421C File Offset: 0x0003241C
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

	// Token: 0x040005EC RID: 1516
	internal int x;

	// Token: 0x040005ED RID: 1517
	internal int y;

	// Token: 0x040005EE RID: 1518
	internal int goc = 1;

	// Token: 0x040005EF RID: 1519
	internal int n = 360;

	// Token: 0x040005F0 RID: 1520
	internal MyRandom rd = new MyRandom();

	// Token: 0x040005F1 RID: 1521
	internal MyVector fw = new MyVector();

	// Token: 0x040005F2 RID: 1522
	internal int[] color = new int[] { 16711680, 16776960, 65280, 16777215, 255, 65535, 15790320, 12632256 };
}
