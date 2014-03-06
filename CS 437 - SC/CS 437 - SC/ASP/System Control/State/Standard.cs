using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP
{
    namespace SystemControl
    {
        namespace States
        {
            /*
                     * Standard Mode 
                     * 
                     * Upon receiving waypoints, and started up, SC will plot the best route between all waypoints. The path does not take
                     * into account obstructions found along the way, but it does take into account obstructions listed by the user.
                     * The SC DOES NOT CONTROL THE MOVEMENT, IT JUST SETS THE DESTINATION. IT MAKES A DECISION AND SAYS "MNM, DO STUFF" 
                     * AND IT HAPPENS. It says "Going left is safe" or "Halt", mnm uses that information to perform correctly.
                     * MNM is not a pseudo module and extension of SC. No, MNM handles all short term (immediate) movement options. 
                     * SC handles plotting the current direction and goal destination. 
                     * 
                     * 1) If not in proximity of goal waypoint, continue on toward goal waypoint. 
                     * 2) If in proximity of goal waypoint, traverse around point. 
                     * 3) If done exploring the point, move on to next point.
                     */
            public class Standard : State
            {

                public PathingManager pathfinding;
                private bool pathingTo;
                private Vector3 lastPosition;

                override public void Cycle()
                {
                    if (pathfinding.Goal != null)
                    {
                        lastPosition = Driver.locationTime.TimeLocation.Location;
                        if (pathingTo)
                        {

                            if (InRange(lastPosition, pathfinding.Goal))
                            {

                                Console.WriteLine("DRIVER:STANDARD Reached Goal Waypoint");
                                pathingTo = false;

                                explore(pathfinding.Goal);
                            }
                        }
                        else if (new Random().Next(1000) == 1) // To simulate the time it may take to analyze an obstruction.
                        {
                            DoneExploring();
                        }

                    }
                    else
                    {
                        nextGoal();
                    }
                    
                     

                }

                public bool InRange(Vector3 vec, Waypoint way)
                {
                    if (vec.Magnitude(way.getVector3()) <= way.getRadius())
                    {
                        return true;

                    }
                    else
                        return false;
                }

                public void DoneExploring(){
                    Console.WriteLine("DRIVER:STANDARD Done Exploring Waypoint: " + pathfinding.Goal.getId());
                    pathingTo = true;
                    nextGoal();
                    
                }

                private void nextGoal(){
                    
                    List<Waypoint> temp = Driver.storedWaypoints.ListWaypoints();

                    if (temp.Count == 0)
                    {
                        Console.Error.WriteLine("DRIVER:STANDARD:Error No waypoints.");
                    }
                    else
                    {
                        
                        int position = temp.IndexOf(pathfinding.Goal);
                        if (position == temp.Count - 1 || position == -1)
                        {
                            pathfinding.Goal = temp[0];
                        }
                        else if (position >= 0)
                        {
                            pathfinding.Goal = temp[position + 1];
                        }
                    }
                }


                private void explore(Waypoint goal)
                {

                    Console.WriteLine("DRIVER:STANDARD -> MNM  Explore waypoint: "+goal.getId());
                }

                public void Refresh()
                {
                    if (pathfinding.Goal==null)
                        Console.Error.WriteLine("DRIVER:STANDARD:Error No goal destination.");
                    else
                    {
                        pathfinding.Refresh();
                        pathingTo = true;
                    }
                }


                override public void Start()
                {
                    
                    Console.WriteLine(Driver.storedWaypoints.ListWaypoints().Count);

                    if (pathfinding == null)
                    {
                        pathfinding = new PathingManager();
                    }
                    if (pathfinding.Goal == null)
                    {
                        nextGoal();
                    }
                    pathingTo = true;

                }

                override public void Stop()
                {
                    pathingTo = true;
                }

                override public bool Ready()
                {
                    if (pathfinding != null && pathfinding.Goal != null)
                        return true;
                    else
                        return false;
                }

        }
        }
    }
}