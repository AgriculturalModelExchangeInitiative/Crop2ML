using System;
using System.Collections.Generic;

public static class IsMomentRegistredModule
{
	public static void IsMomentRegistred(Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar)
	{
		int isMomentRegistredZC_39;
    	isMomentRegistredZC_39 = Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible, calendar);
    }
}