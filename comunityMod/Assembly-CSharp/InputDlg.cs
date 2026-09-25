using System;

// Token: 0x02000053 RID: 83
public class InputDlg : Dialog
{
	// Token: 0x06000495 RID: 1173 RVA: 0x0004D188 File Offset: 0x0004B388
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

	// Token: 0x06000496 RID: 1174 RVA: 0x0004D23C File Offset: 0x0004B43C
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0004D2AC File Offset: 0x0004B4AC
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0004D301 File Offset: 0x0004B501
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0004D317 File Offset: 0x0004B517
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0004D32A File Offset: 0x0004B52A
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0004D332 File Offset: 0x0004B532
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000973 RID: 2419
	protected string[] info;

	// Token: 0x04000974 RID: 2420
	public TField tfInput;

	// Token: 0x04000975 RID: 2421
	internal int padLeft;
}
