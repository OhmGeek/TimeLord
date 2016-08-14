using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    
    public class FormClass
    {
        public string FormName;
        public bool visible;
        public int numberOfCols;
        public Rectangle formBounds;

        public FormClass(string formName)
        {
            FormName = formName;
        
            visible = true;
        }
        public override string ToString()
        {
             return this.FormName;
        }
    }
}
