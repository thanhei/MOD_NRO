using System;

// Token: 0x0200002C RID: 44
public abstract class Effect2
{
	// Token: 0x06000269 RID: 617 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void update()
	{
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x04000547 RID: 1351
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x04000548 RID: 1352
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x04000549 RID: 1353
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x0400054A RID: 1354
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x0400054B RID: 1355
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x0400054C RID: 1356
	public static MyVector vEffectFeet = new MyVector();
}
