using System;

// Token: 0x0200003B RID: 59
public abstract class Effect2
{
	// Token: 0x06000272 RID: 626 RVA: 0x000045ED File Offset: 0x000027ED
	public virtual void update()
	{
	}

	// Token: 0x06000273 RID: 627 RVA: 0x000045ED File Offset: 0x000027ED
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x040002E3 RID: 739
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x040002E4 RID: 740
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x040002E5 RID: 741
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x040002E6 RID: 742
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x040002E7 RID: 743
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x040002E8 RID: 744
	public static MyVector vEffectFeet = new MyVector();
}
