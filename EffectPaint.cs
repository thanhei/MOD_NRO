using System;

// Token: 0x02000059 RID: 89
public class EffectPaint
{
	// Token: 0x06000308 RID: 776 RVA: 0x00005A22 File Offset: 0x00003C22
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x0400050D RID: 1293
	public int index;

	// Token: 0x0400050E RID: 1294
	public Mob eMob;

	// Token: 0x0400050F RID: 1295
	public global::Char eChar;

	// Token: 0x04000510 RID: 1296
	public EffectCharPaint effCharPaint;

	// Token: 0x04000511 RID: 1297
	public bool isFly;
}
