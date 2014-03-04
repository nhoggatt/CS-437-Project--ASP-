using System;
using System.Collections.Generic;


namespace ASP
{
    namespace General
    {
        public class WaypointManager
        {
            private List<Waypoint> waypoints = new List<Waypoint>();

            public void AddWaypoint(Waypoint waypoint)
            {

                waypoints.Add(waypoint);
            }

            public void AddWaypoint(double x, double y, double z, double radius)
            {

                AddWaypoint(new Waypoint(x, y, z, radius));
            }

            public List<Waypoint> ListWaypoints() // Should go to report
            {
                return waypoints;
            }

            public void RemoveWaypoint(int id)
            {
                waypoints.RemoveAt(id);
            }

            public int GetId(Waypoint way)
            {
                return waypoints.FindIndex(x => x.getX() == way.getX() && x.getY() == way.getY() && x.getZ() == way.getZ()
                    && x.getRadius() == way.getRadius());
            }

            public void UpdateWaypoint(int id, double x, double y, double z, double radius)
            {
                waypoints[id] = new Waypoint(x, y, z, radius);
            }





        }
    }
}