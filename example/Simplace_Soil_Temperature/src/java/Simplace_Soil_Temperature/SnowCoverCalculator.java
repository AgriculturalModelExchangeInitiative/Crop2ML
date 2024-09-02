import  java.io.*;
import  java.util.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
public class SnowCoverCalculator
{
    public void Init(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a,  SoilTemperatureExogenous ex)
    {
        double iTempMax = ex.getiTempMax();
        double iTempMin = ex.getiTempMin();
        double iRadiation = ex.getiRadiation();
        double iRAIN = ex.getiRAIN();
        double iCropResidues = ex.getiCropResidues();
        double iPotentialSoilEvaporation = ex.getiPotentialSoilEvaporation();
        double iLeafAreaIndex = ex.getiLeafAreaIndex();
        Double [] iSoilTempArray = ex.getiSoilTempArray();
        double pInternalAlbedo;
        double SnowWaterContent = 0.0;
        double SoilSurfaceTemperature = 0.0;
        Integer AgeOfSnow = 0;
        pInternalAlbedo = 0.0d;
        double TMEAN;
        double TAMPL;
        double DST;
        if (Albedo == (double)(0))
        {
            pInternalAlbedo = 0.0226d * Math.log10(cCarbonContent) + 0.1502d;
        }
        else
        {
            pInternalAlbedo = Albedo;
        }
        TMEAN = 0.5d * (iTempMax + iTempMin);
        TAMPL = 0.5d * (iTempMax - iTempMin);
        DST = TMEAN + (TAMPL * (iRadiation * (1 - pInternalAlbedo) - 14) / 20);
        SoilSurfaceTemperature = DST;
        AgeOfSnow = cInitialAgeOfSnow;
        SnowWaterContent = cInitialSnowWaterContent;
        s.setpInternalAlbedo(pInternalAlbedo);
        s.setSnowWaterContent(SnowWaterContent);
        s.setSoilSurfaceTemperature(SoilSurfaceTemperature);
        s.setAgeOfSnow(AgeOfSnow);
    }
    private double cCarbonContent;
    public double getcCarbonContent()
    { return cCarbonContent; }

    public void setcCarbonContent(double _cCarbonContent)
    { this.cCarbonContent= _cCarbonContent; } 
    
    private Integer cInitialAgeOfSnow;
    public Integer getcInitialAgeOfSnow()
    { return cInitialAgeOfSnow; }

    public void setcInitialAgeOfSnow(Integer _cInitialAgeOfSnow)
    { this.cInitialAgeOfSnow= _cInitialAgeOfSnow; } 
    
    private double cInitialSnowWaterContent;
    public double getcInitialSnowWaterContent()
    { return cInitialSnowWaterContent; }

    public void setcInitialSnowWaterContent(double _cInitialSnowWaterContent)
    { this.cInitialSnowWaterContent= _cInitialSnowWaterContent; } 
    
    private double Albedo;
    public double getAlbedo()
    { return Albedo; }

    public void setAlbedo(double _Albedo)
    { this.Albedo= _Albedo; } 
    
    private double cSnowIsolationFactorA;
    public double getcSnowIsolationFactorA()
    { return cSnowIsolationFactorA; }

    public void setcSnowIsolationFactorA(double _cSnowIsolationFactorA)
    { this.cSnowIsolationFactorA= _cSnowIsolationFactorA; } 
    
    private double cSnowIsolationFactorB;
    public double getcSnowIsolationFactorB()
    { return cSnowIsolationFactorB; }

    public void setcSnowIsolationFactorB(double _cSnowIsolationFactorB)
    { this.cSnowIsolationFactorB= _cSnowIsolationFactorB; } 
    
