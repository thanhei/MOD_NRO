using System;

namespace Mod.R
{
	// Token: 0x02000125 RID: 293
	internal static class Strings
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x000A84A4 File Offset: 0x000A66A4
		internal static void LoadLanguage(sbyte newLanguage)
		{
			if (newLanguage == 0)
			{
				Strings.LoadLanguageVI();
				return;
			}
			Strings.LoadLanguageEN();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000A84B5 File Offset: 0x000A66B5
		internal static string OnOffStatus(bool value)
		{
			if (!value)
			{
				return mResources.OFF;
			}
			return mResources.ON;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000A84C8 File Offset: 0x000A66C8
		private static void LoadLanguageVI()
		{
			Strings.someonePet = " [<color=cyan>" + mResources.pet + "</color> của {0}]";
			Strings.petLostMaster = " [<color=cyan>" + mResources.pet + "</color> bị lạc sư phụ]";
			Strings.autoChatDisabled = "Đã tắt Auto Chat";
			Strings.delaySeconds = "Delay:\n{0} giây";
			Strings.autoChatContent = "Nội dung Auto Chat";
			Strings.inputDelay = "Nhập thời gian delay";
			Strings.gobackTo = "Goback đến map: {0}, khu: {1}, tọa độ: ({2}, {3})";
			Strings.communityMod = "NRO Mod Cộng đồng";
			Strings.gameVersion = "Phiên bản game";
			Strings.registered = "Đã đăng ký";
			Strings.inputContent = "Nhập nội dung";
			Strings.viewContent = "Xem nội dung";
			Strings.timeMilliseconds = "Thời gian (ms)";
			Strings.errorOccurred = "Có lỗi xảy ra";
			Strings.contentSaved = "Đã lưu nội dung";
			Strings.autoAttack = "Tự đánh";
			Strings.youAreNotNamekian = "Bạn không phải là Namek";
			Strings.completed = "Đã hoàn thành";
			Strings.zone = "khu";
			Strings.functionShouldBeDisabled = "Bạn cần tắt chức năng \"{0}\"!";
			Strings.functionShouldBeEnabled = "Bạn cần bật chức năng \"{0}\"!";
			Strings.valueChanged = "Đã thay đổi {0} thành {1}";
			Strings.invalidValue = "Giá trị không hợp lệ";
			Strings.inputNumberOutOfRange = "Số đã nhập phải trong khoảng {0} và {1}";
			Strings.inputNumberMustBeBiggerThanOrEqual = "Số đã nhập phải lớn hơn hoặc bằng {0}";
			Strings.empty = "Rỗng";
			Strings.scaleModeStretchToFill = "Dãn ra vừa màn hình";
			Strings.scaleModeScaleAndCrop = "Thu nhỏ vừa màn hình";
			Strings.scaleModeScaleToFit = "Phóng to vừa màn hình";
			Strings.add = "Thêm";
			Strings.deleteAll = "Xóa tất cả";
			Strings.more = "Thêm";
			Strings.imageVideoFile = "Tệp ảnh/video";
			Strings.videoFile = "Tệp video";
			Strings.allFileTypes = "Tất cả";
			Strings.speed = "Tốc độ";
			Strings.delete = "Xóa";
			Strings.fullPath = "Đường dẫn đầy đủ";
			Strings.goTo = "Đi đến";
			Strings.level = "Mức";
			Strings.accounts = "Tài khoản";
			Strings.lastLogin = "Lần đăng nhập cuối";
			Strings.haventLoggedInYet = "Chưa đăng nhập lần nào";
			Strings.justNow = "Vài giây trước";
			Strings.minutesAgo = "{0} phút trước";
			Strings.hoursAgo = "{0} giờ trước";
			Strings.yesterdayAt = "Hôm qua lúc {0}";
			Strings.back = "Quay lại";
			Strings.info = "Thông tin";
			Strings.master = "Sư phụ";
			Strings.gender = "Hệ";
			Strings.name = "Tên";
			Strings.edit = "Sửa";
			Strings.select = "Chọn";
			Strings.save = "Lưu";
			Strings.import = "Nhập";
			Strings.logout = "Đăng xuất";
			Strings.custom = "Tùy chỉnh";
			Strings.settings = "Cài đặt";
			Strings.timeout = "Thời gian chờ";
			Strings.inputFPS = "Nhập số khung hình trên giây";
			Strings.inputGameSpeed = "Nhập tốc độ game";
			Strings.inputGameSpeedHint = "Tốc độ, có thể nhập số thập phân";
			Strings.inputGameDelay = "Nhập thời gian delay";
			Strings.inputGameDelayHint = "Thời gian delay, có thể nhập số thập phân";
			Strings.inputMyCharSpeed = "Nhập tốc độ nhân vật";
			Strings.inputMyCharSpeedHint = "Tốc độ";
			Strings.inputTimeChangeBg = "Nhập thời gian thay đổi hình nền";
			Strings.inputTimeChangeBgHint = "Thời gian (giây)";
			Strings.customBgChatPopup = "Loại hình nền được hỗ trợ: ảnh (*.jpg, *.png), ảnh động (*.gif), video (*.mp4).\nẢnh động và video tiêu tốn nhiều tài nguyên của hệ thống, nên cân nhắc trước khi sử dụng.";
			Strings.customBgOpenBgList = "Mở danh sách hình nền";
			Strings.customBgAddNewBg = "Thêm hình nền mới";
			Strings.customBgRemoveAll = "Xóa hết hình nền";
			Strings.customBgAllBgRemoved = "Đã xóa hết hình nền trong danh sách";
			Strings.customBgAutoChangeBg = "Tự động chuyển hình nền";
			Strings.customBgScaleMode = "Chế độ vẽ ảnh nền";
			Strings.customBgResetScaleModeToDefault = "Đặt lại chế độ vẽ ảnh nền về mặc định";
			Strings.customBgSetTimeChange = "Thay đổi thời gian chuyển hình nền";
			Strings.customBgSelectBgFiles = "Chọn hình nền";
			Strings.customBgChangeGifSpeed = "Thay đổi tốc độ ảnh động";
			Strings.customBgInputGifSpeed = "Nhập tốc độ ảnh động";
			Strings.customBgSwitchToThisBg = "Chuyển sang hình nền này";
			Strings.customBgRemovedBg = "Đã xóa hình nền \"{0}\"";
			Strings.customBgList = "Danh sách hình nền tùy chỉnh";
			Strings.customBgGifSpeed = "Tốc độ ảnh động";
			Strings.pickMobMonsterAdded = "Đã thêm quái id {0} vào danh sách";
			Strings.pickMobMonsterRemoved = "Đã xóa quái id {0} khỏi danh sách";
			Strings.pickMobMonsterTypeAdded = "Đã thêm \"{0}\" [{1}] vào danh sách";
			Strings.pickMobMonsterTypeRemoved = "Đã xóa \"{0}\" [{1}] khỏi danh sách";
			Strings.pickMobAutoPickItemListRemoved = "Đã xoá \"{0}\" [{1}] khỏi danh sách tự động nhặt vật phẩm";
			Strings.pickMobAutoPickItemListAdded = "Đã thêm \"{0}\" [{1}] vào danh sách tự động nhặt vật phẩm";
			Strings.pickMobAutoPickItemTypesListRemoved = "Đã xoá loại vật phẩm {0} khỏi danh sách tự động nhặt";
			Strings.pickMobAutoPickItemTypesListAdded = "Đã thêm loại vật phẩm {0} vào danh sách tự động nhặt";
			Strings.pickMobPlsFocusOnMonsterOrItem = "Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách tương ứng";
			Strings.pickMobMonsterListCleared = "Đã xoá danh sách quái tàn sát";
			Strings.pickMobItemListResetToDefault = "Danh sách vật phẩm đã được đặt lại mặc định";
			Strings.pickMobConfiguredPickGemsOnly = "Đã cài đặt chỉ nhặt ngọc";
			Strings.pickMobSkillListRemoved = "Đã xoá \"{0}\" [{1}] khỏi danh sách kỹ năng dùng để đánh quái";
			Strings.pickMobSkillListAdded = "Đã thêm \"{0}\" [{1}] vào danh sách kỹ năng dùng để đánh quái";
			Strings.pickMobSkillListResetToDefault = "Đã đặt danh sách kỹ năng dùng để đánh quái về mặc định";
			Strings.pickMobDontPickItemListAdded = "Đã thêm \"{0}\" [{1}] vào danh sách vật phẩm không tự động nhặt";
			Strings.pickMobDontPickItemListRemoved = "Đã xoá \"{0}\" [{1}] khỏi danh sách vật phẩm không tự động nhặt";
			Strings.pickMobDontPickItemTypeListAdded = "Đã thêm loại vật phẩm {0} vào danh sách không tự động nhặt";
			Strings.pickMobDontPickItemTypeListRemoved = "Đã xoá loại vật phẩm {0} khỏi danh sách không tự động nhặt";
			Strings.pickMobPlsFocusOnItem = "Cần trỏ vào vật phẩm cần thêm vào danh sách";
			Strings.pickMobFocusedMob = "Quái đang trỏ vào: {0}, id: {1}, loại: {2}";
			Strings.pickMobFocusedItem = "Vật phẩm đang trỏ vào: {0}, id: {1}, loại: {2}";
			Strings.pickMobRemoveMobIdFromList = "Xóa quái id ({0}) khỏi danh sách";
			Strings.pickMobAddMobIdToList = "Thêm quái id ({0}) vào danh sách";
			Strings.pickMobRemoveFromList = "Xóa {0} khỏi danh sách";
			Strings.pickMobAddToList = "Thêm {0} vào danh sách";
			Strings.pickMobAddItemTypeToList = "Thêm loại vật phẩm ({0}) vào danh sách";
			Strings.pickMobRemoveItemTypeFromList = "Xóa loại vật phẩm ({0}) khỏi danh sách";
			Strings.pickMobRemoveFromDontPickList = "Xóa {0} khỏi danh sách không nhặt";
			Strings.pickMobAddToDontPickList = "Thêm {0} vào danh sách không nhặt";
			Strings.pickMobRemoveItemTypeFromDontPickList = "Xóa loại vật phẩm ({0}) khỏi danh sách không nhặt";
			Strings.pickMobAddItemTypeToDontPickList = "Thêm loại vật phẩm ({0}) vào danh sách không nhặt";
			Strings.pickMobClearMonsterList = "Xóa danh sách tàn sát";
			Strings.pickMobAddToSkillList = "Thêm {0} vào danh sách kỹ năng";
			Strings.pickMobRemoveFromSkillList = "Xóa {0} khỏi danh sách kỹ năng";
			Strings.pickMobResetSkillListToDefault = "Đặt lại danh sách kỹ năng";
			Strings.pickMobResetItemListToDefault = "Đặt lại danh sách vật phẩm";
			Strings.pickMobViewMonsterList = "Xem danh sách quái";
			Strings.pickMobMonsterIdList = "Danh sách id quái";
			Strings.pickMobMonsterTypeList = "Danh sách loại quái";
			Strings.pickMobViewItemList = "Xem danh sách vật phẩm";
			Strings.pickMobAutoPickItemList = "Danh sách vật phẩm tự động nhặt";
			Strings.pickMobAutoPickItemTypeList = "Danh sách loại vật phẩm tự động nhặt";
			Strings.pickMobDontPickItemList = "Danh sách vật phẩm không tự động nhặt";
			Strings.pickMobDontPickItemTypeList = "Danh sách loại vật phẩm không tự động nhặt";
			Strings.pickMobViewSkillList = "Xem danh sách kỹ năng";
			Strings.pickMobSkillList = "Danh sách kỹ năng dùng để đánh quái";
			Strings.introCurrentPath = "Đường dẫn video hiện tại";
			Strings.introChangeVideoPath = "Chọn đường dẫn video";
			Strings.introInputVolume = "Nhập âm lượng intro";
			Strings.introInputVolumeHint = "Âm lượng";
			Strings.introNoVideo = "Đường dẫn video intro chưa được đặt";
			Strings.xmapUseSpecialCapsule = "Sử dụng capsule đặc biệt khi Xmap";
			Strings.xmapUseNormalCapsule = "Sử dụng capsule thường khi Xmap";
			Strings.xmapCanceled = "Đã huỷ Xmap";
			Strings.xmapChatPopup = "XmapNRO by Phucprotein\nMap hiện tại: {0}, ID: {1}\nVui lòng chọn nơi muốn đến";
			Strings.xmapCantFindWay = "Không thể tìm thấy đường đi";
			Strings.xmapDestinationReached = "Đã đến nơi";
			Strings.xmapTimeout = "Thời gian chờ xmap";
			Strings.xmapUseAStar = "Xmap chạy bộ";
			Strings.xmapEditTimeout = "Thay đổi thời gian chờ xmap";
			Strings.teleportMenuOpenSavedCharList = "Nhân vật đã lưu";
			Strings.teleportMenuCharacterAdded = "Đã thêm {0} vào danh sách";
			Strings.teleportMenuCantRemoveTargetChar = "Không thể xóa nhân vật đang auto dịch chuyển tới";
			Strings.teleportMenuCharacterRemoved = "Đã xóa {0} khỏi danh sách";
			Strings.teleportMenuStopTeleporting = "Dừng auto dịch chuyển";
			Strings.teleportMenuSelectTarget = "Chọn nhân vật mục tiêu";
			Strings.teleportMenuStopTeleportToTarget = "Dừng auto dịch chuyển đến {0}";
			Strings.teleportMenuAddCharacterByID = "Thêm nhân vật bằng ID";
			Strings.teleportMenuAddEveryoneInZone = "Thêm tất cả người trong khu";
			Strings.teleportMenuEveryoneAdded = "Đã thêm tất cả người trong khu vào danh sách";
			Strings.teleportMenuRemoveCharacter = "Xóa nhân vật đã lưu";
			Strings.teleportMenuCleared = "Đã xóa toàn bộ nhân vật đã lưu";
			Strings.teleportMenuTeleportingToCharacter = "Dịch chuyển đến {0}";
			Strings.teleportMenuNoRemovableChar = "Không có nhân vật nào xóa được trong danh sách";
			Strings.teleportMenuAutoTeleportTo = "Auto dịch chuyển đến";
			Strings.teleportMenuCharacterList = "Danh sách nhân vật";
			Strings.teleportMenuInputCharIDTextFieldName = "Nhập ID nhân vật";
			Strings.teleportMenuInputCharIDTextFieldHint = "ID";
			Strings.teleportMenuAddedCharacterWithID = "Đã thêm nhân vật với ID {0}";
			Strings.vnInputEnable = "Chế độ gõ tiếng Việt";
			Strings.vnInputDiacritics = "Kiểu đặt dấu";
			Strings.vnInputInputMethod = "Kiểu gõ";
			Strings.vnInputConsumeRepeatKey = "Bỏ qua phím đặt dấu lặp";
			Strings.inGameAccountManagerAddAccount = "Thêm tài khoản";
			Strings.inGameAccountManagerEditAccount = "Sửa tài khoản";
			Strings.inGameAccountManagerConfirmDeleteAcc = "Bạn có chắc chắn muốn xoá tài khoản này khỏi danh sách không?";
			Strings.inGameAccountManagerServerBlank = "Bạn chưa chọn server";
			Strings.inGameAccountManagerUnregisteredAccountMustBeOnTeaMobiServer = "Tài khoản chưa đăng ký phải ở server TeaMobi";
			Strings.inGameAccountManagerEditServer = "Sửa server";
			Strings.inGameAccountManagerServerName = "Tên server";
			Strings.inGameAccountManagerServerAddress = "Địa chỉ";
			Strings.inGameAccountManagerServerPort = "Cổng";
			Strings.inGameAccountManagerImportAccounts = "Nhập tài khoản từ dữ liệu";
			Strings.inGameAccountManagerImportAccountsInputData = "Dữ liệu tài khoản";
			Strings.inGameAccountManagerRegexMatchLines = "Biểu thức chính quy danh sách tài khoản";
			Strings.inGameAccountManagerRegexMatchAccountInfo = "Biểu thức chính quy thông tin tài khoản";
			Strings.inGameAccountManagerServerNameBlank = "Tên server không được để trống";
			Strings.inGameAccountManagerServerAddressBlank = "Địa chỉ server không được để trống";
			Strings.inGameAccountManagerServerPortBlank = "Cổng server không được để trống";
			Strings.inGameAccountManagerServerPortInvalid = "Cổng server không hợp lệ";
			Strings.inGameAccountManagerImportAccountsHelp = "Nhập dữ liệu tài khoản, biểu thức chính quy danh sách tài khoản (<color=orange>RegEx 1</color>) và biểu thức chính quy thông tin tài khoản (<color=blue>RegEx 2</color>) tương ứng vào các trường. Bạn nên thử với dữ liệu mẫu nhỏ trước để tránh bị đơ.\n<color=orange>RegEx 1</color> phải có 1 nhóm. Kết quả khớp của nhóm này sẽ là dữ liệu đầu vào cho <color=blue>RegEx 2</color>.\n<color=blue>RegEx 2</color> có thể có 2 nhóm (tài khoản, mật khẩu) hoặc 3 nhóm (thêm máy chủ). Nếu <color=blue>RegEx 2</color> chỉ có 2 nhóm, bạn phải chọn máy chủ trong danh sách máy chủ.\n\nVD: Dữ liệu đầu vào của bạn:\n\n<color=black>acc1@example.com|pass1|1</color>\n<color=black>acc2@example.com|pass2|2</color>\n<color=black>acc3@example.com|pass3|9</color>\n<color=black>acc4@example.com|pass3|12</color>\n<color=black>...</color>\n\nGiá trị của <color=orange>RegEx 1</color> sẽ là \"<color=grey>(.*?)\\r?\\n</color>\",\nGiá trị của <color=blue>RegEx 2</color> sẽ là \"<color=grey>(.*)\\|(.*)\\|([0-9]*)</color>\".\n\n";
			Strings.inGameAccountManagerImportAccountsInputDataBlank = "Dữ liệu không được để trống";
			Strings.inGameAccountManagerRegexMatchLinesBlank = "Biểu thức chính quy không được để trống";
			Strings.inGameAccountManagerImportAccountsResult = "Nhập thành công {0} tài khoản ({1:F2}%), nhập thất bại {2} tài khoản ({3:F2}%)";
			Strings.inGameAccountManagerRegexMatchAccountInfoBlank = "Biểu thức chính quy không được để trống";
			Strings.inGameAccountManagerUnregisteredAccountAlreadyAdded = "Tài khoản đã có trong danh sách";
			Strings.inGameAccountManagerAccountAdded = "Đã thêm tài khoản vào danh sách";
			Strings.autoSellTrashItemsBoxFull = "Rương đã đầy, không thể chứa thêm đồ";
			Strings.autoLoginReattemptLoginIn = "Đăng nhập lại trong {0} giây";
			Strings.paintControllerButtonsDPadArrowKeys = "Mũi tên";
			Strings.paintControllerButtonsLeftStickMove = "Di chuyển";
			Strings.paintControllerButtonsLeftStickButtonTeleport = "Dịch chuyển";
			Strings.paintControllerButtonsRightStickMoveCamera = "Di chuyển góc nhìn";
			Strings.paintControllerButtonsRightStickButtonLockCamera = "Khóa góc nhìn";
			Strings.modMenuPanelTabName = new string[][]
			{
				new string[] { "Bật/tắt", "" },
				new string[] { "Điều", "chỉnh" },
				new string[] { "Chức", "năng" },
				new string[] { "Nhà phát", "triển" }
			};
			Strings.vSyncDescription = "Tự động giới hạn FPS theo tốc độ khung hình của màn hình";
			Strings.showTargetInfoTitle = "Thông tin đối thủ";
			Strings.showTargetInfoDescription = "Hiện gần chính xác thời gian NRD, khiên, khỉ, huýt sáo... của đối thủ";
			Strings.autoSendAttackDescription = "Tự động gửi lệnh tấn công mục tiêu mà không di chuyển nhân vật";
			Strings.autoLoginTitle = "Đăng nhập lại khi mất kết nối";
			Strings.autoLoginDescription = "Tự động đăng nhập lại và quay lại map, khu và vị trí cũ khi kết nối tới máy chủ bị gián đoạn";
			Strings.showCharListTitle = "Danh sách nhân vật";
			Strings.showCharListDescription = "Hiện danh sách nhân vật trong khu hiện tại";
			Strings.showPetInCharListTitle = "Hiện đệ tử trong danh sách nhân vật";
			Strings.showPetInCharListDescription = "Hiện đệ tử trong danh sách nhân vật trong khu hiện tại";
			Strings.autoTrainForNewbieTitle = "Tự động up SS";
			Strings.autoTrainForNewbieDescription = "Tự động up acc mới đến nhiệm vụ vào bang";
			Strings.noLongerNewAccount = "Bạn đã qua nhiệm vụ vào bang";
			Strings.autoSellTrashItemsTitle = "Tự động bán đồ rác";
			Strings.autoSellTrashItemsDescription = "Tự động bán vật phẩm không cần thiết khi hành trang đầy và cất đồ giá trị vào rương, bạn nên dọn sạch hành trang của mình trước khi bật để tránh bị mất vật phẩm";
			Strings.customBackgroundTitle = "Hình nền tùy chỉnh";
			Strings.customBackgroundDescription = "Thay thế hình nền mặc định của game bằng hình nền tùy chỉnh";
			Strings.hideGameUITitle = "Ẩn UI";
			Strings.hideGameUIDescription = "Ẩn giao diện game (các nút vẫn có thể bấm)";
			Strings.skipSpaceshipTitle = "Bỏ qua tàu vũ trụ";
			Strings.skipSpaceshipDescription = "Bỏ qua hoạt ảnh tàu vũ trụ";
			Strings.notifyBossTitle = "Thông báo Boss";
			Strings.notifyBossDescription = "Hiển thị danh sách thông báo boss";
			Strings.introTitle = "Video intro";
			Strings.introDescription = "Phát một đoạn video ngắn khi mở game";
			Strings.xmapUseNormalCapsuleDescription = "Sử dụng capsule thường khi xmap, không nên bật nếu bạn không muốn tốn capsule thường";
			Strings.xmapUseSpecialCapsuleDescription = "Sử dụng capsule đặc biệt khi xmap";
			Strings.xmapUseAStarDescription = "Sử dụng thuật toán A* để tìm đường đi thay vì dịch chuyển";
			Strings.pickMobTitle = "Tàn sát";
			Strings.pickMobDescription = "Tự động đánh quái";
			Strings.pickMobAvoidSuperMobTitle = "Né siêu quái khi tàn sát";
			Strings.avoidSuperMobDescription = "Không đánh siêu quái khi tàn sát";
			Strings.pickMobVDHTitle = "Vượt địa hình khi tàn sát";
			Strings.pickMobVDHDescription = "Tự động vượt địa hình khi đang tàn sát";
			Strings.pickMobAttackMonsterBySendCommandTitle = "Đánh quái không dùng vũ lực";
			Strings.pickMobAttackMonsterBySendCommandDescription = "Tàn sát quái bằng cách gửi lệnh tấn công thay vì nhấn vào quái";
			Strings.autoPickItemTitle = "Tự động nhặt vật phẩm";
			Strings.autoPickItemDescription = "Tự động nhặt vật phẩm ở gần";
			Strings.pickMobPickMyItemOnlyTitle = "Không nhặt vật phẩm của người khác";
			Strings.pickMobPickMyItemOnlyDescription = "Chỉ nhặt vật phẩm của bản thân, do đệ tử đánh quái rơi ra và vật phẩm không thuộc về bất cứ ai";
			Strings.pickMobLimitPickTimesTitle = "Giới hạn số lần nhặt";
			Strings.pickMobLimitPickTimesDescription = "Giới hạn số lần tự động nhặt một vật phẩm";
			Strings.autoAskForPeansTitle = "Auto xin đậu";
			Strings.autoAskForPeansDescription = "Tự động gửi tin nhắn xin đậu thần vào kênh chat bang hội";
			Strings.youAreNotInAClan = "Bạn không ở trong bang hội nào";
			Strings.autoDonatePeansTitle = "Auto cho đậu";
			Strings.autoDonatePeansDescription = "Tự động cho đậu thần khi có tin nhắn xin đậu thần của các thành viên trong bang hội";
			Strings.autoHarvestPeansTitle = "Auto vặt đậu";
			Strings.autoHarvestPeansDescription = "Tự động thu hoạch đậu thần từ cây đậu thần ở nhà";
			Strings.setFPSDescription = "Điều chỉnh số lượng khung hình trên giây của game";
			Strings.setGameSpeedTitle = "Tốc độ game";
			Strings.setGameSpeedDescription = "Điều chỉnh tốc độ game";
			Strings.setGameDelayTitle = "Delay game";
			Strings.setGameDelayDescription = "Điều chỉnh thời gian chờ của game";
			Strings.setReduceGraphicsTitle = "Giảm đồ họa";
			Strings.setReduceGraphicsChoices = new string[]
			{
				"Đang tắt",
				"Đang bật (" + Strings.level.ToLower() + " 1)",
				"Đang bật (" + Strings.level.ToLower() + " 2)",
				"Đang bật (" + Strings.level.ToLower() + " 3)"
			};
			Strings.setMyCharSpeedTitle = "Tốc độ nhân vật";
			Strings.setMyCharSpeedDescription = "Điều chỉnh tốc độ di chuyển của nhân vật";
			Strings.setGoBackChoices = new string[] { "Đang tắt", "Đang bật (quay lại chỗ cũ khi chết)", "Đang bật (đến map, khu và tọa độ cố định khi chết)" };
			Strings.setAutoTrainPetTitle = "Auto up đệ tử";
			Strings.setAutoTrainPetChoices = new string[] { "Đang tắt", "Đang bật (up đệ thường)", "Đang bật (up đệ né siêu quái)", "Đang bật (up đệ Kaioken)" };
			Strings.youDontHaveDisciple = "Bạn không có đệ tử";
			Strings.setAutoAttackWhenDiscipleNeededTitle = "Đánh khi đệ kêu";
			Strings.setAutoAttackWhenDiscipleNeededChoices = new string[] { "Đánh quái gần nhất", "Đánh đệ tử (tự động bật cờ xám)", "Đánh bản thân (tự động bật cờ xám)" };
			Strings.setAutoRescueTitle = "Auto trị thương";
			Strings.setAutoRescueChoices = new string[] { "Đang tắt", "Đang bật (trị thương mọi người)", "Đang bật (chỉ thành viên trong bang hội)", "Đang bật (chỉ đệ tử)", "Đang bật (chỉ đệ tử của bản thân)" };
			Strings.setAutoRescueSkill3Null = "Bạn chưa có kỹ năng Trị thương";
			Strings.setAutoRescueSkill3BuffInvalid = "Skill 3 của bạn không phải kỹ năng Trị thương";
			Strings.customBgDefaultScaleModeTitle = "Chế độ vẽ ảnh nền mặc định";
			Strings.setXmapTimeoutDescription = "Điều chỉnh thời gian chờ (giây) trước khi chuyển sang chế độ dịch chuyển khi sử dụng xmap chạy bộ";
			Strings.setTimeChangeCustomBgTitle = "Thời gian đổi hình nền";
			Strings.setTimeChangeCustomBgDescription = "Điều chỉnh thời gian thay đổi hình nền (giây)";
			Strings.setIntroVolumeTitle = "Âm lượng intro";
			Strings.setIntroVolumeDescription = "Điều chỉnh âm lượng video phát khi mở game";
			Strings.openXmapMenuTitle = "Menu Xmap";
			Strings.openXmapMenuDescription = "Mở menu Xmap (lệnh \"xmp\" hoặc nút 'x')";
			Strings.openPickMobMenuTitle = "Menu PickMob";
			Strings.openPickMobMenuDescription = "Mở menu PickMob (lệnh \"pickmob\")";
			Strings.openTeleportMenuTitle = "Menu Teleport";
			Strings.openTeleportMenuDescription = "Mở menu dịch chuyển (lệnh \"tele\" hoặc nút 'z')";
			Strings.openCustomBackgroundMenuTitle = "Menu Custom Background";
			Strings.openCustomBackgroundMenuDescription = "Mở menu hình nền tùy chỉnh";
			Strings.openIntroMenuTitle = "Menu Intro";
			Strings.openIntroMenuDescription = "Mở menu intro video";
			Strings.introSelectFile = "Chọn tệp video";
			Strings.openSetsMenuTitle = "Menu Set đồ";
			Strings.openSetsMenuDescription = "Mở menu set đồ (lệnh \"set\" hoặc nút '`')";
			Strings.openVietnameseInputMenuTitle = "Menu gõ tiếng Việt";
			Strings.openVietnameseInputMenuDescription = "Mở menu gõ tiếng Việt, sử dụng khi có vấn đề với trình gõ tiếng Việt bên ngoài (UniKey, VietKey, ...)";
			Strings.addUserAoToAccountManagerTitle = "Thêm tài khoản hiện tại vào danh sách";
			Strings.addUserAoToAccountManagerDescription = "Thêm tài khoản chưa đăng ký hiện tại vào danh sách tài khoản";
			Strings.openedByExternalAccountManager = "Tài khoản được quản lý bởi Quản lý tài khoản bên ngoài";
			Strings.accountAlreadyRegistered = "Tài khoản đã được đăng ký";
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000A91B0 File Offset: 0x000A73B0
		private static void LoadLanguageEN()
		{
			Strings.someonePet = " [{0}'s <color=cyan>" + mResources.pet + "</color>]";
			Strings.petLostMaster = " [<color=cyan>" + mResources.pet + "</color> lost master]";
			Strings.gobackTo = "Goback to map: {0}, zone: {1}, coordinates: ({2}, {3})";
			Strings.autoChatDisabled = "Auto Chat disabled";
			Strings.delaySeconds = "Delay:\n{0} seconds";
			Strings.autoChatContent = "Auto Chat contents";
			Strings.communityMod = "DBO Community Mod";
			Strings.gameVersion = "Game version";
			Strings.registered = "Registered";
			Strings.inputContent = "Enter the content";
			Strings.viewContent = "View content";
			Strings.inputDelay = "Enter the delay time";
			Strings.timeMilliseconds = "Time (ms)";
			Strings.errorOccurred = "Error occurred";
			Strings.contentSaved = "Content saved";
			Strings.autoAttack = "Auto attack";
			Strings.youAreNotNamekian = "You are not a Namekian character";
			Strings.completed = "Completed";
			Strings.zone = "zone";
			Strings.functionShouldBeDisabled = "You need to disable the \"{0}\" feature!";
			Strings.functionShouldBeEnabled = "You need to enable the \"{0}\" feature!";
			Strings.valueChanged = "Changed {0} to {1}";
			Strings.invalidValue = "Invalid value";
			Strings.inputNumberOutOfRange = "The number entered must be between {0} and {1}";
			Strings.inputNumberMustBeBiggerThanOrEqual = "The number entered must be bigger than or equal to {0}";
			Strings.empty = "Empty";
			Strings.scaleModeStretchToFill = "Stretch to fill";
			Strings.scaleModeScaleAndCrop = "Scale and crop";
			Strings.scaleModeScaleToFit = "Scale to fit";
			Strings.add = "Add";
			Strings.deleteAll = "Delete all";
			Strings.more = "More";
			Strings.imageVideoFile = "Image/video file";
			Strings.videoFile = "Video file";
			Strings.allFileTypes = "All file types";
			Strings.speed = "Speed";
			Strings.delete = "Delete";
			Strings.fullPath = "Full path";
			Strings.goTo = "Go to";
			Strings.level = "Level";
			Strings.accounts = "Accounts";
			Strings.lastLogin = "Last login";
			Strings.haventLoggedInYet = "Haven't logged in yet";
			Strings.justNow = "Just now";
			Strings.minutesAgo = "{0} minute(s) ago";
			Strings.hoursAgo = "{0} hour(s) ago";
			Strings.yesterdayAt = "Yesterday at {0}";
			Strings.back = "Back";
			Strings.info = "Info";
			Strings.master = "Master";
			Strings.gender = "Gender";
			Strings.name = "Name";
			Strings.edit = "Edit";
			Strings.select = "Select";
			Strings.save = "Save";
			Strings.import = "Import";
			Strings.logout = "Logout";
			Strings.custom = "Custom";
			Strings.settings = "Settings";
			Strings.timeout = "Timeout";
			Strings.inputFPS = "Enter the number of frames per second";
			Strings.inputGameSpeed = "Enter the game speed";
			Strings.inputGameSpeedHint = "Game speed, can be decimal number";
			Strings.inputGameDelay = "Enter game delay";
			Strings.inputGameDelayHint = "Delay, can be decimal number";
			Strings.inputMyCharSpeed = "Enter the speed of my character";
			Strings.inputMyCharSpeedHint = "Speed";
			Strings.inputTimeChangeBg = "Enter the background interval";
			Strings.inputTimeChangeBgHint = "Time (seconds)";
			Strings.customBgChatPopup = "Supported background types: static images (*.jpg, *.png), animated images (*.gif), videos (*.mp4).\nAnimated images and videos consume a lot of system resources, which you should consider before using them.";
			Strings.customBgOpenBgList = "Open background list";
			Strings.customBgAddNewBg = "Add new background";
			Strings.customBgRemoveAll = "Remove all backgrounds";
			Strings.customBgAllBgRemoved = "All backgrounds have been removed from the list";
			Strings.customBgAutoChangeBg = "Auto change background";
			Strings.customBgScaleMode = "Background scale mode";
			Strings.customBgResetScaleModeToDefault = "Reset background scale mode to default";
			Strings.customBgSetTimeChange = "Change background interval";
			Strings.customBgChangeGifSpeed = "Change animated background speed";
			Strings.customBgSelectBgFiles = "Select background files";
			Strings.customBgInputGifSpeed = "Enter the speed of animated backgrounds";
			Strings.customBgSwitchToThisBg = "Switch to this background";
			Strings.customBgRemovedBg = "Background \"{0}\" has been removed";
			Strings.customBgList = "Custom background list";
			Strings.customBgGifSpeed = "Animated background speed";
			Strings.pickMobMonsterAdded = "Monster with id {0} has been added to the list";
			Strings.pickMobMonsterRemoved = "Monster with id {0} has been removed from the list";
			Strings.pickMobMonsterTypeAdded = "\"{0}\" [{1}] has been added to the list";
			Strings.pickMobMonsterTypeRemoved = "\"{0}\" [{1}] has been removed from the list";
			Strings.pickMobAutoPickItemListRemoved = "\"{0}\" [{1}] has been removed from the auto pick item list";
			Strings.pickMobAutoPickItemListAdded = "\"{0}\" [{1}] has been added to the auto pick item list";
			Strings.pickMobAutoPickItemTypesListRemoved = "Item type {0} has been removed from the auto pick list";
			Strings.pickMobAutoPickItemTypesListAdded = "Item type {0} has been added to the auto pick list";
			Strings.pickMobPlsFocusOnMonsterOrItem = "Please focus on the monster or item to add to the corresponding list";
			Strings.pickMobMonsterListCleared = "Monster list has been cleared";
			Strings.pickMobItemListResetToDefault = "Item list has been reset to default";
			Strings.pickMobConfiguredPickGemsOnly = "Configured to pick gems only";
			Strings.pickMobSkillListRemoved = "\"{0}\" [{1}] has been removed from the skill list used to attack monsters";
			Strings.pickMobSkillListAdded = "\"{0}\" [{1}] has been added to the skill list used to attack monsters";
			Strings.pickMobSkillListResetToDefault = "Skill list used to attack monsters has been reset to default";
			Strings.pickMobDontPickItemListAdded = "\"{0}\" [{1}] has been added to the item list not to auto pick";
			Strings.pickMobDontPickItemListRemoved = "\"{0}\" [{1}] has been removed from the item list not to auto pick";
			Strings.pickMobDontPickItemTypeListAdded = "Item type {0} has been added to the list not to auto pick";
			Strings.pickMobDontPickItemTypeListRemoved = "Item type {0} has been removed from the list not to auto pick";
			Strings.pickMobPlsFocusOnItem = "Please focus on the item to add to the list";
			Strings.pickMobFocusedMob = "Focused monster: {0}, id: {1}, type: {2}";
			Strings.pickMobFocusedItem = "Focused item: {0}, id: {1}, type: {2}";
			Strings.pickMobRemoveMobIdFromList = "Remove monster with id ({0}) from the list";
			Strings.pickMobAddMobIdToList = "Add monster with id ({0}) to the list";
			Strings.pickMobRemoveFromList = "Remove {0} from the list";
			Strings.pickMobAddToList = "Add {0} to the list";
			Strings.pickMobAddItemTypeToList = "Add item type ({0}) to the list";
			Strings.pickMobRemoveItemTypeFromList = "Remove item type ({0}) from the list";
			Strings.pickMobRemoveFromDontPickList = "Remove {0} from the list not to auto pick";
			Strings.pickMobAddToDontPickList = "Add {0} to the list not to auto pick";
			Strings.pickMobRemoveItemTypeFromDontPickList = "Remove item type ({0}) from the list not to auto pick";
			Strings.pickMobAddItemTypeToDontPickList = "Add item type ({0}) to the list not to auto pick";
			Strings.pickMobClearMonsterList = "Clear monster list";
			Strings.pickMobAddToSkillList = "Add {0} to the skill list";
			Strings.pickMobRemoveFromSkillList = "Remove {0} from the skill list";
			Strings.pickMobResetSkillListToDefault = "Reset the skill list to default";
			Strings.pickMobResetItemListToDefault = "Reset the item list to default";
			Strings.pickMobViewMonsterList = "View monster list";
			Strings.pickMobMonsterIdList = "Monster id list";
			Strings.pickMobMonsterTypeList = "Monster type list";
			Strings.pickMobViewItemList = "View item list";
			Strings.pickMobAutoPickItemList = "Auto pick item list";
			Strings.pickMobAutoPickItemTypeList = "Auto pick item type list";
			Strings.pickMobDontPickItemList = "Don't auto pick item list";
			Strings.pickMobDontPickItemTypeList = "Don't auto pick item type list";
			Strings.pickMobViewSkillList = "View skill list";
			Strings.pickMobSkillList = "Skill list used to attack monsters";
			Strings.introCurrentPath = "Current video path";
			Strings.introChangeVideoPath = "Change video path";
			Strings.introInputVolume = "Enter the volume of the intro video";
			Strings.introInputVolumeHint = "Volume";
			Strings.introNoVideo = "Intro video path has not been set";
			Strings.xmapUseSpecialCapsule = "Use special capsule when Xmap";
			Strings.xmapUseNormalCapsule = "Use normal capsule when Xmap";
			Strings.xmapCanceled = "Xmap canceled";
			Strings.xmapChatPopup = "XmapNRO by Phucprotein\nCurrent map: {0}, ID: {1}\nPlease select the destination";
			Strings.xmapCantFindWay = "No possible way was found";
			Strings.xmapDestinationReached = "Destination reached";
			Strings.xmapTimeout = "Xmap timeout";
			Strings.xmapUseAStar = "Use pathfinding when using Xmap";
			Strings.xmapEditTimeout = "Edit Xmap timeout";
			Strings.teleportMenuOpenSavedCharList = "Saved characters";
			Strings.teleportMenuCharacterAdded = "{0} has been added to the list";
			Strings.teleportMenuCantRemoveTargetChar = "Can't remove the character that you are auto teleporting to";
			Strings.teleportMenuCharacterRemoved = "{0} has been removed from the list";
			Strings.teleportMenuStopTeleporting = "Stop auto teleporting";
			Strings.teleportMenuSelectTarget = "Select target character";
			Strings.teleportMenuStopTeleportToTarget = "Stopped auto teleporting to {0}";
			Strings.teleportMenuAddCharacterByID = "Add character by ID";
			Strings.teleportMenuAddEveryoneInZone = "Add everyone in the current zone";
			Strings.teleportMenuEveryoneAdded = "Everyone in the current zone has been added to the list";
			Strings.teleportMenuRemoveCharacter = "Remove saved character";
			Strings.teleportMenuCleared = "All saved characters have been removed";
			Strings.teleportMenuTeleportingToCharacter = "Teleporting to {0}";
			Strings.teleportMenuNoRemovableChar = "No character that can be removed in the list";
			Strings.teleportMenuAutoTeleportTo = "Auto teleport";
			Strings.teleportMenuCharacterList = "Character list";
			Strings.teleportMenuInputCharIDTextFieldName = "Enter character ID";
			Strings.teleportMenuInputCharIDTextFieldHint = "ID";
			Strings.teleportMenuAddedCharacterWithID = "Character with ID {0} has been added to the list";
			Strings.vnInputEnable = "Vietnamese input mode";
			Strings.vnInputDiacritics = "Diacritics type";
			Strings.vnInputInputMethod = "Input method";
			Strings.vnInputConsumeRepeatKey = "Consume repeat key";
			Strings.inGameAccountManagerAddAccount = "Add account";
			Strings.inGameAccountManagerEditAccount = "Edit account";
			Strings.inGameAccountManagerConfirmDeleteAcc = "Are you sure you want to delete this account from the list?";
			Strings.inGameAccountManagerServerBlank = "You haven't selected a server";
			Strings.inGameAccountManagerUnregisteredAccountMustBeOnTeaMobiServer = "Unregistered accounts must be on TeaMobi server";
			Strings.inGameAccountManagerEditServer = "Edit server";
			Strings.inGameAccountManagerServerName = "Server name";
			Strings.inGameAccountManagerServerAddress = "Address";
			Strings.inGameAccountManagerServerPort = "Port";
			Strings.inGameAccountManagerImportAccounts = "Import accounts";
			Strings.inGameAccountManagerImportAccountsInputData = "Data";
			Strings.inGameAccountManagerRegexMatchLines = "Regular expression for account list creation";
			Strings.inGameAccountManagerRegexMatchAccountInfo = "Regular expression for account info extraction";
			Strings.inGameAccountManagerImportAccountsHelp = "Enter the data, regular expression for account list creation (<color=orange>RegEx 1</color>), and the regular expression for account info extraction (<color=blue>RegEx 2</color>), respectively into their fields. Try with small data sample first to avoid being stuck.\n<color=orange>RegEx 1</color> must have 1 group, and the match of it will be the input for <color=blue>RegEx 2</color>.\n<color=blue>RegEx 2</color> can have 2 groups (account, password) or 3 groups (with server). If <color=blue>RegEx 2</color> has only 2 groups, you must specify the default server in the server selection field.\n\nEx: Your input data:\n\n<color=black>acc1@example.com|pass1|1</color>\n<color=black>acc2@example.com|pass2|2</color>\n<color=black>acc3@example.com|pass3|9</color>\n<color=black>acc4@example.com|pass3|12</color>\n<color=black>...</color>\n\nThe value of <color=orange>RegEx 1</color> will be \"<color=grey>(.*?)\\r?\\n</color>\",\nand the value of <color=blue>RegEx 2</color> will be \"<color=grey>(.*)\\|(.*)\\|([0-9]*)</color>\".\n\n";
			Strings.inGameAccountManagerServerNameBlank = "Server name can't be blank";
			Strings.inGameAccountManagerServerAddressBlank = "Server address can't be blank";
			Strings.inGameAccountManagerServerPortBlank = "Server port can't be blank";
			Strings.inGameAccountManagerServerPortInvalid = "Invalid server port";
			Strings.inGameAccountManagerImportAccountsInputDataBlank = "Data can't be blank";
			Strings.inGameAccountManagerRegexMatchLinesBlank = "Regular expression can't be blank";
			Strings.inGameAccountManagerRegexMatchAccountInfoBlank = "Regular expression can't be blank";
			Strings.inGameAccountManagerImportAccountsResult = "Successfully imported {0} accounts ({1:F2}%), failed to import {2} accounts ({3:F2}%)";
			Strings.inGameAccountManagerUnregisteredAccountAlreadyAdded = "Account has already been added";
			Strings.inGameAccountManagerAccountAdded = "Account has been added to the account list";
			Strings.autoSellTrashItemsBoxFull = "Your chest is full, new items can't be stored";
			Strings.autoLoginReattemptLoginIn = "Reattempt login in {0} seconds";
			Strings.paintControllerButtonsDPadArrowKeys = "Arrow keys";
			Strings.paintControllerButtonsLeftStickMove = "Move";
			Strings.paintControllerButtonsLeftStickButtonTeleport = "Teleport";
			Strings.paintControllerButtonsRightStickMoveCamera = "Move camera";
			Strings.paintControllerButtonsRightStickButtonLockCamera = "Lock camera";
			Strings.modMenuPanelTabName = new string[][]
			{
				new string[] { "Toggle", "" },
				new string[] { "Adjust", "" },
				new string[] { "Func-", "tions" },
				new string[] { "Deve-", "loper" }
			};
			Strings.vSyncDescription = "Automatically limit the FPS according to the monitor's refresh rate";
			Strings.showTargetInfoTitle = "Target's effect information";
			Strings.showTargetInfoDescription = "Show the near-accurate duration of Black Star Dragonball, Energy Shield, Super Monkey, Whistle, etc. of the target character";
			Strings.autoSendAttackDescription = "Automatically send attack commands to the target character without moving your character";
			Strings.autoLoginTitle = "Auto login";
			Strings.autoLoginDescription = "Automatically attempt to log in and return to the last map, zone and position when the connection to the server is lost";
			Strings.showCharListTitle = "Character list";
			Strings.showCharListDescription = "Show the character list in the current zone";
			Strings.showPetInCharListTitle = "Include disciples in the character list";
			Strings.showPetInCharListDescription = "Include disciples in the character list in the current zone";
			Strings.autoTrainForNewbieTitle = "Auto train new account";
			Strings.autoTrainForNewbieDescription = "Auto train new account until the clan mission";
			Strings.noLongerNewAccount = "You have completed the clan mission";
			Strings.autoSellTrashItemsTitle = "Auto sell trash items";
			Strings.autoSellTrashItemsDescription = "Automatically sell unnecessary items when the inventory is full and store valuable items in the chest, you should clean your inventory before enabling this feature to avoid losing items";
			Strings.customBackgroundTitle = "Custom background";
			Strings.customBackgroundDescription = "Replace the game default background with custom background";
			Strings.hideGameUITitle = "Hide UI";
			Strings.hideGameUIDescription = "Hide the game UI (buttons are still clickable)";
			Strings.skipSpaceshipTitle = "Skip spaceship";
			Strings.skipSpaceshipDescription = "Skip spaceship animation";
			Strings.notifyBossTitle = "Boss notification";
			Strings.notifyBossDescription = "Show boss notification list";
			Strings.introTitle = "Intro video";
			Strings.introDescription = "Play a short video when opening the game";
			Strings.xmapUseNormalCapsuleDescription = "Use normal capsule when xmap, do not enable if you don't want to waste your normal capsules";
			Strings.xmapUseSpecialCapsuleDescription = "Use special capsule when xmap";
			Strings.xmapUseAStarDescription = "Use A* algorithm to find the path instead of teleporting to the destination";
			Strings.pickMobTitle = "Slaughter";
			Strings.pickMobDescription = "Auto attack monsters";
			Strings.pickMobAvoidSuperMobTitle = "Avoid super monsters";
			Strings.avoidSuperMobDescription = "Don't attack super monsters when slaughtering";
			Strings.pickMobVDHTitle = "Cross terrain while slaughtering";
			Strings.pickMobVDHDescription = "Automatically cross terrain while slaughtering";
			Strings.pickMobAttackMonsterBySendCommandTitle = "Attack monsters by sending commands";
			Strings.pickMobAttackMonsterBySendCommandDescription = "Slaughter monsters by sending attack commands instead of double-clicking on them";
			Strings.autoPickItemTitle = "Auto pick items";
			Strings.autoPickItemDescription = "Automatically pick up nearby items";
			Strings.pickMobPickMyItemOnlyTitle = "Don't pick up other people's items";
			Strings.pickMobPickMyItemOnlyDescription = "Only pick up your own items, items dropped by your own disciples when fighting monsters, and items that do not belong to anyone";
			Strings.pickMobLimitPickTimesTitle = "Limit the number of pickup attempts";
			Strings.pickMobLimitPickTimesDescription = "Limit the number of attempts to pick up an item";
			Strings.autoAskForPeansTitle = "Auto ask for Senzu beans";
			Strings.autoAskForPeansDescription = "Automatically send messages asking for Senzu beans to the clan chat channel";
			Strings.youAreNotInAClan = "You are not in any clan";
			Strings.autoDonatePeansTitle = "Auto donate Senzu beans";
			Strings.autoDonatePeansDescription = "Automatically give Senzu beans when there is a request message from a clan member";
			Strings.autoHarvestPeansTitle = "Auto harvest Senzu beans";
			Strings.autoHarvestPeansDescription = "Automatically harvest Senzu beans from the Senzu tree at home";
			Strings.setFPSDescription = "Adjust the number of frames per second of the game";
			Strings.setGameSpeedTitle = "Game speed";
			Strings.setGameSpeedDescription = "Adjust the game speed";
			Strings.setGameDelayTitle = "Game delay";
			Strings.setGameDelayDescription = "Adjust the game delay time";
			Strings.setReduceGraphicsTitle = "Reduce graphics quality";
			Strings.setReduceGraphicsChoices = new string[]
			{
				"Disabled",
				"Enabled (" + Strings.level.ToLower() + " 1)",
				"Enabled (" + Strings.level.ToLower() + " 2)",
				"Enabled (" + Strings.level.ToLower() + " 3)"
			};
			Strings.setMyCharSpeedTitle = "Character speed";
			Strings.setMyCharSpeedDescription = "Adjust the movement speed of my character";
			Strings.setGoBackChoices = new string[] { "Disabled", "Enabled (return to the same place where you died)", "Enabled (go to the fixed map, zone, and position when you died)" };
			Strings.setAutoTrainPetTitle = "Auto train disciple";
			Strings.setAutoTrainPetChoices = new string[] { "Disabled", "Enabled (normal mode)", "Enabled (avoid super monsters mode)", "Enabled (kaioken mode)" };
			Strings.youDontHaveDisciple = "You don't have disciple";
			Strings.setAutoAttackWhenDiscipleNeededTitle = "Attack when the disciple needs";
			Strings.setAutoAttackWhenDiscipleNeededChoices = new string[] { "Nearest monster", "Attack the disciple (automatically change flag to gray)", "Attack self (automatically change flag to gray)" };
			Strings.setAutoRescueTitle = "Auto rescue";
			Strings.setAutoRescueChoices = new string[] { "Disabled", "Enabled (everyone)", "Enabled (clan members only)", "Enabled (disciple only)", "Enabled (own disciple only)" };
			Strings.setAutoRescueSkill3Null = "You don't have Rescue skill";
			Strings.setAutoRescueSkill3BuffInvalid = "Your third skill is not Rescue";
			Strings.customBgDefaultScaleModeTitle = "Default background scale mode";
			Strings.setXmapTimeoutDescription = "Edit the timeout (seconds) before switching to the teleporting mode when using xmap";
			Strings.setTimeChangeCustomBgTitle = "Background duration";
			Strings.setTimeChangeCustomBgDescription = "Adjust the time to switch background (seconds)";
			Strings.setIntroVolumeTitle = "Change intro volume";
			Strings.setIntroVolumeDescription = "Adjust the volume of the video played when opening the game";
			Strings.openXmapMenuTitle = "Xmap menu";
			Strings.openXmapMenuDescription = "Open the Xmap menu (\"xmp\" chat command or 'x' key)";
			Strings.openPickMobMenuTitle = "PickMob menu";
			Strings.openPickMobMenuDescription = "Open the PickMob menu (\"pickmob\" chat command)";
			Strings.openTeleportMenuTitle = "Teleport menu";
			Strings.openTeleportMenuDescription = "Open the Teleport menu (\"tele\" chat command or 'z' key)";
			Strings.openCustomBackgroundMenuTitle = "Custom Background menu";
			Strings.openCustomBackgroundMenuDescription = "Open the Custom Background menu";
			Strings.openIntroMenuTitle = "Intro menu";
			Strings.openIntroMenuDescription = "Open the intro video menu";
			Strings.introSelectFile = "Select video file";
			Strings.openSetsMenuTitle = "Sets menu";
			Strings.openSetsMenuDescription = "Open the Sets menu (\"set\" command or '`' key)";
			Strings.openVietnameseInputMenuTitle = "Vietnamese typing menu";
			Strings.openVietnameseInputMenuDescription = "Open the Vietnamese typing menu, useful when external Vietnamese typing programs (UniKey, VietKey, etc.) doesn't work";
			Strings.addUserAoToAccountManagerTitle = "Add current account to the account list";
			Strings.addUserAoToAccountManagerDescription = "Add the current unregistered account to the account list";
			Strings.openedByExternalAccountManager = "Your account is managed by external Account manager";
			Strings.accountAlreadyRegistered = "Account already registered";
		}

		// Token: 0x0400158C RID: 5516
		internal static readonly string DEFAULT_IP_SERVERS = "Vũ trụ 1:dragon1.teamobi.com:14445:0:0:0,Vũ trụ 2:dragon2.teamobi.com:14445:0:0:0,Vũ trụ 3:dragon3.teamobi.com:14445:0:0:0,Vũ trụ 4:dragon4.teamobi.com:14445:0:0:0,Vũ trụ 5:dragon5.teamobi.com:14445:0:0:0,Vũ trụ 6:dragon6.teamobi.com:14445:0:0:0,Vũ trụ 7:dragon7.teamobi.com:14445:0:0:0,Vũ trụ 8:dragon10.teamobi.com:14446:0:0:0,Vũ trụ 9:dragon10.teamobi.com:14447:0:0:0,Vũ trụ 10:dragon10.teamobi.com:14445:0:0:0,Vũ trụ 11:dragon11.teamobi.com:14445:0:0:0,Vũ trụ 12:dragon12.teamobi.com:14445:0:0:0,Vũ trụ 13:dragon13.teamobi.com:14446:0:0:0,Super 1:dragon11.teamobi.com:14446:0:1:0,Super 2:dragon11.teamobi.com:17001:0:1:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0:0:0,Universe 1:dragon.indonaga.com:14445:1:0:0,Naga:dragon.indonaga.com:14446:2:0:0,0,0";

		// Token: 0x0400158D RID: 5517
		internal static string communityMod = "";

		// Token: 0x0400158E RID: 5518
		internal static string gameVersion = "";

		// Token: 0x0400158F RID: 5519
		internal static string registered = "";

		// Token: 0x04001590 RID: 5520
		internal static string autoChatDisabled = "";

		// Token: 0x04001591 RID: 5521
		internal static string inputContent = "";

		// Token: 0x04001592 RID: 5522
		internal static string delaySeconds = "";

		// Token: 0x04001593 RID: 5523
		internal static string viewContent = "";

		// Token: 0x04001594 RID: 5524
		internal static string autoChatContent = "";

		// Token: 0x04001595 RID: 5525
		internal static string inputDelay = "";

		// Token: 0x04001596 RID: 5526
		internal static string timeMilliseconds = "";

		// Token: 0x04001597 RID: 5527
		internal static string errorOccurred = "";

		// Token: 0x04001598 RID: 5528
		internal static string contentSaved = "";

		// Token: 0x04001599 RID: 5529
		internal static string autoAttack = "";

		// Token: 0x0400159A RID: 5530
		internal static string gobackTo = "";

		// Token: 0x0400159B RID: 5531
		internal static string youAreNotNamekian = "";

		// Token: 0x0400159C RID: 5532
		internal static string completed = "";

		// Token: 0x0400159D RID: 5533
		internal static string[][] modMenuPanelTabName = new string[0][];

		// Token: 0x0400159E RID: 5534
		internal static string someonePet = "";

		// Token: 0x0400159F RID: 5535
		internal static string petLostMaster = "";

		// Token: 0x040015A0 RID: 5536
		internal static string zone = "";

		// Token: 0x040015A1 RID: 5537
		internal static string vSyncDescription = "";

		// Token: 0x040015A2 RID: 5538
		internal static string showTargetInfoTitle = "";

		// Token: 0x040015A3 RID: 5539
		internal static string showTargetInfoDescription = "";

		// Token: 0x040015A4 RID: 5540
		internal static string autoSendAttackDescription = "";

		// Token: 0x040015A5 RID: 5541
		internal static string autoLoginTitle = "";

		// Token: 0x040015A6 RID: 5542
		internal static string autoLoginDescription = "";

		// Token: 0x040015A7 RID: 5543
		internal static string showCharListTitle = "";

		// Token: 0x040015A8 RID: 5544
		internal static string showCharListDescription = "";

		// Token: 0x040015A9 RID: 5545
		internal static string showPetInCharListTitle = "";

		// Token: 0x040015AA RID: 5546
		internal static string showPetInCharListDescription = "";

		// Token: 0x040015AB RID: 5547
		internal static string autoTrainForNewbieTitle = "";

		// Token: 0x040015AC RID: 5548
		internal static string autoTrainForNewbieDescription = "";

		// Token: 0x040015AD RID: 5549
		internal static string autoSellTrashItemsTitle = "";

		// Token: 0x040015AE RID: 5550
		internal static string autoSellTrashItemsDescription = "";

		// Token: 0x040015AF RID: 5551
		internal static string noLongerNewAccount = "";

		// Token: 0x040015B0 RID: 5552
		internal static string customBackgroundTitle = "";

		// Token: 0x040015B1 RID: 5553
		internal static string hideGameUITitle = "";

		// Token: 0x040015B2 RID: 5554
		internal static string hideGameUIDescription = "";

		// Token: 0x040015B3 RID: 5555
		internal static string customBackgroundDescription = "";

		// Token: 0x040015B4 RID: 5556
		internal static string notifyBossTitle = "";

		// Token: 0x040015B5 RID: 5557
		internal static string notifyBossDescription = "";

		// Token: 0x040015B6 RID: 5558
		internal static string pickMobTitle = "";

		// Token: 0x040015B7 RID: 5559
		internal static string pickMobDescription = "";

		// Token: 0x040015B8 RID: 5560
		internal static string functionShouldBeDisabled = "";

		// Token: 0x040015B9 RID: 5561
		internal static string functionShouldBeEnabled = "";

		// Token: 0x040015BA RID: 5562
		internal static string pickMobAvoidSuperMobTitle = "";

		// Token: 0x040015BB RID: 5563
		internal static string avoidSuperMobDescription = "";

		// Token: 0x040015BC RID: 5564
		internal static string pickMobVDHTitle = "";

		// Token: 0x040015BD RID: 5565
		internal static string pickMobVDHDescription = "";

		// Token: 0x040015BE RID: 5566
		internal static string pickMobAttackMonsterBySendCommandTitle = "";

		// Token: 0x040015BF RID: 5567
		internal static string pickMobAttackMonsterBySendCommandDescription = "";

		// Token: 0x040015C0 RID: 5568
		internal static string autoPickItemTitle = "";

		// Token: 0x040015C1 RID: 5569
		internal static string autoPickItemDescription = "";

		// Token: 0x040015C2 RID: 5570
		internal static string pickMobPickMyItemOnlyTitle = "";

		// Token: 0x040015C3 RID: 5571
		internal static string pickMobPickMyItemOnlyDescription = "";

		// Token: 0x040015C4 RID: 5572
		internal static string pickMobLimitPickTimesTitle = "";

		// Token: 0x040015C5 RID: 5573
		internal static string pickMobLimitPickTimesDescription = "";

		// Token: 0x040015C6 RID: 5574
		internal static string autoAskForPeansTitle = "";

		// Token: 0x040015C7 RID: 5575
		internal static string autoAskForPeansDescription = "";

		// Token: 0x040015C8 RID: 5576
		internal static string youAreNotInAClan = "";

		// Token: 0x040015C9 RID: 5577
		internal static string autoDonatePeansTitle = "";

		// Token: 0x040015CA RID: 5578
		internal static string autoDonatePeansDescription = "";

		// Token: 0x040015CB RID: 5579
		internal static string autoHarvestPeansTitle = "";

		// Token: 0x040015CC RID: 5580
		internal static string autoHarvestPeansDescription = "";

		// Token: 0x040015CD RID: 5581
		internal static string setFPSDescription = "";

		// Token: 0x040015CE RID: 5582
		internal static string setGameSpeedTitle = "";

		// Token: 0x040015CF RID: 5583
		internal static string setGameSpeedDescription = "";

		// Token: 0x040015D0 RID: 5584
		internal static string setReduceGraphicsTitle = "";

		// Token: 0x040015D1 RID: 5585
		internal static string level = "";

		// Token: 0x040015D2 RID: 5586
		internal static string[] setReduceGraphicsChoices = new string[0];

		// Token: 0x040015D3 RID: 5587
		internal static string setMyCharSpeedTitle = "";

		// Token: 0x040015D4 RID: 5588
		internal static string setMyCharSpeedDescription = "";

		// Token: 0x040015D5 RID: 5589
		internal static string[] setGoBackChoices = new string[0];

		// Token: 0x040015D6 RID: 5590
		internal static string setAutoTrainPetTitle = "";

		// Token: 0x040015D7 RID: 5591
		internal static string[] setAutoTrainPetChoices = new string[0];

		// Token: 0x040015D8 RID: 5592
		internal static string youDontHaveDisciple = "";

		// Token: 0x040015D9 RID: 5593
		internal static string setAutoAttackWhenDiscipleNeededTitle = "";

		// Token: 0x040015DA RID: 5594
		internal static string[] setAutoAttackWhenDiscipleNeededChoices = new string[0];

		// Token: 0x040015DB RID: 5595
		internal static string setAutoRescueTitle = "";

		// Token: 0x040015DC RID: 5596
		internal static string[] setAutoRescueChoices = new string[0];

		// Token: 0x040015DD RID: 5597
		internal static string setTimeChangeCustomBgTitle = "";

		// Token: 0x040015DE RID: 5598
		internal static string customBgDefaultScaleModeTitle = "";

		// Token: 0x040015DF RID: 5599
		internal static string setTimeChangeCustomBgDescription = "";

		// Token: 0x040015E0 RID: 5600
		internal static string openXmapMenuTitle = "";

		// Token: 0x040015E1 RID: 5601
		internal static string openXmapMenuDescription = "";

		// Token: 0x040015E2 RID: 5602
		internal static string openPickMobMenuTitle = "";

		// Token: 0x040015E3 RID: 5603
		internal static string openPickMobMenuDescription = "";

		// Token: 0x040015E4 RID: 5604
		internal static string openTeleportMenuTitle = "";

		// Token: 0x040015E5 RID: 5605
		internal static string openTeleportMenuDescription = "";

		// Token: 0x040015E6 RID: 5606
		internal static string openCustomBackgroundMenuTitle = "";

		// Token: 0x040015E7 RID: 5607
		internal static string openCustomBackgroundMenuDescription = "";

		// Token: 0x040015E8 RID: 5608
		internal static string openSetsMenuTitle = "";

		// Token: 0x040015E9 RID: 5609
		internal static string openSetsMenuDescription = "";

		// Token: 0x040015EA RID: 5610
		internal static string valueChanged = "";

		// Token: 0x040015EB RID: 5611
		internal static string invalidValue = "";

		// Token: 0x040015EC RID: 5612
		internal static string inputFPS = "";

		// Token: 0x040015ED RID: 5613
		internal static string inputGameSpeed = "";

		// Token: 0x040015EE RID: 5614
		internal static string inputGameSpeedHint = "";

		// Token: 0x040015EF RID: 5615
		internal static string setGameDelayTitle = "";

		// Token: 0x040015F0 RID: 5616
		internal static string setGameDelayDescription = "";

		// Token: 0x040015F1 RID: 5617
		internal static string inputGameDelay = "";

		// Token: 0x040015F2 RID: 5618
		internal static string inputGameDelayHint = "";

		// Token: 0x040015F3 RID: 5619
		internal static string inputMyCharSpeed = "";

		// Token: 0x040015F4 RID: 5620
		internal static string inputMyCharSpeedHint = "";

		// Token: 0x040015F5 RID: 5621
		internal static string inputTimeChangeBg = "";

		// Token: 0x040015F6 RID: 5622
		internal static string inputTimeChangeBgHint = "";

		// Token: 0x040015F7 RID: 5623
		internal static string customBgChatPopup = "";

		// Token: 0x040015F8 RID: 5624
		internal static string customBgOpenBgList = "";

		// Token: 0x040015F9 RID: 5625
		internal static string customBgAddNewBg = "";

		// Token: 0x040015FA RID: 5626
		internal static string customBgRemoveAll = "";

		// Token: 0x040015FB RID: 5627
		internal static string customBgAllBgRemoved = "";

		// Token: 0x040015FC RID: 5628
		internal static string customBgAutoChangeBg = "";

		// Token: 0x040015FD RID: 5629
		internal static string customBgScaleMode = "";

		// Token: 0x040015FE RID: 5630
		internal static string customBgResetScaleModeToDefault = "";

		// Token: 0x040015FF RID: 5631
		internal static string customBgSetTimeChange = "";

		// Token: 0x04001600 RID: 5632
		internal static string customBgChangeGifSpeed = "";

		// Token: 0x04001601 RID: 5633
		internal static string customBgInputGifSpeed = "";

		// Token: 0x04001602 RID: 5634
		internal static string speed = "";

		// Token: 0x04001603 RID: 5635
		internal static string customBgSwitchToThisBg = "";

		// Token: 0x04001604 RID: 5636
		internal static string delete = "";

		// Token: 0x04001605 RID: 5637
		internal static string customBgRemovedBg = "";

		// Token: 0x04001606 RID: 5638
		internal static string fullPath = "";

		// Token: 0x04001607 RID: 5639
		internal static string customBgList = "";

		// Token: 0x04001608 RID: 5640
		internal static string inputNumberOutOfRange = "";

		// Token: 0x04001609 RID: 5641
		internal static string customBgGifSpeed = "";

		// Token: 0x0400160A RID: 5642
		internal static string inputNumberMustBeBiggerThanOrEqual = "";

		// Token: 0x0400160B RID: 5643
		internal static string imageVideoFile = "";

		// Token: 0x0400160C RID: 5644
		internal static string videoFile = "";

		// Token: 0x0400160D RID: 5645
		internal static string allFileTypes = "";

		// Token: 0x0400160E RID: 5646
		internal static string customBgSelectBgFiles = "";

		// Token: 0x0400160F RID: 5647
		internal static string skipSpaceshipTitle = "";

		// Token: 0x04001610 RID: 5648
		internal static string skipSpaceshipDescription = "";

		// Token: 0x04001611 RID: 5649
		internal static string setAutoRescueSkill3BuffInvalid = "";

		// Token: 0x04001612 RID: 5650
		internal static string setAutoRescueSkill3Null = "";

		// Token: 0x04001613 RID: 5651
		internal static string pickMobMonsterAdded = "";

		// Token: 0x04001614 RID: 5652
		internal static string pickMobMonsterRemoved = "";

		// Token: 0x04001615 RID: 5653
		internal static string pickMobMonsterTypeAdded = "";

		// Token: 0x04001616 RID: 5654
		internal static string pickMobMonsterTypeRemoved = "";

		// Token: 0x04001617 RID: 5655
		internal static string pickMobAutoPickItemListRemoved = "";

		// Token: 0x04001618 RID: 5656
		internal static string pickMobAutoPickItemListAdded = "";

		// Token: 0x04001619 RID: 5657
		internal static string pickMobAutoPickItemTypesListRemoved = "";

		// Token: 0x0400161A RID: 5658
		internal static string pickMobAutoPickItemTypesListAdded = "";

		// Token: 0x0400161B RID: 5659
		internal static string pickMobPlsFocusOnMonsterOrItem = "";

		// Token: 0x0400161C RID: 5660
		internal static string pickMobMonsterListCleared = "";

		// Token: 0x0400161D RID: 5661
		internal static string pickMobItemListResetToDefault = "";

		// Token: 0x0400161E RID: 5662
		internal static string pickMobConfiguredPickGemsOnly = "";

		// Token: 0x0400161F RID: 5663
		internal static string pickMobSkillListRemoved = "";

		// Token: 0x04001620 RID: 5664
		internal static string pickMobSkillListAdded = "";

		// Token: 0x04001621 RID: 5665
		internal static string pickMobSkillListResetToDefault = "";

		// Token: 0x04001622 RID: 5666
		internal static string pickMobDontPickItemListRemoved = "";

		// Token: 0x04001623 RID: 5667
		internal static string pickMobDontPickItemListAdded = "";

		// Token: 0x04001624 RID: 5668
		internal static string pickMobPlsFocusOnItem = "";

		// Token: 0x04001625 RID: 5669
		internal static string pickMobDontPickItemTypeListAdded = "";

		// Token: 0x04001626 RID: 5670
		internal static string pickMobDontPickItemTypeListRemoved = "";

		// Token: 0x04001627 RID: 5671
		internal static string pickMobFocusedMob = "";

		// Token: 0x04001628 RID: 5672
		internal static string pickMobFocusedItem = "";

		// Token: 0x04001629 RID: 5673
		internal static string pickMobRemoveMobIdFromList = "";

		// Token: 0x0400162A RID: 5674
		internal static string pickMobAddMobIdToList = "";

		// Token: 0x0400162B RID: 5675
		internal static string pickMobRemoveFromList = "";

		// Token: 0x0400162C RID: 5676
		internal static string pickMobAddToList = "";

		// Token: 0x0400162D RID: 5677
		internal static string pickMobAddItemTypeToList = "";

		// Token: 0x0400162E RID: 5678
		internal static string pickMobRemoveItemTypeFromList = "";

		// Token: 0x0400162F RID: 5679
		internal static string pickMobRemoveFromDontPickList = "";

		// Token: 0x04001630 RID: 5680
		internal static string pickMobAddToDontPickList = "";

		// Token: 0x04001631 RID: 5681
		internal static string pickMobRemoveItemTypeFromDontPickList = "";

		// Token: 0x04001632 RID: 5682
		internal static string pickMobAddItemTypeToDontPickList = "";

		// Token: 0x04001633 RID: 5683
		internal static string pickMobClearMonsterList = "";

		// Token: 0x04001634 RID: 5684
		internal static string pickMobAddToSkillList = "";

		// Token: 0x04001635 RID: 5685
		internal static string pickMobRemoveFromSkillList = "";

		// Token: 0x04001636 RID: 5686
		internal static string pickMobResetSkillListToDefault = "";

		// Token: 0x04001637 RID: 5687
		internal static string pickMobResetItemListToDefault = "";

		// Token: 0x04001638 RID: 5688
		internal static string pickMobViewMonsterList = "";

		// Token: 0x04001639 RID: 5689
		internal static string empty = "";

		// Token: 0x0400163A RID: 5690
		internal static string pickMobMonsterIdList = "";

		// Token: 0x0400163B RID: 5691
		internal static string pickMobMonsterTypeList = "";

		// Token: 0x0400163C RID: 5692
		internal static string pickMobViewItemList = "";

		// Token: 0x0400163D RID: 5693
		internal static string pickMobAutoPickItemList = "";

		// Token: 0x0400163E RID: 5694
		internal static object pickMobAutoPickItemTypeList = "";

		// Token: 0x0400163F RID: 5695
		internal static object pickMobDontPickItemList = "";

		// Token: 0x04001640 RID: 5696
		internal static object pickMobDontPickItemTypeList = "";

		// Token: 0x04001641 RID: 5697
		internal static string pickMobViewSkillList = "";

		// Token: 0x04001642 RID: 5698
		internal static string pickMobSkillList = "";

		// Token: 0x04001643 RID: 5699
		internal static string introCurrentPath = "";

		// Token: 0x04001644 RID: 5700
		internal static string introChangeVideoPath = "";

		// Token: 0x04001645 RID: 5701
		internal static string setIntroVolumeTitle = "";

		// Token: 0x04001646 RID: 5702
		internal static string introInputVolume = "";

		// Token: 0x04001647 RID: 5703
		internal static string introInputVolumeHint = "";

		// Token: 0x04001648 RID: 5704
		internal static string introTitle = "";

		// Token: 0x04001649 RID: 5705
		internal static string introDescription = "";

		// Token: 0x0400164A RID: 5706
		internal static string openIntroMenuTitle = "";

		// Token: 0x0400164B RID: 5707
		internal static string openIntroMenuDescription = "";

		// Token: 0x0400164C RID: 5708
		internal static string setIntroVolumeDescription = "";

		// Token: 0x0400164D RID: 5709
		internal static string introNoVideo = "";

		// Token: 0x0400164E RID: 5710
		internal static string introSelectFile = "";

		// Token: 0x0400164F RID: 5711
		internal static string xmapUseSpecialCapsule = "";

		// Token: 0x04001650 RID: 5712
		internal static string xmapUseNormalCapsule = "";

		// Token: 0x04001651 RID: 5713
		internal static string xmapCanceled = "";

		// Token: 0x04001652 RID: 5714
		internal static string xmapChatPopup = "";

		// Token: 0x04001653 RID: 5715
		internal static string goTo = "";

		// Token: 0x04001654 RID: 5716
		internal static string xmapCantFindWay = "";

		// Token: 0x04001655 RID: 5717
		internal static string xmapDestinationReached = "";

		// Token: 0x04001656 RID: 5718
		internal static string scaleModeStretchToFill = "";

		// Token: 0x04001657 RID: 5719
		internal static string scaleModeScaleAndCrop = "";

		// Token: 0x04001658 RID: 5720
		internal static string scaleModeScaleToFit = "";

		// Token: 0x04001659 RID: 5721
		internal static string teleportMenuOpenSavedCharList = "";

		// Token: 0x0400165A RID: 5722
		internal static string add = "";

		// Token: 0x0400165B RID: 5723
		internal static string teleportMenuCharacterAdded = "";

		// Token: 0x0400165C RID: 5724
		internal static string teleportMenuCantRemoveTargetChar = "";

		// Token: 0x0400165D RID: 5725
		internal static string teleportMenuCharacterRemoved = "";

		// Token: 0x0400165E RID: 5726
		internal static string teleportMenuStopTeleporting = "";

		// Token: 0x0400165F RID: 5727
		internal static string teleportMenuSelectTarget = "";

		// Token: 0x04001660 RID: 5728
		internal static string teleportMenuStopTeleportToTarget = "";

		// Token: 0x04001661 RID: 5729
		internal static string teleportMenuAddCharacterByID = "";

		// Token: 0x04001662 RID: 5730
		internal static string teleportMenuAddEveryoneInZone = "";

		// Token: 0x04001663 RID: 5731
		internal static string teleportMenuEveryoneAdded = "";

		// Token: 0x04001664 RID: 5732
		internal static string teleportMenuRemoveCharacter = "";

		// Token: 0x04001665 RID: 5733
		internal static string deleteAll = "";

		// Token: 0x04001666 RID: 5734
		internal static string teleportMenuCleared = "";

		// Token: 0x04001667 RID: 5735
		internal static string teleportMenuTeleportingToCharacter = "";

		// Token: 0x04001668 RID: 5736
		internal static string more = "";

		// Token: 0x04001669 RID: 5737
		internal static string teleportMenuNoRemovableChar = "";

		// Token: 0x0400166A RID: 5738
		internal static string teleportMenuAutoTeleportTo = "";

		// Token: 0x0400166B RID: 5739
		internal static string teleportMenuCharacterList = "";

		// Token: 0x0400166C RID: 5740
		internal static string teleportMenuInputCharIDTextFieldName = "";

		// Token: 0x0400166D RID: 5741
		internal static string teleportMenuInputCharIDTextFieldHint = "";

		// Token: 0x0400166E RID: 5742
		internal static string teleportMenuAddedCharacterWithID = "";

		// Token: 0x0400166F RID: 5743
		internal static string openVietnameseInputMenuTitle = "";

		// Token: 0x04001670 RID: 5744
		internal static string openVietnameseInputMenuDescription = "";

		// Token: 0x04001671 RID: 5745
		internal static string vnInputInputMethod = "";

		// Token: 0x04001672 RID: 5746
		internal static string vnInputDiacritics = "";

		// Token: 0x04001673 RID: 5747
		internal static string vnInputConsumeRepeatKey = "";

		// Token: 0x04001674 RID: 5748
		internal static string vnInputEnable = "";

		// Token: 0x04001675 RID: 5749
		internal static string accounts = "";

		// Token: 0x04001676 RID: 5750
		internal static string lastLogin = "";

		// Token: 0x04001677 RID: 5751
		internal static string justNow = "";

		// Token: 0x04001678 RID: 5752
		internal static string minutesAgo = "";

		// Token: 0x04001679 RID: 5753
		internal static string hoursAgo = "";

		// Token: 0x0400167A RID: 5754
		internal static string yesterdayAt = "";

		// Token: 0x0400167B RID: 5755
		internal static string back = "";

		// Token: 0x0400167C RID: 5756
		internal static string haventLoggedInYet = "";

		// Token: 0x0400167D RID: 5757
		internal static string info = "";

		// Token: 0x0400167E RID: 5758
		internal static string master = "";

		// Token: 0x0400167F RID: 5759
		internal static string gender = "";

		// Token: 0x04001680 RID: 5760
		internal static string name = "";

		// Token: 0x04001681 RID: 5761
		internal static string edit = "";

		// Token: 0x04001682 RID: 5762
		internal static string select = "";

		// Token: 0x04001683 RID: 5763
		internal static string save = "";

		// Token: 0x04001684 RID: 5764
		internal static string import = "";

		// Token: 0x04001685 RID: 5765
		internal static string logout = "";

		// Token: 0x04001686 RID: 5766
		internal static string inGameAccountManagerConfirmDeleteAcc = "";

		// Token: 0x04001687 RID: 5767
		internal static string inGameAccountManagerAddAccount = "";

		// Token: 0x04001688 RID: 5768
		internal static string inGameAccountManagerEditAccount = "";

		// Token: 0x04001689 RID: 5769
		internal static string inGameAccountManagerServerBlank = "";

		// Token: 0x0400168A RID: 5770
		internal static string inGameAccountManagerUnregisteredAccountMustBeOnTeaMobiServer = "";

		// Token: 0x0400168B RID: 5771
		internal static string inGameAccountManagerEditServer = "";

		// Token: 0x0400168C RID: 5772
		internal static string inGameAccountManagerServerName = "";

		// Token: 0x0400168D RID: 5773
		internal static string inGameAccountManagerServerAddress = "";

		// Token: 0x0400168E RID: 5774
		internal static string inGameAccountManagerServerPort = "";

		// Token: 0x0400168F RID: 5775
		internal static string inGameAccountManagerImportAccounts = "";

		// Token: 0x04001690 RID: 5776
		internal static string inGameAccountManagerImportAccountsInputData = "";

		// Token: 0x04001691 RID: 5777
		internal static string inGameAccountManagerRegexMatchLines = "";

		// Token: 0x04001692 RID: 5778
		internal static string inGameAccountManagerRegexMatchAccountInfo = "";

		// Token: 0x04001693 RID: 5779
		internal static string inGameAccountManagerServerNameBlank = "";

		// Token: 0x04001694 RID: 5780
		internal static string inGameAccountManagerServerAddressBlank = "";

		// Token: 0x04001695 RID: 5781
		internal static string inGameAccountManagerServerPortBlank = "";

		// Token: 0x04001696 RID: 5782
		internal static string inGameAccountManagerServerPortInvalid = "";

		// Token: 0x04001697 RID: 5783
		internal static string inGameAccountManagerImportAccountsHelp = "";

		// Token: 0x04001698 RID: 5784
		internal static string inGameAccountManagerImportAccountsInputDataBlank = "";

		// Token: 0x04001699 RID: 5785
		internal static string inGameAccountManagerRegexMatchLinesBlank = "";

		// Token: 0x0400169A RID: 5786
		internal static string inGameAccountManagerRegexMatchAccountInfoBlank = "";

		// Token: 0x0400169B RID: 5787
		internal static string inGameAccountManagerImportAccountsResult = "";

		// Token: 0x0400169C RID: 5788
		internal static string inGameAccountManagerUnregisteredAccountAlreadyAdded = "";

		// Token: 0x0400169D RID: 5789
		internal static string inGameAccountManagerAccountAdded = "";

		// Token: 0x0400169E RID: 5790
		internal static string custom = "";

		// Token: 0x0400169F RID: 5791
		internal static string settings = "";

		// Token: 0x040016A0 RID: 5792
		internal static string xmapTimeout = "";

		// Token: 0x040016A1 RID: 5793
		internal static string xmapUseAStar = "";

		// Token: 0x040016A2 RID: 5794
		internal static string xmapEditTimeout = "";

		// Token: 0x040016A3 RID: 5795
		internal static string timeout = "";

		// Token: 0x040016A4 RID: 5796
		internal static string xmapUseNormalCapsuleDescription = "";

		// Token: 0x040016A5 RID: 5797
		internal static string xmapUseSpecialCapsuleDescription = "";

		// Token: 0x040016A6 RID: 5798
		internal static string xmapUseAStarDescription = "";

		// Token: 0x040016A7 RID: 5799
		internal static string setXmapTimeoutDescription = "";

		// Token: 0x040016A8 RID: 5800
		internal static string addUserAoToAccountManagerTitle = "";

		// Token: 0x040016A9 RID: 5801
		internal static string addUserAoToAccountManagerDescription = "";

		// Token: 0x040016AA RID: 5802
		internal static string openedByExternalAccountManager = "";

		// Token: 0x040016AB RID: 5803
		internal static string accountAlreadyRegistered = "";

		// Token: 0x040016AC RID: 5804
		internal static string autoSellTrashItemsBoxFull = "";

		// Token: 0x040016AD RID: 5805
		internal static string autoLoginReattemptLoginIn = "";

		// Token: 0x040016AE RID: 5806
		internal static string paintControllerButtonsDPadArrowKeys = "";

		// Token: 0x040016AF RID: 5807
		internal static string paintControllerButtonsLeftStickMove = "";

		// Token: 0x040016B0 RID: 5808
		internal static string paintControllerButtonsLeftStickButtonTeleport = "";

		// Token: 0x040016B1 RID: 5809
		internal static string paintControllerButtonsRightStickMoveCamera = "";

		// Token: 0x040016B2 RID: 5810
		internal static string paintControllerButtonsRightStickButtonLockCamera = "";
	}
}
