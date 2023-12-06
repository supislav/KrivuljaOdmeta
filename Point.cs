using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KrivuljaOdmeta
{
    internal class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point[] points { get; set; }

        private Point Calculate(double t)
        {
            double r1 = 0.515; //[m] 
            double n = 18.5; //[1/min]
            double v1 = (r1 * Math.PI * n) / (30 * 60); //[m/s]
            double x1 = t * v1;
            double y1 = -0.5 * 9.81 * Math.Pow(t, 2);
            return new Point(x1, y1);
        }


    }
}
