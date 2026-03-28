using System;

// Token: 0x0200003F RID: 63
public class FireWorkEff
{
	// Token: 0x0600028B RID: 651 RVA: 0x000199C8 File Offset: 0x00017BC8
	public static void preDraw()
	{
		if (FireWorkEff.st)
		{
			FireWorkEff.animate();
		}
		if (FireWorkEff.t > 32 && FireWorkEff.st)
		{
			FireWorkEff.st = false;
			FireWorkEff.mg.removeAllElements();
			FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00019A40 File Offset: 0x00017C40
	public static void paint(mGraphics g)
	{
		FireWorkEff.preDraw();
		g.setColor(0);
		g.fillRect(0, 0, FireWorkEff.w, FireWorkEff.h);
		g.setColor(16711680);
		for (int i = 0; i < FireWorkEff.mg.size(); i++)
		{
			((FireWorkMn)FireWorkEff.mg.elementAt(i)).paint(g);
		}
		if (!FireWorkEff.st)
		{
			FireWorkEff.keyPressed(-(global::Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00019ACC File Offset: 0x00017CCC
	public static void keyPressed(int k)
	{
		if (k == -5 && !FireWorkEff.st)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -7 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 60;
			FireWorkEff.x0 = 0;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -6 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 120;
			FireWorkEff.x0 = FireWorkEff.w;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00019B6C File Offset: 0x00017D6C
	public static void add()
	{
		FireWorkEff.y0 = 0;
		FireWorkEff.v = 16;
		FireWorkEff.t = 0;
		FireWorkEff.a = 0f;
		for (int i = 0; i < 3; i++)
		{
			FireWorkEff.mang_y[i] = 0;
			FireWorkEff.mang_x[i] = FireWorkEff.x0;
		}
		FireWorkEff.st = true;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00019BC4 File Offset: 0x00017DC4
	public static void animate()
	{
		FireWorkEff.mang_y[2] = FireWorkEff.mang_y[1];
		FireWorkEff.mang_x[2] = FireWorkEff.mang_x[1];
		FireWorkEff.mang_y[1] = FireWorkEff.mang_y[0];
		FireWorkEff.mang_x[1] = FireWorkEff.mang_x[0];
		FireWorkEff.mang_y[0] = FireWorkEff.y;
		FireWorkEff.mang_x[0] = FireWorkEff.x;
		FireWorkEff.x = Res.cos((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.v * FireWorkEff.t + FireWorkEff.x0;
		FireWorkEff.y = (int)((float)(FireWorkEff.v * Res.sin((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.t) - FireWorkEff.a * (float)FireWorkEff.t * (float)FireWorkEff.t / 2f) + FireWorkEff.y0;
		if (FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay)
		{
			FireWorkEff.t++;
			FireWorkEff.last = FireWorkEff.time();
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000056CC File Offset: 0x000038CC
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x04000304 RID: 772
	private static int w;

	// Token: 0x04000305 RID: 773
	private static int h;

	// Token: 0x04000306 RID: 774
	private static MyRandom r = new MyRandom();

	// Token: 0x04000307 RID: 775
	private static MyVector mg = new MyVector();

	// Token: 0x04000308 RID: 776
	private static int f = 17;

	// Token: 0x04000309 RID: 777
	private static int x;

	// Token: 0x0400030A RID: 778
	private static int y;

	// Token: 0x0400030B RID: 779
	private static int ag;

	// Token: 0x0400030C RID: 780
	private static int x0;

	// Token: 0x0400030D RID: 781
	private static int y0;

	// Token: 0x0400030E RID: 782
	private static int t;

	// Token: 0x0400030F RID: 783
	private static int v;

	// Token: 0x04000310 RID: 784
	private static int ymax = 269;

	// Token: 0x04000311 RID: 785
	private static float a;

	// Token: 0x04000312 RID: 786
	private static int[] mang_x = new int[3];

	// Token: 0x04000313 RID: 787
	private static int[] mang_y = new int[3];

	// Token: 0x04000314 RID: 788
	private static bool st = false;

	// Token: 0x04000315 RID: 789
	private static long last = 0L;

	// Token: 0x04000316 RID: 790
	private static long delay = 150L;
}
