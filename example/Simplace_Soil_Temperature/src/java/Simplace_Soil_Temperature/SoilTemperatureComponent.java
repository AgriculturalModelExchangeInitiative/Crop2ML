public class SoilTemperatureComponent
{
    
    public SoilTemperatureComponent() { }

    SnowCoverCalculator _SnowCoverCalculator = new SnowCoverCalculator();
    STMPsimCalculator _STMPsimCalculator = new STMPsimCalculator();

    public double getcCarbonContent()
    { return _SnowCoverCalculator.getcCarbonContent(); }
    public void setcCarbonContent(double _cCarbonContent){
    _SnowCoverCalculator.setcCarbonContent(_cCarbonContent);
    }

    public double getcAlbedo()
    { return _SnowCoverCalculator.getAlbedo(); }
    public void setcAlbedo(double _cAlbedo){
    _SnowCoverCalculator.setAlbedo(_cAlbedo);
    }

    public Double [] getcSoilLayerDepth()
    { return _STMPsimCalculator.getcSoilLayerDepth(); }
    public void setcSoilLayerDepth(Double [] _cSoilLayerDepth){
    _STMPsimCalculator.setcSoilLayerDepth(_cSoilLayerDepth);
    }

    public double getcFirstDayMeanTemp()
    { return _STMPsimCalculator.getcFirstDayMeanTemp(); }
    public void setcFirstDayMeanTemp(double _cFirstDayMeanTemp){
    _STMPsimCalculator.setcFirstDayMeanTemp(_cFirstDayMeanTemp);
    }

    public double getcAverageGroundTemperature()
    { return _STMPsimCalculator.getcAVT(); }
    public void setcAverageGroundTemperature(double _cAverageGroundTemperature){
    _STMPsimCalculator.setcAVT(_cAverageGroundTemperature);
    }

    public double getcAverageBulkDensity()
    { return _STMPsimCalculator.getcABD(); }
    public void setcAverageBulkDensity(double _cAverageBulkDensity){
    _STMPsimCalculator.setcABD(_cAverageBulkDensity);
    }

    public double getcDampingDepth()
    { return _STMPsimCalculator.getcDampingDepth(); }
    public void setcDampingDepth(double _cDampingDepth){
    _STMPsimCalculator.setcDampingDepth(_cDampingDepth);
    }
    public void  Calculate_Model(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a, SoilTemperatureExogenous ex)
    {
        ex.setiTempMax(ex.getiAirTemperatureMax());
        ex.setiTempMin(ex.getiAirTemperatureMin());
        ex.setiRadiation(ex.getiGlobalSolarRadiation());
        ex.setiSoilTempArray(s.getSoilTempArray());
        _SnowCoverCalculator.Calculate_Model(s, s1, r, a, ex);
        ex.setiSoilSurfaceTemperature(s.getSoilSurfaceTemperature());
        _STMPsimCalculator.Calculate_Model(s, s1, r, a, ex);
    }
    private double cCarbonContent;
    private double cAlbedo;
    private double Albedo;
    private Double [] cSoilLayerDepth;
    private double cFirstDayMeanTemp;
    private double cAverageGroundTemperature;
    private double cAVT;
    private double cAverageBulkDensity;
    private double cABD;
    private double cDampingDepth;
    public SoilTemperatureComponent(SoilTemperatureComponent toCopy) // copy constructor 
    {
        this.cCarbonContent = toCopy.getcCarbonContent();
        this.cAlbedo = toCopy.getcAlbedo();
        
        for (int i = 0; i < toCopy.getcSoilLayerDepth().length; i++)
        {
            cSoilLayerDepth[i] = toCopy.getcSoilLayerDepth()[i];
        }
        this.cFirstDayMeanTemp = toCopy.getcFirstDayMeanTemp();
        this.cAverageGroundTemperature = toCopy.getcAverageGroundTemperature();
        this.cAverageBulkDensity = toCopy.getcAverageBulkDensity();
        this.cDampingDepth = toCopy.getcDampingDepth();

    }
}