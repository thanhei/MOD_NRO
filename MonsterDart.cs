using System;

// Token: 0x02000047 RID: 71
public class MonsterDart : Effect2
{
	// Token: 0x060002AF RID: 687 RVA: 0x0001AD0C File Offset: 0x00018F0C
	public MonsterDart(int x, int y, bool isBoss, long dame, long dameMp, global::Char c, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.c = c;
		this.va = this.info.va;
		this.setAngle(Res.angle(c.cx - x, c.cy - y));
		if (x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w)
		{
			SoundMn.gI().mobKame(dartType);
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0001ADBC File Offset: 0x00018FBC
	public MonsterDart(int x, int y, bool isBoss, long dame, long dameMp, int xTo, int yTo, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.xTo = xTo;
		this.yTo = yTo;
		this.va = this.info.va;
		this.setAngle(Res.angle(xTo - x, yTo - y));
		if (x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w)
		{
			SoundMn.gI().mobKame(dartType);
		}
		this.c = null;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x000057DC File Offset: 0x000039DC
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00005811 File Offset: 0x00003A11
	public static void addMonsterDart(int x, int y, bool isBoss, long dame, long dameMp, global::Char c, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0001AE70 File Offset: 0x00019070
	public static void addMonsterDart(int x, int y, bool isBoss, long dame, long dameMp, int xTo, int yTo, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, xTo, yTo, dartType));
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0001AE98 File Offset: 0x00019098
	public override void update()
	{
		for (int i = 0; i < (int)this.info.nUpdate; i++)
		{
			if (this.info.tail.Length > 0)
			{
				this.darts.addElement(new SmallDart(this.x, this.y));
			}
			this.dx = ((this.c == null) ? this.xTo : this.c.cx) - this.x;
			this.dy = ((this.c == null) ? this.yTo : this.c.cy) - 10 - this.y;
			int num = 60;
			if (TileMap.mapID == 0)
			{
				num = 600;
			}
			this.life++;
			if ((this.c != null && (this.c.statusMe == 5 || this.c.statusMe == 14)) || this.c == null)
			{
				this.x += (((this.c == null) ? this.xTo : this.c.cx) - this.x) / 2;
				this.y += (((this.c == null) ? this.yTo : this.c.cy) - this.y) / 2;
			}
			if ((Res.abs(this.dx) < 16 && Res.abs(this.dy) < 16) || this.life > num)
			{
				if (this.c != null && this.c.charID >= 0 && this.dameMp != -1L)
				{
					if (this.dameMp != -100L)
					{
						this.c.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						ServerEffect.addServerEffect(80, this.c, 1);
					}
				}
				Effect2.vEffect2.removeElement(this);
				if (this.dameMp != -100L)
				{
					ServerEffect.addServerEffect(81, this.c, 1);
					if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
					{
						SoundMn.gI().explode_2();
					}
				}
			}
			int num2 = Res.angle(this.dx, this.dy);
			if (global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096)
			{
				if (global::Math.abs(num2 - this.angle) < 15)
				{
					this.angle = num2;
				}
				else if ((num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180)
				{
					this.angle = Res.fixangle(this.angle + 15);
				}
				else
				{
					this.angle = Res.fixangle(this.angle - 15);
				}
			}
			if (!this.isSpeedUp && this.va < 8192)
			{
				this.va += 1024;
			}
			this.vx = this.va * Res.cos(this.angle) >> 10;
			this.vy = this.va * Res.sin(this.angle) >> 10;
			this.dx += this.vx;
			int num3 = this.dx >> 10;
			this.x += num3;
			this.dx &= 1023;
			this.dy += this.vy;
			int num4 = this.dy >> 10;
			this.y += num4;
			this.dy &= 1023;
		}
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
			smallDart.index++;
			if (smallDart.index >= this.info.tail.Length)
			{
				this.darts.removeElementAt(j);
			}
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0001B314 File Offset: 0x00019514
	public static int findDirIndexFromAngle(int angle)
	{
		int i = 0;
		while (i < MonsterDart.ARROWINDEX.Length - 1)
		{
			if (angle >= MonsterDart.ARROWINDEX[i] && angle <= MonsterDart.ARROWINDEX[i + 1])
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

	// Token: 0x060002B6 RID: 694 RVA: 0x0001B364 File Offset: 0x00019564
	public override void paint(mGraphics g)
	{
		int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
		int num2 = (int)MonsterDart.FRAME[num];
		int transform = MonsterDart.TRANSFORM[num];
		for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
			SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
		}
		int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
		SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
			SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
		}
		SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
		for (int k = 0; k < this.darts.size(); k++)
		{
			SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
			if (Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent)
			{
				SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000582C File Offset: 0x00003A2C
	public static void addMonsterDart(int x2, int y2, bool checkIsBoss, int dame2, int dameMp2, Mob mobToAttack, sbyte dartType)
	{
		MonsterDart.addMonsterDart(x2, y2, checkIsBoss, (long)dame2, (long)dameMp2, mobToAttack.x, mobToAttack.y, (int)dartType);
	}

	// Token: 0x04000357 RID: 855
	public int va;

	// Token: 0x04000358 RID: 856
	private DartInfo info;

	// Token: 0x04000359 RID: 857
	public static MyRandom r = new MyRandom();

	// Token: 0x0400035A RID: 858
	public int angle;

	// Token: 0x0400035B RID: 859
	public int vx;

	// Token: 0x0400035C RID: 860
	public int vy;

	// Token: 0x0400035D RID: 861
	public int x;

	// Token: 0x0400035E RID: 862
	public int y;

	// Token: 0x0400035F RID: 863
	public int z;

	// Token: 0x04000360 RID: 864
	public int xTo;

	// Token: 0x04000361 RID: 865
	public int yTo;

	// Token: 0x04000362 RID: 866
	private int life;

	// Token: 0x04000363 RID: 867
	public bool isSpeedUp;

	// Token: 0x04000364 RID: 868
	public long dame;

	// Token: 0x04000365 RID: 869
	public long dameMp;

	// Token: 0x04000366 RID: 870
	public global::Char c;

	// Token: 0x04000367 RID: 871
	public bool isBoss;

	// Token: 0x04000368 RID: 872
	public MyVector darts = new MyVector();

	// Token: 0x04000369 RID: 873
	private int dx;

	// Token: 0x0400036A RID: 874
	private int dy;

	// Token: 0x0400036B RID: 875
	public static int[] ARROWINDEX = new int[]
	{
		0,
		15,
		37,
		52,
		75,
		105,
		127,
		142,
		165,
		195,
		217,
		232,
		255,
		285,
		307,
		322,
		345,
		370
	};

	// Token: 0x0400036C RID: 876
	public static int[] TRANSFORM = new int[]
	{
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		2,
		2,
		3,
		3,
		4,
		5,
		5,
		5,
		1
	};

	// Token: 0x0400036D RID: 877
	public static sbyte[] FRAME = new sbyte[]
	{
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0
	};
}
