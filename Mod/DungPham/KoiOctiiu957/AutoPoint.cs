using System;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x02000112 RID: 274
	public class AutoPoint : IActionListener, IChatable
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x00009515 File Offset: 0x00007715
		public static AutoPoint getInstance()
		{
			if (AutoPoint._Instance == null)
			{
				AutoPoint._Instance = new AutoPoint();
			}
			return AutoPoint._Instance;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0000952D File Offset: 0x0000772D
		public static void Update()
		{
			if (AutoPoint.isAutoPoint)
			{
				AutoPoint.DoIt();
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000A7CF4 File Offset: 0x000A5EF4
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(AutoPoint.inputPointToAdd[0]))
				{
					try
					{
						int num = int.Parse(ChatTextField.gI().tfChat.getText());
						if ((AutoPoint.typePotential == 0 || AutoPoint.typePotential == 1) && num % 20 != 0)
						{
							GameScr.info1.addInfo("Chỉ Số HP, MP Phải chia hết cho 20. Vui Lòng Nhập Lại!", 0);
							return;
						}
						if (AutoPoint.typePotential == 0 || AutoPoint.typePotential == 1)
						{
							num /= 20;
						}
						Service.gI().upPotential(AutoPoint.typePotential, num);
						GameScr.info1.addInfo("Đã Cộng Xong!", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPoint.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPoint.inputPointAddTo[0]))
				{
					try
					{
						int num2 = int.Parse(ChatTextField.gI().tfChat.getText());
						if ((AutoPoint.typePotential == 0 || AutoPoint.typePotential == 1) && num2 % 20 != 0)
						{
							GameScr.info1.addInfo("Chỉ Số HP, MP Phải chia hết cho 20. Vui Lòng Nhập Lại!", 0);
							return;
						}
						if (AutoPoint.typePotential == 0 || AutoPoint.typePotential == 1)
						{
							num2 /= 20;
						}
						int num3 = global::Char.myCharz().cHPGoc / 20;
						if (AutoPoint.typePotential == 1)
						{
							num3 = global::Char.myCharz().cMPGoc / 20;
						}
						if (AutoPoint.typePotential == 2)
						{
							num3 = global::Char.myCharz().cDamGoc;
						}
						if (AutoPoint.typePotential == 3)
						{
							num3 = global::Char.myCharz().cDefGoc;
						}
						if (AutoPoint.typePotential == 4)
						{
							num3 = global::Char.myCharz().cCriticalGoc;
						}
						if (num2 <= num3)
						{
							GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
							return;
						}
						Service.gI().upPotential(AutoPoint.typePotential, num2 - num3);
						GameScr.info1.addInfo("Đã Cộng Xong!", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPoint.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPoint.inputDamageAuto[0]))
				{
					try
					{
						int num4 = AutoPoint.damageToAuto = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Auto Cộng Sức Đánh: " + NinjaUtil.getMoneys((long)num4), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Sức Đánh Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPoint.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPoint.inputHPAuto[0]))
				{
					try
					{
						int num5 = AutoPoint.hpToAuto = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Auto Cộng HP: " + NinjaUtil.getMoneys((long)num5), 0);
					}
					catch
					{
						GameScr.info1.addInfo("HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPoint.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPoint.inputMPAuto[0]))
				{
					try
					{
						int num6 = AutoPoint.mpToAuto = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Auto Cộng MP: " + NinjaUtil.getMoneys((long)num6), 0);
					}
					catch
					{
						GameScr.info1.addInfo("MP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPoint.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
				AutoPoint.ResetChatTextField();
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000A80A8 File Offset: 0x000A62A8
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoPoint.ShowMenuAutoPoint();
				return;
			case 2:
				AutoPoint.ShowMenuAutoPointFast();
				return;
			case 3:
				AutoPoint.isAutoPoint = !AutoPoint.isAutoPoint;
				GameScr.info1.addInfo("Auto\n" + (AutoPoint.isAutoPoint ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 4:
				ChatTextField.gI().strChat = AutoPoint.inputDamageAuto[0];
				ChatTextField.gI().tfChat.name = AutoPoint.inputDamageAuto[1];
				ChatTextField.gI().startChat2(AutoPoint.getInstance(), string.Empty);
				return;
			case 5:
				ChatTextField.gI().strChat = AutoPoint.inputHPAuto[0];
				ChatTextField.gI().tfChat.name = AutoPoint.inputHPAuto[1];
				ChatTextField.gI().startChat2(AutoPoint.getInstance(), string.Empty);
				return;
			case 6:
				ChatTextField.gI().strChat = AutoPoint.inputMPAuto[0];
				ChatTextField.gI().tfChat.name = AutoPoint.inputMPAuto[1];
				ChatTextField.gI().startChat2(AutoPoint.getInstance(), string.Empty);
				return;
			case 7:
				AutoPoint.ShowMenuAddPoint(0);
				return;
			case 8:
				AutoPoint.ShowMenuAddPoint(1);
				return;
			case 9:
				AutoPoint.ShowMenuAddPoint(2);
				return;
			case 10:
				AutoPoint.ShowMenuAddPoint(3);
				return;
			case 11:
				AutoPoint.ShowMenuAddPoint(4);
				return;
			case 12:
				AutoPoint.typePotential = (int)p;
				GameCanvas.panel.isShow = false;
				ChatTextField.gI().strChat = AutoPoint.inputPointToAdd[0];
				ChatTextField.gI().tfChat.name = AutoPoint.inputPointToAdd[1];
				ChatTextField.gI().startChat2(AutoPoint.getInstance(), string.Empty);
				return;
			case 13:
				AutoPoint.typePotential = (int)p;
				GameCanvas.panel.isShow = false;
				ChatTextField.gI().strChat = AutoPoint.inputPointAddTo[0];
				ChatTextField.gI().tfChat.name = AutoPoint.inputPointAddTo[1];
				ChatTextField.gI().startChat2(AutoPoint.getInstance(), string.Empty);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000A82B0 File Offset: 0x000A64B0
		public static void ShowMenu()
		{
			AutoPoint.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto\nCộng\nChỉ Số", AutoPoint.getInstance(), 1, null));
			myVector.addElement(new Command("Cộng\nChỉ Số\nNhanh", AutoPoint.getInstance(), 2, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000A8304 File Offset: 0x000A6504
		public static void ShowMenuAutoPoint()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto\n" + (AutoPoint.isAutoPoint ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoPoint.getInstance(), 3, null));
			myVector.addElement(new Command("Sức Đánh\n[" + NinjaUtil.getMoneys((long)AutoPoint.damageToAuto) + "]", AutoPoint.getInstance(), 4, null));
			myVector.addElement(new Command("HP\n[" + NinjaUtil.getMoneys((long)AutoPoint.hpToAuto) + "]", AutoPoint.getInstance(), 5, null));
			myVector.addElement(new Command("MP\n[" + NinjaUtil.getMoneys((long)AutoPoint.mpToAuto) + "]", AutoPoint.getInstance(), 6, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000A83D8 File Offset: 0x000A65D8
		public static void ShowMenuAutoPointFast()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("HP", AutoPoint.getInstance(), 7, null));
			myVector.addElement(new Command("MP", AutoPoint.getInstance(), 8, null));
			myVector.addElement(new Command("Sức Đánh", AutoPoint.getInstance(), 9, null));
			myVector.addElement(new Command("Giáp", AutoPoint.getInstance(), 10, null));
			myVector.addElement(new Command("Chí Mạng", AutoPoint.getInstance(), 11, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000A8470 File Offset: 0x000A6670
		public static void ShowMenuAddPoint(int typePotential)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Cộng", AutoPoint.getInstance(), 12, typePotential));
			myVector.addElement(new Command("Cộng\nTới Mức", AutoPoint.getInstance(), 13, typePotential));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000045ED File Offset: 0x000027ED
		private static void LoadData()
		{
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000045ED File Offset: 0x000027ED
		private static void smethod_0()
		{
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000A84CC File Offset: 0x000A66CC
		public static void DoIt()
		{
			if (global::Char.myCharz().cDamGoc < AutoPoint.damageToAuto)
			{
				if (global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cDamGoc * 100) && GameCanvas.gameTick % 20 == 0)
				{
					Service.gI().upPotential(2, 1);
					return;
				}
			}
			else if (global::Char.myCharz().cHPGoc < AutoPoint.hpToAuto)
			{
				if (global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cHPGoc + 1000) && GameCanvas.gameTick % 20 == 0)
				{
					Service.gI().upPotential(0, 1);
					return;
				}
			}
			else if (global::Char.myCharz().cMPGoc < AutoPoint.mpToAuto && global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cMPGoc + 1000) && GameCanvas.gameTick % 20 == 0)
			{
				Service.gI().upPotential(1, 1);
			}
		}

		// Token: 0x0400163A RID: 5690
		private static AutoPoint _Instance;

		// Token: 0x0400163B RID: 5691
		public static int typePotential;

		// Token: 0x0400163C RID: 5692
		public static bool isAutoPoint;

		// Token: 0x0400163D RID: 5693
		public static int damageToAuto;

		// Token: 0x0400163E RID: 5694
		public static int hpToAuto;

		// Token: 0x0400163F RID: 5695
		public static int mpToAuto;

		// Token: 0x04001640 RID: 5696
		public static string[] inputDamageAuto = new string[]
		{
			"Nhập Sức Đánh Mà Bạn Muốn Auto",
			"Sức Đánh"
		};

		// Token: 0x04001641 RID: 5697
		public static string[] inputHPAuto = new string[]
		{
			"Nhập HP Mà Bạn Muốn Auto",
			"HP"
		};

		// Token: 0x04001642 RID: 5698
		public static string[] inputMPAuto = new string[]
		{
			"Nhập MP Mà Bạn Muốn Auto",
			"MP"
		};

		// Token: 0x04001643 RID: 5699
		public static string[] inputPointToAdd = new string[]
		{
			"Nhập Chỉ Số Mà Bạn Muốn Cộng Thêm",
			"Chỉ Số"
		};

		// Token: 0x04001644 RID: 5700
		public static string[] inputPointAddTo = new string[]
		{
			"Nhập Chỉ Số Mà Bạn Muốn Cộng Tới",
			"Chỉ Số"
		};
	}
}
