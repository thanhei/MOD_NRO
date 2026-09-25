using System;
using System.Collections.Generic;

namespace Mod.Xmap
{
	// Token: 0x020000FD RID: 253
	internal class XmapAlgorithm
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x000A35CC File Offset: 0x000A17CC
		internal static List<MapNext> findWay(int mapStart, int mapEnd)
		{
			LogMod.writeLine(string.Format("[xmap][dbg] Bắt đầu tìm đường từ {0} tới {1}", mapStart, mapEnd));
			if (XmapAlgorithm.xmapData == null)
			{
				throw new Exception("xmapData is null");
			}
			int num = XmapAlgorithm.xmapData.links.Length;
			MapNext[] array = new MapNext[num];
			bool[] array2 = new bool[num];
			int[] array3 = new int[num];
			for (int i = 0; i < num; i++)
			{
				array3[i] = int.MaxValue;
			}
			array3[mapStart] = 0;
			for (int j = 0; j < num; j++)
			{
				int num2 = -1;
				for (int k = 0; k < num; k++)
				{
					if (!array2[k] && (num2 == -1 || array3[k] < array3[num2]))
					{
						num2 = k;
					}
				}
				if (num2 == -1)
				{
					break;
				}
				List<MapNext> list = XmapAlgorithm.xmapData.links[num2];
				int count = list.Count;
				for (int l = 0; l < count; l++)
				{
					MapNext mapNext = list[l];
					int num3 = 1;
					if (mapNext.type == TypeMapNext.NpcMenu && mapNext.info[0] == 38)
					{
						num3 = 100;
					}
					int num4 = array3[num2] + num3;
					if (num4 < array3[mapNext.to])
					{
						array3[mapNext.to] = num4;
						array[mapNext.to] = mapNext;
					}
				}
				array2[num2] = true;
			}
			List<MapNext> list2 = new List<MapNext>();
			for (int num5 = mapEnd; num5 != mapStart; num5 = array[num5].mapStart)
			{
				list2.Add(array[num5]);
			}
			list2.Reverse();
			if (list2[0].mapStart == mapStart)
			{
				return list2;
			}
			return null;
		}

		// Token: 0x040014F9 RID: 5369
		internal static XmapData xmapData;
	}
}
