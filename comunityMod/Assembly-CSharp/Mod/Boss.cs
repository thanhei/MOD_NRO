using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mod.Graphics;
using Mod.ModHelper;
using Mod.Xmap;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000D7 RID: 215
	public class Boss
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00095B96 File Offset: 0x00093D96
		private Boss(string name, string map)
		{
			this.name = name;
			this.map = map;
			this.GetMapId(name, map);
			this.AppearTime = DateTime.Now;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00095BC8 File Offset: 0x00093DC8
		private void GetMapId(string name, string map)
		{
			if (map == "Vách núi Aru")
			{
				this.mapId = 42;
				return;
			}
			if (map == "Vách núi Moori")
			{
				this.mapId = 43;
				return;
			}
			if (map == "Trạm tàu vũ trụ")
			{
				if (name.StartsWith("Số ") || name.StartsWith("Tiểu đội"))
				{
					this.mapId = 25;
					return;
				}
				if (name.Contains("Bojack") || name.StartsWith("Bujin") || name.StartsWith("Bido") || name.StartsWith("Zangya") || name.StartsWith("Bido"))
				{
					this.mapId = 24;
					return;
				}
			}
			else
			{
				this.mapId = Boss.GetMapID(map);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00095C88 File Offset: 0x00093E88
		public static void AddBoss(string chatVip)
		{
			if (Boss.strBossHasBeenKilled.Any<string>(new Func<string, bool>(chatVip.Contains)))
			{
				Boss.strBossHasBeenKilled.ForEach(delegate(string s)
				{
					chatVip = chatVip.Replace(s, "|");
				});
				string[] array2 = chatVip.Split('|', StringSplitOptions.None);
				Boss boss = null;
				try
				{
					boss = Boss.listBosses.Last<Boss>((Boss b) => (!new int[] { 79, 82, 83 }.Contains(b.mapId) || !Regex.IsMatch(b.name, "(Tiểu đội trưởng|(Captain|Kapten) Ginyu|Số [1-4]|Jeice|Burter|Recoome|Guldo)")) && b.name == array2[1] && string.IsNullOrEmpty(b.killer));
				}
				catch (InvalidOperationException)
				{
				}
				if (boss == null)
				{
					boss = new Boss(array2[1], "");
					Boss.listBosses.Add(boss);
				}
				boss.isDied = true;
				boss.killer = array2[0];
				return;
			}
			if (chatVip.StartsWith(Boss.strBossAppeared[0]))
			{
				Boss.strBossAppeared.ForEach(delegate(string s)
				{
					chatVip = chatVip.Replace(s, "|");
				});
				string[] array = chatVip.Split('|', StringSplitOptions.None);
				Boss boss2 = null;
				try
				{
					int mapId = Boss.GetMapID(array[2]);
					boss2 = Boss.listBosses.Last<Boss>((Boss b) => (!new int[] { 79, 82, 83 }.Contains(mapId) || !Regex.IsMatch(array[1], "(Tiểu đội trưởng|(Captain|Kapten) Ginyu|Số [1-4]|Jeice|Burter|Recoome|Guldo)")) && string.IsNullOrEmpty(b.map) && b.name == array[1]);
				}
				catch (InvalidOperationException)
				{
				}
				if (boss2 == null)
				{
					boss2 = new Boss(array[1], array[2]);
					Boss.listBosses.Add(boss2);
				}
				else
				{
					boss2.map = array[2];
					boss2.GetMapId(boss2.name, boss2.map);
				}
				if (array.Length == 4)
				{
					boss2.zoneId = int.Parse(array[3]);
				}
				if (Boss.listBosses.Count > Boss.MAX_BOSS)
				{
					Boss.listBosses.RemoveAt(0);
				}
				int num;
				int num2;
				int num3;
				Boss.getScrollBar(out num, out num2, out num3);
				if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY && Boss.offsetX == 0)
				{
					Boss.offsetX = num;
				}
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00095E94 File Offset: 0x00094094
		public override string ToString()
		{
			TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
			string text = this.name + " - ";
			if (string.IsNullOrEmpty(this.map))
			{
				text += "chưa biết";
			}
			else
			{
				text += string.Format("{0} [{1}]", this.map, this.mapId);
			}
			text += " - ";
			if (!this.isDied)
			{
				if (this.zoneId > -1)
				{
					text += string.Format("khu {0} - ", this.zoneId);
				}
				int num = (int)Math.Floor((decimal)timeSpan.TotalHours);
				if (num > 0)
				{
					text += string.Format("{0}h", num);
				}
				if (timeSpan.Minutes > 0)
				{
					text += string.Format("{0}m", timeSpan.Minutes);
				}
				text += string.Format("{0}s", timeSpan.Seconds);
			}
			else if (!string.IsNullOrEmpty(this.killer))
			{
				text = text + "Bị " + this.killer + " tiêu diệt";
			}
			else
			{
				text += "Đã chết";
			}
			return text;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00095FEC File Offset: 0x000941EC
		public string ToString(bool enableRichText)
		{
			if (!enableRichText)
			{
				return this.ToString();
			}
			TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
			string text = "yellow";
			string text2 = "yellow";
			if (TileMap.mapID == this.mapId)
			{
				text = "orange";
				text2 = "red";
				if (Utils.FindCharInMap(this.name) != null)
				{
					text = "red";
				}
			}
			string text3 = string.Concat(new string[] { "<color=", text, ">", this.name, "</color> - " });
			if (string.IsNullOrEmpty(this.map))
			{
				text3 += "chưa biết";
			}
			else
			{
				text3 += string.Format("<color={0}>{1}</color> [<color={2}>{3}</color>]", new object[] { text2, this.map, text2, this.mapId });
			}
			text3 += " - ";
			if (!this.isDied)
			{
				if (this.zoneId > -1)
				{
					if (TileMap.mapID == this.mapId)
					{
						if (TileMap.zoneID == this.zoneId)
						{
							text3 += string.Format("<color=yellow>khu</color> <color=red>{0}</color> - ", this.zoneId);
						}
						else
						{
							text3 += string.Format("<color=yellow>khu {0}</color> - ", this.zoneId);
						}
					}
					else
					{
						text3 += string.Format("khu <color=yellow>{0}</color> - ", this.zoneId);
					}
				}
				int num = (int)Math.Floor((decimal)timeSpan.TotalHours);
				if (num > 0)
				{
					text3 += string.Format("<color=orange>{0}</color>h", num);
				}
				if (timeSpan.Minutes > 0)
				{
					text3 += string.Format("<color=orange>{0}</color>m", timeSpan.Minutes);
				}
				text3 += string.Format("<color=orange>{0}</color>s", timeSpan.Seconds);
			}
			else if (!string.IsNullOrEmpty(this.killer))
			{
				text3 = text3 + "Bị <color=orange>" + this.killer + "</color> tiêu diệt";
			}
			else
			{
				text3 += "Đã chết";
			}
			return text3;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00096214 File Offset: 0x00094414
		public static int Paint(int _y, mGraphics g)
		{
			if (!Boss.isEnabled || Boss.listBosses.Count <= 0)
			{
				return Boss.getSpaceOccupied();
			}
			Boss.maxLength = 0;
			Boss.y = _y;
			if (!Boss.isCollapsed)
			{
				Boss.PaintListBosses(g);
				Boss.PaintScroll(g);
			}
			Boss.PaintRect(g);
			return Boss.getSpaceOccupied();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00096268 File Offset: 0x00094468
		private static void PaintListBosses(mGraphics g)
		{
			int num = 0;
			if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
			{
				num = Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY;
			}
			GUIStyle[] array = new GUIStyle[Boss.MAX_BOSS_DISPLAY];
			for (int i = num - Boss.offset; i < Boss.listBosses.Count - Boss.offset; i++)
			{
				array[i - num + Boss.offset] = new GUIStyle(GUI.skin.label)
				{
					alignment = TextAnchor.UpperRight,
					fontSize = 6 * mGraphics.zoomLevel,
					fontStyle = FontStyle.Bold,
					richText = true
				};
				Boss boss = Boss.listBosses[i];
				Boss.maxLength = Math.Max(Utils.getWidth(array[i - num + Boss.offset], string.Format("{0}. {1}", i + 1, boss)), Boss.maxLength);
			}
			Boss.FillBackground(g);
			int num2 = GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength;
			for (int j = num - Boss.offset; j < Boss.listBosses.Count - Boss.offset; j++)
			{
				int num3 = Boss.y + Boss.distanceBetweenLines * (j - num + Boss.offset);
				Boss boss2 = Boss.listBosses[j];
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
				if (boss2.isDied)
				{
					g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.2f));
				}
				if (GameCanvas.isMouseFocus(num2, num3, Boss.maxLength, 7))
				{
					g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
				}
				g.fillRect(num2, num3 + 1, Boss.maxLength, 7);
				if (GameCanvas.isMouseFocus(num2, num3, Boss.maxLength, 7))
				{
					CustomGraphics.fillRect(num2 + 1, num3 + 7, (Boss.maxLength - 2) * mGraphics.zoomLevel + 2, 1, Color.white, true);
				}
				g.drawString(string.Format("{0}. {1}", j + 1, boss2.ToString(true)), -(Boss.x + Boss.offsetX), mGraphics.zoomLevel - 3 + num3, array[j - num + Boss.offset]);
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000964B4 File Offset: 0x000946B4
		private static void PaintScroll(mGraphics g)
		{
			if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
			{
				int num;
				int num2;
				Boss.getButtonUp(out num, out num2);
				int num3;
				int num4;
				Boss.getButtonDown(out num3, out num4);
				int num5;
				int num6;
				int num7;
				Boss.getScrollBar(out num5, out num6, out num7);
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
				g.fillRect(num, num2, 9, num6 + 12);
				g.drawRegion(Mob.imgHP, 0, (Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY) ? 24 : 54, 9, 6, 1, num, num2, 0);
				g.drawRegion(Mob.imgHP, 0, (Boss.offset > 0) ? 24 : 54, 9, 6, 0, num3, num4, 0);
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
				g.fillRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)Boss.listBosses.Count * (float)(Boss.listBosses.Count - Boss.offset - Boss.MAX_BOSS_DISPLAY)), num5, num7);
				g.setColor(new Color(0.7f, 0.7f, 0f, 1f));
				g.drawRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)Boss.listBosses.Count * (float)(Boss.listBosses.Count - Boss.offset - Boss.MAX_BOSS_DISPLAY)), num5 - 1, num7 - 1);
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0009662C File Offset: 0x0009482C
		private static void PaintRect(mGraphics g)
		{
			int num;
			int num2;
			int num3;
			Boss.getScrollBar(out num, out num2, out num3);
			if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
			{
				num = 0;
			}
			int num4 = Boss.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
			int num5 = Boss.distanceBetweenLines * Math.Min(Boss.MAX_BOSS_DISPLAY, Boss.listBosses.Count) + 7;
			GUIStyle guistyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 7 * mGraphics.zoomLevel,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperRight,
				richText = true
			};
			guistyle.normal.textColor = Color.white;
			Boss.titleWidth = Utils.getWidth(guistyle, Boss.LIST_BOSS);
			g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8);
			if (GameCanvas.isMouseFocus(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8))
			{
				g.setColor(guistyle.normal.textColor);
				g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - 1, Boss.titleWidth - 1, 1);
			}
			g.drawString(Boss.LIST_BOSS, -(Boss.x + Boss.offsetX) + num, Boss.y - Boss.distanceBetweenLines - 2, guistyle);
			int num6;
			int num7;
			Boss.getCollapseButton(out num6, out num7);
			g.drawRegion(Mob.imgHP, 0, 18, 9, 6, Boss.isCollapsed ? 5 : 4, num6, num7, 0);
			if (Boss.isCollapsed || Boss.listBosses.Count <= 0)
			{
				return;
			}
			g.setColor(Color.yellow);
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, num4 - Boss.titleWidth - 9 - ((num > 0) ? 2 : 0), 1);
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) + num, Boss.y - 5, 3 + ((num > 0) ? 1 : 0), 1);
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, 1, num5);
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3 + num4, Boss.y - 5, 1, num5 + 1);
			g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5 + num5, num4 + 1, 1);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000968F0 File Offset: 0x00094AF0
		private static void FillBackground(mGraphics g)
		{
			if (!Boss.isCollapsed && Boss.listBosses.Count > 0)
			{
				g.setColor(new Color(0f, 0f, 0f, 0.075f));
				int num;
				int num2;
				int num3;
				Boss.getScrollBar(out num, out num2, out num3);
				if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
				{
					num = 0;
				}
				int num4 = Boss.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
				int num5 = Boss.distanceBetweenLines * Math.Min(Boss.MAX_BOSS_DISPLAY, Boss.listBosses.Count) + 7;
				g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, num4, num5);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000969AC File Offset: 0x00094BAC
		private static int GetMapID(string mapName)
		{
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (TileMap.mapNames[i].Equals(mapName))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000969E0 File Offset: 0x00094BE0
		public static void UpdateTouch()
		{
			if (Boss.lastBoss != -1 && mSystem.currentTimeMillis() - Utils.GetLastTimePress() > 200L)
			{
				Boss.lastBoss = -1;
			}
			if (!Boss.isEnabled)
			{
				return;
			}
			if (!GameCanvas.isTouch || ChatTextField.gI().isShow || GameCanvas.menu.showMenu)
			{
				return;
			}
			int num;
			int num2;
			Boss.getCollapseButton(out num, out num2);
			int num3;
			int num4;
			int num5;
			Boss.getScrollBar(out num3, out num4, out num5);
			if (GameCanvas.isPointerHoldIn(num, num2, 6, 9) || GameCanvas.isMouseFocus(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num3, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8))
			{
				GameCanvas.isPointerJustDown = false;
				GameScr.gI().isPointerDowning = false;
				if (GameCanvas.isPointerClick)
				{
					Boss.isCollapsed = !Boss.isCollapsed;
				}
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (Boss.isCollapsed)
			{
				return;
			}
			int num6 = 0;
			if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
			{
				num6 = Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY;
			}
			for (int i = num6 - Boss.offset; i < Boss.listBosses.Count - Boss.offset; i++)
			{
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength, Boss.y + 1 + Boss.distanceBetweenLines * (i - num6 + Boss.offset), Boss.maxLength, 7))
				{
					GameCanvas.isPointerJustDown = false;
					GameScr.gI().isPointerDowning = false;
					if (GameCanvas.isPointerClick)
					{
						if (Boss.listBosses[i].isDied)
						{
							GameScr.info1.addInfo("Boss đã " + (string.IsNullOrEmpty(Boss.listBosses[i].killer) ? "chết" : ("bị " + Boss.listBosses[i].killer + " tiêu diệt")) + "!", 0);
						}
						else
						{
							if (Boss.lastBoss == i && mSystem.currentTimeMillis() - Utils.GetLastTimePress() <= 200L)
							{
								if (TileMap.mapID != Boss.listBosses[i].mapId)
								{
									if (ThreadAction<XmapController>.gI.IsActing)
									{
										XmapController.finishXmap();
									}
									XmapController.start(Boss.listBosses[i].mapId);
									Boss.lastBoss = -1;
									return;
								}
							}
							else
							{
								Boss.lastBoss = i;
							}
							if (TileMap.mapID == Boss.listBosses[i].mapId)
							{
								int j = 0;
								while (j < GameScr.vCharInMap.size())
								{
									global::Char @char = GameScr.vCharInMap.elementAt(j) as global::Char;
									if (@char.cName == Boss.listBosses[i].name)
									{
										global::Char.myCharz().deFocusNPC();
										global::Char.myCharz().itemFocus = null;
										global::Char.myCharz().mobFocus = null;
										if (global::Char.myCharz().charFocus != @char)
										{
											global::Char.myCharz().charFocus = @char;
											break;
										}
										Utils.TeleportMyChar(@char);
										break;
									}
									else
									{
										j++;
									}
								}
								if (j == GameScr.vCharInMap.size())
								{
									if (Boss.listBosses[i].zoneId != -1 && TileMap.zoneID != Boss.listBosses[i].zoneId)
									{
										GameScr.info1.addInfo(string.Format("Vào khu {0}!", Boss.listBosses[i].zoneId), 0);
										Service.gI().requestChangeZone(Boss.listBosses[i].zoneId, 0);
										return;
									}
									GameScr.info1.addInfo("Boss không có trong khu!", 0);
								}
							}
						}
					}
					GameCanvas.clearAllPointerEvent();
					return;
				}
			}
			if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
			{
				int num7;
				int num8;
				Boss.getButtonUp(out num7, out num8);
				if (GameCanvas.isPointerMove && GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(num7, num8, num3, num4))
				{
					float num9 = (float)num4 / (float)Boss.listBosses.Count;
					float num10 = (float)(GameCanvas.pyMouse - num8) / num9;
					if (float.IsNaN(num10))
					{
						return;
					}
					Boss.offset = Mathf.Clamp(Boss.listBosses.Count - Mathf.RoundToInt(num10), 0, Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY);
					return;
				}
				else
				{
					if (GameCanvas.isPointerHoldIn(num7, num8, 9, 6))
					{
						GameCanvas.isPointerJustDown = false;
						GameScr.gI().isPointerDowning = false;
						if (GameCanvas.isPointerClick)
						{
							if (Boss.offset + Boss.MAX_BOSS_DISPLAY <= Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
							{
								Boss.offset += Boss.MAX_BOSS_DISPLAY;
							}
							else if (Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
							{
								Boss.offset++;
							}
						}
						GameCanvas.clearAllPointerEvent();
						return;
					}
					int num11;
					int num12;
					Boss.getButtonDown(out num11, out num12);
					if (GameCanvas.isPointerHoldIn(num11, num12, 9, 6))
					{
						GameCanvas.isPointerJustDown = false;
						GameScr.gI().isPointerDowning = false;
						if (GameCanvas.isPointerClick)
						{
							if (Boss.offset - Boss.MAX_BOSS_DISPLAY >= 0)
							{
								Boss.offset -= Boss.MAX_BOSS_DISPLAY;
							}
							else if (Boss.offset > 0)
							{
								Boss.offset--;
							}
						}
						GameCanvas.clearAllPointerEvent();
						return;
					}
				}
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00096F00 File Offset: 0x00095100
		public static void Update()
		{
			for (int i = Boss.listBosses.Count - 1; i >= 0; i--)
			{
				Boss boss = Boss.listBosses[i];
				if (boss.mapId == TileMap.mapID && !global::Char.isLoadingMap)
				{
					int j = 0;
					while (j < GameScr.vCharInMap.size())
					{
						global::Char @char = GameScr.vCharInMap.elementAt(j) as global::Char;
						if (@char.cName == boss.name)
						{
							if (boss.zoneId == -1)
							{
								boss.zoneId = TileMap.zoneID;
							}
							if (@char.isDie || @char.cHP == 0)
							{
								boss.isDied = true;
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
					if (boss.zoneId == TileMap.zoneID && j == GameScr.vCharInMap.size())
					{
						boss.isDied = true;
					}
				}
			}
			if (Boss.isEnabled && !Boss.isCollapsed && GameCanvas.isMouseFocus(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength, Boss.y + 1, Boss.maxLength, 8 * Boss.MAX_BOSS_DISPLAY))
			{
				if (GameCanvas.pXYScrollMouse > 0 && Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
				{
					Boss.offset++;
				}
				if (GameCanvas.pXYScrollMouse < 0 && Boss.offset > 0)
				{
					Boss.offset--;
				}
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0009705D File Offset: 0x0009525D
		private static void getButtonUp(out int buttonUpX, out int buttonUpY)
		{
			buttonUpX = GameCanvas.w - (Boss.x + Boss.offsetX) + 2;
			buttonUpY = Boss.y + 1;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0009707D File Offset: 0x0009527D
		private static void getButtonDown(out int buttonDownX, out int buttonDownY)
		{
			buttonDownX = GameCanvas.w - (Boss.x + Boss.offsetX) + 2;
			buttonDownY = Boss.y + 2 + Boss.distanceBetweenLines * (Boss.MAX_BOSS_DISPLAY - 1);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000970AB File Offset: 0x000952AB
		private static void getScrollBar(out int scrollBarWidth, out int scrollBarHeight, out int scrollBarThumbHeight)
		{
			scrollBarWidth = 9;
			scrollBarHeight = Boss.MAX_BOSS_DISPLAY * Boss.distanceBetweenLines - 1 - 12;
			scrollBarThumbHeight = Mathf.CeilToInt((float)Boss.MAX_BOSS_DISPLAY / (float)Boss.listBosses.Count * (float)scrollBarHeight);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000970E0 File Offset: 0x000952E0
		private static void getCollapseButton(out int collapseButtonX, out int collapseButtonY)
		{
			int num;
			int num2;
			int num3;
			Boss.getScrollBar(out num, out num2, out num3);
			if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
			{
				num = 0;
			}
			collapseButtonX = GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num - 8;
			collapseButtonY = Boss.y - Boss.distanceBetweenLines + 1;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00097137 File Offset: 0x00095337
		public static void setState(bool value)
		{
			Boss.isEnabled = value;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00097140 File Offset: 0x00095340
		internal static int getSpaceOccupied()
		{
			if (!Boss.isEnabled || Boss.listBosses.Count <= 0)
			{
				return 0;
			}
			byte b = 5;
			byte b2 = 7;
			if (!Boss.isCollapsed)
			{
				return (int)(b2 + b) + Boss.distanceBetweenLines * Mathf.Clamp(Boss.listBosses.Count, 0, Boss.MAX_BOSS_DISPLAY);
			}
			return Boss.distanceBetweenLines;
		}

		// Token: 0x0400146C RID: 5228
		public string name;

		// Token: 0x0400146D RID: 5229
		public string map;

		// Token: 0x0400146E RID: 5230
		public int mapId;

		// Token: 0x0400146F RID: 5231
		public int zoneId = -1;

		// Token: 0x04001470 RID: 5232
		public DateTime AppearTime;

		// Token: 0x04001471 RID: 5233
		public bool isDied;

		// Token: 0x04001472 RID: 5234
		public string killer;

		// Token: 0x04001473 RID: 5235
		public static List<Boss> listBosses = new List<Boss>();

		// Token: 0x04001474 RID: 5236
		public static bool isEnabled;

		// Token: 0x04001475 RID: 5237
		public static int distanceBetweenLines = 8;

		// Token: 0x04001476 RID: 5238
		private static int offset = 0;

		// Token: 0x04001477 RID: 5239
		public static int x = 6;

		// Token: 0x04001478 RID: 5240
		public static int y = 0;

		// Token: 0x04001479 RID: 5241
		private static int maxLength = 0;

		// Token: 0x0400147A RID: 5242
		private static int lastBoss = -1;

		// Token: 0x0400147B RID: 5243
		public static bool isCollapsed;

		// Token: 0x0400147C RID: 5244
		public static readonly int MAX_BOSS_DISPLAY = 5;

		// Token: 0x0400147D RID: 5245
		private static readonly int MAX_BOSS = 100;

		// Token: 0x0400147E RID: 5246
		private static readonly string LIST_BOSS = "Danh sách Boss";

		// Token: 0x0400147F RID: 5247
		private static GUIStyle collapsedStyle;

		// Token: 0x04001480 RID: 5248
		private static int listBossWidth = 0;

		// Token: 0x04001481 RID: 5249
		private static int titleWidth;

		// Token: 0x04001482 RID: 5250
		private static int offsetX;

		// Token: 0x04001483 RID: 5251
		private static readonly List<string> strBossHasBeenKilled = new List<string> { " mọi người đều ngưỡng mộ.", " everyone admired.", " semua orang mengagumi.", " đã đánh bại và nhận được cải trang thành ", " killed and receive disguise of ", " membunuh Dan menerima disguise ", ": Đã tiêu diệt được ", ": defeated ", ": mengalahkan " };

		// Token: 0x04001484 RID: 5252
		private static readonly List<string> strBossAppeared = new List<string> { "BOSS ", " vừa xuất hiện tại ", " appear at ", " muncul di ", " khu vực ", " zone ", " zona " };
	}
}
