using System;
using System.IO;
using System.Linq;
using System.Text;
using Mod.Constants;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.CommandMod.Hotkey;
using Mod.ModHelper.Menu;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000EE RID: 238
	internal static class Utils
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000A0BD6 File Offset: 0x0009EDD6
		internal static string PersistentDataPath
		{
			get
			{
				return Utils.persistentDataPath;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000A0BDD File Offset: 0x0009EDDD
		internal static bool IsOpenedByExternalAccountManager
		{
			get
			{
				return Utils.isOpenedByExternalAccountManager;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000A0BE4 File Offset: 0x0009EDE4
		internal static bool IsAndroidBuild()
		{
			return Application.platform == RuntimePlatform.Android;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000A0BEF File Offset: 0x0009EDEF
		internal static bool IsLinuxBuild()
		{
			return Application.platform == RuntimePlatform.LinuxPlayer;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000A0BFA File Offset: 0x0009EDFA
		internal static bool IsWindowsBuild()
		{
			return Application.platform == RuntimePlatform.WindowsPlayer;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000A0C04 File Offset: 0x0009EE04
		internal static bool IsEditor()
		{
			return Application.isEditor;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000A0C0B File Offset: 0x0009EE0B
		internal static bool IsMobile()
		{
			return Utils.IsAndroidBuild() || Application.platform == RuntimePlatform.IPhonePlayer;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000A0C1E File Offset: 0x0009EE1E
		internal static bool IsPC()
		{
			return !Utils.IsMobile();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000A0C28 File Offset: 0x0009EE28
		internal static void CheckBackButtonPress()
		{
			if (GameCanvas.panel != null || GameCanvas.panel2 != null)
			{
				if (GameCanvas.panel != null && GameCanvas.panel.isShow)
				{
					GameCanvas.panel.hide();
					return;
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.hide();
					return;
				}
			}
			if (InfoDlg.isShow)
			{
				return;
			}
			if (GameCanvas.currentDialog != null && GameCanvas.currentDialog is MsgDlg)
			{
				GameCanvas.endDlg();
				return;
			}
			if (ChatTextField.gI().isShow)
			{
				ChatTextField.gI().close();
				return;
			}
			if (GameCanvas.menu.showMenu)
			{
				GameCanvas.menu.closeMenu();
				return;
			}
			GameCanvas.checkBackButton();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000A0CD3 File Offset: 0x0009EED3
		internal static MyVector getMyVectorMe()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz());
			return myVector;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000A0CE5 File Offset: 0x0009EEE5
		internal static bool canBuffMe(out Skill skillBuff)
		{
			skillBuff = global::Char.myCharz().getSkill(new SkillTemplate
			{
				id = Utils.ID_SKILL_BUFF
			});
			return skillBuff != null;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000A0D0C File Offset: 0x0009EF0C
		internal static string getTextPopup(PopUp popUp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < popUp.says.Length; i++)
			{
				stringBuilder.Append(popUp.says[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000A0D58 File Offset: 0x0009EF58
		internal static bool isUsingTDLT()
		{
			return ItemTime.isExistItem((int)Utils.ID_ICON_ITEM_TDLT);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000A0D64 File Offset: 0x0009EF64
		internal static sbyte getIndexItemBag(params short[] templatesId)
		{
			global::Char @char = global::Char.myCharz();
			int num = @char.arrItemBag.Length;
			sbyte b = 0;
			while ((int)b < num)
			{
				Item item = @char.arrItemBag[(int)b];
				if (item != null && templatesId.Contains(item.template.id))
				{
					return b;
				}
				b += 1;
			}
			return -1;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000A0DAF File Offset: 0x0009EFAF
		internal static void teleToNpc(Npc npc)
		{
			Utils.TeleportMyChar(npc.cx, npc.ySd - npc.ySd % 24);
			global::Char.myCharz().npcFocus = npc;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000A0DD7 File Offset: 0x0009EFD7
		internal static void requestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000A0DF8 File Offset: 0x0009EFF8
		internal static void setWaypointChangeMap(Waypoint waypoint)
		{
			int mapID = TileMap.mapID;
			string textPopup = Utils.getTextPopup(waypoint.popup);
			if (mapID == 27 && textPopup == TileMap.mapNames[53])
			{
				return;
			}
			if ((mapID == 70 && textPopup == TileMap.mapNames[69]) || (mapID == 73 && textPopup == TileMap.mapNames[67]) || (mapID == 110 && textPopup == TileMap.mapNames[106]))
			{
				Utils.waypointLeft = waypoint;
				return;
			}
			if (((mapID == 106 || mapID == 107) && textPopup == TileMap.mapNames[110]) || ((mapID == 105 || mapID == 108) && textPopup == TileMap.mapNames[109]) || (mapID == 109 && textPopup == TileMap.mapNames[105]))
			{
				Utils.waypointMiddle = waypoint;
				return;
			}
			if (mapID == 70 && textPopup == TileMap.mapNames[71])
			{
				Utils.waypointRight = waypoint;
				return;
			}
			if (waypoint.maxX < 60)
			{
				Utils.waypointLeft = waypoint;
				return;
			}
			if ((int)waypoint.minX > TileMap.pxw - 60)
			{
				Utils.waypointRight = waypoint;
				return;
			}
			Utils.waypointMiddle = waypoint;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000A0F10 File Offset: 0x0009F110
		internal static void UpdateWaypointChangeMap()
		{
			Utils.waypointLeft = (Utils.waypointMiddle = (Utils.waypointRight = null));
			if (TileMap.mapID == 46)
			{
				Utils.waypointRight = new Waypoint(570, 576, 570, 576, true, false, TileMap.mapNames[47]);
			}
			int num = TileMap.vGo.size();
			if (num == 0 && TileMap.mapID == 45)
			{
				Utils.waypointMiddle = new Waypoint(570, 576, 570, 576, true, false, TileMap.mapNames[46]);
			}
			for (int i = 0; i < num; i++)
			{
				Utils.setWaypointChangeMap((Waypoint)TileMap.vGo.elementAt(i));
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000A0FC0 File Offset: 0x0009F1C0
		[ChatCommand("tdc")]
		[ChatCommand("cspeed")]
		internal static void setSpeedRun(int speed)
		{
			Utils.myCharSpeed = speed;
			GameScr.info1.addInfo("Tốc độ chạy: " + speed.ToString(), 0);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
		[ChatCommand("speed")]
		internal static void setSpeedGame(float speed)
		{
			Time.timeScale = speed;
			GameScr.info1.addInfo("Tốc độ game: " + speed.ToString(), 0);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000A1008 File Offset: 0x0009F208
		[ChatCommand("hsme")]
		[ChatCommand("buffme")]
		[HotkeyCommand('b')]
		internal static void buffMe()
		{
			Skill skill;
			if (!Utils.canBuffMe(out skill))
			{
				GameScr.info1.addInfo("Không tìm thấy kỹ năng Trị thương", 0);
				return;
			}
			Service.gI().selectSkill((int)Utils.ID_SKILL_BUFF);
			Service.gI().sendPlayerAttack(new MyVector(), Utils.getMyVectorMe(), -1);
			Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
			skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000A107C File Offset: 0x0009F27C
		internal static void TeleportMyChar(int x, int y)
		{
			global::Char.myCharz().currentMovePoint = null;
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			if (Utils.isUsingTDLT())
			{
				return;
			}
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000A1100 File Offset: 0x0009F300
		[HotkeyCommand('n')]
		internal static void showMenuTeleNpc()
		{
			if (GameScr.vNpc.size() == 0)
			{
				GameScr.info1.addInfo("Không có NPC nào", 0);
				return;
			}
			new MenuBuilder().map<Npc>(GameScr.vNpc, (Npc npc) => new MenuItem(string.IsNullOrEmpty(npc.template.name.Trim()) ? "(no name)" : npc.template.name, new MenuAction(delegate
			{
				Utils.teleToNpc(npc);
			}))).start();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000A1160 File Offset: 0x0009F360
		[ChatCommand("csb")]
		[HotkeyCommand('c')]
		internal static void useCapsule()
		{
			sbyte indexItemBag = Utils.getIndexItemBag(new short[] { 193, 194 });
			if (indexItemBag == -1)
			{
				GameScr.info1.addInfo("Không tìm thấy capsule", 0);
				return;
			}
			Service.gI().useItem(0, 1, indexItemBag, -1);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000A11AC File Offset: 0x0009F3AC
		[ChatCommand("bt")]
		[HotkeyCommand('f')]
		internal static void usePorata()
		{
			sbyte indexItemBag = Utils.getIndexItemBag(new short[] { 921, 454 });
			if (indexItemBag == -1)
			{
				GameScr.info1.addInfo("Không tìm thấy bông tai", 0);
				return;
			}
			Service.gI().useItem(0, 1, indexItemBag, -1);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000A11F8 File Offset: 0x0009F3F8
		[ChatCommand("skey")]
		internal static void syncKey(int channel)
		{
			Utils.channelSyncKey = channel;
			if (channel == -1)
			{
				GameScr.info1.addInfo("Đã tắt đồng bộ phím", 0);
				return;
			}
			GameScr.info1.addInfo(string.Format("Đồng bộ phím với kênh {0}", channel), 0);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000A1230 File Offset: 0x0009F430
		[HotkeyCommand('j')]
		internal static void ChangeMapLeft()
		{
			if (Utils.IsMeInNRDMap() || Utils.waypointLeft == null)
			{
				Utils.TeleportMyChar(60);
				return;
			}
			Utils.ChangeMap(Utils.waypointLeft);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000A1254 File Offset: 0x0009F454
		[HotkeyCommand('k')]
		internal static void ChangeMapMiddle()
		{
			if (Utils.IsMeInNRDMap())
			{
				if (global::Char.myCharz().bag >= 0 && ClanImage.idImages.containsKey(global::Char.myCharz().bag.ToString()))
				{
					ClanImage clanImage = (ClanImage)ClanImage.idImages.get(global::Char.myCharz().bag.ToString());
					if (clanImage.idImage != null)
					{
						for (int i = 0; i < clanImage.idImage.Length; i++)
						{
							if (clanImage.idImage[i] == 2322)
							{
								for (int j = 0; j < GameScr.vNpc.size(); j++)
								{
									Npc npc = (Npc)GameScr.vNpc.elementAt(j);
									if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
									{
										global::Char.myCharz().npcFocus = npc;
										Utils.TeleportMyChar(npc.cx, npc.cy - 3);
										return;
									}
								}
							}
						}
					}
				}
				for (int k = 0; k < GameScr.vItemMap.size(); k++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
					if (itemMap != null && itemMap.IsNRD())
					{
						global::Char.myCharz().itemFocus = itemMap;
						Utils.TeleportMyChar(itemMap.x, itemMap.y);
						return;
					}
				}
				return;
			}
			if (Utils.waypointMiddle == null)
			{
				Utils.TeleportMyChar(TileMap.pxw / 2);
				return;
			}
			Utils.ChangeMap(Utils.waypointMiddle);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000A13C3 File Offset: 0x0009F5C3
		[HotkeyCommand('l')]
		internal static void ChangeMapRight()
		{
			if (Utils.IsMeInNRDMap() || Utils.waypointRight == null)
			{
				Utils.TeleportMyChar(TileMap.pxw - 60);
				return;
			}
			Utils.ChangeMap(Utils.waypointRight);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000A13EC File Offset: 0x0009F5EC
		[HotkeyCommand('g')]
		internal static void sendGiaoDichToCharFocusing()
		{
			global::Char charFocus = global::Char.myCharz().charFocus;
			if (charFocus == null)
			{
				GameScr.info1.addInfo("Trỏ vào nhân vật để giao dịch", 0);
				return;
			}
			Service.gI().giaodich(0, charFocus.charID, -1, -1);
			GameScr.info1.addInfo("Đã gửi lời mời giao dịch đến " + charFocus.cName, 0);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000A1446 File Offset: 0x0009F646
		[ChatCommand("k")]
		internal static void changeZone(int zone)
		{
			Service.gI().requestChangeZone(zone, -1);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000A1454 File Offset: 0x0009F654
		[HotkeyCommand('m')]
		internal static void menuZone()
		{
			Service.gI().openUIZone();
			GameCanvas.panel.setTypeZone();
			GameCanvas.panel.show();
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000A1474 File Offset: 0x0009F674
		internal static void ChangeMap(Waypoint waypoint)
		{
			if (waypoint != null)
			{
				Utils.TeleportMyChar(waypoint.GetX(), waypoint.GetY());
				Utils.requestChangeMap(waypoint);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000A1490 File Offset: 0x0009F690
		internal static bool IsMeInNRDMap()
		{
			return TileMap.mapID >= 85 && TileMap.mapID <= 91;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000A14AC File Offset: 0x0009F6AC
		internal static long LoadDataLong(string name, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Open);
			byte[] array = new byte[8];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000A1500 File Offset: 0x0009F700
		internal static bool LoadDataBool(string name, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Open);
			byte[] array = new byte[1];
			fileStream.Read(array, 0, 1);
			fileStream.Close();
			return array[0] == 1;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000A1550 File Offset: 0x0009F750
		internal static string LoadDataString(string name, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			string text2 = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();
			return text2;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000A159C File Offset: 0x0009F79C
		internal static double LoadDataDouble(string name, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Open);
			byte[] array = new byte[8];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return BitConverter.ToDouble(array, 0);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000A15F0 File Offset: 0x0009F7F0
		internal static bool TryLoadDataLong(string name, out long value, bool isCommon = true)
		{
			value = 0L;
			try
			{
				value = Utils.LoadDataLong(name, isCommon);
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return false;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000A1628 File Offset: 0x0009F828
		internal static bool TryLoadDataBool(string name, out bool value, bool isCommon = true)
		{
			value = false;
			try
			{
				value = Utils.LoadDataBool(name, isCommon);
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return false;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000A1660 File Offset: 0x0009F860
		internal static bool TryLoadDataString(string name, out string value, bool isCommon = true)
		{
			value = null;
			try
			{
				value = Utils.LoadDataString(name, isCommon);
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return false;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000A1698 File Offset: 0x0009F898
		internal static bool TryLoadDataDouble(string name, out double value, bool isCommon = true)
		{
			value = 0.0;
			try
			{
				value = Utils.LoadDataDouble(name, isCommon);
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return false;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000A16D8 File Offset: 0x0009F8D8
		internal static void SaveData(string name, long value, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Create);
			fileStream.Write(BitConverter.GetBytes(value), 0, 8);
			fileStream.Flush();
			fileStream.Close();
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000A1734 File Offset: 0x0009F934
		internal static void SaveData(string name, bool status, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Create);
			fileStream.Write(new byte[] { status ? 1 : 0 }, 0, 1);
			fileStream.Flush();
			fileStream.Close();
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000A179C File Offset: 0x0009F99C
		internal static void SaveData(string name, string data, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Create);
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			fileStream.Write(bytes, 0, bytes.Length);
			fileStream.Flush();
			fileStream.Close();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000A1800 File Offset: 0x0009FA00
		internal static void SaveData(string name, double value, bool isCommon = true)
		{
			string text = Utils.dataPath;
			if (!isCommon)
			{
				text = Path.Combine(Rms.GetiPhoneDocumentsPath(), "ModData");
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			FileStream fileStream = new FileStream(Path.Combine(text, name), FileMode.Create);
			fileStream.Write(BitConverter.GetBytes(value), 0, 8);
			fileStream.Flush();
			fileStream.Close();
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000A185B File Offset: 0x0009FA5B
		internal static void TeleportMyChar(IMapObject obj)
		{
			Utils.TeleportMyChar(obj.getX(), obj.getY());
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000A186E File Offset: 0x0009FA6E
		internal static void TeleportMyChar(int x)
		{
			Utils.TeleportMyChar(x, Utils.GetYGround(x));
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000A187C File Offset: 0x0009FA7C
		internal static int getWidth(GUIStyle gUIStyle, string s)
		{
			return (int)(gUIStyle.CalcSize(new GUIContent(s)).x * 1.025f / (float)mGraphics.zoomLevel);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000A189D File Offset: 0x0009FA9D
		internal static int getHeight(GUIStyle gUIStyle, string content)
		{
			return (int)gUIStyle.CalcSize(new GUIContent(content)).y / mGraphics.zoomLevel;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000A18B8 File Offset: 0x0009FAB8
		internal static int GetYGround(int x)
		{
			int num = 50;
			int i = 0;
			while (i < 30)
			{
				num += 24;
				if (TileMap.tileTypeAt(x, num, 2))
				{
					if (num % 24 != 0)
					{
						num -= num % 24;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return num;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000A18F4 File Offset: 0x0009FAF4
		internal static int Distance(IMapObject mapObject1, IMapObject mapObject2)
		{
			return Res.distance(mapObject1.getX(), mapObject1.getY(), mapObject2.getX(), mapObject2.getY());
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000A1913 File Offset: 0x0009FB13
		[HotkeyCommand('w')]
		internal static void KhinhCong()
		{
			global::Char.myCharz().cy -= 50;
			Service.gI().charMove();
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000A1932 File Offset: 0x0009FB32
		[HotkeyCommand('s')]
		internal static void DonTho()
		{
			global::Char.myCharz().cy += 50;
			Service.gI().charMove();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000A1951 File Offset: 0x0009FB51
		[HotkeyCommand('a')]
		internal static void DichTrai()
		{
			global::Char.myCharz().cx -= 50;
			Service.gI().charMove();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000A1970 File Offset: 0x0009FB70
		[HotkeyCommand('d')]
		internal static void DichPhai()
		{
			global::Char.myCharz().cx += 50;
			Service.gI().charMove();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000A198F File Offset: 0x0009FB8F
		internal static short getNRSDId()
		{
			if (Utils.IsMeInNRDMap())
			{
				return (short)(2400 - TileMap.mapID);
			}
			return 0;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000A19A8 File Offset: 0x0009FBA8
		internal static bool isMeWearingActivationSet(int idSet)
		{
			int num = 0;
			for (int i = 0; i < 5; i++)
			{
				Item item = global::Char.myCharz().arrItemBody[i];
				if (item == null)
				{
					return false;
				}
				if (item.itemOption == null)
				{
					return false;
				}
				for (int j = 0; j < item.itemOption.Length; j++)
				{
					if (item.itemOption[j].optionTemplate.id == idSet)
					{
						num++;
						break;
					}
				}
			}
			return num == 5;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000A1A11 File Offset: 0x0009FC11
		internal static bool isMeWearingTXHSet()
		{
			return global::Char.myCharz().cgender == 0 && Utils.isMeWearingActivationSet(127);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000A1A28 File Offset: 0x0009FC28
		internal static bool isMeWearingPikkoroDaimaoSet()
		{
			return global::Char.myCharz().cgender == 1 && Utils.isMeWearingActivationSet(132);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000A1A43 File Offset: 0x0009FC43
		internal static bool isMeWearingCadicSet()
		{
			return global::Char.myCharz().cgender == 2 && Utils.isMeWearingActivationSet(134);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000A1A5E File Offset: 0x0009FC5E
		internal static void DoDoubleClickToObj(IMapObject mapObject)
		{
			GameScr.gI().doDoubleClickToObj(mapObject);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000A1A6B File Offset: 0x0009FC6B
		internal static bool CanNextMap()
		{
			return !global::Char.isLoadingMap && !global::Char.ischangingMap && !Controller.isStopReadMessage;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000A1A88 File Offset: 0x0009FC88
		internal static bool HasStarOption(Item item, out uint star, out uint starE)
		{
			star = 0U;
			starE = 0U;
			bool flag = false;
			if (item.itemOption == null)
			{
				return flag;
			}
			if ((item.template.type <= ItemTemplateType.Shirt || item.template.type >= ItemTemplateType.Radar) && item.template.type != ItemTemplateType.TrainingSuite)
			{
				return flag;
			}
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				if (item.itemOption[i].optionTemplate.id == 102)
				{
					star = (starE = (uint)item.itemOption[i].param);
				}
				if (item.itemOption[i].optionTemplate.id == 107)
				{
					starE = (uint)item.itemOption[i].param;
				}
			}
			if (starE != 0U)
			{
				flag = true;
			}
			starE -= star;
			return flag;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000A1B4F File Offset: 0x0009FD4F
		internal static long GetLastTimePress()
		{
			return GameCanvas.lastTimePress;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000A1B58 File Offset: 0x0009FD58
		internal static int GetPetGender()
		{
			string name = global::Char.myPetz().arrPetSkill[0].template.name;
			if (name == GameScr.nClasss[0].skillTemplates[0].name)
			{
				return GameScr.nClasss[0].classId;
			}
			if (name == GameScr.nClasss[1].skillTemplates[0].name)
			{
				return GameScr.nClasss[1].classId;
			}
			if (name == GameScr.nClasss[2].skillTemplates[0].name)
			{
				return GameScr.nClasss[2].classId;
			}
			return 3;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000A1BF8 File Offset: 0x0009FDF8
		internal static string TrimUntilFit(string str, GUIStyle style, int width)
		{
			if ((int)((float)Utils.getWidth(style, str) / 1.025f) > width)
			{
				while (Utils.getWidth(style, str + "...") > width)
				{
					str = str.Remove(str.Length - 1, 1);
				}
				str = str.Trim() + "...";
			}
			return str;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000A1C54 File Offset: 0x0009FE54
		internal static bool HasActivateOption(Item item)
		{
			if (item.itemOption == null)
			{
				return false;
			}
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				if (item.itemOption[i].optionTemplate.id >= 127 && item.itemOption[i].optionTemplate.id <= 144)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000A1CB0 File Offset: 0x0009FEB0
		internal static global::Char FindCharInMap(string name)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char.GetNameWithoutClanTag(false) == name)
				{
					return @char;
				}
			}
			return null;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000A1CF5 File Offset: 0x0009FEF5
		internal static bool IsMyCharHome()
		{
			return TileMap.mapID == global::Char.myCharz().cgender + 21;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000A1D0C File Offset: 0x0009FF0C
		internal static string FormatWithSIPrefix(double number)
		{
			string[] array = new string[] { "", "k", "M", "B", "T" };
			int num = Math.Max(0, Math.Min((int)Math.Floor(Math.Log10(Math.Abs(number)) / 3.0), array.Length - 1));
			double num2 = number * Math.Pow(1000.0, (double)(-(double)num));
			return string.Format("{0:0.##}{1}", num2, array[num]);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000A1D9C File Offset: 0x0009FF9C
		internal static void ResetTextField(ChatTextField chatTextField)
		{
			if (chatTextField == null)
			{
				return;
			}
			chatTextField.left = new Command(mResources.OK, chatTextField, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
			chatTextField.right = new Command(mResources.DELETE, chatTextField, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
			chatTextField.center = null;
			chatTextField.w = chatTextField.tfChat.width + 20;
			chatTextField.h = chatTextField.tfChat.height + 26;
			chatTextField.x = GameCanvas.w / 2 - chatTextField.w / 2;
			chatTextField.tfChat.y = GameCanvas.h - 40 - chatTextField.tfChat.height;
			chatTextField.y = chatTextField.tfChat.y - 18;
			if (Main.isPC && chatTextField.w > 320)
			{
				chatTextField.w = 320;
			}
			chatTextField.left.x = chatTextField.x;
			chatTextField.right.x = chatTextField.x + chatTextField.w - 68;
			if (GameCanvas.isTouch)
			{
				chatTextField.y -= 15;
				chatTextField.h += 30;
				chatTextField.left.x = GameCanvas.w / 2 - 68 - 5;
				chatTextField.right.x = GameCanvas.w / 2 + 5;
				chatTextField.left.y = GameCanvas.h - 30;
				chatTextField.right.y = GameCanvas.h - 30;
			}
			chatTextField.yBegin = chatTextField.tfChat.y;
			chatTextField.yUp = GameCanvas.h / 2 - 2 * chatTextField.tfChat.height;
			if (Main.isWindowsPhone)
			{
				chatTextField.tfChat.showSubTextField = false;
			}
			if (Main.isIPhone)
			{
				chatTextField.tfChat.isPaintMouse = false;
			}
			chatTextField.tfChat.name = "chat";
			if (Main.isWindowsPhone)
			{
				chatTextField.tfChat.strInfo = chatTextField.tfChat.name;
			}
			chatTextField.tfChat.width = GameCanvas.w - 6;
			if (Main.isPC && chatTextField.tfChat.width > 250)
			{
				chatTextField.tfChat.width = 250;
			}
			chatTextField.tfChat.height = mScreen.ITEM_HEIGHT + 2;
			chatTextField.tfChat.x = GameCanvas.w / 2 - chatTextField.tfChat.width / 2;
			chatTextField.tfChat.isFocus = true;
			chatTextField.tfChat.setMaxTextLenght(80);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000A203C File Offset: 0x000A023C
		internal static string GetRootDataPath()
		{
			string text = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Data");
			if (Utils.IsEditor() || Utils.IsAndroidBuild())
			{
				text = Utils.PersistentDataPath;
			}
			return text;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000A2073 File Offset: 0x000A0273
		internal static double Distance(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		// Token: 0x040014C9 RID: 5321
		private static string persistentDataPath = Application.persistentDataPath;

		// Token: 0x040014CA RID: 5322
		internal static readonly string dataPath = Path.Combine(Utils.GetRootDataPath(), "CommonModData");

		// Token: 0x040014CB RID: 5323
		internal static readonly string PathAutoChat = Path.Combine(Utils.dataPath, "autochat.txt");

		// Token: 0x040014CC RID: 5324
		internal static readonly string PathChatCommand = Path.Combine(Utils.dataPath, "chatCommands.json");

		// Token: 0x040014CD RID: 5325
		internal static readonly string PathChatHistory = Path.Combine(Utils.dataPath, "chat.txt");

		// Token: 0x040014CE RID: 5326
		internal static readonly string PathHotkeyCommand = Path.Combine(Utils.dataPath, "hotkeyCommands.json");

		// Token: 0x040014CF RID: 5327
		internal static readonly sbyte ID_SKILL_BUFF = 7;

		// Token: 0x040014D0 RID: 5328
		internal static readonly short ID_ICON_ITEM_TDLT = 4387;

		// Token: 0x040014D1 RID: 5329
		internal static readonly short ID_NPC_MOD_FACE = 7333;

		// Token: 0x040014D2 RID: 5330
		internal static string status = "Đã kết nối";

		// Token: 0x040014D3 RID: 5331
		internal static int myCharSpeed = 8;

		// Token: 0x040014D4 RID: 5332
		internal static Waypoint waypointLeft;

		// Token: 0x040014D5 RID: 5333
		internal static Waypoint waypointMiddle;

		// Token: 0x040014D6 RID: 5334
		internal static Waypoint waypointRight;

		// Token: 0x040014D7 RID: 5335
		internal static string username = "";

		// Token: 0x040014D8 RID: 5336
		internal static string password = "";

		// Token: 0x040014D9 RID: 5337
		internal static JObject server = null;

		// Token: 0x040014DA RID: 5338
		internal static JObject sizeData = null;

		// Token: 0x040014DB RID: 5339
		internal static int channelSyncKey = -1;

		// Token: 0x040014DC RID: 5340
		internal static global::System.Random random = new global::System.Random();

		// Token: 0x040014DD RID: 5341
		private static bool isOpenedByExternalAccountManager;
	}
}
