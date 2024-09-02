#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
namespace Simplace_Soil_Temperature {
class SoilTemperatureAuxiliary
{
    private:
        double SnowIsolationIndex ;
    public:
        SoilTemperatureAuxiliary();
        double getSnowIsolationIndex();
        void setSnowIsolationIndex(double _SnowIsolationIndex);

};
}
