using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASP.General;

namespace ASP
{

    namespace General
    {
        public class PathingManager
        {

            //Assumption
            /*
             * Pathfinding in a aquatic environment is very similar to 2d pathfinding. The major difference is
             * the height factor. Basically, you need to be at value z (or higher) in order to path to a certain tile. 
             * 
             * The added complexity, which is optional for this implementation, is whether or not there is an upper
             * bound at a particular location. The most special situation is when there is both an upper and lower bound
             * at a location (could be a tunnel, cave, etc). 
             * 
             * The first step is to take the environmental map (lets just assume its a simple grid). It will contain z bounds
             * on each of its tiles. The second step is to make sure that the z bounds of adjacent tiles is within the range 
             * of adjacent tiles. If not, it will forever be considered "blocked" (this is optional step). Next step,
             * Receive your goal destination. Using the goal destination, use A*, with the following modifications:
             * 1) All neighbors must also be pathable (check z values).
             * 2) Store the path under waypoint type
             * 
             * Rinse and repeat.
             */

            private Grid map;
               
            private Vector3 currentDestination;
            private Waypoint goal;
            private Waypoint lastWaypoint;


            WaypointManager currentPath = new WaypointManager();


            public PathingManager()
            {
                map = new Grid();
            }

            public PathingManager(double [,] lowerBound, double[,] upperBound, Vector3[,] obstructions)
            {
                 map = new Grid(lowerBound, upperBound,obstructions);
            }

            public Waypoint Goal
            {

                get { return goal; }
                set { CalculatePath(value); goal = value; }
            }
            public Vector3 CurrentDestination
            {

                get { return currentDestination; }
            }

            private void CalculatePath(Waypoint destination)
            {

            }
            
            public void Refresh(){
                CalculatePath(goal);
            }
        }

   
    }

}
