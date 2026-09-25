using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
internal class Net
{
	// Token: 0x060005D5 RID: 1493 RVA: 0x00057C18 File Offset: 0x00055E18
	public static void update()
	{
		if (Net.www != null && Net.www.isDone)
		{
			string text = string.Empty;
			if (Net.www.error == null || Net.www.error.Equals(string.Empty))
			{
				text = Net.www.text;
			}
			Net.www = null;
			if (Net.h != null)
			{
				Net.h.perform(text);
			}
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00057C83 File Offset: 0x00055E83
	public static void connectHTTP(string link, Command h)
	{
		if (Net.www != null)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00057CA7 File Offset: 0x00055EA7
	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		if (link != null)
		{
			h.perform(link);
		}
	}

	// Token: 0x04000C0B RID: 3083
	public static WWW www;

	// Token: 0x04000C0C RID: 3084
	public static Command h;
}
