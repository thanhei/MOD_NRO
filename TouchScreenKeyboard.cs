using System;

// Token: 0x02000013 RID: 19
public class TouchScreenKeyboard
{
	// Token: 0x06000085 RID: 133 RVA: 0x000045EA File Offset: 0x000027EA
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType t, bool b1, bool b2, bool type, bool b3, string caption)
	{
		return null;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000045ED File Offset: 0x000027ED
	public static void Clear()
	{
	}

	// Token: 0x0400002A RID: 42
	public static bool hideInput;

	// Token: 0x0400002B RID: 43
	public static bool visible;

	// Token: 0x0400002C RID: 44
	public bool done;

	// Token: 0x0400002D RID: 45
	public bool active;

	// Token: 0x0400002E RID: 46
	public string text;
}
