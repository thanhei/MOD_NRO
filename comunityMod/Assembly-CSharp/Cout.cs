using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class Cout
{
	// Token: 0x060001FE RID: 510 RVA: 0x0002AFA6 File Offset: 0x000291A6
	public static void println(string s)
	{
		if (mSystem.isTest)
		{
			Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
			Cout.count++;
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0002AFDB File Offset: 0x000291DB
	public static void Log(string str)
	{
		if (mSystem.isTest)
		{
			Debug.Log(str);
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0002AFEA File Offset: 0x000291EA
	public static void LogError(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0002AFF9 File Offset: 0x000291F9
	public static void LogError2(string str)
	{
		bool isTest = mSystem.isTest;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0002AFEA File Offset: 0x000291EA
	public static void LogError3(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0002B001 File Offset: 0x00029201
	public static void LogWarning(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogWarning(str);
		}
	}

	// Token: 0x040004A3 RID: 1187
	public static int count;
}
