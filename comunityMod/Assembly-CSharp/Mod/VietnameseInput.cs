using System;
using Mod.ModHelper.Menu;
using Mod.R;
using Vietpad.InputMethod;

namespace Mod
{
	// Token: 0x020000F1 RID: 241
	internal class VietnameseInput
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x000A21E0 File Offset: 0x000A03E0
		static VietnameseInput()
		{
			VietKeyHandler.VietModeEnabled = false;
			VietKeyHandler.SmartMark = true;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000A21F0 File Offset: 0x000A03F0
		internal static void ShowMenu()
		{
			new MenuBuilder().setChatPopup(string.Concat(new string[]
			{
				Strings.vnInputEnable,
				": ",
				Strings.OnOffStatus(VietKeyHandler.VietModeEnabled),
				Environment.NewLine,
				Strings.vnInputInputMethod,
				": ",
				Enum.GetName(typeof(InputMethods), VietKeyHandler.InputMethod),
				Environment.NewLine,
				Strings.vnInputDiacritics,
				": ",
				VietKeyHandler.DiacriticsPosClassic ? "òa, úy" : "oà, uý",
				Environment.NewLine,
				Strings.vnInputConsumeRepeatKey,
				": ",
				Strings.OnOffStatus(VietKeyHandler.ConsumeRepeatKey)
			})).addItem(Strings.vnInputEnable + ": " + Strings.OnOffStatus(VietKeyHandler.VietModeEnabled), new MenuAction(delegate
			{
				VietKeyHandler.VietModeEnabled = !VietKeyHandler.VietModeEnabled;
				GameScr.info1.addInfo(Strings.vnInputEnable + ": " + Strings.OnOffStatus(VietKeyHandler.VietModeEnabled), 0);
			})).addItem(Strings.vnInputInputMethod + ": " + Enum.GetName(typeof(InputMethods), VietKeyHandler.InputMethod), new MenuAction(delegate
			{
				VietKeyHandler.InputMethod++;
				if (VietKeyHandler.InputMethod > InputMethods.Auto)
				{
					VietKeyHandler.InputMethod = InputMethods.Telex;
				}
				GameScr.info1.addInfo(Strings.vnInputInputMethod + ": " + Enum.GetName(typeof(InputMethods), VietKeyHandler.InputMethod), 0);
			}))
				.addItem(Strings.vnInputDiacritics + ": " + (VietKeyHandler.DiacriticsPosClassic ? "òa, úy" : "oà, uý"), new MenuAction(delegate
				{
					VietKeyHandler.DiacriticsPosClassic = !VietKeyHandler.DiacriticsPosClassic;
					GameScr.info1.addInfo(Strings.vnInputDiacritics + ": " + (VietKeyHandler.DiacriticsPosClassic ? "òa, úy" : "oà, uý"), 0);
				}))
				.addItem(Strings.vnInputConsumeRepeatKey + ": " + Strings.OnOffStatus(VietKeyHandler.ConsumeRepeatKey), new MenuAction(delegate
				{
					VietKeyHandler.ConsumeRepeatKey = !VietKeyHandler.ConsumeRepeatKey;
					GameScr.info1.addInfo(Strings.vnInputConsumeRepeatKey + ": " + Strings.OnOffStatus(VietKeyHandler.ConsumeRepeatKey), 0);
				}))
				.addItem("Test", new MenuAction(delegate
				{
					ChatTextField.gI().strChat = "Test";
					ChatTextField.gI().tfChat.name = "Test";
					ChatTextField.gI().startChat2(new VietnameseInput.VietnameseInputChatable(), string.Empty);
				}))
				.start();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000A2410 File Offset: 0x000A0610
		internal static void LoadData()
		{
			bool flag;
			if (Utils.TryLoadDataBool("vn_input_enabled", out flag, true))
			{
				VietKeyHandler.VietModeEnabled = flag;
			}
			long num;
			if (Utils.TryLoadDataLong("vn_input_input_method", out num, true))
			{
				VietKeyHandler.InputMethod = (InputMethods)num;
			}
			bool flag2;
			if (Utils.TryLoadDataBool("vn_input_diacritics", out flag2, true))
			{
				VietKeyHandler.DiacriticsPosClassic = flag2;
			}
			bool flag3;
			if (Utils.TryLoadDataBool("vn_input_consume_repeat_key", out flag3, true))
			{
				VietKeyHandler.ConsumeRepeatKey = flag3;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000A2474 File Offset: 0x000A0674
		internal static void SaveData()
		{
			Utils.SaveData("vn_input_enabled", VietKeyHandler.VietModeEnabled, true);
			Utils.SaveData("vn_input_input_method", (long)VietKeyHandler.InputMethod, true);
			Utils.SaveData("vn_input_diacritics", VietKeyHandler.DiacriticsPosClassic, true);
			Utils.SaveData("vn_input_consume_repeat_key", VietKeyHandler.ConsumeRepeatKey, true);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000A24C4 File Offset: 0x000A06C4
		internal static bool ToVietnamese(string str, out string result, ref int caretPos, int inputType)
		{
			result = "";
			if (!VietKeyHandler.VietModeEnabled)
			{
				return false;
			}
			if (inputType != TField.INPUT_TYPE_ANY || str.StartsWith("/"))
			{
				return false;
			}
			result = VietKeyHandler.HandleTextInput(str, caretPos - 1);
			if (result != str)
			{
				caretPos -= str.Length - result.Length;
				return true;
			}
			return false;
		}

		// Token: 0x020000F2 RID: 242
		private class VietnameseInputChatable : IChatable
		{
			// Token: 0x06000D52 RID: 3410 RVA: 0x000A2524 File Offset: 0x000A0724
			public void onChatFromMe(string text, string to)
			{
				GameScr.info1.addInfo(text, 0);
				this.onCancelChat();
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x0009F278 File Offset: 0x0009D478
			public void onCancelChat()
			{
				ChatTextField.gI().ResetTF();
			}
		}
	}
}
