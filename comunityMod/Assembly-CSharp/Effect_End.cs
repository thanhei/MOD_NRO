using System;

// Token: 0x02000036 RID: 54
public class Effect_End
{
	// Token: 0x06000294 RID: 660 RVA: 0x00030814 File Offset: 0x0002EA14
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
		this.dir_nguoc = ((dir == -1) ? 2 : 0);
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

	// Token: 0x06000295 RID: 661 RVA: 0x0003094C File Offset: 0x0002EB4C
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
		this.dir_nguoc = ((this.dir == -1) ? 2 : 0);
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

	// Token: 0x06000296 RID: 662 RVA: 0x00030B44 File Offset: 0x0002ED44
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
		this.dir_nguoc = ((dir == -1) ? 2 : 0);
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

	// Token: 0x06000297 RID: 663 RVA: 0x00030C84 File Offset: 0x0002EE84
	public static Image getImage(int id)
	{
		if (id < 0)
		{
			return null;
		}
		string text = "/e/e_" + id.ToString() + ".png";
		Image image = null;
		try
		{
			image = mSystem.loadImage(text);
		}
		catch (Exception)
		{
		}
		return image;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00030CD0 File Offset: 0x0002EED0
	public static void setSoundSkill_END(int x, int y, int typeEffect)
	{
		try
		{
			int num = -1;
			Res.random(3);
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

	// Token: 0x06000299 RID: 665 RVA: 0x00030D24 File Offset: 0x0002EF24
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

	// Token: 0x0600029A RID: 666 RVA: 0x00030E54 File Offset: 0x0002F054
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

	// Token: 0x0600029B RID: 667 RVA: 0x00030F70 File Offset: 0x0002F170
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

	// Token: 0x0600029C RID: 668 RVA: 0x000310F0 File Offset: 0x0002F2F0
	public void removeEff()
	{
		this.isRemove = true;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x000310FC File Offset: 0x0002F2FC
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

	// Token: 0x0600029E RID: 670 RVA: 0x000311D4 File Offset: 0x0002F3D4
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
		this.x += num >> 10;
		num &= 1023;
		num2 += this.vy1000;
		this.y += num2 >> 10;
		num2 &= 1023;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x000313C4 File Offset: 0x0002F5C4
	public int setFrameAngle(int goc)
	{
		if (goc <= 15 || goc > 345)
		{
			return 12;
		}
		int num = (goc - 15) / 15 + 1;
		if (num > 24)
		{
			num = 24;
		}
		return (int)this.mpaintone_Arrow[num];
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x000313FC File Offset: 0x0002F5FC
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
		this.frame = this.setFrameAngle(Res.angle(num, num2));
		this.fSpeed = this.frame;
		this.create_Speed(num, num2);
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x000314BC File Offset: 0x0002F6BC
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
			num2 = ((dx >= 0) ? 1 : (-1));
		}
		if (num3 == 0 && dy < num)
		{
			num3 = ((dy >= 0) ? 1 : (-1));
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

	// Token: 0x060002A2 RID: 674 RVA: 0x00031538 File Offset: 0x0002F738
	public void moveTo_xy(int toX, int toY, int fMove, int typeEff_End, int rangeEnd)
	{
		if (this.f < fMove)
		{
			this.frame = this.setFrameAngle((this.dir == -1) ? 180 : 0);
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
		if (Res.abs(this.x - toX) >= Res.abs(this.vMax) || Res.abs(this.y - toY) >= Res.abs(this.vMax) || typeEff_End < 0)
		{
			return;
		}
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
			return;
		}
		if (this.isAddSub)
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

	// Token: 0x060002A3 RID: 675 RVA: 0x000316C4 File Offset: 0x0002F8C4
	public void paint_Arrow(mGraphics g, FrameImage frm, int index, int x, int y, int anchor, bool isCountFr)
	{
		if (frm != null)
		{
			int num = frm.nFrame / 3;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = 3;
			int num3;
			if (frm.nFrame <= 6)
			{
				num3 = ((frm.nFrame <= 3) ? (this.f % num) : ((this.f / num2 % 2 != 0) ? 3 : 0));
			}
			else
			{
				num = 1;
				num3 = ((this.f / num2 - this.fMove > 8) ? 6 : ((this.f / num2 - this.fMove > 4) ? 3 : 0));
			}
			int num4 = num * (int)this.mImageArrow[index] + num3;
			if (frm.nFrame < 3)
			{
				num4 = this.f / num2 % frm.nFrame;
			}
			if (isCountFr)
			{
				num4 = this.f / num2 % frm.nFrame;
			}
			frm.drawFrame(num4, x, y, (int)this.mXoayArrow[index], anchor, g);
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x00031798 File Offset: 0x0002F998
	internal void set_End_String(int typeEffect)
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

	// Token: 0x060002A5 RID: 677 RVA: 0x00031808 File Offset: 0x0002FA08
	internal void upd_End_String()
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
			return;
		}
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

	// Token: 0x060002A6 RID: 678 RVA: 0x000318CF File Offset: 0x0002FACF
	internal void pnt_End_String(mGraphics g)
	{
		if (this.fraImgEff != null)
		{
			this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00031908 File Offset: 0x0002FB08
	internal void set_FireWork()
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

	// Token: 0x060002A8 RID: 680 RVA: 0x000319D8 File Offset: 0x0002FBD8
	internal void upd_FireWork()
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

	// Token: 0x060002A9 RID: 681 RVA: 0x00031AD8 File Offset: 0x0002FCD8
	internal void pnt_FireWork(mGraphics g)
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

	// Token: 0x060002AA RID: 682 RVA: 0x00031B5C File Offset: 0x0002FD5C
	internal void set_Skill_Kamex10()
	{
		this.w = this.fra_skill[0].frameWidth;
		this.h = this.fra_skill[0].frameHeight;
		this.vMax = Res.abs(this.x - this.target.x);
		this.nFrame = new byte[] { 0, 0, 0, 1, 1, 1 };
		this.isAddSub = false;
		SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_1, SoundMn.volume);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00031BE8 File Offset: 0x0002FDE8
	internal void upd_Skill_Kamex10()
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
			this.nFrame = new byte[] { 2, 2, 2, 3, 3, 3 };
			this.isAddSub = true;
		}
		if (this.f > this.nFrame.Length - 1)
		{
			if (this.isAddSub)
			{
				this.removeEff();
				return;
			}
			this.f = 0;
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00031CE4 File Offset: 0x0002FEE4
	internal void pnt_Skill_Kamex10(mGraphics g)
	{
		if (this.fra_skill != null)
		{
			g.setClip(this.x, this.y - this.h / 2, this.w, this.h);
			this.Fill_Rect_Img(g, this.fra_skill[0], this.fra_skill[1], this.fra_skill[2], (int)this.nFrame[this.f], this.x, this.y, this.vMax);
			GameCanvas.resetTransGameScr(g);
			if (this.dir == -1 && this.fra_skill[0] != null)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.f], this.x + this.w - this.fra_skill[0].frameWidth, this.y - this.fra_skill[0].frameHeight / 2 - 1, 2, 0, g);
			}
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00031DCC File Offset: 0x0002FFCC
	internal void set_Skill_Destroy()
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
			return;
		}
		this.removeEff();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00031F58 File Offset: 0x00030158
	internal void upd_Skill_Destroy()
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

	// Token: 0x060002AF RID: 687 RVA: 0x00032164 File Offset: 0x00030364
	internal void pnt_Skill_Destroy(mGraphics g)
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

	// Token: 0x060002B0 RID: 688 RVA: 0x0003221C File Offset: 0x0003041C
	internal void set_Skill_MaFuba()
	{
		this.nFrame = new byte[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
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
		if (this.listObj != null && this.listObj.Length != 0)
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

	// Token: 0x060002B1 RID: 689 RVA: 0x000323B4 File Offset: 0x000305B4
	internal void changeAngleStar()
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

	// Token: 0x060002B2 RID: 690 RVA: 0x00032440 File Offset: 0x00030640
	internal void setDotStar()
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

	// Token: 0x060002B3 RID: 691 RVA: 0x000325FC File Offset: 0x000307FC
	internal void upd_Skill_MaFuba()
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
						1, 1, 0, 0, 0, 0, 1, 1, 1, 1,
						0, 0, 0, 1, 1, 1, 0, 0, 1, 1,
						1, 2
					};
					this.hideListObj_Mafuba(true);
					this.stt = 1;
					return;
				}
				this.changeAngleStar();
				this.setDotStar();
				this.updListObj_Mafuba(true);
				return;
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
						3, 3, 3, 3, 3, 4, 4, 4, 5, 5,
						5
					};
					return;
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
					2, 2, 1, 1, 0, 0, 3, 3, 3, 0,
					0, 0, 4, 4, 4, 0, 0
				};
				return;
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
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 3, 3, 3,
					0, 0, 0, 4, 4, 4, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 3, 3, 0, 0, 4,
					4
				};
				return;
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

	// Token: 0x060002B4 RID: 692 RVA: 0x00032934 File Offset: 0x00030B34
	internal void pnt_Skill_MaFuba(mGraphics g)
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
			int num = mGraphics.BOTTOM | mGraphics.HCENTER;
			int num2 = this.dy;
			if (this.nFrame_2[this.fSpeed] == 0 || this.nFrame_2[this.fSpeed] == 1)
			{
				num = mGraphics.VCENTER | mGraphics.HCENTER;
				num2 = 0;
			}
			this.fra_skill[1].drawFrame((int)this.nFrame_2[this.fSpeed], this.x, this.y + num2 + this.vy, 0, num, g);
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00032A0C File Offset: 0x00030C0C
	internal void Fill_Rect_Img(mGraphics g, FrameImage head, FrameImage body, FrameImage foot, int frame, int x, int y, int w)
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
					body.drawFrame(frame, (i != num2 - 1) ? ((!flag) ? (x + i * body.frameWidth) : (x + foot.frameWidth + body.frameWidth + i * body.frameWidth)) : ((!flag) ? (x + w - body.frameWidth) : (x + foot.frameWidth)), y - body.frameHeight / 2, 2, 0, g);
				}
			}
			else
			{
				for (int j = 0; j < num2; j++)
				{
					body.drawFrame(frame, (j != num2 - 1) ? ((!flag) ? (x + j * body.frameWidth) : (x + j * body.frameWidth + head.frameWidth)) : ((!flag) ? (x + w - body.frameWidth) : (x + w - (body.frameWidth + foot.frameWidth))), y - body.frameHeight / 2, 0, 0, g);
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
				return;
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

	// Token: 0x060002B6 RID: 694 RVA: 0x00032BE0 File Offset: 0x00030DE0
	internal void set_LINE_IN()
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

	// Token: 0x060002B7 RID: 695 RVA: 0x00032C5C File Offset: 0x00030E5C
	internal void upd_LINE_IN()
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
				return;
			}
			this.fRemove = Res.random(4, 6);
			this.f = 0;
			this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00032D18 File Offset: 0x00030F18
	internal void create_Star_Line_In(int vline, int minline, int maxline, int numpoint)
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
			bool flag = Res.random(4) == 0;
			num6 = Res.fixangle(num6 % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, flag);
			if (numpoint > 0)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
			line = new Line();
			num6 = Res.fixangle((num6 + (180 + Res.random_Am(2, 5))) % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, flag);
			if (numpoint > 0)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00032F4C File Offset: 0x0003114C
	internal void pnt_LINE_IN(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Line line = (Line)this.VecEffEnd.elementAt(i);
			if (line != null)
			{
				int num = 0;
				if (i / 2 < this.colorpaint.Length)
				{
					num = this.colorpaint[i / 2];
				}
				g.setColor(num);
				g.drawLine(line.x0 / 1000, line.y0 / 1000, line.x1 / 1000, line.y1 / 1000);
				if (line.is2Line)
				{
					g.drawLine(line.x0 / 1000 + 1, line.y0 / 1000, line.x1 / 1000 + 1, line.y1 / 1000);
				}
			}
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00033028 File Offset: 0x00031228
	internal void set_End_Rock()
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

	// Token: 0x060002BB RID: 699 RVA: 0x0003312C File Offset: 0x0003132C
	internal void upd_End_Rock()
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

	// Token: 0x060002BC RID: 700 RVA: 0x00033198 File Offset: 0x00031398
	internal void pnt_End_Rock(mGraphics g)
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

	// Token: 0x060002BD RID: 701 RVA: 0x00033200 File Offset: 0x00031400
	internal void updListObj_Mafuba(bool ismafuba)
	{
		if (this.listObj == null)
		{
			return;
		}
		for (int i = 0; i < this.listObj.Length; i++)
		{
			if (this.listObj[i] != null)
			{
				if (this.listObj[i].type == 0)
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
					global::Char @char = ((global::Char.myCharz().charID != this.listObj[i].id) ? GameScr.findCharInMap(this.listObj[i].id) : global::Char.myCharz());
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

	// Token: 0x060002BE RID: 702 RVA: 0x000332F4 File Offset: 0x000314F4
	internal void hideListObj_Mafuba(bool ishide)
	{
		if (this.listObj == null)
		{
			return;
		}
		for (int i = 0; i < this.listObj.Length; i++)
		{
			if (this.listObj[i] != null)
			{
				if (this.listObj[i].type == 0)
				{
					Mob mob = GameScr.findMobInMap(this.listObj[i].id);
					if (mob != null)
					{
						mob.isHide = ishide;
					}
				}
				else
				{
					global::Char @char = ((global::Char.myCharz().charID != this.listObj[i].id) ? GameScr.findCharInMap(this.listObj[i].id) : global::Char.myCharz());
					if (@char != null)
					{
						@char.isHide = ishide;
					}
				}
			}
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00033398 File Offset: 0x00031598
	internal void get_Img_Skill()
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
				array = new int[] { 7 };
				array2 = new int[] { 28 };
			}
			if (this.typeSub == 1)
			{
				array = new int[] { 2 };
				array2 = new int[] { 23 };
			}
			break;
		case 17:
			num = 25;
			array = new int[] { 2 };
			array2 = new int[] { 16 };
			break;
		case 18:
			num = 24;
			array = new int[1];
			array2 = new int[] { 9 };
			break;
		case 19:
			num = 25;
			array = new int[1];
			array2 = new int[] { 14 };
			break;
		case 20:
			num = 26;
			array = new int[1];
			array2 = new int[] { 21 };
			break;
		case 21:
			num = 24;
			array = new int[] { 1 };
			array2 = new int[] { 10 };
			break;
		case 22:
			num = 25;
			array = new int[] { 1 };
			array2 = new int[] { 15 };
			break;
		case 23:
			num = 26;
			array = new int[] { 1 };
			array2 = new int[] { 22 };
			break;
		case 24:
			num = 24;
			array = new int[] { 2, 3, 4 };
			array2 = new int[] { 11, 12, 13 };
			break;
		case 25:
			num = 25;
			array = new int[] { 3, 4, 5, 6 };
			array2 = new int[] { 17, 18, 19, 20 };
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
			array = new int[] { num2, 3 };
			array2 = new int[] { num3, 24 };
			break;
		}
		}
		if (array == null || array2 == null)
		{
			return;
		}
		this.fra_skill = new FrameImage[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			FrameImage frameImage = mSystem.getFraImage(string.Concat(new string[]
			{
				"Skills_",
				num.ToString(),
				"_",
				this.typePaint.ToString(),
				"_",
				array[i].ToString()
			}));
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

	// Token: 0x060002C0 RID: 704 RVA: 0x0003364C File Offset: 0x0003184C
	internal void set_Gong()
	{
		if (this.charUse != null)
		{
			if (this.typeEffect == 21)
			{
				this.x = this.charUse.cx - 3 * this.charUse.cdir;
				this.y = this.charUse.cy;
				SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_0, SoundMn.volume);
				return;
			}
			if (this.typeEffect == 22)
			{
				this.x = this.charUse.cx + 20 * this.charUse.cdir;
				this.y = this.charUse.cy - 4;
				SoundMn.playSound(this.x, this.y, SoundMn.DESTROY_2, SoundMn.volume);
				return;
			}
			if (this.typeEffect == 23)
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy - 50;
				SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_2, SoundMn.volume);
				return;
			}
			this.x = this.charUse.cx;
			this.y = this.charUse.cy;
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00033780 File Offset: 0x00031980
	internal void upd_Gong()
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
				return;
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x000338B0 File Offset: 0x00031AB0
	internal void pnt_Gong(mGraphics g, int anchor)
	{
		if (this.fra_skill[0] != null)
		{
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00033904 File Offset: 0x00031B04
	internal void set_Pow()
	{
		this.nFrame = null;
		this.n_frame = 3;
		if (this.typeEffect == 18)
		{
			if (this.typeSub == 0)
			{
				this.nFrame = new byte[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
				return;
			}
			this.nFrame = new byte[]
			{
				3, 3, 3, 4, 4, 4, 5, 5, 5, 6,
				6, 6
			};
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00033964 File Offset: 0x00031B64
	internal void upd_Pow()
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
				return;
			}
		}
		else if (this.nFrame != null)
		{
			if (this.f > this.nFrame.Length)
			{
				this.removeEff();
				return;
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00033A04 File Offset: 0x00031C04
	internal void pnt_Pow(mGraphics g, int anchor)
	{
		if (this.fra_skill[0] != null)
		{
			if (this.nFrame != null)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.f % this.nFrame.Length], this.x, this.y, this.dir_nguoc, anchor, g);
				return;
			}
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00033A97 File Offset: 0x00031C97
	internal void set_Sub()
	{
		if (this.typeEffect == 17)
		{
			this.x += ((this.dir != 0) ? (-this.fra_skill[0].frameWidth) : 0);
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00033ACC File Offset: 0x00031CCC
	internal void upd_Sub()
	{
		if (this.timeRemove > 0)
		{
			if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.removeEff();
				return;
			}
		}
		else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
		{
			this.removeEff();
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00033B20 File Offset: 0x00031D20
	internal void pnt_Sub(mGraphics g, int anchor)
	{
		this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir, anchor, g);
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00004887 File Offset: 0x00002A87
	internal void set_()
	{
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00004887 File Offset: 0x00002A87
	internal void upd_()
	{
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00004887 File Offset: 0x00002A87
	internal void pnt_(mGraphics g)
	{
	}

	// Token: 0x04000581 RID: 1409
	public const sbyte Lvlpaint_All = -1;

	// Token: 0x04000582 RID: 1410
	public const sbyte Lvlpaint_Front = 0;

	// Token: 0x04000583 RID: 1411
	public const sbyte Lvlpaint_Mid = 1;

	// Token: 0x04000584 RID: 1412
	public const sbyte Lvlpaint_Mid_2 = 2;

	// Token: 0x04000585 RID: 1413
	public const sbyte Lvlpaint_Behind = 3;

	// Token: 0x04000586 RID: 1414
	public const short End_String_Lose = 0;

	// Token: 0x04000587 RID: 1415
	public const short End_String_Win = 1;

	// Token: 0x04000588 RID: 1416
	public const short End_String_Draw = 2;

	// Token: 0x04000589 RID: 1417
	public const short End_FireWork = 3;

	// Token: 0x0400058A RID: 1418
	public const short End_line_in = 9;

	// Token: 0x0400058B RID: 1419
	public const short End_e8_rock = 10;

	// Token: 0x0400058C RID: 1420
	public const short End_e8_ice = 11;

	// Token: 0x0400058D RID: 1421
	public const short End_SUB_MaFuBa = 16;

	// Token: 0x0400058E RID: 1422
	public const short End_SUB_Destroy = 17;

	// Token: 0x0400058F RID: 1423
	public const short End_POW_Kamex10 = 18;

	// Token: 0x04000590 RID: 1424
	public const short End_POW_Destroy = 19;

	// Token: 0x04000591 RID: 1425
	public const short End_POW_MaFuBa = 20;

	// Token: 0x04000592 RID: 1426
	public const short End_GONG_Kamex10 = 21;

	// Token: 0x04000593 RID: 1427
	public const short End_GONG_Destroy = 22;

	// Token: 0x04000594 RID: 1428
	public const short End_GONG_MaFuBa = 23;

	// Token: 0x04000595 RID: 1429
	public const short End_Skill_Kamex10 = 24;

	// Token: 0x04000596 RID: 1430
	public const short End_Skill_Destroy = 25;

	// Token: 0x04000597 RID: 1431
	public const short End_Skill_MaFuBa = 26;

	// Token: 0x04000598 RID: 1432
	internal MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

	// Token: 0x04000599 RID: 1433
	public FrameImage fraImgEff;

	// Token: 0x0400059A RID: 1434
	public byte[] nFrame = new byte[10];

	// Token: 0x0400059B RID: 1435
	public byte[] nFrame_2 = new byte[10];

	// Token: 0x0400059C RID: 1436
	public int typePaint;

	// Token: 0x0400059D RID: 1437
	public int typeEffect;

	// Token: 0x0400059E RID: 1438
	public int typeSub;

	// Token: 0x0400059F RID: 1439
	public int range;

	// Token: 0x040005A0 RID: 1440
	public short idEndeff;

	// Token: 0x040005A1 RID: 1441
	public int fRemove;

	// Token: 0x040005A2 RID: 1442
	public int fMove;

	// Token: 0x040005A3 RID: 1443
	public int n_frame;

	// Token: 0x040005A4 RID: 1444
	public int x;

	// Token: 0x040005A5 RID: 1445
	public int y;

	// Token: 0x040005A6 RID: 1446
	public int w;

	// Token: 0x040005A7 RID: 1447
	public int h;

	// Token: 0x040005A8 RID: 1448
	public int dir;

	// Token: 0x040005A9 RID: 1449
	public int dir_nguoc;

	// Token: 0x040005AA RID: 1450
	public int levelPaint;

	// Token: 0x040005AB RID: 1451
	public int f;

	// Token: 0x040005AC RID: 1452
	public int frame;

	// Token: 0x040005AD RID: 1453
	public int fSpeed;

	// Token: 0x040005AE RID: 1454
	public int vx;

	// Token: 0x040005AF RID: 1455
	public int vy;

	// Token: 0x040005B0 RID: 1456
	public int x1000;

	// Token: 0x040005B1 RID: 1457
	public int y1000;

	// Token: 0x040005B2 RID: 1458
	public int vx1000;

	// Token: 0x040005B3 RID: 1459
	public int vy1000;

	// Token: 0x040005B4 RID: 1460
	public int dy_throw;

	// Token: 0x040005B5 RID: 1461
	public int vMax;

	// Token: 0x040005B6 RID: 1462
	public int toX;

	// Token: 0x040005B7 RID: 1463
	public int toY;

	// Token: 0x040005B8 RID: 1464
	public int stt;

	// Token: 0x040005B9 RID: 1465
	public int dx;

	// Token: 0x040005BA RID: 1466
	public int dy;

	// Token: 0x040005BB RID: 1467
	public short timeRemove;

	// Token: 0x040005BC RID: 1468
	public long time;

	// Token: 0x040005BD RID: 1469
	public bool isRemove;

	// Token: 0x040005BE RID: 1470
	public bool isAddSub;

	// Token: 0x040005BF RID: 1471
	public global::Char charUse;

	// Token: 0x040005C0 RID: 1472
	public Point[] listObj;

	// Token: 0x040005C1 RID: 1473
	public Point target;

	// Token: 0x040005C2 RID: 1474
	public static short[][] arrInfoEff = new short[][]
	{
		new short[] { 68, 264, 4 },
		new short[] { 30, 120, 4 },
		new short[] { 66, 280, 4 },
		new short[] { 0, 0, 1 },
		new short[] { 111, 68, 2 },
		new short[] { 90, 68, 2 },
		new short[] { 125, 68, 2 },
		new short[] { 47, 282, 6 },
		new short[] { 10, 40, 4 },
		new short[] { 92, 525, 7 },
		new short[] { 62, 372, 6 },
		new short[] { 80, 352, 4 },
		new short[] { 80, 352, 4 },
		new short[] { 80, 352, 4 },
		new short[] { 72, 240, 3 },
		new short[] { 20, 42, 3 },
		new short[] { 65, 160, 4 },
		new short[] { 50, 300, 6 },
		new short[] { 84, 168, 2 },
		new short[] { 90, 540, 6 },
		new short[] { 180, 900, 6 },
		new short[] { 62, 186, 3 },
		new short[] { 34, 80, 4 },
		new short[] { 140, 560, 4 },
		new short[] { 64, 600, 6 },
		new short[] { 36, 200, 5 },
		new short[] { 35, 200, 5 },
		new short[] { 50, 250, 5 },
		new short[] { 50, 240, 6 }
	};

	// Token: 0x040005C3 RID: 1475
	public int life;

	// Token: 0x040005C4 RID: 1476
	public int goc_Arc;

	// Token: 0x040005C5 RID: 1477
	public int va;

	// Token: 0x040005C6 RID: 1478
	public int gocT_Arc;

	// Token: 0x040005C7 RID: 1479
	public byte[] mpaintone_Arrow = new byte[]
	{
		12, 11, 10, 9, 8, 7, 6, 5, 4, 3,
		2, 1, 0, 23, 22, 21, 20, 19, 18, 17,
		16, 15, 14, 13
	};

	// Token: 0x040005C8 RID: 1480
	public byte[] mImageArrow = new byte[]
	{
		0, 0, 2, 1, 1, 2, 0, 0, 2, 1,
		1, 2, 0, 0, 2, 1, 1, 2, 0, 0,
		2, 1, 1, 2
	};

	// Token: 0x040005C9 RID: 1481
	public byte[] mXoayArrow = new byte[]
	{
		2, 2, 3, 3, 3, 4, 5, 5, 5, 5,
		5, 1, 0, 0, 0, 0, 0, 7, 6, 6,
		6, 6, 6, 2
	};

	// Token: 0x040005CA RID: 1482
	internal int rS;

	// Token: 0x040005CB RID: 1483
	internal int angleS;

	// Token: 0x040005CC RID: 1484
	internal int angleO;

	// Token: 0x040005CD RID: 1485
	internal int iAngleS;

	// Token: 0x040005CE RID: 1486
	internal int iDotS;

	// Token: 0x040005CF RID: 1487
	internal int[] xArgS;

	// Token: 0x040005D0 RID: 1488
	internal int[] yArgS;

	// Token: 0x040005D1 RID: 1489
	internal int[] xDotS;

	// Token: 0x040005D2 RID: 1490
	internal int[] yDotS;

	// Token: 0x040005D3 RID: 1491
	public static int[][] colorStar = new int[][]
	{
		new int[] { 16310304, 16298056, 16777215 },
		new int[] { 7045120, 12643960, 16777215 },
		new int[] { 2407423, 11987199, 16777215 }
	};

	// Token: 0x040005D4 RID: 1492
	internal int[] colorpaint;

	// Token: 0x040005D5 RID: 1493
	internal int indexColorStar;

	// Token: 0x040005D6 RID: 1494
	internal int xline;

	// Token: 0x040005D7 RID: 1495
	internal int yline;

	// Token: 0x040005D8 RID: 1496
	internal FrameImage[] fra_skill;
}
