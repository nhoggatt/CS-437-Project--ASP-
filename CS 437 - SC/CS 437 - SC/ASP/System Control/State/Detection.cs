using System;
using ASP.Report;
using ASP.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP
{
    namespace SystemControl
    {
        namespace States
        {
            public class Detection : State
            {

                private Material currentContaminant;

                private List<int> recentContaminants = new List<int>();

                private Vector3 currentCyclePosition;
                private bool newPosition;


                public void ContaminantUpdate(Material mat)
                {

                    //if (Driver.storedStandard.InRange(Driver.locationTime.TimeLocation.Location, Driver.CurrentWaypoint()))
                    {
                        if (currentContaminant == null)
                        {
                            currentContaminant = mat;
                        }
                        else if (!recentContaminants.Contains(mat.Id))
                        {
                            if (currentContaminant.ThreatLevel < mat.ThreatLevel)
                            {
                                currentContaminant = mat;
                            }

                        }
                        else if (mat.Id == currentContaminant.Id)
                        {
                            if (mat.Concentration > currentContaminant.Concentration)
                            {
                                currentCyclePosition = Driver.locationTime.TimeLocation.Location;
                                newPosition = true;

                                Console.WriteLine("DRIVER:DETECTION Area with higher concentration of contaminant detected.");
                            }
                        }

                        Driver.CheckState();
                    }
                }

                override public void Cycle()
                {
                    if ( newPosition)
                    {
                        newPosition = false;
                        currentCyclePosition = Driver.locationTime.TimeLocation.Location;
                        Console.WriteLine("DRIVER:DETECTION -> MNM Circle around current location. ");
                    }

                    /*
                     * Nothing occurs until 
                     * A) New cycle position. Need to reinform MNM.
                     * B) MNM finished circling. At that point, send report.
                     */ 
                }

                override public void Start()
                {
                    if (currentContaminant == null)
                    {
                        Console.WriteLine("DRIVER:DETECTION No material defined. Exiting Detection Mode.");
                        Stop();


                    }
                    else
                    {
                        if (currentCyclePosition == null || newPosition)
                        {
                            newPosition = false;
                            currentCyclePosition = Driver.locationTime.TimeLocation.Location;


                            Console.WriteLine("DRIVER:DETECTION -> MNM Circle around current location. ");
                        /*
                         * If MNM is not circle around the current location, tell it to start
                         *
                         */
                        }

                        
                    }
                }

                //Finished circling around a given position, looking for higher concentration of contaminant
                public void DoneSearching()
                {

                    Console.WriteLine("DRIVER:DETECTION Done Searching for contaminant. ");
                    recentContaminants.Add(currentContaminant.Id);
                    currentContaminant = null;
                    
                    Stop();
                }

                override public void Stop()
                {
                    currentCyclePosition = null;
                    Driver.CheckState();
                }

                //Called when reaching a new waypoint.
                public void Clear()
                {

                    Console.WriteLine("DRIVER:DETECTION Recent contaminant list cleared. ");
                    recentContaminants.Clear();
                }

                override public bool Ready()
                {
                    if (currentContaminant != null)
                        return true;
                    else
                        return false;


                }

            }
        }
    }
}