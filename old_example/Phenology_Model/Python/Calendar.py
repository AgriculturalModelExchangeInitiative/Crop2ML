def CalendarSet(moment, theDate, cumulTT, calendarMoments):
    if (moment == "ZC_00_Sowing"):
        calendarMoments.clear()
    CalendarDaily = {theDate:cumulTT}
    calendarMoments[moment]=CalendarDaily
    return calendarMoments
    
def IsMomentRegistred(moment, calendarMoments):
    if moment in calendarMoments.keys():
        return 1
    else: return 0

def CumulTTOf(moment,calendarMoments ):
    if moment in calendarMoments.keys(): return calendarMoments[moment].values()[0]
    

def cumulTTFrom(cumulField, fromc, CumulTTsinceStart, calendarMoments):
    return CumulTTsinceStart - thermalTimeFromBeginning(cumulField, fromc, calendarMoments );

def thermalTimeFromBeginning(cumulField, toc,  calendarMoments):
    if (IsMomentRegistred(toc,  calendarMoments)==1):
        return CumulTTOf(toc,  calendarMoments);


if __name__ == '__main__':
    pass
