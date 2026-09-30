using System;

namespace Mod.UI
{
    // Mở lại menu mod sau khi chọn một lệnh (bật/tắt, nhập số, thêm/xóa...), khỏi phải vào lại từ đầu.
    // - Mỗi hàm ShowMenu của mod gọi Remember(...) trước menu.startAt
    // - Menu.doCloseMenu gọi BeforePerform/AfterPerform quanh command.performAction()
    // - Lệnh "bắt đầu làm việc" (tàn sát, đi map...) gọi NoReopen() để menu tắt như cũ
    // - Update() mỗi frame: chờ ô nhập / panel / dialog do lệnh mở ra đóng hết rồi mới mở lại menu
    public static class ModMenuHelper
    {
        public delegate void ShowMenuFn();

        // Menu mod đang hiện
        private static ShowMenuFn current;

        // Menu chờ mở lại + vị trí đang chọn / cuộn lúc bấm lệnh
        private static ShowMenuFn pending;
        private static int pendingIndex;
        private static int pendingCmx;

        private static ShowMenuFn performingFn;
        private static bool noReopen;

        public static void Remember(ShowMenuFn fn)
        {
            current = fn;
            pending = null;
        }

        public static void NoReopen()
        {
            noReopen = true;
        }

        public static void BeforePerform(Command cmd)
        {
            performingFn = IsModCommand(cmd) ? current : null;
            pendingIndex = GameCanvas.menu.menuSelectedItem;
            pendingCmx = global::Menu.cmx;
            current = null;
            noReopen = false;
        }

        public static void AfterPerform()
        {
            ShowMenuFn fn = performingFn;
            performingFn = null;
            if (fn == null || noReopen)
            {
                noReopen = false;
                return;
            }
            // Lệnh đã tự mở menu con thì thôi
            if (GameCanvas.menu.showMenu)
            {
                return;
            }
            pending = fn;
        }

        // Menu đóng bằng nút đóng / bấm ra ngoài
        public static void OnMenuClosed()
        {
            current = null;
            pending = null;
        }

        public static void Update()
        {
            if (pending == null)
            {
                return;
            }
            // Đã có menu khác mở (menu NPC, menu game...) thì bỏ
            if (GameCanvas.menu.showMenu)
            {
                pending = null;
                return;
            }
            if (IsBusy())
            {
                return;
            }
            ShowMenuFn fn = pending;
            pending = null;
            fn();
            RestorePosition();
        }

        // Giữ lại mục đang chọn và vị trí cuộn để menu dài không nhảy về đầu
        private static void RestorePosition()
        {
            global::Menu menu = GameCanvas.menu;
            if (!menu.showMenu || menu.menuItems == null)
            {
                return;
            }
            if (pendingIndex >= 0 && pendingIndex < menu.menuItems.size())
            {
                menu.menuSelectedItem = pendingIndex;
            }
            int cmx = System.Math.Max(0, System.Math.Min(pendingCmx, global::Menu.cmxLim));
            global::Menu.cmx = cmx;
            global::Menu.cmtoX = cmx;
        }

        private static bool IsBusy()
        {
            return ChatTextField.gI().isShow
                || GameCanvas.currentDialog != null
                || InfoDlg.isShow
                || ChatPopup.currChatPopup != null
                || ChatPopup.serverChatPopUp != null
                || (GameCanvas.panel != null && GameCanvas.panel.isShow)
                || (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
                || Mod.Menu.ItemListPanel.gI().isShowing
                || Mod.Menu.SkillSelectionPanel.gI().isShowing;
        }

        private static bool IsModCommand(Command cmd)
        {
            if (cmd == null || cmd.actionListener == null)
            {
                return false;
            }
            string ns = cmd.actionListener.GetType().Namespace;
            return ns != null && ns.StartsWith("Mod");
        }
    }
}
