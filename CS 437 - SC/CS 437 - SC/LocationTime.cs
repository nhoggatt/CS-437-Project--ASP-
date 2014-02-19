using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP
{
    namespace General
    {
        public class LocationTime
        {

            DateTime dateTime;
            Vector3 location;

            public DateTime DateTime
            {
                get { return dateTime; }
            }
            public Vector3 Location
            {
                get { return location; }
                set { location = value; }
            }
            

            public LocationTime(Vector3 location)
            {
                dateTime = DateTime.Today;
                this.location=location;
            }


            /*
             * 3) [Short Report] send locational data + time to IC to transmit 
             *      float[3] 
             *      long
             *     OR
             *    [Long Report]
             *      i) Recieve information from environment module              
             *      ii) create a report using the information gathered
             *      iii) geographical data + time to IC to transmit  
             *      iv) send to IC for transmission
             *      
             *     environmentaldata (each report )
             *      -waypoint int
             *      -x double
             *      -y double
             *      -z double
             *      -pressure double 
             *      -temp double
             *      -radiation double
             *      -light double
             *      -Material
             *      +int id
             *      +bool contaminant
             *      +int threatLevel
             *      +double concentration
             *     double[3]
             *     long
             *          
             *     [Maintenance Report] 
             *     i) Compile status of modules (if functioning)
             *     ii) Power levels
             *     iii) [Long Report]
             *     iv) Send to IC for transmission 
             *     
             *     [Long Report]
             *     double[?]
             *     bool[] functioning
             *      broken hardware or non functioning modules
             *     
             *     
             **/


        }

    }
}