using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod.CustomPanel;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.CommandMod.Hotkey;
using Mod.ModHelper.Menu;
using Mod.R;
using UnityEngine;

namespace Mod.TeleportMenu
{
	// Token: 0x0200010B RID: 267
	internal class TeleportMenuMain
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x000A4744 File Offset: 0x000A2944
		[ChatCommand("tele")]
		[HotkeyCommand('z')]
		internal static void ShowMenu()
		{
			MenuBuilder menuBuilder = new MenuBuilder();
			menuBuilder.addItem(TeleportMenuMain.listTeleportChars.Count > 0, Strings.teleportMenuOpenSavedCharList, new MenuAction(delegate
			{
				TeleportMenuMain.ShowListChars(TeleportMenuMain.TeleportStatus.TeleportTo);
			}));
			global::Char charFocus = global::Char.myCharz().charFocus;
			global::Char @char;
			if (charFocus != null && charFocus.IsNormalChar(false, false))
			{
				@char = charFocus;
			}
			else
			{
				@char = global::Char.myCharz().ClosestChar(70, true);
			}
			if (@char != null)
			{
				TeleportChar teleportChar2 = new TeleportChar(@char);
				menuBuilder.addItem(!TeleportMenuMain.listTeleportChars.Contains(teleportChar2), string.Format("{0}\n{1}\n[{2}]", Strings.add, teleportChar2.Name, teleportChar2.ID), new MenuAction(delegate
				{
					TeleportMenuMain.listTeleportChars.Insert(0, teleportChar2);
					TeleportMenuMain.SaveData();
					GameScr.info1.addInfo(string.Format(Strings.teleportMenuCharacterAdded, teleportChar2) + "!", 0);
				}));
			}
			if (charFocus != null && charFocus.IsNormalChar(false, false))
			{
				TeleportChar teleportChar = new TeleportChar(charFocus);
				menuBuilder.addItem(TeleportMenuMain.listTeleportChars.Contains(teleportChar) && ((TeleportMenuMain.isAutoTeleportTo && TeleportMenuMain.charAutoTeleportTo != teleportChar) || !TeleportMenuMain.isAutoTeleportTo), string.Format("{0}\n{1}\n[{2}]", Strings.delete, teleportChar.Name, teleportChar.ID), new MenuAction(delegate
				{
					if (!TeleportMenuMain.isAutoTeleportTo || teleportChar == TeleportMenuMain.charAutoTeleportTo)
					{
						GameScr.info1.addInfo(Strings.teleportMenuCantRemoveTargetChar + "!", 0);
						return;
					}
					TeleportMenuMain.listTeleportChars.Remove(teleportChar);
					TeleportMenuMain.SaveData();
					GameScr.info1.addInfo(string.Format(Strings.teleportMenuCharacterRemoved, teleportChar) + "!", 0);
				}));
			}
			menuBuilder.addItem(TeleportMenuMain.listTeleportChars.Count > 0, TeleportMenuMain.isAutoTeleportTo ? Strings.teleportMenuStopTeleporting : Strings.teleportMenuSelectTarget, new MenuAction(delegate
			{
				if (!TeleportMenuMain.isAutoTeleportTo)
				{
					TeleportMenuMain.ShowListChars(TeleportMenuMain.TeleportStatus.AutoTeleportTo);
					return;
				}
				TeleportMenuMain.isAutoTeleportTo = false;
				GameScr.info1.addInfo(string.Format(Strings.teleportMenuStopTeleportToTarget, TeleportMenuMain.charAutoTeleportTo) + "!", 0);
				TeleportMenuMain.charAutoTeleportTo = null;
			})).addItem(Strings.teleportMenuAddCharacterByID, new MenuAction(delegate
			{
				ChatTextField.gI().strChat = "";
				ChatTextField.gI().tfChat.name = Strings.teleportMenuInputCharIDTextFieldHint;
				ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				ChatTextField.gI().startChat2(new TeleportMenuMain.TeleportMenuChatable(), Strings.teleportMenuInputCharIDTextFieldName);
			})).addItem(GameScr.vCharInMap.size() > 1, Strings.teleportMenuAddEveryoneInZone, new MenuAction(delegate
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (char2.IsNormalChar(false, false))
					{
						TeleportChar teleportChar3 = new TeleportChar(char2);
						if (!TeleportMenuMain.listTeleportChars.Contains(teleportChar3))
						{
							TeleportMenuMain.listTeleportChars.Add(teleportChar3);
						}
					}
				}
				TeleportMenuMain.SaveData();
				GameScr.info1.addInfo(Strings.teleportMenuEveryoneAdded + "!", 0);
			}));
			if (TeleportMenuMain.listTeleportChars.Count > 0)
			{
				menuBuilder.addItem(Strings.teleportMenuRemoveCharacter, new MenuAction(delegate
				{
					TeleportMenuMain.ShowListChars(TeleportMenuMain.TeleportStatus.Delete);
				})).addItem(Strings.deleteAll, new MenuAction(delegate
				{
					for (int j = TeleportMenuMain.listTeleportChars.Count - 1; j >= 0; j--)
					{
						if (TeleportMenuMain.listTeleportChars[j] != TeleportMenuMain.charAutoTeleportTo)
						{
							TeleportMenuMain.listTeleportChars.RemoveAt(j);
						}
					}
					TeleportMenuMain.SaveData();
					GameScr.info1.addInfo(Strings.teleportMenuCleared + "!", 0);
				}));
			}
			menuBuilder.start();
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000A49F0 File Offset: 0x000A2BF0
		internal static void LoadData()
		{
			try
			{
				if (!TeleportMenuMain.isDataLoaded)
				{
					foreach (string text in Utils.LoadDataString(string.Format("teleport_list_{0}_{1}", GameMidlet.IP, GameMidlet.PORT), true).Split('|', StringSplitOptions.None))
					{
						try
						{
							if (!string.IsNullOrEmpty(text))
							{
								string[] array2 = text.Split(',', StringSplitOptions.None);
								TeleportChar teleportChar = new TeleportChar(array2[0], int.Parse(array2[1]), long.Parse(array2[2]));
								if (!TeleportMenuMain.listTeleportChars.Contains(teleportChar))
								{
									TeleportMenuMain.listTeleportChars.Add(teleportChar);
								}
							}
						}
						catch
						{
						}
					}
					TeleportMenuMain.isDataLoaded = true;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000A4AB8 File Offset: 0x000A2CB8
		internal static void SaveData()
		{
			string text = "";
			foreach (TeleportChar teleportChar in TeleportMenuMain.listTeleportChars)
			{
				text = string.Concat(new string[]
				{
					text,
					teleportChar.Name,
					",",
					teleportChar.ID.ToString(),
					",",
					teleportChar.LastTimeTeleportTo.ToString(),
					"|"
				});
			}
			Utils.SaveData(string.Format("teleport_list_{0}_{1}", GameMidlet.IP, GameMidlet.PORT), text, true);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000A4B80 File Offset: 0x000A2D80
		private static void ShowListChars(TeleportMenuMain.TeleportStatus status)
		{
			MenuBuilder menuBuilder = new MenuBuilder();
			int num = 0;
			int num2 = TeleportMenuMain.listTeleportChars.Count;
			while (menuBuilder.menuItems.Count < ((num2 < 5) ? num2 : 5))
			{
				TeleportChar teleportChar = TeleportMenuMain.listTeleportChars.ElementAt<TeleportChar>(num);
				num++;
				if (status == TeleportMenuMain.TeleportStatus.Delete)
				{
					if (teleportChar == TeleportMenuMain.charAutoTeleportTo)
					{
						num2--;
					}
					else
					{
						menuBuilder.addItem(string.Format("{0}\n[{1}]", teleportChar.Name, teleportChar.ID), new MenuAction(delegate
						{
							TeleportMenuMain.listTeleportChars.Remove(teleportChar);
							TeleportMenuMain.SaveData();
							GameScr.info1.addInfo(string.Format(Strings.teleportMenuCharacterRemoved, teleportChar) + "!", 0);
						}));
					}
				}
				else
				{
					menuBuilder.addItem(string.Format("{0}\n[{1}]", teleportChar.Name, teleportChar.ID), new MenuAction(delegate
					{
						GameScr.info1.addInfo(string.Format(Strings.teleportMenuTeleportingToCharacter, teleportChar) + "...", 0);
						TeleportMenuMain.TeleportToPlayer(teleportChar.ID, true);
						teleportChar.LastTimeTeleportTo = mSystem.currentTimeMillis();
					}));
				}
			}
			menuBuilder.addItem(TeleportMenuMain.listTeleportChars.Count > 5, Strings.more + "...", new MenuAction(delegate
			{
				TeleportMenuMain.currentTeleportStatus = status;
				TeleportMenuMain.showTeleportCharListPanel();
			}));
			menuBuilder.start();
			if (menuBuilder.menuItems.Count <= 0)
			{
				GameScr.info1.addInfo(Strings.teleportMenuNoRemovableChar + "!", 0);
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000A4CEC File Offset: 0x000A2EEC
		private static void showTeleportCharListPanel()
		{
			TeleportMenuMain.SortList();
			CustomPanelMenu.Show(new CustomPanelMenuConfig
			{
				SetTabAction = new Action<Panel>(TeleportMenuMain.SetTabTeleportListPanel),
				DoFireItemAction = new Action<Panel>(TeleportMenuMain.DoFireTeleportListPanel),
				PaintTabHeaderAction = new Action<Panel, mGraphics>(TeleportMenuMain.PaintTabHeader),
				PaintAction = new Action<Panel, mGraphics>(TeleportMenuMain.PaintTeleportListPanel)
			}, null);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000A4D54 File Offset: 0x000A2F54
		internal static void Update()
		{
			if ((float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
			{
				foreach (TeleportChar teleportChar in TeleportMenuMain.listTeleportChars.Where<TeleportChar>((TeleportChar tC) => tC.Name == "no name"))
				{
					global::Char @char = GameScr.findCharInMap(teleportChar.ID);
					if (@char != null)
					{
						teleportChar.Name = @char.cName;
					}
				}
			}
			if (TeleportMenuMain.isAutoTeleportTo)
			{
				bool flag = false;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					if (((global::Char)GameScr.vCharInMap.elementAt(i)).charID == TeleportMenuMain.charAutoTeleportTo.ID)
					{
						flag = true;
						break;
					}
				}
				if ((float)(GameCanvas.gameTick % 30) * Time.timeScale == 0f && flag && TeleportMenuMain.previousDisguiseId != -1 && !TeleportMenuMain.isChangeDisguise)
				{
					new Thread(delegate
					{
						TeleportMenuMain.isChangeDisguise = true;
						for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
						{
							Item item = global::Char.myCharz().arrItemBag[j];
							if (item != null && (int)item.template.id == TeleportMenuMain.previousDisguiseId)
							{
								do
								{
									Service.gI().getItem(4, (sbyte)j);
									Thread.Sleep(500);
								}
								while ((int)global::Char.myCharz().arrItemBody[5].template.id != TeleportMenuMain.previousDisguiseId);
								break;
							}
						}
						TeleportMenuMain.previousDisguiseId = -1;
					}).Start();
				}
				if (!flag && TeleportMenuMain.isAutoTeleportTo && mSystem.currentTimeMillis() - TeleportMenuMain.lastTimeAutoTeleportTo >= 2000L)
				{
					TeleportMenuMain.lastTimeAutoTeleportTo = mSystem.currentTimeMillis();
					if (TeleportMenuMain.previousDisguiseId == -1)
					{
						new Thread(delegate
						{
							if (global::Char.myCharz().arrItemBody[5] == null || (global::Char.myCharz().arrItemBody[5] != null && (global::Char.myCharz().arrItemBody[5].template.id < 592 || global::Char.myCharz().arrItemBody[5].template.id > 594)))
							{
								if (global::Char.myCharz().arrItemBody[5] != null)
								{
									TeleportMenuMain.previousDisguiseId = (int)global::Char.myCharz().arrItemBody[5].template.id;
								}
								for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
								{
									Item item2 = global::Char.myCharz().arrItemBag[k];
									if (item2 != null && item2.template.id >= 592 && item2.template.id <= 594)
									{
										do
										{
											Service.gI().getItem(4, (sbyte)k);
											Thread.Sleep(250);
										}
										while (global::Char.myCharz().arrItemBody[5].template.id < 592 || global::Char.myCharz().arrItemBody[5].template.id > 594);
										break;
									}
								}
							}
							TeleportMenuMain.TeleportToPlayer(TeleportMenuMain.charAutoTeleportTo.ID, false);
						}).Start();
					}
					TeleportMenuMain.charAutoTeleportTo.LastTimeTeleportTo = mSystem.currentTimeMillis();
				}
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000A4EF4 File Offset: 0x000A30F4
		private static void SortList()
		{
			TeleportMenuMain.listTeleportChars_orderByName = TeleportMenuMain.listTeleportChars.OrderBy<TeleportChar, string>((TeleportChar tC) => tC.Name).ToList<TeleportChar>();
			TeleportMenuMain.listTeleportChars = TeleportMenuMain.listTeleportChars.OrderBy<TeleportChar, long>((TeleportChar tC) => -tC.LastTimeTeleportTo).ToList<TeleportChar>();
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000A4F67 File Offset: 0x000A3167
		internal static void SetTabTeleportListPanel(Panel panel)
		{
			SetTabPanelTemplates.setTabListTemplate(panel, new ICollection[] { TeleportMenuMain.listTeleportChars });
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000A4F80 File Offset: 0x000A3180
		internal static void DoFireTeleportListPanel(Panel panel)
		{
			TeleportMenuMain.listTeleportChars_orderByName = TeleportMenuMain.listTeleportChars.OrderBy<TeleportChar, string>((TeleportChar tC) => tC.Name).ToList<TeleportChar>();
			if (panel.selected < 0)
			{
				return;
			}
			string text = "";
			Action action = null;
			TeleportChar teleportChar = TeleportMenuMain.listTeleportChars_orderByName[panel.selected];
			switch (TeleportMenuMain.currentTeleportStatus)
			{
			case TeleportMenuMain.TeleportStatus.TeleportTo:
				text = Strings.goTo;
				action = delegate
				{
					GameScr.info1.addInfo(string.Format(Strings.teleportMenuTeleportingToCharacter, teleportChar) + "...", 0);
					TeleportMenuMain.TeleportToPlayer(teleportChar.ID, true);
					teleportChar.LastTimeTeleportTo = mSystem.currentTimeMillis();
				};
				break;
			case TeleportMenuMain.TeleportStatus.Delete:
				text = Strings.delete;
				action = delegate
				{
					if (teleportChar != TeleportMenuMain.charAutoTeleportTo)
					{
						TeleportMenuMain.listTeleportChars.Remove(teleportChar);
						TeleportMenuMain.SaveData();
						GameScr.info1.addInfo(string.Format(Strings.teleportMenuCharacterRemoved, teleportChar) + "!", 0);
						TeleportMenuMain.showTeleportCharListPanel();
						return;
					}
					GameCanvas.startOKDlg(Strings.teleportMenuCantRemoveTargetChar + "!");
				};
				break;
			case TeleportMenuMain.TeleportStatus.AutoTeleportTo:
				text = Strings.teleportMenuAutoTeleportTo;
				action = delegate
				{
					TeleportMenuMain.currentTeleportStatus = TeleportMenuMain.TeleportStatus.TeleportTo;
					TeleportMenuMain.showTeleportCharListPanel();
				};
				break;
			}
			new MenuBuilder().addItem(text, new MenuAction(action)).setPos(panel.X, (panel.selected + 1) * panel.ITEM_HEIGHT - panel.cmy + panel.yScroll).start();
			panel.cp = new ChatPopup();
			panel.cp.isClip = false;
			panel.cp.sayWidth = 180;
			panel.cp.cx = 3 + panel.X - ((panel.X != 0) ? (Res.abs(panel.cp.sayWidth - panel.W) + 8) : 0);
			panel.cp.says = mFont.tahoma_7_red.splitFontArray("|0|2|" + TeleportMenuMain.listTeleportChars_orderByName[panel.selected].Name + "\n|6|ID: " + TeleportMenuMain.listTeleportChars_orderByName[panel.selected].ID.ToString(), panel.cp.sayWidth - 10);
			panel.cp.delay = 10000000;
			panel.cp.c = null;
			panel.cp.sayRun = 7;
			panel.cp.ch = 15 - panel.cp.sayRun + panel.cp.says.Length * 12 + 10;
			if (panel.cp.ch > GameCanvas.h - 80)
			{
				panel.cp.ch = GameCanvas.h - 80;
				panel.cp.lim = panel.cp.says.Length * 12 - panel.cp.ch + 17;
				if (panel.cp.lim < 0)
				{
					panel.cp.lim = 0;
				}
				ChatPopup.cmyText = 0;
				panel.cp.isClip = true;
			}
			panel.cp.cy = GameCanvas.menu.menuY - panel.cp.ch;
			while (panel.cp.cy < 10)
			{
				panel.cp.cy++;
				GameCanvas.menu.menuY++;
			}
			panel.cp.mH = 0;
			panel.cp.strY = 10;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000A529D File Offset: 0x000A349D
		private static void PaintTabHeader(Panel panel, mGraphics g)
		{
			PaintPanelTemplates.PaintTabHeaderTemplate(panel, g, Strings.teleportMenuCharacterList);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000A52AC File Offset: 0x000A34AC
		internal static void PaintTeleportListPanel(Panel panel, mGraphics g)
		{
			PaintPanelTemplates.PaintCollectionCaptionAndDescriptionTemplate<TeleportChar>(panel, g, TeleportMenuMain.listTeleportChars_orderByName, (TeleportChar c) => c.Name, (TeleportChar c) => string.Format("ID: {0}", c.ID), true);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000A5304 File Offset: 0x000A3504
		private static void TeleportToPlayer(int charId, bool isAutoUseYardrat = true)
		{
			GameEvents.OnGotoPlayer(charId, isAutoUseYardrat);
		}

		// Token: 0x04001526 RID: 5414
		internal static List<TeleportChar> listTeleportChars = new List<TeleportChar>();

		// Token: 0x04001527 RID: 5415
		private static List<TeleportChar> listTeleportChars_orderByName = new List<TeleportChar>();

		// Token: 0x04001528 RID: 5416
		private static TeleportMenuMain.TeleportStatus currentTeleportStatus;

		// Token: 0x04001529 RID: 5417
		private static bool isDataLoaded;

		// Token: 0x0400152A RID: 5418
		private static bool isAutoTeleportTo;

		// Token: 0x0400152B RID: 5419
		private static TeleportChar charAutoTeleportTo;

		// Token: 0x0400152C RID: 5420
		private static long lastTimeAutoTeleportTo;

		// Token: 0x0400152D RID: 5421
		private static bool isChangeDisguise;

		// Token: 0x0400152E RID: 5422
		private static int previousDisguiseId = -1;

		// Token: 0x0200010C RID: 268
		private class TeleportMenuChatable : IChatable
		{
			// Token: 0x06000DDD RID: 3549 RVA: 0x000A532C File Offset: 0x000A352C
			public void onChatFromMe(string text, string to)
			{
				if (string.IsNullOrEmpty(text) || to != Strings.teleportMenuInputCharIDTextFieldName)
				{
					this.onCancelChat();
					return;
				}
				try
				{
					int num = int.Parse(text);
					if (num < 0)
					{
						GameCanvas.startOKDlg(string.Format(Strings.inputNumberMustBeBiggerThanOrEqual, 0) + "!");
						return;
					}
					TeleportMenuMain.listTeleportChars.Add(new TeleportChar(num));
					TeleportMenuMain.SaveData();
					GameScr.info1.addInfo(string.Format(Strings.teleportMenuAddedCharacterWithID, num) + "!", 0);
				}
				catch
				{
					GameCanvas.startOKDlg(Strings.invalidValue + "!");
				}
				ChatTextField.gI().ResetTF();
				TeleportMenuMain.SortList();
			}

			// Token: 0x06000DDE RID: 3550 RVA: 0x0009F278 File Offset: 0x0009D478
			public void onCancelChat()
			{
				ChatTextField.gI().ResetTF();
			}
		}

		// Token: 0x0200010D RID: 269
		internal enum TeleportStatus
		{
			// Token: 0x04001530 RID: 5424
			TeleportTo,
			// Token: 0x04001531 RID: 5425
			Delete,
			// Token: 0x04001532 RID: 5426
			AutoTeleportTo
		}
	}
}
