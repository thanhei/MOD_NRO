using System;

// Token: 0x020000A0 RID: 160
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x060005F3 RID: 1523 RVA: 0x0004C150 File Offset: 0x0004A350
	public BigBoss2(int id, short px, short py, int templateID, long hp, long maxHp, int s)
	{
		if (BigBoss2.shadowBig == null)
		{
			BigBoss2.shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");
		}
		this.mobId = id;
		this.xTo = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yTo = (int)py;
		this.yFirst = (int)py;
		this.hp = hp;
		this.maxHp = maxHp;
		this.templateId = templateID;
		this.w_hp_bar = 100;
		this.h_hp_bar = 6;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0004C2D4 File Offset: 0x0004A4D4
	public void getDataB()
	{
		BigBoss2.data = null;
		BigBoss2.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			109,
			"/data"
		});
		try
		{
			BigBoss2.data.readData2(patch);
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00006CB9 File Offset: 0x00004EB9
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00006CC9 File Offset: 0x00004EC9
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0004A37C File Offset: 0x0004857C
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			string text = (string)Mob.newMob.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00006F79 File Offset: 0x00005179
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0004C3A8 File Offset: 0x0004A5A8
	private void updateShadown()
	{
		int num = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		if (this.ySd <= 0)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) == 0)
		{
			this.isOutMap = true;
		}
		else if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			this.xSd = this.x;
			this.ySd = this.y;
			this.isOutMap = false;
		}
		while (this.isOutMap && this.wCount < 10)
		{
			this.wCount++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				return;
			}
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0004C4E0 File Offset: 0x0004A6E0
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x000045ED File Offset: 0x000027ED
	public new void updateSuperEff()
	{
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0004C534 File Offset: 0x0004A734
	public override void update()
	{
		if (!this.isUpdate())
		{
			return;
		}
		this.updateShadown();
		switch (this.status)
		{
		case 0:
		case 1:
			this.updateDead();
			break;
		case 2:
			this.updateMobStandWait();
			break;
		case 3:
			this.updateMobAttack();
			break;
		case 4:
			this.timeStatus = 0;
			this.updateMobFly();
			break;
		case 5:
			this.timeStatus = 0;
			this.updateMobWalk();
			break;
		case 6:
			this.timeStatus = 0;
			this.p1++;
			this.y += this.p1;
			if (this.y >= this.yFirst)
			{
				this.y = this.yFirst;
				this.p1 = 0;
				this.status = 5;
			}
			break;
		case 7:
			this.updateInjure();
			break;
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0004C630 File Offset: 0x0004A830
	private void updateDead()
	{
		this.checkFrameTick(this.stand);
		if (GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
		}
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0004C700 File Offset: 0x0004A900
	private void updateMobFly()
	{
		if (this.flyUp)
		{
			this.dy++;
			this.y -= this.dy;
			this.checkFrameTick(this.fly);
			if (this.y <= -500)
			{
				this.flyUp = false;
				this.flyDown = true;
				this.dy = 0;
			}
		}
		if (this.flyDown)
		{
			this.x = this.xTo;
			this.dy += 2;
			this.y += this.dy;
			this.checkFrameTick(this.hitground);
			if (this.y > this.yFirst)
			{
				this.y = this.yFirst;
				this.flyDown = false;
				this.dy = 0;
				this.status = 2;
				GameScr.shock_scr = 10;
				this.shock = true;
			}
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000045ED File Offset: 0x000027ED
	public new void setInjure()
	{
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0004C7F0 File Offset: 0x0004A9F0
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx <= this.x) ? -1 : 1);
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
		{
			if (this.x < cx)
			{
				this.x = cx - this.w;
			}
			else
			{
				this.x = cx + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00006D07 File Offset: 0x00004F07
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x000045ED File Offset: 0x000027ED
	private void updateInjure()
	{
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0004C8D0 File Offset: 0x0004AAD0
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00006FAE File Offset: 0x000051AE
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00006FBE File Offset: 0x000051BE
	public void setAttack(global::Char[] cAttack, long[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0004C944 File Offset: 0x0004AB44
	public new void updateMobAttack()
	{
		if ((int)this.type == 0)
		{
			if (this.tick == this.attack1.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			if (this.tick == 8)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0L, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		if ((int)this.type == 1)
		{
			if (this.tick == this.attack2.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack2);
			if (this.tick == 8)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, this.dameHP[j], 0L, this.charAttack[j], 24);
				}
			}
		}
		if ((int)this.type == 2)
		{
			if (this.tick == this.fly.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			if (this.tick == 12)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0L, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x000045ED File Offset: 0x000027ED
	public new void updateMobWalk()
	{
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0004AAC4 File Offset: 0x00048CC4
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00006D6E File Offset: 0x00004F6E
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00006D7E File Offset: 0x00004F7E
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0004CC10 File Offset: 0x0004AE10
	public override void paint(mGraphics g)
	{
		if (BigBoss2.data == null)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			if (!this.changBody)
			{
				BigBoss2.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			return;
		}
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		g.translate(0, GameCanvas.transY);
		if (!this.changBody)
		{
			BigBoss2.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		g.translate(0, -GameCanvas.transY);
		int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
		int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
		int num = imageWidth;
		int num2 = this.x - imageWidth;
		int y = this.y - this.h - 5;
		int num3 = imageWidth * 2 * this.per / 100;
		int num4;
		if (num3 > num)
		{
			num4 = num3 - num;
			if (num4 <= 0)
			{
				num4 = 0;
			}
		}
		else
		{
			num = num3;
			num4 = 0;
		}
		g.drawImage(GameScr.imgHP_tm_xam, num2, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(GameScr.imgHP_tm_xam, num2 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, num2, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(this.imgHPtem, 0, 0, num4, imageHeight, 0, num2 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		if (this.shock)
		{
			this.tShock++;
			Effect me = new Effect(((int)this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me);
			Effect me2 = new Effect(((int)this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me2);
			if (this.tShock == 50)
			{
				this.tShock = 0;
				this.shock = false;
			}
		}
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00006D9B File Offset: 0x00004F9B
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00006FE3 File Offset: 0x000051E3
	public new void startDie()
	{
		this.hp = 0L;
		this.injureThenDie = true;
		this.hp = 0L;
		this.status = 1;
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0004CF08 File Offset: 0x0004B108
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int x = mobToAttack.x;
		int y = mobToAttack.y;
		if (Res.abs(x - this.x) < this.w * 2 && Res.abs(y - this.y) < this.h * 2)
		{
			if (this.x < x)
			{
				this.x = x - this.w;
			}
			else
			{
				this.x = x + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00006DDE File Offset: 0x00004FDE
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0000701F File Offset: 0x0000521F
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00006DF1 File Offset: 0x00004FF1
	public new int getH()
	{
		return 40;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0000702A File Offset: 0x0000522A
	public new int getW()
	{
		return 50;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0004CFE8 File Offset: 0x0004B1E8
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00006DF5 File Offset: 0x00004FF5
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00006E0E File Offset: 0x0000500E
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0000702E File Offset: 0x0000522E
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00007037 File Offset: 0x00005237
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x04000AB1 RID: 2737
	public static Image shadowBig;

	// Token: 0x04000AB2 RID: 2738
	public static EffectData data;

	// Token: 0x04000AB3 RID: 2739
	public int xTo;

	// Token: 0x04000AB4 RID: 2740
	public int yTo;

	// Token: 0x04000AB5 RID: 2741
	public bool haftBody;

	// Token: 0x04000AB6 RID: 2742
	public bool change;

	// Token: 0x04000AB7 RID: 2743
	private Mob mob1;

	// Token: 0x04000AB8 RID: 2744
	public new int xSd;

	// Token: 0x04000AB9 RID: 2745
	public new int ySd;

	// Token: 0x04000ABA RID: 2746
	private bool isOutMap;

	// Token: 0x04000ABB RID: 2747
	private int wCount;

	// Token: 0x04000ABC RID: 2748
	public new bool isShadown = true;

	// Token: 0x04000ABD RID: 2749
	private int tick;

	// Token: 0x04000ABE RID: 2750
	private int frame;

	// Token: 0x04000ABF RID: 2751
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000AC0 RID: 2752
	private bool wy;

	// Token: 0x04000AC1 RID: 2753
	private int wt;

	// Token: 0x04000AC2 RID: 2754
	private int fy;

	// Token: 0x04000AC3 RID: 2755
	private int ty;

	// Token: 0x04000AC4 RID: 2756
	public new int typeSuperEff;

	// Token: 0x04000AC5 RID: 2757
	private global::Char focus;

	// Token: 0x04000AC6 RID: 2758
	private int timeDead;

	// Token: 0x04000AC7 RID: 2759
	private bool flyUp;

	// Token: 0x04000AC8 RID: 2760
	private bool flyDown;

	// Token: 0x04000AC9 RID: 2761
	private int dy;

	// Token: 0x04000ACA RID: 2762
	public bool changePos;

	// Token: 0x04000ACB RID: 2763
	private int tShock;

	// Token: 0x04000ACC RID: 2764
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000ACD RID: 2765
	private int tA;

	// Token: 0x04000ACE RID: 2766
	private global::Char[] charAttack;

	// Token: 0x04000ACF RID: 2767
	private long[] dameHP;

	// Token: 0x04000AD0 RID: 2768
	private sbyte type;

	// Token: 0x04000AD1 RID: 2769
	public new int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000AD2 RID: 2770
	public new int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x04000AD3 RID: 2771
	public new int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000AD4 RID: 2772
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		7,
		7,
		7,
		8,
		8,
		8,
		9,
		9,
		9
	};

	// Token: 0x04000AD5 RID: 2773
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		10,
		10,
		10,
		11,
		11,
		11,
		12,
		12,
		12
	};

	// Token: 0x04000AD6 RID: 2774
	public int[] attack3 = new int[]
	{
		0,
		0,
		1,
		1,
		4,
		4,
		6,
		6,
		8,
		8,
		25,
		25,
		26,
		26,
		28,
		28,
		30,
		30,
		32,
		32,
		2,
		2,
		1,
		1
	};

	// Token: 0x04000AD7 RID: 2775
	public int[] fly = new int[]
	{
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6,
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x04000AD8 RID: 2776
	public int[] hitground = new int[]
	{
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x04000AD9 RID: 2777
	private bool shock;

	// Token: 0x04000ADA RID: 2778
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000ADB RID: 2779
	public new global::Char injureBy;

	// Token: 0x04000ADC RID: 2780
	public new bool injureThenDie;

	// Token: 0x04000ADD RID: 2781
	public new Mob mobToAttack;

	// Token: 0x04000ADE RID: 2782
	public new int forceWait;

	// Token: 0x04000ADF RID: 2783
	public new bool blindEff;

	// Token: 0x04000AE0 RID: 2784
	public new bool sleepEff;
}
