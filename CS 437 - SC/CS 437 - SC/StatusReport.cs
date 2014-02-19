using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP {

    public class StatusReport
    {
        LocationTime current;

        public StatusReport()
        {
            current = new LocationTime(new LocationGenerator().Location());
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
         *     Each report type shall be seperate.
         *     
         *     It should be possible to store multiple reports. When it is time to send the reports
         *     to the user, they will all be offloaded. Critical reports will go through a seperate 
         *     process, and get sent as soon as possible. 
         **/


    }

    }