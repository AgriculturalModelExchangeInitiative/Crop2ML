
using System;
public static class Energy_Balance
{
	static void EnergyBalance(double extraSolarRadiation, double VPDair, double maxTair, double minTair, double vaporPressure,double solarRadiation, double tau, double deficitOnTopLayers, 
   							double hslope, double Ntip, double wc3cm,double plantHeight,int isWindVpDefined,double wind, 
   						 	double stefanBoltzman=4.903E-09,double lambdaV= 2.454, double soilDiffusionConstant = 4.2,
   							double Alpha = 1.5,  double psychrometricConstant=0.66,  double tauAlpha= 0.3, double vonKarman=0.42,
   							double heightWeatherMeasurements = 2, double zm = 0.13, double d= 0.67, double zh = 0.013 ,double rhoDensityAir=1.225,
   							double specificHeatCapacityAir = 0.00101, int SwitchMaize = 1,double albedoCoefficient = 0.23,double elevation=0)
	{
		double netRadiation;
		double netOutGoingLongWaveRadiation;
		double netRadiationEquivalentEvaporation;
		double diffusionLimitedEvaporation;
		double evapoTranspirationPriestlyTaylor;
		double AlphaE;
		double evapoTranspirationPenman;
		double evapoTranspiration;
			
		double Nsr = (1 - albedoCoefficient) * solarRadiation;
		double clearSkySolarRadiation = (0.75 + 2 * Math.Pow(10, -5) * elevation) * extraSolarRadiation;
		double averageT = (Math.Pow(maxTair + 273.16, 4) + Math.Pow(minTair + 273.16, 4)) / 2;
		double surfaceEmissivity = (0.34 - 0.14 * Math.Sqrt(vaporPressure / 10));
		double cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35);
		double Nolr = stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor;
		netRadiation= Nsr - Nolr;
		netOutGoingLongWaveRadiation = Nolr;
		
		netRadiationEquivalentEvaporation = netRadiation / lambdaV * 1000;
		
		if (deficitOnTopLayers / 1000 <= 0)
		{
			diffusionLimitedEvaporation = 8.3 * 1000;
		}
		else
		{
			if (deficitOnTopLayers / 1000 < 25)
			{
				diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000)) * 1000;
			}
			else
			{
				diffusionLimitedEvaporation = 0;
			}
		}
		
		evapoTranspirationPriestlyTaylor = Math.Max((Alpha * hslope * (netRadiationEquivalentEvaporation) / (hslope + psychrometricConstant)), 0);
		
		
		if (tau < tauAlpha)
		{
			AlphaE = 1 ;
		}
		else
		{
			AlphaE = Alpha - ((Alpha - 1) * (1 - tau) / (1 - tauAlpha));
		}
		double energyLimitedEvaporation= (evapoTranspirationPriestlyTaylor / Alpha) * AlphaE * tau;
		
		double h = Math.Max(10, plantHeight) / 100;
		double conductance = (wind * Math.Pow(vonKarman, 2)) / (Math.Log((heightWeatherMeasurements - d * h) / (zm * h)) * Math.Log((heightWeatherMeasurements - d * h) / (zh * h)));
		
		
		if (isWindVpDefined == 1)
		{
			evapoTranspirationPenman = evapoTranspirationPriestlyTaylor / Alpha + 1000 * ((rhoDensityAir * specificHeatCapacityAir * VPDair * conductance) / (lambdaV * (hslope + psychrometricConstant)));
			evapoTranspiration = evapoTranspirationPenman;
		}
		else
		{
			evapoTranspiration = evapoTranspirationPriestlyTaylor;
		}
		
		double potentialTranspiration= evapoTranspiration * (1 - tau);
		
		double soilEvaporation = Math.Min(diffusionLimitedEvaporation, energyLimitedEvaporation);
		
		double potentialEvapoTranspiration = potentialTranspiration + soilEvaporation;
		
		double soilHeatFlux = tau * netRadiationEquivalentEvaporation - soilEvaporation;
		
		double cropHeatFlux = netRadiationEquivalentEvaporation - soilHeatFlux - potentialTranspiration;
		
		double minCanopyTemperature = minTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000);
		double maxCanopyTemperature = maxTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000);

		
	}
	public static void Main(string[] args){}
	
}


