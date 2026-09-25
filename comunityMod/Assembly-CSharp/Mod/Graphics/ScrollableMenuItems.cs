using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Mod.Graphics
{
	// Token: 0x0200015D RID: 349
	internal class ScrollableMenuItems<T>
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000B568C File Offset: 0x000B388C
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x000B5694 File Offset: 0x000B3894
		internal Action<mGraphics, int, int, int, int, int> PaintItemAction
		{
			get
			{
				return this.paintItemAction;
			}
			set
			{
				this.paintItemAction = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000B569D File Offset: 0x000B389D
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x000B56A5 File Offset: 0x000B38A5
		internal int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000B56AE File Offset: 0x000B38AE
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000B56B6 File Offset: 0x000B38B6
		internal int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000B56BF File Offset: 0x000B38BF
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000B56C7 File Offset: 0x000B38C7
		internal int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000B56D0 File Offset: 0x000B38D0
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x000B56D8 File Offset: 0x000B38D8
		internal int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x000B56E1 File Offset: 0x000B38E1
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x000B56E9 File Offset: 0x000B38E9
		internal int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x000B56F2 File Offset: 0x000B38F2
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x000B56FA File Offset: 0x000B38FA
		internal int CurrentItemIndex
		{
			get
			{
				return this.currentItemIndex;
			}
			set
			{
				this.currentItemIndex = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000B5703 File Offset: 0x000B3903
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x000B570B File Offset: 0x000B390B
		internal int StepScroll
		{
			get
			{
				return this.stepScroll;
			}
			set
			{
				this.stepScroll = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x000B5714 File Offset: 0x000B3914
		internal int CurrentOffset
		{
			get
			{
				return this.currentOffset;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000B571C File Offset: 0x000B391C
		internal List<T> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000B5724 File Offset: 0x000B3924
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x000B572C File Offset: 0x000B392C
		internal bool AllowSelectNone { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x000B5735 File Offset: 0x000B3935
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x000B573D File Offset: 0x000B393D
		internal Action ItemSelected { get; set; }

		// Token: 0x06001081 RID: 4225 RVA: 0x000B5748 File Offset: 0x000B3948
		internal ScrollableMenuItems(List<T> values)
		{
			this.items = values;
			this.currentStepScroll = this.stepScroll;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000B5798 File Offset: 0x000B3998
		internal void Reset()
		{
			if (this.AllowSelectNone || this.items.Count == 0)
			{
				this.currentItemIndex = -1;
			}
			else
			{
				this.currentItemIndex = 0;
			}
			this.currentOffset = (this.currentOffsetTo = 0);
			this.currentStepScroll = this.stepScroll;
			this.isPressKey = false;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000B57F0 File Offset: 0x000B39F0
		internal void Update()
		{
			if (this.currentOffset != this.currentOffsetTo)
			{
				if (this.currentOffset < this.currentOffsetTo)
				{
					if (this.currentOffset + this.currentStepScroll * 2 > this.currentOffsetTo)
					{
						this.currentStepScroll /= 3;
					}
					if (this.currentOffset > this.currentOffsetTo || this.currentStepScroll == 0)
					{
						this.currentOffset = this.currentOffsetTo;
						this.currentStepScroll = this.stepScroll;
						return;
					}
					this.currentOffset += this.currentStepScroll;
					return;
				}
				else if (this.currentOffset > this.currentOffsetTo)
				{
					if (this.currentOffset - this.currentStepScroll * 2 < this.currentOffsetTo)
					{
						this.currentStepScroll /= 3;
					}
					if (this.currentOffset < this.currentOffsetTo || this.currentStepScroll == 0)
					{
						this.currentOffset = this.currentOffsetTo;
						this.currentStepScroll = this.stepScroll;
						return;
					}
					this.currentOffset -= this.currentStepScroll;
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000B58FC File Offset: 0x000B3AFC
		internal void UpdateKey()
		{
			if ((float)this.items.Count > (float)this.height / (float)this.itemHeight)
			{
				if (GameCanvas.pXYScrollMouse != 0)
				{
					if (ScrollableMenuItems<T>.IsPointerIn(this.x, this.y, this.width, this.height))
					{
						this.currentStepScroll = this.stepScroll;
						if (GameCanvas.pXYScrollMouse < 0)
						{
							this.currentOffsetTo += this.stepScroll;
						}
						else if (GameCanvas.pXYScrollMouse > 0)
						{
							this.currentOffsetTo -= this.stepScroll;
						}
						this.isPressKey = false;
					}
				}
				else
				{
					if (GameCanvas.isPointerJustDown && this.lastMouseY == -1 && ScrollableMenuItems<T>.IsPointerIn(this.x, this.y, this.width, this.height))
					{
						this.lastMouseY = GameCanvas.pyMouse;
						this.currentStepScroll = this.stepScroll;
						this.lastOffsetTo = (this.currentOffsetTo = this.currentOffset);
						this.isPressKey = false;
					}
					if (GameCanvas.isPointerMove)
					{
						this.currentOffsetTo = (this.currentOffset = this.lastOffsetTo - (GameCanvas.pyMouse - this.lastMouseY));
						this.isPressKey = false;
					}
				}
				if (this.currentOffsetTo > this.items.Count * this.itemHeight - this.height)
				{
					this.currentOffsetTo = this.items.Count * this.itemHeight - this.height;
				}
				if (this.currentOffsetTo < 0)
				{
					this.currentOffsetTo = 0;
				}
			}
			if (GameCanvas.isPointerHoldIn(this.x, this.y, this.width, this.height) && (!GameCanvas.isPointerMove || !GameCanvas.isPointerJustRelease))
			{
				GameCanvas.isPointerJustDown = false;
				GameScr.gI().isPointerDowning = false;
				if (GameCanvas.isPointerSelect && (this.lastMouseY == GameCanvas.pyMouse || this.lastMouseY == -1))
				{
					this.isPressKey = false;
					int num = (GameCanvas.pyMouse - this.y + this.currentOffset) / this.itemHeight;
					if (num != this.currentItemIndex && num < this.items.Count)
					{
						this.currentItemIndex = num;
					}
					else if (this.AllowSelectNone || this.items.Count == 0)
					{
						this.currentItemIndex = -1;
					}
					if (this.AllowSelectNone || this.currentItemIndex != -1)
					{
						new Thread(delegate
						{
							Thread.Sleep(50);
							Action itemSelected = this.ItemSelected;
							if (itemSelected == null)
							{
								return;
							}
							itemSelected();
						}).Start();
					}
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				this.lastMouseY = -1;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				int num2 = this.currentOffsetTo;
				int num3 = this.currentOffsetTo + this.height / this.itemHeight;
				int num4 = this.itemHeight * (this.currentItemIndex - 2);
				int num5 = this.itemHeight * (this.currentItemIndex - this.height / this.itemHeight + 2);
				bool flag = true;
				if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
					if (this.currentItemIndex == -1)
					{
						this.currentItemIndex = this.items.Count - 1;
						if ((float)this.items.Count > (float)this.height / (float)this.itemHeight)
						{
							this.currentOffsetTo = (this.currentOffset = this.itemHeight * (this.currentItemIndex - 1));
						}
						flag = false;
					}
					else if (this.currentItemIndex - 1 < 0)
					{
						this.currentItemIndex = this.items.Count - 1;
						this.currentOffsetTo = (this.currentOffset = this.itemHeight * (this.currentItemIndex - 1));
						flag = false;
					}
					else
					{
						this.currentItemIndex--;
					}
					if (flag && (float)this.items.Count > (float)this.height / (float)this.itemHeight)
					{
						if (!this.isPressKey)
						{
							if (this.currentOffsetTo < num5)
							{
								this.currentOffsetTo = (this.currentOffset = num5 - this.itemHeight);
							}
							else if (this.currentOffsetTo > num4)
							{
								this.currentOffsetTo = (this.currentOffset = num4);
							}
						}
						else if (num4 < num2)
						{
							this.currentOffsetTo = (this.currentOffset = num4);
						}
					}
				}
				else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
					if (this.currentItemIndex == -1)
					{
						this.currentItemIndex = 0;
						if ((float)this.items.Count > (float)this.height / (float)this.itemHeight)
						{
							this.currentOffsetTo = (this.currentOffset = 0);
						}
						flag = false;
					}
					else if (this.currentItemIndex + 1 >= this.items.Count)
					{
						this.currentItemIndex = 0;
						this.currentOffsetTo = (this.currentOffset = 0);
						flag = false;
					}
					else
					{
						this.currentItemIndex++;
					}
					if (flag && (float)this.items.Count > (float)this.height / (float)this.itemHeight)
					{
						if (!this.isPressKey)
						{
							if (this.currentOffsetTo < num5)
							{
								this.currentOffsetTo = (this.currentOffset = num5);
							}
							else if (this.currentOffsetTo > num4)
							{
								this.currentOffsetTo = (this.currentOffset = num4 + this.itemHeight * 2);
							}
						}
						else if (num5 > num3)
						{
							this.currentOffsetTo = (this.currentOffset = num5);
						}
					}
				}
				if (this.currentOffsetTo > this.items.Count * this.itemHeight - this.height)
				{
					this.currentOffsetTo = (this.currentOffset = this.items.Count * this.itemHeight - this.height);
				}
				if (this.currentOffsetTo < 0)
				{
					this.currentOffsetTo = (this.currentOffset = 0);
				}
				this.isPressKey = true;
			}
			if (GameCanvas.keyPressed[13])
			{
				GameCanvas.keyPressed[13] = false;
				if (this.AllowSelectNone || this.items.Count == 0)
				{
					this.currentItemIndex = -1;
				}
				this.isPressKey = false;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && (this.AllowSelectNone || this.currentItemIndex != -1))
			{
				new Thread(delegate
				{
					Action itemSelected2 = this.ItemSelected;
					if (itemSelected2 == null)
					{
						return;
					}
					itemSelected2();
				}).Start();
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000B5F6C File Offset: 0x000B416C
		internal void Paint(mGraphics g)
		{
			g.setColor(13870191);
			g.fillRect(this.x, this.y, this.width, this.height);
			g.setColor(Color.black);
			g.drawRect(this.x - 1, this.y - 1, this.width + 1, this.height + 1);
			g.setClip(this.x, this.y, this.width, this.height);
			for (int i = Math.Min((this.currentOffset + this.height) / this.itemHeight, this.items.Count - 1); i >= Math.Max(this.currentOffset / this.itemHeight, 0); i--)
			{
				g.setColor(Color.white);
				if (i == this.currentItemIndex)
				{
					g.setColor(16775613);
				}
				g.fillRect(this.x, this.y + i * this.itemHeight - this.currentOffset, this.width, this.itemHeight);
				g.setColor(new Color(0f, 0f, 0f, 0.3f));
				g.fillRect(this.x, this.y + (i + 1) * this.itemHeight - this.currentOffset, this.width, 1);
				Action<mGraphics, int, int, int, int, int> action = this.paintItemAction;
				if (action != null)
				{
					action(g, i, this.x, this.y + i * this.itemHeight - this.currentOffset, this.width, this.itemHeight);
				}
			}
			if (this.currentOffset <= 0)
			{
				g.setColor(new Color(0f, 0f, 0f, 0.3f));
				g.fillRect(this.x, this.y - this.currentOffset, this.width, 1);
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000B283B File Offset: 0x000B0A3B
		private static bool IsPointerIn(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x040017C9 RID: 6089
		private Action<mGraphics, int, int, int, int, int> paintItemAction;

		// Token: 0x040017CA RID: 6090
		private List<T> items = new List<T>();

		// Token: 0x040017CB RID: 6091
		private int x;

		// Token: 0x040017CC RID: 6092
		private int y;

		// Token: 0x040017CD RID: 6093
		private int width;

		// Token: 0x040017CE RID: 6094
		private int height;

		// Token: 0x040017CF RID: 6095
		private int currentItemIndex = -1;

		// Token: 0x040017D0 RID: 6096
		private int stepScroll = 70;

		// Token: 0x040017D1 RID: 6097
		private int currentOffset;

		// Token: 0x040017D2 RID: 6098
		private int currentOffsetTo;

		// Token: 0x040017D3 RID: 6099
		private int currentStepScroll;

		// Token: 0x040017D4 RID: 6100
		private int lastMouseY = -1;

		// Token: 0x040017D5 RID: 6101
		private int lastOffsetTo;

		// Token: 0x040017D6 RID: 6102
		private int itemHeight = 40;

		// Token: 0x040017D7 RID: 6103
		private bool isPressKey;
	}
}
