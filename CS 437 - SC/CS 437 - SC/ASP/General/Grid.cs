using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.General
{
   class Grid
    {
       private double[,] lowerBound = new double[,]
                {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}

                };
       private double[,] upperBound = new double[,]
                {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}

                };

       Vector3[,] obstructions;

       public Grid(double [,] lowerBound, double[,] upperBound, Vector3[,] coords)
       {
           this.lowerBound = lowerBound;
           this.upperBound = upperBound;
           this.obstructions = coords;
       }

       public Grid()
       {
           Console.WriteLine("DRIVER:STANDARD:PATHINGMANAGER:GRID Test map in use.");
           obstructions = new Vector3[4,4];
           for (int i = 0; i < lowerBound.GetLength(0); i++)
           {
               for (int j = 0; j <lowerBound.GetLength(0); j++)
               {
                   Console.WriteLine(i+" "+j);
                   obstructions[i,j] = new Vector3(i,j,0);
               }
           }
       }


        public double[,] LowerBound
        {
            get { return lowerBound; }
        }
        public double[,] UpperBound
        {
            get { return upperBound; }
        }
            

        public Vector3[,] Coords
        {
            get { return obstructions; }
        }



    }
}
