using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class SMS
{
	// Token: 0x0600009F RID: 159 RVA: 0x00004659 File Offset: 0x00002859
	public static int send(string content, string to)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return SMS.__send(content, to);
		}
		return SMS._send(content, to);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000AD00 File Offset: 0x00008F00
	private static int _send(string content, string to)
	{
		if (SMS.status != 0)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (SMS.status == 0)
				{
					break;
				}
			}
			if (SMS.status != 0)
			{
				Cout.LogError("CANNOT SEND SMS " + content + " WHEN SENDING " + SMS._content);
				return -1;
			}
		}
		SMS._content = content;
		SMS._to = to;
		SMS._result = -1;
		SMS.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			if (SMS.status == 0)
			{
				break;
			}
		}
		if (j == 500)
		{
			Debug.LogError("TOO LONG FOR SEND SMS " + content);
			SMS.status = 0;
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Send SMS ",
				content,
				" done in ",
				j * 5,
				"ms"
			}));
		}
		return SMS._result;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000AE10 File Offset: 0x00009010
	private static int __send(string content, string to)
	{
		int num = iOSPlugins.Check();
		Cout.println("vao sms ko " + num);
		if (num >= 0)
		{
			SMS.f = true;
			SMS.sendEnable = true;
			iOSPlugins.SMSsend(to, content, num);
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		return num;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000AE5C File Offset: 0x0000905C
	public static void update()
	{
		float num = Time.time;
		if (num - (float)SMS.time > 1f)
		{
			SMS.time++;
		}
		if (SMS.f)
		{
			SMS.OnSMS();
		}
		if (SMS.status == 2)
		{
			SMS.status = 1;
			try
			{
				SMS._result = SMS.__send(SMS._content, SMS._to);
			}
			catch (Exception ex)
			{
				Debug.Log("CANNOT SEND SMS");
			}
			SMS.status = 0;
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000AEEC File Offset: 0x000090EC
	private static void OnSMS()
	{
		if (SMS.sendEnable)
		{
			if (iOSPlugins.checkRotation() == 1)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			else if (iOSPlugins.checkRotation() == -1)
			{
				Screen.orientation = ScreenOrientation.Portrait;
			}
			else if (iOSPlugins.checkRotation() == 0)
			{
				Screen.orientation = ScreenOrientation.AutoRotation;
			}
			else if (iOSPlugins.checkRotation() == 2)
			{
				Screen.orientation = ScreenOrientation.LandscapeRight;
			}
			else if (iOSPlugins.checkRotation() == 3)
			{
				Screen.orientation = ScreenOrientation.PortraitUpsideDown;
			}
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				iOSPlugins.Send();
				SMS.sendEnable = false;
				SMS.time0 = 0;
			}
		}
		if (iOSPlugins.unpause() == 1)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				SMS.f = false;
				iOSPlugins.back();
				SMS.time0 = 0;
			}
		}
	}

	// Token: 0x04000052 RID: 82
	private const int INTERVAL = 5;

	// Token: 0x04000053 RID: 83
	private const int MAXTIME = 500;

	// Token: 0x04000054 RID: 84
	private static int status;

	// Token: 0x04000055 RID: 85
	private static int _result;

	// Token: 0x04000056 RID: 86
	private static string _to;

	// Token: 0x04000057 RID: 87
	private static string _content;

	// Token: 0x04000058 RID: 88
	private static bool f;

	// Token: 0x04000059 RID: 89
	private static int time;

	// Token: 0x0400005A RID: 90
	public static bool sendEnable;

	// Token: 0x0400005B RID: 91
	private static int time0;
}
