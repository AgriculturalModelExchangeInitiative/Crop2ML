#include "SoilTemperatureState.h"
using namespace Simplace_Soil_Temperature;

SoilTemperatureState::SoilTemperatureState() {}

double SoilTemperatureState::getpInternalAlbedo() { return this->pInternalAlbedo; }
double SoilTemperatureState::getSnowWaterContent() { return this->SnowWaterContent; }
double SoilTemperatureState::getSoilSurfaceTemperature() { return this->SoilSurfaceTemperature; }
int SoilTemperatureState::getAgeOfSnow() { return this->AgeOfSnow; }
std::vector<double> & SoilTemperatureState::getrSoilTempArrayRate() { return this->rSoilTempArrayRate; }
std::vector<double> & SoilTemperatureState::getpSoilLayerDepth() { return this->pSoilLayerDepth; }
std::vector<double> & SoilTemperatureState::getSoilTempArray() { return this->SoilTempArray; }

void SoilTemperatureState::setpInternalAlbedo(double _pInternalAlbedo) { this->pInternalAlbedo = _pInternalAlbedo; }
void SoilTemperatureState::setSnowWaterContent(double _SnowWaterContent) { this->SnowWaterContent = _SnowWaterContent; }
void SoilTemperatureState::setSoilSurfaceTemperature(double _SoilSurfaceTemperature) { this->SoilSurfaceTemperature = _SoilSurfaceTemperature; }
void SoilTemperatureState::setAgeOfSnow(int _AgeOfSnow) { this->AgeOfSnow = _AgeOfSnow; }
void SoilTemperatureState::setrSoilTempArrayRate(std::vector<double> const &_rSoilTempArrayRate){
    this->rSoilTempArrayRate = _rSoilTempArrayRate;
}
void SoilTemperatureState::setpSoilLayerDepth(std::vector<double> const &_pSoilLayerDepth){
    this->pSoilLayerDepth = _pSoilLayerDepth;
}
void SoilTemperatureState::setSoilTempArray(std::vector<double> const &_SoilTempArray){
    this->SoilTempArray = _SoilTempArray;
}