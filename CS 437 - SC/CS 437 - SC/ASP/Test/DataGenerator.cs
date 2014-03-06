using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.Report;

namespace ASP.Test
{
    class DataGenerator
    {

   public class Sensors {
	static Random generator = new Random();



    public static Material[] getMaterials()
    {
        int materials = generator.Next(6);
        Material[] mat = new Material[materials];
        for (int i = 0; i < materials; i++)
        {
            int contam = foundContaminant();
            mat[i] = new Material(i,contam!=0, contam, contamAmount());
        }
        return mat;
    }
    public static Material[] getMaterials( Material add)
    {
        int materials = generator.Next(5);
        Material[] mat = new Material[materials+1];
        for (int i = 1; i < materials+1; i++)
        {
            int contam = foundContaminant();
            mat[i] = new Material(generator.Next(1000), contam != 0, contam, contamAmount());
        }
        mat[0] = add;
        return mat;
    }
	public static double getTemp(){
		return generator.NextDouble()*50;
	}
	
	public static double getPressure(){
		return generator.NextDouble()*35;
	}
	
	public static double getRadiation(){
		return generator.NextDouble()*20;
	}
	
	public static double getpH(){
		return generator.NextDouble()*17;
	}
	
	public static double getConductivity(){
		return generator.NextDouble()*2050;  
	}
	
	public static double getLight(){
		return generator.NextDouble()*2150;   
	}
	
	public static bool foundObstruction(){
		return generator.Next(2)==1 ? true : false;
	}
	
	public static int foundContaminant(){
        bool foundSomething = generator.Next(10) == 1 ? true : false; 
		if(foundSomething){
			return generator.Next(13)+1;
		}
		else {
			return 0;
		}
	}
	
	public static double[] obstructionDimensions() {
		double[] dimensions = new double[3]; 
			dimensions[0] = generator.NextDouble()*10; 
			dimensions[1] = generator.NextDouble()*10; 
			dimensions[2] = generator.NextDouble()*10; 
		return dimensions;
	}
	
	public static double contamAmount(){
		return generator.NextDouble()/100;
	}
	
	public static void takePicture(){
		Console.WriteLine("A picture of the obstruction has been taken and sent to the user."); 
	}
}

    }
}
