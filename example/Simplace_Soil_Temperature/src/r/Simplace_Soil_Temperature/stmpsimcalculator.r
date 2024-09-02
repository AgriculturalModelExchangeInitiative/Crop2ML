library(gsubfn)

init_stmpsimcalculator <- function (cSoilLayerDepth,
         cFirstDayMeanTemp,
         cAVT,
         cABD,
         cDampingDepth,
         iSoilWaterContent,
         iSoilSurfaceTemperature){
    SoilTempArray<- vector()
    rSoilTempArrayRate<- vector()
    pSoilLayerDepth<- vector()
    SoilTempArray <- NULL
    rSoilTempArrayRate <- NULL
    pSoilLayerDepth <- NULL
    tStmp<- vector()
    tStmpRate<- vector()
    tz<- vector()
    tProfileDepth <- cSoilLayerDepth[length(cSoilLayerDepth) - 1+1]
    additionalDepth <- cDampingDepth - tProfileDepth
    firstAdditionalLayerHight <- additionalDepth - as.double(floor(additionalDepth))
    layers <- as.integer(abs(as.double(ceiling(additionalDepth)))) + length(cSoilLayerDepth)
    tStmp <- vector(, layers)
    tStmpRate <- vector(, layers)
    tz <- vector(, layers)
    for( i in seq(0, length(tStmp)-1, 1)){
        if (i < length(cSoilLayerDepth))
        {
            depth <- cSoilLayerDepth[i+1]
        }
        else
        {
            depth <- tProfileDepth + firstAdditionalLayerHight + i - length(cSoilLayerDepth)
        }
        tz[i+1] <- depth
        tStmp[i+1] <- (cFirstDayMeanTemp * (cDampingDepth - depth) + (cAVT * depth)) / cDampingDepth
    }
    rSoilTempArrayRate <- tStmpRate
    SoilTempArray <- tStmp
    pSoilLayerDepth <- tz
    return (list ("SoilTempArray" = SoilTempArray,"rSoilTempArrayRate" = rSoilTempArrayRate,"pSoilLayerDepth" = pSoilLayerDepth))
}

model_stmpsimcalculator <- function (cSoilLayerDepth,
         cFirstDayMeanTemp,
         cAVT,
         cABD,
         cDampingDepth,
         iSoilWaterContent,
         iSoilSurfaceTemperature,
         SoilTempArray,
         rSoilTempArrayRate,
         pSoilLayerDepth){
    #'- Name: STMPsimCalculator -Version: 001, -Time step: 1
    #'- Description:
    #'            * Title: STMPsimCalculator model
    #'            * Authors: Gunther Krauss
    #'            * Reference: ('http://www.simplace.net/doc/simplace_modules/',)
    #'            * Institution: INRES Pflanzenbau, Uni Bonn
    #'            * ExtendedDescription: as given in the documentation
    #'            * ShortDescription: None
    #'- inputs:
    #'            * name: cSoilLayerDepth
    #'                          ** description : Depth of soil layer
    #'                          ** inputtype : parameter
    #'                          ** parametercategory : constant
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** len : 
    #'                          ** max : 20.0
    #'                          ** min : 0.03
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    #'            * name: cFirstDayMeanTemp
    #'                          ** description : Mean air temperature on first day
    #'                          ** inputtype : parameter
    #'                          ** parametercategory : constant
    #'                          ** datatype : DOUBLE
    #'                          ** max : 50.0
    #'                          ** min : -40.0
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    #'            * name: cAVT
    #'                          ** description : Constant Temperature of deepest soil layer - use long term mean air temperature
    #'                          ** inputtype : parameter
    #'                          ** parametercategory : constant
    #'                          ** datatype : DOUBLE
    #'                          ** max : 20.0
    #'                          ** min : -10.0
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    #'            * name: cABD
    #'                          ** description : Mean bulk density
    #'                          ** inputtype : parameter
    #'                          ** parametercategory : constant
    #'                          ** datatype : DOUBLE
    #'                          ** max : 4.0
    #'                          ** min : 1.0
    #'                          ** default : 2.0
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/tonne_per_cubic_metre
    #'            * name: cDampingDepth
    #'                          ** description : Initial value for damping depth of soil
    #'                          ** inputtype : parameter
    #'                          ** parametercategory : constant
    #'                          ** datatype : DOUBLE
    #'                          ** max : 20.0
    #'                          ** min : 1.5
    #'                          ** default : 6.0
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    #'            * name: iSoilWaterContent
    #'                          ** description : Water content, sum of whole soil profile
    #'                          ** inputtype : variable
    #'                          ** variablecategory : exogenous
    #'                          ** datatype : DOUBLE
    #'                          ** max : 20.0
    #'                          ** min : 1.5
    #'                          ** default : 5.0
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    #'            * name: iSoilSurfaceTemperature
    #'                          ** description : Temperature at soil surface
    #'                          ** inputtype : variable
    #'                          ** variablecategory : exogenous
    #'                          ** datatype : DOUBLE
    #'                          ** max : 20.0
    #'                          ** min : 1.5
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    #'            * name: SoilTempArray
    #'                          ** description : Array of soil temperatures in layers 
    #'                          ** inputtype : variable
    #'                          ** variablecategory : state
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** len : 
    #'                          ** max : 50.0
    #'                          ** min : -40.0
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    #'            * name: rSoilTempArrayRate
    #'                          ** description : Array of daily temperature change
    #'                          ** inputtype : variable
    #'                          ** variablecategory : state
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** len : 
    #'                          ** max : 20
    #'                          ** min : -20
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    #'            * name: pSoilLayerDepth
    #'                          ** description : Depth of soil layer plus additional depth
    #'                          ** inputtype : variable
    #'                          ** variablecategory : state
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** len : 
    #'                          ** max : 20.0
    #'                          ** min : 0.03
    #'                          ** default : 
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    #'- outputs:
    #'            * name: SoilTempArray
    #'                          ** description : Array of soil temperatures in layers 
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** variablecategory : state
    #'                          ** len : 
    #'                          ** max : 50.0
    #'                          ** min : -40.0
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    #'            * name: rSoilTempArrayRate
    #'                          ** description : Array of daily temperature change
    #'                          ** datatype : DOUBLEARRAY
    #'                          ** variablecategory : state
    #'                          ** len : 
    #'                          ** max : 20
    #'                          ** min : -20
    #'                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    XLAG <- .8
    XLG1 <- 1 - XLAG
    DP <- 1 + (2.5 * cABD / (cABD + exp(6.53 - (5.63 * cABD))))
    WC <- 0.001 * iSoilWaterContent / ((.356 - (.144 * cABD)) * cSoilLayerDepth[(length(cSoilLayerDepth) - 1)+1])
    DD <- exp(log(0.5 / DP) * ((1 - WC) / (1 + WC)) * 2) * DP
    Z1 <- as.double(0)
    for( i in seq(0, length(SoilTempArray)-1, 1)){
        ZD <- 0.5 * (Z1 + pSoilLayerDepth[i+1]) / DD
        RATE <- ZD / (ZD + exp(-.8669 - (2.0775 * ZD))) * (cAVT - iSoilSurfaceTemperature)
        RATE <- XLG1 * (RATE + iSoilSurfaceTemperature - SoilTempArray[i+1])
        Z1 <- pSoilLayerDepth[i+1]
        rSoilTempArrayRate[i+1] <- RATE
        SoilTempArray[i+1] <- SoilTempArray[i+1] + rSoilTempArrayRate[i+1]
    }
    return (list ("SoilTempArray" = SoilTempArray,"rSoilTempArrayRate" = rSoilTempArrayRate))
}