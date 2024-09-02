EnergyBalance = function(extraSolarRadiation, maxTair,minTair,vaporPressure,solarRadiation, tau,deficitOnTopLayers, 
                         hslope, Ntip ,hourlyTemp, hourlyVPDair,hourlySolarRadiation, wc3cm,plantHeight,isWindVpDefined,wind, 
                         RH,stefanBoltzman=4.903E-09,lambdaV= 2.454, soilDiffusionConstant = 4.2,
                         Alpha = 1.5,  psychrometricConstant=0.66,  tauAlpha= 0.3, vonKarman=0.42,
                         heightWeatherMeasurements = 2, zm = 0.13, d= 0.67, zh = 0.013 ,rhoDensityAir=1.225,
                         specificHeatCapacityAir = 0.00101, SwitchMaize = 1,albedoCoefficient = 0.23,elevation=0 )
{
  Nsr = (1 - albedoCoefficient)*solarRadiation
  clearSkySolarRadiation = (0.75 + 2 * 10^(-5) * elevation) * extraSolarRadiation
  averageT =((maxTair + 273.16)^4 + (minTair + 273.16)^4) / 2                           
  surfaceEmissivity = (0.34 - 0.14 * sqrt(vaporPressure / 10.0))                                    
  cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35)
  Nolr = stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor
  netRadiation= Nsr - Nolr
  netOutGoingLongWaveRadiation = Nolr
  
  netRadiationEquivalentEvaporation = netRadiation / lambdaV * 1000
  
  if (deficitOnTopLayers / 1000.0 <= 0)
    diffusionLimitedEvaporation = 8.3 * 1000
  else
    if (deficitOnTopLayers / 1000.0 < 25)
    diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000.0)) * 1000
  else 
    diffusionLimitedEvaporation = 0
  
  evapoTranspirationPriestlyTaylor = max((Alpha * hslope * (netRadiationEquivalentEvaporation) / (hslope + psychrometricConstant)), 0)
  
  
  if (tau < tauAlpha)
    AlphaE = 1 
  else
    AlphaE = Alpha - ((Alpha - 1) * (1 - tau) / (1 - tauAlpha))
  energyLimitedEvaporation= (evapoTranspirationPriestlyTaylor / Alpha) * AlphaE * tau 
  
  h = max(10, plantHeight) / 100.0 
  conductance = (wind * pow(vonKarman, 2)) / (log((heightWeatherMeasurements - d * h) / (zm * h)) * log((heightWeatherMeasurements - d * h) / (zh * h)))
  
  
  if (isWindVpDefined == 1)
    {
      evapoTranspirationPenman = evapoTranspirationPriestlyTaylor / Alpha + 1000.0 * ((rhoDensityAir * specificHeatCapacityAir * VPDair * conductance) / (lambdaV * (hslope + psychrometricConstant))) 
      evapoTranspiration = evapoTranspirationPenman
    }
  else
    evapoTranspiration = evapoTranspirationPriestlyTaylor
  
  
  potentialTranspiration= evapoTranspiration * (1.0 - tau)  
  
  soilEvaporation = min(diffusionLimitedEvaporation, energyLimitedEvaporation)
  
  potentialEvapoTranspiration = potentialTranspiration + soilEvaporation
  
  soilHeatFlux = tau * netRadiationEquivalentEvaporation - soilEvaporation
  
  cropHeatFlux = netRadiationEquivalentEvaporation - soilHeatFlux - potentialTranspiration
  
  minCanopyTemperature = minTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000.0)
  maxCanopyTemperature = maxTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000.0)
  
  
}
