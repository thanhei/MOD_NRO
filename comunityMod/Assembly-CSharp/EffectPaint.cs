using System;

// Token: 0x02000033 RID: 51
public class EffectPaint
{
	// Token: 0x0600028D RID: 653 RVA: 0x000305C6 File Offset: 0x0002E7C6
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x0400056D RID: 1389
	public int index;

	// Token: 0x0400056E RID: 1390
	public Mob eMob;

	// Token: 0x0400056F RID: 1391
	public global::Char eChar;

	// Token: 0x04000570 RID: 1392
	public EffectCharPaint effCharPaint;

	// Token: 0x04000571 RID: 1393
	public bool isFly;
}
