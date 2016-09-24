using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace SteamNotificationsTray
{
    static class IconUtils
    {
        public static Icon CreateIconWithBackground(Icon origIcon, Color background, Size size)
        {
            using (Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(background);
                    int iconXPos = (bmp.Width - origIcon.Width) / 2;
                    int iconYPos = (bmp.Height - origIcon.Height) / 2;
                    g.DrawIcon(origIcon, iconXPos, iconYPos);
                    g.Flush();
                }

                IntPtr iconHandle = bmp.GetHicon();
                Icon icon = Icon.FromHandle(iconHandle);
                return icon;
            }
        }

        public static Icon CreateIconWithText(string text, Font font, Color textColor, Color background, Size size)
        {
            using (Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(background);
                    SizeF textSize = g.MeasureString(text, font);
                    int textXPos = (bmp.Width - (int)Math.Ceiling(textSize.Width)) / 2;
                    int textYPos = (bmp.Height - (int)Math.Ceiling(textSize.Height)) / 2;
                    g.DrawString(text, font, new SolidBrush(textColor), new PointF(textXPos, textYPos));
                    g.Flush();
                }

                IntPtr iconHandle = bmp.GetHicon();
                Icon icon = Icon.FromHandle(iconHandle);
                return icon;
            }
        }
    }
}
