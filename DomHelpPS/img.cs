using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomHelpPS
{
    class img
    {
        public int row { get; set; }
        public int col { get; set; }
        public Mat mat { get; set; }
        public string path { get; set; }
        public int xmin { get; set; }
        public int ymin { get; set; }
        public int xmax { get; set; }
        public int ymax { get; set; }

        public img(int row,int col ,Mat mat,string path,int xmax,int xmin,int ymin,int ymax) {
            this.row = row;
            this.col = col;
            this.mat = mat;
            this.path = path;
            this.xmax = xmax;
            this.xmin = xmin;
            this.ymin = ymin;
            this.ymax = ymax;
        }
    }
}
