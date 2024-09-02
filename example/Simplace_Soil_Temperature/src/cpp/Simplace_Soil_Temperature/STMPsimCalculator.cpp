#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include <numeric>
#include <algorithm>
#include <array>
#include <map>
#include <tuple>
#include "STMPsimCalculator.h"
using namespace Simplace_Soil_Temperature;
void STMPsimCalculator::Init(SoilTemperatureState &s, SoilTemperatureState &s1, SoilTemperatureRate &r, SoilTemperatureAuxiliary &a, SoilTemperatureExogenous &ex)
{
    double iSoilWaterContent = ex.getiSoilWaterContent();
    double iSoilSurfaceTemperature = ex.getiSoilSurfaceTemperature();
    std::vector<double> SoilTempArray;
    std::vector<double> rSoilTempArrayRate;
    std::vector<double> pSoilLayerDepth;
    double tProfileDepth;
    double additionalDepth;
    double firstAdditionalLayerHight;
    int layers;
    std::vector<double> tStmp;
    std::vector<double> tStmpRate;
    std::vector<double> tz;
    int i;
    double depth;
    tProfileDepth = cSoilLayerDepth[cSoilLayerDepth.size() - 1];
    additionalDepth = cDampingDepth - tProfileDepth;
    firstAdditionalLayerHight = additionalDepth - float(std::floor(additionalDepth));
    layers = int(std::abs(float((int) std::ceil(additionalDepth)))) + cSoilLayerDepth.size();
    tStmp = std::vector<double> (layers);
    tStmpRate = std::vector<double> (layers);
    tz = std::vector<double> (layers);
    for (i=0 ; i!=tStmp.size() ; i+=1)
    {
        if (i < cSoilLayerDepth.size())
        {
            depth = cSoilLayerDepth[i];
        }
        else
        {
            depth = tProfileDepth + firstAdditionalLayerHight + i - cSoilLayerDepth.size();
        }
        tz[i] = depth;
        tStmp[i] = (cFirstDayMeanTemp * (cDampingDepth - depth) + (cAVT * depth)) / cDampingDepth;
    }
    rSoilTempArrayRate = tStmpRate;
    SoilTempArray = tStmp;
    pSoilLayerDepth = tz;
    s.setSoilTempArray(SoilTempArray);
    s.setrSoilTempArrayRate(rSoilTempArrayRate);
    s.setpSoilLayerDepth(pSoilLayerDepth);
}
STMPsimCalculator::STMPsimCalculator() {}
std::vector<double> & STMPsimCalculator::getcSoilLayerDepth() { return this->cSoilLayerDepth; }
double STMPsimCalculator::getcFirstDayMeanTemp() { return this->cFirstDayMeanTemp; }
double STMPsimCalculator::getcAVT() { return this->cAVT; }
double STMPsimCalculator::getcABD() { return this->cABD; }
double STMPsimCalculator::getcDampingDepth() { return this->cDampingDepth; }
void STMPsimCalculator::setcSoilLayerDepth(std::vector<double> const &_cSoilLayerDepth){
    this->cSoilLayerDepth = _cSoilLayerDepth;
}
void STMPsimCalculator::setcFirstDayMeanTemp(double _cFirstDayMeanTemp) { this->cFirstDayMeanTemp = _cFirstDayMeanTemp; }
void STMPsimCalculator::setcAVT(double _cAVT) { this->cAVT = _cAVT; }
void STMPsimCalculator::setcABD(double _cABD) { this->cABD = _cABD; }
void STMPsimCalculator::setcDampingDepth(double _cDampingDepth) { this->cDampingDepth = _cDampingDepth; }
void STMPsimCalculator::Calculate_Model(SoilTemperatureState &s, SoilTemperatureState &s1, SoilTemperatureRate &r, SoilTemperatureAuxiliary &a, SoilTemperatureExogenous &ex)
{
    //- Name: STMPsimCalculator -Version: 001, -Time step: 1
    //- Description:
    //            * Title: STMPsimCalculator model
    //            * Authors: Gunther Krauss
    //            * Reference: ('http://www.simplace.net/doc/simplace_modules/',)
    //            * Institution: INRES Pflanzenbau, Uni Bonn
    //            * ExtendedDescription: as given in the documentation
    //            * ShortDescription: None
    //- inputs:
    //            * name: cSoilLayerDepth
    //                          ** description : Depth of soil layer
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : 
    //                          ** max : 20.0
    //                          ** min : 0.03
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    //            * name: cFirstDayMeanTemp
    //                          ** description : Mean air temperature on first day
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 50.0
    //                          ** min : -40.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: cAVT
    //                          ** description : Constant Temperature of deepest soil layer - use long term mean air temperature
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 20.0
    //                          ** min : -10.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: cABD
    //                          ** description : Mean bulk density
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 4.0
    //                          ** min : 1.0
    //                          ** default : 2.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/tonne_per_cubic_metre
    //            * name: cDampingDepth
    //                          ** description : Initial value for damping depth of soil
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 20.0
    //                          ** min : 1.5
    //                          ** default : 6.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    //            * name: iSoilWaterContent
    //                          ** description : Water content, sum of whole soil profile
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 20.0
    //                          ** min : 1.5
    //                          ** default : 5.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
    //            * name: iSoilSurfaceTemperature
    //                          ** description : Temperature at soil surface
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 20.0
    //                          ** min : 1.5
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: SoilTempArray
    //                          ** description : Array of soil temperatures in layers 
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : 
    //                          ** max : 50.0
    //                          ** min : -40.0
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: rSoilTempArrayRate
    //                          ** description : Array of daily temperature change
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : 
    //                          ** max : 20
    //                          ** min : -20
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    //            * name: pSoilLayerDepth
    //                          ** description : Depth of soil layer plus additional depth
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : 
    //                          ** max : 20.0
    //                          ** min : 0.03
    //                          ** default : 
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/metre
    //- outputs:
    //            * name: SoilTempArray
    //                          ** description : Array of soil temperatures in layers 
    //                          ** datatype : DOUBLEARRAY
    //                          ** variablecategory : state
    //                          ** len : 
    //                          ** max : 50.0
    //                          ** min : -40.0
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
    //            * name: rSoilTempArrayRate
    //                          ** description : Array of daily temperature change
    //                          ** datatype : DOUBLEARRAY
    //                          ** variablecategory : state
    //                          ** len : 
    //                          ** max : 20
    //                          ** min : -20
    //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day
    double iSoilWaterContent = ex.getiSoilWaterContent();
    double iSoilSurfaceTemperature = ex.getiSoilSurfaceTemperature();
    std::vector<double> & SoilTempArray = s.getSoilTempArray();
    std::vector<double> & rSoilTempArrayRate = s.getrSoilTempArrayRate();
    std::vector<double> & pSoilLayerDepth = s.getpSoilLayerDepth();
    double XLAG;
    double XLG1;
    double DP;
    double WC;
    double DD;
    double Z1;
    int i;
    double ZD;
    double RATE;
    XLAG = .8;
    XLG1 = 1 - XLAG;
    DP = 1 + (2.5 * cABD / (cABD + std::exp(6.53 - (5.63 * cABD))));
    WC = 0.001 * iSoilWaterContent / ((.356 - (.144 * cABD)) * cSoilLayerDepth[(cSoilLayerDepth.size() - 1)]);
    DD = std::exp(std::log(0.5 / DP) * ((1 - WC) / (1 + WC)) * 2) * DP;
    Z1 = float(0);
    for (i=0 ; i!=SoilTempArray.size() ; i+=1)
    {
        ZD = 0.5 * (Z1 + pSoilLayerDepth[i]) / DD;
        RATE = ZD / (ZD + std::exp(-.8669 - (2.0775 * ZD))) * (cAVT - iSoilSurfaceTemperature);
        RATE = XLG1 * (RATE + iSoilSurfaceTemperature - SoilTempArray[i]);
        Z1 = pSoilLayerDepth[i];
        rSoilTempArrayRate[i] = RATE;
        SoilTempArray[i] = SoilTempArray[i] + rSoilTempArrayRate[i];
    }
    s.setSoilTempArray(SoilTempArray);
    s.setrSoilTempArrayRate(rSoilTempArrayRate);
}