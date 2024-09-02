#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include<vector>
#include<string>
namespace Simplace_Soil_Temperature {
class SoilTemperatureState
{
    private:
        double pInternalAlbedo ;
        double SnowWaterContent ;
        double SoilSurfaceTemperature ;
        int AgeOfSnow ;
        std::vector<double> rSoilTempArrayRate ;
        std::vector<double> pSoilLayerDepth ;
        std::vector<double> SoilTempArray ;
    public:
        SoilTemperatureState();
        double getpInternalAlbedo();
        void setpInternalAlbedo(double _pInternalAlbedo);
        double getSnowWaterContent();
        void setSnowWaterContent(double _SnowWaterContent);
        double getSoilSurfaceTemperature();
        void setSoilSurfaceTemperature(double _SoilSurfaceTemperature);
        int getAgeOfSnow();
        void setAgeOfSnow(int _AgeOfSnow);
        std::vector<double> & getrSoilTempArrayRate();
        void setrSoilTempArrayRate(const std::vector<double> &  _rSoilTempArrayRate);
        std::vector<double> & getpSoilLayerDepth();
        void setpSoilLayerDepth(const std::vector<double> &  _pSoilLayerDepth);
        std::vector<double> & getSoilTempArray();
        void setSoilTempArray(const std::vector<double> &  _SoilTempArray);

};
}
