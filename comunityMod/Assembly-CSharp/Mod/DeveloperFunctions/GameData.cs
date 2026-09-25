using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod.ModHelper;
using Mod.ModHelper.Menu;
using Newtonsoft.Json;
using UnityEngine;

namespace Mod.DeveloperFunctions
{
	// Token: 0x0200015F RID: 351
	internal static class GameData
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x000B62A8 File Offset: 0x000B44A8
		internal static void ShowMenu()
		{
			new MenuBuilder().setChatPopup("Select the data type you want to extract.").addItem("Maps", new MenuAction(delegate
			{
				GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataMaps));
			})).addItem("NPC templates", new MenuAction(delegate
			{
				GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataNPCTemplates));
			}))
				.addItem("Monster templates", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataMonsterTemplates));
				}))
				.addItem("Item templates", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataItemTemplates));
				}))
				.addItem("ItemOption templates", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataItemOptionTemplates));
				}))
				.addItem("SkillOption templates", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataSkillOptionTemplates));
				}))
				.addItem("NClasses", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataNClasses));
				}))
				.addItem("Parts", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataParts));
				}))
				.addItem("EffectCharPaints", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataEffectCharPaints));
				}))
				.addItem("Darts", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataDarts));
				}))
				.addItem("ArrowPaints", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataArrowPaints));
				}))
				.addItem("SkillPaints", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(new Func<bool, object>(GameData.GetDataSkillPaints));
				}))
				.addItem("All types", new MenuAction(delegate
				{
					GameData.WriteJSONToClipboard(delegate(bool _)
					{
						Service.gI().updateMap();
						Thread.Sleep(250);
						Service.gI().updateItem();
						Thread.Sleep(250);
						Service.gI().updateSkill();
						Thread.Sleep(250);
						Service.gI().updateData();
						Thread.Sleep(250);
						return new
						{
							maps = GameData.GetDataMaps(false),
							npcTemplates = GameData.GetDataNPCTemplates(false),
							monsterTemplates = GameData.GetDataMonsterTemplates(false),
							itemTemplates = GameData.GetDataItemTemplates(false),
							itemOptionTemplates = GameData.GetDataItemOptionTemplates(false),
							skillOptionTemplates = GameData.GetDataSkillOptionTemplates(false),
							nClasses = GameData.GetDataNClasses(false),
							parts = GameData.GetDataParts(false),
							effectCharPaints = GameData.GetDataEffectCharPaints(false),
							darts = GameData.GetDataDarts(false),
							arrowPaints = GameData.GetDataArrowPaints(false),
							skillPaints = GameData.GetDataSkillPaints(false)
						};
					});
				}))
				.start();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000B651F File Offset: 0x000B471F
		private static void WriteJSONToClipboard(Func<bool, object> getData)
		{
			new Thread(delegate
			{
				object data = getData(true);
				MainThreadDispatcher.Dispatch(delegate
				{
					GUIUtility.systemCopyBuffer = JsonConvert.SerializeObject(data, Formatting.Indented);
					GameCanvas.startOKDlg("JSON data has been copied to the clipboard!");
				});
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000B654C File Offset: 0x000B474C
		private static Dictionary<int, string> GetDataMaps(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateMap();
				Thread.Sleep(1000);
			}
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				dictionary.Add(i, TileMap.mapNames[i]);
			}
			return dictionary;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000B6597 File Offset: 0x000B4797
		private static NpcTemplate[] GetDataNPCTemplates(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateMap();
				Thread.Sleep(1000);
			}
			return Npc.arrNpcTemplate;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000B65B5 File Offset: 0x000B47B5
		private static MobTemplate[] GetDataMonsterTemplates(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateMap();
				Thread.Sleep(1000);
			}
			return Mob.arrMobTemplate;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000B65D4 File Offset: 0x000B47D4
		private static ItemTemplate[] GetDataItemTemplates(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateItem();
				Thread.Sleep(1000);
			}
			Hashtable h = ItemTemplates.itemTemplates.h;
			ItemTemplate[] array = new ItemTemplate[h.Count];
			h.Values.CopyTo(array, 0);
			return array.OrderBy<ItemTemplate, short>((ItemTemplate i) => i.id).ToArray<ItemTemplate>();
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000B6644 File Offset: 0x000B4844
		private static ItemOptionTemplate[] GetDataItemOptionTemplates(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateItem();
				Thread.Sleep(1000);
			}
			return GameScr.gI().iOptionTemplates;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000B6667 File Offset: 0x000B4867
		private static SkillOptionTemplate[] GetDataSkillOptionTemplates(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateSkill();
				Thread.Sleep(1000);
			}
			return GameScr.gI().sOptionTemplates;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000B668C File Offset: 0x000B488C
		private static NClass[] GetDataNClasses(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateSkill();
				Thread.Sleep(1000);
			}
			NClass[] array = new NClass[GameScr.nClasss.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = GameScr.nClasss[i];
				for (int j = 0; j < array[i].skillTemplates.Length; j++)
				{
					for (int k = 0; k < array[i].skillTemplates[j].skills.Length; k++)
					{
						array[i].skillTemplates[j].skills[k].template = null;
					}
				}
			}
			return array;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000B671E File Offset: 0x000B491E
		private static Part[] GetDataParts(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateData();
				Thread.Sleep(1000);
			}
			return GameScr.parts;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000B673C File Offset: 0x000B493C
		private static EffectCharPaint[] GetDataEffectCharPaints(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateData();
				Thread.Sleep(1000);
			}
			return GameScr.efs;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000B675A File Offset: 0x000B495A
		private static DartInfo[] GetDataDarts(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateData();
				Thread.Sleep(1000);
			}
			return GameScr.darts;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000B6778 File Offset: 0x000B4978
		private static Arrowpaint[] GetDataArrowPaints(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateData();
				Thread.Sleep(1000);
			}
			return GameScr.arrs;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000B6796 File Offset: 0x000B4996
		private static SkillPaint[] GetDataSkillPaints(bool isUpdate = true)
		{
			if (isUpdate)
			{
				Service.gI().updateData();
				Thread.Sleep(1000);
			}
			return GameScr.sks;
		}
	}
}
