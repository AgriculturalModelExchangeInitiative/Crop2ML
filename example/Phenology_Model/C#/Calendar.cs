using System;
using System.Collections.Generic;
using System.Linq;

public static class Calendar
{


	public static  Dictionary <GrowthStage, Dictionary<DateTime, double>> CalendarSet(GrowthStage moment, DateTime theDate, double cumulTT, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendarMoments)
	{
		if (moment == GrowthStage.ZC_00_Sowing)
		{
			calendarMoments.Clear();
		}
				
		Dictionary <DateTime, double>  CalendarDaily = new Dictionary <DateTime, double>();
		
		CalendarDaily.Add(theDate, cumulTT);
		
		calendarMoments.Add(moment,CalendarDaily);
		
		return calendarMoments;
	}
	

	public static int IsMomentRegistred(GrowthStage moment, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendarMoments)
	{
		
		return calendarMoments.ContainsKey(moment) ? 1 : 0 ;
	}

	public static double CumulTTOf(GrowthStage moment, Dictionary <GrowthStage, Dictionary<DateTime, double>> calendarMoments )
	{
		
		return (calendarMoments.ContainsKey(moment)) ? calendarMoments[moment].Values.ToArray()[0]: 0;
	}
	
	public static double cumulTTFrom(int cumulField, GrowthStage from, double CumulTTsinceStart,  Dictionary <GrowthStage, Dictionary<DateTime, double>> calendarMoments)
	{
		return CumulTTsinceStart - thermalTimeFromBeginning(cumulField, from, calendarMoments );
	}
	
	private static double thermalTimeFromBeginning(int cumulField, GrowthStage to,  Dictionary <GrowthStage, Dictionary<DateTime, double>> calendarMoments)
	{
		if (IsMomentRegistred(to,  calendarMoments)==1)
		{
			return CumulTTOf(to,  calendarMoments);
		}
		throw new ArgumentException("the moment " + to + " is not yet registred.");
	}
}

