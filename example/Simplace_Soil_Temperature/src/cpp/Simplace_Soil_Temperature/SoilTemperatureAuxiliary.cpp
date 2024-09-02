#include "SoilTemperatureAuxiliary.h"
using namespace Simplace_Soil_Temperature;

SoilTemperatureAuxiliary::SoilTemperatureAuxiliary() {}

double SoilTemperatureAuxiliary::getSnowIsolationIndex() { return this->SnowIsolationIndex; }

void SoilTemperatureAuxiliary::setSnowIsolationIndex(double _SnowIsolationIndex) { this->SnowIsolationIndex = _SnowIsolationIndex; }