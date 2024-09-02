import  java.io.*;
import  java.util.*;
import java.time.LocalDateTime;

public class SoilTemperatureAuxiliary
{
    private double SnowIsolationIndex;
    
    public SoilTemperatureAuxiliary() { }
    
    public SoilTemperatureAuxiliary(SoilTemperatureAuxiliary toCopy, boolean copyAll) // copy constructor 
    {
        if (copyAll)
        {
            this.SnowIsolationIndex = toCopy.getSnowIsolationIndex();
        }
    }
    public double getSnowIsolationIndex()
    { return SnowIsolationIndex; }

    public void setSnowIsolationIndex(double _SnowIsolationIndex)
    { this.SnowIsolationIndex= _SnowIsolationIndex; } 
    
}