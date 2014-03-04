using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP
{
    namespace Report
    {
        public class MaintenanceReport : AbstractReport
        {

            DiagnosticData data;

            public MaintenanceReport()
            {

                data = new DiagnosticData();
                //Calls diagnostic function to check on modules/components. If a module returns as broken, will update status in SC. If a module is already not functioning, it will not bother to check up on the module. 
                //NOTE: the user should be able to set the status of a module/hardware component, and that should be the only way to change a part's status after it has been confirmed broken.

            }


            /*
             * 3) [Short Report] send locational data + time to IC to transmit 
             *      LocationTime
             *     OR
             *    [Long Report]
             *      i) Recieve information from environment module              
             *      ii) create a report using the information gathered
             *      iii) geographical data + time to IC to transmit  
             *      iv) send to IC for transmission
             *      
             *     environmentaldata (each report )
             *      -waypoint int
             *      -pressure double 
             *      -temp double
             *      -radiation double
             *      -light double
             *      -Material
             *      +int id
             *      +bool contaminant
             *      +int threatLevel
             *      +double concentration
             *     Waypoint position
             *     LocationTime
             *          
             *     [Maintenance Report] 
             *     i) Compile status of modules (if functioning)
             *     ii) Power levels
             *     iii) Send to IC for transmission 
             *     
             *     [Long Report]
             *     double[n] power levels
             *     bool[n] functioning
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
}