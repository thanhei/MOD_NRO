using System;
using System.Collections.Generic;
using System.Linq;
using Mod.R;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000E9 RID: 233
	internal class ListCharsInMap
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x0009F39C File Offset: 0x0009D59C
		internal static void Update()
		{
			if (!ListCharsInMap.isEnabled)
			{
				return;
			}
			ListCharsInMap.listChars.Clear();
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char.IsNormalChar(true, false))
				{
					ListCharsInMap.listChars.Add(@char);
					if (ListCharsInMap.isShowPet && @char.charID > 0)
					{
						global::Char char2 = GameScr.findCharInMap(-@char.charID);
						if (char2 != null)
						{
							ListCharsInMap.listChars.Add(char2);
						}
					}
				}
			}
			if (ListCharsInMap.isShowPet)
			{
				for (int j = 0; j < GameScr.vCharInMap.size(); j++)
				{
					global::Char char3 = (global::Char)GameScr.vCharInMap.elementAt(j);
					if (char3.IsNormalChar(false, true) && !ListCharsInMap.listChars.Contains(char3))
					{
						ListCharsInMap.listChars.Add(char3);
					}
				}
			}
			if (ListCharsInMap.offset >= ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
			{
				if (ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR > 0)
				{
					ListCharsInMap.offset = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
				}
				else
				{
					ListCharsInMap.offset = 0;
				}
			}
			if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength, ListCharsInMap.y + 1, ListCharsInMap.maxLength, 8 * ListCharsInMap.MAX_CHAR))
			{
				if (GameCanvas.pXYScrollMouse > 0 && ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
				{
					ListCharsInMap.offset++;
				}
				if (GameCanvas.pXYScrollMouse < 0 && ListCharsInMap.offset > 0)
				{
					ListCharsInMap.offset--;
				}
			}
			int num;
			int num2;
			int num3;
			ListCharsInMap.getScrollBar(out num, out num2, out num3);
			if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
			{
				ListCharsInMap.offsetX = num;
				return;
			}
			ListCharsInMap.offsetX = 0;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0009F564 File Offset: 0x0009D764
		internal static int Paint(int _y, mGraphics g)
		{
			if (!ListCharsInMap.isEnabled)
			{
				return ListCharsInMap.getSpaceOccupied();
			}
			if (ListCharsInMap.offset >= ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
			{
				if (ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR > 0)
				{
					ListCharsInMap.offset = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
				}
				else
				{
					ListCharsInMap.offset = 0;
				}
			}
			ListCharsInMap.y = _y;
			ListCharsInMap.maxLength = 0;
			if (!ListCharsInMap.isCollapsed)
			{
				ListCharsInMap.PaintListChars(g);
				ListCharsInMap.PaintScroll(g);
			}
			ListCharsInMap.PaintRect(g);
			return ListCharsInMap.getSpaceOccupied();
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0009F5F0 File Offset: 0x0009D7F0
		private static string formatHP(global::Char ch)
		{
			int cHP = ch.cHP;
			int cHPFull = ch.cHPFull;
			float num = (float)cHP / (float)cHPFull;
			Color color = new Color(Mathf.Clamp(2f - num * 2f, 0f, 1f), Mathf.Clamp(num * 2f, 0f, 1f), 0f);
			string text = string.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
			{
				(int)(color.r * 255f),
				(int)(color.g * 255f),
				(int)(color.b * 255f),
				(int)(color.a * 255f)
			});
			if (cHP == 0)
			{
				text = "black";
			}
			return string.Concat(new string[]
			{
				"<color=white>[<color=",
				text,
				">",
				NinjaUtil.getMoneys((long)ch.cHP),
				"</color>/<color=lime>",
				NinjaUtil.getMoneys((long)ch.cHPFull),
				"</color>]</color>"
			});
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0009F710 File Offset: 0x0009D910
		private static void PaintListChars(mGraphics g)
		{
			int num = 0;
			List<KeyValuePair<string, GUIStyle>> list = new List<KeyValuePair<string, GUIStyle>>();
			int num2 = 0;
			if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
			{
				num2 = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
			}
			for (int i = num2 - ListCharsInMap.offset; i < ListCharsInMap.listChars.Count - ListCharsInMap.offset; i++)
			{
				GUIStyle guistyle = new GUIStyle(GUI.skin.label)
				{
					fontSize = 6 * mGraphics.zoomLevel,
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.UpperRight,
					richText = true
				};
				global::Char @char = ListCharsInMap.listChars[i];
				string text = string.Concat(new string[]
				{
					"<color=orange>",
					@char.GetClanTag(),
					"</color>",
					@char.GetNameWithoutClanTag(true),
					" ",
					ListCharsInMap.formatHP(@char)
				});
				if (@char.IsNormalChar(false, false))
				{
					text += string.Format(" - {0} [{1}]", @char.GetGender(true), @char.charID);
				}
				if (@char.IsPet())
				{
					text = text + " - " + @char.GetGender(true);
					global::Char char2 = GameScr.findCharInMap(-@char.charID);
					if (char2 != null)
					{
						text += string.Format(Strings.someonePet, char2.GetNameWithoutClanTag(true));
					}
					else
					{
						text += Strings.petLostMaster;
					}
					num++;
				}
				else if (!@char.IsBoss())
				{
					text = (i + 1 - num).ToString() + ". " + text;
				}
				else
				{
					num++;
				}
				if (@char.charEffectTime.hasBlackStarDragonBall || @char.IsBoss())
				{
					text = "<color=red>" + text + "</color>";
				}
				else if (@char.IsPet())
				{
					text = "<color=cyan>" + text + "</color>";
				}
				if (@char.cHP <= 0)
				{
					text = "<color=black>" + text + "</color>";
				}
				list.Add(new KeyValuePair<string, GUIStyle>(text, guistyle));
				ListCharsInMap.maxLength = Math.Max(ListCharsInMap.maxLength, Utils.getWidth(list[i - num2 + ListCharsInMap.offset].Value, text) + ((@char.cFlag != 0) ? (ListCharsInMap.distanceBetweenLines + 1) : 0));
			}
			ListCharsInMap.FillBackground(g);
			for (int j = num2 - ListCharsInMap.offset; j < ListCharsInMap.listChars.Count - ListCharsInMap.offset; j++)
			{
				int num3 = 0;
				global::Char char3 = ListCharsInMap.listChars[j];
				if (char3.cFlag != 0)
				{
					num3 = ListCharsInMap.distanceBetweenLines + 1;
					if (char3.cFlag == 9 || char3.cFlag == 10)
					{
						GUIStyle guistyle2 = new GUIStyle(GUI.skin.label)
						{
							alignment = TextAnchor.UpperCenter,
							fontSize = 6 * mGraphics.zoomLevel
						};
						guistyle2.normal.textColor = Color.white;
						if (char3.cFlag == 9)
						{
							g.drawString("K", -(ListCharsInMap.x + ListCharsInMap.offsetX), mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), guistyle2);
						}
						if (char3.cFlag == 10)
						{
							g.drawString("M", -(ListCharsInMap.x + ListCharsInMap.offsetX), mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), guistyle2);
						}
					}
					g.setColor(char3.GetFlagColor());
					g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.distanceBetweenLines + 1, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.distanceBetweenLines - 1, ListCharsInMap.distanceBetweenLines - 1);
				}
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
				if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - num3, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
				{
					g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
				}
				if (global::Char.myCharz().charFocus == ListCharsInMap.listChars[j])
				{
					g.setColor(new Color(1f, 0.5f, 0f, 0.5f));
				}
				g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength - num3, ListCharsInMap.distanceBetweenLines - 1);
				if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - num3, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
				{
					int width = Utils.getWidth(list[j - num2 + ListCharsInMap.offset].Value, list[j - num2 + ListCharsInMap.offset].Key);
					g.setColor(Color.white);
					g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - width - num3 + 1, ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + 7, width - 2, 1);
					float cHP = (float)char3.cHP;
					int cHPFull = char3.cHPFull;
					float num4 = cHP / (float)cHPFull;
					Color color = new Color(Mathf.Clamp(2f - num4 * 2f, 0f, 1f), Mathf.Clamp(num4 * 2f, 0f, 1f), 0f);
					g.setColor(color);
					g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - width - num3 + 1, ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + 7, (int)(num4 * (float)(width - 2)), 1);
				}
				int num5 = 0;
				if (char3.IsBoss())
				{
					num5 = -1;
				}
				g.drawString(list[j - num2 + ListCharsInMap.offset].Key, -(ListCharsInMap.x + ListCharsInMap.offsetX) - num3, mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + num5, list[j - num2 + ListCharsInMap.offset].Value);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0009FDC4 File Offset: 0x0009DFC4
		private static void FillBackground(mGraphics g)
		{
			if (!ListCharsInMap.isCollapsed && ListCharsInMap.listChars.Count > 0)
			{
				g.setColor(new Color(0f, 0f, 0f, 0.075f));
				int num;
				int num2;
				int num3;
				ListCharsInMap.getScrollBar(out num, out num2, out num3);
				if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
				{
					num = 0;
				}
				int num4 = ListCharsInMap.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
				int num5 = ListCharsInMap.distanceBetweenLines * Math.Min(ListCharsInMap.MAX_CHAR, ListCharsInMap.listChars.Count) + 7;
				g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, num4, num5);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0009FE80 File Offset: 0x0009E080
		private static void PaintScroll(mGraphics g)
		{
			if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
			{
				int num;
				int num2;
				ListCharsInMap.getButtonUp(out num, out num2);
				int num3;
				int num4;
				ListCharsInMap.getButtonDown(out num3, out num4);
				int num5;
				int num6;
				int num7;
				ListCharsInMap.getScrollBar(out num5, out num6, out num7);
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
				g.fillRect(num, num2, 9, num6 + 12);
				g.drawRegion(Mob.imgHP, 0, (ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR) ? 18 : 54, 9, 6, 1, num, num2, 0);
				g.drawRegion(Mob.imgHP, 0, (ListCharsInMap.offset > 0) ? 18 : 54, 9, 6, 0, num3, num4, 0);
				g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
				g.fillRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)ListCharsInMap.listChars.Count * (float)(ListCharsInMap.listChars.Count - ListCharsInMap.offset - ListCharsInMap.MAX_CHAR)), num5, num7);
				g.setColor(new Color(0.7f, 0.7f, 0f, 1f));
				g.drawRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)ListCharsInMap.listChars.Count * (float)(ListCharsInMap.listChars.Count - ListCharsInMap.offset - ListCharsInMap.MAX_CHAR)), num5 - 1, num7 - 1);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0009FFF8 File Offset: 0x0009E1F8
		private static void PaintRect(mGraphics g)
		{
			int num;
			int num2;
			int num3;
			ListCharsInMap.getScrollBar(out num, out num2, out num3);
			if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
			{
				num = 0;
			}
			int num4 = ListCharsInMap.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
			int num5 = ListCharsInMap.distanceBetweenLines * Math.Min(ListCharsInMap.MAX_CHAR, ListCharsInMap.listChars.Count) + 7;
			int num6 = ListCharsInMap.listChars.Where<global::Char>((global::Char c) => c.IsNormalChar(false, false)).Count<global::Char>();
			float num7 = (float)num6 / (float)GameScr.gI().maxPlayer[TileMap.zoneID];
			Color color = new Color(Mathf.Clamp(num7 * 2f, 0f, 1f), Mathf.Clamp(2f - num7 * 2f, 0f, 1f), 0f);
			string text = string.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
			{
				(int)(color.r * 255f),
				(int)(color.g * 255f),
				(int)(color.b * 255f),
				(int)(color.a * 255f)
			});
			string text2 = string.Format("<color=yellow>{0}</color> {1} <color=yellow>{2}</color> [<color={3}>{4}</color>/<color=red>{5}</color>]", new object[]
			{
				TileMap.mapName,
				Strings.zone,
				TileMap.zoneID,
				text,
				num6,
				GameScr.gI().maxPlayer[TileMap.zoneID]
			});
			GUIStyle guistyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 7 * mGraphics.zoomLevel,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperRight,
				richText = true
			};
			guistyle.normal.textColor = Color.white;
			ListCharsInMap.titleWidth = Utils.getWidth(guistyle, text2);
			g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8);
			if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8))
			{
				g.setColor(guistyle.normal.textColor);
				g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - 1, ListCharsInMap.titleWidth - 1, 1);
			}
			g.drawString(text2, -(ListCharsInMap.x + ListCharsInMap.offsetX) + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines - 2, guistyle);
			int num8;
			int num9;
			ListCharsInMap.getCollapseButton(out num8, out num9);
			g.drawRegion(Mob.imgHP, 0, 18, 9, 6, ListCharsInMap.isCollapsed ? 5 : 4, num8, num9, 0);
			if (ListCharsInMap.isCollapsed || ListCharsInMap.listChars.Count <= 0)
			{
				return;
			}
			g.setColor(Color.yellow);
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, num4 - ListCharsInMap.titleWidth - 9 - ((num > 0) ? 2 : 0), 1);
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + num, ListCharsInMap.y - 5, 3 + ((num > 0) ? 1 : 0), 1);
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, 1, num5);
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3 + num4, ListCharsInMap.y - 5, 1, num5 + 1);
			g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5 + num5, num4 + 1, 1);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000A0400 File Offset: 0x0009E600
		internal static void updateTouch()
		{
			if (!ListCharsInMap.isEnabled)
			{
				return;
			}
			try
			{
				if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
				{
					int num;
					int num2;
					int num3;
					ListCharsInMap.getScrollBar(out num, out num2, out num3);
					int num4;
					int num5;
					ListCharsInMap.getCollapseButton(out num4, out num5);
					if (GameCanvas.isPointerHoldIn(num4, num5, 9, 6) || GameCanvas.isPointerHoldIn(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8))
					{
						GameCanvas.isPointerJustDown = false;
						GameScr.gI().isPointerDowning = false;
						if (GameCanvas.isPointerClick)
						{
							ListCharsInMap.isCollapsed = !ListCharsInMap.isCollapsed;
						}
						GameCanvas.clearAllPointerEvent();
					}
					else
					{
						int num6 = 0;
						if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
						{
							num6 = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
						}
						for (int i = num6 - ListCharsInMap.offset; i < ListCharsInMap.listChars.Count - ListCharsInMap.offset; i++)
						{
							if (GameCanvas.isPointerHoldIn(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - ((ListCharsInMap.listChars[i].cFlag != 0) ? (ListCharsInMap.distanceBetweenLines + 1) : 0), ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (i - num6 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
							{
								GameCanvas.isPointerJustDown = false;
								GameScr.gI().isPointerDowning = false;
								if (GameCanvas.isPointerClick)
								{
									global::Char.myCharz().mobFocus = null;
									global::Char.myCharz().npcFocus = null;
									global::Char.myCharz().itemFocus = null;
									if (global::Char.myCharz().charFocus != ListCharsInMap.listChars[i])
									{
										global::Char.myCharz().charFocus = ListCharsInMap.listChars[i];
									}
									else
									{
										Utils.TeleportMyChar(ListCharsInMap.listChars[i]);
									}
								}
								global::Char.myCharz().currentMovePoint = null;
								GameCanvas.clearAllPointerEvent();
								return;
							}
						}
						if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
						{
							int num7;
							int num8;
							ListCharsInMap.getButtonUp(out num7, out num8);
							if (GameCanvas.isPointerMove && GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(num7, num8, num, num2))
							{
								float num9 = (float)num2 / (float)ListCharsInMap.listChars.Count;
								float num10 = (float)(GameCanvas.pyMouse - num8) / num9;
								if (!float.IsNaN(num10))
								{
									ListCharsInMap.offset = Mathf.Clamp(ListCharsInMap.listChars.Count - Mathf.RoundToInt(num10), 0, ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR);
								}
							}
							else if (GameCanvas.isPointerHoldIn(num7, num8, 9, 6))
							{
								GameCanvas.isPointerJustDown = false;
								GameScr.gI().isPointerDowning = false;
								if (GameCanvas.isPointerClick && ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
								{
									ListCharsInMap.offset++;
								}
								GameCanvas.clearAllPointerEvent();
							}
							else
							{
								int num11;
								int num12;
								ListCharsInMap.getButtonDown(out num11, out num12);
								if (GameCanvas.isPointerHoldIn(num11, num12, 9, 6))
								{
									GameCanvas.isPointerJustDown = false;
									GameScr.gI().isPointerDowning = false;
									if (GameCanvas.isPointerClick && ListCharsInMap.offset > 0)
									{
										ListCharsInMap.offset--;
									}
									GameCanvas.clearAllPointerEvent();
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000A0750 File Offset: 0x0009E950
		private static void getButtonUp(out int buttonUpX, out int buttonUpY)
		{
			buttonUpX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + 2;
			buttonUpY = ListCharsInMap.y + 1;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000A0770 File Offset: 0x0009E970
		private static void getButtonDown(out int buttonDownX, out int buttonDownY)
		{
			buttonDownX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + 2;
			buttonDownY = ListCharsInMap.y + 2 + ListCharsInMap.distanceBetweenLines * (ListCharsInMap.MAX_CHAR - 1);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000A079E File Offset: 0x0009E99E
		private static void getScrollBar(out int scrollBarWidth, out int scrollBarHeight, out int scrollBarThumbHeight)
		{
			scrollBarWidth = 9;
			scrollBarHeight = ListCharsInMap.MAX_CHAR * ListCharsInMap.distanceBetweenLines - 1 - 12;
			scrollBarThumbHeight = Mathf.CeilToInt((float)ListCharsInMap.MAX_CHAR / (float)ListCharsInMap.listChars.Count * (float)scrollBarHeight);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000A07D4 File Offset: 0x0009E9D4
		private static void getCollapseButton(out int collapseButtonX, out int collapseButtonY)
		{
			int num;
			int num2;
			int num3;
			ListCharsInMap.getScrollBar(out num, out num2, out num3);
			if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
			{
				num = 0;
			}
			collapseButtonX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num - 8;
			collapseButtonY = ListCharsInMap.y - ListCharsInMap.distanceBetweenLines + 1;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000A082B File Offset: 0x0009EA2B
		internal static void setState(bool value)
		{
			ListCharsInMap.isEnabled = value;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000A0833 File Offset: 0x0009EA33
		internal static void setStatePet(bool value)
		{
			ListCharsInMap.isShowPet = value;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000A083C File Offset: 0x0009EA3C
		internal static int getSpaceOccupied()
		{
			if (!ListCharsInMap.isEnabled)
			{
				return 0;
			}
			byte b = 5;
			byte b2 = 7;
			if (ListCharsInMap.isCollapsed)
			{
				return ListCharsInMap.distanceBetweenLines;
			}
			return (int)(b2 + b) + ListCharsInMap.distanceBetweenLines * Mathf.Clamp(ListCharsInMap.listChars.Count, 0, ListCharsInMap.MAX_CHAR);
		}

		// Token: 0x040014B4 RID: 5300
		internal static List<global::Char> listChars = new List<global::Char>();

		// Token: 0x040014B5 RID: 5301
		internal static bool isEnabled;

		// Token: 0x040014B6 RID: 5302
		internal static bool isShowPet;

		// Token: 0x040014B7 RID: 5303
		internal static int x = 6;

		// Token: 0x040014B8 RID: 5304
		internal static int y = 0;

		// Token: 0x040014B9 RID: 5305
		private static int maxLength = 0;

		// Token: 0x040014BA RID: 5306
		private static readonly int MAX_CHAR = 6;

		// Token: 0x040014BB RID: 5307
		private static int distanceBetweenLines = 8;

		// Token: 0x040014BC RID: 5308
		private static int offset = 0;

		// Token: 0x040014BD RID: 5309
		private static bool isCollapsed;

		// Token: 0x040014BE RID: 5310
		private static int titleWidth;

		// Token: 0x040014BF RID: 5311
		private static int offsetX;
	}
}
