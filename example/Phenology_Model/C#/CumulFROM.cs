using System;
using System.Collections.Generic;
public static class CumulFROM
{
	public static void CalculateCumulFROM(Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar, int SwitchMaize, double cumulTT)
	{
		double cumulTTFromZC_65;
		double cumulTTFromZC_39;
		double cumulTTFromZC_91;

		if (Calendar.IsMomentRegistred(GrowthStage.ZC_65_Anthesis, calendar) == 1)
		{
			if (SwitchMaize == 0)
			{
				cumulTTFromZC_65 = Calendar.cumulTTFrom(0, GrowthStage.ZC_65_Anthesis, cumulTT, calendar);
			}
			else
			{
				cumulTTFromZC_65 =Calendar.cumulTTFrom(6, GrowthStage.ZC_65_Anthesis, cumulTT, calendar);
			}
		}
		if (Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible, calendar) == 1)
		{
			if (SwitchMaize == 0)
			{
				cumulTTFromZC_39 = Calendar.cumulTTFrom(0, GrowthStage.ZC_39_FlagLeafLiguleJustVisible, cumulTT,calendar);
			}
			else
			{
				cumulTTFromZC_39 = Calendar.cumulTTFrom(6, GrowthStage.ZC_39_FlagLeafLiguleJustVisible, cumulTT, calendar);
			}
		}
		if (Calendar.IsMomentRegistred(GrowthStage.ZC_91_EndGrainFilling, calendar) == 1)
		{
			if (SwitchMaize == 0)
			{
				cumulTTFromZC_91 = Calendar.cumulTTFrom(0, GrowthStage.ZC_91_EndGrainFilling, cumulTT, calendar);
			}
			else
			{
				cumulTTFromZC_91 = Calendar.cumulTTFrom(6, GrowthStage.ZC_91_EndGrainFilling, cumulTT, calendar);
			}
		}
	}
}