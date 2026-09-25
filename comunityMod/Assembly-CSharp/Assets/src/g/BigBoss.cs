using System;

namespace Assets.src.g
{
	// Token: 0x020001B3 RID: 435
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x000C3AEC File Offset: 0x000C1CEC
		public BigBoss(int id, short px, short py, int templateID, int hp, int maxhp, int s)
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

		// Token: 0x0600128F RID: 4751 RVA: 0x000C3CB8 File Offset: 0x000C1EB8
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				100.ToString(),
				"/data"
			});
			try
			{
				BigBoss.data.readData2(text);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100.ToString() + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000C3D94 File Offset: 0x000C1F94
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				101.ToString(),
				"/data"
			});
			try
			{
				BigBoss.data.readData2(text);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0000468C File Offset: 0x0000288C
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0000469C File Offset: 0x0000289C
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000C3E74 File Offset: 0x000C2074
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

		// Token: 0x06001294 RID: 4756 RVA: 0x000C3EB1 File Offset: 0x000C20B1
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000C3EE4 File Offset: 0x000C20E4
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

		// Token: 0x06001296 RID: 4758 RVA: 0x000C4000 File Offset: 0x000C2200
		internal new void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00004887 File Offset: 0x00002A87
		public new void updateSuperEff()
		{
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000C4050 File Offset: 0x000C2250
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

		// Token: 0x06001299 RID: 4761 RVA: 0x000C4124 File Offset: 0x000C2324
		internal void updateDead()
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

		// Token: 0x0600129A RID: 4762 RVA: 0x000C41FC File Offset: 0x000C23FC
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

		// Token: 0x0600129B RID: 4763 RVA: 0x00004887 File Offset: 0x00002A87
		public new void setInjure()
		{
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000C42E0 File Offset: 0x000C24E0
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

		// Token: 0x0600129D RID: 4765 RVA: 0x00004AE0 File Offset: 0x00002CE0
		internal new bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00004887 File Offset: 0x00002A87
		internal new void updateInjure()
		{
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000C43A8 File Offset: 0x000C25A8
		internal new void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000C4425 File Offset: 0x000C2625
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000C4438 File Offset: 0x000C2638
		public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			if (type < 3)
			{
				this.status = 3;
			}
			if (type == 3)
			{
				this.flyUp = true;
				this.status = 4;
			}
			if (type == 4)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				}
			}
			if (type == 7)
			{
				this.status = 3;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000C44B8 File Offset: 0x000C26B8
		public new void updateMobAttack()
		{
			if (this.type == 7)
			{
				if (this.tick > 8)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? (-15) : 15), this.y - 40, 1);
				}
			}
			if (this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
				this.checkFrameTick(this.attack1);
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? (-45) : 45), this.y - 30, true, this.dameHP[i], 0, this.charAttack[i], 24);
					}
				}
			}
			if (this.type == 1)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				if (this.tick == 18)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			sbyte b = this.type;
			if (this.type != 2)
			{
				return;
			}
			if (this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1)))
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : (-1));
			this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
			if (this.tick == 13)
			{
				GameScr.shock_scr = 10;
				this.shock = true;
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
				}
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00004887 File Offset: 0x00002A87
		public new void updateMobWalk()
		{
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000C478C File Offset: 0x000C298C
		public new bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00004E43 File Offset: 0x00003043
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00004E50 File Offset: 0x00003050
		public new bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000C47F0 File Offset: 0x000C29F0
		public override void paint(mGraphics g)
		{
			if (BigBoss.data == null || this.isHide)
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
					Res.outz("type= " + this.type.ToString());
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
				BigBoss.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00005108 File Offset: 0x00003308
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000C4AAA File Offset: 0x000C2CAA
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

		// Token: 0x060012AA RID: 4778 RVA: 0x000C4AE4 File Offset: 0x000C2CE4
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

		// Token: 0x060012AB RID: 4779 RVA: 0x00005214 File Offset: 0x00003414
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000C4BAC File Offset: 0x000C2DAC
		public new int getY()
		{
			if (this.haftBody)
			{
				return this.y - 20;
			}
			return this.y - 60;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00005227 File Offset: 0x00003427
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000C4BC9 File Offset: 0x000C2DC9
		public new int getW()
		{
			return 60;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0000526A File Offset: 0x0000346A
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0000527F File Offset: 0x0000347F
		public new void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000C4C0E File Offset: 0x000C2E0E
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000C4C17 File Offset: 0x000C2E17
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x0400199F RID: 6559
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x040019A0 RID: 6560
		public static EffectData data;

		// Token: 0x040019A1 RID: 6561
		public int xTo;

		// Token: 0x040019A2 RID: 6562
		public int yTo;

		// Token: 0x040019A3 RID: 6563
		public bool haftBody;

		// Token: 0x040019A4 RID: 6564
		public bool change;

		// Token: 0x040019A5 RID: 6565
		public new int xSd;

		// Token: 0x040019A6 RID: 6566
		public new int ySd;

		// Token: 0x040019A7 RID: 6567
		internal new bool isOutMap;

		// Token: 0x040019A8 RID: 6568
		internal new int wCount;

		// Token: 0x040019A9 RID: 6569
		public new bool isShadown = true;

		// Token: 0x040019AA RID: 6570
		internal new int tick;

		// Token: 0x040019AB RID: 6571
		internal new int frame;

		// Token: 0x040019AC RID: 6572
		internal new bool wy;

		// Token: 0x040019AD RID: 6573
		internal new int wt;

		// Token: 0x040019AE RID: 6574
		internal new int fy;

		// Token: 0x040019AF RID: 6575
		internal new int ty;

		// Token: 0x040019B0 RID: 6576
		public new int typeSuperEff;

		// Token: 0x040019B1 RID: 6577
		internal global::Char focus;

		// Token: 0x040019B2 RID: 6578
		internal bool flyUp;

		// Token: 0x040019B3 RID: 6579
		internal bool flyDown;

		// Token: 0x040019B4 RID: 6580
		internal int dy;

		// Token: 0x040019B5 RID: 6581
		public bool changePos;

		// Token: 0x040019B6 RID: 6582
		internal int tShock;

		// Token: 0x040019B7 RID: 6583
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040019B8 RID: 6584
		internal int tA;

		// Token: 0x040019B9 RID: 6585
		internal global::Char[] charAttack;

		// Token: 0x040019BA RID: 6586
		internal int[] dameHP;

		// Token: 0x040019BB RID: 6587
		internal sbyte type;

		// Token: 0x040019BC RID: 6588
		public new int[] stand = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1
		};

		// Token: 0x040019BD RID: 6589
		public int[] stand_1 = new int[]
		{
			37, 37, 37, 38, 38, 38, 39, 39, 40, 40,
			40, 39, 39, 39, 38, 38, 38
		};

		// Token: 0x040019BE RID: 6590
		public new int[] move = new int[]
		{
			1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
			3, 3, 2, 2, 2
		};

		// Token: 0x040019BF RID: 6591
		public new int[] moveFast = new int[] { 1, 1, 2, 2, 3, 3, 2 };

		// Token: 0x040019C0 RID: 6592
		public new int[] attack1 = new int[]
		{
			0, 0, 34, 34, 35, 35, 36, 36, 2, 2,
			1, 1
		};

		// Token: 0x040019C1 RID: 6593
		public new int[] attack2 = new int[]
		{
			0, 0, 0, 4, 4, 6, 6, 9, 9, 10,
			10, 13, 13, 15, 15, 17, 17, 19, 19, 21,
			21, 23, 23
		};

		// Token: 0x040019C2 RID: 6594
		public int[] attack3 = new int[]
		{
			0, 0, 1, 1, 4, 4, 6, 6, 8, 8,
			25, 25, 26, 26, 28, 28, 30, 30, 32, 32,
			2, 2, 1, 1
		};

		// Token: 0x040019C3 RID: 6595
		public int[] attack2_1 = new int[]
		{
			37, 37, 5, 5, 7, 7, 11, 11, 14, 14,
			16, 16, 18, 18, 20, 20, 22, 22, 24, 24
		};

		// Token: 0x040019C4 RID: 6596
		public int[] attack3_1 = new int[]
		{
			37, 37, 37, 38, 38, 5, 5, 7, 7, 11,
			11, 27, 27, 29, 29, 31, 31, 33, 33, 38,
			38
		};

		// Token: 0x040019C5 RID: 6597
		public int[] fly = new int[] { 8, 8, 9, 9, 10, 10, 12, 12 };

		// Token: 0x040019C6 RID: 6598
		public int[] hitground = new int[]
		{
			0, 0, 1, 1, 4, 4, 6, 6, 8, 8,
			25, 25, 26, 26, 28, 28, 30, 30, 32, 32,
			2, 2, 1, 1
		};

		// Token: 0x040019C7 RID: 6599
		internal bool shock;

		// Token: 0x040019C8 RID: 6600
		internal new sbyte[] cou = new sbyte[] { -1, 1 };

		// Token: 0x040019C9 RID: 6601
		public new global::Char injureBy;

		// Token: 0x040019CA RID: 6602
		public new bool injureThenDie;

		// Token: 0x040019CB RID: 6603
		public new Mob mobToAttack;

		// Token: 0x040019CC RID: 6604
		public new int forceWait;

		// Token: 0x040019CD RID: 6605
		public new bool blindEff;

		// Token: 0x040019CE RID: 6606
		public new bool sleepEff;
	}
}
