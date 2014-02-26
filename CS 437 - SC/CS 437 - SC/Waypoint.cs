using System;
using System.Collections.Generic;


namespace ASP {
    namespace General{
public class Waypoint
{
    private double radius;
    private Vector3 coords;

    public Waypoint(double x, double y, double z, double radius)
    {
        coords = new Vector3(x,y,z);

        this.radius = radius;
    }

    public Waypoint(General.Vector3 coords, double radius)
    {
        this.coords = coords ;

        this.radius = radius;
    }

    public Vector3 getVector3()
    {
        return coords;
    }
    public double getX (){
        return coords.X;
    }

    public double getY()
    {
        return coords.Y;
    }

    public double getZ()
    {
        return coords.Z;
    }

    public double getRadius()
    {
        return radius;
    }
    public void setX(double x)
    {
        coords.X = x;
    }

    public void setY(double y)
    {
         coords.Y = y;
    }

    public void setZ(double z)
    {
        coords.Z = z;
    }

}
}
}