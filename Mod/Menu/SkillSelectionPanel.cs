using System;
using System.Collections.Generic;
using UnityEngine;
using Mod.DungPham.KoiOctiiu957;

namespace Mod.Menu
{
    public class SkillSelectionPanel
    {
        private static SkillSelectionPanel instance;
        public bool isShowing;
        
        public int menuX;
        public int menuY;
        public int menuW;
        public int menuH;
        
        public int yOffset = 0;
        public int maxScroll = 0;
        private int lastPy = -1;

        public static SkillSelectionPanel gI()
        {
            if (instance == null)
            {
                instance = new SkillSelectionPanel();
            }
            return instance;
        }

        public void Show()
        {
            menuW = 150;
            menuH = 200;
            menuX = (GameCanvas.w - menuW) / 2;
            menuY = (GameCanvas.h - menuH) / 2;
            yOffset = 0;
            lastPy = -1;
            isShowing = true;
            GameCanvas.clearAllPointerEvent();
        }

        public void Hide()
        {
            isShowing = false;
            GameCanvas.clearAllPointerEvent();
        }

        public void Paint(mGraphics g)
        {
            if (!isShowing) return;

            g.translate(-g.getTranslateX(), -g.getTranslateY());
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);

            // Shadow
            g.setColor(0, 0.5f);
            g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);

            // Native Frame Background
            GameCanvas.paintz.paintFrameSimple(menuX, menuY, menuW, menuH, g);

            // Header Background (blue)
            g.setColor(4688365); 
            g.fillRect(menuX + 1, menuY + 1, menuW - 2, 24);

            // Header Separator
            g.setColor(6702080);
            g.fillRect(menuX + 1, menuY + 25, menuW - 2, 1);

            // Title
            mFont.tahoma_7b_white.drawString(g, "Cài đặt kỹ năng", menuX + menuW / 2, menuY + 7, mFont.CENTER);

            // Set clip for scrolling area
            g.setClip(menuX + 1, menuY + 26, menuW - 2, menuH - 27);
            g.translate(0, -yOffset);

            int cY = menuY + 35;
            int mX = menuX + 15;

            if (global::Char.myCharz() != null && global::Char.myCharz().vSkillFight != null)
            {
                for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
                {
                    Skill s = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
                    if (s != null && s.template != null)
                    {
                        bool isChecked = AutoTrain.selectedAutoTrainSkills.Contains((int)s.template.id);
                        paintRadioCheckbox(g, mX, cY, s.template.name, isChecked);
                        cY += 25;
                    }
                }
                
                int contentHeight = cY - (menuY + 35);
                maxScroll = contentHeight - (menuH - 35) + 10;
                if (maxScroll < 0) maxScroll = 0;
            }

            // Restore clip and translate
            g.translate(0, yOffset);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        }

        private void paintRadioCheckbox(mGraphics g, int x, int y, string text, bool isChecked)
        {
            // Draw a custom rounded-like radio button using native colors
            g.setColor(6702080); // Brown border
            g.fillRect(x, y, 12, 12);
            g.setColor(14338484); // Tan inner (same as frame)
            g.fillRect(x + 1, y + 1, 10, 10);

            if (isChecked)
            {
                // Draw a nice filled dot
                g.setColor(4688365); // Blue dot (matches header)
                g.fillRect(x + 3, y + 3, 6, 6);
            }

            mFont.tahoma_7b_dark.drawString(g, text, x + 18, y, mFont.LEFT);
        }

        public void Update()
        {
            if (!isShowing) return;

            // Scroll handling
            if (GameCanvas.isPointerHoldIn(menuX, menuY + 25, menuW, menuH - 25))
            {
                if (GameCanvas.isPointerMove)
                {
                    if (lastPy != -1)
                    {
                        yOffset += lastPy - GameCanvas.py;
                        if (yOffset < 0) yOffset = 0;
                        if (yOffset > maxScroll) yOffset = maxScroll;
                    }
                    lastPy = GameCanvas.py;
                }
                else if (GameCanvas.isPointerDown)
                {
                    lastPy = GameCanvas.py;
                }
            }
            else
            {
                lastPy = -1;
            }

            if (!GameCanvas.isPointerDown && !GameCanvas.isPointerMove)
            {
                lastPy = -1;
            }

            // Click outside to close (only if we didn't drag)
            if (GameCanvas.isPointerClick)
            {
                if (!GameCanvas.isPointer(menuX, menuY, menuW, menuH))
                {
                    Hide();
                    return;
                }
            }

            int cY = menuY + 35 - yOffset;
            int mX = menuX + 15;

            if (global::Char.myCharz() != null && global::Char.myCharz().vSkillFight != null)
            {
                for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
                {
                    Skill s = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
                    if (s != null && s.template != null)
                    {
                        if (cY >= menuY + 25 && cY <= menuY + menuH - 10)
                        {
                            if (GameCanvas.isPointerClick && GameCanvas.isPointer(mX, cY, menuW - 20, 20))
                            {
                                int sId = (int)s.template.id;
                                if (AutoTrain.selectedAutoTrainSkills.Contains(sId))
                                {
                                    AutoTrain.selectedAutoTrainSkills.Remove(sId);
                                }
                                else
                                {
                                    AutoTrain.selectedAutoTrainSkills.Add(sId);
                                }
                                AutoTrain.SaveAutoTrainSkills();
                                GameCanvas.clearAllPointerEvent();
                                return;
                            }
                        }
                        cY += 25;
                    }
                }
            }
        }
    }
}
