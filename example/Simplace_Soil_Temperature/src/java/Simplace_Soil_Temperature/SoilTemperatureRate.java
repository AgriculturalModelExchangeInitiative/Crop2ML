import  java.io.*;
import  java.util.*;
import java.time.LocalDateTime;

public class SoilTemperatureRate
{
    private double rSnowWaterContentRate;
    private double rSoilSurfaceTemperatureRate;
    private Integer rAgeOfSnowRate;
    
    public SoilTemperatureRate() { }
    
    public SoilTemperatureRate(SoilTemperatureRate toCopy, boolean copyAll) // copy constructor 
    {
        if (copyAll)
        {
            this.rSnowWaterContentRate = toCopy.getrSnowWaterContentRate();
            this.rSoilSurfaceTemperatureRate = toCopy.getrSoilSurfaceTemperatureRate();
            this.rAgeOfSnowRate = toCopy.getrAgeOfSnowRate();
        }
    }
    public double getrSnowWaterContentRate()
    { return rSnowWaterContentRate; }

    public void setrSnowWaterContentRate(double _rSnowWaterContentRate)
    { this.rSnowWaterContentRate= _rSnowWaterContentRate; } 
    
    public double getrSoilSurfaceTemperatureRate()
    { return rSoilSurfaceTemperatureRate; }

    public void setrSoilSurfaceTemperatureRate(double _rSoilSurfaceTemperatureRate)
    { this.rSoilSurfaceTemperatureRate= _rSoilSurfaceTemperatureRate; } 
    
    public Integer getrAgeOfSnowRate()
    { return rAgeOfSnowRate; }

    public void setrAgeOfSnowRate(Integer _rAgeOfSnowRate)
    { this.rAgeOfSnowRate= _rAgeOfSnowRate; } 
    
}