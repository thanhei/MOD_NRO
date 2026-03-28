using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class Cout
{
	// Token: 0x06000006 RID: 6 RVA: 0x00004000 File Offset: 0x00002200
	public static void println(string s)
	{
		if (mSystem.isTest)
		{
			Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
			Cout.count++;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000403E File Offset: 0x0000223E
	public static void Log(string str)
	{
		if (mSystem.isTest)
		{
			Debug.Log(str);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00004050 File Offset: 0x00002250
	public static void LogError(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00004062 File Offset: 0x00002262
	public static void LogError2(string str)
	{
		if (mSystem.isTest)
		{
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00004062 File Offset: 0x00002262
	public static void LogError3(string str)
	{
		if (mSystem.isTest)
		{
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000406E File Offset: 0x0000226E
	public static void LogWarning(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogWarning(str);
		}
	}

	// Token: 0x04000001 RID: 1
	public static int count;
}
