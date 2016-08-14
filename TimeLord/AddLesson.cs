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
    public enum AddLessonMode
    {
        Add,Edit,Delete
    }
    public partial class AddLesson : Form
    {
        public Timetable currentTT = null;
        private bool dataLoaded = false;
        public AddLessonMode mode = AddLessonMode.Add; //this is the default.
        private Lesson loadedLesson = null;
        public AddLesson()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void LoadData() {

            if (currentTT == null)
            {
                this.Close();

            }
            cb_day.DataSource = currentTT.Week;
            cb_teacherCode.DataSource = currentTT.Staff;
            cb_subjectCode.DataSource = currentTT.Subjects;
            cb_room.DataSource = currentTT.Rooms;
            cb_yearGroup.DataSource = currentTT.Years;
            dataLoaded = true;
        }
        public void LoadLesson(Lesson lessonToLoad)
        {
            this.loadedLesson = lessonToLoad;
        }
        
        private void AddLesson_Load(object sender, EventArgs e)
        {
            if (!dataLoaded) LoadData();


            if (mode == AddLessonMode.Edit)
            {
                this.Text = "Edit Lesson";
                cb_day.SelectedIndex = loadedLesson.DayIndex;
                cb_yearGroup.SelectedIndex = loadedLesson.YearIndex;
                cb_room.SelectedIndex = cb_room.FindString(loadedLesson.RoomCode);
                cb_teacherCode.SelectedIndex = cb_teacherCode.FindString(loadedLesson.TeacherAbbreviation);
                cb_subjectCode.SelectedIndex = cb_subjectCode.FindString(loadedLesson.SubjectAbbreviation);
                cb_class.SelectedIndex = loadedLesson.FormIndex;
                cb_periodStart.SelectedIndex = loadedLesson.PeriodIndex;
            }
        }

        private void cb_day_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cb_day.SelectedIndex != -1) || (cb_day.SelectedIndex <currentTT.Week.Count))
            {
                cb_periodStart.DataSource = null;
                
                cb_periodStart.DataSource = currentTT.Week[cb_day.SelectedIndex].PeriodsInDay;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void cb_yearGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_yearGroup.SelectedIndex != -1)
            {
                cb_class.DataSource = null;
                cb_class.DataSource = currentTT.Years[cb_yearGroup.SelectedIndex].Forms;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!currentTT.IsFinalised()) currentTT.Finalise(); //finalises structure, so that we can't change it.
            if (cb_periodStart.Text == "") return;
            if (cb_noOfPeriods.Text == "") return;
            if ((cb_periodStart.SelectedIndex < 0) || (cb_periodStart.SelectedIndex > 255)) return;

            byte periodIndex = Convert.ToByte(cb_periodStart.SelectedIndex);
            byte dayIndex = Convert.ToByte(cb_day.SelectedIndex);
            byte noOfPeriods = Convert.ToByte(cb_noOfPeriods.Text);
            string teacherCode = cb_teacherCode.Text;
            string subjectCode = cb_subjectCode.Text;
            string roomCode = cb_room.Text;
            byte yearIndex = Convert.ToByte(cb_yearGroup.SelectedIndex);
            byte formIndex = Convert.ToByte(cb_class.SelectedIndex);
            byte homeworkAmount = Convert.ToByte(num_hwkAmount.Value);
            string message = "";
            string m2 = "";

            if (currentTT.IsClassClash(dayIndex, periodIndex, yearIndex, formIndex, out message))
                m2 += message + Environment.NewLine;
            if (currentTT.IsRoomClash(dayIndex, periodIndex, cb_room.SelectedIndex, out message))
                m2 += message;
            if (currentTT.IsStaffClash(dayIndex, periodIndex, cb_teacherCode.SelectedIndex, out message))
                m2 += message;

            if (m2 != "")
            {
                MessageBox.Show(m2);
                return;
            }

            if (mode == AddLessonMode.Edit) currentTT.DeleteLesson(loadedLesson);

            currentTT.AddLesson(dayIndex, periodIndex, noOfPeriods, subjectCode, teacherCode, roomCode, yearIndex, formIndex,homeworkAmount,ck_locked.Checked,ck_invisible.Checked);
            
            this.Close();
        }

        private void ck_locked_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
