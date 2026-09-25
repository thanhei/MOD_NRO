using System;
using System.Collections.Generic;

namespace Mod.ModHelper
{
	// Token: 0x0200013F RID: 319
	internal class UIReportersManager
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x000B10D5 File Offset: 0x000AF2D5
		public static void AddReporter(Reporter reporter)
		{
			UIReportersManager.reporters.Add(reporter);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000B10E2 File Offset: 0x000AF2E2
		public static void RemoveReporter(Reporter reporter)
		{
			UIReportersManager.reporters.Remove(reporter);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000B10F0 File Offset: 0x000AF2F0
		public static void ClearReporters()
		{
			UIReportersManager.reporters.Clear();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000B10FC File Offset: 0x000AF2FC
		public static void handlePaintGameScr(mGraphics g)
		{
			int num = 50;
			foreach (Reporter reporter in UIReportersManager.reporters)
			{
				num += reporter(num, g) + 5;
			}
		}

		// Token: 0x0400176E RID: 5998
		public const byte MinY = 50;

		// Token: 0x0400176F RID: 5999
		public const byte ItemGap = 5;

		// Token: 0x04001770 RID: 6000
		private static List<Reporter> reporters = new List<Reporter>();
	}
}
