using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLord
{
    public partial class VisibleYearsAndForms : Form
    {
        public Timetable TT = null;
        public VisibleYearsAndForms()
        {
            InitializeComponent();
        }

        //private void RefreshDayList()
        //{

        //    list_years.DataSource = null;
        //    list_years.DataSource = TT.Years;

        //    if (TT.Years.Count != 0)
        //    {
        //        list_years.SelectedIndex = TT.Years.Count - 1;

        //    }
        //    for (int i = 0; i < TT.Years.Count; i++)
        //        if (TT.Years[i].Visible) list_years.SetItemCheckState(i, CheckState.Checked);

        //}
        private void VisibleYearsAndForms_Load(object sender, EventArgs e)
        {
            if (TT == null)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }

            RefreshDayList();

        }

        private void RefreshDayList()
        {
            list_years.Items.Clear();
            for (int YearIndex = 0; YearIndex < TT.Years.Count; YearIndex++)
            {
                list_years.Items.Add(TT.Years[YearIndex], TT.Years[YearIndex].Visible);
            }

        }


        private void list_days_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_forms.Items.Clear();
            if (list_years.SelectedIndex != -1)
            {
                YearGroup selectedYear = (YearGroup)list_years.SelectedItem;
                for (int i = 0; i < selectedYear.Forms.Count; i++)
                {
                    list_forms.Items.Add(selectedYear.Forms[i].FormName,selectedYear.Forms[i].visible); 
                }
                list_forms.Refresh();
            }
        }

        private void btn_allDay_Click(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
       
        private void list_years_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            TT.Years[e.Index].Visible = (e.NewValue == CheckState.Checked);

            if (e.NewValue == CheckState.Unchecked)
            {
                foreach (FormClass form in TT.Years[e.Index].Forms)
                    form.visible = false;
            }
            else
            {
                foreach (FormClass form in TT.Years[e.Index].Forms)
                    form.visible = true;
            }


        }

        private void list_forms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (list_years.SelectedIndex < 0) return;
            TT.Years[list_years.SelectedIndex].Forms[e.Index].visible = (e.NewValue == CheckState.Checked);

            


        }
    }
}
