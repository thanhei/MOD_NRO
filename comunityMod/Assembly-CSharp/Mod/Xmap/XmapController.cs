using System;
using System.Collections.Generic;
using System.Threading;
using Mod.ModHelper;
using Mod.ModHelper.CommandMod.Chat;
using Mod.R;

namespace Mod.Xmap
{
	// Token: 0x020000FE RID: 254
	internal class XmapController : ThreadActionUpdate<XmapController>
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000A3760 File Offset: 0x000A1960
		internal override int Interval
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000A3764 File Offset: 0x000A1964
		protected override void update()
		{
			LogMod.writeLine(string.Format("[xmap][dbg] update {0}", XmapController.mapEnd));
			if (XmapController.way == null)
			{
				if (!XmapController.isNextMapFailed)
				{
					string mapName = TileMap.mapNames[XmapController.mapEnd];
					MainThreadDispatcher.Dispatch(delegate
					{
						GameScr.info1.addInfo(Strings.goTo + ": " + mapName, 0);
					});
				}
				LogMod.writeLine("[xmap][dbg] Đang tạo dữ liệu map");
				XmapAlgorithm.xmapData = new XmapData();
				MainThreadDispatcher.Dispatch(new Action(XmapAlgorithm.xmapData.Load));
				while (!XmapAlgorithm.xmapData.isLoaded)
				{
					Thread.Sleep(100);
				}
				XmapAlgorithm.xmapData.LoadLinkMapCapsule();
				try
				{
					XmapController.way = XmapAlgorithm.findWay(TileMap.mapID, XmapController.mapEnd);
				}
				catch (Exception ex)
				{
					LogMod.writeLine(string.Format("[xmap][err] Lỗi tìm đường đi\n{0}", ex));
				}
				XmapController.indexWay = 0;
				if (XmapController.way == null)
				{
					MainThreadDispatcher.Dispatch(delegate
					{
						GameScr.info1.addInfo(Strings.xmapCantFindWay + "!", 0);
					});
					XmapController.finishXmap();
					return;
				}
			}
			if (TileMap.mapID == XmapController.way[XmapController.way.Count - 1].to && !global::Char.myCharz().IsCharDead())
			{
				MainThreadDispatcher.Dispatch(delegate
				{
					GameScr.info1.addInfo(Strings.xmapDestinationReached + "!", 0);
				});
				XmapController.finishXmap();
				return;
			}
			if (TileMap.mapID == XmapController.way[XmapController.indexWay].mapStart)
			{
				if (global::Char.myCharz().IsCharDead())
				{
					Service.gI().returnTownFromDead();
					XmapController.isNextMapFailed = true;
					XmapController.way = null;
				}
				else if (Utils.CanNextMap())
				{
					MainThreadDispatcher.Dispatch(delegate
					{
						Pk9rXmap.NextMap(XmapController.way[XmapController.indexWay]);
					});
					LogMod.writeLine(string.Format("[xmap][dbg] nextMap: {0}", XmapController.way[XmapController.indexWay].to));
				}
				Thread.Sleep(500);
				return;
			}
			if (TileMap.mapID == XmapController.way[XmapController.indexWay].to)
			{
				XmapController.indexWay++;
				return;
			}
			XmapController.isNextMapFailed = true;
			XmapController.way = null;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000A39A8 File Offset: 0x000A1BA8
		[ChatCommand("xmp")]
		internal static void start(int mapId)
		{
			if (ThreadAction<XmapController>.gI.IsActing)
			{
				XmapController.finishXmap();
				LogMod.writeLine("[xmap][info] Hủy xmap tới " + TileMap.mapNames[XmapController.mapEnd] + " để thực hiện xmap mới");
			}
			XmapController.mapEnd = mapId;
			ThreadAction<XmapController>.gI.toggle(new bool?(true));
			LogMod.writeLine("[xmap][info] Bắt đầu xmap tới " + TileMap.mapNames[XmapController.mapEnd]);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000A3A15 File Offset: 0x000A1C15
		internal static void finishXmap()
		{
			LogMod.writeLine("[xmap][info] Kết thúc xmap");
			XmapController.way = null;
			XmapController.isNextMapFailed = false;
			ThreadAction<XmapController>.gI.toggle(new bool?(false));
		}

		// Token: 0x040014FA RID: 5370
		private static int mapEnd;

		// Token: 0x040014FB RID: 5371
		private static List<MapNext> way;

		// Token: 0x040014FC RID: 5372
		private static int indexWay;

		// Token: 0x040014FD RID: 5373
		private static bool isNextMapFailed;
	}
}
