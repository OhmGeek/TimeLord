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
    public partial class Weeks : Form
    {
        public Timetable TT = null;


        public Weeks()
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
            
            Day newDay = new Day("New Day");
            
            TT.Week.Add(newDay);
            //Add Day

            RefreshDayList();
        }



        private void RefreshDayList()
        {
           
            list_days.DataSource = null;
            list_days.DataSource = TT.Week;

            if (TT.Week.Count != 0)
            {
                list_days.SelectedIndex = TT.Week.Count - 1;
            }

        }
        private void btn_removeDay_Click(object sender, EventArgs e)
        {
            if (TT.Week.Count == 0) return;
            TT.Week.Remove((Day)list_days.SelectedItem); //delete the selected item. Use (Day) to specify we are dealing with a Day type...
            RefreshDayList();
            //Remove Day
        }

        private void btn_addPeriod_Click(object sender, EventArgs e)
        {
            //Add Period
            
            Day selectedDay = (Day)list_days.SelectedItem;

            if (selectedDay == null) return;

            selectedDay.AddPeriod("Period X", "Times");
            ListViewItem lvi = new ListViewItem(Convert.ToString(selectedDay.GetBackPeriod().PeriodIdentifier));
            lvi.SubItems.Add(selectedDay.GetBackPeriod().PeriodDisplay);
            lvi.SubItems.Add(selectedDay.GetBackPeriod().PeriodDescription); //THIS IS INEFFICIENT, SO YOU SHOULD WRITE SOME MORE FUNCTIONS IN THE CLASS!!!!!
            list_periods.Items.Add(lvi);
            list_periods.Refresh();
        }

        private void btn_removePeriod_Click(object sender, EventArgs e)
        {
            //Remove Period Selected.
            if (list_days.SelectedItem == null) return;
            if (list_periods.SelectedItems.Count == 0) return;
            if (list_periods.SelectedItems[0] == null) return;

            int dayIndex = list_days.SelectedIndex;
            int periodIndex = list_periods.SelectedIndices[0];

            TT.Week[dayIndex].PeriodsInDay.RemoveAt(periodIndex);
            list_periods.Items.RemoveAt(periodIndex);

            //clear text boxes...
            tb_periodDisplay.Text = "";
            tb_periodDesc.Text = "";

        }

        private void listView_periods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_periods.Items.Count == 0) return;
            if (list_periods.SelectedItems.Count == 0) return;
            tb_periodDisplay.Text = list_periods.SelectedItems[0].SubItems[1].Text;
            tb_periodDesc.Text = list_periods.SelectedItems[0].SubItems[2].Text;
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
            Day selectedDay = (Day)list_days.SelectedItem;
            tb_dayName.Text = selectedDay.DayName;


            //display selected periods for that particular day.
           

            foreach (Period p in selectedDay.GetPeriodsInDay())
            {
                ListViewItem lvi = new ListViewItem(Convert.ToString(p.PeriodIdentifier));
                lvi.SubItems.Add(p.PeriodDisplay);
                lvi.SubItems.Add(p.PeriodDescription);
                list_periods.Items.Add(lvi);
            }


            
        }

        private void tb_dayName_TextChanged(object sender, EventArgs e)
        {
            Day selectedDay = (Day)list_days.SelectedItem;
            selectedDay.DayName = tb_dayName.Text;
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
            TT.Week[list_days.SelectedIndex].GetPeriodsInDay()[index].PeriodDisplay = tb_periodDisplay.Text;
            list_periods.SelectedItems[0].SubItems[1].Text = tb_periodDisplay.Text;


        }

        private void tb_periodDesc_TextChanged(object sender, EventArgs e)
        {
            //save the periodDesc to the real timetable format, not just the list.
              //save the periodDisplay to the real timetable format.
            if (list_periods.Items.Count == 0) return;
            if (list_periods.SelectedItems.Count == 0) return;
            if (list_days.SelectedItem == null) return;

            int index = list_periods.SelectedIndices[0]; //this is the index to access.
            TT.Week[list_days.SelectedIndex].GetPeriodsInDay()[index].PeriodDescription = tb_periodDesc.Text;
            list_periods.SelectedItems[0].SubItems[2].Text =tb_periodDesc.Text;




        }
    }
}
