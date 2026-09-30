using System;
using System.Collections.Generic;
using Mod.UI;

namespace Mod.Menu
{
    // Panel xem danh sách ID vật phẩm dạng "stt. tên (id)", bấm nút X cuối dòng để xóa
    public class ItemListPanel : BasePanel
    {
        public delegate void OnListChanged();

        private static ItemListPanel instance;
        public bool isShowing;

        private List<int> items;
        private OnListChanged onChanged;

        // Nút X gốc của game (nút đóng Panel)
        private static Image imgX;
        private const string IMG_X_PATH = "/mainImage/myTexture2dbtX.png";

        public static ItemListPanel gI()
        {
            if (instance == null)
            {
                instance = new ItemListPanel();
            }
            return instance;
        }

        protected override bool GetIsShowing() { return isShowing; }
        protected override void SetIsShowing(bool value) { isShowing = value; }

        public void Show(string title, List<int> items, OnListChanged onChanged)
        {
            this.title = title;
            this.items = items;
            this.onChanged = onChanged;
            base.Show();
            // Rộng hơn panel kỹ năng để tên vật phẩm dài không bị tràn
            menuW = System.Math.Min(220, GameCanvas.w - 20);
            menuX = (GameCanvas.w - menuW) / 2;
        }

        private static string GetItemName(int id)
        {
            try
            {
                ItemTemplate template = ItemTemplates.get((short)id);
                if (template != null && template.name != null)
                {
                    return template.name;
                }
            }
            catch (Exception)
            {
            }
            return "?";
        }

        // Cắt bớt chữ cho vừa độ rộng, thêm "..." ở cuối
        private static string FitText(string s, int maxW)
        {
            if (mFont.tahoma_7b_dark.getWidth(s) <= maxW)
            {
                return s;
            }
            while (s.Length > 1 && mFont.tahoma_7b_dark.getWidth(s + "...") > maxW)
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s + "...";
        }

        private static Image GetImgX()
        {
            if (imgX == null)
            {
                imgX = (Panel.imgX != null) ? Panel.imgX : GameCanvas.loadImage(IMG_X_PATH);
            }
            return imgX;
        }

        private static int GetXSize()
        {
            Image img = GetImgX();
            return (img != null) ? mGraphics.getImageWidth(img) : 12;
        }

        private static int GetRowH()
        {
            Image img = GetImgX();
            int h = (img != null) ? mGraphics.getImageHeight(img) : 12;
            return System.Math.Max(20, h + 4);
        }

        private int GetXPosX()
        {
            return menuX + menuW - 8 - GetXSize();
        }

        // y góc trên của nút X, căn giữa theo dòng chữ (chữ cao ~10px)
        private static int GetXPosY(int textY)
        {
            Image img = GetImgX();
            int h = (img != null) ? mGraphics.getImageHeight(img) : 12;
            return textY + 5 - h / 2;
        }

        protected override void PaintContent(mGraphics g)
        {
            int cY = menuY + 35;
            int mX = menuX + 10;
            if (items == null || items.Count == 0)
            {
                mFont.tahoma_7b_dark.drawString(g, "Danh sách trống", menuX + menuW / 2, cY, mFont.CENTER);
                maxScroll = 0;
                return;
            }
            int xX = GetXPosX();
            int rowH = GetRowH();
            int textMaxW = xX - mX - 6;
            Image img = GetImgX();
            for (int i = 0; i < items.Count; i++)
            {
                int id = items[i];
                string text = (i + 1).ToString() + ". " + GetItemName(id) + " (" + id.ToString() + ")";
                mFont.tahoma_7b_dark.drawString(g, FitText(text, textMaxW), mX, cY, mFont.LEFT);
                if (img != null)
                {
                    g.drawImage(img, xX, GetXPosY(cY), 0);
                }
                else
                {
                    mFont.tahoma_7b_red.drawString(g, "X", xX + 6, cY, mFont.CENTER);
                }
                cY += rowH;
            }
            int contentHeight = cY - (menuY + 35);
            maxScroll = contentHeight - (menuH - 35) + 10;
            if (maxScroll < 0) maxScroll = 0;
        }

        protected override void UpdateContent()
        {
            if (items == null || !GameCanvas.isPointerClick)
            {
                return;
            }
            int cY = menuY + 35 - yOffset;
            int xX = GetXPosX();
            int xSize = GetXSize();
            int rowH = GetRowH();
            for (int i = 0; i < items.Count; i++)
            {
                if (cY >= menuY + 25 && cY <= menuY + menuH - 10)
                {
                    // Vùng bấm rộng hơn nút X một chút cho dễ trúng
                    if (GameCanvas.isPointer(xX - 4, GetXPosY(cY) - 4, xSize + 8, xSize + 8))
                    {
                        items.RemoveAt(i);
                        if (onChanged != null)
                        {
                            onChanged();
                        }
                        if (yOffset > 0)
                        {
                            yOffset = System.Math.Max(0, yOffset - rowH);
                        }
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }
                }
                cY += rowH;
            }
        }
    }
}
