using System;

// Token: 0x02000039 RID: 57
public class EffecMn
{
	// Token: 0x06000259 RID: 601 RVA: 0x00005573 File Offset: 0x00003773
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00005580 File Offset: 0x00003780
	public static void removeEff(int id)
	{
		if (EffecMn.getEffById(id) != null)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0001776C File Offset: 0x0001596C
	public static Effect getEffById(int id)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			Effect effect = (Effect)EffecMn.vEff.elementAt(i);
			if (effect.effId == id)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x000177B4 File Offset: 0x000159B4
	public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == -layer)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
			}
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00017810 File Offset: 0x00015A10
	public static void paintLayer1(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 1)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001786C File Offset: 0x00015A6C
	public static void paintLayer2(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 2)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x000178C8 File Offset: 0x00015AC8
	public static void paintLayer3(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 3)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00017924 File Offset: 0x00015B24
	public static void paintLayer4(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 4)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00017980 File Offset: 0x00015B80
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x040002A0 RID: 672
	public static MyVector vEff = new MyVector();
}
