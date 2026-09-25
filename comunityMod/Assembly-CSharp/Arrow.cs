using System;

// Token: 0x0200000D RID: 13
public class Arrow
{
	// Token: 0x06000063 RID: 99 RVA: 0x00003A8C File Offset: 0x00001C8C
	public Arrow(global::Char charBelong, Arrowpaint arrp)
	{
		this.charBelong = charBelong;
		this.arrp = arrp;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003AA4 File Offset: 0x00001CA4
	public void update()
	{
		if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
		{
			this.endMe();
			return;
		}
		if (this.charBelong.mobFocus != null)
		{
			this.axTo = this.charBelong.mobFocus.x;
			this.ayTo = this.charBelong.mobFocus.y - this.charBelong.mobFocus.h / 4;
		}
		else if (this.charBelong.charFocus != null)
		{
			this.axTo = this.charBelong.charFocus.cx;
			this.ayTo = this.charBelong.charFocus.cy - this.charBelong.charFocus.ch / 4;
		}
		int num = this.axTo - this.ax;
		int num2 = this.ayTo - this.ay;
		int num3 = 5;
		int num4 = 4;
		if (num + num2 < 60)
		{
			num4 = 3;
		}
		else if (num + num2 < 30)
		{
			num4 = 2;
		}
		if (this.ax != this.axTo)
		{
			if (num > 0 && num < num3)
			{
				this.ax = this.axTo;
			}
			else if (num < 0 && num > -num3)
			{
				this.ax = this.axTo;
			}
			else
			{
				this.avx = this.axTo - this.ax << 2;
				this.adx += this.avx;
				this.ax += this.adx >> num4;
				this.adx &= 15;
			}
		}
		if (this.ay != this.ayTo)
		{
			if (num2 > 0 && num2 < num3)
			{
				this.ay = this.ayTo;
			}
			else if (num2 < 0 && num2 > -num3)
			{
				this.ay = this.ayTo;
			}
			else
			{
				this.avy = this.ayTo - this.ay << 2;
				this.ady += this.avy;
				this.ay += this.ady >> num4;
				this.ady &= 15;
			}
		}
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (this.charBelong.mobFocus != null)
		{
			num5 = this.axTo - this.charBelong.mobFocus.w / 4;
			num7 = this.axTo + this.charBelong.mobFocus.w / 4;
			num6 = this.ayTo - this.charBelong.mobFocus.h / 4;
			num8 = this.ayTo + this.charBelong.mobFocus.h / 4;
		}
		else if (this.charBelong.charFocus != null)
		{
			num5 = this.axTo - this.charBelong.charFocus.cw / 4;
			num7 = this.axTo + this.charBelong.charFocus.cw / 4;
			num6 = this.ayTo - this.charBelong.charFocus.ch / 4;
			num8 = this.ayTo + this.charBelong.charFocus.ch / 4;
		}
		if (this.life > 0)
		{
			this.life--;
		}
		if (this.life == 0 || (this.ax >= num5 && this.ax <= num7 && this.ay >= num6 && this.ay <= num8))
		{
			this.endMe();
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003E04 File Offset: 0x00002004
	internal void endMe()
	{
		this.charBelong.arr = null;
		this.ax = (this.ay = (this.axTo = (this.ayTo = (this.avx = (this.avy = (this.adx = (this.ady = 0)))))));
		this.charBelong.setAttack();
		if (this.charBelong.me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00003E88 File Offset: 0x00002088
	public void paint(mGraphics g)
	{
		int num = Arrow.findDirIndexFromAngle(Res.angle(this.axTo - this.ax, -(this.ayTo - this.ay)));
		SmallImage.drawSmallImage(g, this.arrp.imgId[(int)Arrow.FRAME[num]], this.ax, this.ay, Arrow.TRANSFORM[num], mGraphics.VCENTER | mGraphics.HCENTER);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003EF4 File Offset: 0x000020F4
	public static int findDirIndexFromAngle(int angle)
	{
		int i = 0;
		while (i < Arrow.ARROWINDEX.Length - 1)
		{
			if (angle >= Arrow.ARROWINDEX[i] && angle <= Arrow.ARROWINDEX[i + 1])
			{
				if (i >= 16)
				{
					return 0;
				}
				return i;
			}
			else
			{
				i++;
			}
		}
		return 0;
	}

	// Token: 0x0400003F RID: 63
	public int life;

	// Token: 0x04000040 RID: 64
	public int ax;

	// Token: 0x04000041 RID: 65
	public int ay;

	// Token: 0x04000042 RID: 66
	public int axTo;

	// Token: 0x04000043 RID: 67
	public int ayTo;

	// Token: 0x04000044 RID: 68
	public int avx;

	// Token: 0x04000045 RID: 69
	public int avy;

	// Token: 0x04000046 RID: 70
	public int adx;

	// Token: 0x04000047 RID: 71
	public int ady;

	// Token: 0x04000048 RID: 72
	public global::Char charBelong;

	// Token: 0x04000049 RID: 73
	public Arrowpaint arrp;

	// Token: 0x0400004A RID: 74
	public static sbyte[] FRAME = new sbyte[]
	{
		0, 1, 2, 1, 0, 1, 2, 1, 0, 1,
		2, 1, 0, 1, 2, 1, 0, 1, 2, 1,
		0, 1, 2, 1, 0
	};

	// Token: 0x0400004B RID: 75
	public static int[] ARROWINDEX = new int[]
	{
		0, 15, 37, 52, 75, 105, 127, 142, 165, 195,
		217, 232, 255, 285, 307, 322, 345, 370
	};

	// Token: 0x0400004C RID: 76
	public static int[] TRANSFORM = new int[]
	{
		0, 0, 0, 7, 6, 6, 6, 2, 2, 3,
		3, 4, 5, 5, 5, 1
	};
}
