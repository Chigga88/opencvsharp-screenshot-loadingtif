using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomHelpPS
{
    class imginfo
    {
        public string fname { set; get; }
        public int X { set; get; }
        public int Y { set; get; }
        public int width { set; get; }
        public int height { set; get; }
        public int col { set; get; }
        public int row { set; get; }
        public string imgname { set; get; }
        public imginfo(string fname,int X,int Y,int width,int height, int col, int row,string imgname) {
            this.fname = fname;
            this.X = X;
            this.Y = Y;
            this.width = width;
            this.height = height;
            this.col = col;
            this.row = row;
            this.imgname = imgname;
        }
    }
}
