using System;
using System.Collections.Generic;
using System.Threading;


namespace ASP
{
    namespace General
    {
        public class WaypointManager
        {
            private List<Waypoint> waypoints = new List<Waypoint>();
            static int checkedValue = 0;
            void Lockin()
            {


                while (0 != Interlocked.Exchange(ref checkedValue, 1))
                {
                    
                }
                
            }
            void LockOut()
            {


                Interlocked.Exchange(ref checkedValue, 0);
                


            }
            public void AddWaypoint(Waypoint waypoint)
            {
                Lockin();
                waypoints.Add(waypoint);
                LockOut();
            }

            public void AddWaypoint(double x, double y, double z, double radius)
            {
                AddWaypoint(new Waypoint(waypoints.Count, x, y, z, radius));
            }

            public List<Waypoint> ListWaypoints() // Should go to report
            {
                Lockin();
                LockOut();
                return waypoints;
            }

            public void RemoveWaypoint(int id)
            {
                Lockin();
                waypoints.RemoveAt(id);
                LockOut();
            }

            public int GetId(Waypoint way)
            {
                return waypoints.FindIndex(x => x.getX() == way.getX() && x.getY() == way.getY() && x.getZ() == way.getZ()
                    && x.getRadius() == way.getRadius());
                
            }

            public void UpdateWaypoint(int id, double x, double y, double z, double radius)
            {
                Lockin();
                waypoints[id] = new Waypoint(id,x, y, z, radius);
                LockOut();
            }
            
            





        }
    }
}