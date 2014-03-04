using System;
using ASP.General;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    namespace Report
    {
        public class EnvironmentalData
        {

            private Material[] materials;
            private double conductivity;
            private double pressure;
            private double temperature;
            private double radiationLevel;
            private double lightIntensity;
            private double pH;

            private int waypoint;
            private LocationTime timeLocation;


            public Material[] Materials
            {
                get { return materials; }
            }
            public double Conductivity
            {
                get { return conductivity; }
            }
            public double Pressure
            {
                get { return pressure; }
            }
            public double Temperature
            {
                get { return Temperature; }
            }
            public double RadiationLevel
            {
                get { return radiationLevel; }
            }
            public double LightIntensity
            {
                get { return lightIntensity; }
            }
            public int Waypoint
            {
                get { return waypoint; }
            }
            public LocationTime TimeLocation
            {
                get { return TimeLocation; }
            }


            public EnvironmentalData(double pressure, double temperature, double radiationLevel,
                double lightIntensity, int waypoint, double conductivity, double pH, Waypoint coords, Material[] materials)
            {
                this.conductivity = conductivity;
                this.pressure = pressure;
                this.temperature = temperature;
                this.radiationLevel = radiationLevel;
                this.lightIntensity = lightIntensity;
                this.waypoint = waypoint;
                this.timeLocation = new LocationTime(coords.getVector3());
                this.materials = materials;
                this.pH = pH;
            }

        }

    }
}