'''
Created on 18 sept. 2018

@author: midingoy
'''

def CalculateUpdatePhase(IsVernalizable,  Dse,  PFLLAnth,  Dcd, Dgf,  Degfm,  MaxDL,  SLDL,  IgnoreGrainMaturation, PHEADANTH,  SwitchMaize,choosePhyllUse,
                                             P, DayLength,cumulTT,  Vernaprog,  MinFinalNumber,LeafNumber,  GrainCumulTT,  GAI,  isMomentRegistredZC_39,
                                             cumulTTFromZC_39,  phaseValue,  cumulTTFromZC_91,  Fixphyll,  Phyllochron,  hasLastPrimordiumAppeared, FinalLeafNumber):
    phaseValue1 = phaseValue;
    if (phaseValue1 >= 0 and phaseValue1 < 1):
        if (SwitchMaize==0):
            if (cumulTT >= Dse):
                phaseValue = 1;#Emergence
            else:
                phaseValue = phaseValue1;
        else:
            if (cumulTT >= Dse):
                phaseValue= 1;#Emergence
            else:
                phaseValue = phaseValue1;
                
    elif (phaseValue1 >= 1 and phaseValue1 < 2):#EmergenceToFloralInitiation
        if ((IsVernalizable==1 and Vernaprog >= 1) or (IsVernalizable==0)):
            if (SwitchMaize==0):
                if (DayLength > MaxDL):
                    FinalLeafNumber = MinFinalNumber;
                    hasLastPrimordiumAppeared = 1;
                else:
                    appFLN = MinFinalNumber + SLDL * (MaxDL - DayLength);
                    
                        # calculation of final leaf number from daylength at inflexion plus 2 leaves
                    if (appFLN / 2.0 <= LeafNumber):
                        FinalLeafNumber = appFLN;
                        hasLastPrimordiumAppeared =1;
                    else:
                        phaseValue = phaseValue1;

            else:
                hasLastPrimordiumAppeared = 1;
                

                #CheckFloralInitiation
            if (hasLastPrimordiumAppeared==1):
                
                    phaseValue = 2;#Floralinitiation
                
        else:
            phaseValue = phaseValue1;
            
        
    elif (phaseValue1 >= 2 and phaseValue1 < 4):#FloralInitiationToAnthesis
        if (isMomentRegistredZC_39==1):
            
                #calculate the heading date
            if (phaseValue1 < 3):
                ttFromLastLeafToHeading = 0.0;
                if(choosePhyllUse=="Default"): ttFromLastLeafToHeading =(PFLLAnth - PHEADANTH) * Fixphyll;
                elif (choosePhyllUse == "PTQ"): ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * Phyllochron;
                elif (choosePhyllUse == "Test"): ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * P;

                if (cumulTTFromZC_39 >= ttFromLastLeafToHeading):
                    phaseValue = 3;
                else:
                    phaseValue = phaseValue1;
            else:
                phaseValue = phaseValue1
                                 
                    #CheckAnthesis;
            ttFromLastLeafToAnthesis =0.0;
            if (choosePhyllUse == "Default"): ttFromLastLeafToAnthesis = PFLLAnth * Fixphyll;
            elif (choosePhyllUse == "PTQ"): ttFromLastLeafToAnthesis = PFLLAnth * Phyllochron;
            elif (choosePhyllUse == "Test"): ttFromLastLeafToAnthesis = PFLLAnth * P
        
            
            if (cumulTTFromZC_39 >= ttFromLastLeafToAnthesis):
                phaseValue = 4;#Anthesis
                
        else:
            phaseValue = phaseValue1;
            
    elif (phaseValue1 == 4):#AnthesisToEndCellDivision
            
                #CheckEndCellDivision
        if (GrainCumulTT >= Dcd):
                
            phaseValue = 4.5;#EndCellDivision
                
        else:
                
            phaseValue = phaseValue1;
                
            
    elif (phaseValue1 == 4.5):#EndCellDivisionToEndGrainFill
            
                # CheckEndGrainFilling
        if (GrainCumulTT >= Dgf or GAI <= 0):
                
            phaseValue = 5;#End of grain filling
                
        else:
                
            phaseValue = phaseValue1;
                

            
    elif (phaseValue1 >= 5 and phaseValue1 < 6):#EndGrainFillToMaturity
            
                #CheckMaturity
                #/<Comment>To enable ignoring grain maturation duration</Comment>
        LocalDegfm = Degfm;
        if (IgnoreGrainMaturation==1): LocalDegfm = -1;
                
        if (cumulTTFromZC_91 >= LocalDegfm ):
                
            phaseValue = 6; #maturity
            
                
        else:
                
            phaseValue= phaseValue1;
                

            
    elif (phaseValue1>= 6 and phaseValue1 < 7):
            
        phaseValue = phaseValue1;
    
    
    
    return FinalLeafNumber, phaseValue, hasLastPrimordiumAppeared



if __name__ == '__main__':
    pass