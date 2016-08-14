using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLord
{
    public class Subject
    {
        public string SubjectName;
        public string SubjectAbbreviation;
        public bool selectedForPrinting;
        public Subject(string SubjectName, string Subjectabbrev)
        {
            this.SubjectName = SubjectName;
            this.SubjectAbbreviation = Subjectabbrev;
            this.selectedForPrinting = false;
        }

        public override string ToString()
        {
            return this.SubjectAbbreviation;
        }
    }
}
