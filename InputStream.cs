using System;

// Token: 0x0200000A RID: 10
public class InputStream : myReader
{
	// Token: 0x06000053 RID: 83 RVA: 0x000042EE File Offset: 0x000024EE
	public InputStream()
	{
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000042F6 File Offset: 0x000024F6
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004305 File Offset: 0x00002505
	public InputStream(string filename) : base(filename)
	{
	}
}
