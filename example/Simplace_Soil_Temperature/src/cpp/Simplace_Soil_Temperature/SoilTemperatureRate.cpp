#include "SoilTemperatureRate.h"
using namespace Simplace_Soil_Temperature;

SoilTemperatureRate::SoilTemperatureRate() {}

double SoilTemperatureRate::getrSnowWaterContentRate() { return this->rSnowWaterContentRate; }
double SoilTemperatureRate::getrSoilSurfaceTemperatureRate() { return this->rSoilSurfaceTemperatureRate; }
int SoilTemperatureRate::getrAgeOfSnowRate() { return this->rAgeOfSnowRate; }

void SoilTemperatureRate::setrSnowWaterContentRate(double _rSnowWaterContentRate) { this->rSnowWaterContentRate = _rSnowWaterContentRate; }
void SoilTemperatureRate::setrSoilSurfaceTemperatureRate(double _rSoilSurfaceTemperatureRate) { this->rSoilSurfaceTemperatureRate = _rSoilSurfaceTemperatureRate; }
void SoilTemperatureRate::setrAgeOfSnowRate(int _rAgeOfSnowRate) { this->rAgeOfSnowRate = _rAgeOfSnowRate; }