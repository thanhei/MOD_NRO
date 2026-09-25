using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod.ModHelper;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.CommandMod.Hotkey;
using Mod.ModHelper.Menu;
using Mod.R;
using UnityEngine;

namespace Mod.Xmap
{
	// Token: 0x020000F4 RID: 244
	internal class Pk9rXmap
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x000A267A File Offset: 0x000A087A
		[ChatCommand("xcsdb")]
		internal static void ToggleUseCapsuleVip()
		{
			Pk9rXmap.isUseCapsuleVip = !Pk9rXmap.isUseCapsuleVip;
			GameScr.info1.addInfo(Strings.xmapUseSpecialCapsule + ": " + Strings.OnOffStatus(Pk9rXmap.isUseCapsuleVip), 0);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000A26AD File Offset: 0x000A08AD
		[ChatCommand("xcsb")]
		internal static void ToggleUseCapsuleNormal()
		{
			Pk9rXmap.isUseCapsuleNormal = !Pk9rXmap.isUseCapsuleNormal;
			GameScr.info1.addInfo(Strings.xmapUseNormalCapsule + ": " + Strings.OnOffStatus(Pk9rXmap.isUseCapsuleNormal), 0);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000A26E0 File Offset: 0x000A08E0
		[ChatCommand("xmp")]
		[HotkeyCommand('x')]
		internal static void ShowXmapMenu()
		{
			if (ThreadAction<XmapController>.gI.IsActing)
			{
				LogMod.writeLine("[xmap][info] Người chơi yêu cầu hủy xmap");
				XmapController.finishXmap();
				GameScr.info1.addInfo(Strings.xmapCanceled, 0);
				return;
			}
			XmapData.LoadGroupMaps();
			new MenuBuilder().setChatPopup(string.Format(Strings.xmapChatPopup, TileMap.mapName, TileMap.mapID)).map<GroupMap>(XmapData.groups, delegate(GroupMap groupMap)
			{
				string text = groupMap.names[groupMap.names.Length - 1];
				if (groupMap.names.Length > (int)mResources.language)
				{
					text = groupMap.names[(int)mResources.language];
				}
				return new MenuItem(text, new MenuAction(delegate
				{
					XmapPanel.Show(groupMap.maps);
				}));
			}).addItem(Strings.settings, new MenuAction(new Action(Pk9rXmap.ShowXmapSettings)))
				.start();
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000A278C File Offset: 0x000A098C
		private static void ShowXmapSettings()
		{
			new MenuBuilder().setChatPopup(string.Format(Strings.xmapChatPopup, TileMap.mapName, TileMap.mapID)).addItem(Strings.xmapUseNormalCapsule + ": " + Strings.OnOffStatus(Pk9rXmap.isUseCapsuleNormal), new MenuAction(new Action(Pk9rXmap.ToggleUseCapsuleNormal))).addItem(Strings.xmapUseSpecialCapsule + ": " + Strings.OnOffStatus(Pk9rXmap.isUseCapsuleVip), new MenuAction(new Action(Pk9rXmap.ToggleUseCapsuleVip)))
				.addItem(Strings.xmapUseAStar + ": " + Strings.OnOffStatus(Pk9rXmap.isXmapAStar), new MenuAction(delegate
				{
					Pk9rXmap.isXmapAStar = !Pk9rXmap.isXmapAStar;
					GameScr.info1.addInfo(Strings.xmapUseAStar + ": " + Strings.OnOffStatus(Pk9rXmap.isXmapAStar), 0);
				}))
				.addItem(Strings.xmapEditTimeout, new MenuAction(delegate
				{
					ChatTextField.gI().strChat = Strings.timeout;
					ChatTextField.gI().tfChat.name = Strings.timeout + " (10-300s)";
					ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					ChatTextField.gI().startChat2(new Pk9rXmap.XmapChatable(), string.Empty);
					ChatTextField.gI().tfChat.setText(Pk9rXmap.aStarTimeout.ToString());
				}))
				.start();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000A2890 File Offset: 0x000A0A90
		internal static void Info(string text)
		{
			if (ThreadAction<XmapController>.gI.IsActing)
			{
				if (LocalizedString.xmapCantGoHereKeywords.Any<LocalizedString>((LocalizedString lS) => lS == text))
				{
					XmapController.finishXmap();
					GameScr.info1.addInfo(Strings.xmapCanceled, 0);
					return;
				}
				if (text == LocalizedString.errorOccurred)
				{
					Pk9rXmap.MoveMyChar(XmapUtils.getX(2), XmapUtils.getY(2));
				}
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000A2907 File Offset: 0x000A0B07
		internal static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			global::Char.isLoadingMap = false;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000A2924 File Offset: 0x000A0B24
		internal static bool CanUseCapsuleNormal()
		{
			return Pk9rXmap.isUseCapsuleNormal && !global::Char.myCharz().IsCharDead() && XmapUtils.hasItemCapsuleNormal();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000A2940 File Offset: 0x000A0B40
		internal static bool CanUseCapsuleVip()
		{
			return Pk9rXmap.isUseCapsuleVip && !global::Char.myCharz().IsCharDead() && XmapUtils.hasItemCapsuleVip();
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000A295C File Offset: 0x000A0B5C
		internal static int GetMapIdFromPanelXmap(string mapName)
		{
			return int.Parse(mapName.Split(':', StringSplitOptions.None)[0]);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000A2970 File Offset: 0x000A0B70
		internal static void NextMap(MapNext mapNext)
		{
			switch (mapNext.type)
			{
			case TypeMapNext.AutoWaypoint:
				Pk9rXmap.NextMapAutoWaypoint(mapNext);
				return;
			case TypeMapNext.NpcMenu:
				Pk9rXmap.NextMapNpcMenu(mapNext);
				return;
			case TypeMapNext.NpcPanel:
				Pk9rXmap.NextMapNpcPanel(mapNext);
				return;
			case TypeMapNext.Position:
				Pk9rXmap.NextMapPosition(mapNext);
				return;
			case TypeMapNext.Capsule:
				Pk9rXmap.NextMapCapsule(mapNext);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000A29C1 File Offset: 0x000A0BC1
		internal static void NextMapAutoWaypoint(MapNext mapNext)
		{
			Pk9rXmap.ChangeMap(XmapUtils.findWaypoint(mapNext.to));
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000A29D4 File Offset: 0x000A0BD4
		internal static void NextMapNpcMenu(MapNext mapNext)
		{
			int num = mapNext.info[0];
			if (num == 38)
			{
				bool flag = false;
				int num2 = GameScr.vNpc.size();
				for (int i = 0; i < num2; i++)
				{
					if (((Npc)GameScr.vNpc.elementAt(i)).template.npcTemplateId == num)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Waypoint waypoint;
					if (TileMap.mapID == 27 || TileMap.mapID == 29)
					{
						waypoint = XmapUtils.findWaypoint(28);
					}
					else if (Pk9rXmap.random.Next(27, 29) == 27)
					{
						waypoint = XmapUtils.findWaypoint(27);
					}
					else
					{
						waypoint = XmapUtils.findWaypoint(29);
					}
					Pk9rXmap.ChangeMap(waypoint);
					return;
				}
			}
			Service.gI().openMenu(num);
			for (int j = 1; j < mapNext.info.Length; j++)
			{
				int num3 = mapNext.info[j];
				Service.gI().confirmMenu((short)num, (sbyte)num3);
			}
			global::Char.chatPopup = null;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000A2ABC File Offset: 0x000A0CBC
		internal static void NextMapNpcPanel(MapNext mapNext)
		{
			int num = mapNext.info[0];
			int num2 = mapNext.info[1];
			int num3 = mapNext.info[2];
			Service.gI().openMenu(num);
			Service.gI().confirmMenu((short)num, (sbyte)num2);
			Service.gI().requestMapSelect(num3);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000A2B08 File Offset: 0x000A0D08
		internal static void NextMapPosition(MapNext mapNext)
		{
			int num = mapNext.info[0];
			int num2 = mapNext.info[1];
			Pk9rXmap.MoveMyChar(num, num2);
			if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)num, (double)num2) <= (double)TileMap.size)
			{
				Service.gI().requestChangeMap();
				Service.gI().getMapOffline();
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000A2B6C File Offset: 0x000A0D6C
		internal static void NextMapCapsule(MapNext mapNext)
		{
			XmapUtils.mapCapsuleReturn = TileMap.mapID;
			int num = mapNext.info[0];
			Service.gI().requestMapSelect(num);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000A2B98 File Offset: 0x000A0D98
		private static void MoveMyChar(int x, int y)
		{
			if (!Pk9rXmap.isXmapAStar)
			{
				Utils.TeleportMyChar(x, y);
				return;
			}
			if (Pk9rXmap.isMovingMyChar)
			{
				return;
			}
			Pk9rXmap.isMovingMyChar = true;
			new Thread(delegate
			{
				try
				{
					long num = mSystem.currentTimeMillis();
					int size = (int)TileMap.size;
					Tile tile = new Tile(global::Char.myCharz().cx / size, global::Char.myCharz().cy / size - 1);
					Tile tile2 = new Tile(x / size, y / size);
					Stack<Tile> stack = XmapAStar.FindPath(tile, tile2);
					if (stack.Count == 0)
					{
						XmapController.finishXmap();
						GameScr.info1.addInfo(Strings.xmapCantFindWay, 0);
						Pk9rXmap.isMovingMyChar = false;
					}
					while (stack.Count > 0 && ThreadAction<XmapController>.gI.IsActing)
					{
						if (mSystem.currentTimeMillis() - num > (long)(Pk9rXmap.aStarTimeout * 1000))
						{
							Pk9rXmap.isMovingMyChar = false;
							return;
						}
						Tile tile3 = stack.Pop();
						int num2 = 0;
						int num3 = tile3.x * size;
						int num4 = tile3.y * size;
						global::Char.myCharz().currentMovePoint = new MovePoint(num3, num4);
						while (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)num3, (double)num4) > (double)(size * 2) && ThreadAction<XmapController>.gI.IsActing)
						{
							if (mSystem.currentTimeMillis() - num > (long)(Pk9rXmap.aStarTimeout * 1000))
							{
								Pk9rXmap.isMovingMyChar = false;
								return;
							}
							if (num2 % 500 == 0)
							{
								if (num2 >= 2000)
								{
									num3 = tile3.x * size + Pk9rXmap.random.Next(size / -2, size / 2);
									num4 = tile3.y * size + Pk9rXmap.random.Next(size / -2, size / 2);
									num2 = 0;
								}
								if (global::Char.myCharz().currentMovePoint == null || global::Char.myCharz().currentMovePoint.xEnd != num3 || global::Char.myCharz().currentMovePoint.yEnd != tile3.y * size + num4)
								{
									global::Char.myCharz().currentMovePoint = new MovePoint(num3, num4);
								}
							}
							Thread.Sleep(100);
							num2 += 100;
						}
					}
					Thread.Sleep(500);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				Pk9rXmap.isMovingMyChar = false;
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000A2C00 File Offset: 0x000A0E00
		private static void ChangeMap(Waypoint waypoint)
		{
			if (!Pk9rXmap.isXmapAStar)
			{
				Utils.ChangeMap(waypoint);
				return;
			}
			if (Pk9rXmap.isChangingMap)
			{
				return;
			}
			Pk9rXmap.isChangingMap = true;
			new Thread(delegate
			{
				try
				{
					int size = (int)TileMap.size;
					Tile tile = new Tile(global::Char.myCharz().cx / size, global::Char.myCharz().cy / size - 1);
					Tile tile2 = new Tile(waypoint.GetXInsideMap() / size, (int)waypoint.minY / size);
					Stack<Tile> stack = XmapAStar.FindPath(tile, tile2);
					if (stack.Count == 0)
					{
						XmapController.finishXmap();
						GameScr.info1.addInfo(Strings.xmapCantFindWay, 0);
						Pk9rXmap.isChangingMap = false;
						return;
					}
					long num = mSystem.currentTimeMillis();
					int mapID = TileMap.mapID;
					while (stack.Count > 0 && ThreadAction<XmapController>.gI.IsActing && TileMap.mapID == mapID)
					{
						if (mSystem.currentTimeMillis() - num > (long)(Pk9rXmap.aStarTimeout * 1000))
						{
							Utils.ChangeMap(waypoint);
							Pk9rXmap.isChangingMap = false;
							return;
						}
						Tile tile3 = stack.Pop();
						int num2 = 0;
						int num3 = tile3.x * size;
						int num4 = tile3.y * size;
						global::Char.myCharz().currentMovePoint = new MovePoint(num3, num4);
						while (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)num3, (double)num4) > (double)(size * 2) && ThreadAction<XmapController>.gI.IsActing && TileMap.mapID == mapID)
						{
							if (mSystem.currentTimeMillis() - num > (long)(Pk9rXmap.aStarTimeout * 1000))
							{
								Utils.ChangeMap(waypoint);
								Pk9rXmap.isChangingMap = false;
								return;
							}
							if (num2 % 500 == 0)
							{
								if (num2 >= 1500)
								{
									int num5;
									do
									{
										num5 = Pk9rXmap.random.Next(-2, 3);
									}
									while (num5 == 0);
									num3 = tile3.x * size + num5 * size / 2;
									do
									{
										num5 = Pk9rXmap.random.Next(-2, 3);
									}
									while (num5 == 0);
									num4 = tile3.y * size + num5 * size / 2;
								}
								if (num2 >= 5000)
								{
									stack = XmapAStar.FindPath(new Tile(global::Char.myCharz().cx / size, global::Char.myCharz().cy / size - 1), tile2);
									if (stack.Count == 0)
									{
										XmapController.finishXmap();
										GameScr.info1.addInfo(Strings.xmapCantFindWay, 0);
										Pk9rXmap.isChangingMap = false;
										return;
									}
									break;
								}
								else if (global::Char.myCharz().currentMovePoint == null || global::Char.myCharz().currentMovePoint.xEnd != num3 || global::Char.myCharz().currentMovePoint.yEnd != tile3.y * size + num4)
								{
									global::Char.myCharz().currentMovePoint = new MovePoint(num3, num4);
								}
							}
							Thread.Sleep(100);
							num2 += 100;
						}
					}
					if (Utils.Distance((double)global::Char.myCharz().cx, (double)global::Char.myCharz().cy, (double)waypoint.GetXInsideMap(), (double)waypoint.minY) <= (double)(size * 2))
					{
						waypoint.popup.command.performAction();
					}
					Thread.Sleep(500);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				Pk9rXmap.isChangingMap = false;
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x040014E7 RID: 5351
		internal static bool isUseCapsuleNormal = false;

		// Token: 0x040014E8 RID: 5352
		internal static bool isUseCapsuleVip = true;

		// Token: 0x040014E9 RID: 5353
		internal static bool isXmapAStar = false;

		// Token: 0x040014EA RID: 5354
		private static bool isChangingMap;

		// Token: 0x040014EB RID: 5355
		private static bool isMovingMyChar;

		// Token: 0x040014EC RID: 5356
		internal static int aStarTimeout = 60;

		// Token: 0x040014ED RID: 5357
		private static global::System.Random random = new global::System.Random();

		// Token: 0x020000F5 RID: 245
		private class XmapChatable : IChatable
		{
			// Token: 0x06000D6F RID: 3439 RVA: 0x000A2C80 File Offset: 0x000A0E80
			public void onChatFromMe(string text, string to)
			{
				int num;
				if (!int.TryParse(text, out num))
				{
					GameCanvas.startOKDlg(Strings.invalidValue + "!");
					return;
				}
				if (num < 10 || num > 300)
				{
					GameCanvas.startOKDlg(string.Format(Strings.inputNumberOutOfRange, 10, 300) + "!");
					return;
				}
				Pk9rXmap.aStarTimeout = num;
				GameScr.info1.addInfo(Strings.xmapTimeout + ": " + Pk9rXmap.aStarTimeout.ToString(), 0);
				this.onCancelChat();
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x0009F278 File Offset: 0x0009D478
			public void onCancelChat()
			{
				ChatTextField.gI().ResetTF();
			}
		}
	}
}
