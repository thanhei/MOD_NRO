using System;

// Token: 0x0200005B RID: 91
public class Effect_End
{
	// Token: 0x0600030A RID: 778 RVA: 0x0001D7AC File Offset: 0x0001B9AC
	public Effect_End(int type, int typeSub, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.x = x;
		this.y = y;
		this.levelPaint = levelPaint;
		this.dir = dir;
		this.dir_nguoc = ((dir != -1) ? 0 : 2);
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		if (listObj != null)
		{
			this.listObj = new Point[listObj.Length];
			for (int i = 0; i < this.listObj.Length; i++)
			{
				this.listObj[i] = listObj[i];
			}
		}
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
	public Effect_End(int type, int typeSub, int typePaint, global::Char charUse, Point target, int levelPaint, short timeRemove, short range)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.typePaint = typePaint;
		this.charUse = charUse;
		if (charUse.containsCaiTrang(1265))
		{
			if (this.typeEffect == 21 || this.typeEffect == 22 || this.typeEffect == 23)
			{
				this.charUse.cx += 10 * this.charUse.cdir;
			}
			else if (this.typeEffect == 18 || this.typeEffect == 19 || this.typeEffect == 20)
			{
				this.charUse.cx += -15 * this.charUse.cdir;
			}
			else
			{
				this.charUse.cx += 15 * this.charUse.cdir;
			}
		}
		this.x = this.charUse.cx;
		this.y = this.charUse.cy;
		this.dir = this.charUse.cdir;
		this.dir_nguoc = ((this.dir != -1) ? 0 : 2);
		this.target = target;
		this.levelPaint = levelPaint;
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.range = (int)range;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0001DB08 File Offset: 0x0001BD08
	public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.typePaint = typePaint;
		this.x = x;
		this.y = y;
		this.levelPaint = levelPaint;
		this.dir = dir;
		this.dir_nguoc = ((dir != -1) ? 0 : 2);
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		if (listObj != null)
		{
			this.listObj = new Point[listObj.Length];
			for (int i = 0; i < this.listObj.Length; i++)
			{
				this.listObj[i] = listObj[i];
			}
		}
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0001DC58 File Offset: 0x0001BE58
	public static Image getImage(int id)
	{
		if (id < 0)
		{
			return null;
		}
		string path = "/e/e_" + id + ".png";
		Image result = null;
		try
		{
			result = mSystem.loadImage(path);
		}
		catch (Exception ex)
		{
		}
		return result;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0001DCAC File Offset: 0x0001BEAC
	public static void setSoundSkill_END(int x, int y, int typeEffect)
	{
		try
		{
			int num = -1;
			int num2 = Res.random(3);
			if (num >= 0)
			{
				SoundMn.playSound(x, y, num, SoundMn.volume);
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR setSoundSkill_END: " + ex.ToString());
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0001DD0C File Offset: 0x0001BF0C
	public void create_Effect()
	{
		try
		{
			Effect_End.setSoundSkill_END(this.x, this.y, this.typeEffect);
			int num = this.typeEffect;
			switch (num)
			{
			case 16:
			case 17:
				this.set_Sub();
				break;
			case 18:
			case 19:
			case 20:
				this.set_Pow();
				break;
			case 21:
			case 22:
			case 23:
				this.set_Gong();
				break;
			case 24:
				this.set_Skill_Kamex10();
				break;
			case 25:
				this.set_Skill_Destroy();
				break;
			case 26:
				this.set_Skill_MaFuba();
				break;
			default:
				switch (num)
				{
				case 0:
				case 1:
				case 2:
					this.set_End_String(this.typeEffect);
					break;
				case 3:
					this.set_FireWork();
					break;
				case 9:
					this.set_LINE_IN();
					break;
				case 10:
				case 11:
					this.set_End_Rock();
					break;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR create_Effect: " + ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0001DE60 File Offset: 0x0001C060
	public void update()
	{
		try
		{
			this.f++;
			int num = this.typeEffect;
			switch (num)
			{
			case 16:
			case 17:
				this.upd_Sub();
				break;
			case 18:
			case 19:
			case 20:
				this.upd_Pow();
				break;
			case 21:
			case 22:
			case 23:
				this.upd_Gong();
				break;
			case 24:
				this.upd_Skill_Kamex10();
				break;
			case 25:
				this.upd_Skill_Destroy();
				break;
			case 26:
				this.upd_Skill_MaFuba();
				break;
			default:
				switch (num)
				{
				case 0:
				case 1:
				case 2:
					this.upd_End_String();
					break;
				case 3:
					this.upd_FireWork();
					break;
				case 9:
					this.upd_LINE_IN();
					break;
				case 10:
				case 11:
					this.upd_End_Rock();
					break;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR update: " + ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001DF98 File Offset: 0x0001C198
	public void paint(mGraphics g)
	{
		try
		{
			if (!this.isRemove && this.f >= 0)
			{
				int num = this.typeEffect;
				switch (num)
				{
				case 16:
					if (this.typeSub == 0)
					{
						this.pnt_Sub(g, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						this.pnt_Sub(g, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					break;
				case 17:
					this.pnt_Sub(g, mGraphics.VCENTER);
					break;
				case 18:
				case 19:
				case 20:
					this.pnt_Pow(g, mGraphics.BOTTOM | mGraphics.HCENTER);
					break;
				case 21:
				case 22:
				case 23:
					this.pnt_Gong(g, mGraphics.VCENTER | mGraphics.HCENTER);
					break;
				case 24:
					this.pnt_Skill_Kamex10(g);
					break;
				case 25:
					this.pnt_Skill_Destroy(g);
					break;
				case 26:
					this.pnt_Skill_MaFuba(g);
					break;
				default:
					switch (num)
					{
					case 0:
					case 1:
					case 2:
						this.pnt_End_String(g);
						break;
					case 3:
						this.pnt_FireWork(g);
						break;
					case 9:
						this.pnt_LINE_IN(g);
						break;
					case 10:
					case 11:
						this.pnt_End_Rock(g);
						break;
					}
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Res.err(ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00005A3B File Offset: 0x00003C3B
	public void removeEff()
	{
		this.isRemove = true;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001E13C File Offset: 0x0001C33C
	public void createDanFocus(bool isRandom, global::Char obj)
	{
		if (isRandom)
		{
			switch (Res.random(4))
			{
			case 0:
				this.gocT_Arc = 90;
				break;
			case 1:
				this.gocT_Arc = 270;
				break;
			case 2:
				this.gocT_Arc = 180;
				break;
			case 3:
				this.gocT_Arc = 0;
				break;
			}
		}
		else if (obj.cdir == 1)
		{
			this.gocT_Arc = 0;
		}
		else
		{
			this.gocT_Arc = 180;
		}
		this.va = (int)((short)(256 * this.vMax));
		this.vx = 0;
		this.vy = 0;
		this.life = 0;
		this.vx1000 = this.va * Res.cos(this.gocT_Arc) >> 10;
		this.vy1000 = this.va * Res.sin(this.gocT_Arc) >> 10;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0001E230 File Offset: 0x0001C430
	public void updateAngleXP(int fmove)
	{
		if (this.f < fmove)
		{
			return;
		}
		if (this.charUse == null || this.target == null || this.f >= this.fRemove)
		{
			this.f = this.fRemove;
			return;
		}
		int num = this.target.x - this.charUse.cx;
		int num2 = this.target.y - this.charUse.cy;
		this.life++;
		if ((Res.abs(num) < 10 && Res.abs(num2) < 10) || this.life > this.fRemove)
		{
			this.f = this.fRemove;
			return;
		}
		int num3 = Res.angle(num, num2);
		if (Res.abs(num3 - this.gocT_Arc) < 90 || num * num + num2 * num2 > 4096)
		{
			if (Res.abs(num3 - this.gocT_Arc) < 15)
			{
				this.gocT_Arc = num3;
			}
			else if ((num3 - this.gocT_Arc >= 0 && num3 - this.gocT_Arc < 180) || num3 - this.gocT_Arc < -180)
			{
				this.gocT_Arc = Res.fixangle(this.gocT_Arc + 15);
			}
			else
			{
				this.gocT_Arc = Res.fixangle(this.gocT_Arc - 15);
			}
		}
		if (this.f > this.fRemove * 2 / 3 && this.va < 8192)
		{
			this.va += 3096;
		}
		this.vx1000 = this.va * Res.cos(this.gocT_Arc) >> 10;
		this.vy1000 = this.va * Res.sin(this.gocT_Arc) >> 10;
		num += this.vx1000;
		int num4 = num >> 10;
		this.x += num4;
		num &= 1023;
		num2 += this.vy1000;
		int num5 = num2 >> 10;
		this.y += num5;
		num2 &= 1023;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001E458 File Offset: 0x0001C658
	public int setFrameAngle(int goc)
	{
		int result;
		if (goc <= 15 || goc > 345)
		{
			result = 12;
		}
		else
		{
			int num = (goc - 15) / 15 + 1;
			if (num > 24)
			{
				num = 24;
			}
			result = (int)this.mpaintone_Arrow[num];
		}
		return result;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
	public void create_Arrow(int vMax, Point targetPoint)
	{
		this.vMax = vMax;
		int num;
		int num2;
		if (targetPoint != null)
		{
			num = targetPoint.x - this.x;
			num2 = targetPoint.y - this.y;
			this.toX = targetPoint.x;
			this.toY = targetPoint.y;
		}
		else
		{
			num = this.toX - this.x;
			num2 = this.toY - this.y;
		}
		if (this.x > this.toX)
		{
			this.dir = 2;
			this.dir_nguoc = 0;
		}
		else
		{
			this.dir = 0;
			this.dir_nguoc = 2;
		}
		int frameAngle = Res.angle(num, num2);
		this.frame = this.setFrameAngle(frameAngle);
		this.fSpeed = this.frame;
		this.create_Speed(num, num2);
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001E570 File Offset: 0x0001C770
	public void create_Speed(int dx, int dy)
	{
		int num = Res.getDistance(dx, dy) / this.vMax;
		if (num == 0)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		if (num2 == 0 && dx < num)
		{
			num2 = ((dx >= 0) ? 1 : -1);
		}
		if (num3 == 0 && dy < num)
		{
			num3 = ((dy >= 0) ? 1 : -1);
		}
		if (Res.abs(num2) > Res.abs(dx))
		{
			num2 = dx;
		}
		if (Res.abs(num3) > Res.abs(dy))
		{
			num3 = dy;
		}
		this.vx = num2;
		this.vy = num3;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001E60C File Offset: 0x0001C80C
	public void moveTo_xy(int toX, int toY, int fMove, int typeEff_End, int rangeEnd)
	{
		if (this.f < fMove)
		{
			this.frame = this.setFrameAngle((this.dir != -1) ? 0 : 180);
			return;
		}
		this.frame = this.fSpeed;
		if (Res.abs(this.x - toX) < Res.abs(this.vx))
		{
			this.x = toX;
			this.vx = 0;
		}
		else
		{
			this.x += this.vx;
		}
		if (Res.abs(this.y - toY) < Res.abs(this.vy))
		{
			this.y = toY;
			this.vy = 0;
		}
		else
		{
			this.y += this.vy;
		}
		if (Res.abs(this.x - toX) < Res.abs(this.vMax) && Res.abs(this.y - toY) < Res.abs(this.vMax) && typeEff_End >= 0)
		{
			if (this.target != null)
			{
				int num = this.target.x;
				int num2 = this.target.y;
				if (rangeEnd > 0)
				{
					num += Res.random_Am(0, rangeEnd);
					num2 += Res.random_Am(0, rangeEnd);
				}
				GameScr.addEffectEnd(typeEff_End, 0, 0, num, num2, 1, 0, -1, null);
				this.removeEff();
			}
			else if (this.isAddSub)
			{
				this.isAddSub = false;
				int num3 = this.x;
				int num4 = this.y;
				if (rangeEnd > 1)
				{
					num3 += Res.random_Am_0(rangeEnd);
					num4 += Res.random_Am_0(rangeEnd);
				}
				GameScr.addEffectEnd(typeEff_End, 0, 0, num3, num4, 1, 0, -1, null);
			}
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0001E7C4 File Offset: 0x0001C9C4
	public void paint_Arrow(mGraphics g, FrameImage frm, int index, int x, int y, int anchor, bool isCountFr)
	{
		if (frm == null)
		{
			return;
		}
		int num = frm.nFrame / 3;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = 3;
		int num3;
		if (frm.nFrame > 6)
		{
			num = 1;
			if (this.f / num2 - this.fMove > 8)
			{
				num3 = 6;
			}
			else if (this.f / num2 - this.fMove > 4)
			{
				num3 = 3;
			}
			else
			{
				num3 = 0;
			}
		}
		else if (frm.nFrame > 3)
		{
			num3 = ((this.f / num2 % 2 != 0) ? 3 : 0);
		}
		else
		{
			num3 = this.f % num;
		}
		int idx = num * (int)this.mImageArrow[index] + num3;
		if (frm.nFrame < 3)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		if (isCountFr)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		frm.drawFrame(idx, x, y, (int)this.mXoayArrow[index], anchor, g);
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
	private void set_End_String(int typeEffect)
	{
		if (typeEffect != 0)
		{
			if (typeEffect != 1)
			{
				if (typeEffect == 2)
				{
					this.fraImgEff = new FrameImage(6);
				}
			}
			else
			{
				this.fraImgEff = new FrameImage(5);
			}
		}
		else
		{
			this.fraImgEff = new FrameImage(4);
		}
		this.fRemove = 100;
		this.dy_throw = GameCanvas.h / 3 + 10;
		this.vy = 10;
		this.y1000 = 0;
		this.isAddSub = false;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001E94C File Offset: 0x0001CB4C
	private void upd_End_String()
	{
		this.x = GameCanvas.hw;
		this.y = this.y1000;
		if (this.f > this.fRemove)
		{
			this.removeEff();
		}
		this.vy++;
		if (this.vy > 15)
		{
			this.vy = 15;
		}
		if (this.y1000 + this.vy < this.dy_throw)
		{
			this.y1000 += this.vy;
		}
		else
		{
			this.y1000 = this.dy_throw;
			if (!this.isAddSub)
			{
				this.isAddSub = true;
				if (this.typeSub != -1)
				{
					GameScr.addEffectEnd(this.typeSub, 0, 0, this.x, this.y, this.levelPaint, 0, -1, null);
				}
			}
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00005A44 File Offset: 0x00003C44
	private void pnt_End_String(mGraphics g)
	{
		if (this.fraImgEff != null)
		{
			this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001EA28 File Offset: 0x0001CC28
	private void set_FireWork()
	{
		int num = Res.random(3, 5);
		this.fRemove = 90;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = this.x + Res.random_Am_0(4);
			point.y = this.y + Res.random_Am_0(5);
			if (this.typeSub == 0)
			{
				point.fRe = Res.random(10);
				int num2 = 1;
				if (i % 2 == 0)
				{
					num2 = -1;
				}
				point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num2;
				point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				point.fraImgEff = new FrameImage(7);
			}
			this.VecEffEnd.addElement(point);
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001EB00 File Offset: 0x0001CD00
	private void upd_FireWork()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			point.update();
			if (point.f == point.fRe)
			{
				SoundMn.playSound(point.x, point.y, SoundMn.FIREWORK, SoundMn.volume);
			}
			if (point.f - point.fRe > point.fraImgEff.nFrame * 3 - 1)
			{
				point.f = 0;
				if (this.typeSub == 0)
				{
					point.fRe = Res.random(10);
					int num = 1;
					if (i % 2 == 0)
					{
						num = -1;
					}
					point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num;
					point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				}
			}
		}
		if (this.f >= this.fRemove)
		{
			this.removeEff();
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0001EC0C File Offset: 0x0001CE0C
	private void pnt_FireWork(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			if (point.f - point.fRe > -1 && point.fraImgEff != null)
			{
				point.fraImgEff.drawFrame((point.f - point.fRe) / 3 % point.fraImgEff.nFrame, point.x, point.y, 0, 3, g);
			}
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0001EC9C File Offset: 0x0001CE9C
	private void set_Skill_Kamex10()
	{
		this.w = this.fra_skill[0].frameWidth;
		this.h = this.fra_skill[0].frameHeight;
		this.vMax = Res.abs(this.x - this.target.x);
		this.nFrame = new byte[]
		{
			0,
			0,
			0,
			1,
			1,
			1
		};
		this.isAddSub = false;
		SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_1, SoundMn.volume);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0001ED28 File Offset: 0x0001CF28
	private void upd_Skill_Kamex10()
	{
		this.fSpeed++;
		this.w += 20;
		if (this.w > this.vMax)
		{
			this.w = this.vMax;
		}
		this.x = this.charUse.cx + 10;
		this.y = this.charUse.cy - 3;
		if (this.dir == -1)
		{
			this.x = this.charUse.cx - this.w - 10;
		}
		if (!this.isAddSub && GameCanvas.timeNow - this.time >= (long)this.timeRemove)
		{
			this.f = 0;
			this.nFrame = new byte[]
			{
				2,
				2,
				2,
				3,
				3,
				3
			};
			this.isAddSub = true;
		}
		if (this.f > this.nFrame.Length - 1)
		{
			if (this.isAddSub)
			{
				this.removeEff();
			}
			else
			{
				this.f = 0;
			}
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0001EE38 File Offset: 0x0001D038
	private void pnt_Skill_Kamex10(mGraphics g)
	{
		if (this.fra_skill == null)
		{
			return;
		}
		g.setClip(this.x, this.y - this.h / 2, this.w, this.h);
		this.Fill_Rect_Img(g, this.fra_skill[0], this.fra_skill[1], this.fra_skill[2], (int)this.nFrame[this.f], this.x, this.y, this.vMax);
		GameCanvas.resetTransGameScr(g);
		if (this.dir == -1 && this.fra_skill[0] != null)
		{
			this.fra_skill[0].drawFrame((int)this.nFrame[this.f], this.x + this.w - this.fra_skill[0].frameWidth, this.y - this.fra_skill[0].frameHeight / 2 - 1, 2, 0, g);
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001EF28 File Offset: 0x0001D128
	private void set_Skill_Destroy()
	{
		this.x = this.charUse.cx + 20 * this.charUse.cdir;
		int num = 15;
		this.fMove = (int)this.timeRemove / num;
		if (this.target != null)
		{
			for (int i = 0; i < num; i++)
			{
				Point point = new Point();
				point.fraImgEff = this.fra_skill[0];
				point.fraImgEff_2 = this.fra_skill[2];
				point.x = this.x;
				point.y = this.y;
				if (this.target != null)
				{
					point.toX = this.target.x;
					point.toY = this.target.y;
					if (this.range > 0)
					{
						point.toX += Res.random_Am(0, this.range);
						point.toY += Res.random_Am(0, this.range);
					}
				}
				this.vMax = Res.random(9, 12);
				if (i == num - 1)
				{
					point.fraImgEff = this.fra_skill[1];
					point.fraImgEff_2 = this.fra_skill[3];
					point.toX = this.target.x;
					point.toY = this.target.y;
					this.vMax = 9;
				}
				point.isPaint = false;
				point.isChange = false;
				point.isRemove = false;
				point.create_Arrow(this.vMax);
				this.VecEffEnd.addElement(point);
			}
		}
		else
		{
			this.removeEff();
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001F0C0 File Offset: 0x0001D2C0
	private void upd_Skill_Destroy()
	{
		int num = 0;
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			if (!point.isPaint && GameCanvas.timeNow - this.time >= (long)(i * this.fMove))
			{
				point.isPaint = true;
				GameScr.addEffectEnd(17, 0, this.typePaint, this.charUse.cx, this.charUse.cy - 3, 2, this.dir_nguoc, -1, null);
				if (i == this.VecEffEnd.size() - 1)
				{
					SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_1, SoundMn.volume);
				}
				else
				{
					SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_0, SoundMn.volume);
				}
			}
			if (point.isPaint && !point.isRemove)
			{
				point.f++;
				if (!point.isChange)
				{
					if (point.f < 10 && i == this.VecEffEnd.size() - 1 && this.charUse != null && !TileMap.tileTypeAt(this.charUse.cx - (this.charUse.chw + 1) * this.charUse.cdir, this.charUse.cy, (this.charUse.cdir != 1) ? 4 : 8))
					{
						this.charUse.cx -= this.charUse.cdir;
					}
					point.moveTo_xy(point.toX, point.toY);
					if (point.x == point.toX)
					{
						point.isChange = true;
						point.f = 0;
					}
				}
				if (point.isChange && point.f >= this.n_frame * point.fraImgEff_2.nFrame)
				{
					point.isRemove = true;
				}
			}
			if (point.isRemove)
			{
				num++;
			}
		}
		if (num == this.VecEffEnd.size())
		{
			this.removeEff();
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
	private void pnt_Skill_Destroy(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			if (point.isPaint && !point.isRemove)
			{
				if (!point.isChange)
				{
					point.paint_Arrow(g, point.fraImgEff, mGraphics.VCENTER | mGraphics.HCENTER, false);
				}
				if (point.isChange)
				{
					point.fraImgEff_2.drawFrame(point.f / this.n_frame % point.fraImgEff_2.nFrame, point.x, point.y, this.dir_nguoc, mGraphics.VCENTER | mGraphics.HCENTER, g);
				}
			}
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001F3BC File Offset: 0x0001D5BC
	private void set_Skill_MaFuba()
	{
		this.nFrame = new byte[]
		{
			0,
			0,
			0,
			1,
			1,
			1,
			2,
			2,
			2
		};
		this.isAddSub = false;
		this.fMove = 10;
		this.x1000 = this.x;
		this.y1000 = this.y + 12;
		this.dy = 25;
		this.dy_throw = 19;
		if (this.typeSub == 1)
		{
			this.dy_throw = 21;
		}
		else if (this.typeSub == 2)
		{
			this.dy_throw = 31;
		}
		this.h = this.fra_skill[1].frameHeight + 50 - this.dy_throw;
		this.vy = 1;
		this.vy1000 = 1;
		this.y = this.y1000 - this.h;
		this.rS = 90;
		this.vMax = 1;
		this.angleS = (this.angleO = 25);
		this.iDotS = 1;
		if (this.listObj != null && this.listObj.Length > 0)
		{
			this.iDotS = this.listObj.Length;
		}
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null);
		SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_0, SoundMn.volume);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0001F564 File Offset: 0x0001D764
	private void changeAngleStar()
	{
		if (this.vMax < 40)
		{
			this.vMax += 2;
		}
		this.angleS = this.angleO;
		this.angleS -= this.vMax;
		if (this.angleS >= 360)
		{
			this.angleS -= 360;
		}
		if (this.angleS < 0)
		{
			this.angleS = 360 + this.angleS;
		}
		this.angleO = this.angleS;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
	private void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			if (this.angleS >= 360)
			{
				this.angleS -= 360;
			}
			if (this.angleS < 0)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			if (this.angleS < 90)
			{
				this.xDotS[i] = this.x + this.xArgS[i];
				this.yDotS[i] = this.y - this.yArgS[i];
			}
			else if (this.angleS >= 90 && this.angleS < 180)
			{
				this.xDotS[i] = this.x - this.xArgS[i];
				this.yDotS[i] = this.y - this.yArgS[i];
			}
			else if (this.angleS >= 180 && this.angleS < 270)
			{
				this.xDotS[i] = this.x - this.xArgS[i];
				this.yDotS[i] = this.y + this.yArgS[i];
			}
			else
			{
				this.xDotS[i] = this.x + this.xArgS[i];
				this.yDotS[i] = this.y + this.yArgS[i];
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001F7CC File Offset: 0x0001D9CC
	private void upd_Skill_MaFuba()
	{
		if (this.stt == 0)
		{
			if (this.f == 3)
			{
				SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
			}
			this.frame++;
			if (this.frame > this.nFrame.Length - 1)
			{
				this.frame = this.nFrame.Length - 1;
			}
			if (this.f == this.fMove + 4)
			{
				GameScr.addEffectEnd(16, 1, this.typePaint, this.x, this.y, 3, 0, 2945, null);
			}
			if (this.f > this.fMove + 4)
			{
				this.rS--;
				if (this.rS < 0)
				{
					this.rS = 0;
					this.f = 0;
					this.fSpeed = 0;
					this.nFrame_2 = new byte[]
					{
						1,
						1,
						0,
						0,
						0,
						0,
						1,
						1,
						1,
						1,
						0,
						0,
						0,
						1,
						1,
						1,
						0,
						0,
						1,
						1,
						1,
						2
					};
					this.hideListObj_Mafuba(true);
					this.stt = 1;
				}
				else
				{
					this.changeAngleStar();
					this.setDotStar();
					this.updListObj_Mafuba(true);
				}
			}
		}
		else if (this.stt == 1)
		{
			this.fSpeed++;
			if (this.fSpeed > this.nFrame_2.Length - 1)
			{
				this.fSpeed = this.nFrame_2.Length - 1;
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.vy1000++;
				}
				this.vy += this.vy1000;
				if (this.vy >= this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw)
				{
					this.vy = this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw;
					this.f = 0;
					this.fSpeed = 0;
					this.stt = 2;
					this.nFrame_2 = new byte[]
					{
						3,
						3,
						3,
						3,
						3,
						4,
						4,
						4,
						5,
						5,
						5
					};
				}
			}
		}
		else if (this.stt == 2)
		{
			this.fSpeed++;
			if (this.fSpeed > this.nFrame_2.Length - 1)
			{
				this.stt = 3;
				this.frame = 0;
				this.nFrame = new byte[]
				{
					2,
					2,
					1,
					1,
					0,
					0,
					3,
					3,
					3,
					0,
					0,
					0,
					4,
					4,
					4,
					0,
					0
				};
			}
		}
		else if (this.stt == 3)
		{
			this.frame++;
			if (this.frame == 3)
			{
				SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
			}
			if (this.frame > this.nFrame.Length - 1)
			{
				this.frame = 0;
				this.stt = 4;
				this.nFrame = new byte[]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					3,
					3,
					3,
					0,
					0,
					0,
					4,
					4,
					4,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					3,
					3,
					0,
					0,
					4,
					4
				};
			}
		}
		else
		{
			this.frame++;
			if (this.frame > this.nFrame.Length - 1)
			{
				this.frame = 0;
			}
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null);
				this.updListObj_Mafuba(false);
				this.removeEff();
			}
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001FB34 File Offset: 0x0001DD34
	private void pnt_Skill_MaFuba(mGraphics g)
	{
		if (this.fra_skill == null)
		{
			return;
		}
		if (this.nFrame != null)
		{
			this.fra_skill[0].drawFrame((int)this.nFrame[this.frame], this.x1000, this.y1000, 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
		if (this.stt == 1 || this.stt == 2)
		{
			int anchor = mGraphics.BOTTOM | mGraphics.HCENTER;
			int num = this.dy;
			if (this.nFrame_2[this.fSpeed] == 0 || this.nFrame_2[this.fSpeed] == 1)
			{
				anchor = (mGraphics.VCENTER | mGraphics.HCENTER);
				num = 0;
			}
			this.fra_skill[1].drawFrame((int)this.nFrame_2[this.fSpeed], this.x, this.y + num + this.vy, 0, anchor, g);
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001FC1C File Offset: 0x0001DE1C
	private void Fill_Rect_Img(mGraphics g, FrameImage head, FrameImage body, FrameImage foot, int frame, int x, int y, int w)
	{
		int num = w;
		bool flag = false;
		if (head != null && foot != null)
		{
			flag = true;
			num = w - (head.frameWidth + foot.frameWidth);
		}
		if (num > 0)
		{
			int num2 = num / body.frameWidth;
			if (num % body.frameWidth > 0)
			{
				num2++;
			}
			if (this.dir == -1)
			{
				for (int i = 0; i < num2; i++)
				{
					int num3;
					if (i == num2 - 1)
					{
						if (flag)
						{
							num3 = x + foot.frameWidth;
						}
						else
						{
							num3 = x + w - body.frameWidth;
						}
					}
					else if (flag)
					{
						num3 = x + foot.frameWidth + body.frameWidth + i * body.frameWidth;
					}
					else
					{
						num3 = x + i * body.frameWidth;
					}
					body.drawFrame(frame, num3, y - body.frameHeight / 2, 2, 0, g);
				}
			}
			else
			{
				for (int j = 0; j < num2; j++)
				{
					int num4;
					if (j == num2 - 1)
					{
						if (flag)
						{
							num4 = x + w - (body.frameWidth + foot.frameWidth);
						}
						else
						{
							num4 = x + w - body.frameWidth;
						}
					}
					else if (flag)
					{
						num4 = x + j * body.frameWidth + head.frameWidth;
					}
					else
					{
						num4 = x + j * body.frameWidth;
					}
					body.drawFrame(frame, num4, y - body.frameHeight / 2, 0, 0, g);
				}
			}
		}
		if (this.dir == -1)
		{
			if (head != null)
			{
				head.drawFrame(frame, x + w - head.frameWidth, y - head.frameHeight / 2, 2, 0, g);
			}
			if (foot != null)
			{
				foot.drawFrame(frame, x, y - foot.frameHeight / 2, 2, 0, g);
			}
		}
		else
		{
			if (head != null)
			{
				head.drawFrame(frame, x, y - head.frameHeight / 2, 0, 0, g);
			}
			if (foot != null)
			{
				foot.drawFrame(frame, x + w - foot.frameWidth - 1, y - foot.frameHeight / 2, 0, 0, g);
			}
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001FE5C File Offset: 0x0001E05C
	private void set_LINE_IN()
	{
		this.indexColorStar = this.typeSub;
		this.x1000 = this.x * 1000;
		this.y1000 = this.y * 1000;
		this.fRemove = Res.random(4, 6);
		this.vMax = 5;
		this.xline = 10;
		this.yline = 20;
		this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0001FED8 File Offset: 0x0001E0D8
	private void upd_LINE_IN()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Line line = (Line)this.VecEffEnd.elementAt(i);
			line.update();
			if (this.f >= this.fRemove)
			{
				this.VecEffEnd.removeElement(line);
				i--;
			}
		}
		if (this.f >= this.fRemove)
		{
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.VecEffEnd.removeAllElements();
				this.removeEff();
			}
			else
			{
				this.fRemove = Res.random(4, 6);
				this.f = 0;
				this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
			}
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
	private void create_Star_Line_In(int vline, int minline, int maxline, int numpoint)
	{
		if (this.f == -1)
		{
			this.VecEffEnd.removeAllElements();
		}
		int num = 4;
		this.colorpaint = new int[num];
		if (maxline <= minline)
		{
			maxline = minline + 1;
		}
		for (int i = 0; i < num; i++)
		{
			if (Res.random(2) == 0)
			{
				this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][Res.random(3)];
			}
			else
			{
				this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][2];
			}
		}
		for (int j = 0; j < num; j++)
		{
			Line line = new Line();
			int num2 = 5 + 180 / num * j;
			int num3 = 180 / num + 180 / num * j - 5;
			if (num3 <= num2)
			{
				num3 = num2 + 1;
			}
			int num4 = Res.random(minline, maxline);
			int num5 = Res.random(vline, vline + 3);
			int num6 = Res.random(num2, num3);
			int num7 = Res.random(13, 23);
			bool is2Line = Res.random(4) == 0;
			num6 = Res.fixangle(num6 % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
			if (numpoint > 0)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
			line = new Line();
			num6 += 180 + Res.random_Am(2, 5);
			num6 = Res.fixangle(num6 % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
			if (numpoint > 0)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00020204 File Offset: 0x0001E404
	private void pnt_LINE_IN(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Line line = (Line)this.VecEffEnd.elementAt(i);
			if (line != null)
			{
				int color = 0;
				if (i / 2 < this.colorpaint.Length)
				{
					color = this.colorpaint[i / 2];
				}
				g.setColor(color);
				g.drawLine(line.x0 / 1000, line.y0 / 1000, line.x1 / 1000, line.y1 / 1000);
				if (line.is2Line)
				{
					g.drawLine(line.x0 / 1000 + 1, line.y0 / 1000, line.x1 / 1000 + 1, line.y1 / 1000);
				}
			}
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000202EC File Offset: 0x0001E4EC
	private void set_End_Rock()
	{
		this.fraImgEff = new FrameImage(8);
		this.fRemove = Res.random(23, 27);
		int num = Res.random(1, 3);
		this.toY = this.y - 40;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = this.x + Res.random_Am(0, 20);
			point.y = this.y + Res.random_Am_0(7);
			if (this.typeEffect == 10)
			{
				point.frame = Res.random(0, this.fraImgEff.nFrame - 2);
			}
			else if (this.typeEffect == 11)
			{
				point.frame = Res.random(2, this.fraImgEff.nFrame);
			}
			else
			{
				point.frame = Res.random(0, this.fraImgEff.nFrame);
			}
			point.dis = Res.random(2);
			point.vy = -Res.random(1, 4);
			this.VecEffEnd.addElement(point);
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x000203FC File Offset: 0x0001E5FC
	private void upd_End_Rock()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			point.update();
			if (point.y < this.toY)
			{
				this.VecEffEnd.removeElementAt(i);
				i--;
			}
		}
		if (this.f >= this.fRemove)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00020478 File Offset: 0x0001E678
	private void pnt_End_Rock(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			if (this.fraImgEff != null)
			{
				this.fraImgEff.drawFrame(point.frame, point.x, point.y, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
			}
		}
	}

	// Token: 0x06000333 RID: 819 RVA: 0x000204E8 File Offset: 0x0001E6E8
	private void updListObj_Mafuba(bool ismafuba)
	{
		if (this.listObj == null)
		{
			return;
		}
		for (int i = 0; i < this.listObj.Length; i++)
		{
			if (this.listObj[i] != null)
			{
				if ((int)this.listObj[i].type == 0)
				{
					Mob mob = GameScr.findMobInMap(this.listObj[i].id);
					if (mob != null)
					{
						mob.isMafuba = ismafuba;
						mob.isHide = false;
						mob.xMFB = this.xDotS[i];
						mob.yMFB = this.yDotS[i];
					}
				}
				else
				{
					global::Char @char;
					if (global::Char.myCharz().charID == this.listObj[i].id)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(this.listObj[i].id);
					}
					if (@char != null)
					{
						@char.isMafuba = ismafuba;
						@char.isHide = false;
						@char.xMFB = this.xDotS[i];
						@char.yMFB = this.yDotS[i];
					}
				}
			}
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000205F0 File Offset: 0x0001E7F0
	private void hideListObj_Mafuba(bool ishide)
	{
		if (this.listObj == null)
		{
			return;
		}
		for (int i = 0; i < this.listObj.Length; i++)
		{
			if (this.listObj[i] != null)
			{
				if ((int)this.listObj[i].type == 0)
				{
					Mob mob = GameScr.findMobInMap(this.listObj[i].id);
					if (mob != null)
					{
						mob.isHide = ishide;
					}
				}
				else
				{
					global::Char @char;
					if (global::Char.myCharz().charID == this.listObj[i].id)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(this.listObj[i].id);
					}
					if (@char != null)
					{
						@char.isHide = ishide;
					}
				}
			}
		}
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000206B0 File Offset: 0x0001E8B0
	private void get_Img_Skill()
	{
		int num = 0;
		int[] array = null;
		int[] array2 = null;
		switch (this.typeEffect)
		{
		case 16:
			num = 26;
			if (this.typeSub == 0)
			{
				array = new int[]
				{
					7
				};
				array2 = new int[]
				{
					28
				};
			}
			if (this.typeSub == 1)
			{
				array = new int[]
				{
					2
				};
				array2 = new int[]
				{
					23
				};
			}
			break;
		case 17:
			num = 25;
			array = new int[]
			{
				2
			};
			array2 = new int[]
			{
				16
			};
			break;
		case 18:
			num = 24;
			array = new int[1];
			array2 = new int[]
			{
				9
			};
			break;
		case 19:
			num = 25;
			array = new int[1];
			array2 = new int[]
			{
				14
			};
			break;
		case 20:
			num = 26;
			array = new int[1];
			array2 = new int[]
			{
				21
			};
			break;
		case 21:
			num = 24;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				10
			};
			break;
		case 22:
			num = 25;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				15
			};
			break;
		case 23:
			num = 26;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				22
			};
			break;
		case 24:
			num = 24;
			array = new int[]
			{
				2,
				3,
				4
			};
			array2 = new int[]
			{
				11,
				12,
				13
			};
			break;
		case 25:
			num = 25;
			array = new int[]
			{
				3,
				4,
				5,
				6
			};
			array2 = new int[]
			{
				17,
				18,
				19,
				20
			};
			break;
		case 26:
		{
			num = 26;
			int num2 = 0;
			int num3 = 0;
			if (this.typeSub == 0)
			{
				num2 = 4;
				num3 = 25;
			}
			else if (this.typeSub == 1)
			{
				num2 = 5;
				num3 = 26;
			}
			else if (this.typeSub == 2)
			{
				num2 = 6;
				num3 = 27;
			}
			array = new int[]
			{
				num2,
				3
			};
			array2 = new int[]
			{
				num3,
				24
			};
			break;
		}
		}
		if (array != null && array2 != null)
		{
			this.fra_skill = new FrameImage[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string nameImg = string.Concat(new object[]
				{
					"Skills_",
					num,
					"_",
					this.typePaint,
					"_",
					array[i]
				});
				FrameImage frameImage = mSystem.getFraImage(nameImg);
				if (frameImage == null)
				{
					frameImage = new FrameImage(array2[i]);
				}
				if (frameImage != null)
				{
					this.fra_skill[i] = frameImage;
				}
			}
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00020990 File Offset: 0x0001EB90
	private void set_Gong()
	{
		if (this.charUse != null)
		{
			if (this.typeEffect == 21)
			{
				this.x = this.charUse.cx - 3 * this.charUse.cdir;
				this.y = this.charUse.cy;
				SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_0, SoundMn.volume);
			}
			else if (this.typeEffect == 22)
			{
				this.x = this.charUse.cx + 20 * this.charUse.cdir;
				this.y = this.charUse.cy - 4;
				SoundMn.playSound(this.x, this.y, SoundMn.DESTROY_2, SoundMn.volume);
			}
			else if (this.typeEffect == 23)
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy - 50;
				SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_2, SoundMn.volume);
			}
			else
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy;
			}
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00020ADC File Offset: 0x0001ECDC
	private void upd_Gong()
	{
		if (this.charUse != null)
		{
			if (this.typeEffect == 21)
			{
				this.x = this.charUse.cx - 3 * this.charUse.cdir;
				this.y = this.charUse.cy;
			}
			else if (this.typeEffect == 22)
			{
				this.x = this.charUse.cx + 20 * this.charUse.cdir;
				this.y = this.charUse.cy - 4;
			}
			else if (this.typeEffect == 23)
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy - 50;
			}
			else
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy;
			}
		}
		if (this.timeRemove > 0)
		{
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.removeEff();
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00020C28 File Offset: 0x0001EE28
	private void pnt_Gong(mGraphics g, int anchor)
	{
		if (this.fra_skill[0] != null)
		{
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00020C80 File Offset: 0x0001EE80
	private void set_Pow()
	{
		this.nFrame = null;
		this.n_frame = 3;
		if (this.typeEffect == 18)
		{
			if (this.typeSub == 0)
			{
				this.nFrame = new byte[]
				{
					0,
					0,
					0,
					1,
					1,
					1,
					2,
					2,
					2
				};
			}
			else
			{
				this.nFrame = new byte[]
				{
					3,
					3,
					3,
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
			}
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00020CE8 File Offset: 0x0001EEE8
	private void upd_Pow()
	{
		if (this.charUse != null)
		{
			this.x = this.charUse.cx;
			this.y = this.charUse.cy + 13;
		}
		if (this.timeRemove > 0)
		{
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.removeEff();
			}
		}
		else if (this.nFrame != null)
		{
			if (this.f > this.nFrame.Length)
			{
				this.removeEff();
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00020DA4 File Offset: 0x0001EFA4
	private void pnt_Pow(mGraphics g, int anchor)
	{
		if (this.fra_skill[0] != null)
		{
			if (this.nFrame != null)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.f % this.nFrame.Length], this.x, this.y, this.dir_nguoc, anchor, g);
			}
			else
			{
				this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
			}
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00005A80 File Offset: 0x00003C80
	private void set_Sub()
	{
		if (this.typeEffect == 17)
		{
			this.x += ((this.dir != 0) ? (-this.fra_skill[0].frameWidth) : 0);
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00020E44 File Offset: 0x0001F044
	private void upd_Sub()
	{
		if (this.timeRemove > 0)
		{
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.removeEff();
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00005ABB File Offset: 0x00003CBB
	private void pnt_Sub(mGraphics g, int anchor)
	{
		this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir, anchor, g);
	}

	// Token: 0x0600033F RID: 831 RVA: 0x000045ED File Offset: 0x000027ED
	private void set_()
	{
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000045ED File Offset: 0x000027ED
	private void upd_()
	{
	}

	// Token: 0x06000341 RID: 833 RVA: 0x000045ED File Offset: 0x000027ED
	private void pnt_(mGraphics g)
	{
	}

	// Token: 0x04000516 RID: 1302
	public const sbyte Lvlpaint_All = -1;

	// Token: 0x04000517 RID: 1303
	public const sbyte Lvlpaint_Front = 0;

	// Token: 0x04000518 RID: 1304
	public const sbyte Lvlpaint_Mid = 1;

	// Token: 0x04000519 RID: 1305
	public const sbyte Lvlpaint_Mid_2 = 2;

	// Token: 0x0400051A RID: 1306
	public const sbyte Lvlpaint_Behind = 3;

	// Token: 0x0400051B RID: 1307
	public const short End_String_Lose = 0;

	// Token: 0x0400051C RID: 1308
	public const short End_String_Win = 1;

	// Token: 0x0400051D RID: 1309
	public const short End_String_Draw = 2;

	// Token: 0x0400051E RID: 1310
	public const short End_FireWork = 3;

	// Token: 0x0400051F RID: 1311
	public const short End_line_in = 9;

	// Token: 0x04000520 RID: 1312
	public const short End_e8_rock = 10;

	// Token: 0x04000521 RID: 1313
	public const short End_e8_ice = 11;

	// Token: 0x04000522 RID: 1314
	public const short End_SUB_MaFuBa = 16;

	// Token: 0x04000523 RID: 1315
	public const short End_SUB_Destroy = 17;

	// Token: 0x04000524 RID: 1316
	public const short End_POW_Kamex10 = 18;

	// Token: 0x04000525 RID: 1317
	public const short End_POW_Destroy = 19;

	// Token: 0x04000526 RID: 1318
	public const short End_POW_MaFuBa = 20;

	// Token: 0x04000527 RID: 1319
	public const short End_GONG_Kamex10 = 21;

	// Token: 0x04000528 RID: 1320
	public const short End_GONG_Destroy = 22;

	// Token: 0x04000529 RID: 1321
	public const short End_GONG_MaFuBa = 23;

	// Token: 0x0400052A RID: 1322
	public const short End_Skill_Kamex10 = 24;

	// Token: 0x0400052B RID: 1323
	public const short End_Skill_Destroy = 25;

	// Token: 0x0400052C RID: 1324
	public const short End_Skill_MaFuBa = 26;

	// Token: 0x0400052D RID: 1325
	private MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

	// Token: 0x0400052E RID: 1326
	public FrameImage fraImgEff;

	// Token: 0x0400052F RID: 1327
	public byte[] nFrame = new byte[10];

	// Token: 0x04000530 RID: 1328
	public byte[] nFrame_2 = new byte[10];

	// Token: 0x04000531 RID: 1329
	public int typePaint;

	// Token: 0x04000532 RID: 1330
	public int typeEffect;

	// Token: 0x04000533 RID: 1331
	public int typeSub;

	// Token: 0x04000534 RID: 1332
	public int range;

	// Token: 0x04000535 RID: 1333
	public short idEndeff;

	// Token: 0x04000536 RID: 1334
	public int fRemove;

	// Token: 0x04000537 RID: 1335
	public int fMove;

	// Token: 0x04000538 RID: 1336
	public int n_frame;

	// Token: 0x04000539 RID: 1337
	public int x;

	// Token: 0x0400053A RID: 1338
	public int y;

	// Token: 0x0400053B RID: 1339
	public int w;

	// Token: 0x0400053C RID: 1340
	public int h;

	// Token: 0x0400053D RID: 1341
	public int dir;

	// Token: 0x0400053E RID: 1342
	public int dir_nguoc;

	// Token: 0x0400053F RID: 1343
	public int levelPaint;

	// Token: 0x04000540 RID: 1344
	public int f;

	// Token: 0x04000541 RID: 1345
	public int frame;

	// Token: 0x04000542 RID: 1346
	public int fSpeed;

	// Token: 0x04000543 RID: 1347
	public int vx;

	// Token: 0x04000544 RID: 1348
	public int vy;

	// Token: 0x04000545 RID: 1349
	public int x1000;

	// Token: 0x04000546 RID: 1350
	public int y1000;

	// Token: 0x04000547 RID: 1351
	public int vx1000;

	// Token: 0x04000548 RID: 1352
	public int vy1000;

	// Token: 0x04000549 RID: 1353
	public int dy_throw;

	// Token: 0x0400054A RID: 1354
	public int vMax;

	// Token: 0x0400054B RID: 1355
	public int toX;

	// Token: 0x0400054C RID: 1356
	public int toY;

	// Token: 0x0400054D RID: 1357
	public int stt;

	// Token: 0x0400054E RID: 1358
	public int dx;

	// Token: 0x0400054F RID: 1359
	public int dy;

	// Token: 0x04000550 RID: 1360
	public short timeRemove;

	// Token: 0x04000551 RID: 1361
	public long time;

	// Token: 0x04000552 RID: 1362
	public bool isRemove;

	// Token: 0x04000553 RID: 1363
	public bool isAddSub;

	// Token: 0x04000554 RID: 1364
	public global::Char charUse;

	// Token: 0x04000555 RID: 1365
	public Point[] listObj;

	// Token: 0x04000556 RID: 1366
	public Point target;

	// Token: 0x04000557 RID: 1367
	public static short[][] arrInfoEff = new short[][]
	{
		new short[]
		{
			68,
			264,
			4
		},
		new short[]
		{
			30,
			120,
			4
		},
		new short[]
		{
			66,
			280,
			4
		},
		new short[]
		{
			0,
			0,
			1
		},
		new short[]
		{
			111,
			68,
			2
		},
		new short[]
		{
			90,
			68,
			2
		},
		new short[]
		{
			125,
			68,
			2
		},
		new short[]
		{
			47,
			282,
			6
		},
		new short[]
		{
			10,
			40,
			4
		},
		new short[]
		{
			92,
			525,
			7
		},
		new short[]
		{
			62,
			372,
			6
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			72,
			240,
			3
		},
		new short[]
		{
			20,
			42,
			3
		},
		new short[]
		{
			65,
			160,
			4
		},
		new short[]
		{
			50,
			300,
			6
		},
		new short[]
		{
			84,
			168,
			2
		},
		new short[]
		{
			90,
			540,
			6
		},
		new short[]
		{
			180,
			900,
			6
		},
		new short[]
		{
			62,
			186,
			3
		},
		new short[]
		{
			34,
			80,
			4
		},
		new short[]
		{
			140,
			560,
			4
		},
		new short[]
		{
			64,
			600,
			6
		},
		new short[]
		{
			36,
			200,
			5
		},
		new short[]
		{
			35,
			200,
			5
		},
		new short[]
		{
			50,
			250,
			5
		},
		new short[]
		{
			50,
			240,
			6
		}
	};

	// Token: 0x04000558 RID: 1368
	public int life;

	// Token: 0x04000559 RID: 1369
	public int goc_Arc;

	// Token: 0x0400055A RID: 1370
	public int va;

	// Token: 0x0400055B RID: 1371
	public int gocT_Arc;

	// Token: 0x0400055C RID: 1372
	public byte[] mpaintone_Arrow = new byte[]
	{
		12,
		11,
		10,
		9,
		8,
		7,
		6,
		5,
		4,
		3,
		2,
		1,
		0,
		23,
		22,
		21,
		20,
		19,
		18,
		17,
		16,
		15,
		14,
		13
	};

	// Token: 0x0400055D RID: 1373
	public byte[] mImageArrow = new byte[]
	{
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2
	};

	// Token: 0x0400055E RID: 1374
	public byte[] mXoayArrow = new byte[]
	{
		2,
		2,
		3,
		3,
		3,
		4,
		5,
		5,
		5,
		5,
		5,
		1,
		0,
		0,
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		6,
		6,
		2
	};

	// Token: 0x0400055F RID: 1375
	private int rS;

	// Token: 0x04000560 RID: 1376
	private int angleS;

	// Token: 0x04000561 RID: 1377
	private int angleO;

	// Token: 0x04000562 RID: 1378
	private int iAngleS;

	// Token: 0x04000563 RID: 1379
	private int iDotS;

	// Token: 0x04000564 RID: 1380
	private int[] xArgS;

	// Token: 0x04000565 RID: 1381
	private int[] yArgS;

	// Token: 0x04000566 RID: 1382
	private int[] xDotS;

	// Token: 0x04000567 RID: 1383
	private int[] yDotS;

	// Token: 0x04000568 RID: 1384
	public static int[][] colorStar = new int[][]
	{
		new int[]
		{
			16310304,
			16298056,
			16777215
		},
		new int[]
		{
			7045120,
			12643960,
			16777215
		},
		new int[]
		{
			2407423,
			11987199,
			16777215
		}
	};

	// Token: 0x04000569 RID: 1385
	private int[] colorpaint;

	// Token: 0x0400056A RID: 1386
	private int indexColorStar;

	// Token: 0x0400056B RID: 1387
	private int xline;

	// Token: 0x0400056C RID: 1388
	private int yline;

	// Token: 0x0400056D RID: 1389
	private FrameImage[] fra_skill;
}
