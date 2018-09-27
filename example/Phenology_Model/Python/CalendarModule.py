import Calendar
import Phase
def CalculateCalendar(phase, currentdate, cumulTT,calendar):
    if (Calendar.IsMomentRegistred(Phase.PreviousMoment(phase), calendar) == 0):
        calendar = Calendar.CalendarSet(Phase.PreviousMoment(phase), currentdate, cumulTT, calendar)
    return calendar


