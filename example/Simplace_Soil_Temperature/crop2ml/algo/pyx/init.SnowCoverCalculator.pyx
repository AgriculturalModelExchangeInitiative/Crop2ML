cdef float TMEAN 
cdef float TAMPL 
cdef float DST 
if Albedo == float(0):
    #b'/taken from experimental fit from Thomas Gaiser, 2000, n=35, R2=0.97'
    pInternalAlbedo=0.0226 * log(cCarbonContent, 10) + 0.1502
else:
    pInternalAlbedo=Albedo
#b'/TMEAN = Mean daily air temperature at 2 m (\xc3\x82\xc2\xb0C)'
TMEAN=0.5 * (iTempMax + iTempMin)
#b'/TAMPL = Amplitude of daily air temperature at 2 m (\xc3\x82\xc2\xb0C)'
TAMPL=0.5 * (iTempMax - iTempMin)
#b'/DST = Bare soil surface temperature (\xc3\x82\xc2\xb0C)'
DST=TMEAN + (TAMPL * (iRadiation * (1 - pInternalAlbedo) - 14) / 20)
SoilSurfaceTemperature=DST
AgeOfSnow=cInitialAgeOfSnow
SnowWaterContent=cInitialSnowWaterContent