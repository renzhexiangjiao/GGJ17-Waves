using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PGJ002
{
    public class Tiles
    {
        static int offsetX = 60;
        static int offsetY = 60;
        static int oX = 60;
        static int oY = 240;
        /// <summary>
        /// Determines if the given point is inside the polygon
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="testPoint">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IsPointInPolygon4(Point[] polygon, Point testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        public static bool InsidePolygon(Point[] polygon, Point p)
        {
            int i;
            double angle = 0;
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);

            for (i = 0; i < polygon.Length; i++)
            {
                p1.X = polygon[i].X - p.X;
                p1.Y = polygon[i].Y - p.Y;
                p2.X = polygon[(i + 1) % polygon.Length].X - p.X;
                p2.Y = polygon[(i + 1) % polygon.Length].Y - p.Y;
                angle += Angle2D(p1.X, p1.Y, p2.X, p2.Y);
            }

            if (Math.Abs(angle) < 3.14)
                return false;
            else
                return true;
        }

        /*
           Return the angle between two vectors on a plane
           The angle is from vector 1 to vector 2, positive anticlockwise
           The result is between -pi -> pi
        */
        public static double Angle2D(double x1, double y1, double x2, double y2)
        {
            double dtheta, theta1, theta2;

            theta1 = Math.Atan2(y1, x1);
            theta2 = Math.Atan2(y2, x2);
            dtheta = theta2 - theta1;
            while (dtheta > 3.14)
                dtheta -= 2*3.14;
            while (dtheta < -3.14)
                dtheta += 2*3.14;

            return (dtheta);
        }
        public static Point GetTilePoint(int x, int y)
        {
            return new Point((x > 1 ? 16 : 0) + (y > 2 ? 16 : 0) + oX + (x * offsetX) + (y * (offsetY+10)), (x > 1 ? 16 : 0) + (y > 2 ? -16 : 0) + oY + (y * ((10)-offsetY)) + (x * (offsetX)));
        }
        public static Point GetTilePointForUpgrade(int x, int y, int level)
        {
            return new Point((x > 1 ? 16 : 0) + (y > 2 ? 16 : 0) + oX + (x * offsetX) + (y * (offsetY + 10)), (level*-8) + (x > 1 ? 16 : 0) + (y > 2 ? -16 : 0) + oY + (y * ((10) - offsetY)) + (x * (offsetX)));
        }
        public static Point GetTilePointSelection(int x, int y)
        {
            return new Point((x > 1 ? 16 : 0) + (y > 2 ? 16 : 0) + oX + (x * offsetX) + (y * (offsetY + 10)), (x > 1 ? 16 : 0) + (y > 2 ? -16 : 0) + oY + 48 + (y * ((10) - offsetY)) + (x * (offsetX)));
        }
        public static Point[] GetTilePolygon(int x, int y)
        {
            return new Point[] { Tiles.GetTilePointSelection(x+0, y+0), Tiles.GetTilePointSelection(x+1, y+0), Tiles.GetTilePointSelection(x+1, y+1), Tiles.GetTilePointSelection(x+0, y+1) };
        }


    }
}
