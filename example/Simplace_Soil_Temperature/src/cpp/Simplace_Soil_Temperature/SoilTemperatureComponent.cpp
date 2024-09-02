#include "SoilTemperatureComponent.h"
using namespace Simplace_Soil_Temperature;
SoilTemperatureComponent::SoilTemperatureComponent()
{
       
}


double SoilTemperatureComponent::getcCarbonContent(){ return this->cCarbonContent; }
double SoilTemperatureComponent::getcAlbedo(){ return this->cAlbedo; }
std::vector<double> & SoilTemperatureComponent::getcSoilLayerDepth(){ return this->cSoilLayerDepth; }
double SoilTemperatureComponent::getcFirstDayMeanTemp(){ return this->cFirstDayMeanTemp; }
double SoilTemperatureComponent::getcAverageGroundTemperature(){ return this->cAverageGroundTemperature; }
double SoilTemperatureComponent::getcAverageBulkDensity(){ return this->cAverageBulkDensity; }
double SoilTemperatureComponent::getcDampingDepth(){ return this->cDampingDepth; }

void SoilTemperatureComponent::setcCarbonContent(double _cCarbonContent)
{
    _SnowCoverCalculator.setcCarbonContent(_cCarbonContent);
}
void SoilTemperatureComponent::setcAlbedo(double _cAlbedo)
{
    _SnowCoverCalculator.setAlbedo(_cAlbedo);
}
void SoilTemperatureComponent::setcSoilLayerDepth(const std::vector<double> & _cSoilLayerDepth)
{
    _STMPsimCalculator.setcSoilLayerDepth(_cSoilLayerDepth);
}
void SoilTemperatureComponent::setcFirstDayMeanTemp(double _cFirstDayMeanTemp)
{
    _STMPsimCalculator.setcFirstDayMeanTemp(_cFirstDayMeanTemp);
}
void SoilTemperatureComponent::setcAverageGroundTemperature(double _cAverageGroundTemperature)
{
    _STMPsimCalculator.setcAVT(_cAverageGroundTemperature);
}
void SoilTemperatureComponent::setcAverageBulkDensity(double _cAverageBulkDensity)
{
    _STMPsimCalculator.setcABD(_cAverageBulkDensity);
}
void SoilTemperatureComponent::setcDampingDepth(double _cDampingDepth)
{
    _STMPsimCalculator.setcDampingDepth(_cDampingDepth);
}
void SoilTemperatureComponent::Calculate_Model(SoilTemperatureState &s, SoilTemperatureState &s1, SoilTemperatureRate &r, SoilTemperatureAuxiliary &a, SoilTemperatureExogenous &ex)
{
    ex.setiTempMax(ex.getiAirTemperatureMax());
    ex.setiTempMin(ex.getiAirTemperatureMin());
    ex.setiRadiation(ex.getiGlobalSolarRadiation());
    ex.setiSoilTempArray(s.getSoilTempArray());
    _SnowCoverCalculator.Calculate_Model(s, s1, r, a, ex);
    ex.setiSoilSurfaceTemperature(s.getSoilSurfaceTemperature());
    _STMPsimCalculator.Calculate_Model(s, s1, r, a, ex);
}
SoilTemperatureComponent::SoilTemperatureComponent(SoilTemperatureComponent& toCopy)
{
    cCarbonContent = toCopy.getcCarbonContent();
    cAlbedo = toCopy.getcAlbedo();
    for (int i = 0; i < toCopy.getcSoilLayerDepth().size(); i++)
{
    cSoilLayerDepth[i] = toCopy.getcSoilLayerDepth()[i];
}

    cFirstDayMeanTemp = toCopy.getcFirstDayMeanTemp();
    cAverageGroundTemperature = toCopy.getcAverageGroundTemperature();
    cAverageBulkDensity = toCopy.getcAverageBulkDensity();
    cDampingDepth = toCopy.getcDampingDepth();
}