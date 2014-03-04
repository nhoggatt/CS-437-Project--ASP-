using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.SystemControl;

namespace ASP.Report
{
    public class ReportManager
    {

        public delegate void Reports();
        public Dictionary<string,Reports> reports = new Dictionary<string,Reports>();


        private EnvironmentalReport currentEnvironmentalReport;

        public ReportManager()
        {
            reports.Add("maintenance", prepareMaintenanceReport);
            reports.Add("status", prepareStatusReport);
            reports.Add("obstruction", prepareObstructionReport);
            reports.Add("environmental", prepareEnvironmentalReport);
        }

        public void CreateEnvironmentalDataReport(EnvironmentalData data)
        {
            if (currentEnvironmentalReport == null)
                currentEnvironmentalReport = new EnvironmentalReport(data);
            else
            {
                SendReport(1);
                currentEnvironmentalReport = new EnvironmentalReport(data);

            }
            Console.WriteLine("DRIVER:REPORTMANAGER New environmental report created.");
        }


        public void SendReport(int type)
        {
            //send to Yash

            AbstractReport temp;
            switch (type)
            {
                case 0: //status
                    temp = new StatusReport();
                    Console.WriteLine("Status Report \n id"+temp.Current.Waypoint_Id+"\nLocation"+temp.Current.Location+"\n Datetime"+temp.Current.DateTime);
                    break;
                case 1: //status
                    break;
                case 2://status
                    break;
                default:
                    break;
            }

        }
            
        


        private void prepareStatusReport()
        {
           StatusReport temp = new StatusReport();
        }

        private void prepareEnvironmentalReport()
        {

        }

        private void prepareMaintenanceReport()
        {

        }
        private void prepareObstructionReport()
        {

        }
    }
}
