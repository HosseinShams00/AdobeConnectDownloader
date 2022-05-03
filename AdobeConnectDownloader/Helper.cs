using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdobeConnectDownloader.Helper
{
    public class Time
    {
        public static string ConvertUintToDuration(uint milisecond)
        {
            int MiliSecond = (int)(milisecond % 1000);
            int second = (int)((milisecond / 1000) % 60);
            int min = (int)((milisecond / 1000) / 60);
            int houre = (int)(min / 60);
            min = min >= 60 ? min % 60 : min;
            string result = "";

            result += $"{((houre < 10) ? ("0" + houre) : houre)}:";
            result += $"{((min < 10) ? ("0" + min) : min)}:";
            result += $"{((second < 10) ? ("0" + second) : second)}.";

            for (int i = 0; i < 3 - MiliSecond.ToString().Length; i++)
                result += "0";
            result += MiliSecond;

            return result;
        }

        public static uint ConvertTimeToMilisecond(string time)
        {
            string[] data = time.Split(':');
            uint hour = uint.Parse(data[0]);
            uint min = uint.Parse(data[1]);
            uint second = uint.Parse(data[2].Split('.')[0]);
            uint miliSecond = uint.Parse(data[2].Split('.')[1]);

            uint result = ((hour * 3600) + (min * 60) + second) * 1000 + miliSecond;
            return result;
        }
    }


    public class Design
    {
        public static Region GetRoundRegion(bool upLeft, bool upRight, bool lowRight, bool lowLeft, Size size, float xradius, float yradius)
        {
            return new Region(RectClass.MakeRoundedRectGraphicPath(new RectangleF(0, 0, size.Width, size.Height), xradius, yradius, upLeft, upRight, lowRight, lowLeft));
        }

        private class RectClass
        {
            // Draw a rectangle in the indicated Rectangle
            // rounding the indicated corners.
            public static GraphicsPath MakeRoundedRectGraphicPath(
                RectangleF rect, float xradius, float yradius,
                bool round_ul, bool round_ur, bool round_lr, bool round_ll)
            {
                // Make a GraphicsPath to draw the rectangle.
                PointF point1, point2;
                GraphicsPath path = new GraphicsPath();

                // Upper left corner.
                if (round_ul)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 180, 90);
                    point1 = new PointF(rect.X + xradius, rect.Y);
                }
                else point1 = new PointF(rect.X, rect.Y);

                // Top side.
                if (round_ur)
                    point2 = new PointF(rect.Right - xradius, rect.Y);
                else
                    point2 = new PointF(rect.Right, rect.Y);
                path.AddLine(point1, point2);

                // Upper right corner.
                if (round_ur)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 270, 90);
                    point1 = new PointF(rect.Right, rect.Y + yradius);
                }
                else point1 = new PointF(rect.Right, rect.Y);

                // Right side.
                if (round_lr)
                    point2 = new PointF(rect.Right, rect.Bottom - yradius);
                else
                    point2 = new PointF(rect.Right, rect.Bottom);
                path.AddLine(point1, point2);

                // Lower right corner.
                if (round_lr)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius,
                        rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 0, 90);
                    point1 = new PointF(rect.Right - xradius, rect.Bottom);
                }
                else point1 = new PointF(rect.Right, rect.Bottom);

                // Bottom side.
                if (round_ll)
                    point2 = new PointF(rect.X + xradius, rect.Bottom);
                else
                    point2 = new PointF(rect.X, rect.Bottom);
                path.AddLine(point1, point2);

                // Lower left corner.
                if (round_ll)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 90, 90);
                    point1 = new PointF(rect.X, rect.Bottom - yradius);
                }
                else point1 = new PointF(rect.X, rect.Bottom);

                // Left side.
                if (round_ul)
                    point2 = new PointF(rect.X, rect.Y + yradius);
                else
                    point2 = new PointF(rect.X, rect.Y);
                path.AddLine(point1, point2);

                // Join with the start point.
                path.CloseFigure();

                return path;
            }

        }
    }

}
