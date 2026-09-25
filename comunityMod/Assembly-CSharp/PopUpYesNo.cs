using System;

// Token: 0x0200008D RID: 141
public class PopUpYesNo : IActionListener
{
	// Token: 0x06000760 RID: 1888 RVA: 0x00074F30 File Offset: 0x00073130
	public void setPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.info = new string[] { info };
		this.H = 29;
		this.cmdYes = cmdYes;
		this.cmdNo = cmdNo;
		this.cmdYes.img = (this.cmdNo.img = GameScr.imgNut);
		this.cmdYes.imgFocus = (this.cmdNo.imgFocus = GameScr.imgNutF);
		this.cmdYes.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdNo.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdYes.h = mGraphics.getImageHeight(cmdYes.img);
		this.cmdNo.h = mGraphics.getImageHeight(cmdYes.img);
		this.last = mSystem.currentTimeMillis();
		this.dem = this.info[0].Length / 3;
		if (this.dem < 15)
		{
			this.dem = 15;
		}
		TextInfo.reset();
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00075030 File Offset: 0x00073230
	public void paint(mGraphics g)
	{
		PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + ((!GameCanvas.isTouch) ? 10 : 0), 16777215, false);
		if (this.info != null)
		{
			TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
			if (GameCanvas.isTouch)
			{
				this.cmdYes.paint(g);
				mFont.tahoma_7_yellow.drawString(g, this.dem.ToString() + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
				return;
			}
			if (TField.isQwerty)
			{
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
				return;
			}
			mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000751BC File Offset: 0x000733BC
	public void update()
	{
		if (this.info != null)
		{
			this.X = GameCanvas.w - 5 - this.W;
			this.Y = 45;
			if (GameCanvas.w - 50 > 155 + this.W)
			{
				this.X = GameCanvas.w - 55 - this.W;
				this.Y = 5;
			}
			this.cmdYes.x = this.X - 35;
			this.cmdYes.y = this.Y;
			this.curr = mSystem.currentTimeMillis();
			Res.outz("curr - last= " + (this.curr - this.last).ToString());
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.dem--;
			}
			if (this.dem == 0)
			{
				GameScr.gI().popUpYesNo = null;
			}
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00004887 File Offset: 0x00002A87
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000E38 RID: 3640
	public Command cmdYes;

	// Token: 0x04000E39 RID: 3641
	public Command cmdNo;

	// Token: 0x04000E3A RID: 3642
	public string[] info;

	// Token: 0x04000E3B RID: 3643
	internal int X;

	// Token: 0x04000E3C RID: 3644
	internal int Y;

	// Token: 0x04000E3D RID: 3645
	internal int W = 120;

	// Token: 0x04000E3E RID: 3646
	internal int H;

	// Token: 0x04000E3F RID: 3647
	internal int dem;

	// Token: 0x04000E40 RID: 3648
	internal long last;

	// Token: 0x04000E41 RID: 3649
	internal long curr;
}
