package sirius;
public class EnergyBalance {

    static void EnergyBalance(double extraSolarRadiation, double VPDair, double maxTair, double minTair, double vaporPressure,
            double solarRadiation, double tau, double deficitOnTopLayers,
            double hslope, double plantHeight, int isWindVpDefined, double wind,
            double stefanBoltzman, double lambdaV , double soilDiffusionConstant,
            double Alpha, double psychrometricConstant, double tauAlpha, double vonKarman,
            double heightWeatherMeasurements, double zm, double d, double zh, double rhoDensityAir,
            double specificHeatCapacityAir, double albedoCoefficient, double elevation) 
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
        double clearSkySolarRadiation = (0.75 + 2 * Math.pow(10, -5) * elevation) * extraSolarRadiation;
        double averageT = (Math.pow(maxTair + 273.16, 4) + Math.pow(minTair + 273.16, 4)) / 2;
        double surfaceEmissivity = (0.34 - 0.14 * Math.sqrt(vaporPressure / 10));
        double cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35);
        double Nolr = stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor;
        netRadiation = Nsr - Nolr;
        netOutGoingLongWaveRadiation = Nolr;

        netRadiationEquivalentEvaporation = netRadiation / lambdaV * 1000;

        if (deficitOnTopLayers / 1000 <= 0) {
            diffusionLimitedEvaporation = 8.3 * 1000;
        } else {
            if (deficitOnTopLayers / 1000 < 25) {
                diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000)) * 1000;
            } else {
                diffusionLimitedEvaporation = 0;
            }
        }

        evapoTranspirationPriestlyTaylor = Math.max((Alpha * hslope * (netRadiationEquivalentEvaporation) / (hslope + psychrometricConstant)), 0);


        if (tau < tauAlpha) {
            AlphaE = 1;
        } else {
            AlphaE = Alpha - ((Alpha - 1) * (1 - tau) / (1 - tauAlpha));
        }
        double energyLimitedEvaporation = (evapoTranspirationPriestlyTaylor / Alpha) * AlphaE * tau;

        double h = Math.max(10, plantHeight) / 100;
        double conductance = (wind * Math.pow(vonKarman, 2)) / (Math.log((heightWeatherMeasurements - d * h) / (zm * h)) * Math.log((heightWeatherMeasurements - d * h) / (zh * h)));

        if (isWindVpDefined == 1) {
            evapoTranspirationPenman = evapoTranspirationPriestlyTaylor / Alpha + 1000 * ((rhoDensityAir * specificHeatCapacityAir * VPDair * conductance) / (lambdaV * (hslope + psychrometricConstant)));
            evapoTranspiration = evapoTranspirationPenman;
        } else {
            evapoTranspiration = evapoTranspirationPriestlyTaylor;
        }

        double potentialTranspiration = evapoTranspiration * (1 - tau);

        double soilEvaporation = Math.min(diffusionLimitedEvaporation, energyLimitedEvaporation);

        double potentialEvapoTranspiration = potentialTranspiration + soilEvaporation;

        double soilHeatFlux = tau * netRadiationEquivalentEvaporation - soilEvaporation;

        double cropHeatFlux = netRadiationEquivalentEvaporation - soilHeatFlux - potentialTranspiration;

        double minCanopyTemperature = minTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000);
        double maxCanopyTemperature = maxTair + cropHeatFlux / ((rhoDensityAir * specificHeatCapacityAir * conductance / lambdaV) * 1000);

    }

    public static void main(String[] args) {
        // TODO code application logic here
    }

}
