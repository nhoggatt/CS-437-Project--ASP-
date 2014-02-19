using System;
using System.Collections.Generic;

public class Waypoint
{
    private double x, y, z, radius;

    public Waypoint(double x, double y, double z, double radius)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.radius = radius;
    }

    public double getX (){
        return x;
    }

    public double getY()
    {
        return y;
    }

    public double getZ()
    {
        return z;
    }
    public void setX(double x)
    {
        this.x = x;
    }

    public void setY(double y)
    {
        this.y = y;
    }

    public void setZ(double z)
    {
        this.z = z;
    }

}
