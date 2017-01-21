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
        static int oX = 0;
        static int oY = 140;
        public static Point GetTilePoint(int x, int y)
        {
            return new Point(oX + (x * offsetX) + (y * (offsetY+10)), oY + (y * ((10)-offsetY)) + (x * (offsetX)));
        }
        public static Point GetTilePointSelection(int x, int y)
        {
            return new Point(oX + (x * offsetX) + (y * (offsetY + 10)), oY + (48) + (y * ((10) - offsetY)) + (x * (offsetX)));
        }


    }
}
