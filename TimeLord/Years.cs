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
    public partial class Years : Form
    {
        public Timetable TT = null;


        public Years()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //if text inside text box for week or period, save them first.
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btn_addDay_Click(object sender, EventArgs e)
        {


            if (TT.Years.Count() == 256)
            {
                MessageBox.Show("Operation couldn't be completed, because you cannot have more than 256 years.");
                return;
            }



            YearGroup newYear = new YearGroup("Year X");
            
            TT.Years.Add(newYear);
            //Add Day

            RefreshDayList();
        }



        private void RefreshDayList()
        {
           
            list_days.DataSource = null;
            list_days.DataSource = TT.Years;
          
            if (TT.Years.Count != 0)
            {
                list_days.SelectedIndex = TT.Years.Count - 1;
            }

        }
        private void btn_removeDay_Click(object sender, EventArgs e)
        {
            if (TT.Years.Count == 0) return;
            TT.Years.Remove((YearGroup)list_days.SelectedItem); //delete the selected item. Use (Day) to specify we are dealing with a Day type...
            RefreshDayList();
            //Remove Day
        }

        private void btn_addPeriod_Click(object sender, EventArgs e)
        {
            //Add Period
           
            YearGroup selectedYear = (YearGroup)list_days.SelectedItem;

            


            if (selectedYear == null) return;
            if (selectedYear.Forms.Count() == 256)
            {
                MessageBox.Show("Operation not allowed, as you cannot have more than 256 forms per year.");
                return;
            }
            selectedYear.AddForm("Form");
            ListViewItem lvi = new ListViewItem(selectedYear.GetBackForm().ToString());
             //THIS IS INEFFICIENT, SO YOU SHOULD WRITE SOME MORE FUNCTIONS IN THE CLASS!!!!!
            list_periods.Items.Add(lvi);
            list_periods.Refresh();
        }

        private void btn_removePeriod_Click(object sender, EventArgs e)
        {
            //Remove Period Selected.
            if (list_days.SelectedItem == null) return;
            if (list_periods.SelectedItems.Count == 0) return;
            if (list_periods.SelectedItems[0] == null) return;

            int yearIndex = list_days.SelectedIndex;
            int formIndex = list_periods.SelectedIndices[0];

            TT.Years[yearIndex].Forms.RemoveAt(formIndex);
            list_periods.Items.RemoveAt(formIndex);

            //clear text boxes...
            tb_periodDisplay.Text = "";
      

        }

        private void listView_periods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_periods.Items.Count == 0) return;
            if (list_periods.SelectedItems.Count == 0) return;
            tb_periodDisplay.Text = list_periods.SelectedItems[0].SubItems[0].Text;
           
        }

        private void Weeks_Load(object sender, EventArgs e)
        {
            if (TT == null)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }

            RefreshDayList();
        }

        private void list_days_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_periods.Items.Clear(); //clear the period list, ready for displaying new ones.
            if (list_days.SelectedItem == null)
            {
                return;
            }
            YearGroup selectedYear = (YearGroup)list_days.SelectedItem;
            tb_dayName.Text = selectedYear.YearName;



            //display selected periods for that particular day.
           

            foreach (FormClass p in selectedYear.Forms)
            {
                ListViewItem lvi = new ListViewItem(p.ToString());
                list_periods.Items.Add(lvi);
            }


            
        }

        private void tb_dayName_TextChanged(object sender, EventArgs e)
        {
            YearGroup selectedYear = (YearGroup)list_days.SelectedItem;
            selectedYear.YearName = tb_dayName.Text;
        }

        private void tb_dayName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshDayList();
      
            }
        }

        private void tb_periodDisplay_TextChanged(object sender, EventArgs e)
        {
            //save the periodDisplay to the real timetable format.
            if (list_periods.Items.Count == 0) return;
            if (list_periods.SelectedItems.Count == 0) return;
            if (list_days.SelectedItem == null) return;

            int index = list_periods.SelectedIndices[0]; //this is the index to access.
      
            

            TT.Years[list_days.SelectedIndex].Forms[index] = new FormClass(tb_periodDisplay.Text);
            list_periods.SelectedItems[0].SubItems[0].Text = tb_periodDisplay.Text;


        }

    }
}
