using System;
using ASP.General;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP
{
    class LocationGenerator
    {
        Random rand = new Random();
        double x, y, z;
        public LocationGenerator()
        {
            this.x = rand.NextDouble() * 100;
            this.y = rand.NextDouble() * 100;
            this.z = rand.NextDouble() * 10;
        }

        public Vector3 Location()
        {
            return new Vector3(x,y,z);
        }
    }
}
