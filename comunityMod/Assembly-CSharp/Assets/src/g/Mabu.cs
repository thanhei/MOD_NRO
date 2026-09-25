using System;

namespace Assets.src.g
{
	// Token: 0x020001B7 RID: 439
	internal class Mabu : global::Char
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x000C544A File Offset: 0x000C364A
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000C5468 File Offset: 0x000C3668
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == global::Char.myCharz().charID)
			{
				this.focus = global::Char.myCharz();
				return;
			}
			this.focus = GameScr.findCharInMap(id);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000C54C4 File Offset: 0x000C36C4
		public new void checkFrameTick(int[] array)
		{
			if (this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					EffecMn.addEff(new Effect(19, this.cx, this.cy + 20, 2, 1, -1));
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if (this.skillID == 1 && this.tick == array.Length - 1)
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

		// Token: 0x060012CB RID: 4811 RVA: 0x000C557C File Offset: 0x000C377C
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				102.ToString(),
				"/data"
			});
			try
			{
				Mabu.data1.readData2(text);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102.ToString() + "/img.png");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000C5620 File Offset: 0x000C3820
		public void setSkill(sbyte id, short x, short y, global::Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo > this.cx) ? 1 : (-1));
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000C5678 File Offset: 0x000C3878
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				103.ToString(),
				"/data"
			});
			try
			{
				Mabu.data2.readData2(text);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000C5728 File Offset: 0x000C3928
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
					this.effEat.trans = ((this.effEat.x > this.focus.cx) ? 1 : 0);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if (this.skillID != -1)
			{
				if (this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? (-30) : 30);
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
							this.charAttack[i].doInjure(this.damageAttack[i], 0, false, false);
						}
					}
				}
				if (this.skillID != 3)
				{
					return;
				}
				this.xTo = this.charAttack[this.pIndex].cx;
				this.yTo = this.charAttack[this.pIndex].cy;
				this.cx += (this.xTo - this.cx) / 3;
				this.cy += (this.yTo - this.cy) / 3;
				if (GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(19, this.cx, this.cy, 2, 1, -1));
				}
				if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
				{
					this.cx = this.xTo;
					this.cy = this.yTo;
					this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0, false, false);
					this.pIndex++;
					if (this.pIndex == this.charAttack.Length)
					{
						this.skillID = -1;
						this.pIndex = 0;
						return;
					}
				}
			}
			else
			{
				base.update();
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000C5AA4 File Offset: 0x000C3CA4
		public override void paint(mGraphics g)
		{
			if (this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if (this.skillID == 0 || this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
				return;
			}
			base.paint(g);
		}

		// Token: 0x040019E0 RID: 6624
		public static EffectData data1;

		// Token: 0x040019E1 RID: 6625
		public static EffectData data2;

		// Token: 0x040019E2 RID: 6626
		internal new int tick;

		// Token: 0x040019E3 RID: 6627
		internal int lastDir;

		// Token: 0x040019E4 RID: 6628
		internal bool addFoot;

		// Token: 0x040019E5 RID: 6629
		internal Effect effEat;

		// Token: 0x040019E6 RID: 6630
		internal new global::Char focus;

		// Token: 0x040019E7 RID: 6631
		public int xTo;

		// Token: 0x040019E8 RID: 6632
		public int yTo;

		// Token: 0x040019E9 RID: 6633
		public bool haftBody;

		// Token: 0x040019EA RID: 6634
		public bool change;

		// Token: 0x040019EB RID: 6635
		internal global::Char[] charAttack;

		// Token: 0x040019EC RID: 6636
		internal int[] damageAttack;

		// Token: 0x040019ED RID: 6637
		internal int dx;

		// Token: 0x040019EE RID: 6638
		public static int[] skill1 = new int[]
		{
			0, 0, 1, 1, 2, 2, 3, 3, 4, 4,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5
		};

		// Token: 0x040019EF RID: 6639
		public static int[] skill2 = new int[]
		{
			0, 0, 6, 6, 7, 7, 8, 8, 9, 9,
			9, 9, 9, 10, 10
		};

		// Token: 0x040019F0 RID: 6640
		public static int[] skill3 = new int[]
		{
			0, 0, 1, 1, 2, 2, 3, 3, 4, 4,
			5, 5, 6, 6, 7, 7, 8, 8, 9, 9,
			10, 10, 11, 11, 12, 12
		};

		// Token: 0x040019F1 RID: 6641
		public static int[] skill4 = new int[] { 13, 13, 14, 14, 15, 15, 16, 16 };

		// Token: 0x040019F2 RID: 6642
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x040019F3 RID: 6643
		public sbyte skillID = -1;

		// Token: 0x040019F4 RID: 6644
		internal int frame;

		// Token: 0x040019F5 RID: 6645
		internal int pIndex;
	}
}
