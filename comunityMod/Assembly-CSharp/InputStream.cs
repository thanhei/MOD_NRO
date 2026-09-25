using System;

// Token: 0x02000054 RID: 84
public class InputStream : myReader
{
	// Token: 0x0600049C RID: 1180 RVA: 0x0004D339 File Offset: 0x0004B539
	public InputStream()
	{
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0004D341 File Offset: 0x0004B541
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0004D350 File Offset: 0x0004B550
	public InputStream(string filename)
		: base(filename)
	{
	}
}
