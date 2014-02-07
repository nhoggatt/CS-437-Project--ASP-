using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class EnvironmentalData
    {
       
        Material[] materials;
        double pressure;
        double temperature;
        double radiationLevel;
        double lightIntensity;
       
        int waypoint;
        double[] coords;

    //envdata create waypoint x y z pressure temp radiation light
    //envdata add <material info>
    //envdata add <material 2 info>
    //envdata complete <final info>
    
     public EnvironmentalData(double pressure, double temperature, double radiationLevel, double lightIntensity, int waypoint, double[] coords, Material[] materials)
        {
         this.pressure = pressure;
         this.temperature = temperature;
         this.radiationLevel = radiationLevel;
         this.lightIntensity = lightIntensity;
         this.waypoint = waypoint;
         this.coords = coords;
         this.materials = materials;
        }  

    }

