using System;

namespace Assets.src.g
{
	// Token: 0x020000B6 RID: 182
	internal class Mabu : global::Char
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00007D2E File Offset: 0x00005F2E
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000749AC File Offset: 0x00072BAC
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == global::Char.myCharz().charID)
			{
				this.focus = global::Char.myCharz();
			}
			else
			{
				this.focus = GameScr.findCharInMap(id);
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00074A10 File Offset: 0x00072C10
		public new void checkFrameTick(int[] array)
		{
			if ((int)this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					Effect me = new Effect(19, this.cx, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(me);
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if ((int)this.skillID == 1 && this.tick == array.Length - 1)
			{
				this.skillID = 3;
				this.cy -= 15;
				return;
			}
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00074ADC File Offset: 0x00072CDC
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				102,
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102 + "/img.png");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00074B80 File Offset: 0x00072D80
		public void setSkill(sbyte id, short x, short y, global::Char[] charHit, long[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo <= this.cx) ? -1 : 1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00074BDC File Offset: 0x00072DDC
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				103,
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00074C8C File Offset: 0x00072E8C
		public override void update()
		{
			if (this.focus != null)
			{
				if (this.effEat.t >= 30)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x <= this.focus.cx) ? 0 : 1);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if ((int)this.skillID != -1)
			{
				if ((int)this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					if ((this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo))
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure(this.damageAttack[i], 0L, false, false);
						}
					}
				}
				if ((int)this.skillID == 3)
				{
					this.xTo = this.charAttack[this.pIndex].cx;
					this.yTo = this.charAttack[this.pIndex].cy;
					this.cx += (this.xTo - this.cx) / 3;
					this.cy += (this.yTo - this.cy) / 3;
					if (GameCanvas.gameTick % 5 == 0)
					{
						Effect me = new Effect(19, this.cx, this.cy, 2, 1, -1);
						EffecMn.addEff(me);
					}
					if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
					{
						this.cx = this.xTo;
						this.cy = this.yTo;
						this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0L, false, false);
						this.pIndex++;
						if (this.pIndex == this.charAttack.Length)
						{
							this.skillID = -1;
							this.pIndex = 0;
						}
					}
				}
				return;
			}
			base.update();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00075044 File Offset: 0x00073244
		public override void paint(mGraphics g)
		{
			if ((int)this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if ((int)this.skillID == 0 || (int)this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
			}
			else
			{
				base.paint(g);
			}
		}

		// Token: 0x04000FA0 RID: 4000
		public static EffectData data1;

		// Token: 0x04000FA1 RID: 4001
		public static EffectData data2;

		// Token: 0x04000FA2 RID: 4002
		private new int tick;

		// Token: 0x04000FA3 RID: 4003
		private int lastDir;

		// Token: 0x04000FA4 RID: 4004
		private bool addFoot;

		// Token: 0x04000FA5 RID: 4005
		private Effect effEat;

		// Token: 0x04000FA6 RID: 4006
		private new global::Char focus;

		// Token: 0x04000FA7 RID: 4007
		public int xTo;

		// Token: 0x04000FA8 RID: 4008
		public int yTo;

		// Token: 0x04000FA9 RID: 4009
		public bool haftBody;

		// Token: 0x04000FAA RID: 4010
		public bool change;

		// Token: 0x04000FAB RID: 4011
		private global::Char[] charAttack;

		// Token: 0x04000FAC RID: 4012
		private long[] damageAttack;

		// Token: 0x04000FAD RID: 4013
		private int dx;

		// Token: 0x04000FAE RID: 4014
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x04000FAF RID: 4015
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x04000FB0 RID: 4016
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x04000FB1 RID: 4017
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x04000FB2 RID: 4018
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x04000FB3 RID: 4019
		public sbyte skillID = -1;

		// Token: 0x04000FB4 RID: 4020
		private int frame;

		// Token: 0x04000FB5 RID: 4021
		private int pIndex;
	}
}
