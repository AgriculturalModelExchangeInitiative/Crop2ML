using System;
using System.Collections.Generic;

public static class CalendarModule
{
	public static void CalculateCalendar(double phase,  DateTime currentdate, double cumulTT, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendar)
	{
    	if (Calendar.IsMomentRegistred(Phase.PreviousMoment(phase), calendar) == 0)
    		calendar = Calendar.CalendarSet(Phase.PreviousMoment(phase), currentdate, cumulTT, calendar);
	}
}
