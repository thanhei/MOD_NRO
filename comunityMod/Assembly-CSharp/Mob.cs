using System;
using System.Runtime.CompilerServices;
using Assets.src.g;

// Token: 0x0200006C RID: 108
public class Mob : IMapObject
{
	// Token: 0x06000550 RID: 1360 RVA: 0x000533D8 File Offset: 0x000515D8
	public Mob()
	{
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00053564 File Offset: 0x00051764
	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, int hp, sbyte level, int maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		this.pointx = pointx;
		this.x = (int)pointx;
		this.xFirst = (int)pointx;
		this.pointy = pointy;
		this.y = (int)pointy;
		this.yFirst = (int)pointy;
		this.status = (int)status;
		if (templateId != 70)
		{
			this.checkData();
			this.getData();
		}
		if (!Mob.isExistNewMob(templateId.ToString() + string.Empty))
		{
			Mob.newMob.addElement(templateId.ToString() + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.updateHp_bar();
		this.per_tem = (int)((long)hp * 100L / (long)this.maxHp);
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		if (this.isNewModStand())
		{
			this.stand = new int[]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			this.move = new int[]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			this.moveFast = new int[]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			this.attack1 = new int[]
			{
				3, 3, 3, 3, 4, 4, 4, 4, 5, 5,
				5, 5
			};
			this.attack2 = new int[]
			{
				3, 3, 3, 3, 4, 4, 4, 4, 5, 5,
				5, 5
			};
			return;
		}
		if (this.isNewMod())
		{
			this.stand = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
				1, 1
			};
			this.move = new int[]
			{
				1, 1, 1, 1, 2, 2, 2, 2, 1, 1,
				1, 1, 3, 3, 3, 3
			};
			this.moveFast = new int[] { 1, 1, 2, 2, 1, 1, 3, 3 };
			this.attack1 = new int[]
			{
				4, 4, 4, 5, 5, 5, 6, 6, 6, 6,
				6
			};
			this.attack2 = new int[]
			{
				7, 7, 7, 8, 8, 8, 9, 9, 9, 9,
				9
			};
			return;
		}
		if (this.isSpecial())
		{
			this.stand = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
				1, 1
			};
			this.move = new int[]
			{
				2, 2, 3, 3, 2, 2, 4, 4, 2, 2,
				3, 3, 2, 2, 4, 4
			};
			this.moveFast = new int[] { 2, 2, 3, 3, 2, 2, 4, 4 };
			this.attack1 = new int[] { 5, 6, 7, 8, 9, 10, 11, 12 };
			this.attack2 = new int[] { 5, 12, 13, 14 };
			return;
		}
		this.stand = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1
		};
		this.move = new int[]
		{
			1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
			3, 3, 2, 2, 2
		};
		this.moveFast = new int[] { 1, 1, 2, 2, 3, 3, 2 };
		this.attack1 = new int[] { 4, 5, 6 };
		this.attack2 = new int[] { 7, 8, 9 };
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x000539F5 File Offset: 0x00051BF5
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00053A1C File Offset: 0x00051C1C
	public void getData()
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId.ToString();
			if (MyStream.readFile(text) != null)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			if (Mob.lastMob.size() > 15)
			{
				Mob.arrMobTemplate[int.Parse((string)Mob.lastMob.elementAt(0))].data = null;
				Mob.lastMob.removeElementAt(0);
			}
			Mob.lastMob.addElement(this.templateId.ToString() + string.Empty);
			return;
		}
		this.w = Mob.arrMobTemplate[this.templateId].data.width;
		this.h = Mob.arrMobTemplate[this.templateId].data.height;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0000468C File Offset: 0x0000288C
	public virtual void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0000469C File Offset: 0x0000289C
	public virtual void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00053B60 File Offset: 0x00051D60
	public static bool isExistNewMob(string id)
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

	// Token: 0x06000557 RID: 1367 RVA: 0x00053BA0 File Offset: 0x00051DA0
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			if (Mob.arrMobTemplate[i].data != null)
			{
				num++;
			}
		}
		if (num < 10)
		{
			return;
		}
		for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
		{
			if (Mob.arrMobTemplate[j].data != null && num > 5)
			{
				Mob.arrMobTemplate[j].data = null;
			}
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00053C0A File Offset: 0x00051E0A
	public void checkFrameTick(int[] array)
	{
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
		this.tick++;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00053C3C File Offset: 0x00051E3C
	internal void updateShadown()
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

	// Token: 0x0600055A RID: 1370 RVA: 0x00053D58 File Offset: 0x00051F58
	internal void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
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
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00053EA4 File Offset: 0x000520A4
	public void updateSuperEff()
	{
		if (this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		if (this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00053F08 File Offset: 0x00052108
	public virtual void update()
	{
		if (this.isMafuba)
		{
			return;
		}
		this.GetFrame();
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this.x, this.y, 1);
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
		}
		if (!GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
				{
					global::Char char2 = new global::Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					if (@char.cgender == 0)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char2, 25);
					}
				}
			}
			if (global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32)
			{
				global::Char char3 = new global::Char();
				char3.cx = global::Char.myCharz().cx;
				char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
				if (global::Char.myCharz().cgender == 0)
				{
					MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char3, 25);
				}
			}
		}
		if (this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
		{
			EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			long num = mSystem.currentTimeMillis();
			if (num - this.last >= 1000L)
			{
				this.seconds--;
				this.last = num;
				if (this.seconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
				}
			}
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 11;
				}
				else
				{
					this.frame = 10;
				}
			}
			else if (this.isSpecial())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (GameCanvas.gameTick % 20 > 5)
			{
				this.frame = 11;
			}
			else
			{
				this.frame = 10;
			}
		}
		if (!this.isUpdate())
		{
			return;
		}
		if (this.isShadown)
		{
			this.updateShadown();
		}
		if (this.vMobMove == null && Mob.arrMobTemplate[this.templateId].rangeMove != 0)
		{
			return;
		}
		if (this.status != 3 && this.isBusyAttackSomeOne)
		{
			if (this.cFocus != null)
			{
				this.cFocus.doInjure(this.dame, this.dameMp, false, true);
			}
			else if (this.mobToAttack != null)
			{
				this.mobToAttack.setInjure();
			}
			this.isBusyAttackSomeOne = false;
		}
		if (this.levelBoss > 0)
		{
			this.updateSuperEff();
		}
		switch (this.status)
		{
		case 1:
			this.isDisable = false;
			this.isDontMove = false;
			this.isFire = false;
			this.isIce = false;
			this.isWind = false;
			this.y += this.p1;
			if (GameCanvas.gameTick % 2 == 0)
			{
				if (this.p2 > 1)
				{
					this.p2--;
				}
				else if (this.p2 < -1)
				{
					this.p2++;
				}
			}
			this.x += this.p2;
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				this.frame = 11;
			}
			else if (this.isSpecial())
			{
				this.frame = 15;
			}
			else
			{
				this.frame = 11;
			}
			if (this.isDie)
			{
				this.isDie = false;
				if (this.isMobMe)
				{
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						if (((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId)
						{
							GameScr.vMob.removeElementAt(j);
						}
					}
				}
				this.p1 = 0;
				this.p2 = 0;
				this.x = (this.y = 0);
				this.hp = this.getTemplate().hp;
				this.status = 0;
				this.timeStatus = 0;
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2)
			{
				this.p1 = ((this.p1 <= 4) ? (-this.p1) : (-4));
				if (this.p3 == 0)
				{
					this.p3 = 16;
				}
			}
			else
			{
				this.p1++;
			}
			if (this.p3 > 0)
			{
				this.p3--;
				if (this.p3 == 0)
				{
					this.isDie = true;
					return;
				}
			}
			break;
		case 2:
			if (this.holdEffID == 0 && !this.isFreez && !this.blindEff && !this.sleepEff)
			{
				this.timeStatus = 0;
				this.updateMobStandWait();
				return;
			}
			break;
		case 3:
			if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez)
			{
				this.updateMobAttack();
				return;
			}
			break;
		case 4:
			if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez)
			{
				this.timeStatus = 0;
				this.p1++;
				if (this.p1 > 40 + this.mobId % 5)
				{
					this.y -= 2;
					this.status = 5;
					this.p1 = 0;
					return;
				}
			}
			break;
		case 5:
			if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff)
			{
				if (!this.isFreez)
				{
					this.timeStatus = 0;
					this.updateMobWalk();
					return;
				}
				if (Mob.arrMobTemplate[this.templateId].type == 4)
				{
					this.ty++;
					this.wt++;
					this.fy += ((!this.wy) ? 1 : (-1));
					if (this.wt == 10)
					{
						this.wt = 0;
						this.wy = !this.wy;
						return;
					}
				}
			}
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
				return;
			}
			break;
		case 7:
			this.updateInjure();
			break;
		default:
			return;
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000546D8 File Offset: 0x000528D8
	public void setInjure()
	{
		if (this.hp > 0 && this.status != 3 && this.status != 7)
		{
			this.timeStatus = 4;
			this.status = 7;
			if (this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00054748 File Offset: 0x00052948
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0005478C File Offset: 0x0005298C
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss2)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000547D0 File Offset: 0x000529D0
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BachTuoc)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00054814 File Offset: 0x00052A14
	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
		if (mob is NewBoss)
		{
			return (NewBoss)mob;
		}
		return null;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00054844 File Offset: 0x00052A44
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				GameScr.vMob.removeElement(mob);
				return;
			}
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0005488C File Offset: 0x00052A8C
	public void setAttack(global::Char cFocus)
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
			this.p3 = 0;
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00004AE0 File Offset: 0x00002CE0
	internal bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0005492B File Offset: 0x00052B2B
	internal bool isNewModStand()
	{
		return this.templateId == 76;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00054937 File Offset: 0x00052B37
	internal bool isNewMod()
	{
		return this.templateId >= 73 && !this.isNewModStand();
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00054950 File Offset: 0x00052B50
	internal void updateInjure()
	{
		if (!this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
		{
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (this.frame != 10)
				{
					this.frame = 10;
				}
				else
				{
					this.frame = 11;
				}
			}
			else if (this.isSpecial())
			{
				if (this.frame != 1)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (this.frame != 10)
			{
				this.frame = 10;
			}
			else
			{
				this.frame = 11;
			}
		}
		this.timeStatus--;
		if (this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11)))
		{
			if ((this.injureBy != null && this.injureThenDie) || this.hp == 0)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				if (this.injureBy != null)
				{
					this.dir = -this.injureBy.cdir;
					if (Res.abs(this.x - this.injureBy.cx) < 24)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
			return;
		}
		if (Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null)
		{
			int num = -this.injureBy.cdir << 1;
			if (this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.x -= num;
			}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00054BB8 File Offset: 0x00052DB8
	internal void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		sbyte type = Mob.arrMobTemplate[this.templateId].type;
		if (type > 3)
		{
			if (type - 4 <= 1)
			{
				this.p1++;
				if (this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
				{
					this.status = 5;
				}
			}
		}
		else
		{
			this.p1++;
			if (this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
		}
		if (this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.cFocus.cx > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else if (this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.mobToAttack.x > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		if (this.forceWait > 0)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00054D74 File Offset: 0x00052F74
	public void updateMobAttack()
	{
		int[] array = ((this.p3 != 0) ? this.attack2 : this.attack1);
		if (this.tick < array.Length)
		{
			this.checkFrameTick(array);
			if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		if (this.p1 == 0)
		{
			int num = ((this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx);
			int num2 = ((this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy);
			if (!this.isNewMod())
			{
				if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
				if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
			}
			if ((Mob.arrMobTemplate[this.templateId].type == 4 || Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			if (this.p2 > array.Length - 1 || this.p1 == 1)
			{
				this.p1 = 1;
				if (this.p3 == 0)
				{
					if (this.cFocus != null)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						this.mobToAttack.setInjure();
					}
					this.isBusyAttackSomeOne = false;
				}
				else
				{
					if (this.cFocus != null)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
					}
					else
					{
						global::Char @char = new global::Char();
						@char.cx = this.mobToAttack.x;
						@char.cy = this.mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
					}
					this.isBusyAttackSomeOne = false;
				}
			}
			this.dir = ((this.x < num) ? 1 : (-1));
		}
		else if (this.p1 == 1)
		{
			if (Mob.arrMobTemplate[this.templateId].type != 0 && !this.isDontMove && !this.isIce)
			{
				bool flag = this.isWind;
			}
			if (this.tick == array.Length)
			{
				this.status = 2;
				this.p1 = 0;
				this.p2 = 0;
				this.tick = 0;
			}
		}
		if (this.tick == 5 && this.cFocus != null && this.cFocus.charID == global::Char.myCharz().charID)
		{
			if (this.templateId == 88 && this.p3 != 0)
			{
				GameScr.shock_scr = 2;
			}
			if (this.templateId == 89)
			{
				GameScr.shock_scr = 2;
			}
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x000550CC File Offset: 0x000532CC
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			if (this.injureThenDie)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			if (!this.isIce)
			{
				if (this.isDontMove || this.isWind)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
						if (this.isNewModStand())
						{
							this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
						}
						else
						{
							this.frame = 0;
						}
						num = 2;
						break;
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						if (b == 1)
						{
							if (GameCanvas.gameTick % 2 == 1)
							{
								break;
							}
						}
						else if (b > 2)
						{
							b += (sbyte)(this.mobId % 2);
						}
						else if (GameCanvas.gameTick % 2 == 1)
						{
							b -= 1;
						}
						this.x += (int)b * this.dir;
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
						}
						if (Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : (-1));
							if (Res.abs(this.x - global::Char.myCharz().cx) < 20)
							{
								this.x -= this.dir * 10;
							}
							this.status = 2;
							this.forceWait = 20;
						}
						this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
						break;
					}
					case 4:
					{
						num = 4;
						sbyte b2 = Mob.arrMobTemplate[this.templateId].speed + (sbyte)(this.mobId % 2);
						this.x += (int)b2 * this.dir;
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed + (sbyte)(this.mobId % 2);
						this.x += (int)b3 * this.dir;
						b3 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b3 * this.dirV;
						}
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						if (TileMap.tileTypeAt(this.x, this.y, 2))
						{
							if (GameCanvas.gameTick % 10 > 5)
							{
								this.y = TileMap.tileYofPixel(this.y);
								this.status = 4;
								this.p1 = 0;
								this.dirV = -1;
							}
							else
							{
								this.dirV = -1;
							}
						}
						break;
					}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("lineee: " + num.ToString());
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00055610 File Offset: 0x00053810
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00055620 File Offset: 0x00053820
	public bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000556C2 File Offset: 0x000538C2
	public bool isUpdate()
	{
		return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00004E50 File Offset: 0x00003050
	public bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x000556F4 File Offset: 0x000538F4
	public void updateHp_bar()
	{
		this.len = (int)((long)this.hp * 100L / (long)this.maxHp * (long)this.w_hp_bar) / 100;
		this.per = (int)((long)this.hp * 100L / (long)this.maxHp);
		if (this.per == 100)
		{
			this.per_tem = this.per;
		}
		if (this.per >= 100)
		{
			this.per_tem = this.per;
		}
		this.offset = 0;
		if (this.per < 30)
		{
			this.color = 15473700;
			this.imgHPtem = GameScr.imgHP_tm_do;
			return;
		}
		if (this.per < 60)
		{
			this.color = 16744448;
			this.imgHPtem = GameScr.imgHP_tm_vang;
			return;
		}
		this.color = 11992374;
		this.imgHPtem = GameScr.imgHP_tm_xanh;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x000557CC File Offset: 0x000539CC
	public virtual void paint(mGraphics g)
	{
		if (this.isHide)
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
			if (this.isShadown && this.status != 0)
			{
				this.paintShadow(g);
			}
			if (!this.isPaint() || (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0))
			{
				return;
			}
			g.translate(0, GameCanvas.transY);
			if (!this.changBody)
			{
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			g.translate(0, -GameCanvas.transY);
			if (global::Char.myCharz().mobFocus == null || !global::Char.myCharz().mobFocus.Equals(this) || this.status == 1 || this.hp <= 0 || this.imgHPtem == null)
			{
				return;
			}
			int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
			int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
			int num = imageWidth * this.per / 100;
			int num2 = num;
			if (this.per_tem >= this.per)
			{
				int num3 = imageWidth;
				int num4 = this.per_tem;
				int num6;
				if (GameCanvas.gameTick % 6 > 3)
				{
					int num5 = this.offset;
					this.offset = num5 + 1;
					num6 = num5;
				}
				else
				{
					num6 = this.offset;
				}
				num2 = num3 * (this.per_tem = num4 - num6) / 100;
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
			g.drawImage(GameScr.imgHP_tm_xam, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
			g.setColor(16777215);
			g.fillRect(this.x - (imageWidth >> 1), this.y - this.h - 5, num2, 2);
			g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
			return;
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00005108 File Offset: 0x00003308
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00055AA8 File Offset: 0x00053CA8
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00055AF8 File Offset: 0x00053CF8
	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x > this.x) ? 1 : (-1));
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		if (Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2)
		{
			if (this.x < num)
			{
				this.x = num - this.w;
			}
			else
			{
				this.x = num + this.w;
			}
			this.p3 = 0;
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00005214 File Offset: 0x00003414
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00055BC0 File Offset: 0x00053DC0
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00055BC8 File Offset: 0x00053DC8
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00055BD0 File Offset: 0x00053DD0
	public int getW()
	{
		return this.w;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00055BD8 File Offset: 0x00053DD8
	public void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0000526A File Offset: 0x0000346A
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0000527F File Offset: 0x0000347F
	public void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00055C16 File Offset: 0x00053E16
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00055C1F File Offset: 0x00053E1F
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00055C28 File Offset: 0x00053E28
	public void GetFrame()
	{
		if (this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
			this.stand = this.frameArr[0];
			this.move = this.frameArr[1];
			this.moveFast = this.frameArr[2];
			this.attack1 = this.frameArr[3];
			this.attack2 = this.frameArr[4];
			this.hurt = this.frameArr[5];
			this.isGetFr = false;
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00055CE6 File Offset: 0x00053EE6
	internal bool isTypeNewMod()
	{
		return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
	}

	// Token: 0x04000B3A RID: 2874
	public int countDie;

	// Token: 0x04000B3B RID: 2875
	public long lastTimeDie;

	// Token: 0x04000B3C RID: 2876
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000B3D RID: 2877
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000B3E RID: 2878
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000B3F RID: 2879
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000B40 RID: 2880
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000B41 RID: 2881
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000B42 RID: 2882
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000B43 RID: 2883
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000B44 RID: 2884
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000B45 RID: 2885
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000B46 RID: 2886
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000B47 RID: 2887
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000B48 RID: 2888
	public const sbyte MA_WALK = 5;

	// Token: 0x04000B49 RID: 2889
	public const sbyte MA_FALL = 6;

	// Token: 0x04000B4A RID: 2890
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000B4B RID: 2891
	public bool changBody;

	// Token: 0x04000B4C RID: 2892
	public short smallBody;

	// Token: 0x04000B4D RID: 2893
	public bool isHintFocus;

	// Token: 0x04000B4E RID: 2894
	public string flystring;

	// Token: 0x04000B4F RID: 2895
	public int flyx;

	// Token: 0x04000B50 RID: 2896
	public int flyy;

	// Token: 0x04000B51 RID: 2897
	public int flyIndex;

	// Token: 0x04000B52 RID: 2898
	public bool isFreez;

	// Token: 0x04000B53 RID: 2899
	public int seconds;

	// Token: 0x04000B54 RID: 2900
	public long last;

	// Token: 0x04000B55 RID: 2901
	public long cur;

	// Token: 0x04000B56 RID: 2902
	public int holdEffID;

	// Token: 0x04000B57 RID: 2903
	public int hp;

	// Token: 0x04000B58 RID: 2904
	public int maxHp;

	// Token: 0x04000B59 RID: 2905
	public int x;

	// Token: 0x04000B5A RID: 2906
	public int y;

	// Token: 0x04000B5B RID: 2907
	public int dir = 1;

	// Token: 0x04000B5C RID: 2908
	public int dirV = 1;

	// Token: 0x04000B5D RID: 2909
	public int status;

	// Token: 0x04000B5E RID: 2910
	public int p1;

	// Token: 0x04000B5F RID: 2911
	public int p2;

	// Token: 0x04000B60 RID: 2912
	public int p3;

	// Token: 0x04000B61 RID: 2913
	public int xFirst;

	// Token: 0x04000B62 RID: 2914
	public int yFirst;

	// Token: 0x04000B63 RID: 2915
	public int vy;

	// Token: 0x04000B64 RID: 2916
	public int exp;

	// Token: 0x04000B65 RID: 2917
	public int w;

	// Token: 0x04000B66 RID: 2918
	public int h;

	// Token: 0x04000B67 RID: 2919
	public int hpInjure;

	// Token: 0x04000B68 RID: 2920
	public int charIndex;

	// Token: 0x04000B69 RID: 2921
	public int timeStatus;

	// Token: 0x04000B6A RID: 2922
	public int mobId;

	// Token: 0x04000B6B RID: 2923
	public bool isx;

	// Token: 0x04000B6C RID: 2924
	public bool isy;

	// Token: 0x04000B6D RID: 2925
	public bool isDisable;

	// Token: 0x04000B6E RID: 2926
	public bool isDontMove;

	// Token: 0x04000B6F RID: 2927
	public bool isFire;

	// Token: 0x04000B70 RID: 2928
	public bool isIce;

	// Token: 0x04000B71 RID: 2929
	public bool isWind;

	// Token: 0x04000B72 RID: 2930
	public bool isDie;

	// Token: 0x04000B73 RID: 2931
	public MyVector vMobMove = new MyVector();

	// Token: 0x04000B74 RID: 2932
	public bool isGo;

	// Token: 0x04000B75 RID: 2933
	public string mobName;

	// Token: 0x04000B76 RID: 2934
	public int templateId;

	// Token: 0x04000B77 RID: 2935
	public short pointx;

	// Token: 0x04000B78 RID: 2936
	public short pointy;

	// Token: 0x04000B79 RID: 2937
	public global::Char cFocus;

	// Token: 0x04000B7A RID: 2938
	public int dame;

	// Token: 0x04000B7B RID: 2939
	public int dameMp;

	// Token: 0x04000B7C RID: 2940
	public int sys;

	// Token: 0x04000B7D RID: 2941
	public sbyte levelBoss;

	// Token: 0x04000B7E RID: 2942
	public sbyte level;

	// Token: 0x04000B7F RID: 2943
	public bool isBoss;

	// Token: 0x04000B80 RID: 2944
	public bool isMobMe;

	// Token: 0x04000B81 RID: 2945
	public static MyVector lastMob = new MyVector();

	// Token: 0x04000B82 RID: 2946
	public static MyVector newMob = new MyVector();

	// Token: 0x04000B83 RID: 2947
	public bool isMafuba;

	// Token: 0x04000B84 RID: 2948
	public int xMFB;

	// Token: 0x04000B85 RID: 2949
	public int yMFB;

	// Token: 0x04000B86 RID: 2950
	public int xSd;

	// Token: 0x04000B87 RID: 2951
	public int ySd;

	// Token: 0x04000B88 RID: 2952
	internal bool isOutMap;

	// Token: 0x04000B89 RID: 2953
	internal int wCount;

	// Token: 0x04000B8A RID: 2954
	public bool isShadown = true;

	// Token: 0x04000B8B RID: 2955
	internal int tick;

	// Token: 0x04000B8C RID: 2956
	internal int frame;

	// Token: 0x04000B8D RID: 2957
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000B8E RID: 2958
	internal bool wy;

	// Token: 0x04000B8F RID: 2959
	internal int wt;

	// Token: 0x04000B90 RID: 2960
	internal int fy;

	// Token: 0x04000B91 RID: 2961
	internal int ty;

	// Token: 0x04000B92 RID: 2962
	public int typeSuperEff;

	// Token: 0x04000B93 RID: 2963
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04000B94 RID: 2964
	public int[] stand = new int[]
	{
		0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
		1, 1
	};

	// Token: 0x04000B95 RID: 2965
	public int[] move = new int[]
	{
		1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
		3, 3, 2, 2, 2
	};

	// Token: 0x04000B96 RID: 2966
	public int[] moveFast = new int[] { 1, 1, 2, 2, 3, 3, 2 };

	// Token: 0x04000B97 RID: 2967
	public int[] attack1 = new int[] { 4, 5, 6 };

	// Token: 0x04000B98 RID: 2968
	public int[] attack2 = new int[] { 7, 8, 9 };

	// Token: 0x04000B99 RID: 2969
	public int[] hurt = new int[1];

	// Token: 0x04000B9A RID: 2970
	internal int color = 8421504;

	// Token: 0x04000B9B RID: 2971
	public int len = 24;

	// Token: 0x04000B9C RID: 2972
	public int w_hp_bar = 24;

	// Token: 0x04000B9D RID: 2973
	public int per = 100;

	// Token: 0x04000B9E RID: 2974
	public int per_tem = 100;

	// Token: 0x04000B9F RID: 2975
	public byte h_hp_bar = 4;

	// Token: 0x04000BA0 RID: 2976
	public Image imgHPtem;

	// Token: 0x04000BA1 RID: 2977
	internal int offset;

	// Token: 0x04000BA2 RID: 2978
	public bool isHide;

	// Token: 0x04000BA3 RID: 2979
	internal sbyte[] cou = new sbyte[] { -1, 1 };

	// Token: 0x04000BA4 RID: 2980
	public global::Char injureBy;

	// Token: 0x04000BA5 RID: 2981
	public bool injureThenDie;

	// Token: 0x04000BA6 RID: 2982
	public Mob mobToAttack;

	// Token: 0x04000BA7 RID: 2983
	public int forceWait;

	// Token: 0x04000BA8 RID: 2984
	public bool blindEff;

	// Token: 0x04000BA9 RID: 2985
	public bool sleepEff;

	// Token: 0x04000BAA RID: 2986
	internal int[][] frameArr = new int[][]
	{
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 0, 0, 0, 0, 1, 1, 1, 1 }
	};

	// Token: 0x04000BAB RID: 2987
	internal bool isGetFr = true;
}
