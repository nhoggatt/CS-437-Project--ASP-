using System;
using ASP.General;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.Test;


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
            private double ph;

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

                get { return temperature; }
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

            public double pH
            {
                get { return ph; }
            }
            public LocationTime TimeLocation
            {
                get { return timeLocation; }
            }


            public EnvironmentalData(double pressure, double temperature, double radiationLevel,
                double lightIntensity,  double conductivity, double pH, Waypoint coords, Material[] materials)
            {
                this.conductivity = conductivity;
                this.pressure = pressure;
                this.temperature = temperature;
                this.radiationLevel = radiationLevel;
                this.lightIntensity = lightIntensity;
                this.waypoint = coords.getId();
                this.timeLocation = new LocationTime(coords.getVector3());
                this.materials = materials;
                this.ph = pH;
            }
            public EnvironmentalData(Waypoint coords)
            {
                this.conductivity = DataGenerator.Sensors.getConductivity();
                this.pressure = DataGenerator.Sensors.getPressure();
                this.temperature = DataGenerator.Sensors.getTemp();
                this.radiationLevel = DataGenerator.Sensors.getRadiation();
                this.lightIntensity = DataGenerator.Sensors.getLight();
                this.waypoint = coords.getId();
                this.timeLocation = new LocationTime(coords.getVector3());
                this.materials = DataGenerator.Sensors.getMaterials();
                this.ph = DataGenerator.Sensors.getpH();
            }

            public EnvironmentalData(Waypoint coords, Material contaminant)
            {
                this.conductivity = DataGenerator.Sensors.getConductivity();
                this.pressure = DataGenerator.Sensors.getPressure();
                this.temperature = DataGenerator.Sensors.getTemp();
                this.radiationLevel = DataGenerator.Sensors.getRadiation();
                this.lightIntensity = DataGenerator.Sensors.getLight();
                this.waypoint = coords.getId();
                this.timeLocation = new LocationTime(coords.getVector3());

                this.materials = DataGenerator.Sensors.getMaterials(contaminant);
                this.ph = DataGenerator.Sensors.getpH();
            }

        }

    }
}