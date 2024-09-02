MODULE Soiltemperaturemod
    USE Snowcovercalculatormod
    USE Stmpsimcalculatormod
    IMPLICIT NONE
CONTAINS

    SUBROUTINE model_soiltemperature(cCarbonContent, &
        cAlbedo, &
        iAirTemperatureMax, &
        iAirTemperatureMin, &
        iGlobalSolarRadiation, &
        iRAIN, &
        iCropResidues, &
        iPotentialSoilEvaporation, &
        iLeafAreaIndex, &
        SoilTempArray, &
        cSoilLayerDepth, &
        cFirstDayMeanTemp, &
        cAverageGroundTemperature, &
        cAverageBulkDensity, &
        cDampingDepth, &
        iSoilWaterContent, &
        pInternalAlbedo, &
        SnowWaterContent, &
        SoilSurfaceTemperature, &
        AgeOfSnow, &
        rSoilTempArrayRate, &
        pSoilLayerDepth, &
        SnowIsolationIndex, &
        rSnowWaterContentRate, &
        rSoilSurfaceTemperatureRate, &
        rAgeOfSnowRate)
        IMPLICIT NONE
        INTEGER:: i_cyml_r
        REAL, INTENT(IN) :: cCarbonContent
        REAL, INTENT(IN) :: cAlbedo
        REAL, INTENT(IN) :: iAirTemperatureMax
        REAL, INTENT(IN) :: iAirTemperatureMin
        REAL, INTENT(IN) :: iGlobalSolarRadiation
        REAL, INTENT(IN) :: iRAIN
        REAL, INTENT(IN) :: iCropResidues
        REAL, INTENT(IN) :: iPotentialSoilEvaporation
        REAL, INTENT(IN) :: iLeafAreaIndex
        REAL , DIMENSION(: ), INTENT(INOUT) :: SoilTempArray
        REAL , DIMENSION(: ), INTENT(IN) :: cSoilLayerDepth
        REAL, INTENT(IN) :: cFirstDayMeanTemp
        REAL, INTENT(IN) :: cAverageGroundTemperature
        REAL, INTENT(IN) :: cAverageBulkDensity
        REAL, INTENT(IN) :: cDampingDepth
        REAL, INTENT(IN) :: iSoilWaterContent
        REAL, INTENT(IN) :: pInternalAlbedo
        REAL, INTENT(INOUT) :: SnowWaterContent
        REAL, INTENT(INOUT) :: SoilSurfaceTemperature
        INTEGER, INTENT(INOUT) :: AgeOfSnow
        REAL , DIMENSION(: ), INTENT(INOUT) :: rSoilTempArrayRate
        REAL , DIMENSION(: ), INTENT(IN) :: pSoilLayerDepth
        INTEGER:: cInitialAgeOfSnow
        REAL:: cInitialSnowWaterContent
        REAL:: Albedo
        REAL:: cSnowIsolationFactorA
        REAL:: cSnowIsolationFactorB
        REAL:: iTempMax
        REAL:: iTempMin
        REAL:: iRadiation
        REAL , DIMENSION(: ), ALLOCATABLE :: iSoilTempArray
        REAL, INTENT(OUT) :: rSnowWaterContentRate
        REAL, INTENT(OUT) :: rSoilSurfaceTemperatureRate
        INTEGER, INTENT(OUT) :: rAgeOfSnowRate
        REAL, INTENT(OUT) :: SnowIsolationIndex
        REAL:: cAVT
        REAL:: cABD
        REAL:: iSoilSurfaceTemperature
        !- Name: SoilTemperature -Version: 001, -Time step: 1
        !- Description:
    !            * Title: SoilTemperature model
    !            * Authors: Gunther Krauss
    !            * Reference: ('http://www.simplace.net/doc/simplace_modules/',)
    !            * Institution: INRES Pflanzenbau, Uni Bonn
    !            * ExtendedDescription: as given in the documentation
    !            * ShortDescription: None
        !- inputs:
    !            * name: cCarbonContent
    !                          ** description : Carbon content of upper soil layer
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 20.0
    !                          ** min : 0.5
    !                          ** default : 0.5
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/percent
    !            * name: cAlbedo
    !                          ** description : Albedo
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 1.0
    !                          ** min : 0.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    !            * name: iAirTemperatureMax
    !                          ** description : Daily maximum air temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 50.0
    !                          ** min : -40.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: iAirTemperatureMin
    !                          ** description : Daily minimum air temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 50.0
    !                          ** min : -40.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: iGlobalSolarRadiation
    !                          ** description : Global Solar radiation
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 2000.0
    !                          ** min : 0.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/megajoule_per_square_metre
    !            * name: iRAIN
    !                          ** description : Rain amount
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 60.0
    !                          ** min : 0.0
    !                          ** default : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    !            * name: iCropResidues
    !                          ** description : Crop residues plus above ground biomass
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 20000.0
    !                          ** min : 0.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre
    !            * name: iPotentialSoilEvaporation
    !                          ** description : Potenial Evaporation
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 12.0
    !                          ** min : 0.0
    !                          ** default : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    !            * name: iLeafAreaIndex
    !                          ** description : Leaf area index
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 10.0
    !                          ** min : 0.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre
    !            * name: SoilTempArray
    !                          ** description : Soil Temp array of last day
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : 
    !                          ** max : 35.0
    !                          ** min : -15.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: cSoilLayerDepth
    !                          ** description : Depth of soil layer
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : 
    !                          ** max : 20.0
    !                          ** min : 0.03
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    !            * name: cFirstDayMeanTemp
    !                          ** description : Mean air temperature on first day
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 50.0
    !                          ** min : -40.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: cAverageGroundTemperature
    !                          ** description : Constant Temperature of deepest soil layer - use long term mean air temperature
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 20.0
    !                          ** min : -10.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: cAverageBulkDensity
    !                          ** description : Mean bulk density
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 4.0
    !                          ** min : 1.0
    !                          ** default : 2.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/tonne_per_cubic_metre
    !            * name: cDampingDepth
    !                          ** description : Initial value for damping depth of soil
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 20.0
    !                          ** min : 1.5
    !                          ** default : 6.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    !            * name: iSoilWaterContent
    !                          ** description : Water content, sum of whole soil profile
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 20.0
    !                          ** min : 1.5
    !                          ** default : 5.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    !            * name: pInternalAlbedo
    !                          ** description : Albedo privat
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 1.0
    !                          ** min : 0.0
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    !            * name: SnowWaterContent
    !                          ** description : Snow water content
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 1500.0
    !                          ** min : 0.0
    !                          ** default : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    !            * name: SoilSurfaceTemperature
    !                          ** description : Soil surface temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 70.0
    !                          ** min : -40.0
    !                          ** default : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: AgeOfSnow
    !                          ** description : Age of snow
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : INT
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/day
    !            * name: rSoilTempArrayRate
    !                          ** description : Array of daily temperature change
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : 
    !                          ** max : 20
    !                          ** min : -20
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    !            * name: pSoilLayerDepth
    !                          ** description : Depth of soil layer plus additional depth
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : 
    !                          ** max : 20.0
    !                          ** min : 0.03
    !                          ** default : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
        !- outputs:
    !            * name: SoilSurfaceTemperature
    !                          ** description : Soil surface temperature
    !                          ** datatype : DOUBLE
    !                          ** variablecategory : state
    !                          ** max : 70.0
    !                          ** min : -40.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: SnowIsolationIndex
    !                          ** description : Snow isolation index
    !                          ** datatype : DOUBLE
    !                          ** variablecategory : auxiliary
    !                          ** max : 1.0
    !                          ** min : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    !            * name: SnowWaterContent
    !                          ** description : Snow water content
    !                          ** datatype : DOUBLE
    !                          ** variablecategory : state
    !                          ** max : 1500.0
    !                          ** min : 0.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    !            * name: rSnowWaterContentRate
    !                          ** description : daily snow water content change rate
    !                          ** datatype : DOUBLE
    !                          ** variablecategory : rate
    !                          ** max : 1500.0
    !                          ** min : -1500.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre_per_day
    !            * name: rSoilSurfaceTemperatureRate
    !                          ** description : daily soil surface temperature change rate
    !                          ** datatype : DOUBLE
    !                          ** variablecategory : rate
    !                          ** max : 70.0
    !                          ** min : -40.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    !            * name: rAgeOfSnowRate
    !                          ** description : daily age of snow change rate
    !                          ** datatype : INT
    !                          ** variablecategory : rate
    !                          ** max : 
    !                          ** min : 
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
    !            * name: AgeOfSnow
    !                          ** description : Age of snow
    !                          ** datatype : INT
    !                          ** variablecategory : state
    !                          ** max : 
    !                          ** min : 0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/day
    !            * name: SoilTempArray
    !                          ** description : Array of soil temperatures in layers 
    !                          ** datatype : DOUBLEARRAY
    !                          ** variablecategory : state
    !                          ** len : 
    !                          ** max : 50.0
    !                          ** min : -40.0
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    !            * name: rSoilTempArrayRate
    !                          ** description : Array of daily temperature change
    !                          ** datatype : DOUBLEARRAY
    !                          ** variablecategory : state
    !                          ** len : 
    !                          ** max : 20
    !                          ** min : -20
    !                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
        Albedo = cAlbedo
        iTempMax = iAirTemperatureMax
        iTempMin = iAirTemperatureMin
        iRadiation = iGlobalSolarRadiation
        iSoilTempArray = SoilTempArray
        cAVT = cAverageGroundTemperature
        cABD = cAverageBulkDensity
        call model_snowcovercalculator(cCarbonContent, cInitialAgeOfSnow,  &
                cInitialSnowWaterContent, Albedo, pInternalAlbedo,  &
                cSnowIsolationFactorA, cSnowIsolationFactorB, iTempMax, iTempMin,  &
                iRadiation, iRAIN, iCropResidues, iPotentialSoilEvaporation,  &
                iLeafAreaIndex, iSoilTempArray, SnowWaterContent,  &
                SoilSurfaceTemperature,  &
                AgeOfSnow,rSnowWaterContentRate,rSoilSurfaceTemperatureRate,rAgeOfSnowRate,SnowIsolationIndex)
        iSoilSurfaceTemperature = SoilSurfaceTemperature
        call model_stmpsimcalculator(cSoilLayerDepth, cFirstDayMeanTemp,  &
                cAVT, cABD, cDampingDepth, iSoilWaterContent,  &
                iSoilSurfaceTemperature, SoilTempArray, rSoilTempArrayRate,  &
                pSoilLayerDepth)
    END SUBROUTINE model_soiltemperature

END MODULE
