using System;
using System.Collections.Generic;
using System.IO;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000B8 RID: 184
	public class AutoChat : IActionListener, IChatable
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x00082A16 File Offset: 0x00080C16
		public static AutoChat getInstance()
		{
			if (AutoChat._Instance == null)
			{
				AutoChat._Instance = new AutoChat();
			}
			return AutoChat._Instance;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00082A2E File Offset: 0x00080C2E
		public static void Update()
		{
			if (AutoChat.isAutoChatPublic)
			{
				AutoChat.ChatPublic();
			}
			if (AutoChat.isAutoChatGlobal)
			{
				AutoChat.ChatGlobal();
			}
			if (AutoChat.isAutoSpamChatGlobal)
			{
				AutoChat.SpamChatGlobal();
			}
			if (AutoChat.isAutoInbox)
			{
				AutoChat.ChatInbox();
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00082A60 File Offset: 0x00080C60
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() == null || ChatTextField.gI().tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
			{
				ChatTextField.gI().isShow = false;
				AutoChat.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoChat.inputDelayChatPublic[0]))
			{
				int num = 0;
				try
				{
					num = int.Parse(ChatTextField.gI().tfChat.getText());
					if (num > 0)
					{
						try
						{
							AutoChat.autoChatPublicContent = File.ReadAllText("Data\\TextChatPublic.ini");
							AutoChat.delayAutoChatPublic = (long)num;
							AutoChat.isAutoChatPublic = true;
							GameScr.info1.addInfo("Auto Chat: " + ((AutoChat.delayAutoChatPublic >= 5000L) ? NinjaUtil.getMoneys(AutoChat.delayAutoChatPublic) : "5.000") + " mili giây", 0);
							if (AutoChat.isSaveData)
							{
								Rms.saveRMSInt("AutoChatIsAutoChatPublic", 1);
								Rms.saveRMSString("AutoChatDelayChatPublic", AutoChat.delayAutoChatPublic.ToString());
							}
						}
						catch
						{
							GameScr.info1.addInfo("Lỗi đọc File!", 0);
						}
					}
					if (num <= 0)
					{
						GameCanvas.startOKDlg("Delay không hợp lệ!");
					}
				}
				catch
				{
					GameCanvas.startOKDlg("Delay không hợp lệ!");
				}
				AutoChat.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoChat.inputDelayChatGlobal[0]))
			{
				int num2 = 0;
				try
				{
					num2 = int.Parse(ChatTextField.gI().tfChat.getText());
					if (num2 > 0)
					{
						try
						{
							AutoChat.autoChatGlobalContent = File.ReadAllText("Data\\TextChatGlobal.ini");
							AutoChat.delayAutoChatGlobal = (long)num2;
							AutoChat.isAutoChatGlobal = true;
							AutoChat.isAutoSpamChatGlobal = false;
							GameScr.info1.addInfo("Auto Chat Thế Giới: " + NinjaUtil.getMoneys(AutoChat.delayAutoChatGlobal) + " mili giây", 0);
							if (AutoChat.isSaveData)
							{
								Rms.saveRMSInt("AutoChatIsAutoChatGlobal", 1);
								Rms.saveRMSString("AutoChatDelayChatGlobal", AutoChat.delayAutoChatGlobal.ToString());
							}
						}
						catch
						{
							GameScr.info1.addInfo("Lỗi đọc File!", 0);
						}
					}
					if (num2 <= 0)
					{
						GameCanvas.startOKDlg("Delay không hợp lệ!");
					}
				}
				catch
				{
					GameCanvas.startOKDlg("Delay không hợp lệ!");
				}
				AutoChat.ResetChatTextField();
				return;
			}
			if (!ChatTextField.gI().strChat.Equals(AutoChat.inputSpamChatGlobal[0]))
			{
				return;
			}
			int num3 = 0;
			try
			{
				num3 = int.Parse(ChatTextField.gI().tfChat.getText());
				if (num3 > 0)
				{
					try
					{
						AutoChat.autoChatGlobalContent = File.ReadAllText("Data\\TextChatGlobal.ini");
						AutoChat.timesSpamChatGlobal = num3;
						AutoChat.spammedChatGlobalTimes = 0;
						AutoChat.gems = global::Char.myCharz().luong + global::Char.myCharz().luongKhoa;
						AutoChat.isAutoChatGlobal = false;
						AutoChat.isAutoSpamChatGlobal = true;
						GameScr.info1.addInfo("Spam Chat Thế Giới: " + AutoChat.timesSpamChatGlobal.ToString() + " lần", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Lỗi đọc File!", 0);
					}
				}
				if (num3 <= 0)
				{
					GameCanvas.startOKDlg("Số lần không hợp lệ!");
				}
			}
			catch
			{
				GameCanvas.startOKDlg("Số lần không hợp lệ!");
			}
			AutoChat.ResetChatTextField();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00082DAC File Offset: 0x00080FAC
		public void onCancelChat()
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00082DB0 File Offset: 0x00080FB0
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				if (!AutoChat.isAutoChatPublic)
				{
					ChatTextField.gI().strChat = AutoChat.inputDelayChatPublic[0];
					ChatTextField.gI().tfChat.name = AutoChat.inputDelayChatPublic[1];
					ChatTextField.gI().startChat2(AutoChat.getInstance(), string.Empty);
				}
				if (AutoChat.isAutoChatPublic)
				{
					AutoChat.isAutoChatPublic = false;
					GameScr.info1.addInfo("Auto Chat\n[STATUS: OFF]", 0);
					if (AutoChat.isSaveData)
					{
						Rms.saveRMSInt("AutoChatIsAutoChatPublic", 0);
						return;
					}
				}
				break;
			case 2:
				if (!AutoChat.isAutoChatGlobal)
				{
					ChatTextField.gI().strChat = AutoChat.inputDelayChatGlobal[0];
					ChatTextField.gI().tfChat.name = AutoChat.inputDelayChatGlobal[1];
					ChatTextField.gI().startChat2(AutoChat.getInstance(), string.Empty);
				}
				if (AutoChat.isAutoChatGlobal)
				{
					AutoChat.isAutoChatGlobal = false;
					GameScr.info1.addInfo("Auto Chat Thế Giới\n[STATUS: OFF]", 0);
					if (AutoChat.isSaveData)
					{
						Rms.saveRMSInt("AutoChatIsAutoChatGlobal", 0);
						return;
					}
				}
				break;
			case 3:
				if (!AutoChat.isAutoSpamChatGlobal)
				{
					ChatTextField.gI().strChat = AutoChat.inputSpamChatGlobal[0];
					ChatTextField.gI().tfChat.name = AutoChat.inputSpamChatGlobal[1];
					ChatTextField.gI().startChat2(AutoChat.getInstance(), string.Empty);
				}
				if (AutoChat.isAutoSpamChatGlobal)
				{
					AutoChat.isAutoSpamChatGlobal = false;
					GameScr.info1.addInfo("Auto Spam Chat Thế Giới\n[STATUS: OFF]", 0);
					return;
				}
				break;
			case 4:
				if (AutoChat.isAutoInbox)
				{
					AutoChat.isAutoInbox = false;
					GameScr.info1.addInfo("Auto Inbox\n[STATUS: OFF]", 0);
				}
				else
				{
					try
					{
						AutoChat.autoInboxContent = File.ReadAllText("Data\\TextChatInbox.ini");
						AutoChat.isAutoInbox = true;
						GameScr.info1.addInfo("Auto Inbox\n[STATUS: ON]", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Lỗi đọc File!", 0);
					}
				}
				if (AutoChat.isSaveData)
				{
					Rms.saveRMSInt("AutoChatIsAutoChatInbox", AutoChat.isAutoInbox ? 1 : 0);
					return;
				}
				break;
			case 5:
				AutoChat.isSaveData = !AutoChat.isSaveData;
				GameScr.info1.addInfo("Lưu Cài Đặt\n" + (AutoChat.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				Rms.saveRMSInt("AutoChatIsSaveRms", AutoChat.isSaveData ? 1 : 0);
				if (AutoChat.isSaveData)
				{
					AutoChat.SaveData();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0008300C File Offset: 0x0008120C
		public static void ShowMenu()
		{
			AutoChat.LoadSavedData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Chat\n" + (AutoChat.isAutoChatPublic ? ("[" + NinjaUtil.getMoneys(AutoChat.delayAutoChatPublic) + " mili giây]") : "[STATUS: OFF]"), AutoChat.getInstance(), 1, null));
			myVector.addElement(new Command("Auto Chat Thế Giới\n" + (AutoChat.isAutoChatGlobal ? ("[" + NinjaUtil.getMoneys(AutoChat.delayAutoChatGlobal) + " mili giây]") : "[STATUS: OFF]"), AutoChat.getInstance(), 2, null));
			myVector.addElement(new Command("Auto Spam Chat Thế Giới\n" + (AutoChat.isAutoSpamChatGlobal ? string.Concat(new string[]
			{
				"[",
				AutoChat.spammedChatGlobalTimes.ToString(),
				"/",
				AutoChat.timesSpamChatGlobal.ToString(),
				"]"
			}) : "[STATUS: OFF]"), AutoChat.getInstance(), 3, null));
			myVector.addElement(new Command("Auto Inbox\n" + (AutoChat.isAutoInbox ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoChat.getInstance(), 4, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (AutoChat.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoChat.getInstance(), 5, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0008317B File Offset: 0x0008137B
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000831AC File Offset: 0x000813AC
		private static void LoadSavedData()
		{
			AutoChat.isSaveData = (Rms.loadRMSInt("AutoChatIsSaveRms") == 1);
			if (AutoChat.isSaveData)
			{
				AutoChat.isAutoChatPublic = (Rms.loadRMSInt("AutoChatIsAutoChatPublic") == 1);
				AutoChat.isAutoChatGlobal = (Rms.loadRMSInt("AutoChatIsAutoChatGlobal") == 1);
				AutoChat.isAutoInbox = (Rms.loadRMSInt("AutoChatIsAutoChatInbox") == 1);
				if (AutoChat.isAutoChatPublic)
				{
					AutoChat.delayAutoChatPublic = long.Parse(Rms.loadRMSString("AutoChatDelayChatPublic"));
				}
				if (AutoChat.isAutoChatGlobal)
				{
					AutoChat.delayAutoChatGlobal = long.Parse(Rms.loadRMSString("AutoChatDelayChatGlobal"));
				}
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00083240 File Offset: 0x00081440
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoChatIsAutoChatPublic", AutoChat.isAutoChatPublic ? 1 : 0);
			Rms.saveRMSInt("AutoChatIsAutoChatGlobal", AutoChat.isAutoChatGlobal ? 1 : 0);
			Rms.saveRMSInt("AutoChatIsAutoChatInbox", AutoChat.isAutoInbox ? 1 : 0);
			if (AutoChat.isAutoChatPublic)
			{
				Rms.saveRMSString("AutoChatDelayChatPublic", AutoChat.delayAutoChatPublic.ToString());
			}
			if (AutoChat.isAutoChatGlobal)
			{
				Rms.saveRMSString("AutoChatDelayChatGlobal", AutoChat.delayAutoChatGlobal.ToString());
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000832C4 File Offset: 0x000814C4
		private static void ChatPublic()
		{
			if (AutoChat.delayAutoChatPublic < 5000L)
			{
				AutoChat.delayAutoChatPublic = 5000L;
			}
			if (AutoChat.autoChatPublicContent == null || AutoChat.autoChatPublicContent.Equals(""))
			{
				try
				{
					AutoChat.autoChatPublicContent = File.ReadAllText("Data\\TextChatPublic.ini");
				}
				catch
				{
					AutoChat.autoChatPublicContent = "Dũng đẹp trai!";
				}
			}
			if (mSystem.currentTimeMillis() - AutoChat.lastTimeChatPublic > AutoChat.delayAutoChatPublic)
			{
				AutoChat.lastTimeChatPublic = mSystem.currentTimeMillis();
				Service.gI().chat("(" + Res.random(100, 999).ToString() + "dp) " + AutoChat.autoChatPublicContent);
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00083380 File Offset: 0x00081580
		private static void ChatGlobal()
		{
			int num = global::Char.myCharz().luong + global::Char.myCharz().luongKhoa;
			if (num < 5)
			{
				AutoChat.isAutoChatGlobal = false;
				GameScr.info1.addInfo("Bạn không đủ ngọc để chat!", 0);
				return;
			}
			if (AutoChat.delayAutoChatGlobal <= 0L)
			{
				AutoChat.delayAutoChatGlobal = 5000L;
			}
			if (AutoChat.autoChatGlobalContent == null || AutoChat.autoChatGlobalContent.Equals(""))
			{
				try
				{
					AutoChat.autoChatGlobalContent = File.ReadAllText("Data\\TextChatGlobal.ini");
				}
				catch
				{
					AutoChat.autoChatGlobalContent = "Dũng đẹp trai!";
				}
			}
			if (AutoChat.gems == num && mSystem.currentTimeMillis() - AutoChat.lastTimeChatGlobal > 1000L)
			{
				AutoChat.lastTimeChatGlobal = mSystem.currentTimeMillis() - AutoChat.delayAutoChatGlobal - 500L;
			}
			if (mSystem.currentTimeMillis() - AutoChat.lastTimeChatGlobal > AutoChat.delayAutoChatGlobal)
			{
				AutoChat.lastTimeChatGlobal = mSystem.currentTimeMillis();
				AutoChat.countChatGlobal++;
				AutoChat.gems = num;
				Service.gI().chatGlobal(string.Concat(new string[]
				{
					AutoChat.countChatGlobal.ToString(),
					"dp: ",
					AutoChat.autoChatGlobalContent,
					" ",
					Res.random(100000, 999999).ToString()
				}));
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000834D0 File Offset: 0x000816D0
		private static void SpamChatGlobal()
		{
			int num = global::Char.myCharz().luong + global::Char.myCharz().luongKhoa;
			if (num < 5 || AutoChat.timesSpamChatGlobal <= 0 || AutoChat.spammedChatGlobalTimes >= AutoChat.timesSpamChatGlobal)
			{
				AutoChat.isAutoSpamChatGlobal = false;
			}
			if (AutoChat.autoChatGlobalContent == null || AutoChat.autoChatGlobalContent.Equals(""))
			{
				try
				{
					AutoChat.autoChatGlobalContent = File.ReadAllText("Data\\TextChatGlobal.ini");
				}
				catch
				{
					AutoChat.autoChatGlobalContent = "Dũng đẹp trai!";
				}
			}
			if (AutoChat.gems != num)
			{
				AutoChat.gems = num;
				AutoChat.spammedChatGlobalTimes++;
			}
			if (mSystem.currentTimeMillis() - AutoChat.lastTimeChatGlobal >= 150L)
			{
				AutoChat.lastTimeChatGlobal = mSystem.currentTimeMillis();
				AutoChat.countChatGlobal++;
				AutoChat.gems = num;
				Service.gI().chatGlobal(string.Concat(new string[]
				{
					AutoChat.countChatGlobal.ToString(),
					"dp: ",
					AutoChat.autoChatGlobalContent,
					" ",
					Res.random(100000, 999999).ToString()
				}));
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000835F8 File Offset: 0x000817F8
		private static void ChatInbox()
		{
			if (AutoChat.listCharInbox == null)
			{
				AutoChat.listCharInbox = new List<int>();
			}
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (AutoChat.isInboxable(@char) && !AutoChat.listCharInbox.Contains(@char.charID))
				{
					AutoChat.listCharInbox.Add(@char.charID);
				}
			}
			if (AutoChat.autoInboxContent == null || AutoChat.autoInboxContent.Equals(""))
			{
				try
				{
					AutoChat.autoInboxContent = File.ReadAllText("Data\\TextChatInbox.ini");
				}
				catch
				{
					AutoChat.autoInboxContent = "Dũng đẹp trai!";
				}
			}
			if (AutoChat.listCharInbox.Count > 0 && mSystem.currentTimeMillis() - AutoChat.lastTimeInbox > 2000L)
			{
				AutoChat.lastTimeInbox = mSystem.currentTimeMillis();
				AutoChat.countChatInbox++;
				Service.gI().chatPlayer(string.Concat(new string[]
				{
					AutoChat.countChatInbox.ToString(),
					"dp: ",
					AutoChat.autoInboxContent,
					" ",
					Res.random(100000, 999999).ToString()
				}), AutoChat.listCharInbox[0]);
				AutoChat.listCharInbox.RemoveAt(0);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00083754 File Offset: 0x00081954
		private static bool isInboxable(global::Char ch)
		{
			return ch != null && ch.cName != null && !ch.cName.Equals("") && !char.IsUpper(char.Parse(ch.cName.Substring(0, 1))) && !ch.isPet && !ch.isMiniPet && !ch.cName.StartsWith("#") && !ch.cName.StartsWith("$");
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000837D0 File Offset: 0x000819D0
		static AutoChat()
		{
			AutoChat.LoadSavedData();
		}

		// Token: 0x04001261 RID: 4705
		private static AutoChat _Instance;

		// Token: 0x04001262 RID: 4706
		private static bool isAutoChatPublic;

		// Token: 0x04001263 RID: 4707
		private static long delayAutoChatPublic;

		// Token: 0x04001264 RID: 4708
		private static long lastTimeChatPublic;

		// Token: 0x04001265 RID: 4709
		private static string autoChatPublicContent;

		// Token: 0x04001266 RID: 4710
		private static bool isAutoChatGlobal;

		// Token: 0x04001267 RID: 4711
		private static long delayAutoChatGlobal;

		// Token: 0x04001268 RID: 4712
		private static long lastTimeChatGlobal;

		// Token: 0x04001269 RID: 4713
		private static string autoChatGlobalContent;

		// Token: 0x0400126A RID: 4714
		private static int countChatGlobal;

		// Token: 0x0400126B RID: 4715
		private static int gems;

		// Token: 0x0400126C RID: 4716
		private static bool isAutoSpamChatGlobal;

		// Token: 0x0400126D RID: 4717
		private static int spammedChatGlobalTimes;

		// Token: 0x0400126E RID: 4718
		private static int timesSpamChatGlobal;

		// Token: 0x0400126F RID: 4719
		public static bool isAutoInbox;

		// Token: 0x04001270 RID: 4720
		public static List<int> listCharInbox = new List<int>();

		// Token: 0x04001271 RID: 4721
		private static string autoInboxContent;

		// Token: 0x04001272 RID: 4722
		private static long lastTimeInbox;

		// Token: 0x04001273 RID: 4723
		private static int countChatInbox;

		// Token: 0x04001274 RID: 4724
		private static bool isSaveData;

		// Token: 0x04001275 RID: 4725
		private static string[] inputDelayChatPublic = new string[]
		{
			"Nhập delay chat:",
			"Mili giây (>=5000ms)"
		};

		// Token: 0x04001276 RID: 4726
		private static string[] inputDelayChatGlobal = new string[]
		{
			"Nhập delay chat thế giới:",
			"Mili giây (>=5000ms)"
		};

		// Token: 0x04001277 RID: 4727
		private static string[] inputSpamChatGlobal = new string[]
		{
			"Nhập số lần spam",
			"Số lần"
		};
	}
}
