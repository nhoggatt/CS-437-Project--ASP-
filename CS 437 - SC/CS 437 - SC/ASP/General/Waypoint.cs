using System;
using System.Collections.Generic;


namespace ASP {
    namespace General{
public class Waypoint
{
    private int id;
    private double radius;
    private Vector3 coords;

    public Waypoint(int id, double x, double y, double z, double radius)
    {
        coords = new Vector3(x,y,z);

        this.id = id;
        this.radius = radius;
    }

    public Waypoint(int id, General.Vector3 coords, double radius)
    {
        this.coords = coords ;
        this.id = id;
        this.radius = radius;
    }

    public Vector3 getVector3()
    {
        return coords;
    }

    public int getId()
    {
        return id;
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

    public void setId(int id)
    {
        this.id= id;
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

    public override string ToString()
    {
        return "id: "+id+" x: "+coords.X + " y: " + coords.Y + " z: " + coords.Z;
    }

}
}
}