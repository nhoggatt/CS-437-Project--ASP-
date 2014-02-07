using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Material
    {
       
        int  material_id;
        bool contaminant;
        int threatLevel;
        double concentration;

       

     public Material(int id, bool contaminant, int threatLevel, double concentration)
        {
         this.material_id = id;
         this.contaminant = contaminant;
         this.threatLevel = threatLevel;
         this.concentration = concentration;
        }  

    }

