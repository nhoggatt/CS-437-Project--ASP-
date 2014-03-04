using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    namespace Report
    {
        public class Material
        {

            int material_id;
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

            public int Id
            {
                get { return material_id; }
            }
            public bool Contaminant
            {
                get { return contaminant; }
            }
            public int ThreatLevel
            {
                get { return threatLevel; }
            }
            public double Concentration
            {
                get { return concentration; }
            }

        }
    }
}

