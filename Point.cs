using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KrivuljaOdmeta
{
    public class Point
    {
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }

        public Point(double x1, double x2, double y1)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
        }

        public static Point Calculate(double t)
        {
            double r1 = 0.515; //[m] 
            double r2 = 0.795; //[m]

            double n = 18.5; //[1/min]

            double gama1 = (r1 * Math.Pow(Math.PI, 2) * Math.Pow(n, 2)) / (Math.Pow(30, 2) * 9.81);
            double gama2 = (r2 * Math.Pow(Math.PI, 2) * Math.Pow(n, 2)) / (Math.Pow(30, 2) * 9.81);

            double v1 = (r1 * Math.PI * n) / (30); //[m/s]
            double v2 = (r2 * Math.PI * n) / (30); //[m/s]

            double x1 = t * v1 * Math.Sin(gama1); //[m]
            double x2 = t * v2 * Math.Sin(gama2); //[m]

            double y1 = -0.5 * 9.81 * Math.Pow(t, 2); //[m]
            return new Point(x1, x2, y1);
        }
    }
}
