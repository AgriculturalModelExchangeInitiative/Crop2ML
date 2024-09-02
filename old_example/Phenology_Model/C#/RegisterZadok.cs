using System;
using System.Collections.Generic;

public static class RegisterZadok
{

	public static void CalculateRegisterZadok(double FinalLeafNumber, double LeafNumber,
	                  DateTime  currentdate, double cumulTT, double slopeTSFLN,
	               double intTSFLN, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar, double phase, double cumulTTFromZC_65, double Der )
	{
		Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar1 = new Dictionary <GrowthStage, Dictionary<DateTime, double>>(calendar);

		int hasZadokStageChanged;
		GrowthStage currentZadokStage;
		
		int roundedFinalLeafNumber = (int)(FinalLeafNumber + 0.5);

		if (HasReachedHaun(4, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_21_MainShootPlus1Tiller, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_21_MainShootPlus1Tiller, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_21_MainShootPlus1Tiller;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedHaun(5, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_22_MainShootPlus2Tiller, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_22_MainShootPlus2Tiller, currentdate,cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_22_MainShootPlus2Tiller;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedHaun(6, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_23_MainShootPlus3Tiller, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_23_MainShootPlus3Tiller,currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_23_MainShootPlus3Tiller;
			hasZadokStageChanged = 1;
		}
		else if (FinalLeafNumber > 0 && HasReachedHaun(slopeTSFLN * FinalLeafNumber - intTSFLN, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.TerminalSpikelet, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.TerminalSpikelet, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.TerminalSpikelet;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedFlagLeaf(4, roundedFinalLeafNumber, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_30_PseudoStemErection, calendar1) == 0)) 
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_30_PseudoStemErection, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_30_PseudoStemErection;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedFlagLeaf(3, roundedFinalLeafNumber, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_31_1stNodeDetectable, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_31_1stNodeDetectable, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_31_1stNodeDetectable;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedFlagLeaf(2, roundedFinalLeafNumber,LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_32_2ndNodeDetectable, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_32_2ndNodeDetectable, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_32_2ndNodeDetectable;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedFlagLeaf(1, roundedFinalLeafNumber, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_37_FlagLeafJustVisible, calendar1) == 0))
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_37_FlagLeafJustVisible, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_37_FlagLeafJustVisible;
			hasZadokStageChanged = 1;
		}
		else if (HasReachedFlagLeaf(0, roundedFinalLeafNumber, LeafNumber) && (Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible, calendar1) == 0))
		{
		}
		else if ((Calendar.IsMomentRegistred(GrowthStage.ZC_85_MidGrainFilling, calendar1) == 0) && phase == 4.5 && cumulTTFromZC_65 >= Der)//EndCellDivisionToEndGrainFill
		         
		{
			calendar = Calendar.CalendarSet(GrowthStage.ZC_85_MidGrainFilling, currentdate, cumulTT, calendar1);
			currentZadokStage = GrowthStage.ZC_85_MidGrainFilling;
			hasZadokStageChanged = 1;
		}
		else
		{
			hasZadokStageChanged = 0;
			calendar = new Dictionary <GrowthStage, Dictionary<DateTime, double>>(calendar1);
		}

	}
		

	private static bool HasReachedFlagLeaf(int stg, int roundedFinalLeafNumber, double leafNumber)
	{
		double trgt = roundedFinalLeafNumber - (double)stg;
		return (leafNumber >= trgt && trgt > 0);
	}

	private static bool HasReachedHaun(double stg, double leafNumber)
	{
		return (leafNumber >= stg);
	}
}