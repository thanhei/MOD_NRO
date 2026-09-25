using System;

// Token: 0x02000039 RID: 57
public class Firework
{
	// Token: 0x060002D7 RID: 727 RVA: 0x00034270 File Offset: 0x00032470
	public Firework(int x0, int y0, int v, int angle, int cl)
	{
		this.y0 = y0;
		this.x0 = x0;
		this.a = 1f;
		this.v = v;
		this.angle = angle;
		this.w = GameCanvas.w;
		this.h = GameCanvas.h;
		this.last = this.time();
		for (int i = 0; i < 2; i++)
		{
			this.arr_x[i] = x0;
			this.arr_y[i] = y0;
		}
		this.cl = cl;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0003432C File Offset: 0x0003252C
	public void preDraw()
	{
		if (this.time() - this.last >= this.delay)
		{
			this.t++;
			this.last = this.time();
			this.arr_x[1] = this.arr_x[0];
			this.arr_y[1] = this.arr_y[0];
			this.arr_x[0] = this.x;
			this.arr_y[0] = this.y;
			this.x = Res.cos((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.v * this.t + this.x0;
			this.y = (int)((float)(this.v * Res.sin((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.t) - this.a * (float)this.t * (float)this.t / 2f) + this.y0;
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00034440 File Offset: 0x00032640
	public void paint(mGraphics g)
	{
		this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
		for (int i = 0; i < 2; i++)
		{
			this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
		}
		if (this.act)
		{
			this.preDraw();
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x000340EB File Offset: 0x000322EB
	public long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x060002DB RID: 731 RVA: 0x000344B9 File Offset: 0x000326B9
	public void Drawline(mGraphics g, int x, int y, int color)
	{
		g.setColor(color);
		g.fillRect(x, y, 1, 2);
	}

	// Token: 0x040005F3 RID: 1523
	public int w;

	// Token: 0x040005F4 RID: 1524
	public int h;

	// Token: 0x040005F5 RID: 1525
	public int v;

	// Token: 0x040005F6 RID: 1526
	public int x0;

	// Token: 0x040005F7 RID: 1527
	public int x;

	// Token: 0x040005F8 RID: 1528
	public int y;

	// Token: 0x040005F9 RID: 1529
	public int y0;

	// Token: 0x040005FA RID: 1530
	public int angle;

	// Token: 0x040005FB RID: 1531
	public int t;

	// Token: 0x040005FC RID: 1532
	public int cl = 16711680;

	// Token: 0x040005FD RID: 1533
	internal float a;

	// Token: 0x040005FE RID: 1534
	internal long last;

	// Token: 0x040005FF RID: 1535
	internal long delay = 150L;

	// Token: 0x04000600 RID: 1536
	internal bool act = true;

	// Token: 0x04000601 RID: 1537
	internal int[] arr_x = new int[2];

	// Token: 0x04000602 RID: 1538
	internal int[] arr_y = new int[2];
}
