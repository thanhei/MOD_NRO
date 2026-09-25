using System;
using Mod.R;

namespace Mod.Xmap
{
	// Token: 0x02000109 RID: 265
	internal class XmapUtils
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x000A4378 File Offset: 0x000A2578
		internal static int getX(sbyte type)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (waypoint.maxX < 60 && type == 0)
				{
					return 15;
				}
				if ((int)waypoint.minX > TileMap.pxw - 60 && type == 2)
				{
					return TileMap.pxw - 15;
				}
			}
			return 0;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000A43DC File Offset: 0x000A25DC
		internal static int getY(sbyte type)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (waypoint.maxX < 60 && type == 0)
				{
					return (int)waypoint.maxY;
				}
				if ((int)waypoint.minX > TileMap.pxw - 60 && type == 2)
				{
					return (int)waypoint.maxY;
				}
			}
			return 0;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000A4440 File Offset: 0x000A2640
		internal static Waypoint findWaypoint(int idMap)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (Utils.getTextPopup(waypoint.popup).Equals(TileMap.mapNames[idMap]))
				{
					return waypoint;
				}
			}
			return null;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000A4490 File Offset: 0x000A2690
		internal static int getMapIdFromName(string mapName)
		{
			int cgender = global::Char.myCharz().cgender;
			if (mapName.Equals(LocalizedString.goHome))
			{
				return XmapUtils.ID_MAP_HOME_BASE + cgender;
			}
			if (mapName.Equals(LocalizedString.spaceshipStation))
			{
				return XmapUtils.ID_MAP_TTVT_BASE + cgender;
			}
			if (LocalizedString.backTo.ContainsReversed(mapName))
			{
				mapName = LocalizedString.backTo.Replace(mapName, "");
				if (TileMap.mapNames[XmapUtils.mapCapsuleReturn].Equals(mapName))
				{
					return XmapUtils.mapCapsuleReturn;
				}
				if (mapName == LocalizedString.stoneForest)
				{
					return -1;
				}
			}
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (mapName.Equals(TileMap.mapNames[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000A4547 File Offset: 0x000A2747
		internal static int getIdMapHome(int cgender)
		{
			return XmapUtils.ID_MAP_HOME_BASE + cgender;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000A4550 File Offset: 0x000A2750
		internal static int getIdMapLang(int cgender)
		{
			return XmapUtils.ID_MAP_LANG_BASE * cgender;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000A455C File Offset: 0x000A275C
		internal static bool hasItemCapsuleVip()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == XmapUtils.ID_ITEM_CAPSULE_VIP)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000A45A0 File Offset: 0x000A27A0
		internal static bool hasItemCapsuleNormal()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == XmapUtils.ID_ITEM_CAPSULE_NORMAL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400151D RID: 5405
		internal static int mapCapsuleReturn = -1;

		// Token: 0x0400151E RID: 5406
		internal static readonly short ID_ITEM_CAPSULE_VIP = 194;

		// Token: 0x0400151F RID: 5407
		internal static readonly short ID_ITEM_CAPSULE_NORMAL = 193;

		// Token: 0x04001520 RID: 5408
		internal static readonly int ID_MAP_HOME_BASE = 21;

		// Token: 0x04001521 RID: 5409
		internal static readonly int ID_MAP_LANG_BASE = 7;

		// Token: 0x04001522 RID: 5410
		internal static readonly int ID_MAP_TTVT_BASE = 24;
	}
}
