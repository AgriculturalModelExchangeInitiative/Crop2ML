import Calendar

def CalculateUpdateLeafFlag( LeafNumber, HasFlagLeafLiguleAppeared, FinalLeafNumber ,  calendar,  currentdate, cumulTT, phase ):
    
    if (phase >= 1 and phase< 4):
        calendar1 = calendar.copy()
        if (LeafNumber > 0):
            if (HasFlagLeafLiguleAppeared == 0 and (FinalLeafNumber > 0 and LeafNumber >= FinalLeafNumber)):
                HasFlagLeafLiguleAppeared = 1;
                if  (Calendar.IsMomentRegistred("ZC_39_FlagLeafLiguleJustVisible", calendar1) == 0):
                    calendar = Calendar.CalendarSet("ZC_39_FlagLeafLiguleJustVisible", currentdate, cumulTT, calendar);
        else:
            HasFlagLeafLiguleAppeared = 0;
            calendar = calendar1.copy();
    return calendar, HasFlagLeafLiguleAppeared

    
