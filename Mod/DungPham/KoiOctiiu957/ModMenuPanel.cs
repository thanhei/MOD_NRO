using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mod.DungPham.KoiOctiiu957
{
    public class ModMenuPanel
    {
        public static bool isShowMenu = false;
        public static int menuX, menuY, menuW, menuH;
        public static int currentTab = 0;
        public static int trainMode = 0; // 0 = Theo Tên, 1 = Vị trí
        public static bool isSelectAllMobs = true;
        public static List<int> ignoredMobTemplates = new List<int>();
        public static string[] tabNames = new string[] { "Luyện Tập", "Vật Phẩm" };

        public static void Paint(mGraphics g)
        {
            if (!isShowMenu) return;

            menuW = 280;
            menuH = 220;
            menuX = (GameCanvas.w - menuW) / 2;
            menuY = (GameCanvas.h - menuH) / 2;

            // Native NRO Frame
            GameCanvas.paintz.paintFrame(menuX, menuY, menuW, menuH, g);

            // Title
            mFont.tahoma_7b_dark.drawString(g, "Tự Động (Mod)", menuX + menuW / 2, menuY + 12, mFont.CENTER);

            // Simulated Premium Close button [X]
            int btnX = menuX + menuW - 22;
            int btnY = menuY + 8;
            g.setColor(6702080); // Brown border
            g.fillRect(btnX, btnY, 16, 16);
            g.setColor(15886616); // Orange-red inner
            g.fillRect(btnX + 1, btnY + 1, 14, 14);
            g.setColor(16777215); // White highlight top/left
            g.drawLine(btnX + 1, btnY + 1, btnX + 14, btnY + 1);
            g.drawLine(btnX + 1, btnY + 1, btnX + 1, btnY + 14);
            
            // The X
            g.setColor(16777215); // White
            g.drawLine(btnX + 4, btnY + 4, btnX + 11, btnY + 11);
            g.drawLine(btnX + 4, btnY + 5, btnX + 10, btnY + 11);
            g.drawLine(btnX + 5, btnY + 4, btnX + 11, btnY + 10);
            g.drawLine(btnX + 11, btnY + 4, btnX + 4, btnY + 11);
            g.drawLine(btnX + 10, btnY + 4, btnX + 4, btnY + 10);
            g.drawLine(btnX + 11, btnY + 5, btnX + 5, btnY + 11);

            // Draw Tabs
            int tabAreaX = menuX + 6;
            int tabAreaY = menuY + 30;
            int tabW = (menuW - 12) / tabNames.Length;
            int tabH = 22;

            for (int i = 0; i < tabNames.Length; i++)
            {
                int tx = tabAreaX + i * tabW;
                
                if (currentTab == i) {
                    g.setColor(14338484); // Beige (Selected)
                    g.fillRect(tx, tabAreaY, tabW, tabH);
                    mFont.tahoma_7b_dark.drawString(g, tabNames[i], tx + tabW / 2, tabAreaY + 6, mFont.CENTER);
                } else {
                    g.setColor(8413243); // Dark brown
                    g.fillRect(tx, tabAreaY, tabW, tabH);
                    mFont.tahoma_7b_white.drawString(g, tabNames[i], tx + tabW / 2, tabAreaY + 6, mFont.CENTER);
                }
                
                // Cell borders
                g.setColor(6702080); // Border brown
                g.drawRect(tx, tabAreaY, tabW, tabH);
            }

            // Fix bottom line of the selected tab to make it seamless
            g.setColor(14338484); // Beige
            g.drawLine(tabAreaX + currentTab * tabW + 1, tabAreaY + tabH, tabAreaX + (currentTab + 1) * tabW - 1, tabAreaY + tabH);

            // Content Area Y offset
            int cY = tabAreaY + tabH + 10;

            if (currentTab == 0)
            {
                // Left Column
                paintCheckbox(g, menuX + 15, cY, "Tự Động Train", AutoTrain.isAutoTrain);
                paintCheckbox(g, menuX + 15, cY + 25, "Auto Goback", AutoTrain.isGoBack);
                
                // Right Column
                paintCheckbox(g, menuX + 140, cY, "Tự Động Đánh", AutoSkill.isAutoSendAttack);
                paintCheckbox(g, menuX + 140, cY + 25, "Né Siêu Quái", AutoTrain.isAvoidSuperMob);

                cY += 55;

                // Mode Selection
                mFont.tahoma_7b_dark.drawString(g, "Chế Độ Lọc:", menuX + 15, cY + 2, mFont.LEFT);
                paintCheckbox(g, menuX + 85, cY, "Theo Tên", trainMode == 0);
                paintCheckbox(g, menuX + 175, cY, "Theo Vị Trí", trainMode == 1);

                cY += 25;

                if (trainMode == 0)
                {
                    int mX = menuX + 15;
                    List<MobTemplate> uniqueMobs = GetUniqueMobs();
                    for (int i = 0; i < uniqueMobs.Count; i++)
                    {
                        if (mX > menuX + menuW - 130)
                        {
                            mX = menuX + 15;
                            cY += 25;
                        }
                        string displayName = uniqueMobs[i].name + " (" + uniqueMobs[i].hp + ")";
                        paintCheckbox(g, mX, cY, displayName, !ignoredMobTemplates.Contains(uniqueMobs[i].mobTemplateId));
                        mX += 130;
                    }
                }
                else
                {
                    // Location mode buttons (Premium look)
                    int btnAddX = menuX + 15;
                    int btnClearX = menuX + 130;
                    
                    // Add Button
                    g.setColor(6702080); g.fillRect(btnAddX, cY, 100, 22);
                    g.setColor(4025134); g.fillRect(btnAddX + 1, cY + 1, 98, 20); // Green
                    mFont.tahoma_7b_white.drawString(g, "+ Thêm Quái Trỏ", btnAddX + 50, cY + 5, mFont.CENTER);

                    // Clear Button
                    g.setColor(6702080); g.fillRect(btnClearX, cY, 100, 22);
                    g.setColor(14828336); g.fillRect(btnClearX + 1, cY + 1, 98, 20); // Red
                    mFont.tahoma_7b_white.drawString(g, "- Xóa Tất Cả", btnClearX + 50, cY + 5, mFont.CENTER);

                    cY += 30;
                    string mobNames = "";
                    if (AutoTrain.listMobIds.Count > 0)
                    {
                        List<string> names = new List<string>();
                        for (int k = 0; k < AutoTrain.listMobIds.Count; k++)
                        {
                            int id = AutoTrain.listMobIds[k];
                            string n = "#" + id;
                            for (int j = 0; j < GameScr.vMob.size(); j++)
                            {
                                Mob mob = (Mob)GameScr.vMob.elementAt(j);
                                if (mob.mobId == id)
                                {
                                    n = mob.getTemplate().name + " (HP:" + mob.hp + ") [#" + id + "]";
                                    break;
                                }
                            }
                            names.Add(n);
                        }
                        mobNames = string.Join(", ", names.ToArray());
                    }
                    else
                    {
                        mobNames = "Chưa có";
                    }

                    string fullText = "Mục tiêu (" + AutoTrain.listMobIds.Count + "): " + mobNames;
                    string[] lines = mFont.tahoma_7b_dark.splitFontArray(fullText, menuW - 30);
                    for (int k = 0; k < lines.Length; k++)
                    {
                        if (cY > menuY + menuH - 15) {
                            mFont.tahoma_7b_dark.drawString(g, "...", menuX + 15, cY, mFont.LEFT);
                            break; 
                        }
                        mFont.tahoma_7b_dark.drawString(g, lines[k], menuX + 15, cY, mFont.LEFT);
                        cY += 12;
                    }
                }
            }
            else if (currentTab == 1)
            {
                paintCheckbox(g, menuX + 15, cY, "Tự Động Nhặt", AutoPick.isAutoPick);
            }
        }

        private static void paintCheckbox(mGraphics g, int x, int y, string text, bool isChecked)
        {
            // Border
            g.setColor(6702080); 
            g.drawRect(x, y, 14, 14);

            if (isChecked)
            {
                // Checked state: Green background
                g.setColor(3048995); // Dark Green
                g.fillRect(x + 1, y + 1, 13, 13);

                // White Checkmark (✓)
                g.setColor(16777215);
                g.drawLine(x + 3, y + 7, x + 6, y + 10);
                g.drawLine(x + 3, y + 8, x + 6, y + 11);
                
                g.drawLine(x + 6, y + 10, x + 11, y + 3);
                g.drawLine(x + 6, y + 11, x + 11, y + 4);
            }
            else
            {
                // Unchecked state: White background
                g.setColor(16777215); 
                g.fillRect(x + 1, y + 1, 13, 13);
            }

            // Text
            mFont.tahoma_7b_dark.drawString(g, text, x + 20, y + 1, mFont.LEFT);
        }

        public static void Update()
        {
            if (!isShowMenu) return;

            if (GameCanvas.isPointerClick)
            {
                if (!GameCanvas.isPointer(menuX, menuY, menuW, menuH))
                {
                    isShowMenu = false;
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }

            if (GameCanvas.isPointer(menuX, menuY, menuW, menuH))
            {
                if (GameCanvas.isPointerClick)
                {
                    // Check Close button
                    if (GameCanvas.isPointer(menuX + menuW - 22, menuY + 8, 16, 16))
                    {
                        isShowMenu = false;
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }

                    // Check Tabs
                    int tabAreaX = menuX + 6;
                    int tabAreaY = menuY + 30;
                    int tabW = (menuW - 12) / tabNames.Length;
                    int tabH = 22;

                    for (int i = 0; i < tabNames.Length; i++)
                    {
                        if (GameCanvas.isPointer(tabAreaX + i * tabW, tabAreaY, tabW, tabH))
                        {
                            currentTab = i;
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                    }

                    // Check Content
                    int cY = tabAreaY + tabH + 10;
                    if (currentTab == 0)
                    {
                        // Left Column
                        if (GameCanvas.isPointer(menuX + 15, cY, 110, 20))
                        {
                            AutoTrain.isAutoTrain = !AutoTrain.isAutoTrain;
                            if (AutoTrain.isAutoTrain) {
                                if (trainMode == 0) RefreshAutoTrainMobs();
                                if (AutoTrain.listMobIds.Count == 0) {
                                    GameScr.info1.addInfo("Chưa chọn quái nào!", 0);
                                    AutoTrain.isAutoTrain = false;
                                } else {
                                    GameScr.isAutoPlay = true;
                                    GameScr.canAutoPlay = false;
                                }
                            } else {
                                GameScr.isAutoPlay = false;
                            }
                            GameScr.info1.addInfo("Auto Train: " + (AutoTrain.isAutoTrain ? "BẬT" : "TẮT"), 0);
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                        if (GameCanvas.isPointer(menuX + 15, cY + 25, 110, 20))
                        {
                            AutoTrain.isGoBack = !AutoTrain.isGoBack;
                            GameScr.info1.addInfo("Auto Goback: " + (AutoTrain.isGoBack ? "BẬT" : "TẮT"), 0);
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                        
                        // Right Column
                        if (GameCanvas.isPointer(menuX + 140, cY, 110, 20))
                        {
                            AutoSkill.isAutoSendAttack = !AutoSkill.isAutoSendAttack;
                            GameScr.info1.addInfo("Tự đánh: " + (AutoSkill.isAutoSendAttack ? "BẬT" : "TẮT"), 0);
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                        if (GameCanvas.isPointer(menuX + 140, cY + 25, 110, 20))
                        {
                            AutoTrain.isAvoidSuperMob = !AutoTrain.isAvoidSuperMob;
                            GameScr.info1.addInfo("Né Siêu Quái: " + (AutoTrain.isAvoidSuperMob ? "BẬT" : "TẮT"), 0);
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }

                        cY += 55;

                        // Mode Selection
                        if (GameCanvas.isPointer(menuX + 85, cY, 80, 20))
                        {
                            trainMode = 0;
                            RefreshAutoTrainMobs();
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                        if (GameCanvas.isPointer(menuX + 175, cY, 90, 20))
                        {
                            trainMode = 1;
                            AutoTrain.listMobIds.Clear();
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }

                        cY += 25;

                        if (trainMode == 0)
                        {
                            int mX = menuX + 15;
                            List<MobTemplate> uniqueMobs = GetUniqueMobs();
                            for (int i = 0; i < uniqueMobs.Count; i++)
                            {
                                if (mX > menuX + menuW - 130)
                                {
                                    mX = menuX + 15;
                                    cY += 25;
                                }
                                if (GameCanvas.isPointer(mX, cY, 125, 20))
                                {
                                    int tId = uniqueMobs[i].mobTemplateId;
                                    if (ignoredMobTemplates.Contains(tId))
                                    {
                                        ignoredMobTemplates.Remove(tId);
                                    }
                                    else
                                    {
                                        ignoredMobTemplates.Add(tId);
                                    }
                                    RefreshAutoTrainMobs();
                                    GameCanvas.clearAllPointerEvent();
                                    return;
                                }
                                mX += 130;
                            }
                        }
                        else
                        {
                            // Button Add
                            if (GameCanvas.isPointer(menuX + 15, cY, 100, 22))
                            {
                                if (global::Char.myCharz().mobFocus != null)
                                {
                                    AutoTrain.listMobIds.Add(global::Char.myCharz().mobFocus.mobId);
                                    GameScr.info1.addInfo("Đã thêm quái ID: " + global::Char.myCharz().mobFocus.mobId, 0);
                                }
                                else
                                {
                                    GameScr.info1.addInfo("Vui lòng trỏ vào 1 con quái trước!", 0);
                                }
                                GameCanvas.clearAllPointerEvent();
                                return;
                            }
                            // Button Clear
                            if (GameCanvas.isPointer(menuX + 130, cY, 100, 22))
                            {
                                AutoTrain.listMobIds.Clear();
                                GameScr.info1.addInfo("Đã xóa danh sách!", 0);
                                GameCanvas.clearAllPointerEvent();
                                return;
                            }
                        }
                    }
                    else if (currentTab == 1)
                    {
                        if (GameCanvas.isPointer(menuX + 15, cY, 110, 20))
                        {
                            AutoPick.isAutoPick = !AutoPick.isAutoPick;
                            GameScr.info1.addInfo("Tự nhặt: " + (AutoPick.isAutoPick ? "BẬT" : "TẮT"), 0);
                            GameCanvas.clearAllPointerEvent();
                            return;
                        }
                    }
                }
                
                // Consume events inside menu
                GameCanvas.clearAllPointerEvent();
            }
        }

        private static List<MobTemplate> GetUniqueMobs()
        {
            List<MobTemplate> list = new List<MobTemplate>();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                if (!mob.isMobMe)
                {
                    bool flag = false;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j].mobTemplateId == mob.templateId)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag) list.Add(mob.getTemplate());
                }
            }
            return list;
        }

        public static void RefreshAutoTrainMobs()
        {
            AutoTrain.listMobIds.Clear();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                if (!mob.isMobMe && !ignoredMobTemplates.Contains(mob.templateId))
                {
                    AutoTrain.listMobIds.Add(mob.mobId);
                }
            }
        }
    }
}
