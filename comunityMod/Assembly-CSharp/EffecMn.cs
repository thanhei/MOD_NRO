using System;

// Token: 0x0200002A RID: 42
public class EffecMn
{
	// Token: 0x06000250 RID: 592 RVA: 0x0002E54A File Offset: 0x0002C74A
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0002E557 File Offset: 0x0002C757
	public static void removeEff(int id)
	{
		if (EffecMn.getEffById(id) != null)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0002E574 File Offset: 0x0002C774
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

	// Token: 0x06000253 RID: 595 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
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

	// Token: 0x06000254 RID: 596 RVA: 0x0002E608 File Offset: 0x0002C808
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

	// Token: 0x06000255 RID: 597 RVA: 0x0002E658 File Offset: 0x0002C858
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

	// Token: 0x06000256 RID: 598 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
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

	// Token: 0x06000257 RID: 599 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
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

	// Token: 0x06000258 RID: 600 RVA: 0x0002E748 File Offset: 0x0002C948
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x04000504 RID: 1284
	public static MyVector vEff = new MyVector();
}
