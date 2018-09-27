import Calendar

def CalculateRegisterZadok( FinalLeafNumber, LeafNumber, currentdate, cumulTT,  slopeTSFLN,intTSFLN, calendar, phase,  cumulTTFromZC_65,  Der, currentZadokStage ):
    calendar1 = calendar.copy()
    
    roundedFinalLeafNumber = int(FinalLeafNumber+0.5);
    
    if (HasReachedHaun(4, LeafNumber) and (Calendar.IsMomentRegistred("ZC_21_MainShootPlus1Tiller", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_21_MainShootPlus1Tiller", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_21_MainShootPlus1Tiller";
        hasZadokStageChanged = 1;
    elif (HasReachedHaun(5, LeafNumber) and (Calendar.IsMomentRegistred("ZC_22_MainShootPlus2Tiller", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_22_MainShootPlus2Tiller", currentdate,cumulTT, calendar1);
        currentZadokStage = "ZC_22_MainShootPlus2Tiller";
        hasZadokStageChanged = 1;
        
    elif (HasReachedHaun(6, LeafNumber) and (Calendar.IsMomentRegistred("ZC_23_MainShootPlus3Tiller", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_23_MainShootPlus3Tiller",currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_23_MainShootPlus3Tiller";
        hasZadokStageChanged = 1;
        
        
    elif (FinalLeafNumber > 0 and HasReachedHaun(slopeTSFLN * FinalLeafNumber - intTSFLN, LeafNumber) and (Calendar.IsMomentRegistred("TerminalSpikelet", calendar1) == 0)):
        calendar = Calendar.CalendarSet("TerminalSpikelet", currentdate, cumulTT, calendar1);
        currentZadokStage = "TerminalSpikelet";
        hasZadokStageChanged = 1;
        
    elif (HasReachedFlagLeaf(4, roundedFinalLeafNumber, LeafNumber) and (Calendar.IsMomentRegistred("ZC_30_PseudoStemErection", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_30_PseudoStemErection", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_30_PseudoStemErection";
        hasZadokStageChanged = 1;
    elif (HasReachedFlagLeaf(3, roundedFinalLeafNumber, LeafNumber) and (Calendar.IsMomentRegistred("ZC_31_1stNodeDetectable", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_31_1stNodeDetectable", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_31_1stNodeDetectable";
        hasZadokStageChanged = 1;
        
    elif (HasReachedFlagLeaf(2, roundedFinalLeafNumber,LeafNumber) and (Calendar.IsMomentRegistred("ZC_32_2ndNodeDetectable", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_32_2ndNodeDetectable", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_32_2ndNodeDetectable";
        hasZadokStageChanged = 1;
        
    elif (HasReachedFlagLeaf(1, roundedFinalLeafNumber, LeafNumber) and (Calendar.IsMomentRegistred("ZC_37_FlagLeafJustVisible", calendar1) == 0)):
        calendar = Calendar.CalendarSet("ZC_37_FlagLeafJustVisible", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_37_FlagLeafJustVisible";
        hasZadokStageChanged = 1;
    elif ((Calendar.IsMomentRegistred("ZC_85_MidGrainFilling", calendar1) == 0) and phase == 4.5 and cumulTTFromZC_65 >= Der):
                 
        calendar = Calendar.CalendarSet("ZC_85_MidGrainFilling", currentdate, cumulTT, calendar1);
        currentZadokStage = "ZC_85_MidGrainFilling";
        hasZadokStageChanged = 1;
    else:
        hasZadokStageChanged = 0;
        calendar = calendar1.copy();
            
    #print currentZadokStage
    
    return hasZadokStageChanged, currentZadokStage, calendar

def HasReachedFlagLeaf(stg, roundedFinalLeafNumber, leafNumber):
    trgt = roundedFinalLeafNumber - stg;
    return (leafNumber >= trgt and trgt > 0);

def HasReachedHaun(stg, leafNumber):
    return (leafNumber >= stg);