    public SnowCoverCalculator() { }
    public void  Calculate_Model(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a,  SoilTemperatureExogenous ex)
    {
        //- Name: SnowCoverCalculator -Version: 001, -Time step: 1
        //- Description:
    //            * Title: SnowCoverCalculator model
    //            * Authors: Gunther Krauss
    //            * Reference: ('http://www.simplace.net/doc/simplace_modules/',)
    //            * Institution: INRES Pflanzenbau, Uni Bonn
    //            * ExtendedDescription: as given in the documentation
    //            * ShortDescription: None
        //- inputs:
    //            * name: cCarbonContent
    //                          ** description : Carbon content of upper soil layer
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 20.0
    //                          ** min : 0.5
    //                          ** default : 0.5
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/percent
    //            * name: cInitialAgeOfSnow
    //                          ** description : Initial age of snow
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/percent
    //            * name: cInitialSnowWaterContent
    //                          ** description : Initial snow water content
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1500.0
    //                          ** min : 0.0
    //                          ** default : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/percent
    //            * name: Albedo
    //                          ** description : Albedo
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1.0
    //                          ** min : 0.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    //            * name: pInternalAlbedo
    //                          ** description : Albedo privat
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 1.0
    //                          ** min : 0.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    //            * name: cSnowIsolationFactorA
    //                          ** description : Static part of the snow isolation index calculation
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 10.0
    //                          ** min : 0.0
    //                          ** default : 2.3
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    //            * name: cSnowIsolationFactorB
    //                          ** description : Dynamic part of the snow isolation index calculation
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1.0
    //                          ** min : 0.0
    //                          ** default : 0.22
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    //            * name: iTempMax
    //                          ** description : Daily maximum air temperature
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 50.0
    //                          ** min : -40.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: iTempMin
    //                          ** description : Daily minimum air temperature
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 50.0
    //                          ** min : -40.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: iRadiation
    //                          ** description : Global Solar radiation
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 2000.0
    //                          ** min : 0.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/megajoule_per_square_metre
    //            * name: iRAIN
    //                          ** description : Rain amount
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 60.0
    //                          ** min : 0.0
    //                          ** default : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    //            * name: iCropResidues
    //                          ** description : Crop residues plus above ground biomass
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 20000.0
    //                          ** min : 0.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre
    //            * name: iPotentialSoilEvaporation
    //                          ** description : Potenial Evaporation
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 12.0
    //                          ** min : 0.0
    //                          ** default : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    //            * name: iLeafAreaIndex
    //                          ** description : Leaf area index
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 10.0
    //                          ** min : 0.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre
    //            * name: iSoilTempArray
    //                          ** description : Soil Temp array of last day
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : 
    //                          ** max : 35.0
    //                          ** min : -15.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: SnowWaterContent
    //                          ** description : Snow water content
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 1500.0
    //                          ** min : 0.0
    //                          ** default : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    //            * name: SoilSurfaceTemperature
    //                          ** description : Soil surface temperature
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 70.0
    //                          ** min : -40.0
    //                          ** default : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: AgeOfSnow
    //                          ** description : Age of snow
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : INT
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/day
        //- outputs:
    //            * name: SnowWaterContent
    //                          ** description : Snow water content
    //                          ** datatype : DOUBLE
    //                          ** variablecategory : state
    //                          ** max : 1500.0
    //                          ** min : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    //            * name: SoilSurfaceTemperature
    //                          ** description : Soil surface temperature
    //                          ** datatype : DOUBLE
    //                          ** variablecategory : state
    //                          ** max : 70.0
    //                          ** min : -40.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: AgeOfSnow
    //                          ** description : Age of snow
    //                          ** datatype : INT
    //                          ** variablecategory : state
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/day
    //            * name: rSnowWaterContentRate
    //                          ** description : daily snow water content change rate
    //                          ** datatype : DOUBLE
    //                          ** variablecategory : rate
    //                          ** max : 1500.0
    //                          ** min : -1500.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre_per_day
    //            * name: rSoilSurfaceTemperatureRate
    //                          ** description : daily soil surface temperature change rate
    //                          ** datatype : DOUBLE
    //                          ** variablecategory : rate
    //                          ** max : 70.0
    //                          ** min : -40.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    //            * name: rAgeOfSnowRate
    //                          ** description : daily age of snow change rate
    //                          ** datatype : INT
    //                          ** variablecategory : rate
    //                          ** max : 
    //                          ** min : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    //            * name: SnowIsolationIndex
    //                          ** description : Snow isolation index
    //                          ** datatype : DOUBLE
    //                          ** variablecategory : auxiliary
    //                          ** max : 1.0
    //                          ** min : 0.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
        double pInternalAlbedo = s.getpInternalAlbedo();
        double iTempMax = ex.getiTempMax();
        double iTempMin = ex.getiTempMin();
        double iRadiation = ex.getiRadiation();
        double iRAIN = ex.getiRAIN();
        double iCropResidues = ex.getiCropResidues();
        double iPotentialSoilEvaporation = ex.getiPotentialSoilEvaporation();
        double iLeafAreaIndex = ex.getiLeafAreaIndex();
        Double [] iSoilTempArray = ex.getiSoilTempArray();
        double SnowWaterContent = s.getSnowWaterContent();
        double SoilSurfaceTemperature = s.getSoilSurfaceTemperature();
        Integer AgeOfSnow = s.getAgeOfSnow();
        double rSnowWaterContentRate;
        double rSoilSurfaceTemperatureRate;
        Integer rAgeOfSnowRate;
        double SnowIsolationIndex;
        double tiCropResidues;
        double tiSoilTempArray;
        double TMEAN;
        double TAMPL;
        double DST;
        double tSoilSurfaceTemperature;
        double tSnowIsolationIndex;
        double SNOWEVAPORATION;
        double SNOWMELT;
        double EAJ;
        double ageOfSnowFactor;
        double SNPKT;
        tiCropResidues = iCropResidues * 10.0d;
        tiSoilTempArray = iSoilTempArray[0];
        TMEAN = 0.5d * (iTempMax + iTempMin);
        TAMPL = 0.5d * (iTempMax - iTempMin);
        DST = TMEAN + (TAMPL * (iRadiation * (1 - pInternalAlbedo) - 14) / 20);
        if (iRAIN > (double)(0) && (tiSoilTempArray < (double)(1) || (SnowWaterContent > (double)(3) || SoilSurfaceTemperature < (double)(0))))
        {
            SnowWaterContent = SnowWaterContent + iRAIN;
        }
        tSnowIsolationIndex = 1.0d;
        if (tiCropResidues < (double)(10))
        {
            tSnowIsolationIndex = tiCropResidues / (tiCropResidues + Math.exp(5.34d - (2.4d * tiCropResidues)));
        }
        if (SnowWaterContent < 1E-10d)
        {
            tSnowIsolationIndex = tSnowIsolationIndex * 0.85d;
            tSoilSurfaceTemperature = 0.5d * (DST + ((1 - tSnowIsolationIndex) * DST) + (tSnowIsolationIndex * tiSoilTempArray));
        }
        else
        {
            tSnowIsolationIndex = Math.max(SnowWaterContent / (SnowWaterContent + Math.exp(cSnowIsolationFactorA - (cSnowIsolationFactorB * SnowWaterContent))), tSnowIsolationIndex);
            tSoilSurfaceTemperature = (1 - tSnowIsolationIndex) * DST + (tSnowIsolationIndex * tiSoilTempArray);
        }
        if (SnowWaterContent == (double)(0) && !(iRAIN > (double)(0) && tiSoilTempArray < (double)(1)))
        {
            SnowWaterContent = (double)(0);
        }
        else
        {
            EAJ = .5d;
            if (SnowWaterContent < (double)(5))
            {
                EAJ = Math.exp(-Math.max((0.4d * iLeafAreaIndex), (0.1d * (tiCropResidues + 0.1d))));
            }
            SNOWEVAPORATION = iPotentialSoilEvaporation * EAJ;
            ageOfSnowFactor = AgeOfSnow / (AgeOfSnow + Math.exp(5.34d - (2.395d * AgeOfSnow)));
            SNPKT = .3333d * (2 * Math.min(tSoilSurfaceTemperature, tiSoilTempArray) + iTempMax);
            if (TMEAN > (double)(0))
            {
                SNOWMELT = Math.max(0, Math.sqrt(iTempMax * iRadiation) * (1.52d + (.54d * ageOfSnowFactor * SNPKT)));
            }
            else
            {
                SNOWMELT = (double)(0);
            }
            if (SNOWMELT + SNOWEVAPORATION > SnowWaterContent)
            {
                SNOWMELT = SNOWMELT / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
                SNOWEVAPORATION = SNOWEVAPORATION / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
            }
            SnowWaterContent = SnowWaterContent - (SNOWMELT + SNOWEVAPORATION);
            if (SnowWaterContent < (double)(0))
            {
                SnowWaterContent = (double)(0);
            }
            if (SnowWaterContent < (double)(5))
            {
                AgeOfSnow = 0;
            }
            else
            {
                AgeOfSnow = AgeOfSnow + 1;
            }
        }
        rSnowWaterContentRate = SnowWaterContent - SnowWaterContent;
        rSoilSurfaceTemperatureRate = tSoilSurfaceTemperature - SoilSurfaceTemperature;
        rAgeOfSnowRate = AgeOfSnow - AgeOfSnow;
        SoilSurfaceTemperature = tSoilSurfaceTemperature;
        SnowIsolationIndex = tSnowIsolationIndex;
        s.setSnowWaterContent(SnowWaterContent);
        s.setSoilSurfaceTemperature(SoilSurfaceTemperature);
        s.setAgeOfSnow(AgeOfSnow);
        r.setrSnowWaterContentRate(rSnowWaterContentRate);
        r.setrSoilSurfaceTemperatureRate(rSoilSurfaceTemperatureRate);
        r.setrAgeOfSnowRate(rAgeOfSnowRate);
        a.setSnowIsolationIndex(SnowIsolationIndex);
    }
}