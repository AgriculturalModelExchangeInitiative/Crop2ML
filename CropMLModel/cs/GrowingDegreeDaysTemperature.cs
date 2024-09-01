double Tavg = (AirTemperatureMaximum+AirTemperatureMinimum)/2;

if (Tavg <= BaseTemperatureDevelopment)
{
    GrowingDegreeDaysTemperatureRate = 0;
}
else if (Tavg >= CutoffTemperatureDevelopment)
{
    GrowingDegreeDaysTemperatureRate = CutoffTemperatureDevelopment -
      BaseTemperatureDevelopment;
}
else
{
    GrowingDegreeDaysTemperatureRate = Tavg - BaseTemperatureDevelopment;
}

GrowingDegreeDaysTemperature = GrowingDegreeDaysTemperature + GrowingDegreeDaysTemperatureRate;