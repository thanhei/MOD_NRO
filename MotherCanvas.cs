using System;

// Token: 0x020000D1 RID: 209
public class MotherCanvas
{
	// Token: 0x06000ADD RID: 2781 RVA: 0x00009076 File Offset: 0x00007276
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x000A20D4 File Offset: 0x000A02D4
	public void checkZoomLevel(int w, int h)
	{
		if (Main.isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h > 384000)
			{
				mGraphics.zoomLevel = 3;
			}
		}
		else if (!Main.isPC)
		{
			if (Main.isIpod)
			{
				mGraphics.zoomLevel = 2;
			}
			else if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h >= 691200)
			{
				mGraphics.zoomLevel = 3;
			}
			else if (w * h > 153600)
			{
				mGraphics.zoomLevel = 2;
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

	// Token: 0x06000ADF RID: 2783 RVA: 0x00008C4D File Offset: 0x00006E4D
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00008C55 File Offset: 0x00006E55
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0000909F File Offset: 0x0000729F
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x000090A8 File Offset: 0x000072A8
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x000090B6 File Offset: 0x000072B6
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x000090C4 File Offset: 0x000072C4
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x000090D2 File Offset: 0x000072D2
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x000090F3 File Offset: 0x000072F3
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00009114 File Offset: 0x00007314
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x000A21A4 File Offset: 0x000A03A4
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x000A21C8 File Offset: 0x000A03C8
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x040014A6 RID: 5286
	public static MotherCanvas instance;

	// Token: 0x040014A7 RID: 5287
	public GameCanvas tCanvas;

	// Token: 0x040014A8 RID: 5288
	public int zoomLevel = 1;

	// Token: 0x040014A9 RID: 5289
	public Image imgCache;

	// Token: 0x040014AA RID: 5290
	private int[] imgRGBCache;

	// Token: 0x040014AB RID: 5291
	private int newWidth;

	// Token: 0x040014AC RID: 5292
	private int newHeight;

	// Token: 0x040014AD RID: 5293
	private int[] output;

	// Token: 0x040014AE RID: 5294
	private int OUTPUTSIZE = 20;
}
