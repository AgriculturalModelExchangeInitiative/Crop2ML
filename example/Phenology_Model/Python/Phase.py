'''
Created on 17 sept. 2018

@author: midingoy
'''

def getPhaseAsString(p):
    if (p < 0): return "BeforeSowing";
    elif (p >= 0 and p < 1): return "SowingToEmergence";
    elif (p >= 1 and p < 2):  return "EmergenceToFloralInitiation";
    elif (p >= 2 and p < 3): return "FloralInitiationToHeading";
    elif (p >= 3 and p < 4):  return "HeadingToAnthesis";
    elif  (p >= 4 and p <4.5) :return "AnthesisToEndCellDivision";
    elif  (p >= 4.5 and p < 5):return "EndCellDivisionToEndGrainFill";
    elif  (p >= 5 and p < 6):return "EndGrainFillToMaturity";
    elif  (p >= 6 and p < 7):return "AllOver";
    else:return "NoPhase";

def PreviousMoment(phaseValue):
    if (phaseValue < 0):return "Unknown";
    elif (phaseValue >= 0 and phaseValue < 1): return "ZC_00_Sowing";
    elif (phaseValue >= 1 and phaseValue < 2):return "ZC_10_Emergence";
    elif (phaseValue >= 2 and phaseValue < 3):return "FloralInitiation";
    elif (phaseValue >= 3 and phaseValue < 4):return "Heading";
    elif (phaseValue == 4):return "ZC_65_Anthesis";
    elif (phaseValue == 4.5):return "ZC_75_EndCellDivision";
    elif (phaseValue >= 5 and phaseValue < 6):return "ZC_91_EndGrainFilling";
    elif (phaseValue >= 6 and phaseValue < 7):return "ZC_92_Maturity";



if __name__ == '__main__':
    pass