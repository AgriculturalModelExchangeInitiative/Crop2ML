using System;
using System.Collections.Generic;
public static class UpdateLeafFlag
{
	public static void CalculateUpdateLeafFlag(double LeafNumber, int HasFlagLeafLiguleAppeared,
	                       double FinalLeafNumber , Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar, DateTime currentdate, double cumulTT, double phase )
	{
		Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar1 = new Dictionary <GrowthStage, Dictionary<DateTime, double>>(calendar);

        if (phase >= 1 && phase< 4)
        {
			if (LeafNumber > 0)
			{
				if (HasFlagLeafLiguleAppeared == 0 && (FinalLeafNumber > 0 && LeafNumber >= FinalLeafNumber))
				{
					HasFlagLeafLiguleAppeared = 1;
					if  (Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible, calendar1) == 0)
						Calendar.CalendarSet(GrowthStage.ZC_39_FlagLeafLiguleJustVisible, currentdate, cumulTT, calendar);
				}
			}
			else
			{
				HasFlagLeafLiguleAppeared = 0;
				calendar = new Dictionary <GrowthStage, Dictionary<DateTime, double>>(calendar1);

			}
       	 }
	}

}