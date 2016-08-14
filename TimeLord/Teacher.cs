using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    public class Teacher
    {
        public string TeacherName;
        public string TeacherAbbreviation;
        public bool Visible;
        public bool selectedForPrint;
        public Rectangle staffBounds;
     
        public Teacher(string Name, string Abbrev)
        {
            this.TeacherName = Name;
            this.TeacherAbbreviation = Abbrev;
            this.Visible = true;
            selectedForPrint = false;
        }

        public override string ToString()
        {
            return this.TeacherAbbreviation;
        }
    }
}
