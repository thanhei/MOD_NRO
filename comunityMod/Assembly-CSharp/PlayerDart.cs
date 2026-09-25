using System;

// Token: 0x02000088 RID: 136
public class PlayerDart
{
	// Token: 0x06000739 RID: 1849 RVA: 0x000737DC File Offset: 0x000719DC
	public PlayerDart(global::Char charBelong, int dartType, SkillPaint sp, int x, int y)
	{
		this.skillPaint = sp;
		this.charBelong = charBelong;
		this.info = GameScr.darts[dartType];
		this.va = this.info.va;
		this.x = x;
		this.y = y;
		IMapObject mapObject2;
		if (charBelong.mobFocus == null)
		{
			IMapObject mapObject = charBelong.charFocus;
			mapObject2 = mapObject;
		}
		else
		{
			IMapObject mapObject = charBelong.mobFocus;
			mapObject2 = mapObject;
		}
		IMapObject mapObject3 = mapObject2;
		this.setAngle(Res.angle(mapObject3.getX() - x, mapObject3.getY() - y));
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00073875 File Offset: 0x00071A75
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000738AC File Offset: 0x00071AAC
	public void update()
	{
		if (!this.isActive)
		{
			return;
		}
		if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
		{
			this.endMe();
			return;
		}
		IMapObject mapObject2;
		if (this.charBelong.mobFocus == null)
		{
			IMapObject mapObject = this.charBelong.charFocus;
			mapObject2 = mapObject;
		}
		else
		{
			IMapObject mapObject = this.charBelong.mobFocus;
			mapObject2 = mapObject;
		}
		IMapObject mapObject3 = mapObject2;
		for (int i = 0; i < (int)this.info.nUpdate; i++)
		{
			if (this.info.tail.Length != 0)
			{
				this.darts.addElement(new SmallDart(this.x, this.y));
			}
			int num = ((this.charBelong.getX() <= mapObject3.getX()) ? (-10) : 10);
			this.dx = mapObject3.getX() + num - this.x;
			this.dy = mapObject3.getY() - mapObject3.getH() / 2 - this.y;
			this.life++;
			if (Res.abs(this.dx) < 20 && Res.abs(this.dy) < 20)
			{
				if (this.charBelong.charFocus != null && this.charBelong.charFocus.me)
				{
					this.charBelong.charFocus.doInjure(this.charBelong.charFocus.damHP, 0, this.charBelong.charFocus.isCrit, this.charBelong.charFocus.isMob);
				}
				this.endMe();
				return;
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

	// Token: 0x0600073C RID: 1852 RVA: 0x00073C18 File Offset: 0x00071E18
	internal void endMe()
	{
		if (!this.charBelong.isUseSkillAfterCharge && this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
		{
			SoundMn.gI().explode_1();
		}
		this.charBelong.setAttack();
		if (this.charBelong.me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
		if (this.charBelong.isUseSkillAfterCharge)
		{
			this.charBelong.isUseSkillAfterCharge = false;
			if (this.charBelong.isLockMove && this.charBelong.me && this.charBelong.statusMe != 14 && this.charBelong.statusMe != 5)
			{
				this.charBelong.isLockMove = false;
			}
			if (this.charBelong.cgender == 2)
			{
				int num = ((!this.charBelong.me) ? this.charBelong.skillTemplateId : ((int)global::Char.myCharz().myskill.skillId));
				if (num < 77 || num > 83)
				{
					GameScr.gI().activeSuperPower(this.x, this.y);
				}
			}
			else
			{
				GameScr.gI().activeSuperPower(this.x, this.y);
			}
		}
		this.charBelong.dart = null;
		this.charBelong.isCreateDark = false;
		this.charBelong.skillPaint = null;
		this.charBelong.skillPaintRandomPaint = null;
		this.charBelong.stopUseChargeSkill();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00073D8C File Offset: 0x00071F8C
	public void paint(mGraphics g)
	{
		if (!this.isActive)
		{
			return;
		}
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
		g.setColor(16711680);
	}

	// Token: 0x04000DC4 RID: 3524
	public global::Char charBelong;

	// Token: 0x04000DC5 RID: 3525
	public DartInfo info;

	// Token: 0x04000DC6 RID: 3526
	public MyVector darts = new MyVector();

	// Token: 0x04000DC7 RID: 3527
	public int angle;

	// Token: 0x04000DC8 RID: 3528
	public int vx;

	// Token: 0x04000DC9 RID: 3529
	public int vy;

	// Token: 0x04000DCA RID: 3530
	public int va;

	// Token: 0x04000DCB RID: 3531
	public int x;

	// Token: 0x04000DCC RID: 3532
	public int y;

	// Token: 0x04000DCD RID: 3533
	public int z;

	// Token: 0x04000DCE RID: 3534
	internal int life;

	// Token: 0x04000DCF RID: 3535
	internal int dx;

	// Token: 0x04000DD0 RID: 3536
	internal int dy;

	// Token: 0x04000DD1 RID: 3537
	public bool isActive = true;

	// Token: 0x04000DD2 RID: 3538
	public bool isSpeedUp;

	// Token: 0x04000DD3 RID: 3539
	public SkillPaint skillPaint;
}
