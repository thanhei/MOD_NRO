using System;

// Token: 0x02000014 RID: 20
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x060000BF RID: 191 RVA: 0x00007AA8 File Offset: 0x00005CA8
	public BigBoss2(int id, short px, short py, int templateID, int hp, int maxHp, int s)
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

	// Token: 0x060000C0 RID: 192 RVA: 0x00007C2C File Offset: 0x00005E2C
	public void getDataB()
	{
		BigBoss2.data = null;
		BigBoss2.data = new EffectData();
		string text = string.Concat(new string[]
		{
			"/x",
			mGraphics.zoomLevel.ToString(),
			"/effectdata/",
			109.ToString(),
			"/data"
		});
		try
		{
			BigBoss2.data.readData2(text);
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109.ToString() + "/img.png");
		}
		catch (Exception)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000468C File Offset: 0x0000288C
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000469C File Offset: 0x0000289C
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00007D00 File Offset: 0x00005F00
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

	// Token: 0x060000C4 RID: 196 RVA: 0x00007D3D File Offset: 0x00005F3D
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00007D70 File Offset: 0x00005F70
	internal new void updateShadown()
	{
		int size = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		if (this.ySd <= 0 || TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd / size, this.ySd / size) == 0)
		{
			this.isOutMap = true;
		}
		else if (TileMap.tileTypeAt(this.xSd / size, this.ySd / size) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
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
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00007E8C File Offset: 0x0000608C
	internal new void paintShadow(mGraphics g)
	{
		sbyte size = TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00004887 File Offset: 0x00002A87
	public new void updateSuperEff()
	{
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00007EE0 File Offset: 0x000060E0
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
			return;
		case 3:
			this.updateMobAttack();
			return;
		case 4:
			this.timeStatus = 0;
			this.updateMobFly();
			return;
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
			return;
		default:
			return;
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00007FB4 File Offset: 0x000061B4
	internal void updateDead()
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

	// Token: 0x060000CA RID: 202 RVA: 0x0000807C File Offset: 0x0000627C
	internal void updateMobFly()
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

	// Token: 0x060000CB RID: 203 RVA: 0x00004887 File Offset: 0x00002A87
	public new void setInjure()
	{
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00008160 File Offset: 0x00006360
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx > this.x) ? 1 : (-1));
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

	// Token: 0x060000CD RID: 205 RVA: 0x00004AE0 File Offset: 0x00002CE0
	internal new bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00004887 File Offset: 0x00002A87
	internal new void updateInjure()
	{
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00008228 File Offset: 0x00006428
	internal new void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00008295 File Offset: 0x00006495
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000082A5 File Offset: 0x000064A5
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000082CC File Offset: 0x000064CC
	public new void updateMobAttack()
	{
		if (this.type == 0)
		{
			if (this.tick == this.attack1.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			if (this.tick == 8)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		if (this.type == 1)
		{
			if (this.tick == this.attack2.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
			this.checkFrameTick(this.attack2);
			if (this.tick == 8)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? (-45) : 45), this.y - 25, true, this.dameHP[j], 0, this.charAttack[j], 24);
				}
			}
		}
		if (this.type != 2)
		{
			return;
		}
		if (this.tick == this.fly.Length - 1)
		{
			this.status = 2;
		}
		this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
		this.checkFrameTick(this.fly);
		this.x += (this.charAttack[0].cx - this.x) / 4;
		this.xTo = this.x;
		this.yTo = this.y;
		if (this.tick == 12)
		{
			for (int k = 0; k < this.charAttack.Length; k++)
			{
				this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
				ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
			}
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00004887 File Offset: 0x00002A87
	public new void updateMobWalk()
	{
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00008554 File Offset: 0x00006754
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00004E43 File Offset: 0x00003043
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00004E50 File Offset: 0x00003050
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000085B8 File Offset: 0x000067B8
	public override void paint(mGraphics g)
	{
		if (BigBoss2.data == null || this.isHide)
		{
			return;
		}
		if (!this.isMafuba)
		{
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
			int num3 = this.y - this.h - 5;
			int num4 = imageWidth * 2 * this.per / 100;
			int num5;
			if (num4 > num)
			{
				num5 = num4 - num;
				if (num5 <= 0)
				{
					num5 = 0;
				}
			}
			else
			{
				num = num4;
				num5 = 0;
			}
			g.drawImage(GameScr.imgHP_tm_xam, num2, num3, mGraphics.TOP | mGraphics.LEFT);
			g.drawImage(GameScr.imgHP_tm_xam, num2 + imageWidth, num3, mGraphics.TOP | mGraphics.LEFT);
			g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, num2, num3, mGraphics.TOP | mGraphics.LEFT);
			g.drawRegion(this.imgHPtem, 0, 0, num5, imageHeight, 0, num2 + imageWidth, num3, mGraphics.TOP | mGraphics.LEFT);
			if (this.shock)
			{
				this.tShock++;
				EffecMn.addEff(new Effect((this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1));
				EffecMn.addEff(new Effect((this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1));
				if (this.tShock == 50)
				{
					this.tShock = 0;
					this.shock = false;
				}
			}
			return;
		}
		if (!this.changBody)
		{
			BigBoss2.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			return;
		}
		SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00005108 File Offset: 0x00003308
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00008858 File Offset: 0x00006A58
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

	// Token: 0x060000DA RID: 218 RVA: 0x00008894 File Offset: 0x00006A94
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x > this.x) ? 1 : (-1));
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

	// Token: 0x060000DB RID: 219 RVA: 0x00005214 File Offset: 0x00003414
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000895C File Offset: 0x00006B5C
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00005227 File Offset: 0x00003427
	public new int getH()
	{
		return 40;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00008967 File Offset: 0x00006B67
	public new int getW()
	{
		return 50;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000896C File Offset: 0x00006B6C
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000526A File Offset: 0x0000346A
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000527F File Offset: 0x0000347F
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000089AA File Offset: 0x00006BAA
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000089B3 File Offset: 0x00006BB3
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x040000F0 RID: 240
	public static Image shadowBig;

	// Token: 0x040000F1 RID: 241
	public static EffectData data;

	// Token: 0x040000F2 RID: 242
	public int xTo;

	// Token: 0x040000F3 RID: 243
	public int yTo;

	// Token: 0x040000F4 RID: 244
	public bool haftBody;

	// Token: 0x040000F5 RID: 245
	public bool change;

	// Token: 0x040000F6 RID: 246
	internal Mob mob1;

	// Token: 0x040000F7 RID: 247
	public new int xSd;

	// Token: 0x040000F8 RID: 248
	public new int ySd;

	// Token: 0x040000F9 RID: 249
	internal new bool isOutMap;

	// Token: 0x040000FA RID: 250
	internal new int wCount;

	// Token: 0x040000FB RID: 251
	public new bool isShadown = true;

	// Token: 0x040000FC RID: 252
	internal new int tick;

	// Token: 0x040000FD RID: 253
	internal new int frame;

	// Token: 0x040000FE RID: 254
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x040000FF RID: 255
	internal new bool wy;

	// Token: 0x04000100 RID: 256
	internal new int wt;

	// Token: 0x04000101 RID: 257
	internal new int fy;

	// Token: 0x04000102 RID: 258
	internal new int ty;

	// Token: 0x04000103 RID: 259
	public new int typeSuperEff;

	// Token: 0x04000104 RID: 260
	internal global::Char focus;

	// Token: 0x04000105 RID: 261
	internal int timeDead;

	// Token: 0x04000106 RID: 262
	internal bool flyUp;

	// Token: 0x04000107 RID: 263
	internal bool flyDown;

	// Token: 0x04000108 RID: 264
	internal int dy;

	// Token: 0x04000109 RID: 265
	public bool changePos;

	// Token: 0x0400010A RID: 266
	internal int tShock;

	// Token: 0x0400010B RID: 267
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x0400010C RID: 268
	internal int tA;

	// Token: 0x0400010D RID: 269
	internal global::Char[] charAttack;

	// Token: 0x0400010E RID: 270
	internal int[] dameHP;

	// Token: 0x0400010F RID: 271
	internal sbyte type;

	// Token: 0x04000110 RID: 272
	public new int[] stand = new int[]
	{
		0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
		1, 1
	};

	// Token: 0x04000111 RID: 273
	public new int[] move = new int[]
	{
		1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
		3, 3, 2, 2, 2
	};

	// Token: 0x04000112 RID: 274
	public new int[] moveFast = new int[] { 1, 1, 2, 2, 3, 3, 2 };

	// Token: 0x04000113 RID: 275
	public new int[] attack1 = new int[]
	{
		0, 0, 0, 7, 7, 7, 8, 8, 8, 9,
		9, 9
	};

	// Token: 0x04000114 RID: 276
	public new int[] attack2 = new int[]
	{
		0, 0, 0, 10, 10, 10, 11, 11, 11, 12,
		12, 12
	};

	// Token: 0x04000115 RID: 277
	public int[] attack3 = new int[]
	{
		0, 0, 1, 1, 4, 4, 6, 6, 8, 8,
		25, 25, 26, 26, 28, 28, 30, 30, 32, 32,
		2, 2, 1, 1
	};

	// Token: 0x04000116 RID: 278
	public int[] fly = new int[]
	{
		4, 4, 4, 5, 5, 5, 6, 6, 6, 6,
		6, 6, 3, 3, 3, 2, 2, 2, 1, 1,
		1
	};

	// Token: 0x04000117 RID: 279
	public int[] hitground = new int[]
	{
		6, 6, 6, 3, 3, 3, 2, 2, 2, 1,
		1, 1
	};

	// Token: 0x04000118 RID: 280
	internal bool shock;

	// Token: 0x04000119 RID: 281
	internal new sbyte[] cou = new sbyte[] { -1, 1 };

	// Token: 0x0400011A RID: 282
	public new global::Char injureBy;

	// Token: 0x0400011B RID: 283
	public new bool injureThenDie;

	// Token: 0x0400011C RID: 284
	public new Mob mobToAttack;

	// Token: 0x0400011D RID: 285
	public new int forceWait;

	// Token: 0x0400011E RID: 286
	public new bool blindEff;

	// Token: 0x0400011F RID: 287
	public new bool sleepEff;
}
