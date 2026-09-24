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
            g.setColor(14338484); // Tan inner
            g.fillRect(x + 1, y + 1, 10, 10);

            if (isChecked)
            {
                // Draw a simple, thick checkmark completely inside the box
                g.setColor(6702080); // Same color as border
                for (int i = 0; i < 3; i++)
                {
                    g.drawLine(x + 2, y + 5 + i, x + 5, y + 8 + i);
                    g.drawLine(x + 5, y + 8 + i, x + 10, y + 3 + i);
                }
            }

            mFont.tahoma_7b_dark.drawString(g, text, x + 20, y, mFont.LEFT);
        }
    }
}
