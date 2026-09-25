using System;

// Token: 0x02000071 RID: 113
public class MonsterDart : Effect2
{
	// Token: 0x0600058F RID: 1423 RVA: 0x000566D8 File Offset: 0x000548D8
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
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

	// Token: 0x06000590 RID: 1424 RVA: 0x00056784 File Offset: 0x00054984
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
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

	// Token: 0x06000591 RID: 1425 RVA: 0x00056832 File Offset: 0x00054A32
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00056867 File Offset: 0x00054A67
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00056884 File Offset: 0x00054A84
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, xTo, yTo, dartType));
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000568AC File Offset: 0x00054AAC
	public override void update()
	{
		for (int i = 0; i < (int)this.info.nUpdate; i++)
		{
			if (this.info.tail.Length != 0)
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
				if (this.c != null && this.c.charID >= 0 && this.dameMp != -1)
				{
					if (this.dameMp != -100)
					{
						this.c.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						ServerEffect.addServerEffect(80, this.c, 1);
					}
				}
				Effect2.vEffect2.removeElement(this);
				if (this.dameMp != -100)
				{
					ServerEffect.addServerEffect(81, this.c, 1);
					if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
					{
						SoundMn.gI().explode_2();
					}
				}
			}
			int num2 = Res.angle(this.dx, this.dy);
			if (Math2.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096)
			{
				if (Math2.abs(num2 - this.angle) < 15)
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
			this.x += this.dx >> 10;
			this.dx &= 1023;
			this.dy += this.vy;
			this.y += this.dy >> 10;
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

	// Token: 0x06000595 RID: 1429 RVA: 0x00056CA4 File Offset: 0x00054EA4
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

	// Token: 0x06000596 RID: 1430 RVA: 0x00056CE8 File Offset: 0x00054EE8
	public override void paint(mGraphics g)
	{
		int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
		int num2 = (int)MonsterDart.FRAME[num];
		int num3 = MonsterDart.TRANSFORM[num];
		for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
			SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
		}
		int num4 = GameCanvas.gameTick % this.info.headBorder.Length;
		SmallImage.drawSmallImage(g, (int)this.info.headBorder[num4][num2], this.x, this.y, num3, 3);
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
			SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
		}
		SmallImage.drawSmallImage(g, (int)this.info.head[num4][num2], this.x, this.y, num3, 3);
		for (int k = 0; k < this.darts.size(); k++)
		{
			SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
			if (Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent)
			{
				SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
			}
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00056EC1 File Offset: 0x000550C1
	public static void addMonsterDart(int x2, int y2, bool checkIsBoss, int dame2, int dameMp2, Mob mobToAttack, sbyte dartType)
	{
		MonsterDart.addMonsterDart(x2, y2, checkIsBoss, dame2, dameMp2, mobToAttack.x, mobToAttack.y, (int)dartType);
	}

	// Token: 0x04000BD6 RID: 3030
	public int va;

	// Token: 0x04000BD7 RID: 3031
	internal DartInfo info;

	// Token: 0x04000BD8 RID: 3032
	public static MyRandom r = new MyRandom();

	// Token: 0x04000BD9 RID: 3033
	public int angle;

	// Token: 0x04000BDA RID: 3034
	public int vx;

	// Token: 0x04000BDB RID: 3035
	public int vy;

	// Token: 0x04000BDC RID: 3036
	public int x;

	// Token: 0x04000BDD RID: 3037
	public int y;

	// Token: 0x04000BDE RID: 3038
	public int z;

	// Token: 0x04000BDF RID: 3039
	public int xTo;

	// Token: 0x04000BE0 RID: 3040
	public int yTo;

	// Token: 0x04000BE1 RID: 3041
	internal int life;

	// Token: 0x04000BE2 RID: 3042
	public bool isSpeedUp;

	// Token: 0x04000BE3 RID: 3043
	public int dame;

	// Token: 0x04000BE4 RID: 3044
	public int dameMp;

	// Token: 0x04000BE5 RID: 3045
	public global::Char c;

	// Token: 0x04000BE6 RID: 3046
	public bool isBoss;

	// Token: 0x04000BE7 RID: 3047
	public MyVector darts = new MyVector();

	// Token: 0x04000BE8 RID: 3048
	internal int dx;

	// Token: 0x04000BE9 RID: 3049
	internal int dy;

	// Token: 0x04000BEA RID: 3050
	public static int[] ARROWINDEX = new int[]
	{
		0, 15, 37, 52, 75, 105, 127, 142, 165, 195,
		217, 232, 255, 285, 307, 322, 345, 370
	};

	// Token: 0x04000BEB RID: 3051
	public static int[] TRANSFORM = new int[]
	{
		0, 0, 0, 7, 6, 6, 6, 2, 2, 3,
		3, 4, 5, 5, 5, 1
	};

	// Token: 0x04000BEC RID: 3052
	public static sbyte[] FRAME = new sbyte[]
	{
		0, 1, 2, 1, 0, 1, 2, 1, 0, 1,
		2, 1, 0, 1, 2, 1, 0, 1, 2, 1,
		0, 1, 2, 1, 0
	};
}
