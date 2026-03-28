using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
internal class Net
{
	// Token: 0x06000081 RID: 129 RVA: 0x0000A740 File Offset: 0x00008940
	public static void update()
	{
		if (Net.www != null && Net.www.isDone)
		{
			string str = string.Empty;
			if (Net.www.error == null || Net.www.error.Equals(string.Empty))
			{
				str = Net.www.text;
			}
			Net.www = null;
			if (Net.h != null)
			{
				Net.h.perform(str);
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000045AE File Offset: 0x000027AE
	public static void connectHTTP(string link, Command h)
	{
		if (Net.www != null)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000045D5 File Offset: 0x000027D5
	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		if (link != null)
		{
			h.perform(link);
		}
	}

	// Token: 0x04000028 RID: 40
	public static WWW www;

	// Token: 0x04000029 RID: 41
	public static Command h;
}
