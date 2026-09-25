using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Mod.Xmap
{
	// Token: 0x02000101 RID: 257
	internal class XmapData
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x000A3AC4 File Offset: 0x000A1CC4
		internal XmapData()
		{
			this.links = new List<MapNext>[TileMap.mapNames.Length];
			for (int i = 0; i < this.links.Length; i++)
			{
				this.links[i] = new List<MapNext>();
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000A3B09 File Offset: 0x000A1D09
		internal void Load()
		{
			this.LoadLinks();
			this.LoadLinksAutoWaypoint();
			this.AddLinksHome();
			this.LoadLinkSieuThi();
			this.LoadLinkToCold();
			this.isLoaded = true;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000A3B30 File Offset: 0x000A1D30
		private static void RemoveMapsHomeInGroupMaps()
		{
			foreach (GroupMap groupMap in XmapData.groups)
			{
				int cgender = global::Char.myCharz().cgender;
				if (cgender != 0)
				{
					if (cgender != 1)
					{
						groupMap.maps.Remove(21);
						groupMap.maps.Remove(22);
					}
					else
					{
						groupMap.maps.Remove(21);
						groupMap.maps.Remove(23);
					}
				}
				else
				{
					groupMap.maps.Remove(22);
					groupMap.maps.Remove(23);
				}
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000A3BE8 File Offset: 0x000A1DE8
		private static void LoadGroupMaps(TextReader reader)
		{
			string text;
			while ((text = reader.ReadLine()) != null)
			{
				text = text.Trim();
				if (!text.StartsWith("#") && !(text == ""))
				{
					List<int> list = Array.ConvertAll<string, int>(reader.ReadLine().Trim().Split(' ', StringSplitOptions.None), new Converter<string, int>(int.Parse)).ToList<int>();
					string[] groupNames = text.Split('|', StringSplitOptions.None);
					if (XmapData.groups.Any<GroupMap>((GroupMap gM) => gM.names.SequenceEqual<string>(groupNames)))
					{
						GroupMap groupMap = XmapData.groups.First<GroupMap>((GroupMap gM) => gM.names.SequenceEqual<string>(groupNames));
						using (List<int>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int num = enumerator.Current;
								if (!groupMap.maps.Contains(num))
								{
									groupMap.maps.Add(num);
								}
							}
							continue;
						}
					}
					XmapData.groups.Add(new GroupMap(groupNames, list));
				}
			}
			reader.Dispose();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000A3D10 File Offset: 0x000A1F10
		private static void LoadGroupMapsFromFile(string path)
		{
			path = Path.Combine(Utils.GetRootDataPath(), path);
			try
			{
				XmapData.LoadGroupMaps(new StreamReader(path));
			}
			catch
			{
			}
			XmapData.RemoveMapsHomeInGroupMaps();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000A3D50 File Offset: 0x000A1F50
		private static void LoadGroupMapsFromResource()
		{
			try
			{
				XmapData.LoadGroupMaps(new StringReader(Resources.Load<TextAsset>("TextData/GroupMapsXmap").text));
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
			XmapData.RemoveMapsHomeInGroupMaps();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000A3DA4 File Offset: 0x000A1FA4
		internal static void LoadGroupMaps()
		{
			XmapData.groups.Clear();
			XmapData.LoadGroupMapsFromResource();
			XmapData.LoadGroupMapsFromFile("TextData\\GroupMapsXmap.txt");
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000A3DC0 File Offset: 0x000A1FC0
		private void LoadLinks(TextReader reader)
		{
			string text;
			while ((text = reader.ReadLine()) != null)
			{
				text = text.Trim();
				if (!text.StartsWith("#") && !text.Equals(""))
				{
					int[] array = Array.ConvertAll<string, int>(text.Split(' ', StringSplitOptions.None), new Converter<string, int>(int.Parse));
					int num = array[0];
					int num2 = array[1];
					TypeMapNext typeMapNext = (TypeMapNext)array[2];
					int num3 = array.Length - 3;
					int[] array2 = new int[num3];
					Array.Copy(array, 3, array2, 0, num3);
					this.links[num].Add(new MapNext(num, num2, typeMapNext, array2));
				}
			}
			reader.Dispose();
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000A3E5C File Offset: 0x000A205C
		private void LoadLinksFromFile(string path)
		{
			path = Path.Combine(Utils.GetRootDataPath(), path);
			try
			{
				this.LoadLinks(new StreamReader(path));
			}
			catch (Exception ex)
			{
				LogMod.writeLine(string.Format("[xmap][error] Lỗi đọc links từ tệp {0}\n{1}", path, ex));
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000A3EA8 File Offset: 0x000A20A8
		private void LoadLinksFromResource()
		{
			TextAsset textAsset = Resources.Load<TextAsset>("TextData/LinkMapsXmap");
			this.LoadLinks(new StringReader(textAsset.text));
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000A3ED1 File Offset: 0x000A20D1
		private void LoadLinks()
		{
			this.LoadLinksFromResource();
			this.LoadLinksFromFile("TextData\\LinkMapsXmap.txt");
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000A3EE4 File Offset: 0x000A20E4
		internal void LoadLinkMapCapsule()
		{
			if (Pk9rXmap.CanUseCapsuleVip())
			{
				LogMod.writeLine("[xmap][dbg] Sử dụng capsule đặc biệt");
				Service.gI().useItem(0, 1, -1, XmapUtils.ID_ITEM_CAPSULE_VIP);
			}
			else
			{
				if (!Pk9rXmap.CanUseCapsuleNormal())
				{
					return;
				}
				LogMod.writeLine("[xmap][dbg] Sử dụng capsule thường");
				Service.gI().useItem(0, 1, -1, XmapUtils.ID_ITEM_CAPSULE_NORMAL);
			}
			int mapID = TileMap.mapID;
			string[] mapNames = GameCanvas.panel.mapNames;
			int num = mapNames.Length;
			for (int i = 0; i < num; i++)
			{
				int mapIdFromName = XmapUtils.getMapIdFromName(mapNames[i]);
				if (mapIdFromName != -1)
				{
					this.links[mapID].Add(new MapNext(mapID, mapIdFromName, TypeMapNext.Capsule, new int[] { i }));
				}
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000A3F8C File Offset: 0x000A218C
		private void LoadLinksAutoWaypoint(TextReader reader)
		{
			string text;
			while ((text = reader.ReadLine()) != null)
			{
				text = text.Trim();
				if (!text.StartsWith("#") && !text.Equals(""))
				{
					int[] array = Array.ConvertAll<string, int>(text.Split(' ', StringSplitOptions.None), new Converter<string, int>(int.Parse));
					int num = array.Length;
					for (int i = 0; i < num; i++)
					{
						int num2 = array[i];
						if (i != 0)
						{
							this.links[num2].Add(new MapNext(num2, array[i - 1], TypeMapNext.AutoWaypoint, new int[0]));
						}
						if (i != num - 1)
						{
							this.links[num2].Add(new MapNext(num2, array[i + 1], TypeMapNext.AutoWaypoint, new int[0]));
						}
					}
				}
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000A404C File Offset: 0x000A224C
		private void LoadLinksAutoWaypointFromResource()
		{
			try
			{
				TextAsset textAsset = Resources.Load<TextAsset>("TextData/AutoLinkMapsWaypoint");
				this.LoadLinksAutoWaypoint(new StringReader(textAsset.text));
			}
			catch (Exception ex)
			{
				LogMod.writeLine(string.Format("[xmap][error] Lỗi đọc links autowaypoint từ resource\n{0}", ex));
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000A409C File Offset: 0x000A229C
		private void LoadLinksAutoWaypointFromFile(string path)
		{
			path = Path.Combine(Utils.GetRootDataPath(), path);
			try
			{
				this.LoadLinksAutoWaypoint(new StreamReader(path));
			}
			catch (Exception ex)
			{
				LogMod.writeLine(string.Format("[xmap][error] Lỗi đọc links autowaypoint từ tệp {0}\n{1}", path, ex));
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000A40E8 File Offset: 0x000A22E8
		private void LoadLinksAutoWaypoint()
		{
			this.LoadLinksAutoWaypointFromResource();
			this.LoadLinksAutoWaypointFromFile("TextData\\AutoLinkMapsWaypoint.txt");
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000A40FC File Offset: 0x000A22FC
		private void AddLinksHome()
		{
			int cgender = global::Char.myCharz().cgender;
			int idMapHome = XmapUtils.getIdMapHome(cgender);
			int idMapLang = XmapUtils.getIdMapLang(cgender);
			this.links[idMapHome].Add(new MapNext(idMapHome, idMapLang, TypeMapNext.AutoWaypoint, null));
			this.links[idMapLang].Add(new MapNext(idMapLang, idMapHome, TypeMapNext.AutoWaypoint, null));
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000A414C File Offset: 0x000A234C
		private void LoadLinkSieuThi()
		{
			int cgender = global::Char.myCharz().cgender;
			int num = XmapUtils.ID_MAP_TTVT_BASE + cgender;
			int[] array = new int[2];
			array[0] = 10;
			int[] array2 = array;
			this.links[84].Add(new MapNext(84, num, TypeMapNext.NpcMenu, array2));
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000A4190 File Offset: 0x000A2390
		private void LoadLinkToCold()
		{
			if (global::Char.myCharz().taskMaint.taskId <= 30)
			{
				return;
			}
			int[] array = new int[2];
			array[0] = 12;
			int[] array2 = array;
			this.links[19].Add(new MapNext(19, 109, TypeMapNext.NpcMenu, array2));
		}

		// Token: 0x04001503 RID: 5379
		private const int ID_MAP_SIEU_THI = 84;

		// Token: 0x04001504 RID: 5380
		private const int ID_MAP_TPVGT = 19;

		// Token: 0x04001505 RID: 5381
		private const int ID_MAP_TO_COLD = 109;

		// Token: 0x04001506 RID: 5382
		internal List<MapNext>[] links;

		// Token: 0x04001507 RID: 5383
		internal bool isLoaded;

		// Token: 0x04001508 RID: 5384
		internal static List<GroupMap> groups = new List<GroupMap>();
	}
}
