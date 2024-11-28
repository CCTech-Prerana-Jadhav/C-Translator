using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    internal class Triangle
    {
        private Point p1;
        private Point p2;
        private Point p3;
        private Point normal;

        public Triangle(Point normal, Point p1, Point p2, Point p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.normal = normal;
        }

        ~Triangle()
        {
           
        }

        public Point P1 => p1; 
        public Point P2 => p2; 
        public Point P3 => p3;
        public Point Normal => normal;
    }
}
