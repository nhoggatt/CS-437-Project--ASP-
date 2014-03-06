using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;
using ASP.Test;

namespace ASP
{
    namespace Report
    {
        public class ObstructionData
        {
            private Material[] materials;
            private double conductivity;
            private double pressure;
            private double temperature;
            private double radiationLevel;
            private double lightIntensity;
            private double pH;
            private int waypoint;

            private double length;
            private double width;
            private double height;

            public Material[] Materials
            {
                get { return materials; }
            }

            public double Length
            {
                get { return length; }
            }
            public double Width
            {
                get { return width; }
            }
            public double Height
            {
                get { return height; }
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
            public double PH
            {
                get { return pH; }
            }


            public ObstructionData(double pressure, double temperature, double radiationLevel,
                double lightIntensity,  double conductivity, double pH, Waypoint coords, Material[] materials,
                double length, double width, double height
                )
            {
                this.conductivity = conductivity;
                this.pressure = pressure;
                this.temperature = temperature;
                this.radiationLevel = radiationLevel;
                this.lightIntensity = lightIntensity;
                this.waypoint = coords.getId();
                this.materials = materials;
                this.pH = pH;
                this.length = length;
                this.width = width;
                this.height = height;
            }


            public ObstructionData(Waypoint coords)
            {
                this.conductivity = DataGenerator.Sensors.getConductivity();
                this.pressure = DataGenerator.Sensors.getPressure();
                this.temperature = DataGenerator.Sensors.getTemp();
                this.radiationLevel = DataGenerator.Sensors.getRadiation();
                this.lightIntensity = DataGenerator.Sensors.getLight();
                this.waypoint = coords.getId();
                this.materials = DataGenerator.Sensors.getMaterials();
                this.pH = DataGenerator.Sensors.getpH();
                double[] temp = DataGenerator.Sensors.obstructionDimensions();
                this.length = temp[0];
                this.width = temp[1];
                this.height = temp[2];
            }


        }

    }
}