using System;
using System.IO;
using System.Text.RegularExpressions;
using Mod.R;

namespace Mod.Auto.AutoChat
{
	// Token: 0x02000189 RID: 393
	public class Setup : IChatable
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		public static Setup gI { get; } = new Setup();

		// Token: 0x0600119B RID: 4507 RVA: 0x000BE504 File Offset: 0x000BC704
		public static void loadFile()
		{
			if (!File.Exists(Utils.PathAutoChat))
			{
				using (StreamWriter streamWriter = File.CreateText(Utils.PathAutoChat))
				{
					streamWriter.WriteLine(Strings.communityMod);
					streamWriter.WriteLine("6500");
				}
			}
			Setup.delayAutoChat = int.Parse(File.ReadAllLines(Utils.PathAutoChat)[1]);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000BE570 File Offset: 0x000BC770
		public static void clearStringTrash()
		{
			if (!File.Exists(Utils.PathChatHistory))
			{
				return;
			}
			string text = Regex.Replace(File.ReadAllText(Utils.PathChatHistory), ",?\\s*mcd\\d{2}\\:\\s*[^\"]*\"[^\"]*\"", "");
			File.WriteAllText(Utils.PathChatHistory, text);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000BE5AF File Offset: 0x000BC7AF
		public void onCancelChat()
		{
			ChatTextField.gI().isShow = false;
			ChatTextField.gI().ResetTF();
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000BE5C8 File Offset: 0x000BC7C8
		public void onChatFromMe(string text, string to)
		{
			string[] array = File.ReadAllLines(Utils.PathAutoChat);
			if (string.IsNullOrEmpty(ChatTextField.gI().tfChat.getText()) || string.IsNullOrEmpty(text))
			{
				return;
			}
			if (ChatTextField.gI().strChat.Contains(Setup.inputTextAutoChat[0]))
			{
				try
				{
					string text2 = ChatTextField.gI().tfChat.getText();
					array[0] = text2;
					File.WriteAllLines(Utils.PathAutoChat, array);
					GameCanvas.startOKDlg(Strings.contentSaved + ": " + text2);
					goto IL_0134;
				}
				catch
				{
					GameCanvas.startOKDlg(Strings.errorOccurred + "!");
					goto IL_0134;
				}
			}
			if (ChatTextField.gI().strChat.Contains(Setup.inputDelayAutoChat[0]))
			{
				try
				{
					string text3 = ChatTextField.gI().tfChat.getText();
					array[1] = text3;
					File.WriteAllLines(Utils.PathAutoChat, array);
					Setup.delayAutoChat = int.Parse(text3);
					if (Setup.delayAutoChat < 5000)
					{
						Setup.delayAutoChat = 5000;
					}
					GameScr.info1.addInfo(string.Format(Strings.valueChanged, "delay", (float)Setup.delayAutoChat / 1000f), 0);
				}
				catch
				{
					GameCanvas.startOKDlg(Strings.errorOccurred + "!");
				}
			}
			IL_0134:
			ChatTextField.gI().ResetTF();
		}

		// Token: 0x040018EE RID: 6382
		public static string[] inputTextAutoChat = new string[]
		{
			Strings.inputContent,
			""
		};

		// Token: 0x040018EF RID: 6383
		public static string[] inputDelayAutoChat = new string[]
		{
			Strings.inputDelay,
			Strings.timeMilliseconds + " (> 5000)"
		};

		// Token: 0x040018F0 RID: 6384
		public static int delayAutoChat = 5000;
	}
}
