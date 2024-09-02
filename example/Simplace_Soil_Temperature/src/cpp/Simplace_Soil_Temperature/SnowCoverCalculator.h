
#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include "SoilTemperatureState.h"
#include "SoilTemperatureRate.h"
#include "SoilTemperatureAuxiliary.h"
#include "SoilTemperatureExogenous.h"
namespace Simplace_Soil_Temperature {
class SnowCoverCalculator
{
    private:
        double cCarbonContent ;
        int cInitialAgeOfSnow ;
        double cInitialSnowWaterContent ;
        double Albedo ;
        double cSnowIsolationFactorA ;
        double cSnowIsolationFactorB ;
    public:
        SnowCoverCalculator();
        void Calculate_Model(SoilTemperatureState &s, SoilTemperatureState &s1, SoilTemperatureRate &r, SoilTemperatureAuxiliary &a, SoilTemperatureExogenous &ex);
        void Init(SoilTemperatureState &s, SoilTemperatureState &s1, SoilTemperatureRate &r, SoilTemperatureAuxiliary &a, SoilTemperatureExogenous &ex);
        double getcCarbonContent();
        void setcCarbonContent(double _cCarbonContent);
        int getcInitialAgeOfSnow();
        void setcInitialAgeOfSnow(int _cInitialAgeOfSnow);
        double getcInitialSnowWaterContent();
        void setcInitialSnowWaterContent(double _cInitialSnowWaterContent);
        double getAlbedo();
        void setAlbedo(double _Albedo);
        double getcSnowIsolationFactorA();
        void setcSnowIsolationFactorA(double _cSnowIsolationFactorA);
        double getcSnowIsolationFactorB();
        void setcSnowIsolationFactorB(double _cSnowIsolationFactorB);

};
}
