using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomHelpPS
{
    public class Block
    {
        public string Name;
        public string Path;

        public int Width;
        public int Height;
        public double CellSize;

        public double Xmin;
        public double Xmax;
        public double Ymin;
        public double Ymax;

        public int StartX;
        public int StartY;

        public int ExternalStartX;
        public int ExternalStartY;

        public int SubWidth;
        public int SubHeight;






        public Point GetPoint(double douWorldX, double douWorldY)
        {
            double douMapX = (douWorldX.RetentionDecimal(6) - this.Xmin) / this.CellSize;
            double douMapY = (this.Ymax - douWorldY.RetentionDecimal(5)) / this.CellSize;
            douMapX = douMapX.RetentionDecimal(6);
            douMapY = douMapY.RetentionDecimal(5);

            if (douMapX==0)
            {
                douMapX = 0.0000001;
            }
            if (douMapY == 0)
            {
                douMapY = 0.0000001;
            }
            int intMapX = (int)Math.Ceiling(douMapX) - 1;
            int intMapY = (int)Math.Ceiling(douMapY) - 1;
            return new Point(intMapX, intMapY);
        }
    }
}
