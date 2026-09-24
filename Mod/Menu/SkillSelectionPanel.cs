using System;
using System.Collections.Generic;
using UnityEngine;
using Mod.DungPham.KoiOctiiu957;
using Mod.UI;

namespace Mod.Menu
{
    public class SkillSelectionPanel : BasePanel
    {
        private static SkillSelectionPanel instance;
        public bool isShowing;

        public static SkillSelectionPanel gI()
        {
            if (instance == null)
            {
                instance = new SkillSelectionPanel();
                instance.title = "Cài đặt kỹ năng";
            }
            return instance;
        }

        protected override bool GetIsShowing() { return isShowing; }
        protected override void SetIsShowing(bool value) { isShowing = value; }

        public override void Show() { base.Show(); }
        public override void Hide() { base.Hide(); }
        public override void Paint(mGraphics g) { base.Paint(g); }
        public override void Update() { base.Update(); }

        protected override void PaintContent(mGraphics g)
        {
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
                        CustomUIHelper.PaintCheckbox(g, mX, cY, s.template.name, isChecked);
                        cY += 25;
                    }
                }
                
                int contentHeight = cY - (menuY + 35);
                maxScroll = contentHeight - (menuH - 35) + 10;
                if (maxScroll < 0) maxScroll = 0;
            }
        }

        protected override void UpdateContent()
        {
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
