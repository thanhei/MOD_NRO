using System;

// Token: 0x02000072 RID: 114
public class MotherCanvas
{
	// Token: 0x06000599 RID: 1433 RVA: 0x00056F3C File Offset: 0x0005513C
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00056F68 File Offset: 0x00055168
	public void checkZoomLevel(int w, int h)
	{
		if (Main.isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
				return;
			}
			if (w * h > 384000)
			{
				mGraphics.zoomLevel = 3;
				return;
			}
		}
		else if (!Main.isPC)
		{
			if (Main.isIpod)
			{
				mGraphics.zoomLevel = 2;
				return;
			}
			if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
				return;
			}
			if (w * h >= 691200)
			{
				mGraphics.zoomLevel = 3;
				return;
			}
			if (w * h > 153600)
			{
				mGraphics.zoomLevel = 2;
				return;
			}
		}
		else
		{
			mGraphics.zoomLevel = 2;
			if (w * h < 480000)
			{
				mGraphics.zoomLevel = 1;
			}
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000357D1 File Offset: 0x000339D1
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000357D9 File Offset: 0x000339D9
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00057002 File Offset: 0x00055202
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0005700B File Offset: 0x0005520B
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00057019 File Offset: 0x00055219
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00057027 File Offset: 0x00055227
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00057035 File Offset: 0x00055235
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00057056 File Offset: 0x00055256
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00057077 File Offset: 0x00055277
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00057098 File Offset: 0x00055298
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x000570BC File Offset: 0x000552BC
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x04000BED RID: 3053
	public static MotherCanvas instance;

	// Token: 0x04000BEE RID: 3054
	public GameCanvas tCanvas;

	// Token: 0x04000BEF RID: 3055
	public int zoomLevel = 1;

	// Token: 0x04000BF0 RID: 3056
	public Image imgCache;

	// Token: 0x04000BF1 RID: 3057
	internal int[] imgRGBCache;

	// Token: 0x04000BF2 RID: 3058
	internal int newWidth;

	// Token: 0x04000BF3 RID: 3059
	internal int newHeight;

	// Token: 0x04000BF4 RID: 3060
	internal int[] output;

	// Token: 0x04000BF5 RID: 3061
	internal int OUTPUTSIZE = 20;
}
