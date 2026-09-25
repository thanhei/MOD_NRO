using System;

// Token: 0x0200003B RID: 59
public class FrameImage
{
	// Token: 0x060002DD RID: 733 RVA: 0x000344D0 File Offset: 0x000326D0
	public FrameImage(int ID)
	{
		this.Id = ID;
		Image image = Effect_End.getImage(ID);
		if (image != null)
		{
			this.imgFrame = image;
			this.frameWidth = (int)Effect_End.arrInfoEff[ID][0];
			this.frameHeight = (int)(Effect_End.arrInfoEff[ID][1] / Effect_End.arrInfoEff[ID][2]);
			this.nFrame = (int)Effect_End.arrInfoEff[ID][2];
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0003453C File Offset: 0x0003273C
	public FrameImage(Image img, int width, int height)
	{
		if (img != null)
		{
			this.imgFrame = img;
			this.frameWidth = width;
			this.frameHeight = height;
			this.nFrame = img.getHeight() / height;
			if (this.nFrame < 1)
			{
				this.nFrame = 1;
			}
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0003458C File Offset: 0x0003278C
	public FrameImage(Image img, int numW, int numH, int numNull)
	{
		if (img != null)
		{
			this.imgFrame = img;
			this.numWidth = numW;
			this.numHeight = numH;
			this.frameWidth = this.imgFrame.getWidth() / numW;
			this.frameHeight = this.imgFrame.getHeight() / numH;
			this.nFrame = numW * numH - numNull;
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x000345F0 File Offset: 0x000327F0
	public void drawFrame(int idx, int x, int y, int trans, int anchor, mGraphics g)
	{
		try
		{
			if (this.imgFrame != null)
			{
				if (idx > this.nFrame)
				{
					idx = this.nFrame;
				}
				int num = idx * this.frameHeight;
				if (num > this.frameHeight * (this.nFrame - 1) || num < 0)
				{
					num = this.frameHeight * (this.nFrame - 1);
				}
				g.drawRegion(this.imgFrame, 0, num, this.frameWidth, this.frameHeight, trans, x, y, anchor);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000607 RID: 1543
	public int frameWidth;

	// Token: 0x04000608 RID: 1544
	public int frameHeight;

	// Token: 0x04000609 RID: 1545
	public int nFrame;

	// Token: 0x0400060A RID: 1546
	public Image imgFrame;

	// Token: 0x0400060B RID: 1547
	public int Id = -1;

	// Token: 0x0400060C RID: 1548
	public int numWidth;

	// Token: 0x0400060D RID: 1549
	public int numHeight;
}
