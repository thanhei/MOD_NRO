using System;
using Mod.Constants;
using Mod.CustomPanel;
using Mod.R;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000DC RID: 220
	internal static class ExtensionMethods
	{
		// Token: 0x06000B63 RID: 2915 RVA: 0x00097514 File Offset: 0x00095714
		internal static void EmulateSetTypePanel(this Panel panel, int position)
		{
			panel.typeShop = -1;
			panel.W = Panel.WIDTH_PANEL;
			panel.H = GameCanvas.h;
			panel.X = 0;
			panel.Y = 0;
			panel.ITEM_HEIGHT = 24;
			panel.position = position;
			if (position == 0)
			{
				panel.xScroll = 2;
				panel.yScroll = 80;
				panel.wScroll = panel.W - 4;
				panel.hScroll = panel.H - 96;
				panel.cmx = (panel.cmtoX = 0);
				panel.X = 0;
			}
			else if (position == 1)
			{
				panel.wScroll = panel.W - 4;
				panel.xScroll = GameCanvas.w - panel.wScroll;
				panel.yScroll = 80;
				panel.hScroll = panel.H - 96;
				panel.X = panel.xScroll - 2;
				panel.cmx = (panel.cmtoX = GameCanvas.w - panel.W);
			}
			panel.TAB_W = panel.W / 5 - 1;
			if (panel.currentTabName.Length < 5)
			{
				panel.TAB_W += 5;
			}
			panel.startTabPos = panel.xScroll + panel.wScroll / 2 - panel.currentTabName.Length * panel.TAB_W / 2;
			panel.cmyLast = new int[panel.currentTabName.Length];
			int[] array = new int[panel.currentTabName.Length];
			for (int i = 0; i < panel.currentTabName.Length; i++)
			{
				array[i] = (GameCanvas.isTouch ? (-1) : 0);
			}
			panel.lastSelect = array;
			panel.scroll = null;
			panel.lastTabIndex[CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU] = panel.currentTabIndex;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000976BC File Offset: 0x000958BC
		internal static string GetFullInfo(this Item item)
		{
			string text = item.template.name;
			if (item.itemOption != null)
			{
				for (int i = 0; i < item.itemOption.Length; i++)
				{
					if (item.itemOption[i].optionTemplate.id == 72)
					{
						text = string.Format("{0} [+{1}]", text, item.itemOption[i].param);
						break;
					}
				}
			}
			if (item.itemOption != null)
			{
				for (int j = 0; j < item.itemOption.Length; j++)
				{
					if (item.itemOption[j].optionTemplate.name.StartsWith("$"))
					{
						string optiongColor = item.itemOption[j].getOptiongColor();
						if (item.itemOption[j].param == 1)
						{
							text = text + "\n" + optiongColor;
						}
						if (item.itemOption[j].param == 0)
						{
							text = text + "\n" + optiongColor;
						}
					}
					else
					{
						string optionString = item.itemOption[j].getOptionString();
						if (!string.IsNullOrEmpty(optionString) && item.itemOption[j].optionTemplate.id != 72)
						{
							text = text + "\n" + optionString;
						}
					}
				}
			}
			if (item.template.strRequire > 1)
			{
				text += string.Format("\n{0}: {1}", mResources.pow_request, item.template.strRequire);
			}
			return text + "\n" + item.template.description;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00097837 File Offset: 0x00095A37
		internal static T getValueProperty<T>(this object obj, string name)
		{
			return (T)((object)obj.GetType().GetProperty(name).GetValue(obj, null));
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00097851 File Offset: 0x00095A51
		internal static void ResetTF(this ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00097890 File Offset: 0x00095A90
		internal static void ResetSize(this GameCanvas gameCanvas)
		{
			GameCanvas.w = MotherCanvas.instance.getWidthz();
			GameCanvas.h = MotherCanvas.instance.getHeightz();
			GameCanvas.hw = GameCanvas.w / 2;
			GameCanvas.hh = GameCanvas.h / 2;
			GameCanvas.wd3 = GameCanvas.w / 3;
			GameCanvas.hd3 = GameCanvas.h / 3;
			GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
			GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
			GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
			GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
			GameCanvas.wd6 = GameCanvas.w / 6;
			GameCanvas.hd6 = GameCanvas.h / 6;
			GameCanvas.isTouch = true;
			if (GameCanvas.w >= 240)
			{
				GameCanvas.isTouchControl = true;
			}
			if (GameCanvas.w < 320)
			{
				GameCanvas.isTouchControlSmallScreen = true;
				GameCanvas.isTouchControlLargeScreen = false;
			}
			if (GameCanvas.w >= 320)
			{
				GameCanvas.isTouchControlSmallScreen = false;
				GameCanvas.isTouchControlLargeScreen = true;
			}
			if (GameCanvas.h <= 160)
			{
				Paint.hTab = 15;
				mScreen.cmdH = 17;
			}
			GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
			Panel.WIDTH_PANEL = 176;
			if (Panel.WIDTH_PANEL > GameCanvas.w)
			{
				Panel.WIDTH_PANEL = GameCanvas.w;
			}
			Panel panel = GameCanvas.panel;
			Utils.ResetTextField((panel != null) ? panel.chatTField : null);
			Panel panel2 = GameCanvas.panel2;
			Utils.ResetTextField((panel2 != null) ? panel2.chatTField : null);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00097A0C File Offset: 0x00095C0C
		internal static void SetGamePadZone(this GamePad gamePad)
		{
			gamePad.isSmallGamePad = GameCanvas.w < 320;
			gamePad.isMediumGamePad = GameCanvas.w >= 320 && GameCanvas.w <= 400;
			gamePad.isLargeGamePad = GameCanvas.w > 400;
			gamePad.xZone = 0;
			if (!gamePad.isLargeGamePad)
			{
				gamePad.wZone = GameCanvas.hw / 3 * 2;
				gamePad.yZone = GameCanvas.h / 2;
				gamePad.hZone = GameCanvas.h - 80;
				return;
			}
			gamePad.wZone = GameCanvas.hw / 3;
			gamePad.yZone = GameCanvas.hh / 2;
			gamePad.hZone = GameCanvas.h;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00097AC0 File Offset: 0x00095CC0
		internal static bool IsFromMyClan(this global::Char ch)
		{
			if (global::Char.myCharz().clan == null)
			{
				return false;
			}
			if (ch.charID == global::Char.myCharz().charID)
			{
				return true;
			}
			if (ch.charID == -global::Char.myCharz().charID)
			{
				return true;
			}
			if (ch.IsPet())
			{
				ch = GameScr.findCharInMap(-ch.charID);
				if (ch == null)
				{
					return false;
				}
			}
			return ch.clanID == global::Char.myCharz().clan.ID;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00097B38 File Offset: 0x00095D38
		internal static int GetTimeHold(this global::Char ch)
		{
			int num = 35;
			try
			{
				if (!ch.me)
				{
					num = 35;
					if (ch.charEffectTime.isTiedByMe && global::Char.myCharz().cgender == 2)
					{
						num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]).point * 5;
					}
				}
				else if (global::Char.myCharz().cgender == 2)
				{
					num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]).point * 5;
				}
			}
			catch
			{
				num = 35;
			}
			return num;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00097BDC File Offset: 0x00095DDC
		internal static int GetTimeMonkey(this global::Char ch)
		{
			int num = 60;
			try
			{
				if (!ch.me)
				{
					switch (ch.head)
					{
					case 192:
						num = 60;
						break;
					case 195:
						num = 70;
						break;
					case 196:
						num = 80;
						break;
					case 197:
						num = 100;
						break;
					case 198:
						num = 120;
						break;
					case 199:
						num = 90;
						break;
					case 200:
						num = 110;
						break;
					}
				}
				else if (global::Char.myCharz().cgender == 2)
				{
					num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[3]).point * 10 + 50;
				}
			}
			catch
			{
				num = 121;
			}
			return num;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00097CA0 File Offset: 0x00095EA0
		internal static int GetTimeShield(this global::Char ch)
		{
			int num;
			try
			{
				if (!ch.me)
				{
					num = 45;
				}
				else
				{
					num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[7]).point * 5 + 10;
				}
			}
			catch
			{
				num = 45;
			}
			return num;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00097CFC File Offset: 0x00095EFC
		internal static int GetTimeMobMe(this global::Char ch)
		{
			int num = 60;
			try
			{
				if (!ch.me)
				{
					sbyte mobTemplateId = ch.mobMe.getTemplate().mobTemplateId;
					if (mobTemplateId <= 25)
					{
						if (mobTemplateId != 8)
						{
							if (mobTemplateId != 11)
							{
								if (mobTemplateId == 25)
								{
									num = 165;
								}
							}
							else
							{
								num = 95;
							}
						}
						else
						{
							num = 60;
						}
					}
					else if (mobTemplateId <= 43)
					{
						if (mobTemplateId != 32)
						{
							if (mobTemplateId == 43)
							{
								num = 200;
							}
						}
						else
						{
							num = 130;
						}
					}
					else if (mobTemplateId != 49)
					{
						if (mobTemplateId == 50)
						{
							num = 270;
						}
					}
					else
					{
						num = 235;
					}
				}
				else if (global::Char.myCharz().cgender == 1)
				{
					num = (global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[4]).point - 1) * 35 + 60;
				}
			}
			catch
			{
				num = 270;
			}
			return num;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00097DE0 File Offset: 0x00095FE0
		internal static int GetTimeHypnotize(this global::Char ch)
		{
			int num = 12;
			try
			{
				if (!ch.me)
				{
					num = 12;
					if (ch.charEffectTime.isHypnotizedByMe)
					{
						num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]).point + 5;
					}
				}
				else if (global::Char.myCharz().cgender == 0)
				{
					num = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]).point + 5;
				}
			}
			catch
			{
				num = 12;
			}
			return num;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00097E78 File Offset: 0x00096078
		internal static int GetTimeStone(this global::Char ch)
		{
			return 5;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00097E7B File Offset: 0x0009607B
		internal static int GetTimeWhistle(this global::Char ch)
		{
			return 31;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00097E7B File Offset: 0x0009607B
		internal static int GetTimeChocolate(this global::Char ch)
		{
			return 31;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00097E7F File Offset: 0x0009607F
		internal static int GetTimeSelfExplode(this global::Char ch)
		{
			return 3;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00097E7F File Offset: 0x0009607F
		internal static int GetTimeQCKK(this global::Char ch)
		{
			return 3;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00097E84 File Offset: 0x00096084
		internal static int GetTimeBlind(this global::Char ch)
		{
			if (ch.me && ch.cgender == 0)
			{
				int num = ch.getSkill(ch.nClass.skillTemplates[2]).point;
				if (Utils.isMeWearingTXHSet())
				{
					num *= 2;
				}
				return num - 1;
			}
			return ch.freezSeconds - 1;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00097ED4 File Offset: 0x000960D4
		internal static string GetNameWithoutClanTag(this global::Char ch, bool enableRichText = false)
		{
			string text = ch.cName.Remove(0, ch.cName.IndexOf(']') + 1).TrimStart(new char[] { ' ', '#', '$' });
			if (enableRichText)
			{
				if (ch.IsPet())
				{
					text = "<color=cyan>" + text + "</color>";
				}
				else if (ch.IsBoss())
				{
					text = string.Format("<color=red><size={0}>{1}</size></color>", 7 * mGraphics.zoomLevel, text);
				}
				else
				{
					text = "<color=yellow>" + text + "</color>";
				}
			}
			return text;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00097F65 File Offset: 0x00096165
		internal static string GetClanTag(this global::Char ch)
		{
			return ch.cName.Substring(0, ch.cName.IndexOf(']') + 1);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00097F84 File Offset: 0x00096184
		internal static bool IsNormalChar(this global::Char ch, bool isIncludeBoss = false, bool isIncludePet = false)
		{
			bool flag = !string.IsNullOrEmpty(ch.cName) && ch.cName != LocalizedString.arbitration;
			if (!string.IsNullOrEmpty(ch.cName))
			{
				if (!isIncludeBoss)
				{
					flag = flag && !char.IsUpper(ch.GetNameWithoutClanTag(false)[0]);
				}
				if (!isIncludePet)
				{
					flag = flag && !ch.IsPet();
				}
			}
			return flag;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00097FF2 File Offset: 0x000961F2
		internal static bool IsBoss(this global::Char ch)
		{
			return !ch.IsPet() && ch.cName != LocalizedString.arbitration && char.IsUpper(ch.GetNameWithoutClanTag(false)[0]);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00098022 File Offset: 0x00096222
		internal static bool IsPet(this global::Char ch)
		{
			return ch.isPet || ch.isMiniPet || ch.cName.StartsWith("#") || ch.cName.StartsWith("$");
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00098058 File Offset: 0x00096258
		internal static global::Char ClosestChar(this global::Char ch, int maxDistance, bool isNormalCharOnly)
		{
			int num = int.MaxValue;
			global::Char @char = null;
			if (GameScr.vCharInMap.size() <= 0)
			{
				return null;
			}
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (char2 != ch && (!isNormalCharOnly || char2.IsNormalChar(false, false)))
				{
					int num2 = Res.distance(ch.cx, ch.cy, char2.cx, char2.cy);
					if (!char2.me && num2 < num)
					{
						num = num2;
						@char = char2;
					}
				}
			}
			if (@char != null && Res.distance(ch.cx, ch.cy, @char.cx, @char.cy) > maxDistance)
			{
				@char = null;
			}
			return @char;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0009810C File Offset: 0x0009630C
		internal static string GetGender(this global::Char ch, bool enableRichText = false)
		{
			if (enableRichText)
			{
				if (ch.cgender == 0)
				{
					return "<color=#0080ffff>TĐ</color>";
				}
				if (ch.cgender == 1)
				{
					return "<color=#00c000ff>NM</color>";
				}
				if (ch.cgender == 2)
				{
					return "<color=#ffff80ff>XD</color>";
				}
				return "<color=magenta>BĐ</color>";
			}
			else
			{
				if (ch.cgender == 0)
				{
					return "TĐ";
				}
				if (ch.cgender == 1)
				{
					return "NM";
				}
				if (ch.cgender == 2)
				{
					return "XD";
				}
				return "BĐ";
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00098180 File Offset: 0x00096380
		internal static Color GetFlagColor(this global::Char ch)
		{
			switch (ch.cFlag)
			{
			case 1:
				return Color.cyan;
			case 2:
				return Color.red;
			case 3:
				return new Color(0.56f, 0.19f, 0.77f);
			case 4:
				return Color.yellow;
			case 5:
				return Color.green;
			case 6:
				return Color.magenta;
			case 7:
				return new Color(1f, 0.5f, 0f);
			case 8:
				return new Color(0.18f, 0.18f, 0.18f);
			case 9:
				return Color.blue;
			case 10:
				return Color.red;
			case 11:
				return Color.blue;
			case 12:
				return Color.white;
			case 13:
				return Color.black;
			default:
				return Color.clear;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00098252 File Offset: 0x00096452
		internal static bool CanUse(this Skill skill)
		{
			return mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown && skill.HasManaToUseSkill();
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00098273 File Offset: 0x00096473
		internal static bool HasManaToUseSkill(this Skill skill)
		{
			return skill.template.manaUseType == 1 && global::Char.myCharz().cMP >= global::Char.myCharz().cMPFull * (skill.manaUse / 100);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000982A8 File Offset: 0x000964A8
		internal static bool IsCharDead(this global::Char ch)
		{
			return ch.isDie || ch.cHP <= 0 || ch.statusMe == 14;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000982C7 File Offset: 0x000964C7
		internal static int GetPetId(this global::Char ch)
		{
			return -ch.charID;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000982D0 File Offset: 0x000964D0
		internal static ItemOption GetBestItemOption(this Item item)
		{
			ItemOption itemOption = null;
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				ItemOption itemOption2 = item.itemOption[i];
				if (itemOption2.optionTemplate != null)
				{
					if (itemOption2.optionTemplate.id >= 127 && itemOption2.optionTemplate.id <= 135)
					{
						itemOption = itemOption2;
						break;
					}
					if (itemOption2.optionTemplate.id <= 36 && itemOption2.optionTemplate.id >= 34)
					{
						itemOption = itemOption2;
					}
					else if (itemOption == null || itemOption.optionTemplate == null || itemOption.optionTemplate.id > 36 || itemOption.optionTemplate.id < 34)
					{
						if (itemOption2.optionTemplate.id == 72)
						{
							itemOption = itemOption2;
						}
						else if ((itemOption == null || itemOption.optionTemplate == null || (itemOption != null && itemOption.optionTemplate != null && itemOption.optionTemplate.id == 72 && itemOption.param < (int)Math.Ceiling((double)itemOption2.param / 2.0))) && itemOption2.optionTemplate.id == 107)
						{
							itemOption = itemOption2;
						}
					}
				}
			}
			return itemOption;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000983EB File Offset: 0x000965EB
		internal static string GetName(this ScaleMode scaleMode)
		{
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				return Strings.scaleModeStretchToFill;
			case ScaleMode.ScaleAndCrop:
				return Strings.scaleModeScaleAndCrop;
			case ScaleMode.ScaleToFit:
				return Strings.scaleModeScaleToFit;
			default:
				return "Unknown";
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00098418 File Offset: 0x00096618
		internal static bool IsNRD(this ItemMap item)
		{
			return item.template.id >= 372 && item.template.id <= 378;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00098444 File Offset: 0x00096644
		internal static void SetColor(this mGraphics g, uint color)
		{
			uint num = color & 255U;
			uint num2 = (color >> 8) & 255U;
			uint num3 = (color >> 16) & 255U;
			uint num4 = (color >> 24) & 255U;
			g.a = num / 255f;
			g.b = num2 / 256f;
			g.g = num3 / 256f;
			g.r = num4 / 256f;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000984B5 File Offset: 0x000966B5
		internal static int GetX(this Waypoint waypoint)
		{
			if (waypoint.maxX < 60)
			{
				return 15;
			}
			if ((int)waypoint.minX <= TileMap.pxw - 60)
			{
				return (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
			}
			return TileMap.pxw - 15;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000984F4 File Offset: 0x000966F4
		internal static int GetXInsideMap(this Waypoint waypoint)
		{
			if (waypoint.maxX < (short)TileMap.size)
			{
				return (int)TileMap.size;
			}
			if ((int)waypoint.minX <= TileMap.pxw - (int)TileMap.size)
			{
				return (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
			}
			return TileMap.pxw - (int)TileMap.size;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00098549 File Offset: 0x00096749
		internal static int GetY(this Waypoint waypoint)
		{
			return (int)waypoint.maxY;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00098554 File Offset: 0x00096754
		internal static bool IsWearableAndVip(this Item item)
		{
			if (item.template.type <= ItemTemplateType.Radar || item.template.type == ItemTemplateType.TrainingSuite)
			{
				for (int i = 0; i < item.itemOption.Length; i++)
				{
					ItemOption itemOption = item.itemOption[i];
					if ((itemOption.optionTemplate.id >= 127 && itemOption.optionTemplate.id <= 135) || (itemOption.optionTemplate.id <= 36 && itemOption.optionTemplate.id >= 34) || itemOption.optionTemplate.name.StartsWith("$") || itemOption.optionTemplate.id == 107)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
