using System;

// Token: 0x020000B2 RID: 178
public class InputDlg : Dialog
{
	// Token: 0x060007FE RID: 2046 RVA: 0x00072828 File Offset: 0x00070A28
	public InputDlg()
	{
		this.padLeft = 40;
		if (GameCanvas.w <= 176)
		{
			this.padLeft = 10;
		}
		this.tfInput = new TField();
		this.tfInput.x = this.padLeft + 10;
		this.tfInput.y = GameCanvas.h - mScreen.ITEM_HEIGHT - 43;
		this.tfInput.width = GameCanvas.w - 2 * (this.padLeft + 10);
		this.tfInput.height = mScreen.ITEM_HEIGHT + 2;
		this.tfInput.isFocus = true;
		this.right = this.tfInput.cmdClear;
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000728E0 File Offset: 0x00070AE0
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00072950 File Offset: 0x00070B50
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00007C1A File Offset: 0x00005E1A
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00007C30 File Offset: 0x00005E30
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00007C43 File Offset: 0x00005E43
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00006C6E File Offset: 0x00004E6E
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000F47 RID: 3911
	protected string[] info;

	// Token: 0x04000F48 RID: 3912
	public TField tfInput;

	// Token: 0x04000F49 RID: 3913
	private int padLeft;
}
