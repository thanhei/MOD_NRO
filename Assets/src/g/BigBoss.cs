using System;

namespace Assets.src.g
{
	// Token: 0x0200009F RID: 159
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0004AFE4 File Offset: 0x000491E4
		public BigBoss(int id, short px, short py, int templateID, long hp, long maxhp, int s)
		{
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.mobId = id;
			this.hp = hp;
			this.maxHp = maxhp;
			this.templateId = templateID;
			this.w_hp_bar = 100;
			this.h_hp_bar = 6;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			if (s == 0)
			{
				this.getDataB();
			}
			if (s == 1)
			{
				this.getDataB2();
			}
			if (s == 2)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.status = 2;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0004B1BC File Offset: 0x000493BC
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				100,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100 + "/img.png");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0004B298 File Offset: 0x00049498
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				101,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00006CB9 File Offset: 0x00004EB9
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00006CC9 File Offset: 0x00004EC9
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0004A37C File Offset: 0x0004857C
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

		// Token: 0x060005D2 RID: 1490 RVA: 0x00006EAD File Offset: 0x000050AD
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0004B378 File Offset: 0x00049578
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

		// Token: 0x060005D4 RID: 1492 RVA: 0x0004B4B0 File Offset: 0x000496B0
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000045ED File Offset: 0x000027ED
		public new void updateSuperEff()
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0004B500 File Offset: 0x00049700
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

		// Token: 0x060005D7 RID: 1495 RVA: 0x0004B5FC File Offset: 0x000497FC
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0004B6E0 File Offset: 0x000498E0
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

		// Token: 0x060005D9 RID: 1497 RVA: 0x000045ED File Offset: 0x000027ED
		public new void setInjure()
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0004B7D0 File Offset: 0x000499D0
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

