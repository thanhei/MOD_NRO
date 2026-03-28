using System;

// Token: 0x02000058 RID: 88
public class EffectManager : MyVector
{
	// Token: 0x060002FD RID: 765 RVA: 0x0001D6BC File Offset: 0x0001B8BC
	public void updateAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null)
			{
				effect_End.update();
				if (effect_End.isRemove)
				{
					base.removeElementAt(i);
				}
			}
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00005970 File Offset: 0x00003B70
	public static void update()
	{
		EffectManager.hiEffects.updateAll();
		EffectManager.mid_2Effects.updateAll();
		EffectManager.midEffects.updateAll();
		EffectManager.lowEffects.updateAll();
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0001D710 File Offset: 0x0001B910
	public void paintAll(mGraphics g)
	{
		for (int i = 0; i < base.size(); i++)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null && !effect_End.isRemove)
			{
				((Effect_End)base.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0001D764 File Offset: 0x0001B964
	public void removeAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null)
			{
				effect_End.isRemove = true;
				base.removeElementAt(i);
			}
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0000599A File Offset: 0x00003B9A
	public static void remove()
	{
		EffectManager.hiEffects.removeAll();
		EffectManager.lowEffects.removeAll();
		EffectManager.midEffects.removeAll();
		EffectManager.mid_2Effects.removeAll();
	}

	// Token: 0x06000302 RID: 770 RVA: 0x000059C4 File Offset: 0x00003BC4
	public static void addHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.addElement(eff);
	}

	// Token: 0x06000303 RID: 771 RVA: 0x000059D1 File Offset: 0x00003BD1
	public static void addMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.addElement(eff);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x000059DE File Offset: 0x00003BDE
	public static void addMid_2Effects(Effect_End eff)
	{
		EffectManager.mid_2Effects.addElement(eff);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000059EB File Offset: 0x00003BEB
	public static void addLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.addElement(eff);
	}

	// Token: 0x04000509 RID: 1289
	public static EffectManager lowEffects = new EffectManager();

	// Token: 0x0400050A RID: 1290
	public static EffectManager mid_2Effects = new EffectManager();

	// Token: 0x0400050B RID: 1291
	public static EffectManager midEffects = new EffectManager();

	// Token: 0x0400050C RID: 1292
	public static EffectManager hiEffects = new EffectManager();
}
