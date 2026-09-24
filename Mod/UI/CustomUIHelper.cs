using System;
using UnityEngine;

namespace Mod.UI
{
    public static class CustomUIHelper
    {
        public static void PaintCheckbox(mGraphics g, int x, int y, string text, bool isChecked)
        {
            // Always draw the base box (unbroken)
            g.setColor(6702080); // Brown border
            g.fillRect(x, y, 12, 12);
            g.setColor(16777215); // White inner
            g.fillRect(x + 1, y + 1, 10, 10);

            if (isChecked)
            {
                g.setColor(6702080);
                g.fillRect(x + 2, y + 5, 2, 3);
                g.fillRect(x + 3, y + 6, 2, 3);
                g.fillRect(x + 4, y + 7, 2, 3);
                g.fillRect(x + 5, y + 6, 2, 3);
                g.fillRect(x + 6, y + 5, 2, 3);
                g.fillRect(x + 7, y + 4, 2, 3);
                g.fillRect(x + 8, y + 3, 2, 3);
                g.fillRect(x + 9, y + 2, 2, 3);
            }

            mFont.tahoma_7b_dark.drawString(g, text, x + 20, y, mFont.LEFT);
        }
    }
}
