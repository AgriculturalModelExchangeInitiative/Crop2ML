
using System;
using System.Collections.Generic;
public static class VernalizationProgress
	
{
	public static void CalculateVernalizationProgress(int IsVernalizable, double Vernaprog, double DeltaTT, double MinTvern, double IntTvern,
	                                                  double VAI, double VBEE, int MinDL, int MaxDL, int DayLength,  double MaxTvern, double LeafNumber,
	                                                  int PNini, double MinFinalNumber, double AMXLFNO, DateTime  currentdate, double cumulTT, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar )
	{
		Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar1 = new Dictionary <GrowthStage, Dictionary<DateTime, double>>(calendar);
		double Vernaprog1 = Vernaprog;
		double MinFinalNumber1 = MinFinalNumber;
		if (IsVernalizable==1 && Vernaprog1 < 1)
		{
			double TT = DeltaTT; // other sirius versions use previous temperature value

			if (TT >= MinTvern && TT <= IntTvern)
			{
				Vernaprog = Vernaprog1 + VAI * TT + VBEE;
			}
			else
			{
				Vernaprog = Vernaprog1;
			}
			if (TT > IntTvern)
			{
				double maxVernaProg = VAI * IntTvern + VBEE;
				double DLverna = Math.Max(MinDL, Math.Min(MaxDL, DayLength));
				Vernaprog += Math.Max(0, maxVernaProg * (1 + ((IntTvern - TT) / (MaxTvern - IntTvern)) * ((DLverna - MinDL) / (MaxDL - MinDL))));
			}

			
			double primordno = 2.0 * LeafNumber + PNini;
			double minLeafNumber = MinFinalNumber1;
			if (Vernaprog >= 1.0 || primordno >= AMXLFNO)
			{
				MinFinalNumber = Math.Max(primordno, MinFinalNumber); ;
				Calendar.CalendarSet(GrowthStage.EndVernalisation, currentdate,cumulTT, calendar1);
				Vernaprog = Math.Max(1, Vernaprog);
			}
			else
			{
				double potlfno = AMXLFNO - (AMXLFNO - minLeafNumber) * Vernaprog;
				if (primordno >= potlfno)
				{
					MinFinalNumber = Math.Max((potlfno + primordno) / 2.0, MinFinalNumber); ;
					Calendar.CalendarSet(GrowthStage.EndVernalisation, currentdate, cumulTT, calendar1);
					Vernaprog = Math.Max(1, Vernaprog);
				}
				else {
					MinFinalNumber = MinFinalNumber1;
				}
			}
		}
		else
		{
			Vernaprog= Vernaprog1;
			MinFinalNumber = MinFinalNumber1;
		}
		
	}

}


