using System;

namespace Mod.UI
{
    public abstract class BasePanel
    {
        public int menuX;
        public int menuY;
        public int menuW;
        public int menuH;
        
        public int yOffset = 0;
        public int maxScroll = 0;
        protected int lastPy = -1;
        public string title = "Menu";

        protected abstract bool GetIsShowing();
        protected abstract void SetIsShowing(bool value);

        public virtual void Show()
        {
            menuW = 150;
            menuH = 200;
            menuX = (GameCanvas.w - menuW) / 2;
            menuY = (GameCanvas.h - menuH) / 2;
            yOffset = 0;
            lastPy = -1;
            SetIsShowing(true);
            GameCanvas.clearAllPointerEvent();
        }

        public virtual void Hide()
        {
            SetIsShowing(false);
            GameCanvas.clearAllPointerEvent();
        }

        public virtual void Paint(mGraphics g)
        {
            if (!GetIsShowing()) return;

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
            mFont.tahoma_7b_white.drawString(g, title, menuX + menuW / 2, menuY + 7, mFont.CENTER);

            // Set clip for scrolling area
            g.setClip(menuX + 1, menuY + 26, menuW - 2, menuH - 27);
            g.translate(0, -yOffset);

            PaintContent(g);

            // Restore clip and translate
            g.translate(0, yOffset);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        }

        protected abstract void PaintContent(mGraphics g);

        public virtual void Update()
        {
            if (!GetIsShowing()) return;

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

            UpdateContent();
        }

        protected abstract void UpdateContent();
    }
}
