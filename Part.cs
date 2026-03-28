using System;

// Token: 0x02000076 RID: 118
public class Part
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00026D90 File Offset: 0x00024F90
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

	// Token: 0x040006FA RID: 1786
	public int type;

	// Token: 0x040006FB RID: 1787
	public PartImage[] pi;
}
