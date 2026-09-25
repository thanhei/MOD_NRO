using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mod.Graphics
{
	// Token: 0x02000155 RID: 341
	internal class ComboBox
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x000B1E1B File Offset: 0x000B001B
		internal bool IsShowingListItems
		{
			get
			{
				return this.isShowingListItems;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x000B1E23 File Offset: 0x000B0023
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x000B1E2B File Offset: 0x000B002B
		internal int SelectedIndex { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x000B1E34 File Offset: 0x000B0034
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x000B1E3C File Offset: 0x000B003C
		internal bool IsFocus { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x000B1E45 File Offset: 0x000B0045
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x000B1E4D File Offset: 0x000B004D
		internal string Hint { get; set; } = "";

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x000B1E56 File Offset: 0x000B0056
		internal List<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000B1E5E File Offset: 0x000B005E
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x000B1E6B File Offset: 0x000B006B
		internal int ListItemsWidth
		{
			get
			{
				return this.scrollableMenuItems.Width;
			}
			set
			{
				this.scrollableMenuItems.Width = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x000B1E79 File Offset: 0x000B0079
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x000B1E86 File Offset: 0x000B0086
		internal int ListItemsHeight
		{
			get
			{
				return this.scrollableMenuItems.Height;
			}
			set
			{
				this.scrollableMenuItems.Height = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x000B1E94 File Offset: 0x000B0094
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x000B1E9C File Offset: 0x000B009C
		internal int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
				this.UpdateScrollableMenuItemsPos();
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000B1EAB File Offset: 0x000B00AB
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x000B1EB3 File Offset: 0x000B00B3
		internal int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
				this.UpdateScrollableMenuItemsPos();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000B1EC2 File Offset: 0x000B00C2
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x000B1ECA File Offset: 0x000B00CA
		internal int Width
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
				this.UpdateScrollableMenuItemsPos();
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000B1EE4 File Offset: 0x000B00E4
		internal ComboBox(string hint, List<string> items)
		{
			this.items = items;
			this.Hint = hint;
			if (this.imgTf == null)
			{
				this.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
			}
			this.h = mScreen.ITEM_HEIGHT + 2;
			this.expandListItems = new Command("", new ComboBox.ActionListener(), 1, this)
			{
				imgFocus = new Image()
			};
			this.scrollableMenuItems = new ScrollableMenuItems<string>(items)
			{
				ItemHeight = this.h,
				Height = this.h * 4,
				PaintItemAction = new Action<mGraphics, int, int, int, int, int>(this.PaintItem),
				StepScroll = this.h * 2,
				ItemSelected = delegate
				{
					this.isShowingListItems = false;
					this.SelectedIndex = this.scrollableMenuItems.CurrentItemIndex;
				}
			};
			this.SelectedIndex = 0;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000B1FC8 File Offset: 0x000B01C8
		private static void InitGraphics()
		{
			if (ComboBox.style == null)
			{
				ComboBox.style = new GUIStyle(GUI.skin.label)
				{
					alignment = TextAnchor.UpperLeft,
					wordWrap = false
				};
				Font font = (Font)Resources.Load(string.Format("FontSys/x{0}/barmeneb", mGraphics.zoomLevel));
				ComboBox.style.font = font;
				GUIStyleState normal = ComboBox.style.normal;
				GUIStyleState hover = ComboBox.style.hover;
				Color color = new Color(0.33f, 0.16f, 0.02f);
				hover.textColor = color;
				normal.textColor = color;
				ComboBox.style.fontSize = 6 * mGraphics.zoomLevel;
			}
			if (ComboBox.imgExpand == null)
			{
				ComboBox.imgExpand = Image.createImage("ComboBox/imgExpand");
				ComboBox.imgExpand.texture = CustomGraphics.Resize(ComboBox.imgExpand.texture, ComboBox.imgExpand.texture.width * mGraphics.zoomLevel / 4, ComboBox.imgExpand.texture.height * mGraphics.zoomLevel / 4);
				ComboBox.imgExpand.w = ComboBox.imgExpand.texture.width;
				ComboBox.imgExpand.h = ComboBox.imgExpand.texture.height;
				Image.setTextureQuality(ComboBox.imgExpand.texture);
			}
			if (ComboBox.imgCollapse == null)
			{
				ComboBox.imgCollapse = Image.createImage("ComboBox/imgCollapse");
				ComboBox.imgCollapse.texture = CustomGraphics.Resize(ComboBox.imgCollapse.texture, ComboBox.imgCollapse.texture.width * mGraphics.zoomLevel / 4, ComboBox.imgCollapse.texture.height * mGraphics.zoomLevel / 4);
				ComboBox.imgCollapse.w = ComboBox.imgCollapse.texture.width;
				ComboBox.imgCollapse.h = ComboBox.imgCollapse.texture.height;
				Image.setTextureQuality(ComboBox.imgCollapse.texture);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000B21B4 File Offset: 0x000B03B4
		private void UpdateScrollableMenuItemsPos()
		{
			this.scrollableMenuItems.X = this.x + 5;
			this.scrollableMenuItems.Y = this.y + this.h + 5;
			this.scrollableMenuItems.Width = this.Width - 10;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000B2202 File Offset: 0x000B0402
		private void PaintItem(mGraphics g, int i, int x, int y, int w, int h)
		{
			mFont.tahoma_7b_dark.drawString(g, this.items[i], x + 3, y + 3, 0);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000B2224 File Offset: 0x000B0424
		internal void Paint(mGraphics g)
		{
			ComboBox.InitGraphics();
			if (this.expandListItems.img == null)
			{
				this.expandListItems.img = ComboBox.imgExpand;
			}
			this.expandListItems.x = this.x + this.w - this.expandListItems.img.getWidth() - 6;
			this.expandListItems.y = this.y + this.h - this.expandListItems.img.getHeight();
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			int num = ComboBox.TEXT_GAP_X + this.x + 3;
			int num2 = this.y + (this.h - mFont.tahoma_8b.getHeight()) / 2 + 2;
			int num3 = this.w - 7;
			g.setColor(0);
			if (this.IsFocus)
			{
				g.drawRegion(this.imgTf, 0, 81, 29, 27, 0, this.x, this.y - 1, 0);
				g.drawRegion(this.imgTf, 0, 135, 29, 27, 0, this.x + this.w - 29, this.y - 1, 0);
				g.drawRegion(this.imgTf, 0, 108, 29, 27, 0, this.x + this.w - 58, this.y - 1, 0);
				for (int i = 0; i < (this.w - 58) / 29; i++)
				{
					g.drawRegion(this.imgTf, 0, 108, 29, 27, 0, this.x + 29 + i * 29, this.y - 1, 0);
				}
				num3 -= this.x + this.w - this.expandListItems.x;
			}
			else
			{
				g.drawRegion(this.imgTf, 0, 0, 29, 27, 0, this.x, this.y - 1, 0);
				g.drawRegion(this.imgTf, 0, 54, 29, 27, 0, this.x + this.w - 29, this.y - 1, 0);
				g.drawRegion(this.imgTf, 0, 27, 29, 27, 0, this.x + this.w - 58, this.y - 1, 0);
				for (int j = 0; j < (this.w - 58) / 29; j++)
				{
					g.drawRegion(this.imgTf, 0, 27, 29, 27, 0, this.x + 29 + j * 29, this.y - 1, 0);
				}
			}
			if (this.IsFocus)
			{
				this.expandListItems.paint(g);
			}
			g.setClip(num - 3, this.y, num3, this.h + 5);
			string text = "";
			if (this.SelectedIndex > -1 && this.SelectedIndex < this.items.Count)
			{
				text = this.items[this.SelectedIndex];
			}
			if (!string.IsNullOrEmpty(text))
			{
				mFont.tahoma_8b.drawString(g, text, num, num2 + 3, 0);
			}
			if (!string.IsNullOrEmpty(this.Hint))
			{
				if (string.IsNullOrEmpty(text))
				{
					if (this.IsFocus)
					{
						mFont.tahoma_7b_focus.drawString(g, this.Hint, num, num2, 0);
					}
					else
					{
						mFont.tahoma_7b_unfocus.drawString(g, this.Hint, num, num2, 0);
					}
				}
				else
				{
					g.drawString(this.Hint, num - 1, num2 - 4, ComboBox.style);
				}
			}
			g.setClip(this.scrollableMenuItems.X - 1, this.scrollableMenuItems.Y - 1, this.scrollableMenuItems.Width + 2, this.scrollableMenuItems.Height + 2);
			if (this.isShowingListItems)
			{
				if (this.expandListItems.img != ComboBox.imgCollapse)
				{
					this.expandListItems.img = ComboBox.imgCollapse;
				}
				this.scrollableMenuItems.Paint(g);
				return;
			}
			if (this.expandListItems.img != ComboBox.imgExpand)
			{
				this.expandListItems.img = ComboBox.imgExpand;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000B2617 File Offset: 0x000B0817
		internal void Update()
		{
			this.scrollableMenuItems.Update();
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000B2624 File Offset: 0x000B0824
		internal void UpdateKey()
		{
			if (!this.IsFocus && GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
			{
				if ((!GameCanvas.isPointerMove || !GameCanvas.isPointerJustRelease) && GameCanvas.isPointerSelect && (this.lastMouseY == GameCanvas.pyMouse || this.lastMouseY == -1))
				{
					GameCanvas.isPointerJustDown = false;
					GameScr.gI().isPointerDowning = false;
					this.IsFocus = true;
					GameCanvas.clearAllPointerEvent();
					return;
				}
				return;
			}
			else
			{
				if (!this.IsFocus)
				{
					return;
				}
				if (GameCanvas.isPointerJustDown && this.lastMouseY == -1 && ComboBox.IsPointerIn(this.x, this.y, this.scrollableMenuItems.Width, this.scrollableMenuItems.Height))
				{
					this.lastMouseY = GameCanvas.pyMouse;
				}
				if (this.isShowingListItems && GameCanvas.isPointerHoldIn(0, 0, GameCanvas.w, GameCanvas.h) && !GameCanvas.isPointerHoldIn(this.scrollableMenuItems.X, this.scrollableMenuItems.Y, this.scrollableMenuItems.Width, this.scrollableMenuItems.Height) && (!GameCanvas.isPointerMove || !GameCanvas.isPointerJustRelease) && GameCanvas.isPointerSelect && (this.lastMouseY == GameCanvas.pyMouse || this.lastMouseY == -1))
				{
					GameCanvas.isPointerJustDown = false;
					GameScr.gI().isPointerDowning = false;
					this.isShowingListItems = false;
					GameCanvas.clearAllPointerEvent();
					return;
				}
				if (!this.isShowingListItems || GameCanvas.isPointerJustRelease)
				{
					this.lastMouseY = -1;
				}
				if (!this.isShowingListItems)
				{
					if (this.IsFocus && GameCanvas.isPointerHoldIn(0, 0, GameCanvas.w, GameCanvas.h) && !GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h) && GameCanvas.isPointerJustRelease)
					{
						this.IsFocus = false;
						return;
					}
					if (this.expandListItems.isPointerPressInside() || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
					{
						this.expandListItems.performAction();
						return;
					}
				}
				else
				{
					this.scrollableMenuItems.UpdateKey();
				}
				return;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000B2828 File Offset: 0x000B0A28
		internal string GetSelectedValue()
		{
			return this.items[this.SelectedIndex];
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000B283B File Offset: 0x000B0A3B
		private static bool IsPointerIn(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x0400179A RID: 6042
		private List<string> items = new List<string>();

		// Token: 0x0400179B RID: 6043
		private Image imgTf;

		// Token: 0x0400179C RID: 6044
		private bool isShowingListItems;

		// Token: 0x0400179D RID: 6045
		private int x;

		// Token: 0x0400179E RID: 6046
		private int y;

		// Token: 0x0400179F RID: 6047
		private int w;

		// Token: 0x040017A0 RID: 6048
		private int h;

		// Token: 0x040017A1 RID: 6049
		private int offset;

		// Token: 0x040017A2 RID: 6050
		private int lastMouseY = -1;

		// Token: 0x040017A3 RID: 6051
		private ScrollableMenuItems<string> scrollableMenuItems;

		// Token: 0x040017A7 RID: 6055
		internal static readonly int TEXT_GAP_X = 4;

		// Token: 0x040017A8 RID: 6056
		private static GUIStyle style;

		// Token: 0x040017A9 RID: 6057
		private static Image imgExpand;

		// Token: 0x040017AA RID: 6058
		private static Image imgCollapse;

		// Token: 0x040017AB RID: 6059
		private Command expandListItems;

		// Token: 0x02000156 RID: 342
		private enum ComboBoxAction
		{
			// Token: 0x040017AD RID: 6061
			ShowListItems = 1
		}

		// Token: 0x02000157 RID: 343
		private class ActionListener : IActionListener
		{
			// Token: 0x0600101B RID: 4123 RVA: 0x000B2880 File Offset: 0x000B0A80
			public void perform(int idAction, object p)
			{
				ComboBox comboBox = (ComboBox)p;
				if (idAction == 1)
				{
					comboBox.isShowingListItems = !comboBox.isShowingListItems;
				}
			}
		}
	}
}
