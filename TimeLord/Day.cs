using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    public class Day
    {
        public string DayName;
        public List<Period> PeriodsInDay;
        public Rectangle dayBounds;
        public Day(string DayName)
        {
            this.PeriodsInDay = new List<Period>();
            this.DayName = DayName;
            this.dayBounds = new Rectangle();

        }

        public Period GetBackPeriod()
        {
            if (PeriodsInDay.Count == 0) return null;
            return PeriodsInDay[PeriodsInDay.Count - 1];
        }

        public List<Period> GetPeriodsInDay()
        {
            return PeriodsInDay;
        }

        public void AddPeriod(Period PeriodToAdd)
        {
            this.PeriodsInDay.Add(PeriodToAdd);
        }

        public void AddPeriod(String PeriodDisplay, String PeriodDescription)
        {
            Period PeriodToAdd = new Period(Convert.ToByte(PeriodsInDay.Count), PeriodDisplay, PeriodDescription);
            this.PeriodsInDay.Add(PeriodToAdd);
        }
        
        public override string ToString()
        {
            return this.DayName;
        }
    }
}
