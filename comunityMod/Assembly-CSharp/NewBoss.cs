using System;

// Token: 0x0200007D RID: 125
public class NewBoss : Mob, IMapObject
{
	// Token: 0x060005D9 RID: 1497 RVA: 0x00057CBC File Offset: 0x00055EBC
	public NewBoss(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.x = (this.xFirst = (int)(px + 20));
		this.yFirst = (int)py;
		this.y = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.h_hp_bar = 6;
		this.w_hp_bar = 100;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.status = 2;
		this.frameArr = null;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0000468C File Offset: 0x0000288C
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0000469C File Offset: 0x0000289C
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00057F1C File Offset: 0x0005611C
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			if (((string)Mob.newMob.elementAt(i)).Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00057F59 File Offset: 0x00056159
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00057F8C File Offset: 0x0005618C
	public new void updateShadown()
	{
		int i = 0;
		this.xSd = this.x;
		if (TileMap.tileTypeAt(this.x, this.y, 2))
		{
			this.ySd = this.y;
			return;
		}
		this.ySd = this.y;
		while (i < 30)
		{
			i++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00058024 File Offset: 0x00056224
	internal new void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
		{
			if (TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4))
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else if (TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8))
			{
				g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
			}
		}
		g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00004887 File Offset: 0x00002A87
	public new void updateSuperEff()
	{
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000581A4 File Offset: 0x000563A4
	public override void update()
	{
		if (this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.GetFrame();
		}
		if (this.frameArr == null || !this.isUpdate())
		{
			return;
		}
		this.updateShadown();
		switch (this.status)
		{
		case 0:
		case 1:
			this.updateDead();
			return;
		case 2:
			this.updateMobStandWait();
			return;
		case 3:
			this.updateMobAttack();
			return;
		case 4:
			this.updateMobFly();
			break;
		case 5:
			this.timeStatus = 0;
			this.updateMobWalk();
			return;
		case 6:
			this.timeStatus = 0;
			this.p1++;
			this.y += this.p1;
			if (this.y >= this.yFirst)
			{
				this.y = this.yFirst;
				this.p1 = 0;
				this.status = 5;
				return;
			}
			break;
		case 7:
			this.updateInjure();
			base.update();
			return;
		default:
			return;
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x000582A0 File Offset: 0x000564A0
	internal void updateDead()
	{
		this.tick++;
		if (this.tick > this.frameArr[13].Length - 1)
		{
			this.tick = this.frameArr[13].Length - 1;
		}
		this.frame = this.frameArr[13][this.tick];
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00004887 File Offset: 0x00002A87
	internal void updateMobFly()
	{
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00058350 File Offset: 0x00056550
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00004887 File Offset: 0x00002A87
	internal new void updateInjure()
	{
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00058400 File Offset: 0x00056600
	internal new void updateMobStandWait()
	{
		this.checkFrameTick(this.frameArr[0]);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0005846F File Offset: 0x0005666F
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00058480 File Offset: 0x00056680
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type, sbyte dir)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.dir = (int)dir;
		this.status = 3;
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00058508 File Offset: 0x00056708
	public new void updateMobAttack()
	{
		if (this.tick == this.frameArr[(int)(this.type + 1)].Length - 1)
		{
			this.status = 2;
		}
		this.checkFrameTick(this.frameArr[(int)(this.type + 1)]);
		if (this.tick == this.frameArr[15][(int)(this.type - 1)])
		{
			for (int i = 0; i < this.charAttack.Length; i++)
			{
				this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				ServerEffect.addServerEffect(this.frameArr[16][(int)(this.type - 1)], this.charAttack[i].cx, this.charAttack[i].cy, 1);
			}
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x000585C4 File Offset: 0x000567C4
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.frameArr[1]);
		sbyte speed = Mob.arrMobTemplate[this.templateId].speed;
		int num = (int)speed;
		if (Res.abs(this.x - this.xTo) < (int)speed)
		{
			num = Res.abs(this.x - this.xTo);
		}
		this.x += ((this.x >= this.xTo) ? (-num) : num);
		this.y = this.yTo;
		if (this.x < this.xTo)
		{
			this.dir = 1;
		}
		else if (this.x > this.xTo)
		{
			this.dir = -1;
		}
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0005869C File Offset: 0x0005689C
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x00004E43 File Offset: 0x00003043
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00058700 File Offset: 0x00056900
	public override void paint(mGraphics g)
	{
		if (Mob.arrMobTemplate[this.templateId].data == null || this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			if (!this.changBody)
			{
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			return;
		}
		else
		{
			if (this.isShadown)
			{
				this.paintShadow(g);
			}
			g.translate(0, GameCanvas.transY);
			if (!this.changBody)
			{
				int num = 33;
				if (this.yTemp == -1)
				{
					this.yTemp = this.y;
				}
				if (TileMap.tileTypeAt(this.x + num, this.y + this.fy, 4))
				{
					this.xTempLeft = TileMap.tileXofPixel(this.x + num) - num;
					this.xTempRight = TileMap.tileXofPixel(this.x + num);
					if (this.x > this.xTempLeft && this.x < this.xTempRight && this.xTempRight != -1)
					{
						this.x = this.xTempLeft;
					}
				}
				if (this.y < this.yTemp && this.yTemp != -1)
				{
					this.yTemp = this.y;
					this.x += num;
				}
				if (this.y > this.yTemp)
				{
					this.yTemp = this.y;
					this.x -= num;
				}
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			g.translate(0, -GameCanvas.transY);
			if (this.hp <= 0)
			{
				return;
			}
			int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
			int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
			int num2 = imageWidth;
			int num3 = this.x - imageWidth;
			int num4 = this.y - this.h - 5;
			int num5 = imageWidth * 2 * this.per / 100;
			int num6 = num5;
			if (this.per_tem >= this.per)
			{
				int num7 = imageWidth;
				int per_tem = this.per_tem;
				int num9;
				if (GameCanvas.gameTick % 6 > 3)
				{
					int num8 = this.offset;
					this.offset = num8 + 1;
					num9 = num8;
				}
				else
				{
					num9 = this.offset;
				}
				num6 = num7 * (this.per_tem = per_tem - num9) / 100;
				if (this.per_tem <= 0)
				{
					this.per_tem = 0;
				}
				if (this.per_tem < this.per)
				{
					this.per_tem = this.per;
				}
				if (this.offset >= 3)
				{
					this.offset = 3;
				}
			}
			int num10;
			if (num5 > num2)
			{
				num10 = num5 - num2;
				if (num10 <= 0)
				{
					num10 = 0;
				}
			}
			else
			{
				num2 = num5;
				num10 = 0;
			}
			g.drawImage(GameScr.imgHP_tm_xam, num3, num4, mGraphics.TOP | mGraphics.LEFT);
			g.drawImage(GameScr.imgHP_tm_xam, num3 + imageWidth, num4, mGraphics.TOP | mGraphics.LEFT);
			g.setColor(16777215);
			g.fillRect(num3, num4, num6, 2);
			g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, num3, num4, mGraphics.TOP | mGraphics.LEFT);
			g.drawRegion(this.imgHPtem, 0, 0, num10, imageHeight, 0, num3 + imageWidth, num4, mGraphics.TOP | mGraphics.LEFT);
			return;
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00005108 File Offset: 0x00003308
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00058ABF File Offset: 0x00056CBF
	public new void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00058AFC File Offset: 0x00056CFC
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00005214 File Offset: 0x00003414
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00055BC0 File Offset: 0x00053DC0
	public new int getY()
	{
		return this.y;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00055BC8 File Offset: 0x00053DC8
	public new int getH()
	{
		return this.h;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00055BD0 File Offset: 0x00053DD0
	public new int getW()
	{
		return this.w;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00058BAC File Offset: 0x00056DAC
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0000526A File Offset: 0x0000346A
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0000527F File Offset: 0x0000347F
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00058BEA File Offset: 0x00056DEA
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00058BF3 File Offset: 0x00056DF3
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00058BFC File Offset: 0x00056DFC
	public new void move(short xMoveTo, short yMoveTo)
	{
		if (yMoveTo == -1)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
			return;
		}
		if (Res.distance(this.x, this.y, this.xTo, this.yTo) > 100)
		{
			this.x = (int)xMoveTo;
			this.y = (int)yMoveTo;
			this.status = 2;
			return;
		}
		this.xTo = (int)xMoveTo;
		this.yTo = (int)yMoveTo;
		this.status = 5;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00058C68 File Offset: 0x00056E68
	public new void GetFrame()
	{
		try
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00058CEC File Offset: 0x00056EEC
	public void setDie()
	{
		this.status = 0;
	}

	// Token: 0x04000C0D RID: 3085
	public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000C0E RID: 3086
	public int xTo;

	// Token: 0x04000C0F RID: 3087
	public int yTo;

	// Token: 0x04000C10 RID: 3088
	public bool haftBody;

	// Token: 0x04000C11 RID: 3089
	public bool change;

	// Token: 0x04000C12 RID: 3090
	public new int xSd;

	// Token: 0x04000C13 RID: 3091
	public new int ySd;

	// Token: 0x04000C14 RID: 3092
	internal new int wCount;

	// Token: 0x04000C15 RID: 3093
	public new bool isShadown = true;

	// Token: 0x04000C16 RID: 3094
	internal new int tick;

	// Token: 0x04000C17 RID: 3095
	internal new int frame;

	// Token: 0x04000C18 RID: 3096
	public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000C19 RID: 3097
	internal new bool wy;

	// Token: 0x04000C1A RID: 3098
	internal new int wt;

	// Token: 0x04000C1B RID: 3099
	internal new int fy;

	// Token: 0x04000C1C RID: 3100
	internal new int ty;

	// Token: 0x04000C1D RID: 3101
	public new int typeSuperEff;

	// Token: 0x04000C1E RID: 3102
	internal global::Char focus;

	// Token: 0x04000C1F RID: 3103
	internal bool flyUp;

	// Token: 0x04000C20 RID: 3104
	internal bool flyDown;

	// Token: 0x04000C21 RID: 3105
	internal int dy;

	// Token: 0x04000C22 RID: 3106
	public bool changePos;

	// Token: 0x04000C23 RID: 3107
	internal int tShock;

	// Token: 0x04000C24 RID: 3108
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000C25 RID: 3109
	internal int tA;

	// Token: 0x04000C26 RID: 3110
	internal global::Char[] charAttack;

	// Token: 0x04000C27 RID: 3111
	internal int[] dameHP;

	// Token: 0x04000C28 RID: 3112
	internal sbyte type;

	// Token: 0x04000C29 RID: 3113
	internal int ff;

	// Token: 0x04000C2A RID: 3114
	internal new int offset;

	// Token: 0x04000C2B RID: 3115
	internal int xTempRight = -1;

	// Token: 0x04000C2C RID: 3116
	internal int xTempLeft = -1;

	// Token: 0x04000C2D RID: 3117
	internal int yTemp = -1;

	// Token: 0x04000C2E RID: 3118
	internal new sbyte[] cou = new sbyte[] { -1, 1 };

	// Token: 0x04000C2F RID: 3119
	public new global::Char injureBy;

	// Token: 0x04000C30 RID: 3120
	public new bool injureThenDie;

	// Token: 0x04000C31 RID: 3121
	public new Mob mobToAttack;

	// Token: 0x04000C32 RID: 3122
	public new int forceWait;

	// Token: 0x04000C33 RID: 3123
	public new bool blindEff;

	// Token: 0x04000C34 RID: 3124
	public new bool sleepEff;

	// Token: 0x04000C35 RID: 3125
	internal new int[][] frameArr = new int[][]
	{
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 }
	};

	// Token: 0x04000C36 RID: 3126
	public new const sbyte stand = 0;

	// Token: 0x04000C37 RID: 3127
	public const sbyte moveFra = 1;

	// Token: 0x04000C38 RID: 3128
	public new const sbyte attack1 = 2;

	// Token: 0x04000C39 RID: 3129
	public new const sbyte attack2 = 3;

	// Token: 0x04000C3A RID: 3130
	public const sbyte attack3 = 4;

	// Token: 0x04000C3B RID: 3131
	public const sbyte attack4 = 5;

	// Token: 0x04000C3C RID: 3132
	public const sbyte attack5 = 6;

	// Token: 0x04000C3D RID: 3133
	public const sbyte attack6 = 7;

	// Token: 0x04000C3E RID: 3134
	public const sbyte attack7 = 8;

	// Token: 0x04000C3F RID: 3135
	public const sbyte attack8 = 9;

	// Token: 0x04000C40 RID: 3136
	public const sbyte attack9 = 10;

	// Token: 0x04000C41 RID: 3137
	public const sbyte attack10 = 11;

	// Token: 0x04000C42 RID: 3138
	public new const sbyte hurt = 12;

	// Token: 0x04000C43 RID: 3139
	public const sbyte die = 13;

	// Token: 0x04000C44 RID: 3140
	public const sbyte fly = 14;

	// Token: 0x04000C45 RID: 3141
	public const sbyte adddame = 15;

	// Token: 0x04000C46 RID: 3142
	public const sbyte typeEff = 16;
}
