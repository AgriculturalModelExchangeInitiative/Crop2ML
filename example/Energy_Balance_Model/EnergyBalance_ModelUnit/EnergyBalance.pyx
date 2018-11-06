from math import *
import cython
def EnergyBalance(double extraSolarRadiation, double VPDair, double maxTair, double minTair, 
                  double vaporPressure,double solarRadiation, double tau, double deficitOnTopLayers,
                  double hslope, double plantHeight, int isWindVpDefined, double wind,
                  double stefanBoltzman, double lambdaV , double soilDiffusionConstant,
                  double Alpha, double psychrometricConstant, double tauAlpha, double vonKarman,
                  double heightWeatherMeasurements, double zm, double d, double zh, double rhoDensityAir,
                  double specificHeatCapacityAir, double albedoCoefficient, double elevation ):
    cdef double netRadiation, netOutGoingLongWaveRadiation, netRadiationEquivalentEvaporation
    cdef double diffusionLimitedEvaporation, evapoTranspirationPriestlyTaylor, AlphaE, evapoTranspirationPenman
    cdef double evapoTranspiration, energyLimitedEvaporation, conductance, soilHeatFlux,  cropHeatFlux
    cdef double maxCanopyTemperature, minCanopyTemperature, potentialTranspiration
    cdef double Nsr, Nolr, clearSkySolarRadiation, averageT, surfaceEmissivity, cloudCoverFactor, h
    
    Nsr = (1 - albedoCoefficient)*solarRadiation
    clearSkySolarRadiation = (0.75 + 2 * pow(10, -5) * elevation) * extraSolarRadiation;
    averageT =(pow(maxTair + 273.16, 4) + pow(minTair + 273.16, 4)) / 2                           
    surfaceEmissivity = (0.34 - 0.14 * sqrt(vaporPressure / 10.0))                                    
    cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35)
    Nolr = stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor
    netRadiation= Nsr - Nolr
    netOutGoingLongWaveRadiation = Nolr
        
    netRadiationEquivalentEvaporation = netRadiation / lambdaV * 1000;
        
    if (deficitOnTopLayers / 1000.0 <= 0):
        diffusionLimitedEvaporation = 8.3 * 1000
    else:
        if (deficitOnTopLayers / 1000.0 < 25):
            diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000.0)) * 1000;
        else: diffusionLimitedEvaporation = 0
                    
    evapoTranspirationPriestlyTaylor = max((Alpha * hslope * (netRadiationEquivalentEvaporation) / (hslope + psychrometricConstant)), 0);
        
 
    if (tau < tauAlpha):
        AlphaE = 1 ; 
    else: 
        AlphaE = Alpha - ((Alpha - 1) * (1 - tau) / (1 - tauAlpha))
    energyLimitedEvaporation= (evapoTranspirationPriestlyTaylor / Alpha) * AlphaE * tau 
 
    h = max(10, plantHeight) / 100.0 
    conductance = (wind * pow(vonKarman, 2)) / (log((heightWeatherMeasurements - d * h) / (zm * h)) * log((heightWeatherMeasurements - d * h) / (zh * h)));
   
        
    if (isWindVpDefined == 1):
        evapoTranspirationPenman = evapoTranspirationPriestlyTaylor / Alpha + 1000.0 * ((rhoDensityAir * specificHeatCapacityAir * VPDair * conductance) / (lambdaV * (hslope + psychrometricConstant))); 
        evapoTranspiration = evapoTranspirationPenman;
    else:
        evapoTranspiration = evapoTranspirationPriestlyTaylor
        
   
    potentialTranspiration= evapoTranspiration * (1.0 - tau);   
   
    soilEvaporation = min(diffusionLimitedEvaporation, energyLimitedEvaporation);
   
    potentialEvapoTranspiration = potentialTranspiration + soilEvaporation;
   
    soilHeatFlux = tau * netRadiationEquivalentEvaporation - soilEvaporation;
 
    cropHeatFlux = netRadiationEquivalentEvaporation - soilHeatFlux - potentialTranspiration;
 
    minCanopyTemperature = minTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000.0);
    maxCanopyTemperature = maxTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000.0);
    

