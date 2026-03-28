using System;

// Token: 0x0200001E RID: 30
public class ipKeyboard
{
	// Token: 0x06000105 RID: 261 RVA: 0x0000D444 File Offset: 0x0000B644
	public static void openKeyBoard(string caption, int type, string text, Command action)
	{
		ipKeyboard.act = action;
		TouchScreenKeyboardType t = (type != 0 && type != 2) ? TouchScreenKeyboardType.NumberPad : TouchScreenKeyboardType.ASCIICapable;
		TouchScreenKeyboard.hideInput = false;
		ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000D488 File Offset: 0x0000B688
	public static void update()
	{
		try
		{
			if (ipKeyboard.tk != null)
			{
				if (ipKeyboard.tk.done)
				{
					if (ipKeyboard.act != null)
					{
						ipKeyboard.act.perform(ipKeyboard.tk.text);
					}
					ipKeyboard.tk.text = string.Empty;
					ipKeyboard.tk = null;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040000E7 RID: 231
	private static TouchScreenKeyboard tk;

	// Token: 0x040000E8 RID: 232
	public static int TEXT;

	// Token: 0x040000E9 RID: 233
	public static int NUMBERIC = 1;

	// Token: 0x040000EA RID: 234
	public static int PASS = 2;

	// Token: 0x040000EB RID: 235
	private static Command act;
}