		// Token: 0x060005DB RID: 1499 RVA: 0x00006D07 File Offset: 0x00004F07
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000045ED File Offset: 0x000027ED
		private void updateInjure()
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0004B8B0 File Offset: 0x00049AB0
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00006EE2 File Offset: 0x000050E2
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0004B93C File Offset: 0x00049B3C
		public void setAttack(global::Char[] cAttack, long[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			if ((int)type < 3)
			{
				this.status = 3;
			}
			if ((int)type == 3)
			{
				this.flyUp = true;
				this.status = 4;
			}
			if ((int)type == 4)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0L, false, false);
				}
			}
			if ((int)type == 7)
			{
				this.status = 3;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0004B9D4 File Offset: 0x00049BD4
		public new void updateMobAttack()
		{
			if ((int)this.type == 7)
			{
				if (this.tick > 8)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			if ((int)this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick(this.attack1);
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, this.dameHP[i], 0L, this.charAttack[i], 24);
					}
				}
			}
			if ((int)this.type == 1)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				if (this.tick == 18)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0L, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			if ((int)this.type == 8)
			{
			}
			if ((int)this.type == 2)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
				if (this.tick == 13)
				{
					GameScr.shock_scr = 10;
					this.shock = true;
					for (int k = 0; k < this.charAttack.Length; k++)
					{
						this.charAttack[k].doInjure(this.dameHP[k], 0L, false, false);
					}
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000045ED File Offset: 0x000027ED
		public new void updateMobWalk()
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0004AAC4 File Offset: 0x00048CC4
		public new bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00006D6E File Offset: 0x00004F6E
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00006D7E File Offset: 0x00004F7E
		public new bool checkIsBoss()
		{
			return this.isBoss || (int)this.levelBoss > 0;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0004BD18 File Offset: 0x00049F18
		public override void paint(mGraphics g)
		{
			if (BigBoss.data == null)
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
					BigBoss.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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
				BigBoss.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
				Res.outz("type= " + this.type);
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

		// Token: 0x060005E6 RID: 1510 RVA: 0x00006D9B File Offset: 0x00004F9B
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00006EF2 File Offset: 0x000050F2
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

		// Token: 0x060005E8 RID: 1512 RVA: 0x0004C02C File Offset: 0x0004A22C
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

		// Token: 0x060005E9 RID: 1513 RVA: 0x00006DDE File Offset: 0x00004FDE
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00006F2E File Offset: 0x0000512E
		public new int getY()
		{
			return (!this.haftBody) ? (this.y - 60) : (this.y - 20);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00006F52 File Offset: 0x00005152
		public new int getW()
		{
			return 60;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0004C10C File Offset: 0x0004A30C
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00006DF5 File Offset: 0x00004FF5
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00006E0E File Offset: 0x0000500E
		public new void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00006F56 File Offset: 0x00005156
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00006F5F File Offset: 0x0000515F
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x04000A81 RID: 2689
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04000A82 RID: 2690
		public static EffectData data;

		// Token: 0x04000A83 RID: 2691
		public int xTo;

		// Token: 0x04000A84 RID: 2692
		public int yTo;

		// Token: 0x04000A85 RID: 2693
		public bool haftBody;

		// Token: 0x04000A86 RID: 2694
		public bool change;

		// Token: 0x04000A87 RID: 2695
		public new int xSd;

		// Token: 0x04000A88 RID: 2696
		public new int ySd;

		// Token: 0x04000A89 RID: 2697
		private bool isOutMap;

		// Token: 0x04000A8A RID: 2698
		private int wCount;

		// Token: 0x04000A8B RID: 2699
		public new bool isShadown = true;

		// Token: 0x04000A8C RID: 2700
		private int tick;

		// Token: 0x04000A8D RID: 2701
		private int frame;

		// Token: 0x04000A8E RID: 2702
		private bool wy;

		// Token: 0x04000A8F RID: 2703
		private int wt;

		// Token: 0x04000A90 RID: 2704
		private int fy;

		// Token: 0x04000A91 RID: 2705
		private int ty;

		// Token: 0x04000A92 RID: 2706
		public new int typeSuperEff;

		// Token: 0x04000A93 RID: 2707
		private global::Char focus;

		// Token: 0x04000A94 RID: 2708
		private bool flyUp;

		// Token: 0x04000A95 RID: 2709
		private bool flyDown;

		// Token: 0x04000A96 RID: 2710
		private int dy;

		// Token: 0x04000A97 RID: 2711
		public bool changePos;

		// Token: 0x04000A98 RID: 2712
		private int tShock;

		// Token: 0x04000A99 RID: 2713
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04000A9A RID: 2714
		private int tA;

		// Token: 0x04000A9B RID: 2715
		private global::Char[] charAttack;

		// Token: 0x04000A9C RID: 2716
		private long[] dameHP;

		// Token: 0x04000A9D RID: 2717
		private sbyte type;

		// Token: 0x04000A9E RID: 2718
		public new int[] stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		};

		// Token: 0x04000A9F RID: 2719
		public int[] stand_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			38,
			39,
			39,
			40,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			38
		};

		// Token: 0x04000AA0 RID: 2720
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

		// Token: 0x04000AA1 RID: 2721
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

		// Token: 0x04000AA2 RID: 2722
		public new int[] attack1 = new int[]
		{
			0,
			0,
			34,
			34,
			35,
			35,
			36,
			36,
			2,
			2,
			1,
			1
		};

		// Token: 0x04000AA3 RID: 2723
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			6,
			6,
			9,
			9,
			10,
			10,
			13,
			13,
			15,
			15,
			17,
			17,
			19,
			19,
			21,
			21,
			23,
			23
		};

		// Token: 0x04000AA4 RID: 2724
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

		// Token: 0x04000AA5 RID: 2725
		public int[] attack2_1 = new int[]
		{
			37,
			37,
			5,
			5,
			7,
			7,
			11,
			11,
			14,
			14,
			16,
			16,
			18,
			18,
			20,
			20,
			22,
			22,
			24,
			24
		};

		// Token: 0x04000AA6 RID: 2726
		public int[] attack3_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			5,
			5,
			7,
			7,
			11,
			11,
			27,
			27,
			29,
			29,
			31,
			31,
			33,
			33,
			38,
			38
		};

		// Token: 0x04000AA7 RID: 2727
		public int[] fly = new int[]
		{
			8,
			8,
			9,
			9,
			10,
			10,
			12,
			12
		};

		// Token: 0x04000AA8 RID: 2728
		public int[] hitground = new int[]
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

		// Token: 0x04000AA9 RID: 2729
		private bool shock;

		// Token: 0x04000AAA RID: 2730
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04000AAB RID: 2731
		public new global::Char injureBy;

		// Token: 0x04000AAC RID: 2732
		public new bool injureThenDie;

		// Token: 0x04000AAD RID: 2733
		public new Mob mobToAttack;

		// Token: 0x04000AAE RID: 2734
		public new int forceWait;

		// Token: 0x04000AAF RID: 2735
		public new bool blindEff;

		// Token: 0x04000AB0 RID: 2736
		public new bool sleepEff;
	}
}
