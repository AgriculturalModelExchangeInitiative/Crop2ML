public class SoilTemperatureComponent
{
    
        public SoilTemperatureComponent() { }
    

    //Declaration of the associated strategies
    SnowCoverCalculator _SnowCoverCalculator = new SnowCoverCalculator();
    STMPsimCalculator _STMPsimCalculator = new STMPsimCalculator();

    public double cCarbonContent
    {
        get
        {
             return _SnowCoverCalculator.cCarbonContent; 
        }
        set
        {
            _SnowCoverCalculator.cCarbonContent = value;
        }
    }
    public double cAlbedo
    {
        get
        {
             return _SnowCoverCalculator.cAlbedo; 
        }
        set
        {
            _SnowCoverCalculator.cAlbedo = value;
        }
    }
    public double Albedo
    {
        get
        {
             return _SnowCoverCalculator.Albedo; 
        }
        set
        {
            _SnowCoverCalculator.Albedo = value;
        }
    }
    public double[] cSoilLayerDepth
    {
        get
        {
             return _STMPsimCalculator.cSoilLayerDepth; 
        }
        set
        {
            _STMPsimCalculator.cSoilLayerDepth = value;
        }
    }
    public double cFirstDayMeanTemp
    {
        get
        {
             return _STMPsimCalculator.cFirstDayMeanTemp; 
        }
        set
        {
            _STMPsimCalculator.cFirstDayMeanTemp = value;
        }
    }
    public double cAverageGroundTemperature
    {
        get
        {
             return _STMPsimCalculator.cAverageGroundTemperature; 
        }
        set
        {
            _STMPsimCalculator.cAverageGroundTemperature = value;
        }
    }
    public double cAVT
    {
        get
        {
             return _STMPsimCalculator.cAVT; 
        }
        set
        {
            _STMPsimCalculator.cAVT = value;
        }
    }
    public double cAverageBulkDensity
    {
        get
        {
             return _STMPsimCalculator.cAverageBulkDensity; 
        }
        set
        {
            _STMPsimCalculator.cAverageBulkDensity = value;
        }
    }
    public double cABD
    {
        get
        {
             return _STMPsimCalculator.cABD; 
        }
        set
        {
            _STMPsimCalculator.cABD = value;
        }
    }
    public double cDampingDepth
    {
        get
        {
             return _STMPsimCalculator.cDampingDepth; 
        }
        set
        {
            _STMPsimCalculator.cDampingDepth = value;
        }
    }

    public void  CalculateModel(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a, SoilTemperatureExogenous ex)
    {
        Albedo = cAlbedo;
        ex.iTempMax = ex.iAirTemperatureMax;
        ex.iTempMin = ex.iAirTemperatureMin;
        ex.iRadiation = ex.iGlobalSolarRadiation;
        ex.iSoilTempArray = s.SoilTempArray;
        cAVT = cAverageGroundTemperature;
        cABD = cAverageBulkDensity;
        _SnowCoverCalculator.CalculateModel(s,s1, r, a, ex);
        ex.iSoilSurfaceTemperature = s.SoilSurfaceTemperature;
        _STMPsimCalculator.CalculateModel(s,s1, r, a, ex);
    }
    
    public SoilTemperatureComponent(SoilTemperatureComponent toCopy): this() // copy constructor 
    {

        cCarbonContent = toCopy.cCarbonContent;
        cAlbedo = toCopy.cAlbedo;
        Albedo = toCopy.Albedo;
        
            for (int i = 0; i < 100; i++)
            { cSoilLayerDepth[i] = toCopy.cSoilLayerDepth[i]; }
    
        cFirstDayMeanTemp = toCopy.cFirstDayMeanTemp;
        cAverageGroundTemperature = toCopy.cAverageGroundTemperature;
        cAVT = toCopy.cAVT;
        cAverageBulkDensity = toCopy.cAverageBulkDensity;
        cABD = toCopy.cABD;
        cDampingDepth = toCopy.cDampingDepth;
    }
}