import  java.io.*;
import  java.util.*;
import java.time.LocalDateTime;
public class SoilTemperatureState
{
    private double pInternalAlbedo;
    private double SnowWaterContent;
    private double SoilSurfaceTemperature;
    private Integer AgeOfSnow;
    private Double [] rSoilTempArrayRate;
    private Double [] pSoilLayerDepth;
    private Double [] SoilTempArray;
    
    public SoilTemperatureState() { }
    
    public SoilTemperatureState(SoilTemperatureState toCopy, boolean copyAll) // copy constructor 
    {
        if (copyAll)
        {
            this.pInternalAlbedo = toCopy.getpInternalAlbedo();
            this.SnowWaterContent = toCopy.getSnowWaterContent();
            this.SoilSurfaceTemperature = toCopy.getSoilSurfaceTemperature();
            this.AgeOfSnow = toCopy.getAgeOfSnow();
            rSoilTempArrayRate = new Double[toCopy.getrSoilTempArrayRate().length];
        for (int i = 0; i < toCopy.getrSoilTempArrayRate().length; i++)
        {
            rSoilTempArrayRate[i] = toCopy.getrSoilTempArrayRate()[i];
        }
            pSoilLayerDepth = new Double[toCopy.getpSoilLayerDepth().length];
        for (int i = 0; i < toCopy.getpSoilLayerDepth().length; i++)
        {
            pSoilLayerDepth[i] = toCopy.getpSoilLayerDepth()[i];
        }
            SoilTempArray = new Double[toCopy.getSoilTempArray().length];
        for (int i = 0; i < toCopy.getSoilTempArray().length; i++)
        {
            SoilTempArray[i] = toCopy.getSoilTempArray()[i];
        }
        }
    }
    public double getpInternalAlbedo()
    { return pInternalAlbedo; }

    public void setpInternalAlbedo(double _pInternalAlbedo)
    { this.pInternalAlbedo= _pInternalAlbedo; } 
    
    public double getSnowWaterContent()
    { return SnowWaterContent; }

    public void setSnowWaterContent(double _SnowWaterContent)
    { this.SnowWaterContent= _SnowWaterContent; } 
    
    public double getSoilSurfaceTemperature()
    { return SoilSurfaceTemperature; }

    public void setSoilSurfaceTemperature(double _SoilSurfaceTemperature)
    { this.SoilSurfaceTemperature= _SoilSurfaceTemperature; } 
    
    public Integer getAgeOfSnow()
    { return AgeOfSnow; }

    public void setAgeOfSnow(Integer _AgeOfSnow)
    { this.AgeOfSnow= _AgeOfSnow; } 
    
    public Double [] getrSoilTempArrayRate()
    { return rSoilTempArrayRate; }

    public void setrSoilTempArrayRate(Double [] _rSoilTempArrayRate)
    { this.rSoilTempArrayRate= _rSoilTempArrayRate; } 
    
    public Double [] getpSoilLayerDepth()
    { return pSoilLayerDepth; }

    public void setpSoilLayerDepth(Double [] _pSoilLayerDepth)
    { this.pSoilLayerDepth= _pSoilLayerDepth; } 
    
    public Double [] getSoilTempArray()
    { return SoilTempArray; }

    public void setSoilTempArray(Double [] _SoilTempArray)
    { this.SoilTempArray= _SoilTempArray; } 
    
}