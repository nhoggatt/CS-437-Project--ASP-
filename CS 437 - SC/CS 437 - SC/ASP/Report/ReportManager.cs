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


        private static EnvironmentalReport currentEnvironmentalReport;

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

        public void AddToEnvironmentalReportData(EnvironmentalData data)
        {
            if (currentEnvironmentalReport == null)
                CreateEnvironmentalDataReport(data);
            else
                currentEnvironmentalReport.Add(data);
        }

        private static string location(General.Vector3 vector)
        {
            return vector.X + " " + vector.Y + " " + vector.Z;
        }

        private static string status(AbstractReport type)
        {
            return "id " + type.Current.Waypoint_Id + "\n Location " + location(type.Current.Location)
                        + "\n Datetime" + type.Current.DateTime;
        }
        private static void SendReport(int type)
        {
            //send to Yash
            Console.WriteLine();

            switch (type)
            {
                case 0: //status
                    StatusReport temp = new StatusReport();
                    Console.WriteLine("Status Report \n "+status(temp));
                    break;

                case 1: //environmental

                    Console.WriteLine("Environmental Report");

                    if (currentEnvironmentalReport.Data.Count == 0)
                        Console.WriteLine(" Empty report");

                    foreach (EnvironmentalData data in currentEnvironmentalReport.Data)
                    {
                        Console.WriteLine(" Environmental Data");
                        Console.WriteLine("\n  Conductivity: " + data.Conductivity + "\n  Pressure: " + data.Pressure + "\n  Temperature: " + data.Temperature +
                            "\n  Radiation Level: " + data.RadiationLevel + "\n  Light Intensity: " + data.LightIntensity + "\n  ph: " + data.pH +
                            "\n  Waypoint: " + data.Waypoint + "\n  Pressure: " +data.Pressure+ "\n  Datetime: " + data.TimeLocation);

                        foreach (Material mat in data.Materials)
                        {
                            Console.WriteLine("\n   Materials");
                             Console.WriteLine("\n    id: " + mat.Id + "\n    contaminant: " + mat.Contaminant + "\n    Threat Level: " + mat.ThreatLevel+
                                 "\n    Concentration:"+mat.Concentration);
                        }
                    }
                    Console.WriteLine(" "+status(currentEnvironmentalReport));
                    break;

                case 2://maintenance
                    MaintenanceReport temp2 = new MaintenanceReport();

                    Console.WriteLine("Maintenance Report");

                    Console.WriteLine(" Diagnostic Data");
                    Console.WriteLine("\n  Power Level: " + temp2.Data.Power);

                    foreach (Component comp in temp2.Data.Status)
                    {
                        Console.WriteLine("\n  Component ID: " + comp.Component_id);
                        Console.WriteLine("\n  Is functional: " + comp.Functional);
                    }

                    break;

                case 3: //Obstruction
                    Console.WriteLine("Obstruction Report");
                    ObstructionReport temp3 = new ObstructionReport(new ObstructionData(Driver.storedStandard.pathfinding.Goal));
                    ObstructionData tempData = temp3.Data;

                    Console.WriteLine(" Obstruction Data");
                    Console.WriteLine("\n  Conductivity: " + tempData.Conductivity + "\n  Pressure: " + tempData.Pressure + "\n  Temperature: " + tempData.Temperature +
                            "\n  Radiation Level: " + tempData.RadiationLevel + "\n  Light Intensity: " + tempData.LightIntensity + "\n  ph: " + tempData.PH+
                            "\n  Waypoint: " + tempData.Waypoint + "\n  Pressure: " + tempData.Pressure + "\n  Length: " + tempData.Length
                            + "\n  Width: " + tempData.Width + "\n  Height: " + tempData.Height);
                    break;
                default:
                    break;
            }
            Console.WriteLine();

        }
            
        


        private void prepareStatusReport()
        {
            SendReport(0);
        }

        private void prepareEnvironmentalReport()
        {
            if (currentEnvironmentalReport == null)
                currentEnvironmentalReport = new EnvironmentalReport();
            SendReport(1);
            currentEnvironmentalReport = null;
        }

        private void prepareMaintenanceReport()
        {
            SendReport(2);
        }
        private void prepareObstructionReport()
        {
            SendReport(3);
        }
    }
}
