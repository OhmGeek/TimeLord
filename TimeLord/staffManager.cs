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
    public partial class staffManager : Form
    {
        public Timetable currentTT = null;
        public staffManager()
        {
            InitializeComponent();
        }

        private void staffManager_Load(object sender, EventArgs e)
        {
            if (currentTT == null)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();

            }

            //populate the view:

            foreach (Teacher currentStaff in currentTT.Staff)
            {
                ListViewItem lvi = new ListViewItem(currentStaff.TeacherAbbreviation);
                lvi.SubItems.Add(currentStaff.TeacherName);
                list_staff.Items.Add(lvi);
            }



        }

        private void btn_addPeriod_Click(object sender, EventArgs e)
        {
            Teacher newTeach = new Teacher("Teacher Name", "XX");
            currentTT.Staff.Add(newTeach);

            ListViewItem lvi = new ListViewItem(newTeach.TeacherAbbreviation);
            lvi.SubItems.Add(newTeach.TeacherName);

            list_staff.Items.Add(lvi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void list_staff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_staff.Items.Count == 0) return;
            if (list_staff.SelectedItems.Count == 0) return;

            int index = list_staff.SelectedIndices[0];
            tb_code.Text = currentTT.Staff[index].TeacherAbbreviation;
            tb_name.Text = currentTT.Staff[index].TeacherName;

        }

        private void tb_code_TextChanged(object sender, EventArgs e)
        {
            if (list_staff.SelectedItems[0] == null)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }

            int index = list_staff.SelectedIndices[0];
            currentTT.Staff[index].TeacherAbbreviation = tb_code.Text;
            list_staff.Items[index].SubItems[0].Text = tb_code.Text;
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            if (list_staff.SelectedItems[0] == null)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }

            int index = list_staff.SelectedIndices[0];
            currentTT.Staff[index].TeacherName = tb_name.Text;
            list_staff.Items[index].SubItems[1].Text = tb_name.Text;
        }

        private void btn_removePeriod_Click(object sender, EventArgs e)
        {
            if (list_staff.Items.Count == 0) return;
            if (list_staff.SelectedItems.Count == 0) return;
            int index = list_staff.SelectedIndices[0];

            list_staff.Items.RemoveAt(index);
            currentTT.Staff.RemoveAt(index);

            int nextIndex = list_staff.Items.Count - 1;

            //if this was the last item, then clear textboxes.
            if (nextIndex < 0)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }

            list_staff.Items[nextIndex].Selected = true;

        }
    }
}
