using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP
{
    public class CurrentLocationTime
    {
        Vector3 location;
        

        public CurrentLocationTime(double x, double y, double z)
        {
            Console.WriteLine("TEST Current location x: " + x + " y: " + y + " z: " + z);
            location = new Vector3(x,y,z);
        }

        public DateTime Time
        {
            get { return DateTime.Now; }
        }
        public LocationTime TimeLocation{
            get { return new LocationTime(location); }
    }
        public void UpdatePosition(double x, double y, double z)
        {
            Console.WriteLine("TEST Current location x: " + x + " y: " + y + " z: " + z);
            location.X += x;
            location.Y += y;
            location.Z += z;
        }


    }
}
