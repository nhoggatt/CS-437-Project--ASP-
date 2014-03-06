using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP
{
    namespace General
    {
        public class Vector3
        {

            double x, y, z;

            public double X
            {
                get { return x; }
                set { x = value; }
            }

            public double Y
            {
                get { return y; }
                set { y = value; }
            }
            public double Z
            {
                get { return z; }
                set { z = value; }
            }

            public Vector3(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public double Magnitude(Vector3 other)
            {
                return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2) + Math.Pow(z - other.z, 2));
            }

            public override string ToString()
            {
                return "x: "+x+" y:"+y+" z: "+z;
            }

        }
    }
}
