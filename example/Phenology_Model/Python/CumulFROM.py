'''
Created on 17 sept. 2018

@author: midingoy
'''
import Calendar

def CalculateCumulFROM( calendar, SwitchMaize, cumulTT):
    
    cumulTTFromZC_65 = 0
    cumulTTFromZC_39 = 0
    cumulTTFromZC_91 = 0    
    if (Calendar.IsMomentRegistred("ZC_65_Anthesis", calendar) == 1):
        if (SwitchMaize == 0): cumulTTFromZC_65 = Calendar.cumulTTFrom(0, "ZC_65_Anthesis", cumulTT, calendar);
        else:cumulTTFromZC_65 =Calendar.cumulTTFrom(6, "ZC_65_Anthesis", cumulTT, calendar);
    
    if (Calendar.IsMomentRegistred("ZC_39_FlagLeafLiguleJustVisible", calendar) == 1):
        if (SwitchMaize == 0): cumulTTFromZC_39 = Calendar.cumulTTFrom(0, "ZC_39_FlagLeafLiguleJustVisible", cumulTT,calendar);
        else:cumulTTFromZC_39 = Calendar.cumulTTFrom(6, "ZC_39_FlagLeafLiguleJustVisible", cumulTT, calendar);
    
    if (Calendar.IsMomentRegistred("ZC_91_EndGrainFilling", calendar) == 1):
        if (SwitchMaize == 0): cumulTTFromZC_91 = Calendar.cumulTTFrom(0, "ZC_91_EndGrainFilling", cumulTT, calendar);
        else:cumulTTFromZC_91 = Calendar.cumulTTFrom(6,"ZC_91_EndGrainFilling", cumulTT, calendar);
    return cumulTTFromZC_65, cumulTTFromZC_39, cumulTTFromZC_91

