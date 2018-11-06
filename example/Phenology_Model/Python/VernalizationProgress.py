'''
Created on 18 sept. 2018

@author: midingoy
'''
import Calendar
def CalculateVernalizationProgress( IsVernalizable,  Vernaprog,  DeltaTT,  MinTvern,  IntTvern,
                                                       VAI,  VBEE,  MinDL,  MaxDL,  DayLength,   MaxTvern,  LeafNumber,
                                                       PNini,  MinFinalNumber,  AMXLFNO,currentdate, cumulTT,calendar ):
    
    Vernaprog1 = Vernaprog
    MinFinalNumber1 = MinFinalNumber
    calendar1 = calendar.copy()
    if (IsVernalizable==1 and Vernaprog1 < 1):
        TT = DeltaTT;
        
        if (TT >= MinTvern and TT <= IntTvern):
            Vernaprog = Vernaprog1 + VAI * TT + VBEE;
            
        else:
            Vernaprog = Vernaprog1;
            
        if (TT > IntTvern):
            maxVernaProg = VAI * IntTvern + VBEE;
            DLverna = max(MinDL, min(MaxDL, DayLength));
            Vernaprog += max(0, maxVernaProg * (1 + ((IntTvern - TT) / (MaxTvern - IntTvern)) * ((DLverna - MinDL) / (MaxDL - MinDL))));
            
        primordno = 2.0 * LeafNumber + PNini;
        
        minLeafNumber = MinFinalNumber1;
        if (Vernaprog >= 1.0 or primordno >= AMXLFNO):
            
            
            MinFinalNumber = max(primordno, MinFinalNumber);
            calendar = Calendar.CalendarSet("EndVernalisation", currentdate,cumulTT, calendar1);
            Vernaprog = max(1, Vernaprog)
            
            
        else:
            
            potlfno = AMXLFNO - (AMXLFNO - minLeafNumber) * Vernaprog;
            if (primordno >= potlfno):
                
                
                MinFinalNumber = max((potlfno + primordno) / 2.0, MinFinalNumber);
                calendar = Calendar.CalendarSet("EndVernalisation", currentdate, cumulTT, calendar1);
                Vernaprog = max(1, Vernaprog);
                
            else:
                MinFinalNumber = MinFinalNumber1;

                
        
    else:
        Vernaprog= Vernaprog1;
        MinFinalNumber = MinFinalNumber1;
        calendar = calendar1.copy()
        

    return Vernaprog, MinFinalNumber, calendar


if __name__ == '__main__':
    pass