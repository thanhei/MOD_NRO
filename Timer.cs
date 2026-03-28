using System;

// Token: 0x020000CC RID: 204
public class Timer
{
	// Token: 0x06000A6F RID: 2671 RVA: 0x00008B5F File Offset: 0x00006D5F
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0009C290 File Offset: 0x0009A490
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		if (Timer.isON && num > Timer.timeExecute)
		{
			Timer.isON = false;
			try
			{
				if (Timer.idAction > 0)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x040013AF RID: 5039
	public static IActionListener timeListener;

	// Token: 0x040013B0 RID: 5040
	public static int idAction;

	// Token: 0x040013B1 RID: 5041
	public static long timeExecute;

	// Token: 0x040013B2 RID: 5042
	public static bool isON;
}
