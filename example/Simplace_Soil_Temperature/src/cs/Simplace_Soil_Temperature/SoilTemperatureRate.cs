using System;
using System.Collections.Generic;

public class SoilTemperatureRate 
{
    private double _rSnowWaterContentRate;
    private double _rSoilSurfaceTemperatureRate;
    private int _rAgeOfSnowRate;
    
        public SoilTemperatureRate() { }
    
    
    public SoilTemperatureRate(SoilTemperatureRate toCopy, bool copyAll) // copy constructor 
    {
    if (copyAll)
    {
    
    rSnowWaterContentRate = toCopy.rSnowWaterContentRate;
    rSoilSurfaceTemperatureRate = toCopy.rSoilSurfaceTemperatureRate;
    rAgeOfSnowRate = toCopy.rAgeOfSnowRate;
    }
    }
    public double rSnowWaterContentRate
        {
            get { return this._rSnowWaterContentRate; }
            set { this._rSnowWaterContentRate= value; } 
        }
    public double rSoilSurfaceTemperatureRate
        {
            get { return this._rSoilSurfaceTemperatureRate; }
            set { this._rSoilSurfaceTemperatureRate= value; } 
        }
    public int rAgeOfSnowRate
        {
            get { return this._rAgeOfSnowRate; }
            set { this._rAgeOfSnowRate= value; } 
        }
}