using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    public class Period
    {
        public byte PeriodIdentifier;
        public string PeriodDisplay;
        public string PeriodDescription;
        public Rectangle periodBounds;
        public Period(byte PeriodIdentifier, string display, string Description)
        {
            this.PeriodIdentifier = PeriodIdentifier;
            this.PeriodDisplay = display;
            this.PeriodDescription = Description;
        }

        public override string ToString()
        {
            return PeriodDisplay;
        }
    }
}
