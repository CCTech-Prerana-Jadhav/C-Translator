using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    internal class Point
    {
        private int mX;
        private int mY;
        private int mZ;

        public Point(int x, int y, int z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }

        ~Point()
        {
            
        }

        public int X => mX; 
        public int Y => mY; 
        public int Z => mZ; 
    }
}
