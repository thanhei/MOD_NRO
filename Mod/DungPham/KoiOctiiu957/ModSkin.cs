using Assets.src.g;

namespace Mod.DungPham.KoiOctiiu957
{
	public class ModSkin : IActionListener, IChatable
	{
		public static ModSkin getInstance()
		{
			if (ModSkin.instance == null)
			{
				ModSkin.instance = new ModSkin();
			}
			return ModSkin.instance;
		}

		public static void Load()
		{
			ModSkin.LoadSkinMods();
		}

		public static void Update()
		{
			global::Char @char = global::Char.myCharz();
			if (@char == null)
			{
				return;
			}
			ModSkin.EnsureSkinModsResolved();
			if (ModSkin.modHeadPart != -1)
			{
				@char.head = ModSkin.modHeadPart;
			}
			if (ModSkin.modBodyPart != -1)
			{
				@char.body = ModSkin.modBodyPart;
			}
			if (ModSkin.modLegPart != -1)
			{
				@char.leg = ModSkin.modLegPart;
			}
			if (ModSkin.modBackPart != -1)
			{
				@char.bag = ModSkin.modBackPart;
			}
			if (ModSkin.modPetSmallId != -1)
			{
				if (@char.petFollow == null)
				{
					@char.petFollow = new PetFollow();
				}
				@char.petFollow.smallID = (short)ModSkin.modPetSmallId;
			}
		}

		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinHead[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 0);
					ModSkin.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinBody[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 1);
					ModSkin.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinLeg[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 2);
					ModSkin.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinBack[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 3);
					ModSkin.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinPet[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 4);
					ModSkin.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(ModSkin.inputSkinBoard[0]))
				{
					ModSkin.HandleInput(ChatTextField.gI().tfChat.getText(), 5);
					ModSkin.ResetChatTextField();
					return;
				}
			}
			else
			{
				ModSkin.ResetChatTextField();
			}
		}

		public void onCancelChat()
		{
		}

		public static bool TryGetBoardMountId(out short mountId)
		{
			ModSkin.EnsureSkinModsResolved();
			mountId = ModSkin.modBoardMountId;
			return ModSkin.modBoardMountId != -1;
		}

		public static string GetMenuSummary()
		{
			int num = 0;
			if (ModSkin.modHeadItemId > 0)
			{
				num++;
			}
			if (ModSkin.modBodyItemId > 0)
			{
				num++;
			}
			if (ModSkin.modLegItemId > 0)
			{
				num++;
			}
			if (ModSkin.modBackItemId > 0)
			{
				num++;
			}
			if (ModSkin.modPetItemId > 0)
			{
				num++;
			}
			if (ModSkin.modBoardItemId > 0)
			{
				num++;
			}
			return (num != 0) ? ("ON " + num.ToString() + "/6") : "OFF";
		}

		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 37:
				ModSkin.ShowMenu();
				return;
			case 38:
				ModSkin.OpenChatInput(ModSkin.inputSkinHead);
				return;
			case 39:
				ModSkin.OpenChatInput(ModSkin.inputSkinBody);
				return;
			case 40:
				ModSkin.OpenChatInput(ModSkin.inputSkinLeg);
				return;
			case 41:
				ModSkin.OpenChatInput(ModSkin.inputSkinBack);
				return;
			case 42:
				ModSkin.OpenChatInput(ModSkin.inputSkinPet);
				return;
			case 43:
				ModSkin.OpenChatInput(ModSkin.inputSkinBoard);
				return;
			default:
				return;
			}
		}

		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Đầu\n" + ModSkin.GetSkinLabel(ModSkin.modHeadItemId), ModSkin.getInstance(), 38, null));
			myVector.addElement(new Command("Thân\n" + ModSkin.GetSkinLabel(ModSkin.modBodyItemId), ModSkin.getInstance(), 39, null));
			myVector.addElement(new Command("Chân\n" + ModSkin.GetSkinLabel(ModSkin.modLegItemId), ModSkin.getInstance(), 40, null));
			myVector.addElement(new Command("Đeo Lưng\n" + ModSkin.GetSkinLabel(ModSkin.modBackItemId), ModSkin.getInstance(), 41, null));
			myVector.addElement(new Command("Pet\n" + ModSkin.GetSkinLabel(ModSkin.modPetItemId), ModSkin.getInstance(), 42, null));
			myVector.addElement(new Command("Ván Bay\n" + ModSkin.GetSkinLabel(ModSkin.modBoardItemId), ModSkin.getInstance(), 43, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		private static void OpenChatInput(string[] inputConfig)
		{
			if (GameCanvas.menu.showMenu)
			{
				GameCanvas.menu.doCloseMenu();
			}
			ChatTextField.gI().strChat = inputConfig[0];
			ChatTextField.gI().tfChat.name = inputConfig[1];
			ChatTextField.gI().startChat2(ModSkin.getInstance(), string.Empty);
		}

		private static string GetSkinLabel(int itemId)
		{
			if (itemId <= 0)
			{
				return "OFF";
			}
			return "ID " + itemId.ToString();
		}

		private static void HandleInput(string text, int slot)
		{
			try
			{
				int itemId = int.Parse(text);
				ModSkin.SetSkinSlot(slot, itemId);
			}
			catch
			{
				GameScr.info1.addInfo("Item ID không hợp lệ, vui lòng nhập lại!", 0);
			}
		}

		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		private static void SetSkinSlot(int slot, int itemId)
		{
			if (itemId <= 0)
			{
				ModSkin.ClearSkinSlot(slot);
				return;
			}
			ItemTemplate itemTemplate = ItemTemplates.get((short)itemId);
			if (itemTemplate == null)
			{
				GameScr.info1.addInfo("Không tìm thấy item ID: " + itemId.ToString(), 0);
				return;
			}
			switch (slot)
			{
			case 0:
				if (itemTemplate.part < 0)
				{
					GameScr.info1.addInfo("Item này không có part đầu.", 0);
					return;
				}
				ModSkin.CaptureOriginalSkinPart(0);
				ModSkin.modHeadItemId = itemId;
				ModSkin.modHeadPart = (int)itemTemplate.part;
				ModSkin.SaveSkinMod("head", itemId);
				GameScr.info1.addInfo("Đã mod đầu ID: " + itemId.ToString(), 0);
				return;
			case 1:
				if (itemTemplate.part < 0)
				{
					GameScr.info1.addInfo("Item này không có part thân.", 0);
					return;
				}
				ModSkin.CaptureOriginalSkinPart(1);
				ModSkin.modBodyItemId = itemId;
				ModSkin.modBodyPart = (int)itemTemplate.part;
				ModSkin.SaveSkinMod("body", itemId);
				GameScr.info1.addInfo("Đã mod thân ID: " + itemId.ToString(), 0);
				return;
			case 2:
				if (itemTemplate.part < 0)
				{
					GameScr.info1.addInfo("Item này không có part chân.", 0);
					return;
				}
				ModSkin.CaptureOriginalSkinPart(2);
				ModSkin.modLegItemId = itemId;
				ModSkin.modLegPart = (int)itemTemplate.part;
				ModSkin.SaveSkinMod("leg", itemId);
				GameScr.info1.addInfo("Đã mod chân ID: " + itemId.ToString(), 0);
				return;
			case 3:
				if (itemTemplate.part < 0)
				{
					GameScr.info1.addInfo("Item này không có part đeo lưng.", 0);
					return;
				}
				ModSkin.CaptureOriginalSkinPart(3);
				ModSkin.modBackItemId = itemId;
				ModSkin.modBackPart = (int)itemTemplate.part;
				ModSkin.SaveSkinMod("back", itemId);
				GameScr.info1.addInfo("Đã mod đeo lưng ID: " + itemId.ToString(), 0);
				return;
			case 4:
				ModSkin.CaptureOriginalPet();
				ModSkin.modPetItemId = itemId;
				ModSkin.modPetSmallId = (int)((itemTemplate.part >= 0) ? itemTemplate.part : itemTemplate.iconID);
				ModSkin.SaveSkinMod("pet", itemId);
				GameScr.info1.addInfo("Đã mod pet ID: " + itemId.ToString(), 0);
				return;
			case 5:
				ModSkin.modBoardItemId = itemId;
				ModSkin.modBoardMountId = (short)((itemTemplate.part >= 0) ? ((int)global::Char.ID_NEW_MOUNT + (int)itemTemplate.part) : itemId);
				ModSkin.SaveSkinMod("board", itemId);
				GameScr.info1.addInfo("Đã mod ván bay ID: " + itemId.ToString(), 0);
				return;
			default:
				return;
			}
		}

		private static void ClearSkinSlot(int slot)
		{
			switch (slot)
			{
			case 0:
				ModSkin.modHeadItemId = 0;
				ModSkin.modHeadPart = -1;
				if (ModSkin.originalHeadPart != -1)
				{
					global::Char.myCharz().head = ModSkin.originalHeadPart;
				}
				ModSkin.originalHeadPart = -1;
				ModSkin.SaveSkinMod("head", 0);
				GameScr.info1.addInfo("Đã tắt mod đầu.", 0);
				return;
			case 1:
				ModSkin.modBodyItemId = 0;
				ModSkin.modBodyPart = -1;
				if (ModSkin.originalBodyPart != -1)
				{
					global::Char.myCharz().body = ModSkin.originalBodyPart;
				}
				ModSkin.originalBodyPart = -1;
				ModSkin.SaveSkinMod("body", 0);
				GameScr.info1.addInfo("Đã tắt mod thân.", 0);
				return;
			case 2:
				ModSkin.modLegItemId = 0;
				ModSkin.modLegPart = -1;
				if (ModSkin.originalLegPart != -1)
				{
					global::Char.myCharz().leg = ModSkin.originalLegPart;
				}
				ModSkin.originalLegPart = -1;
				ModSkin.SaveSkinMod("leg", 0);
				GameScr.info1.addInfo("Đã tắt mod chân.", 0);
				return;
			case 3:
				ModSkin.modBackItemId = 0;
				ModSkin.modBackPart = -1;
				if (ModSkin.originalBackPart != -1)
				{
					global::Char.myCharz().bag = ModSkin.originalBackPart;
				}
				ModSkin.originalBackPart = -1;
				ModSkin.SaveSkinMod("back", 0);
				GameScr.info1.addInfo("Đã tắt mod đeo lưng.", 0);
				return;
			case 4:
				ModSkin.modPetItemId = 0;
				ModSkin.modPetSmallId = -1;
				ModSkin.RestoreOriginalPet();
				ModSkin.SaveSkinMod("pet", 0);
				GameScr.info1.addInfo("Đã tắt mod pet.", 0);
				return;
			case 5:
				ModSkin.modBoardItemId = 0;
				ModSkin.modBoardMountId = -1;
				ModSkin.SaveSkinMod("board", 0);
				GameScr.info1.addInfo("Đã tắt mod ván bay.", 0);
				return;
			default:
				return;
			}
		}

		private static void CaptureOriginalSkinPart(int slot)
		{
			if (global::Char.myCharz() == null)
			{
				return;
			}
			switch (slot)
			{
			case 0:
				if (ModSkin.originalHeadPart == -1)
				{
					ModSkin.originalHeadPart = global::Char.myCharz().head;
				}
				return;
			case 1:
				if (ModSkin.originalBodyPart == -1)
				{
					ModSkin.originalBodyPart = global::Char.myCharz().body;
				}
				return;
			case 2:
				if (ModSkin.originalLegPart == -1)
				{
					ModSkin.originalLegPart = global::Char.myCharz().leg;
				}
				return;
			case 3:
				if (ModSkin.originalBackPart == -1)
				{
					ModSkin.originalBackPart = global::Char.myCharz().bag;
				}
				return;
			default:
				return;
			}
		}

		private static void CaptureOriginalPet()
		{
			if (ModSkin.hasCapturedOriginalPet || global::Char.myCharz() == null)
			{
				return;
			}
			ModSkin.hasOriginalPet = (global::Char.myCharz().petFollow != null);
			ModSkin.originalPetSmallId = ((!ModSkin.hasOriginalPet) ? -1 : ((int)global::Char.myCharz().petFollow.smallID));
			ModSkin.hasCapturedOriginalPet = true;
		}

		private static void RestoreOriginalPet()
		{
			if (!ModSkin.hasCapturedOriginalPet || global::Char.myCharz() == null)
			{
				return;
			}
			if (!ModSkin.hasOriginalPet)
			{
				global::Char.myCharz().petFollow = null;
			}
			else
			{
				if (global::Char.myCharz().petFollow == null)
				{
					global::Char.myCharz().petFollow = new PetFollow();
				}
				global::Char.myCharz().petFollow.smallID = (short)ModSkin.originalPetSmallId;
			}
			ModSkin.hasCapturedOriginalPet = false;
			ModSkin.hasOriginalPet = false;
			ModSkin.originalPetSmallId = -1;
		}

		private static void SaveSkinMod(string key, int itemId)
		{
			Rms.saveRMSString("koi_skin_" + key, itemId.ToString());
		}

		private static int LoadSkinMod(string key)
		{
			string text = Rms.loadRMSString("koi_skin_" + key);
			int result;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out result))
			{
				return 0;
			}
			return result;
		}

		private static void LoadSkinMods()
		{
			ModSkin.LoadSkinSlot(0, ModSkin.LoadSkinMod("head"));
			ModSkin.LoadSkinSlot(1, ModSkin.LoadSkinMod("body"));
			ModSkin.LoadSkinSlot(2, ModSkin.LoadSkinMod("leg"));
			ModSkin.LoadSkinSlot(3, ModSkin.LoadSkinMod("back"));
			ModSkin.LoadSkinSlot(4, ModSkin.LoadSkinMod("pet"));
			ModSkin.LoadSkinSlot(5, ModSkin.LoadSkinMod("board"));
		}

		private static void EnsureSkinModsResolved()
		{
			if (ModSkin.modHeadItemId > 0 && ModSkin.modHeadPart == -1)
			{
				ModSkin.LoadSkinSlot(0, ModSkin.modHeadItemId);
			}
			if (ModSkin.modBodyItemId > 0 && ModSkin.modBodyPart == -1)
			{
				ModSkin.LoadSkinSlot(1, ModSkin.modBodyItemId);
			}
			if (ModSkin.modLegItemId > 0 && ModSkin.modLegPart == -1)
			{
				ModSkin.LoadSkinSlot(2, ModSkin.modLegItemId);
			}
			if (ModSkin.modBackItemId > 0 && ModSkin.modBackPart == -1)
			{
				ModSkin.LoadSkinSlot(3, ModSkin.modBackItemId);
			}
			if (ModSkin.modPetItemId > 0 && ModSkin.modPetSmallId == -1)
			{
				ModSkin.LoadSkinSlot(4, ModSkin.modPetItemId);
			}
			if (ModSkin.modBoardItemId > 0 && ModSkin.modBoardMountId == -1)
			{
				ModSkin.LoadSkinSlot(5, ModSkin.modBoardItemId);
			}
		}

		private static void LoadSkinSlot(int slot, int itemId)
		{
			if (itemId <= 0)
			{
				return;
			}
			ItemTemplate itemTemplate = ItemTemplates.get((short)itemId);
			if (itemTemplate == null)
			{
				return;
			}
			switch (slot)
			{
			case 0:
				ModSkin.modHeadItemId = itemId;
				ModSkin.modHeadPart = (int)itemTemplate.part;
				return;
			case 1:
				ModSkin.modBodyItemId = itemId;
				ModSkin.modBodyPart = (int)itemTemplate.part;
				return;
			case 2:
				ModSkin.modLegItemId = itemId;
				ModSkin.modLegPart = (int)itemTemplate.part;
				return;
			case 3:
				ModSkin.modBackItemId = itemId;
				ModSkin.modBackPart = (int)itemTemplate.part;
				return;
			case 4:
				ModSkin.modPetItemId = itemId;
				ModSkin.modPetSmallId = (int)((itemTemplate.part >= 0) ? itemTemplate.part : itemTemplate.iconID);
				return;
			case 5:
				ModSkin.modBoardItemId = itemId;
				ModSkin.modBoardMountId = (short)((itemTemplate.part >= 0) ? ((int)global::Char.ID_NEW_MOUNT + (int)itemTemplate.part) : itemId);
				return;
			default:
				return;
			}
		}

		private static ModSkin instance;

		private static readonly string[] inputSkinHead = new string[]
		{
			"Nhập item ID đầu (0 để tắt)",
			"itemID"
		};

		private static readonly string[] inputSkinBody = new string[]
		{
			"Nhập item ID thân (0 để tắt)",
			"itemID"
		};

		private static readonly string[] inputSkinLeg = new string[]
		{
			"Nhập item ID chân (0 để tắt)",
			"itemID"
		};

		private static readonly string[] inputSkinBack = new string[]
		{
			"Nhập item ID đeo lưng (0 để tắt)",
			"itemID"
		};

		private static readonly string[] inputSkinPet = new string[]
		{
			"Nhập item ID pet (0 để tắt)",
			"itemID"
		};

		private static readonly string[] inputSkinBoard = new string[]
		{
			"Nhập item ID ván bay (0 để tắt)",
			"itemID"
		};

		private static int modHeadItemId;

		private static int modBodyItemId;

		private static int modLegItemId;

		private static int modBackItemId;

		private static int modPetItemId;

		private static int modBoardItemId;

		private static int modHeadPart = -1;

		private static int modBodyPart = -1;

		private static int modLegPart = -1;

		private static int modBackPart = -1;

		private static int modPetSmallId = -1;

		private static short modBoardMountId = -1;

		private static int originalHeadPart = -1;

		private static int originalBodyPart = -1;

		private static int originalLegPart = -1;

		private static int originalBackPart = -1;

		private static bool hasCapturedOriginalPet;

		private static bool hasOriginalPet;

		private static int originalPetSmallId = -1;

	}
}
