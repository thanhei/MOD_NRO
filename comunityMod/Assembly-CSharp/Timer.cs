using System;

// Token: 0x020000BC RID: 188
public class Timer
{
	// Token: 0x060009CB RID: 2507 RVA: 0x0008CEB9 File Offset: 0x0008B0B9
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0008CEDC File Offset: 0x0008B0DC
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		if (!Timer.isON || num <= Timer.timeExecute)
		{
			return;
		}
		Timer.isON = false;
		try
		{
			if (Timer.idAction > 0)
			{
				GameScr.gI().actionPerform(Timer.idAction, null);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04001154 RID: 4436
	public static IActionListener timeListener;

	// Token: 0x04001155 RID: 4437
	public static int idAction;

	// Token: 0x04001156 RID: 4438
	public static long timeExecute;

	// Token: 0x04001157 RID: 4439
	public static bool isON;
}
