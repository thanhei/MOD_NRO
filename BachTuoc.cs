using System;

// Token: 0x0200009D RID: 157
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x0004A174 File Offset: 0x00048374
	public BachTuoc(int id, short px, short py, int templateID, long hp, long maxHp, int s)
	{
		this.mobId = id;
		this.xFirst = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yFirst = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.w_hp_bar = 100;
		this.h_hp_bar = 6;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0004A2A8 File Offset: 0x000484A8
	public void getDataB()
	{
		BachTuoc.data = null;
		BachTuoc.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			108,
			"/data"
		});
		try
		{
			BachTuoc.data.readData2(patch);
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00006CB9 File Offset: 0x00004EB9
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00006CC9 File Offset: 0x00004EC9
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0004A37C File Offset: 0x0004857C
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

	// Token: 0x060005A8 RID: 1448 RVA: 0x00006CD2 File Offset: 0x00004ED2
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0004A3C4 File Offset: 0x000485C4
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

	// Token: 0x060005AA RID: 1450 RVA: 0x0004A4FC File Offset: 0x000486FC
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000045ED File Offset: 0x000027ED
	public new void updateSuperEff()
	{
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0004A550 File Offset: 0x00048750
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

	// Token: 0x060005AD RID: 1453 RVA: 0x0004A638 File Offset: 0x00048838
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

	// Token: 0x060005AE RID: 1454 RVA: 0x000045ED File Offset: 0x000027ED
	public new void setInjure()
	{
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0004A708 File Offset: 0x00048908
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

	// Token: 0x060005B0 RID: 1456 RVA: 0x00006D07 File Offset: 0x00004F07
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x000045ED File Offset: 0x000027ED
	private void updateInjure()
	{
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0004A7E8 File Offset: 0x000489E8
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00006D40 File Offset: 0x00004F40
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00006D50 File Offset: 0x00004F50
	public void setAttack(global::Char[] cAttack, long[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0004A85C File Offset: 0x00048A5C
	public new void updateMobAttack()
	{
		if ((int)this.type == 3)
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
		if ((int)this.type == 4)
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
					this.charAttack[j].doInjure(this.dameHP[j], 0L, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0004AA30 File Offset: 0x00048C30
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? -2 : 2);
		this.y = this.yTo;
		this.dir = ((this.x >= this.xTo) ? -1 : 1);
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0004AAC4 File Offset: 0x00048CC4
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00006D6E File Offset: 0x00004F6E
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00006D7E File Offset: 0x00004F7E
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0004AB38 File Offset: 0x00048D38
	public override void paint(mGraphics g)
	{
		if (BachTuoc.data == null)
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
				BachTuoc.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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
			BachTuoc.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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

	// Token: 0x060005BB RID: 1467 RVA: 0x00006D9B File Offset: 0x00004F9B
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00006DA2 File Offset: 0x00004FA2
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

	// Token: 0x060005BD RID: 1469 RVA: 0x0004AE30 File Offset: 0x00049030
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

	// Token: 0x060005BE RID: 1470 RVA: 0x00006DDE File Offset: 0x00004FDE
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00006DE6 File Offset: 0x00004FE6
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00006DF1 File Offset: 0x00004FF1
	public new int getH()
	{
		return 40;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00006DF1 File Offset: 0x00004FF1
	public new int getW()
	{
		return 40;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0004AF10 File Offset: 0x00049110
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00006DF5 File Offset: 0x00004FF5
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00006E0E File Offset: 0x0000500E
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00006E22 File Offset: 0x00005022
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00006E2B File Offset: 0x0000502B
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00006E34 File Offset: 0x00005034
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x04000A48 RID: 2632
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000A49 RID: 2633
	public static EffectData data;

	// Token: 0x04000A4A RID: 2634
	public int xTo;

	// Token: 0x04000A4B RID: 2635
	public int yTo;

	// Token: 0x04000A4C RID: 2636
	public bool haftBody;

	// Token: 0x04000A4D RID: 2637
	public bool change;

	// Token: 0x04000A4E RID: 2638
	private Mob mob1;

	// Token: 0x04000A4F RID: 2639
	public new int xSd;

	// Token: 0x04000A50 RID: 2640
	public new int ySd;

	// Token: 0x04000A51 RID: 2641
	private bool isOutMap;

	// Token: 0x04000A52 RID: 2642
	private int wCount;

	// Token: 0x04000A53 RID: 2643
	public new bool isShadown = true;

	// Token: 0x04000A54 RID: 2644
	private int tick;

	// Token: 0x04000A55 RID: 2645
	private int frame;

	// Token: 0x04000A56 RID: 2646
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000A57 RID: 2647
	private bool wy;

	// Token: 0x04000A58 RID: 2648
	private int wt;

	// Token: 0x04000A59 RID: 2649
	private int fy;

	// Token: 0x04000A5A RID: 2650
	private int ty;

	// Token: 0x04000A5B RID: 2651
	public new int typeSuperEff;

	// Token: 0x04000A5C RID: 2652
	private global::Char focus;

	// Token: 0x04000A5D RID: 2653
	private bool flyUp;

	// Token: 0x04000A5E RID: 2654
	private bool flyDown;

	// Token: 0x04000A5F RID: 2655
	private int dy;

	// Token: 0x04000A60 RID: 2656
	public bool changePos;

	// Token: 0x04000A61 RID: 2657
	private int tShock;

	// Token: 0x04000A62 RID: 2658
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000A63 RID: 2659
	private int tA;

	// Token: 0x04000A64 RID: 2660
	private global::Char[] charAttack;

	// Token: 0x04000A65 RID: 2661
	private long[] dameHP;

	// Token: 0x04000A66 RID: 2662
	private sbyte type;

	// Token: 0x04000A67 RID: 2663
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

	// Token: 0x04000A68 RID: 2664
	public int[] movee = new int[]
	{
		0,
		0,
		0,
		2,
		2,
		2,
		3,
		3,
		3,
		4,
		4,
		4
	};

	// Token: 0x04000A69 RID: 2665
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6
	};

	// Token: 0x04000A6A RID: 2666
	public new int[] attack2 = new int[]
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
		9,
		10,
		10,
		10,
		11,
		11
	};

	// Token: 0x04000A6B RID: 2667
	public new int[] hurt = new int[]
	{
		1,
		1,
		7,
		7
	};

	// Token: 0x04000A6C RID: 2668
	private bool shock;

	// Token: 0x04000A6D RID: 2669
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000A6E RID: 2670
	public new global::Char injureBy;

	// Token: 0x04000A6F RID: 2671
	public new bool injureThenDie;

	// Token: 0x04000A70 RID: 2672
	public new Mob mobToAttack;

	// Token: 0x04000A71 RID: 2673
	public new int forceWait;

	// Token: 0x04000A72 RID: 2674
	public new bool blindEff;

	// Token: 0x04000A73 RID: 2675
	public new bool sleepEff;
}
