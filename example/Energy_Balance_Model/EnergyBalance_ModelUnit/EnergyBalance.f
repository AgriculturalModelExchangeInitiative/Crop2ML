SUBROUTINE EnergyBalance( extraSolarRadiation,  VPDair,  maxTair,  minTair,  vaporPressure,
             solarRadiation,  tau,  deficitOnTopLayers,
             hslope,  plantHeight, isWindVpDefined,  wind,
             stefanBoltzman,  lambdaV ,  soilDiffusionConstant,
             Alpha,  psychrometricConstant,  tauAlpha,  vonKarman,
             heightWeatherMeasurements,  zm,  d,  zh,  rhoDensityAir,
             specificHeatCapacityAir,  albedoCoefficient,  elevation) 
   
REAL extraSolarRadiation,VPDair, maxTair,  minTair, vaporPressure, solarRadiation, tau,  deficitOnTopLayers
REAL hslope,  plantHeight,isWindVpDefined,  wind, stefanBoltzman,  lambdaV ,  soilDiffusionConstant
REAL Alpha,  psychrometricConstant,  tauAlpha,  vonKarman, heightWeatherMeasurements,  zm,  d,  zh,  rhoDensityAir
REAL specificHeatCapacityAir,  albedoCoefficient,  elevation
        
REAL Nsr, Nolr, clearSkySolarRadiation, averageT, surfaceEmissivity, cloudCoverFactor, h

REAL netRadiation, netOutGoingLongWaveRadiation, netRadiationEquivalentEvaporation, diffusionLimitedEvaporation 
REAL evapoTranspirationPriestlyTaylor, AlphaE, evapoTranspirationPenman, evapoTranspiration, conductance, energyLimitedEvaporation
REAL potentialEvapoTranspiration, soilHeatFlux, cropHeatFlux, maxCanopyTemperature, minCanopyTemperature, potentialTranspiration

Nsr = (1 - albedoCoefficient) * solarRadiation
clearSkySolarRadiation = (0.75 + 2 * (10**-5) * elevation) * extraSolarRadiation
averageT = ((maxTair + 273.16)**4) + ((minTair + 273.16)**4)) / 2
surfaceEmissivity = (0.34 - 0.14 * SQRT(vaporPressure / 10))
cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35)
Nolr = stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor
netRadiation = Nsr - Nolr
netOutGoingLongWaveRadiation = Nolr
netRadiationEquivalentEvaporation = netRadiation / lambdaV * 1000;

IF (deficitOnTopLayers / 1000 .LE. 0.0) THEN
	diffusionLimitedEvaporation = 8.3 * 1000
ELSE IF (deficitOnTopLayers / 1000 .LE. 25.0) THEN
	diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000)) * 1000
ELSE
	diffusionLimitedEvaporation = 0
ENDIF

evapoTranspirationPriestlyTaylor = MAX((Alpha * hslope * (netRadiationEquivalentEvaporation) / (hslope + psychrometricConstant)), 0)

IF (tau .LE. tauAlpha) THEN
	AlphaE = 1;
ELSE
	AlphaE = Alpha - ((Alpha - 1) * (1 - tau) / (1 - tauAlpha))
ENDIF

energyLimitedEvaporation = (evapoTranspirationPriestlyTaylor / Alpha) * AlphaE * tau

h = MAX(10, plantHeight) / 100;
conductance = (wind *(vonKarman**2)) / (LOG((heightWeatherMeasurements - d * h) / (zm * h)) * LOG((heightWeatherMeasurements - d * h) / (zh * h)))

IF(isWindVpDefined == 1) THEN
	evapoTranspirationPenman = evapoTranspirationPriestlyTaylor / Alpha + 1000 * ((rhoDensityAir * specificHeatCapacityAir * VPDair * conductance) / (lambdaV * (hslope + psychrometricConstant)))
    evapoTranspiration = evapoTranspirationPenman
ELSE
    evapoTranspiration = evapoTranspirationPriestlyTaylor
ENDIF

potentialTranspiration = evapoTranspiration * (1 - tau);
soilEvaporation = MIN(diffusionLimitedEvaporation, energyLimitedEvaporation)
potentialEvapoTranspiration = potentialTranspiration + soilEvaporation
soilHeatFlux = tau * netRadiationEquivalentEvaporation - soilEvaporation
cropHeatFlux = netRadiationEquivalentEvaporation - soilHeatFlux - potentialTranspiration
minCanopyTemperature = minTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000)
maxCanopyTemperature = maxTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000)

RETURN

END SUBROUTINE EnergyBalance
