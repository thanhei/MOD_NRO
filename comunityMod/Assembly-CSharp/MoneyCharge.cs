using System;

// Token: 0x02000070 RID: 112
public class MoneyCharge : mScreen, IActionListener
{
	// Token: 0x06000585 RID: 1413 RVA: 0x00056018 File Offset: 0x00054218
	public MoneyCharge()
	{
		this.w = GameCanvas.w - 20;
		if (this.w > 320)
		{
			this.w = 320;
		}
		this.strPaint = mFont.tahoma_7b_green2.splitFontArray(mResources.pay_card, this.w - 20);
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = GameCanvas.h - 150 - (this.strPaint.Length - 1) * 20;
		this.h = 110 + (this.strPaint.Length - 1) * 20;
		this.yP = this.y;
		this.tfSerial = new TField();
		this.tfSerial.name = mResources.SERI_NUM;
		this.tfSerial.x = this.x + 10;
		this.tfSerial.y = this.y + 35 + (this.strPaint.Length - 1) * 20;
		this.yt = this.tfSerial.y;
		this.tfSerial.width = this.w - 20;
		this.tfSerial.height = mScreen.ITEM_HEIGHT + 2;
		if (GameCanvas.isTouch)
		{
			this.tfSerial.isFocus = false;
		}
		else
		{
			this.tfSerial.isFocus = true;
		}
		this.tfSerial.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.tfSerial.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfSerial.isPaintMouse = false;
		}
		if (!GameCanvas.isTouch)
		{
			this.right = this.tfSerial.cmdClear;
		}
		this.tfCode = new TField();
		this.tfCode.name = mResources.CARD_CODE;
		this.tfCode.x = this.x + 10;
		this.tfCode.y = this.tfSerial.y + 35;
		this.tfCode.width = this.w - 20;
		this.tfCode.height = mScreen.ITEM_HEIGHT + 2;
		this.tfCode.isFocus = false;
		this.tfCode.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.tfCode.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfCode.isPaintMouse = false;
		}
		this.left = new Command(mResources.CLOSE, this, 1, null);
		this.center = new Command(mResources.pay_card2, this, 2, null);
		if (GameCanvas.isTouch)
		{
			this.center.x = GameCanvas.w / 2 + 18;
			this.left.x = GameCanvas.w / 2 - 85;
			this.center.y = (this.left.y = this.y + this.h + 5);
		}
		this.freeAreaHeight = this.tfSerial.y - (4 * this.tfSerial.height - 10);
		this.yP = this.tfSerial.y;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00056330 File Offset: 0x00054530
	public static MoneyCharge gI()
	{
		if (MoneyCharge.instance == null)
		{
			MoneyCharge.instance = new MoneyCharge();
		}
		return MoneyCharge.instance;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00056348 File Offset: 0x00054548
	public override void switchToMe()
	{
		this.focus = 0;
		base.switchToMe();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00056358 File Offset: 0x00054558
	public override void paint(mGraphics g)
	{
		GameScr.gI().paint(g);
		PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
		for (int i = 0; i < this.strPaint.Length; i++)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
		}
		this.tfSerial.paint(g);
		this.tfCode.paint(g);
		base.paint(g);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x000563EF File Offset: 0x000545EF
	public override void update()
	{
		GameScr.gI().update();
		this.tfSerial.update();
		this.tfCode.update();
		if (Main.isWindowsPhone)
		{
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0005641E File Offset: 0x0005461E
	public override void keyPress(int keyCode)
	{
		if (this.tfSerial.isFocus)
		{
			this.tfSerial.keyPressed(keyCode);
		}
		else if (this.tfCode.isFocus)
		{
			this.tfCode.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00056460 File Offset: 0x00054660
	public override void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 1;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			GameCanvas.clearKeyPressed();
			if (this.focus == 1)
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = true;
				if (!GameCanvas.isTouch)
				{
					this.right = this.tfCode.cmdClear;
				}
			}
			else if (this.focus == 0)
			{
				this.tfSerial.isFocus = true;
				this.tfCode.isFocus = false;
				if (!GameCanvas.isTouch)
				{
					this.right = this.tfSerial.cmdClear;
				}
			}
			else
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = false;
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (GameCanvas.isPointerHoldIn(this.tfSerial.x, this.tfSerial.y, this.tfSerial.width, this.tfSerial.height))
			{
				this.focus = 0;
			}
			else if (GameCanvas.isPointerHoldIn(this.tfCode.x, this.tfCode.y, this.tfCode.width, this.tfCode.height))
			{
				this.focus = 1;
			}
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00056618 File Offset: 0x00054818
	public void clearScreen()
	{
		MoneyCharge.instance = null;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00056620 File Offset: 0x00054820
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
		if (idAction == 2)
		{
			if (this.tfSerial.getText() == null || this.tfSerial.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.serial_blank);
				return;
			}
			if (this.tfCode.getText() == null || this.tfCode.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.card_code_blank);
				return;
			}
			Service.gI().sendCardInfo(this.tfSerial.getText(), this.tfCode.getText());
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
	}

	// Token: 0x04000BC9 RID: 3017
	public static MoneyCharge instance;

	// Token: 0x04000BCA RID: 3018
	public TField tfSerial;

	// Token: 0x04000BCB RID: 3019
	public TField tfCode;

	// Token: 0x04000BCC RID: 3020
	internal int x;

	// Token: 0x04000BCD RID: 3021
	internal int y;

	// Token: 0x04000BCE RID: 3022
	internal int w;

	// Token: 0x04000BCF RID: 3023
	internal int h;

	// Token: 0x04000BD0 RID: 3024
	internal string[] strPaint;

	// Token: 0x04000BD1 RID: 3025
	internal int focus;

	// Token: 0x04000BD2 RID: 3026
	internal int yt;

	// Token: 0x04000BD3 RID: 3027
	internal int freeAreaHeight;

	// Token: 0x04000BD4 RID: 3028
	internal int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000BD5 RID: 3029
	internal int yP;
}
