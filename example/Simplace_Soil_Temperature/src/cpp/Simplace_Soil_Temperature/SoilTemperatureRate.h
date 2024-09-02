#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
namespace Simplace_Soil_Temperature {
class SoilTemperatureRate
{
    private:
        double rSnowWaterContentRate ;
        double rSoilSurfaceTemperatureRate ;
        int rAgeOfSnowRate ;
    public:
        SoilTemperatureRate();
        double getrSnowWaterContentRate();
        void setrSnowWaterContentRate(double _rSnowWaterContentRate);
        double getrSoilSurfaceTemperatureRate();
        void setrSoilSurfaceTemperatureRate(double _rSoilSurfaceTemperatureRate);
        int getrAgeOfSnowRate();
        void setrAgeOfSnowRate(int _rAgeOfSnowRate);

};
}
