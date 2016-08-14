using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    public class YearGroup
    {
        public String YearName;
        public List<FormClass> Forms;
        public bool Visible;
        public bool selectedForPrint;
        public Rectangle yearBounds;
       
        public YearGroup(string YearName)
        {
            this.YearName = YearName;
            this.Forms = new List<FormClass>();
            this.Visible = true;
            this.selectedForPrint = false;
            yearBounds = new Rectangle();
        }

        private int getIndexOf(string formname)
        {
            int i = 0;

            do
            {
                i++;
            } while ((i < Forms.Count) && (Forms[i].FormName != formname));

            if (Forms[i].FormName == formname) return i;
            return -1;
        }
        public FormClass GetBackForm()
        {
            if (Forms.Count == 0) return null;
            return Forms[Forms.Count - 1];

        }

        public void AddForm(string FormName)
        {
            FormClass nf = new FormClass(FormName);
            Forms.Add(nf);
        }

        public void DeleteForm(string FormName)
        {
            //find index using linear search
            Forms.RemoveAt(getIndexOf(FormName));
        }

        public override string ToString()
        {
            return this.YearName;
        }
    }
}
