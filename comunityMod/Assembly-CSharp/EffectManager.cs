using System;

// Token: 0x02000032 RID: 50
public class EffectManager : MyVector
{
	// Token: 0x06000282 RID: 642 RVA: 0x00030440 File Offset: 0x0002E640
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

	// Token: 0x06000283 RID: 643 RVA: 0x00030485 File Offset: 0x0002E685
	public static void update()
	{
		EffectManager.hiEffects.updateAll();
		EffectManager.mid_2Effects.updateAll();
		EffectManager.midEffects.updateAll();
		EffectManager.lowEffects.updateAll();
	}

	// Token: 0x06000284 RID: 644 RVA: 0x000304B0 File Offset: 0x0002E6B0
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

	// Token: 0x06000285 RID: 645 RVA: 0x000304F8 File Offset: 0x0002E6F8
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

	// Token: 0x06000286 RID: 646 RVA: 0x00030536 File Offset: 0x0002E736
	public static void remove()
	{
		EffectManager.hiEffects.removeAll();
		EffectManager.lowEffects.removeAll();
		EffectManager.midEffects.removeAll();
		EffectManager.mid_2Effects.removeAll();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00030560 File Offset: 0x0002E760
	public static void addHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.addElement(eff);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0003056D File Offset: 0x0002E76D
	public static void addMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.addElement(eff);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0003057A File Offset: 0x0002E77A
	public static void addMid_2Effects(Effect_End eff)
	{
		EffectManager.mid_2Effects.addElement(eff);
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00030587 File Offset: 0x0002E787
	public static void addLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.addElement(eff);
	}

	// Token: 0x04000569 RID: 1385
	public static EffectManager lowEffects = new EffectManager();

	// Token: 0x0400056A RID: 1386
	public static EffectManager mid_2Effects = new EffectManager();

	// Token: 0x0400056B RID: 1387
	public static EffectManager midEffects = new EffectManager();

	// Token: 0x0400056C RID: 1388
	public static EffectManager hiEffects = new EffectManager();
}
