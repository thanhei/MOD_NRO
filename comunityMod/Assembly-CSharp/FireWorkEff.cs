using System;

// Token: 0x02000037 RID: 55
public class FireWorkEff
{
	// Token: 0x060002CD RID: 717 RVA: 0x00033E14 File Offset: 0x00032014
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

	// Token: 0x060002CE RID: 718 RVA: 0x00033E80 File Offset: 0x00032080
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
			FireWorkEff.keyPressed(-(Math2.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00033F04 File Offset: 0x00032104
	public static void keyPressed(int k)
	{
		if (k == -5 && !FireWorkEff.st)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
			return;
		}
		if (k == -7 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 60;
			FireWorkEff.x0 = 0;
			FireWorkEff.st = true;
			FireWorkEff.add();
			return;
		}
		if (k == -6 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 120;
			FireWorkEff.x0 = FireWorkEff.w;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00033F8C File Offset: 0x0003218C
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

	// Token: 0x060002D1 RID: 721 RVA: 0x00033FDC File Offset: 0x000321DC
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

	// Token: 0x060002D2 RID: 722 RVA: 0x000340EB File Offset: 0x000322EB
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x040005D9 RID: 1497
	internal static int w;

	// Token: 0x040005DA RID: 1498
	internal static int h;

	// Token: 0x040005DB RID: 1499
	internal static MyRandom r = new MyRandom();

	// Token: 0x040005DC RID: 1500
	internal static MyVector mg = new MyVector();

	// Token: 0x040005DD RID: 1501
	internal static int f = 17;

	// Token: 0x040005DE RID: 1502
	internal static int x;

	// Token: 0x040005DF RID: 1503
	internal static int y;

	// Token: 0x040005E0 RID: 1504
	internal static int ag;

	// Token: 0x040005E1 RID: 1505
	internal static int x0;

	// Token: 0x040005E2 RID: 1506
	internal static int y0;

	// Token: 0x040005E3 RID: 1507
	internal static int t;

	// Token: 0x040005E4 RID: 1508
	internal static int v;

	// Token: 0x040005E5 RID: 1509
	internal static int ymax = 269;

	// Token: 0x040005E6 RID: 1510
	internal static float a;

	// Token: 0x040005E7 RID: 1511
	internal static int[] mang_x = new int[3];

	// Token: 0x040005E8 RID: 1512
	internal static int[] mang_y = new int[3];

	// Token: 0x040005E9 RID: 1513
	internal static bool st = false;

	// Token: 0x040005EA RID: 1514
	internal static long last = 0L;

	// Token: 0x040005EB RID: 1515
	internal static long delay = 150L;
}
