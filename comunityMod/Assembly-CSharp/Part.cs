using System;

// Token: 0x02000086 RID: 134
public class Part
{
	// Token: 0x06000737 RID: 1847 RVA: 0x00073780 File Offset: 0x00071980
	public Part(int type)
	{
		this.type = type;
		if (type == 0)
		{
			this.pi = new PartImage[3];
		}
		if (type == 1)
		{
			this.pi = new PartImage[17];
		}
		if (type == 2)
		{
			this.pi = new PartImage[14];
		}
		if (type == 3)
		{
			this.pi = new PartImage[2];
		}
	}

	// Token: 0x04000DBF RID: 3519
	public int type;

	// Token: 0x04000DC0 RID: 3520
	public PartImage[] pi;
}
