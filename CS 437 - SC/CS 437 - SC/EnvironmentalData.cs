using System;
using ASP.General;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    public class EnvironmentalData
    {

        Material[] materials;
        double conductivity;
        double pressure;
        double temperature;
        double radiationLevel;
        double lightIntensity;

        int waypoint;
        LocationTime locationTime;

        //envdata create waypoint x y z pressure temp radiation light
        //envdata add <material info>
        //envdata add <material 2 info>
        //envdata complete <final info>

        public EnvironmentalData(double pressure, double temperature, double radiationLevel,
            double lightIntensity, int waypoint, double conductivity, Waypoint coords, Material[] materials)
        {
            this.conductivity = conductivity;
            this.pressure = pressure;
            this.temperature = temperature;
            this.radiationLevel = radiationLevel;
            this.lightIntensity = lightIntensity;
            this.waypoint = waypoint;
            this.locationTime = new LocationTime(coords.getVector3());
            this.materials = materials;
        }

    }

}