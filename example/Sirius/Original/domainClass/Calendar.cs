using System;
using System.Collections.Generic;

namespace SiriusQualityPhenology
{
    ///<summary>
    ///This class register crop phase changes (see GrowthStage). For each stage it stores the date as well as a copy of all actual cumulated thermal times 
    ///</summary>
    public class Calendar
    {
        ///<summary>
        ///Informations saved when crop phase changes.
        ///</summary>
        public class CalendarDailyInfo : IDisposable
        {
            ///<summary>
            ///The date when the change occurs.
            ///</summary>
            public readonly DateTime Date;

            ///<summary>
            ///The copy of actual thermal times.
            ///</summary>
            public List<double> CumulTT;

            ///<summary>
            ////Create a new CalendarDailyInfo.
            ///</summary>
            ///<param name="date">The date when the crop phase changes.</param>
            ///<param name="cumulTT">The actual sensor thermal times.</param>
            public CalendarDailyInfo(DateTime date, List<double> cumulTT)
            {
                Date = date;
                CumulTT = new List<double>();
                for (int i = 0; i < cumulTT.Count; i++) CumulTT.Add(cumulTT[i]);

                    
            }

            #region IDisposable Members

            public void Dispose()
            {
                CumulTT = null;
            }

            #endregion
        }

        ///<summary>
        ///The dictionary registers the date when the crop phase change.
        ///</summary>
        public Dictionary<GrowthStage, CalendarDailyInfo> calendarMoments;

        ///<summary>
        ///Create a new Calendar
        ///</summary>
        public Calendar()
        {
            calendarMoments = new Dictionary<GrowthStage, CalendarDailyInfo>();
            LastGrowthStageSet = GrowthStage.Unknown;
        }

        ///<summary>
        ///Copy constructor.
        ///</summary>
        ///<param name="toCopy">The calendar to copy.</param>
        public Calendar(Calendar toCopy)
        {
            calendarMoments = new Dictionary<GrowthStage, CalendarDailyInfo>(toCopy.calendarMoments);
            LastGrowthStageSet = toCopy.LastGrowthStageSet;
        }

        ///<summary>
        ///Init this Calendar for a new run.
        ///</summary>
        public void Init()
        {
            LastGrowthStageSet = GrowthStage.Unknown;
        }

        ///<summary>
        ///Register a moment.
        ///</summary>
        ///<param name="moment">The moment.</param>
        ///<param name="theDate">The date when the moment appeared.</param>
        public void Set(GrowthStage moment, DateTime theDate, List<double> cumulTT)
        {
            if (moment == GrowthStage.ZC_00_Sowing)
            {
                calendarMoments.Clear();
            }

            LastGrowthStageSet = moment;
            calendarMoments.Add(moment, new CalendarDailyInfo(theDate, cumulTT));
        }

        public GrowthStage LastGrowthStageSet { get;  set; }

        ///<summary>
        ///Check if a moment already appeared.
        ///</summary>
        ///<param name="moment">the moment to check</param>
        ///<returns>True if the moment is registred, false otherwise.</returns>
        public int IsMomentRegistred(GrowthStage moment)
        {
            return calendarMoments.ContainsKey(moment) ? 1 : 0 ;
        }

        ///<summary>
        ///Get the date of a moment or null if the moment is not registred.
        ///</summary>
        ///<param name="moment">The registred moment</param>
        ///<returns>The date of the given moment if registred or null.</returns>
        public DateTime? this[GrowthStage moment]
        {
            get { return (calendarMoments.ContainsKey(moment)) ? new DateTime?(calendarMoments[moment].Date) : null; }
        }

        ///<summary>
        ///Get the array of thermal times at a given moment.
        ///</summary>
        ///<param name="moment">The given moment</param>
        ///<returns>The array of thermal times of the given registred moment or null.</returns>
        public List<double> CumulTTOf(GrowthStage moment)
        {
            return (calendarMoments.ContainsKey(moment)) ? calendarMoments[moment].CumulTT : null;
        }

        ///<summary>Get the thermal time from a moment</summary>
        ///<param name="cumulField">The field to consider.</param>
        ///<param name="from">The moment to start the cumul.</param>
        ///<param name="CumulTT">the cumulTT since the start</param>
        ///<returns>The thermal time from the moment.</returns>
        public double cumulTTFrom(int cumulField, GrowthStage from, double CumulTTsinceStart)
        {
            return CumulTTsinceStart - thermalTimeFromBeginning(cumulField, from);
        }

        ///<summary>Get the cumul thermal time from beginning of the simulation to a moment</summary>
        ///<param name="cumulField">The type of thermal time to get.</param>
        ///<param name="to">The moment to stop cumul.</param>
        ///<returns>The cumul of thermal times from the beginning of the simulation to the day of the specified moment.</returns>
        private double thermalTimeFromBeginning(int cumulField, GrowthStage to)
        {
            if (IsMomentRegistred(to)==1)
            {
                return CumulTTOf(to)[(int)cumulField];
            }
            throw new ArgumentException("the moment " + to + " is not yet registred.");
        }
    }
}