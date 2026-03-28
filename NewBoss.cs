using System;

// Token: 0x020000BB RID: 187
public class NewBoss : Mob, IMapObject
{
	// Token: 0x0600087D RID: 2173 RVA: 0x00079518 File Offset: 0x00077718
	public NewBoss(int id, short px, short py, int templateID, long hp, long maxHp, int s)
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

	// Token: 0x0600087E RID: 2174 RVA: 0x00006CB9 File Offset: 0x00004EB9
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00006CC9 File Offset: 0x00004EC9
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0004A37C File Offset: 0x0004857C
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

	// Token: 0x06000881 RID: 2177 RVA: 0x00007FDC File Offset: 0x000061DC
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0007977C File Offset: 0x0007797C
	public void updateShadown()
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
				}
				break;
			}
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00079828 File Offset: 0x00077A28
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
		{
			if (TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
			}
			else if (TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8))
			{
				g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
			}
		}
		g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000045ED File Offset: 0x000027ED
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000799BC File Offset: 0x00077BBC
	public override void update()
	{
		if (this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.GetFrame();
		}
		if (this.frameArr == null)
		{
			return;
		}
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
			base.update();
			break;
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00079AE8 File Offset: 0x00077CE8
	private void updateDead()
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

	// Token: 0x06000887 RID: 2183 RVA: 0x000045ED File Offset: 0x000027ED
	private void updateMobFly()
	{
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00079BA0 File Offset: 0x00077DA0
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
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000045ED File Offset: 0x000027ED
	private void updateInjure()
	{
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00079C60 File Offset: 0x00077E60
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.frameArr[0]);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00008011 File Offset: 0x00006211
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00079CD8 File Offset: 0x00077ED8
	public void setAttack(global::Char[] cAttack, long[] dame, sbyte type, sbyte dir)
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

	// Token: 0x0600088D RID: 2189 RVA: 0x00079D64 File Offset: 0x00077F64
	public new void updateMobAttack()
	{
		if (this.tick == this.frameArr[(int)this.type + 1].Length - 1)
		{
			this.status = 2;
		}
		this.checkFrameTick(this.frameArr[(int)this.type + 1]);
		if (this.tick == this.frameArr[15][(int)this.type - 1])
		{
			for (int i = 0; i < this.charAttack.Length; i++)
			{
				this.charAttack[i].doInjure(this.dameHP[i], 0L, false, false);
				ServerEffect.addServerEffect(this.frameArr[16][(int)this.type - 1], this.charAttack[i].cx, this.charAttack[i].cy, 1);
			}
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00079E30 File Offset: 0x00078030
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

	// Token: 0x0600088F RID: 2191 RVA: 0x0004AAC4 File Offset: 0x00048CC4
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00006D6E File Offset: 0x00004F6E
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00079F20 File Offset: 0x00078120
	public override void paint(mGraphics g)
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
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
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			return;
		}
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
		if (this.hp > 0L)
		{
			int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
			int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
			int num2 = imageWidth;
			int num3 = this.x - imageWidth;
			int y = this.y - this.h - 5;
			int num4 = imageWidth * 2 * this.per / 100;
			int w = num4;
			if (this.per_tem >= this.per)
			{
				w = imageWidth * (this.per_tem -= ((GameCanvas.gameTick % 6 <= 3) ? this.offset : this.offset++)) / 100;
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
			int num5;
			if (num4 > num2)
			{
				num5 = num4 - num2;
				if (num5 <= 0)
				{
					num5 = 0;
				}
			}
			else
			{
				num2 = num4;
				num5 = 0;
			}
			g.drawImage(GameScr.imgHP_tm_xam, num3, y, mGraphics.TOP | mGraphics.LEFT);
			g.drawImage(GameScr.imgHP_tm_xam, num3 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
			g.setColor(16777215);
			g.fillRect(num3, y, w, 2);
			g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, num3, y, mGraphics.TOP | mGraphics.LEFT);
			g.drawRegion(this.imgHPtem, 0, 0, num5, imageHeight, 0, num3 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00006D9B File Offset: 0x00004F9B
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00008021 File Offset: 0x00006221
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

	// Token: 0x06000894 RID: 2196 RVA: 0x0007A348 File Offset: 0x00078548
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
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00006DDE File Offset: 0x00004FDE
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00007E71 File Offset: 0x00006071
	public new int getY()
	{
		return this.y;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00007E79 File Offset: 0x00006079
	public new int getH()
	{
		return this.h;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00007E81 File Offset: 0x00006081
	public new int getW()
	{
		return this.w;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0007A408 File Offset: 0x00078608
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00006DF5 File Offset: 0x00004FF5
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00006E0E File Offset: 0x0000500E
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0000805D File Offset: 0x0000625D
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00008066 File Offset: 0x00006266
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0007A44C File Offset: 0x0007864C
	public new void move(short xMoveTo, short yMoveTo)
	{
		if (yMoveTo != -1)
		{
			if (Res.distance(this.x, this.y, this.xTo, this.yTo) > 100)
			{
				this.x = (int)xMoveTo;
				this.y = (int)yMoveTo;
				this.status = 2;
			}
			else
			{
				this.xTo = (int)xMoveTo;
				this.yTo = (int)yMoveTo;
				this.status = 5;
			}
		}
		else
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0007A4C8 File Offset: 0x000786C8
	public new void GetFrame()
	{
		try
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0000806F File Offset: 0x0000626F
	public void setDie()
	{
		this.status = 0;
	}

	// Token: 0x0400105F RID: 4191
	public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04001060 RID: 4192
	public int xTo;

	// Token: 0x04001061 RID: 4193
	public int yTo;

	// Token: 0x04001062 RID: 4194
	public bool haftBody;

	// Token: 0x04001063 RID: 4195
	public bool change;

	// Token: 0x04001064 RID: 4196
	public new int xSd;

	// Token: 0x04001065 RID: 4197
	public new int ySd;

	// Token: 0x04001066 RID: 4198
	private int wCount;

	// Token: 0x04001067 RID: 4199
	public new bool isShadown = true;

	// Token: 0x04001068 RID: 4200
	private int tick;

	// Token: 0x04001069 RID: 4201
	private int frame;

	// Token: 0x0400106A RID: 4202
	public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x0400106B RID: 4203
	private bool wy;

	// Token: 0x0400106C RID: 4204
	private int wt;

	// Token: 0x0400106D RID: 4205
	private int fy;

	// Token: 0x0400106E RID: 4206
	private int ty;

	// Token: 0x0400106F RID: 4207
	public new int typeSuperEff;

	// Token: 0x04001070 RID: 4208
	private global::Char focus;

	// Token: 0x04001071 RID: 4209
	private bool flyUp;

	// Token: 0x04001072 RID: 4210
	private bool flyDown;

	// Token: 0x04001073 RID: 4211
	private int dy;

	// Token: 0x04001074 RID: 4212
	public bool changePos;

	// Token: 0x04001075 RID: 4213
	private int tShock;

	// Token: 0x04001076 RID: 4214
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04001077 RID: 4215
	private int tA;

	// Token: 0x04001078 RID: 4216
	private global::Char[] charAttack;

	// Token: 0x04001079 RID: 4217
	private long[] dameHP;

	// Token: 0x0400107A RID: 4218
	private sbyte type;

	// Token: 0x0400107B RID: 4219
	private int ff;

	// Token: 0x0400107C RID: 4220
	private int offset;

	// Token: 0x0400107D RID: 4221
	private int xTempRight = -1;

	// Token: 0x0400107E RID: 4222
	private int xTempLeft = -1;

	// Token: 0x0400107F RID: 4223
	private int yTemp = -1;

	// Token: 0x04001080 RID: 4224
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04001081 RID: 4225
	public new global::Char injureBy;

	// Token: 0x04001082 RID: 4226
	public new bool injureThenDie;

	// Token: 0x04001083 RID: 4227
	public new Mob mobToAttack;

	// Token: 0x04001084 RID: 4228
	public new int forceWait;

	// Token: 0x04001085 RID: 4229
	public new bool blindEff;

	// Token: 0x04001086 RID: 4230
	public new bool sleepEff;

	// Token: 0x04001087 RID: 4231
	private int[][] frameArr = new int[][]
	{
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		}
	};

	// Token: 0x04001088 RID: 4232
	public new const sbyte stand = 0;

	// Token: 0x04001089 RID: 4233
	public const sbyte moveFra = 1;

	// Token: 0x0400108A RID: 4234
	public new const sbyte attack1 = 2;

	// Token: 0x0400108B RID: 4235
	public new const sbyte attack2 = 3;

	// Token: 0x0400108C RID: 4236
	public const sbyte attack3 = 4;

	// Token: 0x0400108D RID: 4237
	public const sbyte attack4 = 5;

	// Token: 0x0400108E RID: 4238
	public const sbyte attack5 = 6;

	// Token: 0x0400108F RID: 4239
	public const sbyte attack6 = 7;

	// Token: 0x04001090 RID: 4240
	public const sbyte attack7 = 8;

	// Token: 0x04001091 RID: 4241
	public const sbyte attack8 = 9;

	// Token: 0x04001092 RID: 4242
	public const sbyte attack9 = 10;

	// Token: 0x04001093 RID: 4243
	public const sbyte attack10 = 11;

	// Token: 0x04001094 RID: 4244
	public new const sbyte hurt = 12;

	// Token: 0x04001095 RID: 4245
	public const sbyte die = 13;

	// Token: 0x04001096 RID: 4246
	public const sbyte fly = 14;

	// Token: 0x04001097 RID: 4247
	public const sbyte adddame = 15;

	// Token: 0x04001098 RID: 4248
	public const sbyte typeEff = 16;
}
