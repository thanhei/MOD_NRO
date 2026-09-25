using System;

// Token: 0x0200000F RID: 15
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x0600006F RID: 111 RVA: 0x00004484 File Offset: 0x00002684
	public BachTuoc(int id, short px, short py, int templateID, int hp, int maxHp, int s)
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

	// Token: 0x06000070 RID: 112 RVA: 0x000045B8 File Offset: 0x000027B8
	public void getDataB()
	{
		BachTuoc.data = null;
		BachTuoc.data = new EffectData();
		string text = string.Concat(new string[]
		{
			"/x",
			mGraphics.zoomLevel.ToString(),
			"/effectdata/",
			108.ToString(),
			"/data"
		});
		try
		{
			BachTuoc.data.readData2(text);
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108.ToString() + "/img.png");
		}
		catch (Exception)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0000468C File Offset: 0x0000288C
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000469C File Offset: 0x0000289C
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000046A8 File Offset: 0x000028A8
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

	// Token: 0x06000074 RID: 116 RVA: 0x000046E5 File Offset: 0x000028E5
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00004718 File Offset: 0x00002918
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

	// Token: 0x06000076 RID: 118 RVA: 0x00004834 File Offset: 0x00002A34
	internal new void paintShadow(mGraphics g)
	{
		sbyte size = TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004887 File Offset: 0x00002A87
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000488C File Offset: 0x00002A8C
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
			return;
		default:
			return;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004950 File Offset: 0x00002B50
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

	// Token: 0x0600007A RID: 122 RVA: 0x00004887 File Offset: 0x00002A87
	public new void setInjure()
	{
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004A18 File Offset: 0x00002C18
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

	// Token: 0x0600007C RID: 124 RVA: 0x00004AE0 File Offset: 0x00002CE0
	internal new bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004887 File Offset: 0x00002A87
	internal new void updateInjure()
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004B10 File Offset: 0x00002D10
	internal new void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004B7D File Offset: 0x00002D7D
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00004B8D File Offset: 0x00002D8D
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004BAC File Offset: 0x00002DAC
	public new void updateMobAttack()
	{
		if (this.type == 3)
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
		if (this.type != 4)
		{
			return;
		}
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
				this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
				ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00004D58 File Offset: 0x00002F58
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? (-2) : 2);
		this.y = this.yTo;
		this.dir = ((this.x < this.xTo) ? 1 : (-1));
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00004DE0 File Offset: 0x00002FE0
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00004E43 File Offset: 0x00003043
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004E50 File Offset: 0x00003050
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004E68 File Offset: 0x00003068
	public override void paint(mGraphics g)
	{
		if (BachTuoc.data == null || this.isHide)
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
			BachTuoc.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			return;
		}
		SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00005108 File Offset: 0x00003308
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000510F File Offset: 0x0000330F
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

	// Token: 0x06000089 RID: 137 RVA: 0x0000514C File Offset: 0x0000334C
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

	// Token: 0x0600008A RID: 138 RVA: 0x00005214 File Offset: 0x00003414
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000521C File Offset: 0x0000341C
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00005227 File Offset: 0x00003427
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00005227 File Offset: 0x00003427
	public new int getW()
	{
		return 40;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000522C File Offset: 0x0000342C
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000526A File Offset: 0x0000346A
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000527F File Offset: 0x0000347F
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00005290 File Offset: 0x00003490
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00005299 File Offset: 0x00003499
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000052A2 File Offset: 0x000034A2
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x0400005C RID: 92
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x0400005D RID: 93
	public static EffectData data;

	// Token: 0x0400005E RID: 94
	public int xTo;

	// Token: 0x0400005F RID: 95
	public int yTo;

	// Token: 0x04000060 RID: 96
	public bool haftBody;

	// Token: 0x04000061 RID: 97
	public bool change;

	// Token: 0x04000062 RID: 98
	internal Mob mob1;

	// Token: 0x04000063 RID: 99
	public new int xSd;

	// Token: 0x04000064 RID: 100
	public new int ySd;

	// Token: 0x04000065 RID: 101
	internal new bool isOutMap;

	// Token: 0x04000066 RID: 102
	internal new int wCount;

	// Token: 0x04000067 RID: 103
	public new bool isShadown = true;

	// Token: 0x04000068 RID: 104
	internal new int tick;

	// Token: 0x04000069 RID: 105
	internal new int frame;

	// Token: 0x0400006A RID: 106
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x0400006B RID: 107
	internal new bool wy;

	// Token: 0x0400006C RID: 108
	internal new int wt;

	// Token: 0x0400006D RID: 109
	internal new int fy;

	// Token: 0x0400006E RID: 110
	internal new int ty;

	// Token: 0x0400006F RID: 111
	public new int typeSuperEff;

	// Token: 0x04000070 RID: 112
	internal global::Char focus;

	// Token: 0x04000071 RID: 113
	internal bool flyUp;

	// Token: 0x04000072 RID: 114
	internal bool flyDown;

	// Token: 0x04000073 RID: 115
	internal int dy;

	// Token: 0x04000074 RID: 116
	public bool changePos;

	// Token: 0x04000075 RID: 117
	internal int tShock;

	// Token: 0x04000076 RID: 118
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000077 RID: 119
	internal int tA;

	// Token: 0x04000078 RID: 120
	internal global::Char[] charAttack;

	// Token: 0x04000079 RID: 121
	internal int[] dameHP;

	// Token: 0x0400007A RID: 122
	internal sbyte type;

	// Token: 0x0400007B RID: 123
	public new int[] stand = new int[]
	{
		0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
		1, 1
	};

	// Token: 0x0400007C RID: 124
	public int[] movee = new int[]
	{
		0, 0, 0, 2, 2, 2, 3, 3, 3, 4,
		4, 4
	};

	// Token: 0x0400007D RID: 125
	public new int[] attack1 = new int[]
	{
		0, 0, 0, 4, 4, 4, 5, 5, 5, 6,
		6, 6
	};

	// Token: 0x0400007E RID: 126
	public new int[] attack2 = new int[]
	{
		0, 0, 0, 7, 7, 7, 8, 8, 8, 9,
		9, 9, 10, 10, 10, 11, 11
	};

	// Token: 0x0400007F RID: 127
	public new int[] hurt = new int[] { 1, 1, 7, 7 };

	// Token: 0x04000080 RID: 128
	internal bool shock;

	// Token: 0x04000081 RID: 129
	internal new sbyte[] cou = new sbyte[] { -1, 1 };

	// Token: 0x04000082 RID: 130
	public new global::Char injureBy;

	// Token: 0x04000083 RID: 131
	public new bool injureThenDie;

	// Token: 0x04000084 RID: 132
	public new Mob mobToAttack;

	// Token: 0x04000085 RID: 133
	public new int forceWait;

	// Token: 0x04000086 RID: 134
	public new bool blindEff;

	// Token: 0x04000087 RID: 135
	public new bool sleepEff;
}
