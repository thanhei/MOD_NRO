using System;
using System.IO;

namespace Mod
{
	// Token: 0x020000EB RID: 235
	public class LogMod
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x000A08C8 File Offset: 0x0009EAC8
		public static bool filter(string message)
		{
			bool flag = true;
			if (!LogMod.logError)
			{
				flag &= !message.Contains("[err]");
			}
			if (!LogMod.logXmap)
			{
				flag &= !message.Contains("[xmap]");
			}
			if (!LogMod.logDebug)
			{
				flag &= !message.Contains("[dbg]");
			}
			return flag;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000A0920 File Offset: 0x0009EB20
		public static void write(string message)
		{
			if (!LogMod.filter(message))
			{
				return;
			}
			object obj = LogMod.locker;
			lock (obj)
			{
				using (StreamWriter streamWriter = new StreamWriter(LogMod.pathLog, true))
				{
					LogMod.beforeWriteMessage(streamWriter);
					streamWriter.Write(message);
				}
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00004887 File Offset: 0x00002A87
		public static void writeLine(string message)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000A0994 File Offset: 0x0009EB94
		private static void beforeWriteMessage(StreamWriter writer)
		{
			if (LogMod.isShowTime)
			{
				writer.Write(DateTime.Now.ToString());
				writer.Write(" - ");
			}
		}

		// Token: 0x040014C2 RID: 5314
		public static string pathLog = "ModData\\log.txt";

		// Token: 0x040014C3 RID: 5315
		public static bool isShowTime = true;

		// Token: 0x040014C4 RID: 5316
		public static bool logError = true;

		// Token: 0x040014C5 RID: 5317
		public static bool logDebug = true;

		// Token: 0x040014C6 RID: 5318
		public static bool logXmap = true;

		// Token: 0x040014C7 RID: 5319
		private static readonly object locker = new object();
	}
}
